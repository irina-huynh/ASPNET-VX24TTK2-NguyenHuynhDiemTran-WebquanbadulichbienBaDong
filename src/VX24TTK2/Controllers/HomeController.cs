using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VX24TTK2.Models;

namespace VX24TTK2.Controllers
{
    public class HomeController : Controller
    {
        private BaDongTourDbContext db = new BaDongTourDbContext();

        public ActionResult Index()
        {
            ViewBag.RecentReviews = db.CustomerReviews
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt)
                .Take(3)
                .ToList();

            return View(db.Destinations.ToList());
        }

        public ActionResult Destinations()
        {
            return View(db.Destinations.ToList());
        }

        public ActionResult DestinationDetail(int id)
        {
            var item = db.Destinations.Find(id);

            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        public ActionResult Schedules()
        {
            return View(db.TourSchedules.ToList());
        }

        public ActionResult Companies()
        {
            return View(db.TravelCompanies.ToList());
        }

        public ActionResult Reviews()
        {
            var list = db.CustomerReviews
                .Include(x => x.User)
                .Include(x => x.Destination)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return View(list);
        }

        [HttpPost]
        public ActionResult Reviews(CustomerReview model, HttpPostedFileBase imageFile)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (imageFile != null && imageFile.ContentLength > 0)
            {
                string fileName = System.Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                string path = Server.MapPath("~/Content/Uploads/" + fileName);
                imageFile.SaveAs(path);

                model.ImageUrl = "/Content/Uploads/" + fileName;
            }

            model.UserId = (int)Session["UserId"];
            model.CreatedAt = System.DateTime.Now;

            db.CustomerReviews.Add(model);
            db.SaveChanges();

            return RedirectToAction("Reviews");
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}