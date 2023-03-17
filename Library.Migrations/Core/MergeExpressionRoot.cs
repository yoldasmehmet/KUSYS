using FluentMigrator.Infrastructure;
using Libary.Migrations.Interfaces;

namespace Libary.Migrations.Core
{
    public class MergeExpressionRoot : IMergeExpressionRoot
    {
        private readonly IMigrationContext _context;

        public MergeExpressionRoot(IMigrationContext context)
        {
            _context = context;
        }

        public IMergeDataOrInSchemaSyntax IntoTable(string tableName)
        {
            var expression = new MergeDataExpression { TableName = tableName };
            _context.Expressions.Add(expression);
            return new MergeDataExpressionStartBuilder(expression);
        }
    }
}
