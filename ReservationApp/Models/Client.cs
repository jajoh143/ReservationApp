using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationApp.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string StrAdd { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}