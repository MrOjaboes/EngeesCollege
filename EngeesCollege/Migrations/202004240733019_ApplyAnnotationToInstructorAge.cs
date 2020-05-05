namespace EngeesCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyAnnotationToInstructorAge : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Instructors", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructors", "Age", c => c.Short(nullable: false));
        }
    }
}
