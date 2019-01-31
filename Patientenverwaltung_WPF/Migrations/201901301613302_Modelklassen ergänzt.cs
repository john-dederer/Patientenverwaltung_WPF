namespace Patientenverwaltung_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modelklassenergänzt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Healthinsurances",
                c => new
                    {
                        HealthinsuranceId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Street = c.String(nullable: false),
                        Streetnumber = c.String(nullable: false),
                        Postalcode = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        LastChange = c.String(),
                    })
                .PrimaryKey(t => t.HealthinsuranceId);
            
            CreateTable(
                "dbo.Treatments",
                c => new
                    {
                        TreatmentId = c.Long(nullable: false, identity: true),
                        PatientId = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        Other = c.String(),
                        LastChange = c.String(),
                    })
                .PrimaryKey(t => t.TreatmentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Treatments");
            DropTable("dbo.Healthinsurances");
        }
    }
}
