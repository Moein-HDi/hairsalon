namespace hairsalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondmigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "PhoneNumber", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "PhoneNumber", c => c.Int(nullable: false));
        }
    }
}
