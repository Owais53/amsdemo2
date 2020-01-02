namespace amsdemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.Roles",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   Name = c.String(),
               })
               .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    UserName = c.String(),
                    Password = c.String(),
                    RoleId = c.Int(nullable: false),
                    isAdmin = c.Boolean(nullable: false),

                })
                .PrimaryKey(t => t.UserId);

            CreateTable(
                "dbo.Departments",
                c => new
                {
                    DepartmentId = c.Int(nullable: false, identity: true),
                    DepartmentName = c.String(),


                })
                .PrimaryKey(t => t.DepartmentId);



        }

        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Departments");

        }
    }
}
