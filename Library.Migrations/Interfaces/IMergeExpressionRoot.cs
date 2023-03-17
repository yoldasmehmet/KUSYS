namespace Libary.Migrations.Interfaces
{
    public interface IMergeExpressionRoot
    {
        IMergeDataOrInSchemaSyntax IntoTable(string tableName);
    }
}
