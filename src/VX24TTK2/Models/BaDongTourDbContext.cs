using System.Data.Entity;

namespace VX24TTK2.Models
{
    public class BaDongTourDbContext : DbContext
    {
        public BaDongTourDbContext() : base("BaDongTourDbContext")
        {
            Database.SetInitializer<BaDongTourDbContext>(null);
        }

       
        public DbSet<User> Users { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; }
        public DbSet<TourSchedule> TourSchedules { get; set; }
        public DbSet<TourCost> TourCosts { get; set; }
        public DbSet<TravelCompany> TravelCompanies { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        
    }
}