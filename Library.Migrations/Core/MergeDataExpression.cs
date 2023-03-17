using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Libary.Migrations.Core
{

    public class MergeDataExpression : MigrationExpressionBase
    {
        private readonly List<InsertionDataDefinition> _rows = new List<InsertionDataDefinition>();
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        private readonly Dictionary<string, object> _additionalFeatures = new Dictionary<string, object>();

        private readonly List<string> _matchColumns = new List<string>();

        public List<InsertionDataDefinition> Rows
        {
            get { return _rows; }
        }

        public IDictionary<string, object> AdditionalFeatures
        {
            get { return _additionalFeatures; }
        }

        public List<string> MatchColumns
        {
            get { return _matchColumns; }
        }

        public override void ExecuteWith(IMigrationProcessor processor)
        {
            var existingDataSet = processor.ReadTableData(SchemaName, TableName);
            var existingTable = existingDataSet.Tables[0];
            if (_matchColumns.Count == 0)
            {
                foreach (DataColumn item in existingTable.Columns)
                {
                    if (_matchColumns.Contains(item.ColumnName) == false)
                    { _matchColumns.Add(item.ColumnName); }

                }
            }
            foreach (var row in _rows)
            {
                var exists = existingTable.Rows.OfType<DataRow>().Any(r =>
                {


                    return _matchColumns.Select(mc =>
                    {
                        var ex = r[mc];
                        var nw = row.Where(p => p.Key == mc).Select(p => p.Value).SingleOrDefault();
                        bool rtn;
                        if (ex == null || nw == null)
                        {
                            rtn = ex == nw;
                            if (rtn == false)
                            {

                            }
                            return rtn;
                        }
                        rtn = ex.Equals(nw);
                        if (ex is Int64 && nw is Int32)
                        {
                            return ex.Equals(Convert.ToInt64(nw));

                        }
                        if (ex is byte[] && nw is string)
                        {
                            return System.Text.Encoding.ASCII.GetString((byte[])ex) == nw.ToString();
                        }
                        if (rtn == false)
                        {

                        }
                        return rtn;
                    }).All(m => m);
                });

                if (exists)
                {
                    //ExecuteUpdateWith(processor, row);
                }
                else
                {
                    ExecuteInsertWith(processor, row);
                }
            }
        }

        private void ExecuteUpdateWith(IMigrationProcessor processor, List<KeyValuePair<string, object>> row)
        {
            var update = new UpdateDataExpression
            {
                SchemaName = SchemaName,
                TableName = TableName,
                IsAllRows = false,
                Set = row.Where(p => !_matchColumns.Contains(p.Key)).ToList(),
                Where = _matchColumns.Select(mc =>
                {
                    var v = row.Where(p => p.Key == mc).Select(p => p.Value).SingleOrDefault();
                    return new KeyValuePair<string, object>(mc, v);
                }).ToList()
            };

            processor.Process(update);
        }

        private void ExecuteInsertWith(IMigrationProcessor processor, InsertionDataDefinition row)
        {
            var insert = new InsertDataExpression
            {
                SchemaName = SchemaName,
                TableName = TableName
            };

            foreach (var af in _additionalFeatures)
            {
                insert.AdditionalFeatures.Add(af.Key, af.Value);
            }

            insert.Rows.Add(row);

            processor.Process(insert);
        }
    }
}
