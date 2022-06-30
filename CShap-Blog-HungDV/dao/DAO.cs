using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CShap_Blog_HungDV.Models;

namespace CShap_Blog_HungDV.dao
{
    public class DAO
    {
        string str = @"Data Source=DESKTOP-FA5AISU\SQLEXPRESS;Initial Catalog=AspMVC;Integrated Security=True";
        SqlConnection connection;
        SqlCommand command = new SqlCommand();
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataTable dataTable = new DataTable();

        /// <summary>
        /// 
        /// </summary>
        public DAO()
        {
            connection = new SqlConnection(str);
            connection.Open();
        }

        /// <summary>
        /// get all Blog in DataBase
        /// </summary>
        /// <returns>List<Blog></returns>
        public List<Blog> getListBlog()
        {
            List<Blog> listBlog = new List<Blog>();
            command = connection.CreateCommand();
            command.CommandText = "select * from tblBlog";
            dataAdapter.SelectCommand = command;
            dataTable.Clear();
            dataAdapter.Fill(dataTable);
            foreach(DataRow row in dataTable.Rows)
            {
                Blog blog = new Blog();
                blog.Id = int.Parse(row["id"].ToString());
                blog.Title = row["Title"].ToString();
                blog.Des = row["des"].ToString();
                blog.Detail = row["detail"].ToString();
                blog.Category = int.Parse(row["category"].ToString());
                blog.IsPublic = bool.Parse(row["isPublic"].ToString());
                blog.DatePublic = getDate(row["datePublic"].ToString()).ToString("yyyy/MM/dd");
                blog.Position = getPosition(row["position"].ToString());
                blog.Thumbs = row["thumbs"].ToString();
                listBlog.Add(blog);
            }
            return listBlog;
        }

        /// <summary>
        /// get all Blog in DataBase with title of Blog contains keySearch
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        public List<Blog> getListBlogByKeySearch(string keySearch)
        {
            List<Blog> listBlog = new List<Blog>();
            command = connection.CreateCommand();
            command.CommandText = "select * from tblBlog";
            dataAdapter.SelectCommand = command;
            dataTable.Clear();
            dataAdapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["Title"].ToString().Contains(keySearch)) 
                {
                    Blog blog = new Blog();
                    blog.Id = int.Parse(row["id"].ToString());
                    blog.Title = row["Title"].ToString();
                    blog.Des = row["des"].ToString();
                    blog.Detail = row["detail"].ToString();
                    blog.Category = int.Parse(row["category"].ToString());
                    blog.IsPublic = bool.Parse(row["isPublic"].ToString());
                    blog.DatePublic = getDate(row["datePublic"].ToString()).ToString("yyyy/MM/dd");
                    blog.Position = getPosition(row["position"].ToString());
                    blog.Thumbs = row["thumbs"].ToString();
                    listBlog.Add(blog);
                }
            }
            return listBlog;
        }

        /// <summary>
        /// get Blog from DataBase by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Blog</returns>
        public Blog getBlogById(int id)
        {
            return this.getListBlog().Where(blog => blog.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// add Blog into DataBase
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>bool</returns>
        public bool addBlog(Blog blog)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "insert into tblBlog(title, des, detail, category, isPublic, datePublic, position, thumbs) " +
                    "values(N'"+blog.Title+ "', N'"+blog.Des+ "', N'"+blog.Detail+"', "+blog.Category+ ", '"+blog.IsPublic+ "', '"+blog.DatePublic+ "', N'"+ getPosition(blog.Position)+ "', '"+ blog.Thumbs + "')";
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// update Blog in DataBase
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>bool</returns>
        public bool updateBlog(Blog blog)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "update tblBlog set " +
                    "title= N'" + blog.Title + "', des = N'" + blog.Des + "', detail=N'" + blog.Detail + "', category=" + blog.Category + ", isPublic='" + blog.IsPublic + "', datePublic='" + blog.DatePublic + "', position=N'" + getPosition(blog.Position) + "', thumbs=N'" + blog.Thumbs + "' " +
                    "where id= "+blog.Id;
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// delete Blog from DataBase
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>bool</returns>
        public bool deleteBlog(Blog blog)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "delete from tblBlog "+
                    "where id= " + blog.Id;
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// delete Blog from Database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public bool deleteBlogById(int id)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "delete from tblBlog " +
                    "where id= " + id;
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// get List Position from string of list Positiono
        /// </summary>
        /// <param name="stringOfPosition"></param>
        /// <returns>List<bool></returns>
        public List<bool> getPosition(string stringOfPosition)
        {
            List<bool> listResult = new List<bool> { false, false, false, false};
            foreach(string position in stringOfPosition.Split(','))
            {
                listResult[int.Parse(position)-1] = true;
            }
            return listResult;
        }

        /// <summary>
        /// get string of list Position
        /// </summary>
        /// <param name="listPosition"></param>
        /// <returns>string</returns>
        public string getPosition(List<bool> listPosition)
        {
            string result = "";
            List<int> listPositionByInt = new List<int>();
            for (int i=0; i<4; i++)
            {
                if (listPosition[i])
                {
                    listPositionByInt.Add(i + 1);
                    if (i > 0)
                    {
                        result += "," + listPositionByInt[i].ToString();
                    }
                }
            }
            result = listPositionByInt[0]+result;
            return result;
        }

        /// <summary>
        /// get Date of Blog from string
        /// </summary>
        /// <param name="date"></param>
        /// <returns>DateTime</returns>
        private DateTime getDate(string date)
        {
            string[] time = date.Split('/', ' ', ':');
            int nam = int.Parse(time[2]);
            int thang = int.Parse(time[0]);
            int ngay = int.Parse(time[1]);
            DateTime kq = new DateTime(nam, thang, ngay);
            return kq;
        }
    }
}