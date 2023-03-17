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
            Create.Table("user").WithIdColumn().
            WithColumn("name").AsString().
            WithColumn("email").AsString().
            WithColumn("password").AsString().
            WithColumn("description").AsString();

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
            Create.Table("role").WithIdColumn().
            WithColumn("name").AsString().
            WithColumn("description").AsString();
        }
    }
    //[Migration(3, "Mehmet Yoldaş")]
    //public class ShemaChanges_V3 : Migration
    //{
    //    public override void Down()
    //    {
    //    }
    //    public override void Up()
    //    {
    //        Create.Table("user_role").WithIdColumn().
    //        WithColumn("user_id").AsInt32().ForeignKey("user", "id").
    //        WithColumn("role_id").AsInt32().ForeignKey("role", "id");
    //    }
    //}
    [Migration(4, "Mehmet Yoldaş")]
    public class ShemaChanges_V4 : Migration
    {
        public override void Down()
        {
        }
        public override void Up()
        {
            Create.Table("application").WithIdColumn().
            WithColumn("name").AsString().
            WithColumn("ip").AsString().Nullable().
            WithColumn("port").AsString().Nullable().
            WithColumn("description").AsString().Nullable();
        }
    }
    [Migration(5, "Mehmet Yoldaş")]
    public class ShemaChanges_V5 : Migration
    {
        public override void Down()
        {
        }
        public override void Up()
        {

            Create.Table("application_user_role").WithIdColumn().
            WithColumn("user_id").AsInt32().ForeignKey("user", "id").
            WithColumn("role_id").AsInt32().ForeignKey("role", "id").
            WithColumn("application_id").AsInt32().ForeignKey("application", "id");
        }
    }
    [Migration(6, "Mehmet Yoldaş")]
    public class ShemaChanges_V6 : Migration
    {
        public override void Down()
        {
        }
        public override void Up()
        {

            Create.Column("ticket_expiration_second").OnTable("application").AsInt32().Nullable();
        }
    }
    [Migration(7, "Mehmet Yoldaş")]
    public class ShemaChanges_V7 : Migration
    {
        public override void Down()
        {
        }
        public override void Up()
        {

            Create.Table("authentication_type").WithIdColumn().
            WithColumn("name").AsString();

            Create.Column("authentication_type_id").OnTable("user").AsInt32().Nullable().ForeignKey("authentication_type", "id");
        }
    }
    [Migration(8, "Mehmet Yoldaş")]
    public class ShemaChanges_V8 : Migration
    {
        public override void Down()
        {
        }
        public override void Up()
        {

            

            Create.Column("actor").OnTable("user").AsString().Nullable();
        }
    }
}
