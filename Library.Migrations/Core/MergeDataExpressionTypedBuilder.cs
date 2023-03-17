using FluentMigrator.Model;
using Libary.Migrations.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;



namespace Libary.Migrations.Core
{
    public class MergeDataExpressionTypedBuilder<T> : MergeDataExpressionBuilderBase, IMergeDataOrMatchSyntax<T>
    {
        public MergeDataExpressionTypedBuilder(MergeDataExpression expression) : base(expression)
        {
        }

        public IMergeDataOrMatchSyntax<T> Row(T dataAsAnonymousType)
        {
            IDictionary<string, object> data = ExtractData(dataAsAnonymousType);

            var dataDefinition = new InsertionDataDefinition();

            dataDefinition.AddRange(data);

            _expression.Rows.Add(dataDefinition);

            return this;
        }

        public void Match<M>(Func<T, M> f)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(M));
            foreach (PropertyDescriptor property in properties)
            {
                _expression.MatchColumns.Add(property.Name);
            }
        }
    }
}
