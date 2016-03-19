namespace ReservationApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ReservationApp.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<ReservationApp.DAL.ReservationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReservationApp.DAL.ReservationContext context)
        {
            var clients = new List<Client>
            {
                new Client{FirstName = "Jacob", LastName = "Johnson", Phone = "317-000-1111", City = "Overland Park", State = "KS", StrAdd = "111 w 139th Street"},
                new Client{FirstName = "Jacob", LastName = "Hatcher", Phone = "317-222-3333", City = "Indianapolis", State = "IN", StrAdd = "36 Overland Way"},
                new Client{FirstName = "Derrick", LastName = "Lane", Phone = "317-444-5555", City = "North Kansas City", State = "MO", StrAdd = "903 Somewhere Drive"},
                new Client{FirstName = "Nick", LastName = "Kolar", Phone = "317-777-8888", City = "Overland Park", State = "KS", StrAdd = "321 Left Street"},
                new Client{FirstName = "Jose", LastName = "Jefferston", Phone = "241-546-9706", City = "Liberty", State = "MO", StrAdd = "214 Right Way"},
                new Client{FirstName = "Bryce", LastName = "Jonas", Phone = "133-345-753", City = "Cayuga", State = "IN", StrAdd = "124 Under Bridge"},
                new Client{FirstName = "Adam", LastName = "Riskell", Phone = "492-314-7532", City = "Monroe", State = "TN", StrAdd = "314 Over There"},
                new Client{FirstName = "Josh", LastName = "Joshing", Phone = "413-412-4213", City = "Lawrence", State = "KS", StrAdd = "2141 Nowhere Lane"}
            };

            clients.ForEach(x => context.Clients.AddOrUpdate(r => r.LastName, x));
            context.SaveChanges();

            var employees = new List<Employee>
            {
                new Employee{FirstName = "Michelle", LastName = "Roberts", Occupation = "Stylist", ID = 12345 },
                new Employee{FirstName = "John", LastName = "Bair", Occupation = "Barber", ID = 98765},
                new Employee{FirstName = "Michael", LastName = "Ryder", Occupation = "Aesthetician", ID = 11111},
                new Employee{FirstName = "Jerry", LastName = "Nost", Occupation = "Masseuse", ID = 15234},
                new Employee{FirstName = "Tim", LastName = "Levi", Occupation = "Chiropractor", ID = 27341}
            };

            employees.ForEach(x => context.Employees.AddOrUpdate(e => e.ID, x));
            context.SaveChanges();

            var reservations = new List<Reservation>
            {
                new Reservation{ClientID = clients.Single(x=>x.LastName == "Johnson").ID, EmployeeID = employees.Single(x=>x.ID == 15234).ID, Date = DateTime.Parse("3-20-2016"), Time = DateTime.Parse("3:45")},
                new Reservation{ClientID = clients.Single(x=>x.LastName == "Hatcher").ID, EmployeeID = employees.Single(x=>x.ID == 98765).ID, Date = DateTime.Parse("3-20-2016"), Time = DateTime.Parse("4:30")},
                new Reservation{ClientID = clients.Single(x=>x.LastName == "Riskell").ID, EmployeeID = employees.Single(x=>x.ID == 11111).ID, Date = DateTime.Parse("4-1-2016"), Time = DateTime.Parse("2:30")},
                new Reservation{ClientID = clients.Single(x=>x.LastName == "Joshing").ID, EmployeeID = employees.Single(x=>x.ID == 12345).ID, Date = DateTime.Parse("3-29-2016"), Time = DateTime.Parse("7:15")},
                new Reservation{ClientID = clients.Single(x=>x.LastName == "Lane").ID, EmployeeID = employees.Single(x=>x.ID == 27341).ID, Date = DateTime.Parse("4-10-2016"), Time = DateTime.Parse("3:14")}
            };

            foreach(Reservation r in reservations)
            {
                var reservationsInDataBase = context.Reservations.Where(
                    s =>
                        s.Client.ID == r.ClientID &&
                        s.Employee.ID == r.EmployeeID).SingleOrDefault();
                if (reservationsInDataBase == null)
                {
                    context.Reservations.Add(r);
                }
            }
            context.SaveChanges();
        }
    }
}
