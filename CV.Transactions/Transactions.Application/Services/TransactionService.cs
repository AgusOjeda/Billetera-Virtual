using Transactions.AccessData.Interfaces;
using Transactions.Application.Interfaces;
using Transactions.Domain.Entities;
using Transactions.Domain.Models;

namespace Transactions.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IMovementHistoryService _movementHistoryService;

        public TransactionService(ITransactionRepository repository, IMovementHistoryService movementHistoryService)
        {
            _repository = repository;
            _movementHistoryService = movementHistoryService;
        }

        public async Task<Transaction> AddTransaction(Guid fromAccountId, Guid toAccountId, int operationType, string reason, float amount, int state)
        {
            if (amount <= 0)
                return null;

            var transaction = new Transaction
            {
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
                OperationTypeId = operationType,
                State = state,
                Reason = reason,
                Amount = new Decimal(amount),
                DateTime = DateTime.Now
            };

            var result = await _repository.AddTransaction(transaction);

            if (result == 0)
                return null;

            return transaction;
        }

        public async Task<bool> AddMovementToHistory(Transaction transaction, AccountModel emisorAccountData, AccountModel receiverAccountData)
        {
            return await _movementHistoryService.AddMovementFromTransaction(transaction, emisorAccountData, receiverAccountData);
        }

        public async Task<bool> AddMovementRejectedToHistory(decimal amount, int operationTypeId, Guid fromAccountId, Guid toAccountId, AccountModel emisorAccountData, AccountModel receiverAccountData)
        {
            return await _movementHistoryService.AddMovementRejected(amount, operationTypeId, fromAccountId, toAccountId, emisorAccountData, receiverAccountData);
        }

        public bool CurrencyMatch(AccountModel emisorAccountData, AccountModel receiverAccountData)
        {
            return (emisorAccountData.Currency == receiverAccountData.Currency);
        }

        public bool EmisorHasFunds(AccountModel emisorAccountData, float amount)
        {
            return (emisorAccountData.Balance >= new Decimal(amount));
        }

        public bool EmisorIsEnabled(AccountModel emisorAccountData)
        {
            return (emisorAccountData.AccountState == "Validada");
        }

        public bool ReceiverIsEnabled(AccountModel receiverAccountData)
        {
            return (receiverAccountData.AccountState == "Validada");
        }
    }
}
