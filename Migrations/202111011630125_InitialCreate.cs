namespace _1263087.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admits",
                c => new
                    {
                        AdmitID = c.Int(nullable: false, identity: true),
                        AdmitFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalBill = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        //NetBill = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DepartmentID = c.Int(),
                        PatientID = c.Int(),
                    })
                .PrimaryKey(t => t.AdmitID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .ForeignKey("dbo.Patients", t => t.PatientID)
                .Index(t => t.DepartmentID)
                .Index(t => t.PatientID);
            Sql("ALTER TABLE dbo.Admits ADD NetBill As (TotalBill - (TotalBill*Discount))");


            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        AvailableSeat = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false, identity: true),
                        DoctorName = c.String(nullable: false, maxLength: 30),
                        ContactAddress = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Commission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        //TotalBill = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DoctorImage = c.String(),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DoctorID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            Sql("ALTER TABLE dbo.Doctors ADD TotalBill As (Salary + Commission)");


            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        PatientName = c.String(nullable: false),
                        EmergencyContact = c.Int(nullable: false),
                        BloodGroupID = c.Int(),
                        DivisionID = c.Int(),
                        DistrictID = c.Int(),
                        UpazilaID = c.Int(),
                        DepartmentID = c.Int(),
                        DoctorID = c.Int(),
                    })
                .PrimaryKey(t => t.PatientID)
                .ForeignKey("dbo.BloodGroups", t => t.BloodGroupID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .ForeignKey("dbo.Divisions", t => t.DivisionID)
                .ForeignKey("dbo.Districts", t => t.DistrictID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID)
                .ForeignKey("dbo.Upazilas", t => t.UpazilaID)
                .Index(t => t.BloodGroupID)
                .Index(t => t.DivisionID)
                .Index(t => t.DistrictID)
                .Index(t => t.UpazilaID)
                .Index(t => t.DepartmentID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.BloodGroups",
                c => new
                    {
                        BloodGroupID = c.Int(nullable: false, identity: true),
                        BloodGroupName = c.String(),
                    })
                .PrimaryKey(t => t.BloodGroupID);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        DistrictID = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(),
                        DivisionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistrictID)
                .ForeignKey("dbo.Divisions", t => t.DivisionID, cascadeDelete: true)
                .Index(t => t.DivisionID);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        DivisionID = c.Int(nullable: false, identity: true),
                        DivisionName = c.String(),
                    })
                .PrimaryKey(t => t.DivisionID);
            
            CreateTable(
                "dbo.Upazilas",
                c => new
                    {
                        UpazilaID = c.Int(nullable: false, identity: true),
                        UpazilaName = c.String(),
                        DistrictID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UpazilaID)
                .ForeignKey("dbo.Districts", t => t.DistrictID, cascadeDelete: true)
                .Index(t => t.DistrictID);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffID = c.Int(nullable: false, identity: true),
                        StaffName = c.String(nullable: false, maxLength: 20),
                        ContactAddress = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        DepartmentID = c.Int(),
                    })
                .PrimaryKey(t => t.StaffID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ConfirmedEmail = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Staffs", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Patients", "UpazilaID", "dbo.Upazilas");
            DropForeignKey("dbo.Patients", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Patients", "DistrictID", "dbo.Districts");
            DropForeignKey("dbo.Upazilas", "DistrictID", "dbo.Districts");
            DropForeignKey("dbo.Patients", "DivisionID", "dbo.Divisions");
            DropForeignKey("dbo.Districts", "DivisionID", "dbo.Divisions");
            DropForeignKey("dbo.Patients", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Patients", "BloodGroupID", "dbo.BloodGroups");
            DropForeignKey("dbo.Admits", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Doctors", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Admits", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Staffs", new[] { "DepartmentID" });
            DropIndex("dbo.Upazilas", new[] { "DistrictID" });
            DropIndex("dbo.Districts", new[] { "DivisionID" });
            DropIndex("dbo.Patients", new[] { "DoctorID" });
            DropIndex("dbo.Patients", new[] { "DepartmentID" });
            DropIndex("dbo.Patients", new[] { "UpazilaID" });
            DropIndex("dbo.Patients", new[] { "DistrictID" });
            DropIndex("dbo.Patients", new[] { "DivisionID" });
            DropIndex("dbo.Patients", new[] { "BloodGroupID" });
            DropIndex("dbo.Doctors", new[] { "DepartmentID" });
            DropIndex("dbo.Admits", new[] { "PatientID" });
            DropIndex("dbo.Admits", new[] { "DepartmentID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Staffs");
            DropTable("dbo.Upazilas");
            DropTable("dbo.Divisions");
            DropTable("dbo.Districts");
            DropTable("dbo.BloodGroups");
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
            DropTable("dbo.Departments");
            DropTable("dbo.Admits");
        }
    }
}
