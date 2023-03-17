using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.VersionTableInfo;
using Libary.Logging.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using Library.Migrations.Enums;
using Microsoft.AspNetCore.Builder;

namespace Libary.Common.Utils
{
    public class DatabaseMigration
    {

        public static IServiceProvider CreateServicesForPostgres<T>(string adminAcountedConnectionString,params string[] Tags)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .Configure<RunnerOptions>(opt => { opt.Tags = Tags;  })
                .ConfigureRunner(rb => rb
                    .AddPostgres()//.WithVersionTable(new VersionTable() )
                    .WithGlobalConnectionString(adminAcountedConnectionString)
                    .ScanIn(typeof(T).Assembly).ScanIn(typeof(DatabaseMigration).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
        public static IServiceProvider CreateServices<T>(string adminAcountedConnectionString,Type assambly, params string[] Tags)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .Configure<RunnerOptions>(opt => { opt.Tags = Tags; })
                .ConfigureRunner(rb => rb
                    .AddPostgres()//.WithVersionTable(new VersionTable() )
                    .WithGlobalConnectionString(adminAcountedConnectionString)
                    .ScanIn(Assembly.GetAssembly(assambly)).ScanIn(typeof(DatabaseMigration).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
        public static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
        public static IServiceProvider CreateServicesForSqlLite<T>(string adminAcountedConnectionString= "Data Source=changeMe.db")
        {
            // Configure the DB connection
            var serviceProvider = new ServiceCollection()
                // Logging is the replacement for the old IAnnouncer
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Registration of all FluentMigrator-specific services
                .AddFluentMigratorCore()
                // Configure the runner
                .ConfigureRunner(
                    builder => builder
                        // Use SQLite
                        .AddSQLite()
                        // The SQLite connection string
                        .WithGlobalConnectionString(adminAcountedConnectionString)
                    // Specify the assembly with the migrations
                        .WithMigrationsIn(typeof(T).Assembly))
                .BuildServiceProvider();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.MigrateUp();
            }
            return serviceProvider;
        }
        public static void Migrate<Program>(string adminConnectionString, DatabaseType dbType = DatabaseType.Postgres, params string[] Tags)
        {
            new Thread(new ThreadStart(() =>
            {

                try
                {
                    Stopwatch stopWatch = new Stopwatch();
                    Console.WriteLine("Migration servisleri oluşturuluyor.");
                    stopWatch.Start();
                    IServiceProvider service = null;
                    switch (dbType)
                    {
                        case DatabaseType.SqlLite:
                            service = CreateServicesForSqlLite<Program>(adminConnectionString); 
                            break;
                        case DatabaseType.Postgres:
                            service = CreateServicesForPostgres<Program>(adminConnectionString);
                            //builder.Services.AddDbContext<ProjectDbContext>(options =>
                            //           options.UseNpgsql("User ID=postgres;Password=postgres_2021;Server=10.252.0.96;Port=5010;Database=test_kys_db;Integrated Security=true;Pooling=true;"));
                            break;
                        default:
                            service = CreateServicesForPostgres<Program>(adminConnectionString);
                            break;
                    }
                    
                    Console.WriteLine("Migration servisleri oluşturuldu.");
                    Console.WriteLine("Migrate yapılıyor...");
                    DatabaseMigration.UpdateDatabase(service);
                    stopWatch.Stop();
                    var message = String.Format("Migrate {0}'milisaniye içinde başarı ile yapıldı.", stopWatch.ElapsedMilliseconds);
                    Logger.Log(message,LogType.Information);
                    Console.WriteLine(message);
                
                }
                catch (Exception ex)
                {  
                    var error = ex.GetAllMessages();
                    Log.Logger.Error(error);
                    Console.WriteLine(error);
                }

            })).Start();
        }
        public static void Migrate<Program>(string adminConnectionString, Type assambly, params string[] Tags)
        {
            new Thread(new ThreadStart(() =>
            {
                try
                {
                    Console.WriteLine("Migration servisleri oluşturuluyor.");
                    var service = DatabaseMigration.CreateServices<Program>(adminConnectionString, assambly, Tags);
                    DatabaseMigration.UpdateDatabase(service);
                    Logger.Log("Migrate yapıldı.", LogType.Information);
                    Console.WriteLine("Migrate yapıldı.");
                }
                catch (Exception ex)
                {
                    var error = ex.GetAllMessages();
                    Log.Logger.Error(error);
                    Console.Write(error);
                }

            })).Start();
        }
    }

    [VersionTableMetaData]
    public class VersionTable : IVersionTableMetaData
    {
        public string ColumnName
        {
            get { return "Version"; }
        }


        public string TableName
        {
            get { return "Version2"; }
        }

        public string UniqueIndexName
        {
            get { return "UC_Version"; }
        }

        public virtual string AppliedOnColumnName
        {
            get { return "AppliedOn"; }
        }

        public virtual string DescriptionColumnName
        {
            get { return "Description"; }
        }

        public object ApplicationContext { get; set; }

        public bool OwnsSchema { get; set; }

        public string SchemaName { get { return "SchemaName"; } }

    }
}
