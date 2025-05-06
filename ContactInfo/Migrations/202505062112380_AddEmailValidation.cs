namespace ContactInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Email", c => c.String());
        }
    }
}
