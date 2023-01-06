using Transactions.Domain.Entities;
using Transactions.Domain.Models;

namespace Transactions.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> AddMovementToHistory(Transaction transaction, AccountModel emisorAccountData, AccountModel receiverAccountData);
        Task<bool> AddMovementRejectedToHistory(decimal amount, int operationTypeId, Guid fromAccountId, Guid toAccountId, AccountModel emisorAccountData, AccountModel receiverAccountData);
        bool CurrencyMatch(AccountModel emisorAccountData, AccountModel receiverAccountData);
        bool EmisorHasFunds(AccountModel emisorAccountData, float amount);
        bool EmisorIsEnabled(AccountModel emisorAccountData);
        bool ReceiverIsEnabled(AccountModel receiverAccountData);
        Task<Transaction> AddTransaction(Guid fromAccountId, Guid toAccountId, int operationType, string reason, float amount, int state);
    }
}
