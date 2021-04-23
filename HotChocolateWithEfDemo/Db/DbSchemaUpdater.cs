using System;
using System.Reflection;
using DbUp;

namespace HotChocolateWithEfDemo.Db
{
    public class DbSchemaUpdater
    {
        public static void UpgradeDb()
        {
            var upgrader =
                DeployChanges.To
                    .PostgresqlDatabase("User ID=lab_user;Password=lab_pass;Database=graphqlfun;Host=localhost;Port=5432")
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();    
            
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