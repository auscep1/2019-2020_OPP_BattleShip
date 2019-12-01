using KronoBattleship.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KronoBattleship.Datalayer
{

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public static ApplicationDbContext instance = null;
        private static object threadLock = new object();
        // updatinant duombaze reikia pakeist i public
       // public ApplicationDbContext()
        private ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext GetInstance()
        {
            if (instance == null)
            {
                lock (threadLock)
                {
                    instance = new ApplicationDbContext();
                }
            }
            return instance;
        }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Plane> Planes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BattleConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
