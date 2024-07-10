using System.Reflection;
using Container.Infrastructure;
using Container.Infrastructure.Context;
using Container.Infrastructure.Models;
using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Container;

static class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = CreateServices();
        // Put the database update into a scope to ensure
        // that all resources will be disposed.
        using (var scope = serviceProvider.CreateScope())
        {
            UpdateDatabase(scope.ServiceProvider);
        }
        
        using var connection = DapperContext.GetOpenConnection();
        
        var data = await connection.QueryAsync<Company>("select * from Companies;");
        foreach (var row in data)
        {
            Console.WriteLine($"Id: {row.Id} - {row.Name}");
        }
    }

   
     private static IServiceProvider CreateServices()
     {
         return new ServiceCollection()
             .AddLogging(c => c.AddFluentMigratorConsole())
             .AddSingleton<DapperContext>()
             .AddFluentMigratorCore()
             .ConfigureRunner(c=> c.AddMySql5()
                 .WithGlobalConnectionString(ApplicationSettings.DbConnectionString)
                 .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
             .BuildServiceProvider(false);
     }

     private static void UpdateDatabase(IServiceProvider serviceProvider)
     {
         var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
         runner.MigrateUp();
     }     
}