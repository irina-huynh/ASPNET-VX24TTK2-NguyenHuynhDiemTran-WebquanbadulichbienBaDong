using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VX24TTK2.Models;

namespace VX24TTK2.Controllers
{
    public class AdminController : Controller
    {
        private BaDongTourDbContext db = new BaDongTourDbContext();

        private bool IsAdmin()
        {
            return Session["Role"] != null
                && Session["Role"].ToString() == "Admin";
        }

        public ActionResult Dashboard()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            ViewBag.TotalDestinations = db.Destinations.Count();
            ViewBag.TotalReviews = db.CustomerReviews.Count();
            ViewBag.AverageRating = db.CustomerReviews.Any()
                ? db.CustomerReviews.Average(x => x.Rating)
                : 0;

            return View();
        }

        public ActionResult Destinations()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            return View(db.Destinations.ToList());
        }

        public ActionResult CreateDestination()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public ActionResult CreateDestination(Destination model, HttpPostedFileBase imageFile)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (imageFile != null && imageFile.ContentLength > 0)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                string path = Server.MapPath("~/Content/Uploads/" + fileName);
                imageFile.SaveAs(path);
                model.ImageUrl = "/Content/Uploads/" + fileName;
            }

            db.Destinations.Add(model);
            db.SaveChanges();

            return RedirectToAction("Destinations");
        }

        public ActionResult EditDestination(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var destination = db.Destinations.Find(id);
            if (destination == null)
                return HttpNotFound();

            return View(destination);
        }

        [HttpPost]
        public ActionResult EditDestination(Destination model, HttpPostedFileBase imageFile)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var destination = db.Destinations.Find(model.Id);
            if (destination == null)
                return HttpNotFound();

            destination.Name = model.Name;
            destination.Type = model.Type;
            destination.Address = model.Address;
            destination.Description = model.Description;
            destination.Price = model.Price;
            destination.GoogleMapUrl = model.GoogleMapUrl;
            destination.OpenTime = model.OpenTime;
            destination.CloseTime = model.CloseTime;

            if (imageFile != null && imageFile.ContentLength > 0)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                string path = Server.MapPath("~/Content/Uploads/" + fileName);
                imageFile.SaveAs(path);
                destination.ImageUrl = "/Content/Uploads/" + fileName;
            }

            db.SaveChanges();

            return RedirectToAction("Destinations");
        }

        public ActionResult DeleteDestination(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var destination = db.Destinations.Find(id);
            if (destination != null)
            {
                db.Destinations.Remove(destination);
                db.SaveChanges();
            }

            return RedirectToAction("Destinations");
        }

        public ActionResult TourSchedules()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            return View(db.TourSchedules.ToList());
        }

        public ActionResult CreateTourSchedule()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public ActionResult CreateTourSchedule(TourSchedule model)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            db.TourSchedules.Add(model);
            db.SaveChanges();

            return RedirectToAction("TourSchedules");
        }

        public ActionResult EditTourSchedule(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var item = db.TourSchedules.Find(id);
            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        [HttpPost]
        public ActionResult EditTourSchedule(TourSchedule model)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var item = db.TourSchedules.Find(model.Id);
            if (item == null)
                return HttpNotFound();

            item.DestinationId = model.DestinationId;
            item.TravelCompanyId = model.TravelCompanyId;
            item.Title = model.Title;
            item.DayNumber = model.DayNumber;
            item.TimeText = model.TimeText;
            item.Description = model.Description;

            db.SaveChanges();

            return RedirectToAction("TourSchedules");
        }

        public ActionResult DeleteTourSchedule(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var item = db.TourSchedules.Find(id);
            if (item != null)
            {
                db.TourSchedules.Remove(item);
                db.SaveChanges();
            }

            return RedirectToAction("TourSchedules");
        }

        public ActionResult Reviews()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var list = db.CustomerReviews
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return View(list);
        }

        public ActionResult DeleteReview(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var review = db.CustomerReviews.Find(id);
            if (review != null)
            {
                db.CustomerReviews.Remove(review);
                db.SaveChanges();
            }

            return RedirectToAction("Reviews");
        }

        public ActionResult TravelCompanies()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            return View(db.TravelCompanies.ToList());
        }

        public ActionResult CreateTravelCompany()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public ActionResult CreateTravelCompany(TravelCompany model)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            db.TravelCompanies.Add(model);
            db.SaveChanges();

            return RedirectToAction("TravelCompanies");
        }

        public ActionResult EditTravelCompany(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var item = db.TravelCompanies.Find(id);
            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        [HttpPost]
        public ActionResult EditTravelCompany(TravelCompany model)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var item = db.TravelCompanies.Find(model.Id);
            if (item == null)
                return HttpNotFound();

            item.CompanyName = model.CompanyName;
            item.Phone = model.Phone;
            item.Email = model.Email;
            item.Website = model.Website;
            item.Address = model.Address;

            db.SaveChanges();

            return RedirectToAction("TravelCompanies");
        }

        public ActionResult DeleteTravelCompany(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var item = db.TravelCompanies.Find(id);
            if (item != null)
            {
                db.TravelCompanies.Remove(item);
                db.SaveChanges();
            }

            return RedirectToAction("TravelCompanies");
        }

        // Quản lý người dùng
        public ActionResult Users()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var list = db.Users
                .OrderByDescending(x => x.CreatedDate)
                .ToList();

            return View(list);
        }

        public ActionResult DeleteUser(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            int currentUserId = (int)Session["UserId"];

            if (id == currentUserId)
            {
                TempData["Error"] = "Không thể xóa tài khoản đang đăng nhập.";
                return RedirectToAction("Users");
            }

            var user = db.Users.Find(id);

            if (user != null && user.Role != "Admin")
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }

            return RedirectToAction("Users");
        }

        public ActionResult ToggleLockUser(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var user = db.Users.Find(id);

            if (user == null)
                return RedirectToAction("Users");

            // Không khóa Admin
            if (user.Role == "Admin")
                return RedirectToAction("Users");

            user.IsLocked = !user.IsLocked;

            db.SaveChanges();

            return RedirectToAction("Users");
        }

        public ActionResult UserDetail(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var user = db.Users.Find(id);

            if (user == null)
                return HttpNotFound();

            return View(user);
        }
    }

}