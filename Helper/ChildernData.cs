using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GumTree.Helper
{
    public class ChildrenItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public string seoName { get; set; }
        public bool children { get; set; }
        public bool selected { get; set; }
        public List<object> childrenItems { get; set; }
        public bool drilled { get; set; }
    }

    public class ChildernData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string seoName { get; set; }
        public bool children { get; set; }
        public bool selected { get; set; }
        public List<ChildrenItem> childrenItems { get; set; }
        public bool drilled { get; set; }
    }
   
}
