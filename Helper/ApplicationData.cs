using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GumTree.Helper
{
    public class ApplicationData
    {
        public static string Captcha_Username = string.Empty;
        public static string Captcha_Password = string.Empty;
        public static string Title = string.Empty;
        public static string Description = string.Empty;
        public static string Price = string.Empty;
        public static string SingleImageDelay = string.Empty;
        public static string SearchData = string.Empty;
        public static string PlaceId = string.Empty;
        public static string PlaceId1 = string.Empty;
        public static string PlaceId2 = string.Empty;
        public static List<string> lstImages = new List<string>();
        public static string ResponseUser = string.Empty;
        public static bool Isloggedin { get; set; }
    }
}
