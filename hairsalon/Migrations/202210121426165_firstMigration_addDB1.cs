namespace hairsalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration_addDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Role", c => c.String(maxLength: 30));
            AlterColumn("dbo.Accounts", "FirstName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Accounts", "LastName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Accounts", "Password", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Password", c => c.String());
            AlterColumn("dbo.Accounts", "LastName", c => c.String());
            AlterColumn("dbo.Accounts", "FirstName", c => c.String());
            DropColumn("dbo.Accounts", "Role");
        }
    }
}
