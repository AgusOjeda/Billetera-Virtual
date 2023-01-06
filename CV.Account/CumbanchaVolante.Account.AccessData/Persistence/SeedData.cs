using CV.MsAccount.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CV.MsAccount.AccessData.Persistence
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                if (context.Currencies.Any())
                {
                    return;
                }

                var currencies = new List<Currency>()
                {
                    new Currency
                    {
                        Name = "USD",
                        LongName = "Dolar Americano"
                    },
                    new Currency
                    {
                        Name = "ARS",
                        LongName = "Pesos"
                    },
                    new Currency
                    {
                        Name = "EUR",
                        LongName = "Euro"
                    },
                    new Currency
                    {
                        Name = "CAD",
                        LongName = "Dolar Canadiense"
                    },
                    new Currency
                    {
                        Name = "CNY",
                        LongName = "Yuan Chino"
                    }
                 };

                context.AddRange(currencies);

                if (context.AccountStates.Any())
                {
                    return;
                }

                var accountStates = new List<AccountState>()
                {
                new AccountState
                {
                    Name = "Validada"
                },
                new AccountState
                {
                    Name = "Sin Validar"
                },
                new AccountState
                {
                    Name = "Bajo revision"
                }
                };
                context.AddRange(accountStates);

                if (context.Accounts.Any())
                {
                    return;
                }

                var accounts = new List<Account>()
                {


                new Account
                {
                    CustomerId = Guid.NewGuid(),
                    Alias = "Vodka.Ninja.Fuego.Bruma",
                    Cbu = "2541607682340151836742",
                    Balance = 0,
                    AccountState = accountStates[0],
                    Currency = currencies[1]
                },
                //new Account
                //{
                //    CustomerId = Guid.NewGuid(),
                //    Alias = "Alias2.Prueba.sprint",
                //    Cbu = "5602928211100046200699",
                //    Balance = 20,
                //    AccountState = accountStates[1],
                //    Currency = currencies[1]
                //},
                //new Account
                //{
                //    CustomerId = Guid.NewGuid(),
                //    Alias = "Alias3.Prueba.sprint",
                //    Cbu = "5602928211100046200700",
                //    Balance = 25,
                //    AccountState = accountStates[2],
                //    Currency = currencies[2]
                //},
                //new Account
                //{
                //    CustomerId = Guid.NewGuid(),
                //    Alias = "Alias4.Prueba.sprint",
                //    Cbu = "5602928211100046200699",
                //    Balance = 20,
                //    AccountState = accountStates[1],
                //    Currency = currencies[3]
                //},
                //new Account
                //{
                //    CustomerId = Guid.NewGuid(),
                //    Alias = "Alias5.Prueba.sprint",
                //    Cbu = "5602928211100046200699",
                //    Balance = 20,
                //    AccountState = accountStates[0],
                //    Currency = currencies[4]
                //}
                };
                context.AddRange(accounts);

              
                context.SaveChanges();
            }
        }
}  }