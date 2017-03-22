namespace X_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "RealName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RealName", c => c.String());
        }
    }
}
