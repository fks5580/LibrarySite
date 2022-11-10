
using LibrarySite.Models;
using LibrarySite.MVC6.DataContext;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace LibrarySite.Controllers
{
    public class NoteController : Controller
    {
        /// <summary>
        /// 게시물 리스트
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page, string keyword, string searchKind)
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            using (var db = new LibrarySiteDbContext())
            {
                //페이지
                int pageNum = 1;

                // maxListCount : 한 페이지에 불러올 컨텐츠의 수
                int maxListCount = 3;

                int totalCount = 0;

                //검색
                string keywordSearch = keyword ?? string.Empty;

                if (page > 0)
                {
                    pageNum = page;
                }

                ViewBag.Page = pageNum;

                int skip_count = (pageNum - 1) * maxListCount;


                var list = new List<Note>();

                if (string.IsNullOrWhiteSpace(keywordSearch))
                {
                    list = db.Notes.OrderByDescending (x => x.NoteNo)
                            .Skip(skip_count)
                            .Take(maxListCount).ToList();
                    totalCount = db.Notes.Count();
                }
                else
                {
                    switch(searchKind)
                    {
                        case "NoteTitle":
                            list = db.Notes.Where(x => x.NoteTitle.Contains(keywordSearch)).ToList();
                            totalCount = db.Notes.Where(x => x.NoteTitle.Contains(keywordSearch)).Count();
                            break;
                        case "UserNo":
                            list = db.Notes.Where(x => Convert.ToString(x.UserNo).Contains(keywordSearch)).ToList();
                            totalCount = db.Notes.Where(x => Convert.ToString(x.UserNo).Contains(keywordSearch)).Count();
                            break;
                    }

                    list = list.OrderByDescending (x => x.NoteNo)
                            .Skip(skip_count)
                            .Take(maxListCount).ToList();
                    
                }

                ViewBag.Page = pageNum;
                ViewBag.TotalCount = totalCount;
                ViewBag.MaxListCount = maxListCount;
                ViewBag.SearchKind = searchKind;
                ViewBag.Keyword = keyword;

                return View(list);

            }

        }
        public IActionResult Detail(int noteNo)
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            using (var db = new LibrarySiteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                return View(note);
            }
        }

        /// <summary>
        /// 게시물 추가
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(Note model)
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());

            if (ModelState.IsValid)
            {
                using (var db = new LibrarySiteDbContext())
                {
                    db.Notes.Add(model);

                    if (db.SaveChanges() > 0)
                    {
                        return Redirect("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
            }
            return View(model);
        }

        /// <summary>
        /// 게시물 수정
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        /// <summary>
        /// 게시물 삭제
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(int noteNo)
        {
            using (var db = new LibrarySiteDbContext())
            {
                Note note = db.Notes.Find(noteNo);
                db.Notes.Remove(note);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
