using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GumTree.Helper
{
    public class AccountManager
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Proxy { get; set; }
        public string ProxyPort { get; set; }
        public string ProxyUsername { get; set; }
        public string ProxyPassword { get; set; }


        SoftBucketHttpUtillity httpHelper = new SoftBucketHttpUtillity();
    }
}
