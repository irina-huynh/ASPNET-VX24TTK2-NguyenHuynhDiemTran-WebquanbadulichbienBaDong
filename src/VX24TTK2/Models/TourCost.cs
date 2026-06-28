using System.ComponentModel.DataAnnotations;

namespace VX24TTK2.Models
{
    public class TourCost
    {
        [Key]
        public int Id { get; set; }

        public string CostName { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}