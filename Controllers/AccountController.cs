using LibrarySite.Models;
using LibrarySite.MVC6.DataContext;
using LibrarySite.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySite.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            //ID, 비밀번호 - 필수
            if (ModelState.IsValid)
            {
                using (var db = new LibrarySiteDbContext())
                {
                    var user = db.Users
                        .FirstOrDefault(u => u.UserId.Equals(model.UserId) && u.UserPassword.Equals(model.UserPassword));
                
                    if(user != null)
                    {
                        //로그인에 성공했을 때
                        HttpContext.Session.SetString("USER_LOGIN_KEY",user.UserId);
                        return RedirectToAction("LoginSuccess", "Home");
                    }

                }

                //로그인에 실패했을 때
                //사용자 ID 자체가 회원가입 x 인 경우
                ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USER_LOGIN_KEY");
            //모든 세션 제거 - HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 회원 가입
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new LibrarySiteDbContext())
                {
                    db.Users.Add(model);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
