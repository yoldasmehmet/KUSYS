using Libary.Migrations.Interfaces;

namespace Libary.Migrations.Core
{
    public class MergeDataExpressionStartBuilder : MergeDataExpressionBuilderBase, IMergeDataOrInSchemaSyntax
    {
        public MergeDataExpressionStartBuilder(MergeDataExpression expression) : base(expression)
        {
        }

        public IMergeDataOrMatchSyntax<T> Row<T>(T dataAsAnonymousType)
        {
            var typed = new MergeDataExpressionTypedBuilder<T>(_expression);
            return typed.Row(dataAsAnonymousType);
        }

        public IMergeDataSyntax InSchema(string schemaName)
        {
            _expression.SchemaName = schemaName;
            return this;
        }
    }
}
