using Transactions.AccessData.Interfaces;
using Transactions.Application.Interfaces;
using Transactions.Domain.DTOs;
using Transactions.Domain.Entities;
using Transactions.Domain.Models;

namespace Transactions.Application.Services
{
    public class MovementHistoryService : IMovementHistoryService
    {
        private readonly IMovementHistoryRepository _movementHistoryRepository;
        private readonly IEntityMapper _mapper;
        private readonly IOperationRepository _operationRepository;
        private readonly ITransactionStateRepository _transactionStateRepository;

        public MovementHistoryService(
            IMovementHistoryRepository repository,
            IEntityMapper mapper,
            IOperationRepository operationRepository,
            ITransactionStateRepository transactionStateRepository)
        {
            _movementHistoryRepository = repository;
            _mapper = mapper;
            _operationRepository = operationRepository;
            _transactionStateRepository = transactionStateRepository;

        }

        public async Task<List<MovementHistoryDto>> GetMovementsByAccountId(Guid accountId)
        {
            var movementsList = await _movementHistoryRepository.GetMovementsByAccountId(accountId);

            if (movementsList is null)
                return null;

            var mappedMovements = new List<MovementHistoryDto>();

            foreach (var movement in movementsList)
            {
                mappedMovements.Add(_mapper.ToMovementHistoryDto(movement));
            }

            return mappedMovements;
        }

        public async Task<List<MovementHistoryDto>> GetMovementsByDay(Guid accountId, DateTime date)
        {
            var movementsList = await _movementHistoryRepository.GetMovementsByDay(accountId, date);

            if (movementsList is null)
                return null;

            var mappedMovements = new List<MovementHistoryDto>();

            foreach (var movement in movementsList)
            {
                mappedMovements.Add(_mapper.ToMovementHistoryDto(movement));
            }

            return mappedMovements;
        }

        public async Task<int> AddMovement(MovementHistory movementHistory)
        {
            return await _movementHistoryRepository.AddMovement(movementHistory);
        }

        public async Task<bool> AddMovementFromTransaction(Transaction transaction, AccountModel emisorAccountData, AccountModel receiverAccountData)
        {
            var operationType = await _operationRepository.GetOperationTypeById(transaction.OperationTypeId);
            var stateTrx = await _transactionStateRepository.GetStateById(transaction.State);

            if (operationType == null || stateTrx == null) return false;

            var transactionHistory = new MovementHistory
            {
                FromAccountId = transaction.FromAccountId,
                ToAccountId = transaction.ToAccountId,
                OperationType = operationType.Name,
                DateTimeTransaction = transaction.DateTime,
                AmountTransaction = transaction.Amount,
                Currency = emisorAccountData.Currency,
                FromCbu = emisorAccountData.Cbu,
                ToCbu = receiverAccountData.Cbu,
                FullNameEmisorCustomer = emisorAccountData.FullNameCustomer,
                FullNameReceiverCustomer = receiverAccountData.FullNameCustomer,
                DniEmisorCustomer = emisorAccountData.Dni,
                DniReceiverCustomer = receiverAccountData.Dni,
                CuilEmisorCustomer = emisorAccountData.Cuil,
                CuilReceiverCustomer = receiverAccountData.Cuil,
                ResultingStateOfTransaction = stateTrx.Name
            };

            var result = await AddMovement(transactionHistory);

            if (result == 0)
                return false;

            return true;
        }

        public async Task<bool> AddMovementRejected(decimal amount, int operationTypeId, Guid fromAccountId, Guid toAccountId, AccountModel emisorAccountData, AccountModel receiverAccountData)
        {
            var operationType = await _operationRepository.GetOperationTypeById(operationTypeId);
            var stateTrx = await _transactionStateRepository.GetStateById(2);

            if (operationType == null || stateTrx == null) return false;

            if (operationType == null) return false;

            var transactionHistory = new MovementHistory
            {
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
                OperationType = operationType.Name,
                DateTimeTransaction = DateTime.Now,
                AmountTransaction = amount,
                Currency = emisorAccountData.Currency,
                FromCbu = emisorAccountData.Cbu,
                ToCbu = receiverAccountData.Cbu,
                FullNameEmisorCustomer = emisorAccountData.FullNameCustomer,
                FullNameReceiverCustomer = receiverAccountData.FullNameCustomer,
                DniEmisorCustomer = emisorAccountData.Dni,
                DniReceiverCustomer = receiverAccountData.Dni,
                CuilEmisorCustomer = emisorAccountData.Cuil,
                CuilReceiverCustomer = receiverAccountData.Cuil,
                ResultingStateOfTransaction = stateTrx.Name
            };

            var result = await AddMovement(transactionHistory);

            if (result == 0)
                return false;

            return true;
        }
    }
}
