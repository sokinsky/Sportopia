using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace STA.Data {
    [DbConfigurationType(typeof(SqlConnect))]
    public class Context : LMS.Data.Context {
        public Context() : base("name=STA") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Migrations.Configuration>(true));
        }

        #region Tables
        //public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Models.Person> People { get; set; }      
        public DbSet<Models.User> Users { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            #region People
            modelBuilder.Entity<Models.Person>()
                .HasRequired(x => x.User)
                .WithRequiredPrincipal(x => x.Person);
            modelBuilder.Entity<Models.Person>()
                .HasRequired(x => x.Customer)
                .WithRequiredPrincipal(x => x.Person); 
            //modelBuilder.Entity<Models.Person>()
            //    .HasOptional(x => x.User).WithRequired(x=>x.);
            //modelBuilder.Entity<Models.Person>()
            //    .HasRequired(x => x.Customer)
            //    .WithRequiredPrincipal(x => x.Person);
            #endregion
        }
    }
    public class SqlConnect : DbConfiguration {
        public SqlConnect() {
            this.SetProviderServices("System.Data.SqlClient", System.Data.Entity.SqlServer.SqlProviderServices.Instance);
        }
    }

}
