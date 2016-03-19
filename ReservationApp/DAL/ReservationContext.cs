using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReservationApp.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ReservationApp.DAL
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(): base("ReservationContext")
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}