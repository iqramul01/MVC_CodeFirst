namespace _1263087.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationalData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Admits", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Admits", new[] { "DepartmentID" });
            DropColumn("dbo.Admits", "DepartmentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admits", "DepartmentID", c => c.Int());
            CreateIndex("dbo.Admits", "DepartmentID");
            AddForeignKey("dbo.Admits", "DepartmentID", "dbo.Departments", "DepartmentID");
        }
    }
}
