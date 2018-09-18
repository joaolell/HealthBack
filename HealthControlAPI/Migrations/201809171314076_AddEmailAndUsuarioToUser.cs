namespace HealthControlAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailAndUsuarioToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "Usuario", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Usuario");
            DropColumn("dbo.Users", "Email");
        }
    }
}
