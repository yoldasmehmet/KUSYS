using Libary.Common;
using Libary.Migrations.Utils;
using Libary.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Library.Migrations.Enums;
using Microsoft.Extensions.DependencyInjection;

public static class MigrationDependencyInjectionExtensions
{
    public static void UseMigration<T>(this IServiceCollection app, DatabaseType dbType = DatabaseType.Postgres, string connectionStringName = "MigratedConnection")
    {

        AddMigration<T>(dbType, connectionStringName);
    }
    static void AddMigration<T>( DatabaseType dbType = DatabaseType.Postgres, string connectionStringName = "MigratedConnection")
    {
        string connectionString = GetConnectionString(connectionStringName);
        Libary.Common.Utils.DatabaseMigration.Migrate<T>(connectionString,dbType, "Development", "Test", "Production");
    }

    private static string GetConnectionString(string connectionStringName)
    {
        var config = EnvironmentHelper.GetConfiguration();

        var connectionString = config.GetSection("ConnectionStrings").GetSection(connectionStringName).Value;


        if (connectionString == null)
        {

            Console.Write("Lütfen application.json dosyasına aşagıdaki konfigürasyonu giriniz." +
                    Environment.NewLine +
            "\"ConnectionStrings\": " + Environment.NewLine +
                    "{ " + Environment.NewLine +
                    "\"MigratedConnection\": \"ConnectionStringYazilacak\"" + Environment.NewLine +
                    "}" + Environment.NewLine
                    );
        }

        return connectionString;
    }
}
