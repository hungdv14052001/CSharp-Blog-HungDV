using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CShap_Blog_HungDV.Models
{
    public class Category
    {
        private int id;
        private string name;

        public Category()
        {

        }

        public Category(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}