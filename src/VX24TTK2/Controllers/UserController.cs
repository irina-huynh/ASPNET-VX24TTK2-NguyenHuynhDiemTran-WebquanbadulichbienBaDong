using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VX24TTK2.Models;

namespace VX24TTK2.Controllers
{
    public class UserController : Controller
    {
        private BaDongTourDbContext db = new BaDongTourDbContext();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model, string confirmPassword, HttpPostedFileBase avatarFile)
        {
            if (model.Password != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp.";
                return View();
            }

            if (db.Users.Any(x => x.Username == model.Username))
            {
                ViewBag.Error = "Tên đăng nhập này đã được sử dụng.";
                return View();
            }

            if (avatarFile != null && avatarFile.ContentLength > 0)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(avatarFile.FileName);
                string path = Server.MapPath("~/Content/Uploads/" + fileName);
                avatarFile.SaveAs(path);
                model.Avatar = "/Content/Uploads/" + fileName;
            }
            else
            {
                model.Avatar = "/Content/Uploads/default-user.png";
            }

            model.Role = "User";
            model.CreatedDate = DateTime.Now;

            db.Users.Add(model);
            db.SaveChanges();

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Profile()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int id = (int)Session["UserId"];
            var user = db.Users.Find(id);

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateProfile(string fullName, string phone, HttpPostedFileBase avatarFile)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = Convert.ToInt32(Session["UserId"]);
            var user = db.Users.Find(userId);

            if (user == null)
                return HttpNotFound();

            user.FullName = fullName;
            user.Phone = phone;

            if (avatarFile != null && avatarFile.ContentLength > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(avatarFile.FileName);
                string path = Server.MapPath("~/Content/Uploads/" + fileName);

                avatarFile.SaveAs(path);

                user.Avatar = "/Content/Uploads/" + fileName;
            }

            db.SaveChanges();

            Session["UserName"] = user.FullName;

            TempData["Success"] = "Cập nhật thông tin thành công.";

            return RedirectToAction("Profile");
        }
    }
}