namespace Libary.Migrations.Interfaces
{
    public interface IMergeDataOrInSchemaSyntax : IMergeDataSyntax
    {
        IMergeDataSyntax InSchema(string schemaName);
    }
}
