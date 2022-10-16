namespace hairsalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Password", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Password", c => c.String(maxLength: 60));
        }
    }
}
