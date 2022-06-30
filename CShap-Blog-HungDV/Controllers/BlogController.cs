using CShap_Blog_HungDV.dao;
using CShap_Blog_HungDV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CShap_Blog_HungDV.Controllers
{
    public class BlogController : Controller
    {
        private DAO dao = new DAO();
        
        List<Category> listCategory = new EnumList().ListCategory;
        List<Postion> listPostion = new EnumList().ListPostion;

        /// <summary>
        /// set View for BlogList
        /// </summary>
        /// <returns>View() of BlogList</returns>
        public ActionResult BlogList()
        {
            ViewData["listCategory"] = listCategory;
            ViewData["listPostion"] = listPostion;
            List<Blog> listBlog = dao.getListBlog();
            return View(listBlog);
        }


        /// <summary>
        /// set View for BlogCreateEdit
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View(blog)</returns>
        public ActionResult BlogCreateEdit(int id)
        {
            List<Blog> listBlog = dao.getListBlog();
            var blog = new Blog();
            if (id != 0)
            {
                blog = dao.getBlogById(id);
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
        /// use when user Create Blog or Edit Blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>RedirectToAction("BlogList")</returns>
        [HttpPost]
        public ActionResult BlogCreateEdit(Blog blog)
        {
            bool resultSubmit = false;
            if (blog.Id==0)
            {
                if (dao.addBlog(blog))
                {
                    resultSubmit = true;
                }
            }
            else
            {
                if (dao.updateBlog(blog))
                {
                    resultSubmit = true;
                }
            }
            if (resultSubmit)
            {
                return RedirectToAction("BlogList");
            }
            else
            {
                return BlogCreateEdit(blog);
            }
        }

        /// <summary>
        /// set View for BlogCreateEdit
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
        /// delete Blog from DataBase
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BlogDelete(int id)
        {
            dao.deleteBlogById(id);
            return RedirectToAction("BlogList");
        }
    }
}