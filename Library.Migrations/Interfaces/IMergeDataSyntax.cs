namespace Libary.Migrations.Interfaces
{
    public interface IMergeDataSyntax
    {
        IMergeDataOrMatchSyntax<T> Row<T>(T dataAsAnonymousType);
    }
}
