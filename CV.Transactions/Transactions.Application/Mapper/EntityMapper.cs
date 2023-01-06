using Transactions.Application.Interfaces;
using Transactions.Domain.DTOs;
using Transactions.Domain.Entities;

namespace Transactions.Application.Mapper
{
    public class EntityMapper : IEntityMapper
    {
        //public UserDtoForDisplay UserToUserDtoForDisplay(User userEntity)
        //{
        //    var user = new UserDtoForDisplay()
        //    {
        //        //Name = userEntity.Name,
        //        //LastName = userEntity.LastName,
        //        //Email = userEntity.Email
        //    };
        //    return user;
        //}

        public MovementHistoryDto ToMovementHistoryDto(MovementHistory movementHistoryEntity)
        {
            var movementHistory = new MovementHistoryDto()
            {
                OperationType = movementHistoryEntity.OperationType,
                FromAccountId = movementHistoryEntity.FromAccountId,
                FromCbu = movementHistoryEntity.FromCbu,
                FullNameEmisorCustomer = movementHistoryEntity.FullNameEmisorCustomer,
                DniEmisorCustomer = movementHistoryEntity.DniEmisorCustomer,
                CuilEmisorCustomer = movementHistoryEntity.CuilEmisorCustomer,
                ToAccountId = movementHistoryEntity.ToAccountId,
                ToCbu = movementHistoryEntity.ToCbu,
                FullNameReceiverCustomer = movementHistoryEntity.FullNameReceiverCustomer,
                DniReceiverCustomer = movementHistoryEntity.DniReceiverCustomer,
                CuilReceiverCustomer = movementHistoryEntity.CuilReceiverCustomer,
                DateTimeTransaction = movementHistoryEntity.DateTimeTransaction,
                AmountTransaction = movementHistoryEntity.AmountTransaction,
                Currency = movementHistoryEntity.Currency,
                ResultingStateOfTransaction = movementHistoryEntity.ResultingStateOfTransaction
            };

            return movementHistory;
        }

        public TransactionDto ToTransactionDto(Transaction transactionEntity)
        {
            var transaction = new TransactionDto()
            {
                FromAccountId = transactionEntity.FromAccountId,
                ToAccountId = transactionEntity.ToAccountId,
                State = transactionEntity.State,
                OperationTypeId = transactionEntity.OperationTypeId,
                Reason = transactionEntity.Reason,
                Amount = transactionEntity.Amount,
                DateTime = transactionEntity.DateTime,
            };

            return transaction;
        }
    }
}