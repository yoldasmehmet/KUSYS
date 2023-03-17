using System;
using FluentMigrator;
namespace Library.Security.IdentityServer.Migrations.SchemaChanges
{


    [Migration(1, "Mehmet Yoldaş")]
    public class ShemaChanges_V1 : Migration
    {
        public override void Down()
        {
        }
        public override void Up()
        {
            Create.Table("student").WithIdColumn().
            WithColumn("first_name").AsString().
            WithColumn("last_name").AsString().
            WithColumn("user_name").AsString().Unique().
            WithColumn("birth_date").AsDate();

        }
    }
    [Migration(2, "Mehmet Yoldaş")]
    public class ShemaChanges_V2 : Migration
    {
        public override void Down()
        {
        }
        public override void Up()
        {
            Create.Table("course").WithIdColumn().
            WithColumn("course_id").AsString().
            WithColumn("name").AsString();
        }
    }
    [Migration(3, "Mehmet Yoldaş")]
    public class ShemaChanges_V3 : Migration
    {
        public override void Down()
        {
        }
        public override void Up()
        {
            Create.Table("student_course").WithIdColumn().
            WithColumn("course_id").AsInt32().ForeignKey("course", "id").
            WithColumn("student_id").AsInt32().ForeignKey("student", "id");
        }
    }
}
