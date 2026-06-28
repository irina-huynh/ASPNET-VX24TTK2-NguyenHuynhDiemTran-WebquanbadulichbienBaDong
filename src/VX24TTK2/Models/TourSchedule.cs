using System.ComponentModel.DataAnnotations;

namespace VX24TTK2.Models
{
    public class TourSchedule
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public int DayNumber { get; set; }
        public string TimeText { get; set; }
        public string Description { get; set; }
        public int? DestinationId { get; set; }
        public int? TravelCompanyId { get; set; }

        public virtual Destination Destination { get; set; }
        public virtual TravelCompany TravelCompany { get; set; }
    }
}