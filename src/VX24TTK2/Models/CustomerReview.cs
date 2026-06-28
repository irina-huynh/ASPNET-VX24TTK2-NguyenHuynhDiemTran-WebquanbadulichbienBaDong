using System;

namespace VX24TTK2.Models
{
    public class CustomerReview
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int DestinationId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }

        public virtual Destination Destination { get; set; }
    }
}