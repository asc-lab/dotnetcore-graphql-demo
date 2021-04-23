using System;
using System.Reflection;
using DbUp;

namespace HotChocolateWithEfDemo.Db
{
    public class DbSchemaUpdater
    {
        public static void UpgradeDb(string connectionString)
        {
            var upgrader =
                DeployChanges.To
                    .PostgresqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .WithTransactionPerScript()
                    .Build();

            EnsureDatabase.For.PostgresqlDatabase(connectionString);
            
            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();

                throw new ApplicationException("Cannot migrate db");
            }
        }
    }
}