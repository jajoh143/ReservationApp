using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationApp.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Occupation { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}