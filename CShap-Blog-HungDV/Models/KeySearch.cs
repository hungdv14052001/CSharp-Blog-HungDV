using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CShap_Blog_HungDV.Models
{
    public class KeySearch
    {
        private string data;

        public KeySearch()
        {
            this.data = "";
        }
        public KeySearch(string data)
        {
            this.data = data;
        }
        public string Data { get => data; set => data = value; }
    }
}