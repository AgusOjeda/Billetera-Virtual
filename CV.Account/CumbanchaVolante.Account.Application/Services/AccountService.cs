
using CV.MsAccount.AccessData.Interface;
using CV.MsAccount.AccessData.Interfaces;
using CV.MsAccount.Application.Interfaces;
using CV.MsAccount.Application.Response;
using CV.MsAccount.Domain.Entities;

namespace CV.MsAccount.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountStateRepository _accountStateRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IRandomGenerate _randomGenerate;
        private readonly ICustomerService _customerService;

        public AccountService(IAccountRepository accountRepository, IAccountStateRepository accountStateRepository, ICurrencyRepository currencyRepository, IRandomGenerate randomGenerate, ICustomerService customerService)
        {
            _accountRepository = accountRepository;
            _accountStateRepository = accountStateRepository;
            _currencyRepository = currencyRepository;
            _randomGenerate = randomGenerate;
            _customerService = customerService;
        }

        public async Task<AccountIdResponse> CreateAccount(Guid customerId, string shortNameCurrency)
        {
            var accountState = await _accountStateRepository.GetAccountStateByName("Validada");
            if (accountState is null)
            {
                return null;
            }
            var currency = await _currencyRepository.GetCurrencyByName(shortNameCurrency);
            if (currency is null)
            {
                return null;
            }

            var alias = GenerateAlias();
            var cbu = GenerateCbu();

            var account = new Account
            {
                CustomerId = customerId,
                Alias = alias,
                Cbu = cbu,
                Balance = 1000,
                CurrencyId = currency.CurrencyId,
                AccountStateId = accountState.AccountStateId,
            };

            await _accountRepository.CreateAccount(account);

            return new AccountIdResponse
            {
                AccountId = account.AccountId,
            };
        }
        private string GenerateCbu()
        {
            var cbu = _randomGenerate.GenerateCbu();
            return cbu;           
        }
        private string GenerateAlias()
        {
            var alias = _randomGenerate.GenerateAlias();
            return alias;
        }

        public async Task<AccountResponse> GetById(Guid accountId, string jwtToken)
        {
            var account = await _accountRepository.GetAccountById(accountId);

            if (account is null)
            {
                return null;
            }
            else
            {
                var customerResult = await _customerService.GetCustomerById(account.CustomerId, jwtToken);

                if(customerResult is null)
                {
                    return null;
                }

                var accountResponse = new AccountResponse
                {
                    FullNameCustomer = customerResult.FirstName + " " + customerResult.LastName,
                    Balance = account.Balance,
                    Alias = account.Alias,
                    Cbu = account.Cbu,
                    Cuil = customerResult.Cuil, 
                    Currency = account.Currency.Name,
                    AccountState = account.AccountState.Name,
                    Dni = customerResult.Dni,
                };
                return accountResponse;
            }
        }

        public async Task<AccountCbuResponse> GetByCbu(string cbu)
        {
            var account = await _accountRepository.GetAccountByCbu(cbu);

            if (account is null)
            {
                return null;
            }
            else
            {
                var accountCbuResponse = new AccountCbuResponse
                {
                    AccountId = account.AccountId,
                    Balance = account.Balance,
                    Alias = account.Alias,
                    NameCurrency = account.Currency.Name,
                    LongNameCurrency = account.Currency.LongName,
                    Cbu = account.Cbu
                };
                return accountCbuResponse;
            }
        }

        public async Task<AccountBalanceResponse> UpdateAccountBalance(Guid accountId, decimal amount)
        {
            var account = await _accountRepository.GetAccountById(accountId);
            if (account is null)
            {
                return null;
            }
            account.Balance += amount;

            var result = await _accountRepository.UpdateAccount(account);
            if (result is null)
            {
                return null;
            }

            var accountBalanceResponse = new AccountBalanceResponse
            {
                AccountId = account.AccountId,
                Balance = result.Balance,
                NameCurrency = account.Currency.Name
            };
            return accountBalanceResponse;
        }

        public async Task<AccountStateResponse> UpdateAccountState(Guid accountId, string state)
        {
            var account = await _accountRepository.GetAccountById(accountId);
            if (account is null)
            {
                return null;
            }

            var accountState = await _accountStateRepository.GetAccountStateByName(state);
            if (accountState is null)
            {
                return null;
            }

            account.AccountStateId = accountState.AccountStateId;
            var result = await _accountRepository.UpdateAccount(account);
            if (result is null)
            {
                return null;
            }

            var accountStateResponse = new AccountStateResponse
            {
                AccountId = account.AccountId,
                CustomerId = account.CustomerId,
                AccountState = accountState.Name
            };
            return accountStateResponse;
        }

        public async Task<List<AccountCustomerResponse>> GetByCustomerId(Guid customerId)
        {
            var customerAccount = await _accountRepository.GetByCustomerId(customerId);
            if (customerAccount is null)
            {
                return null;
            }

            var accounts = await _accountRepository.GetAccountByCustomerId(customerId);

            var customerAccounts = new List<AccountCustomerResponse>();
            foreach (var account in accounts)
            {
                customerAccounts.Add(new AccountCustomerResponse
                {
                    AccountId = account.AccountId,
                    CustomerId = account.CustomerId,
                });
            }
           return customerAccounts;
        }

        public async Task<AccountIdResponse> DeleteAccount(Guid accountId)
        {
            var account = await _accountRepository.GetAccountById(accountId);
            if (account is null)
            {
                return null;
            }
            await _accountRepository.Delete(account);

            var result = new AccountIdResponse
            {
                AccountId = account.AccountId
            };
            return result;
        }

        public async Task<List<AccountUserResponse>> GetAllFromCustomer(Guid customerId)
        {
            var customer = await _accountRepository.GetAccountByCustomerId(customerId);
            if(customer is null)
            {
                return null;
            };

            var result = new List<AccountUserResponse>();
            var customerAccounts = await _accountRepository.GetAccountsFromUser(customerId);
            if (customerAccounts.Any())
            {            
                foreach (var account in customerAccounts)
                {
                    result.Add(
                        new AccountUserResponse
                        {
                            Balance = account.Balance,
                            Alias = account.Alias,
                            Cbu = account.Cbu,
                            Currency = account.Currency.Name,
                            AccountState = account.AccountState.Name
                        }
                        );
                }
            }
            return result;
        }

        public async Task<AccountIdResponse> UpdateAccountAlias(Guid accountId, string alias)
        {
            var account = await _accountRepository.GetAccountById(accountId);
            if (account is null)
            {
                return null;
            }
            account.Alias = alias;
            var result = await _accountRepository.UpdateAccount(account);

            if (result is null)
            {
                return null;
            }

            var accountIdResponse = new AccountIdResponse
            {
                AccountId = account.AccountId,
            };
            return accountIdResponse;
        }

        public async Task<AccountIdResponse> UpdateAccountCurrency(Guid accountId, string shortNameCurrency)
        {
            var account = await _accountRepository.GetAccountById(accountId);
            if (account is null)
            {
                return null;
            }
            var currency = await _currencyRepository.GetCurrencyByName(shortNameCurrency);
            if (currency is null)
            {
                return null;
            }

            account.CurrencyId = currency.CurrencyId;
            var result = await _accountRepository.UpdateAccount(account);

            if (result is null)
            {
                return null;
            }

            var accountIdResponse = new AccountIdResponse
            {
                AccountId = account.AccountId,
            };
            return accountIdResponse;
        }

        public async Task<AccountIdResponse> GetByAlias(string alias)
        {
            var account = await _accountRepository.GetAccountByAlias(alias);

            if (account is null)
            {
                return null;
            }
            else
            {
                var accountAliasResponse = new AccountIdResponse
                {
                    AccountId = account.AccountId,
                };
                return accountAliasResponse;
            }
        }
    }
}