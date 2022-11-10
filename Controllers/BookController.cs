
using LibrarySite.Models;
using LibrarySite.MVC6.DataContext;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System.Reflection.Metadata;

namespace LibrarySite.Controllers
{
    public class BookController : Controller
    {
        /// <summary>
        /// 도서 리스트
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page, string keyword, string searchKind)
        {
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


                var list = new List<Book>();

                if (string.IsNullOrWhiteSpace(keywordSearch))
                {
                    list = db.Books.OrderByDescending (x => x.BookNo)
                            .Skip(skip_count)
                            .Take(maxListCount).ToList();
                    totalCount = db.Books.Count();
                }
                else
                {
                    switch(searchKind)
                    {
                        case "BookTitle":
                            list = db.Books.Where(x => x.BookTitle.Contains(keywordSearch)).ToList();
                            totalCount = db.Books.Where(x => x.BookTitle.Contains(keywordSearch)).Count();
                            break;

                        case "BookWriter":
                            list = db.Books.Where(x => x.BookWriter.Contains(keywordSearch)).ToList();
                            totalCount = db.Books.Where(x => x.BookWriter.Contains(keywordSearch)).Count();
                            break;
                        case "BookPublisher":
                            list = db.Books.Where(x => x.BookPublisher.Contains(keywordSearch)).ToList();
                            totalCount = db.Books.Where(x => x.BookPublisher.Contains(keywordSearch)).Count();
                            break;
                        case "BookPublish_date":
                            list = db.Books.Where(x => x.BookPublish_date.Contains(keywordSearch)).ToList();
                            totalCount = db.Books.Where(x => x.BookPublish_date.Contains(keywordSearch)).Count();
                            break;
                        case "BookLocation":
                            list = db.Books.Where(x => x.BookLocation.Contains(keywordSearch)).ToList();
                            totalCount = db.Books.Where(x => x.BookLocation.Contains(keywordSearch)).Count();
                            break;
                    }

                    list = list.OrderByDescending (x => x.BookNo)
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
        public IActionResult Detail(int bookNo)
        {
            using (var db = new LibrarySiteDbContext())
            {
                var book = db.Books.FirstOrDefault(n => n.BookNo.Equals(bookNo));
                return View(book);
            }
        }

        /// <summary>
        /// 도서 추가
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
        public IActionResult Add(Book model)
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
                    db.Books.Add(model);

                    if (db.SaveChanges() > 0)
                    {
                        return Redirect("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "도서을 저장할 수 없습니다.");
            }
            return View(model);
        }

        /// <summary>
        /// 도서 수정
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
        public IActionResult Delete(int bookNo)
        {
            using (var db = new LibrarySiteDbContext())
            {
                Book book = db.Books.Find(bookNo);
                db.Books.Remove(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
