using System.Linq;
using System.Web.Mvc;
using VX24TTK2.Models;

namespace VX24TTK2.Controllers
{
    public class AccountController : Controller
    {
        private BaDongTourDbContext db = new BaDongTourDbContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var account = db.Users.FirstOrDefault(x =>
                x.Username == username &&
                x.Password == password);

            if (account == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu.";
                return View();
            }

            if (account.IsLocked)
            {
                ViewBag.Error = "Tài khoản của bạn đã bị khóa.";
                return View();
            }

            Session["UserId"] = account.Id;
            Session["UserName"] = account.FullName;
            Session["Role"] = account.Role;

            if (account.Role == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Remove("UserId");
            Session.Remove("UserName");
            Session.Remove("Role");
            Session.Remove("UserAvatar");

            
            Session.Remove("Admin");

            Session.Clear();
            Session.Abandon();

            Response.Cookies.Clear();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string username, string phone, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp.";
                return View();
            }

            var user = db.Users.FirstOrDefault(x =>
                x.Username == username &&
                x.Phone == phone &&
                x.Role == "User");

            if (user == null)
            {
                ViewBag.Error = "Tên đăng nhập hoặc số điện thoại không đúng.";
                return View();
            }

            if (user.IsLocked)
            {
                ViewBag.Error = "Tài khoản này đang bị khóa, không thể đổi mật khẩu.";
                return View();
            }

            user.Password = newPassword;
            db.SaveChanges();

            ViewBag.Success = "Đổi mật khẩu thành công. Bạn có thể đăng nhập lại.";
            return View();
        }
    }
}