using System.Collections.Generic;
using System;
using FluentMigrator;
using System.Diagnostics.Metrics;

namespace Library.Security.IdentityServer.Migrations.DataChanges
{
    [Migration(1000000, "Mehmet Yoldaş")]
    public class DataChanges_V1_1_Insert : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("user").Row(new
            {
                name = "testuser",
                email = "user@mail.com",
                password = "library".ToMd5(),
                description = "test",
                actor="user"
            });
        }
    }
    [Migration(1000002, "Mehmet Yoldaş")]
    public class DataChanges_V1_2_Insert : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("user").Row(new
            {
                name = "testadmin",
                email = "admin@mail.com",
                password = "library".ToMd5(),
                description = "test",
                actor = "admin"
            });
        }
    }
    [Migration(1000003, "Mehmet Yoldaş")]
    public class DataChanges_V1_3_Insert : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("role").Row(new
            {
                name = "US-1",
                description = "Create/Update/Delete a student"
            });
        }
    }
    [Migration(1000004, "Mehmet Yoldaş")]
    public class DataChanges_V1_4_Insert : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("role").Row(new
            {
                name = "US-2",
                description = "List all Students and see details of a selected student in a popup."
            });
        }
    }
    
    [Migration(1000005, "Mehmet Yoldaş")]
    public class DataChanges_V1_5_Insert : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("role").Row(new
            {
                name = "US-3",
                description = "Match a student with a selection of courses. (Constraint: a student can select a course once.)"
            });
        }
    }
    [Migration(1000006, "Mehmet Yoldaş")]
    public class DataChanges_V1_6_Insert : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("role").Row(new
            {
                name = "US-4",
                description = "List all Student & Course matchings"
            });
        }
    }
    [Migration(1000007, "Mehmet Yoldaş")]
    public class DataChanges_V1_7_Insert : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("authentication_type").Row(new
            {
                name = "LDAP"
            });
            Insert.IntoTable("authentication_type").Row(new
            {
                name = "Custom"
            });
        }
    }
    [Migration(1000008, "Mehmet Yoldaş")]
    public class DataChanges_V1_8_Insert : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("application").Row(new
            {
                name = "KUSYS",

            });

        }
    }
    [Migration(1000009, "Mehmet Yoldaş")]
    public class DataChanges_V1_9_Insert : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("application_user_role").Row(new
            {
                application_id = 1,
                user_id = 1,
                role_id = 2
            });
            Insert.IntoTable("application_user_role").Row(new
            {
                application_id = 1,
                user_id = 1,
                role_id = 3
            });
            Insert.IntoTable("application_user_role").Row(new
            {
                application_id = 1,
                user_id = 1,
                role_id = 4
            });
            Insert.IntoTable("application_user_role").Row(new
            {
                application_id = 1,
                user_id = 2,
                role_id = 1
            });
            Insert.IntoTable("application_user_role").Row(new
            {
                application_id = 1,
                user_id = 2,
                role_id = 2
            });
            Insert.IntoTable("application_user_role").Row(new
            {
                application_id = 1,
                user_id = 2,
                role_id = 3
            });
            Insert.IntoTable("application_user_role").Row(new
            {
                application_id = 1,
                user_id = 2,
                role_id = 4
            });
        }
    }

}
