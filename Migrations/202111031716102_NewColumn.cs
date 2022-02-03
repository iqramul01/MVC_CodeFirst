namespace _1263087.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "ConfirmPhoneNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "ConfirmPhoneNumber");
        }
    }
}
