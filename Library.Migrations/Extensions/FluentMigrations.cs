using FluentMigrator.Builders.Alter;
using FluentMigrator.Builders.Create.Table;
using Libary.Migrations.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



public static class MigrationExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
    {
        return tableWithColumnSyntax
            .WithColumn("id")
            .AsInt32()
            .NotNullable()
            .PrimaryKey()
            .Identity();
    }
    /// <summary>
    /// Tabloya birincil anahtar atar.
    /// </summary>
    /// <param name="tableWithColumnSyntax"></param>
    /// <param name="columnType">Birincil anahtarın türü yazılır.</param>
    /// <returns></returns>
    public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax, ColumnType columnType)
    {
        switch (columnType)
        {
            case ColumnType.Integer:
                return tableWithColumnSyntax
          .WithColumn("id")
          .AsInt32()
          .NotNullable()
          .PrimaryKey()
          .Identity();

            case ColumnType.Bigint:
                return tableWithColumnSyntax
          .WithColumn("id")
          .AsInt64()
          .NotNullable()
          .PrimaryKey()
          .Identity();

            case ColumnType.Guid:
                return tableWithColumnSyntax
             .WithColumn("id")
             .AsGuid()
             .NotNullable()
             .PrimaryKey()
             .Identity();
            default:
                return tableWithColumnSyntax
            .WithColumn("id")
            .AsInt32()
            .NotNullable()
            .PrimaryKey()
            .Identity();
        }

    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithTimeStamps(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
    {
        return tableWithColumnSyntax
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("modified_at").AsDateTime().NotNullable();
    }
    /// <summary>
    /// Bir tabloya varsayılan kolonları ekler. Bunlar: creation_date, creation_user_id, update_date, update_user_id, delete_date
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="TableName">Varsayılan kolonların ekleneceği tablo adı</param>
    public static void AddDefaultColums(this IAlterExpressionRoot expression, string TableName)
    {
        expression.Table(TableName)
            .AddColumn("update_user_id").AsInt32().Nullable()
            .AddColumn("creation_user_id").AsInt32().NotNullable()
            .AddColumn("creation_date").AsDateTime().NotNullable()
            .AddColumn("update_date").AsDateTime().Nullable()
            .AddColumn("delete_date").AsDateTime().Nullable();
    }
    /// <summary>
    /// Bir tabloya Id kolonu ile birlikte varsayılan kolonları ekler. (Id kolonu bigint türündendir.) Bunlar: creation_date, creation_user_id, update_date, update_user_id, delete_date
    /// </summary>
    /// <param name="expression"></param>
    public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumnAndDefaultColumns(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
    {
        return tableWithColumnSyntax
            .WithIdColumn()
            .WithColumn("update_user_id").AsInt32().Nullable()
            .WithColumn("creation_user_id").AsInt32().NotNullable()
            .WithColumn("creation_date").AsDateTime().NotNullable()
            .WithColumn("update_date").AsDateTime().Nullable()
            .WithColumn("delete_date").AsDateTime().Nullable();
    }
    /// <summary>
    /// Bir tabloya varsayılan kolonları ekler. Bunlar: creation_date, creation_user_id, update_date, update_user_id, delete_date
    /// </summary>
    /// <param name="expression"></param>
    public static ICreateTableColumnOptionOrWithColumnSyntax WithDefaultColumns(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
    {
        return tableWithColumnSyntax
            .WithColumn("update_user_id").AsInt32().Nullable()
            .WithColumn("creation_user_id").AsInt32().NotNullable()
            .WithColumn("creation_date").AsDateTime().NotNullable()
            .WithColumn("update_date").AsDateTime().Nullable()
            .WithColumn("delete_date").AsDateTime().Nullable();
    }
}

