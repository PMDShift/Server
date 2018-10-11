using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Server.Database;

namespace Server.Migrations
{
    public class MigrationRunner
    {
        public static void MigrateDatabase()
        {
            var playersServiceProvider = CreateServices(DatabaseID.Players);
            using (var scope = playersServiceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }

            var dataServiceProvider = CreateServices(DatabaseID.Data);
            using (var scope = dataServiceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices(DatabaseID databaseID)
        {
            string tag;
            switch (databaseID)
            {
                case DatabaseID.Players:
                    tag = "players";
                    break;
                case DatabaseID.Data:
                    tag = "data";
                    break;
                default:
                    throw new NotSupportedException();
            }

            var databaseConnection = new DatabaseConnection(databaseID, false);

            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddMySql5()
                    .WithGlobalCommandTimeout(TimeSpan.Zero)
                    // Set the connection string
                    .WithGlobalConnectionString(databaseConnection.Database.ConnectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(MigrationRunner).Assembly).For.Migrations())
                .Configure<RunnerOptions>(opt =>
                {
                    opt.Tags = new string[] { tag };
                })
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            try
            {
                // Execute the migrations
                runner.MigrateUp();
            }
            catch { }
        }
    }
}
