using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wedding_planner.Models {
    public class Wedding : BaseEntity {

        public int WeddingId { get; set; }
        public string WedderOne { get; set; }
        public string WedderTwo { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }

        public List<Attendance> Guests { get; set; }

        public Wedding() {
            Guests = new List<Attendance>();
        }
        
    }
}