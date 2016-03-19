using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationApp.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int ClientID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }

        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
    }
}