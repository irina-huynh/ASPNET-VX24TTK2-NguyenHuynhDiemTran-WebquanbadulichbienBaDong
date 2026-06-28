using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VX24TTK2.Models
{
    public class Destination
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Type { get; set; } // Bãi biển, Vui chơi, Nhà hàng, Khách sạn

        public string Address { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string GoogleMapUrl { get; set; }

        public string OpenTime { get; set; }

        public string CloseTime { get; set; }

        public virtual ICollection<CustomerReview> CustomerReviews { get; set; }
        public virtual ICollection<TourSchedule> TourSchedules { get; set; }
    }
}