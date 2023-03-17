using FluentMigrator.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel;



namespace Libary.Migrations.Core
{
    public abstract class MergeDataExpressionBuilderBase : ISupportAdditionalFeatures
    {
        protected readonly MergeDataExpression _expression;

        public IDictionary<string, object> AdditionalFeatures => _expression.AdditionalFeatures;

        protected MergeDataExpressionBuilderBase(MergeDataExpression expression)
        {
            _expression = expression;
        }

        protected static IDictionary<string, object> ExtractData(object dataAsAnonymousType)
        {
            var data = new Dictionary<string, object>();

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataAsAnonymousType);

            foreach (PropertyDescriptor property in properties)
            {
                data.Add(property.Name, property.GetValue(dataAsAnonymousType));
            }

            return data;
        }
    }
}
