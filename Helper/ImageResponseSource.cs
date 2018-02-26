using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GumTree.Helper
{
    public class ImageResponseSource
    {
        public int id { get; set; }
        public string url { get; set; }
        public object size { get; set; }
        public string thumbnailUrl { get; set; }
        public object error { get; set; }
        public string fileName { get; set; }
        public int position { get; set; }
    }

}
