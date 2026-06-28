using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VX24TTK2.Models
{
    public class TravelCompany
    {
        [Key]
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public virtual ICollection<TourSchedule> TourSchedules { get; set; }
    }
}