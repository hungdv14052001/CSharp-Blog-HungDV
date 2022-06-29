using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CShap_Blog_HungDV.Models
{
    public class Blog
    {
        private int id;
        private string title;
        private string des;
        private string detail;
        private int category;
        private bool isPublic;
        private string datePublic;
        private List<bool> position;
        private string thumbs;

        public Blog()
        {
            this.id = 0;
            this.title = "";
            this.des = "";
            this.detail = "";
            this.category = 0;
            this.isPublic = true;
            this.datePublic = DateTime.Now.ToString("yyyy/MM/dd") ;
            this.position = new List<bool> { false, false, false, false};
            this.thumbs = "thumbs.jpg";
        }

        public Blog(int id, string title, string des, string detail, int category, bool isPublic, string datePublic, List<bool> position, string thumbs)
        {
            this.id = id;
            this.title = title;
            this.des = des;
            this.detail = detail;
            this.category = category;
            this.isPublic = isPublic;
            this.datePublic = datePublic;
            this.position = position;
            this.thumbs = thumbs;
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }

        [DataType(DataType.MultilineText)]
        public string Des { get => des; set => des = value; }

        [DataType(DataType.MultilineText)]
        public string Detail { get => detail; set => detail = value; }
        public int Category { get => category; set => category = value; }
        public bool IsPublic { get => isPublic; set => isPublic = value; }
        public string DatePublic { get => datePublic; set => datePublic = value; }
        public List<bool> Position { get => position; set => position = value; }
        public string Thumbs { get => thumbs; set => thumbs = value; }
    }
}