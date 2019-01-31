using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Patientenverwaltung_WPF
{
    public class PatientContext : DbContext
    {
        public PatientContext() : base("name=patientEntities")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PatientContext, Patientenverwaltung_WPF.Migrations.Configuration>());
        }

        public DbSet<Patient> Patient { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<Treatment> Treatment { get; set; }

        public DbSet<Healthinsurance> Healthinsurance { get; set; }

    }
}
