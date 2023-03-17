using System;
using System.Linq;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.DependencyInjection;
namespace Library.DatabaseMigration
{
    [Migration(1)]
    public abstract class MigrationExistingDB :FluentMigrator. Migration
    {
        public abstract string ImportSchemaScript();

        public override void Down()
        {

        }

        public override void Up()
        {
            Execute.Sql(ImportSchemaScript());
        }
    }
}
