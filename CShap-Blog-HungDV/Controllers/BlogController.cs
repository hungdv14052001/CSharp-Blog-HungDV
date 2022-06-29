using CShap_Blog_HungDV.dao;
using CShap_Blog_HungDV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CShap_Blog_HungDV.Controllers
{
    public class BlogController : Controller
    {
        private DAO dao = new DAO();
        IList<Blog> listBlog = new DAO().getListBlog();
        List<Category> listCategory = new List<Category>
        {
            new Category(0, "Kinh tế"),
            new Category(1, "Thế Giới"),
            new Category(2, "Chính Trị"),
            new Category(3, "Showbit"),
            new Category(4, "Thời Sự"),
            new Category(5, "Giải Trí"),
            new Category(6, "Kinh Doanh"),
            new Category(7, "Giáo Dục"),
            new Category(8, "Thể Thao"),
            new Category(9, "Thể Thao"),
            new Category(10, "Thể Thao"),
            new Category(11, "Thể Thao")
        };
        List<Postion> listPostion = new List<Postion>
        {
            new Postion(1, "Việt Nam"),
            new Postion(2, "Châu Á"),
            new Postion(3, "Châu Âu"),
            new Postion(4, "Châu Mỹ"),
        };

        /// <summary>
        /// functon set View for BlogList.cshtml
        /// </summary>
        /// <returns>View() of BlogList</returns>
        public ActionResult BlogList()
        {
            ViewData["listCategory"] = listCategory;
            ViewData["listPostion"] = listPostion;
            ViewData["listBlog"] = listBlog;
            return View();
        }
        /// <summary>
        /// functon set View for BlogCreateEdit.cshtml
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View(blog)</returns>
        public ActionResult BlogCreateEdit(int id)
        {
            List<Blog> listBlog = dao.getListBlog();
            var blog = new Blog();
            if (id != 0)
            {
                blog= listBlog.Where(b => b.Id == id).FirstOrDefault();
            }
            else
            {
                blog = new Blog();
            }
            IList<SelectListItem> selectList= new List<SelectListItem>();
            foreach (Category category in listCategory)
            {
                selectList.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            }
            ViewBag.selectList = selectList;
            ViewData["listPostion"] = listPostion;
            ViewData["blog"] = blog;
            return View(blog);
        }
        /// <summary>
        /// functon to use when user Create Blog or Edit Blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>RedirectToAction("BlogList")</returns>
        [HttpPost]
        public ActionResult BlogCreateEdit(Blog blog)
        {
            if (blog.Id==0)
            {
                dao.addBlog(blog);
            }
            else
            {
                dao.updateBlog(blog);
            }
            return RedirectToAction("BlogList");
        }

        /// <summary>
        /// functon set View for BlogCreateEdit.cshtml
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        public ActionResult BlogSearch(string keySearch)
        {
            if (string.IsNullOrEmpty(keySearch))
            {
                keySearch = "";
            }
            ViewData["listCategory"] = listCategory;
            ViewData["listPostion"] = listPostion;
            ViewBag.key = keySearch;
            List<Blog> listBlogByKeySearch = dao.getListBlogByKeySearch(keySearch);
            return View(listBlogByKeySearch);
        }

        /// <summary>
        /// function to delete Blog from DataBase
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BlogDelete(int id)
        {
            var blog = listBlog.Where(b => b.Id == id).FirstOrDefault();
            dao.deleteBlog(blog);
            return RedirectToAction("BlogList");
        }
    }
}