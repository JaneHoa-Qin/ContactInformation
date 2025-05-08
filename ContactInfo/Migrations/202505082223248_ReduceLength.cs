namespace ContactInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReduceLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "FirstName", c => c.String(maxLength: 20));
            AlterColumn("dbo.Contacts", "LastName", c => c.String(maxLength: 20));
            AlterColumn("dbo.Contacts", "Address1", c => c.String(maxLength: 50));
            AlterColumn("dbo.Contacts", "Address2", c => c.String(maxLength: 50));
            AlterColumn("dbo.Contacts", "State", c => c.String(maxLength: 20));
            AlterColumn("dbo.Contacts", "Country", c => c.String(maxLength: 30));
            AlterColumn("dbo.Contacts", "Email", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "Country", c => c.String());
            AlterColumn("dbo.Contacts", "State", c => c.String());
            AlterColumn("dbo.Contacts", "Address2", c => c.String());
            AlterColumn("dbo.Contacts", "Address1", c => c.String());
            AlterColumn("dbo.Contacts", "LastName", c => c.String());
            AlterColumn("dbo.Contacts", "FirstName", c => c.String());
        }
    }
}
