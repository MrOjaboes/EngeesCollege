namespace EngeesCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        CourseCode = c.String(nullable: false, maxLength: 50),
                        Category = c.Int(),
                        Credits = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        StartDate = c.DateTime(nullable: false),
                        FacultyId = c.Int(nullable: false),
                        InstructorID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Instructors", t => t.InstructorID)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .Index(t => t.FacultyId)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Salary = c.Decimal(nullable: false, storeType: "money"),
                        Qualification = c.Int(),
                        MaritalStatus = c.Int(),
                        Gender = c.Int(),
                        HireDate = c.DateTime(nullable: false),
                        DoB = c.DateTime(nullable: false),
                        Age = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OfficeAssignments",
                c => new
                    {
                        InstructorID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                        Address = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.InstructorID)
                .ForeignKey("dbo.Instructors", t => t.InstructorID)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        FacultyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FacultyId);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        EnrollmentID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(),
                        Section = c.Int(),
                        EnrollmentDate = c.DateTime(nullable: false),
                        DoB = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        DateInserted = c.DateTime(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        InstructorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.InstructorID })
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Instructors", t => t.InstructorID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.InstructorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseInstructor", "InstructorID", "dbo.Instructors");
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Results", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Results", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Enrollments", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Enrollments", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Courses", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Departments", "InstructorID", "dbo.Instructors");
            DropForeignKey("dbo.OfficeAssignments", "InstructorID", "dbo.Instructors");
            DropIndex("dbo.CourseInstructor", new[] { "InstructorID" });
            DropIndex("dbo.CourseInstructor", new[] { "CourseID" });
            DropIndex("dbo.Results", new[] { "StudentID" });
            DropIndex("dbo.Results", new[] { "CourseID" });
            DropIndex("dbo.Enrollments", new[] { "StudentID" });
            DropIndex("dbo.Enrollments", new[] { "CourseID" });
            DropIndex("dbo.OfficeAssignments", new[] { "InstructorID" });
            DropIndex("dbo.Departments", new[] { "InstructorID" });
            DropIndex("dbo.Departments", new[] { "FacultyId" });
            DropIndex("dbo.Courses", new[] { "DepartmentID" });
            DropTable("dbo.CourseInstructor");
            DropTable("dbo.Results");
            DropTable("dbo.Students");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Faculties");
            DropTable("dbo.OfficeAssignments");
            DropTable("dbo.Instructors");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
        }
    }
}
