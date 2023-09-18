namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate____ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Company = c.String(),
                        Price = c.Single(nullable: false),
                        Type = c.String(),
                        PhotoPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        PhotoPath = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        IsCompanyRepresentative = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserModels");
            DropTable("dbo.ProductModels");
        }
    }
}
