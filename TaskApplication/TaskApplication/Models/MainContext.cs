using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Services.Description;
using TaskApplication.Migrations;

namespace TaskApplication.Models
{
    public class MainContext : DbContext
    {
        public DbSet<TaskModel> Tasks { get; set; }

        public MainContext()
            : base("name=TaskContextConnection")
        {
            Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainContext, Configuration>());

        }

        public MainContext(bool lazyLoadingEnabled = true)
            : base("name=TaskContextConnection")
        {
            Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
        }

        /// <summary>
        /// Create Db Main
        /// </summary>
        /// <returns></returns>
        public static MainContext Create()
        {
            return new MainContext();
        }

        /// <summary>
        /// Bla Bla Bla
        /// </summary>
        /// <param name="modelBuilder"></param>
        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.ToString();
        }*/
    }
}