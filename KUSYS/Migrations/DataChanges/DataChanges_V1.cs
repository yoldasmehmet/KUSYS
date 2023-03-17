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
            Insert.IntoTable("course").Row(new
            {
                course_id = "CSI101",
                name = "Introduction to Computer Science",
            });
            Insert.IntoTable("course").Row(new
            {
                course_id = "CSI102",
                name = "Algorithms",
            });
            Insert.IntoTable("course").Row(new
            {
                course_id = "MAT101",
                name = "Calculus",
            });
            Insert.IntoTable("course").Row(new
            {
                course_id = "PHY101",
                name = "Physics",
            });
        }
    }

    [Migration(1000001, "Mehmet Yoldaş")]
    public class DataChanges_V1_2_Insert : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("student").Row(new
            {
                first_name = "Ali",
                last_name = "Yoldaş",
                user_name = "testuser",

                birth_date = new DateTime(2002, 3, 18)
            });

            Insert.IntoTable("student_course").Row(new
            {
                student_id = 1,
                course_id = 1,
            });
            Insert.IntoTable("student_course").Row(new
            {
                student_id = 1,
                course_id = 2,
            });
            Insert.IntoTable("student_course").Row(new
            {
                student_id = 1,
                course_id = 3,
            });
        }
    }

}