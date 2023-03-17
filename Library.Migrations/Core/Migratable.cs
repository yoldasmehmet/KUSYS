using FluentMigrator.Infrastructure;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libary.Migrations.Interfaces;

namespace Libary.Migrations.Core
{
    namespace Libary.Migrations.Core
    {
        public abstract class Migratable : Migration
        {
            public IMergeExpressionRoot Merge
            {
                get
                {
                    return new MergeExpressionRoot((IMigrationContext)GetType().GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this));
                }
            }

            public override void Down()
            {
            }
        }
    }
}
