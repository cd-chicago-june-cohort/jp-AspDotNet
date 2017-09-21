using System.ComponentModel.DataAnnotations;

namespace wedding_planner.Models {
    public class Attendance : BaseEntity {

        public int AttendanceId { get; set; }
 
        public int UserId { get; set; }
        public User User { get; set; }
 
        public int WeddingId { get; set; }
        public Wedding Wedding { get; set; }
        
    }
}