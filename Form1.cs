using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GumTree.Helper;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace GumTree
{
    public partial class Form1 : Form
    {


        string referer = "http://www.washwasha.org/afdal2017/default.aspx?c=1&idx=2";
        List<string> listproxy = new List<string>();
        List<string> listTitle = new List<string>();
        string PostId = "b4c1014d33f84f05b8a2fc21ecf8203f";

        string Captcha = "03AA7ASh1z4I6BPQ78GoYq5IvRDKSYhhQ2tG8eq8x9glb30giZ8bv90SVJIUlqoY78RDGjzcZBtUaCjGRtIGtisptVZsio-5B5Vl7Uqn7mEEvf3zkkDDicyw-OgNOe1Fk_TJCPYvv5aB_lgYQ4U9T9iIsV89mlf0zlzBPPmu7oeg2zyUHJUvI65YZ0IYgzATsNqpeZLXnHCbiKV5VHggWvEvvuQgy-GxwDGJJ36sle39D09bd7D29e42Q0jW6pUMHOJRBtBK_Cv5tsO--qaNlVXDCGDukKnrQl_BaFbKEWgSzea5OMj1VZESeAoz9mUS1If5uvhiNjE3C9aQGW2aZ4AxcU_6Z4eKoQwTbz2pqFWEC74qSzsYLzqBw";
        Dictionary<string, string> ProfinderServices = new Dictionary<string, string>();
        SoftBucketHttpUtillity httphelper = new SoftBucketHttpUtillity();

        List<AccountManager> lstAm = new List<AccountManager>();

        //string Email = string.Empty;
        string LoginUsername = string.Empty;
        //string Username = string.Empty;
        //string Password = string.Empty;
        //string proxyip = string.Empty;
        //string proxyport = string.Empty;
        //string proxyusername = string.Empty;
        //string proxypassword = string.Empty;
        NameValueCollection nvc = new NameValueCollection();

        public Form1()
        {
            InitializeComponent();
        }

        public void AddToLogger(string log)
        {
            try
            {
                if (!this.IsHandleCreated) return;  // emergency exit

                this.Invoke(new MethodInvoker(delegate
                {
                    try
                    {
                        log = DateTime.Now.ToString() + "    " + log;
                        lstboxLogger.Items.Add(log);
                        lstboxLogger.SelectedIndex = lstboxLogger.Items.Count - 1;
                        //SoftBucketFileUtillity.AppendStringToTextfileNewLine(log, InstagramAutoBot.TwtHelper.ApplicationData.path_ResetDesktopFolder + "\\" + DateTime.Now.ToString("dd-MMM-yyyy") + "_ProcessLogger.txt");
                    }
                    catch (Exception ex)
                    {

                    }
                }));
            }
            catch (Exception ex)
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        private void StartProcess()
        {
            AddToLogger("Start process...");
            //NameValueCollection nvc;
            int i = 0;

            foreach (var item in listproxy)
            {
                try
                {
                    string Email = string.Empty;
                    string LoginUsername = string.Empty;
                    string Username = string.Empty;
                    string Password = string.Empty;
                    string proxyip = string.Empty;
                    string proxyport = string.Empty;
                    string proxyusername = string.Empty;
                    string proxypassword = string.Empty;

                    AccountManager Am = new AccountManager();

                    Email = item.Split(':')[0].Trim();
                    Username = item.Split(':')[0].Replace("@", "%40").Trim();
                    Password = item.Split(':')[1].Trim();
                    proxyip = item.Split(':')[2].Trim();
                    proxyport = item.Split(':')[3].Trim();
                    proxyusername = item.Split(':')[4].Trim();
                    proxypassword = item.Split(':')[5].Trim();

                    if (!string.IsNullOrEmpty(proxyip))
                    {
                        proxyip = item.Split(':')[2].Trim();
                    }
                    else
                    {
                        proxyip = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(proxyport))
                    {
                        proxyport = item.Split(':')[3].Trim();
                    }
                    else
                    {
                        proxyport = "80";
                    }
                    if (!string.IsNullOrEmpty(proxyusername))
                    {
                        proxyusername = item.Split(':')[4].Trim();
                    }
                    else
                    {
                        proxyusername = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(proxypassword))
                    {
                        proxypassword = item.Split(':')[5].Trim();
                    }
                    else
                    {
                        proxypassword = string.Empty;
                    }


                    Am.UserName = Username;
                    Am.Email = Email;
                    Am.Password = Password;
                    Am.Proxy = proxyip;
                    Am.ProxyPort = proxyport;
                    Am.ProxyUsername = proxyusername;
                    Am.ProxyPassword = proxypassword;

                    lstAm.Add(Am);
                }
                catch (Exception ex)
                {

                }
            }

            
            while (true)
            {
                List<string> tempImages = ApplicationData.lstImages;

                foreach (AccountManager item in lstAm)
                {
                    try
                    {


                        #region Latest Code For Ad Posting
                        string PostPlaceId = string.Empty;

                        try
                        {
                            ChildernData resultData = GetChildLocation(ApplicationData.PlaceId);
                            List<NameId> lstnd = GetNameId(resultData);

                            if (lstnd.Count > 0)
                            {
                                var rnd = new Random();
                                int rndCount = rnd.Next(0, lstnd.Count - 1);
                                PostPlaceId = lstnd[rndCount].Id;

                                if (lstnd.Count > 0)
                                {
                                    resultData = GetChildLocation(PostPlaceId);
                                    lstnd = GetNameId(resultData);
                                    rndCount = rnd.Next(0, lstnd.Count - 1);
                                    PostPlaceId = lstnd[rndCount].Id;

                                    if (lstnd.Count > 0)
                                    {
                                        resultData = GetChildLocation(PostPlaceId);
                                        lstnd = GetNameId(resultData);
                                        rndCount = rnd.Next(0, lstnd.Count - 1);
                                        PostPlaceId = lstnd[rndCount].Id;
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {


                        }

                        //if (!string.IsNullOrEmpty(txt_Place.Text.Trim()))
                        //{
                        //    PostPlaceId = txt_Place.Text.Trim();
                        //}
                        //if (!string.IsNullOrEmpty(txt_Place2.Text.Trim()))
                        //{
                        //    PostPlaceId = txt_Place2.Text.Trim();
                        //}
                        //if (!string.IsNullOrEmpty(txt_Place3.Text.Trim()))
                        //{
                        //    PostPlaceId = txt_Place3.Text.Trim();
                        //}

                        //toniefeichterv0372@hotmail.com:Asdf1234
                        //victoriamalin06@gmail.com:victoria786:173.232.97.194:8800

                        if (!ApplicationData.Isloggedin)
                        {
                            AddToLogger("Start Login process for " + item.Email);
                            nvc.Add("Upgrade-Insecure-Requests", "1");

                            string P1 = httphelper.GetPageSourceUsingProxy(new Uri("https://www.gumtree.com/"), item.Proxy, item.ProxyPort, item.ProxyUsername, item.ProxyPassword, "", nvc);
                            string P2 = httphelper.GetPageSourceUsingProxy(new Uri("https://my.gumtree.com/login"), item.Proxy, item.ProxyPort, item.ProxyUsername, item.ProxyPassword, "https://www.gumtree.com/", nvc);


                            nvc.Add("Origin", "https://my.gumtree.com");

                            string Postdata = "username=" + item.UserName + "&password=" + item.Password + "&client_id=&scope=&state=&redirect_uri=&response_type=&g-recaptcha-response=" + Captcha;
                            string P3 = httphelper.PostFormData(new Uri("https://my.gumtree.com/login"), Postdata, "https://my.gumtree.com/login", nvc);

                            string ResponseUsername = SoftBucketHttpUtillity.ParseJsonForUserId(P3, "Hello", "header-nav-mygumtree-icon").Replace("<span class", string.Empty).Replace("=\"", string.Empty);

                            if (P3.Contains("Hello"))
                            {
                                AddToLogger("User sucessfully loggedin : " + "Hello " + ResponseUsername);
                                LoginUsername = ResponseUsername;
                                ApplicationData.ResponseUser = ResponseUsername;
                                ApplicationData.Isloggedin = true;
                            }
                            else
                            {
                                AddToLogger("User not loggedin : " + item.UserName);
                            }
                        }
                        else
                        {

                        }

                        if (ApplicationData.Isloggedin)
                        {
                            AddToLogger("Start Ad Posting for " + item.UserName);
                            nvc = new NameValueCollection();
                            nvc.Add("Upgrade-Insecure-Requests", "1");

                            string GetPageSourceOfAd1 = httphelper.GetPageSourceUsingProxy(new Uri("https://my.gumtree.com/postad"), item.Proxy, item.ProxyPort, item.ProxyUsername, item.ProxyPassword, "https://my.gumtree.com/manage/ads", nvc);
                            string ResponseUriId = httphelper.gResponse.ResponseUri.ToString().Replace("postad", string.Empty).Replace("/", string.Empty).Replace("https:my.gumtree.com", string.Empty);
                            PostId = ResponseUriId;

                            nvc = new NameValueCollection();
                            nvc.Add("Origin", "https://my.gumtree.com");

                            string FinalPostData1 = "{\"formErrors\":{},\"categoryId\":null,\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":null,\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"" + item.Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + item.Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}}}";
                            string PostAddDraft = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData1, "https://my.gumtree.com/postad/" + PostId, nvc);


                            string GetCategoryPageSource = httphelper.GetPageSource(new Uri("https://my.gumtree.com/api/category/suggest?input=sofa"), "", "https://my.gumtree.com/postad/" + PostId, nvc);

                            nvc = new NameValueCollection();
                            nvc.Add("Origin", "https://my.gumtree.com");

                            string FinalPostData2 = "{\"formErrors\":{},\"categoryId\":\"\",\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"" + item.Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + item.Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\"}";
                            string PostAddDraft2 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData2, "https://my.gumtree.com/postad/" + PostId, nvc);

                            string FinalPostData3 = "{\"formErrors\":{},\"categoryId\":\"679\",\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"" + item.Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + item.Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
                            string PostAddDraft3 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData3, "https://my.gumtree.com/postad/" + PostId, nvc);

                            string FinalPostData4 = "{\"formErrors\":{\"global\":[]},\"categoryId\":\"4627\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + item.Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + item.Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"seller_type\":\"private\",\"price\":null},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
                            string PostAddDraft4 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData4, "https://my.gumtree.com/postad/" + PostId, nvc);

                            Random rnd = new Random();
                            listTitle = listTitle.OrderBy(itemTitle => rnd.Next()).ToList();
                            string TitleText = listTitle[0] +" " +"Code-" + rnd.Next(10000, 9999999);


                            string FinalPostData5 = "{\"formErrors\":{},\"categoryId\":\"679\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + item.Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + item.Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"seller_type\":\"private\",\"price\":\"\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
                            string PostAddDraft5 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData5, "https://my.gumtree.com/postad/" + PostId, nvc);

                            //string FinalPostData6 = "{\"formErrors\":{},\"categoryId\":\"\",\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"victoriamalin06@gmail.com\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"victoriamalin06@gmail.com\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\"}";
                            //string PostAddDraft6 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData1, "https://my.gumtree.com/postad/" + PostId, nvc);

                            string ImagePath = string.Empty;
                            //foreach (string image in ApplicationData.lstImages.Take(1))
                            //{

                            //    ImagePath = image;
                            //}

                            if (tempImages.Count == 0)
                            {
                                tempImages = ApplicationData.lstImages;
                            }

                            foreach (string image in tempImages.Take(1))
                            {
                                ImagePath = image;
                            }

                            string status = "";
                            string ImageUploadPageSource = httphelper.MultiPartImageUpload(PostId, "", "", "", "", ImagePath, "", "https://my.gumtree.com/postad/" + PostId, ref status);
                            ImageResponseSource result = JsonConvert.DeserializeObject<ImageResponseSource>(ImageUploadPageSource);

                            string FinalPostData7 = "{\"formErrors\":{},\"categoryId\":\"679\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"" + ApplicationData.Description + "\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + item.Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"" + result.id + "\",\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + item.Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"seller_type\":\"private\",\"price\":\"\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
                            string PostAddDraft7 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData7, "https://my.gumtree.com/postad/" + PostId, nvc);

                            string FinalPostData8 = "{\"formErrors\":{},\"categoryId\":\"679\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"" + ApplicationData.Description + "\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + item.Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"" + result.id + "\",\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + item.Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"seller_type\":\"private\",\"price\":\"" + ApplicationData.Price + "\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
                            string PostAddDraft8 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData8, "https://my.gumtree.com/postad/" + PostId, nvc);

                            string FinalPostData9 = "{\"formErrors\":{},\"categoryId\":\"679\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"" + ApplicationData.Description + "\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + item.Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"" + result.id + "\",\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + item.Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"seller_type\":\"private\",\"price\":\"" + ApplicationData.Price + "\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":true}";
                            string PostAddDraft9 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData9, "https://my.gumtree.com/postad/" + PostId, nvc);

                            nvc = new NameValueCollection();
                            nvc.Add("Upgrade-Insecure-Requests", "1");

                            string GetPageSourceBumpup10 = httphelper.GetPageSource(new Uri("https://my.gumtree.com/postad/" + PostId + "/bumpup"), "https://my.gumtree.com/postad/" + PostId, nvc);

                            try
                            {
                                if (httphelper.gResponse.ResponseUri.ToString().Contains("thankyou"))
                                {
                                    AddToLogger("Ad Created Successfully.");
                                    tempImages.Remove(ImagePath);

                                    int DelayTime = int.Parse(ApplicationData.SingleImageDelay.Trim());
                                    Thread.Sleep(DelayTime * 60 * 1000);
                                }
                                else
                                {
                                    if (httphelper.gResponse.ResponseUri.ToString().Contains("duplicate"))
                                    {
                                        AddToLogger("Duplicate Ad Found.");
                                    }
                                    else
                                    {
                                        AddToLogger("Ad not Created.");
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                AddToLogger("Ad Creation Failed.");
                            }
                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (string.IsNullOrEmpty(txtDelay.Text.Trim()))
                {
                    Thread.Sleep(24 *60* 60 * 1000);
                }
                else
                {
                    int DelayTime = int.Parse(ApplicationData.SingleImageDelay.Trim());
                    Thread.Sleep(DelayTime * 60 * 1000);
                }
            }

            

            //AddToLogger("Voting completed with given proxy.");
        }

        private void btn_grabplace_Click(object sender, EventArgs e)
        {
            try
            {
                //#region Latest Code For Ad Posting
                //string PostPlaceId = string.Empty;

                //if (!string.IsNullOrEmpty(txt_Place.Text.Trim()))
                //{
                //    PostPlaceId = txt_Place.Text.Trim();
                //}
                //if (!string.IsNullOrEmpty(txt_Place2.Text.Trim()))
                //{
                //    PostPlaceId = txt_Place2.Text.Trim();
                //}
                //if (!string.IsNullOrEmpty(txt_Place3.Text.Trim()))
                //{
                //    PostPlaceId = txt_Place3.Text.Trim();
                //}


                //if (!ApplicationData.Isloggedin)
                //{
                //    nvc.Add("Upgrade-Insecure-Requests", "1");

                //    string P1 = httphelper.GetPageSourceUsingProxy(new Uri("https://www.gumtree.com/"), proxyip, proxyport, proxyusername, proxypassword, "", nvc);
                //    string P2 = httphelper.GetPageSourceUsingProxy(new Uri("https://my.gumtree.com/login"), proxyip, proxyport, proxyusername, proxypassword, "https://www.gumtree.com/", nvc);


                //    nvc.Add("Origin", "https://my.gumtree.com");

                //    string Postdata = "username=" + Username + "&password=" + Password + "&client_id=&scope=&state=&redirect_uri=&response_type=&g-recaptcha-response=" + Captcha;
                //    string P3 = httphelper.PostFormData(new Uri("https://my.gumtree.com/login"), Postdata, "https://my.gumtree.com/login", nvc);

                //    string ResponseUsername = SoftBucketHttpUtillity.ParseJsonForUserId(P3, "Hello", "header-nav-mygumtree-icon").Replace("<span class", string.Empty).Replace("=\"", string.Empty);

                //    if (P3.Contains("Hello"))
                //    {
                //        AddToLogger("User sucessfully loggedin : " + "Hello " + ResponseUsername);
                //        LoginUsername = ResponseUsername;
                //        ApplicationData.ResponseUser = ResponseUsername;
                //        ApplicationData.Isloggedin = true;
                //    }
                //    else
                //    {
                //        AddToLogger("User not loggedin : " + Username);
                //    }
                //}
                //else
                //{

                //}

                //if (ApplicationData.Isloggedin)
                //{
                //    nvc = new NameValueCollection();
                //    nvc.Add("Upgrade-Insecure-Requests", "1");

                //    string GetPageSourceOfAd1 = httphelper.GetPageSourceUsingProxy(new Uri("https://my.gumtree.com/postad"), proxyip, proxyport, proxyusername, proxypassword, "https://my.gumtree.com/manage/ads", nvc);
                //    string ResponseUriId = httphelper.gResponse.ResponseUri.ToString().Replace("postad", string.Empty).Replace("/", string.Empty).Replace("https:my.gumtree.com", string.Empty);
                //    PostId = ResponseUriId;

                //    nvc = new NameValueCollection();
                //    nvc.Add("Origin", "https://my.gumtree.com");

                //    string FinalPostData1 = "{\"formErrors\":{},\"categoryId\":null,\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":null,\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}}}";
                //    string PostAddDraft = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData1, "https://my.gumtree.com/postad/" + PostId, nvc);


                //    string GetCategoryPageSource = httphelper.GetPageSource(new Uri("https://my.gumtree.com/api/category/suggest?input=sofa"), "", "https://my.gumtree.com/postad/" + PostId, nvc);

                //    nvc = new NameValueCollection();
                //    nvc.Add("Origin", "https://my.gumtree.com");

                //    string FinalPostData2 = "{\"formErrors\":{},\"categoryId\":\"\",\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\"}";
                //    string PostAddDraft2 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData2, "https://my.gumtree.com/postad/" + PostId, nvc);

                //    string FinalPostData3 = "{\"formErrors\":{},\"categoryId\":\"4627\",\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
                //    string PostAddDraft3 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData3, "https://my.gumtree.com/postad/" + PostId, nvc);

                //    string FinalPostData4 = "{\"formErrors\":{\"global\":[]},\"categoryId\":\"4627\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"price\":null},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
                //    string PostAddDraft4 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData4, "https://my.gumtree.com/postad/" + PostId, nvc);

                //    string FinalPostData5 = "{\"formErrors\":{},\"categoryId\":\"4627\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"price\":\"\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
                //    string PostAddDraft5 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData5, "https://my.gumtree.com/postad/" + PostId, nvc);

                //    //string FinalPostData6 = "{\"formErrors\":{},\"categoryId\":\"\",\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"victoriamalin06@gmail.com\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"victoriamalin06@gmail.com\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\"}";
                //    //string PostAddDraft6 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData1, "https://my.gumtree.com/postad/" + PostId, nvc);

                //    string ImagePath = string.Empty;
                //    foreach (string image in ApplicationData.lstImages.Take(1))
                //    {
                //        ImagePath = image;
                //    }

                //    string status = "";
                //    string ImageUploadPageSource = httphelper.MultiPartImageUpload(PostId, "", "", "", "", ImagePath, "", "https://my.gumtree.com/postad/" + PostId, ref status);
                //    ImageResponseSource result = JsonConvert.DeserializeObject<ImageResponseSource>(ImageUploadPageSource);

                //    string FinalPostData7 = "{\"formErrors\":{},\"categoryId\":\"4627\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"" + ApplicationData.Description + "\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"" + result.id + "\",\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"price\":\"\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
                //    string PostAddDraft7 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData7, "https://my.gumtree.com/postad/" + PostId, nvc);

                //    string FinalPostData8 = "{\"formErrors\":{},\"categoryId\":\"4627\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"" + ApplicationData.Description + "\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"" + result.id + "\",\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"price\":\"" + ApplicationData.Price + "\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
                //    string PostAddDraft8 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData8, "https://my.gumtree.com/postad/" + PostId, nvc);

                //    string FinalPostData9 = "{\"formErrors\":{},\"categoryId\":\"4627\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"" + ApplicationData.Description + "\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"" + result.id + "\",\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"price\":\"" + ApplicationData.Price + "\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":true}";
                //    string PostAddDraft9 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData9, "https://my.gumtree.com/postad/" + PostId, nvc);

                //    nvc = new NameValueCollection();
                //    nvc.Add("Upgrade-Insecure-Requests", "1");

                //    string GetPageSourceBumpup10 = httphelper.GetPageSource(new Uri("https://my.gumtree.com/postad/" + PostId + "/bumpup"), "https://my.gumtree.com/postad/" + PostId, nvc);

                //    try
                //    {
                //        if (httphelper.gResponse.ResponseUri.ToString().Contains("thankyou"))
                //        {
                //            AddToLogger("Ad Created Successfully.");
                //        }
                //        else
                //        {
                //            if (httphelper.gResponse.ResponseUri.ToString().Contains("duplicate"))
                //            {
                //                AddToLogger("Duplicate Ad Found.");
                //            }
                //            else
                //            {
                //                AddToLogger("Ad not Created.");
                //            }

                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        AddToLogger("Ad Creation Failed.");
                //    }
                //}
                //#endregion
            }
            catch (Exception ex)
            {


            } 
               
            


            //try
            //{
            //    double DT = DateTimeToUnixTimestamp(DateTime.Now);
            //    double ConvertDate = double.Parse(DT.ToString().Split('.')[0].Replace(".0", string.Empty));
            //    string EpouchTime = ConvertDate.ToString();
            //    EpouchTime = EpouchTime.Replace(".0", string.Empty);



            //    if (ApplicationData.Isloggedin)
            //    {
            //        ApplicationData.PlaceId1 = txt_Place.Text;// cmb_locationid.Text;
            //        ApplicationData.PlaceId2 = txt_Place2.Text;

            //        #region Old Flow
            //        //if (string.IsNullOrEmpty(ApplicationData.PlaceId2))
            //        //{
            //        //    #region Request 2

            //        //    //string R2 = httphelper.GetPageSourceUsingProxy(new Uri("https://my.gumtree.com/ajax/location/children?id=" + ApplicationData.PlaceId1 + "&_=" + EpouchTime), proxyip, proxyport, proxyusername, proxypassword, "https://my.gumtree.com/postad/" + PostId, nvc);

            //        //    //string[] arr2 = Regex.Split(R2, "id");
            //        //    //string L_Name2 = string.Empty;
            //        //    //string L_ID2 = string.Empty;

            //        //    //List<string> lst2 = new List<string>();
            //        //    //Dictionary<string, string> service_link2 = new Dictionary<string, string>();

            //        //    //foreach (string item1 in arr2)
            //        //    //{
            //        //    //    try
            //        //    //    {
            //        //    //        string item_data = SoftBucketHttpUtillity.ParseJsonForUserId(item1, "\"", "seoName");
            //        //    //        L_ID2 = SoftBucketHttpUtillity.ParseJsonForUserId(item_data, ":", "name").Replace("\"", string.Empty).Replace(",", string.Empty).Trim();
            //        //    //        L_Name2 = SoftBucketHttpUtillity.ParseJsonForUserId(item_data, "name", ",").Replace("\"", string.Empty).Replace(",", string.Empty).Replace(":", string.Empty).Trim();

            //        //    //        service_link2.Add(L_ID2, L_Name2);
            //        //    //    }
            //        //    //    catch (Exception ex)
            //        //    //    {

            //        //    //    }
            //        //    //}

            //        //    //cmb_Place2.Invoke(new MethodInvoker(() =>
            //        //    //{
            //        //    //    try
            //        //    //    {
            //        //    //        cmb_Place2.DataSource = new BindingSource(service_link2, null);
            //        //    //        cmb_Place2.DisplayMember = "Value";
            //        //    //        cmb_Place2.ValueMember = "Key";
            //        //    //    }
            //        //    //    catch (Exception ex)
            //        //    //    {

            //        //    //    }
            //        //    //}));
            //        //    #endregion
            //        //}
            //        //else
            //        //{
            //        //    #region Request 3

            //        //string R3 = httphelper.GetPageSourceUsingProxy(new Uri("https://my.gumtree.com/ajax/location/children?id=" + ApplicationData.PlaceId2 + "&_=" + EpouchTime), proxyip, proxyport, proxyusername, proxypassword, "https://my.gumtree.com/postad/" + PostId, nvc);

            //        //string[] arr3 = Regex.Split(R3, "id");
            //        //string L_Name3 = string.Empty;
            //        //string L_ID3 = string.Empty;

            //        //List<string> lst3 = new List<string>();
            //        //Dictionary<string, string> service_link2 = new Dictionary<string, string>();

            //        //foreach (string item1 in arr3)
            //        //{
            //        //    try
            //        //    {
            //        //        string item_data = SoftBucketHttpUtillity.ParseJsonForUserId(item1, "\"", "seoName");
            //        //        L_ID3 = SoftBucketHttpUtillity.ParseJsonForUserId(item_data, ":", "name").Replace("\"", string.Empty).Replace(",", string.Empty).Trim();
            //        //        L_Name3 = SoftBucketHttpUtillity.ParseJsonForUserId(item_data, "name", ",").Replace("\"", string.Empty).Replace(",", string.Empty).Replace(":", string.Empty).Trim();

            //        //        service_link2.Add(L_ID3, L_Name3);
            //        //    }
            //        //    catch (Exception ex)
            //        //    {

            //        //    }
            //        //}

            //        //cmb_Place3.Invoke(new MethodInvoker(() =>
            //        //{
            //        //    try
            //        //    {
            //        //        cmb_Place3.DataSource = new BindingSource(service_link2, null);
            //        //        cmb_Place3.DisplayMember = "Value";
            //        //        cmb_Place3.ValueMember = "Key";
            //        //    }
            //        //    catch (Exception ex)
            //        //    {

            //        //    }
            //        //}));
            //        #endregion


            //        //SoftBucketHttpUtillity.ParseJsonForUserId(txt_Place3.Text, "[", ",");

            //        if (ApplicationData.PlaceId1 != null)
            //        {
            //            if (string.IsNullOrEmpty(ApplicationData.PlaceId2))
            //            {
            //                #region Request 2

            //                string FinalPostData1 = "{\"formErrors\":{},\"categoryId\":null,\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":null,\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}}}";
            //                string PostAddDraft = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData1, "https://my.gumtree.com/postad/" + PostId, nvc);


            //                string GetCategoryPageSource = httphelper.GetPageSource(new Uri("https://my.gumtree.com/api/category/suggest?input=sofa"), "", "https://my.gumtree.com/postad/" + PostId, nvc);

            //                nvc = new NameValueCollection();
            //                nvc.Add("Origin", "https://my.gumtree.com");

            //                string FinalPostData2 = "{\"formErrors\":{},\"categoryId\":\"\",\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\"}";
            //                string PostAddDraft2 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData2, "https://my.gumtree.com/postad/" + PostId, nvc);

            //                string FinalPostData3 = "{\"formErrors\":{},\"categoryId\":\"4627\",\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
            //                string PostAddDraft3 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData3, "https://my.gumtree.com/postad/" + PostId, nvc);

            //                string R2 = httphelper.GetPageSourceUsingProxy(new Uri("https://my.gumtree.com/ajax/location/children?id=" + ApplicationData.PlaceId1 + "&_=" + EpouchTime), proxyip, proxyport, proxyusername, proxypassword, "https://my.gumtree.com/postad/" + PostId, nvc);

            //                string[] arr2 = Regex.Split(R2, "id");
            //                string L_Name2 = string.Empty;
            //                string L_ID2 = string.Empty;

            //                List<string> lst2 = new List<string>();
            //                Dictionary<string, string> service_link2 = new Dictionary<string, string>();

            //                foreach (string item1 in arr2)
            //                {
            //                    try
            //                    {
            //                        string item_data = SoftBucketHttpUtillity.ParseJsonForUserId(item1, "\"", "seoName");
            //                        L_ID2 = SoftBucketHttpUtillity.ParseJsonForUserId(item_data, ":", "name").Replace("\"", string.Empty).Replace(",", string.Empty).Trim();
            //                        L_Name2 = SoftBucketHttpUtillity.ParseJsonForUserId(item_data, "name", ",").Replace("\"", string.Empty).Replace(",", string.Empty).Replace(":", string.Empty).Trim();

            //                        service_link2.Add(L_ID2, L_Name2);
            //                    }
            //                    catch (Exception ex)
            //                    {

            //                    }
            //                }

            //                cmb_Place2.Invoke(new MethodInvoker(() =>
            //                {
            //                    try
            //                    {
            //                        cmb_Place2.DataSource = new BindingSource(service_link2, null);
            //                        cmb_Place2.DisplayMember = "Value";
            //                        cmb_Place2.ValueMember = "Key";
            //                    }
            //                    catch (Exception ex)
            //                    {

            //                    }
            //                }));
            //                #endregion
            //            }
            //            else
            //            {
            //                #region Request 3

            //                string R3 = httphelper.GetPageSourceUsingProxy(new Uri("https://my.gumtree.com/ajax/location/children?id=" + ApplicationData.PlaceId2 + "&_=" + EpouchTime), proxyip, proxyport, proxyusername, proxypassword, "https://my.gumtree.com/postad/" + PostId, nvc);

            //                string[] arr3 = Regex.Split(R3, "id");
            //                string L_Name3 = string.Empty;
            //                string L_ID3 = string.Empty;

            //                List<string> lst3 = new List<string>();
            //                Dictionary<string, string> service_link3 = new Dictionary<string, string>();

            //                foreach (string item1 in arr3)
            //                {
            //                    try
            //                    {
            //                        string item_data = SoftBucketHttpUtillity.ParseJsonForUserId(item1, "\"", "seoName");
            //                        L_ID3 = SoftBucketHttpUtillity.ParseJsonForUserId(item_data, ":", "name").Replace("\"", string.Empty).Replace(",", string.Empty).Trim();
            //                        L_Name3 = SoftBucketHttpUtillity.ParseJsonForUserId(item_data, "name", ",").Replace("\"", string.Empty).Replace(",", string.Empty).Replace(":", string.Empty).Trim();

            //                        service_link3.Add(L_ID3, L_Name3);
            //                    }
            //                    catch (Exception ex)
            //                    {

            //                    }
            //                }

            //                cmb_Place3.Invoke(new MethodInvoker(() =>
            //                {
            //                    try
            //                    {
            //                        cmb_Place3.DataSource = new BindingSource(service_link3, null);
            //                        cmb_Place3.DisplayMember = "Value";
            //                        cmb_Place3.ValueMember = "Key";
            //                    }
            //                    catch (Exception ex)
            //                    {

            //                    }
            //                }));


            //                string PostPlaceId = txt_Place3.Text;
            //                PostPlaceId = SoftBucketHttpUtillity.ParseJsonForUserId(PostPlaceId, "[", ",");

            //                if (!string.IsNullOrEmpty(PostPlaceId))
            //                {
            //                    string FinalPostData4 = "{\"formErrors\":{\"global\":[]},\"categoryId\":\"4627\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"price\":null},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
            //                    string PostAddDraft4 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData4, "https://my.gumtree.com/postad/" + PostId, nvc);

            //                    string FinalPostData5 = "{\"formErrors\":{},\"categoryId\":\"4627\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"price\":\"\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
            //                    string PostAddDraft5 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData5, "https://my.gumtree.com/postad/" + PostId, nvc);

            //                    //string FinalPostData6 = "{\"formErrors\":{},\"categoryId\":\"\",\"locationId\":null,\"postcode\":null,\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":null,\"description\":null,\"previousContactName\":null,\"contactName\":\"victoria\",\"previousContactEmail\":null,\"contactEmail\":\"victoriamalin06@gmail.com\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":true,\"usePhone\":false,\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":null,\"imageIds\":[],\"youtubeLink\":null,\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"victoriamalin06@gmail.com\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{},\"features\":{\"FEATURED\":{\"selected\":false,\"productName\":\"FEATURE_7_DAY\"}},\"removeDraft\":\"false\"}";
            //                    //string PostAddDraft6 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData1, "https://my.gumtree.com/postad/" + PostId, nvc);

            //                    string ImagePath = string.Empty;
            //                    foreach (string image in ApplicationData.lstImages.Take(1))
            //                    {
            //                        ImagePath = image;
            //                    }

            //                    string status = "";
            //                    string ImageUploadPageSource = httphelper.MultiPartImageUpload(PostId, "", "", "", "", ImagePath, "", "https://my.gumtree.com/postad/" + PostId, ref status);
            //                    ImageResponseSource result = JsonConvert.DeserializeObject<ImageResponseSource>(ImageUploadPageSource);

            //                    string FinalPostData7 = "{\"formErrors\":{},\"categoryId\":\"4627\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"" + ApplicationData.Description + "\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"" + result.id + "\",\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"price\":\"\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
            //                    string PostAddDraft7 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData7, "https://my.gumtree.com/postad/" + PostId, nvc);

            //                    string FinalPostData8 = "{\"formErrors\":{},\"categoryId\":\"4627\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"" + ApplicationData.Description + "\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"" + result.id + "\",\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"price\":\"" + ApplicationData.Price + "\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":false}";
            //                    string PostAddDraft8 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad-draft/" + PostId), FinalPostData8, "https://my.gumtree.com/postad/" + PostId, nvc);

            //                    string FinalPostData9 = "{\"formErrors\":{},\"categoryId\":\"4627\",\"locationId\":\"" + PostPlaceId + "\",\"postcode\":\"\",\"visibleOnMap\":true,\"area\":null,\"termsAgreed\":\"\",\"title\":\"" + TitleText + "\",\"description\":\"" + ApplicationData.Description + "\",\"previousContactName\":null,\"contactName\":\"" + LoginUsername + "\",\"previousContactEmail\":null,\"contactEmail\":\"" + Email + "\",\"contactTelephone\":\"\",\"contactUrl\":null,\"useEmail\":\"true\",\"usePhone\":\"false\",\"useUrl\":false,\"checkoutVariationId\":null,\"mainImageId\":\"0\",\"imageIds\":[\"" + result.id + "\",\"0\"],\"youtubeLink\":\"\",\"websiteUrl\":\"http://\",\"firstName\":null,\"lastName\":null,\"emailAddress\":\"" + Email + "\",\"telephoneNumber\":null,\"password\":null,\"optInMarketing\":true,\"attributes\":{\"price\":\"" + ApplicationData.Price + "\"},\"features\":{\"URGENT\":{\"productName\":\"URGENT\"},\"WEBSITE_URL\":{\"productName\":\"WEBSITE_URL\",\"selected\":\"false\"},\"FEATURED\":{\"productName\":\"FEATURE_7_DAY\"},\"SPOTLIGHT\":{\"productName\":\"HOMEPAGE_SPOTLIGHT\"}},\"removeDraft\":\"false\",\"submitForm\":true}";
            //                    string PostAddDraft9 = httphelper.PostFormData1(new Uri("https://my.gumtree.com/postad/" + PostId), FinalPostData9, "https://my.gumtree.com/postad/" + PostId, nvc);

            //                    nvc = new NameValueCollection();
            //                    nvc.Add("Upgrade-Insecure-Requests", "1");

            //                    string GetPageSourceBumpup10 = httphelper.GetPageSource(new Uri("https://my.gumtree.com/postad/" + PostId + "/bumpup"), "https://my.gumtree.com/postad/" + PostId, nvc);

            //                    try
            //                    {
            //                        if (httphelper.gResponse.ResponseUri.ToString().Contains("thankyou"))
            //                        {
            //                            AddToLogger("Ad Created Successfully.");
            //                        }
            //                        else
            //                        {
            //                            if (httphelper.gResponse.ResponseUri.ToString().Contains("duplicate"))
            //                            {
            //                                AddToLogger("Duplicate Ad Found.");
            //                            }
            //                            else
            //                            {
            //                                AddToLogger("Ad not Created.");
            //                            }

            //                        }
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        AddToLogger("Ad Creation Failed.");
            //                    }

            //                    //string GetPageSourceCheckout = httphelper.GetPageSource(new Uri("https://my.gumtree.com/checkout/" + PostId), "https://my.gumtree.com/postad/" + PostId, nvc);
            //                #endregion
            //                }
            //            }
            //            // string GetPageSourceCheckout = httphelper.GetPageSource(new Uri("https://my.gumtree.com/checkout/" + PostId), "https://my.gumtree.com/postad/" + PostId, nvc);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    AddToLogger(ex.Message);
            //}
        }

        private void cmb_locationid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tempselected = cmb_locationid.SelectedValue.ToString();
                txt_Place.Text = tempselected;

                try
                {
                    ChildernData resultData = GetChildLocation(tempselected);
                    List<NameId> lstnd = GetNameId(resultData);

                    cmb_Place2.Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            cmb_Place2.DataSource = new BindingSource(lstnd, null);
                            cmb_Place2.DisplayMember = "name";
                            cmb_Place2.ValueMember = "Id";
                        }
                        catch (Exception ex)
                        {

                        }
                    }));
                }
                catch (Exception ex)
                {


                }
            }
            catch (Exception ex)
            {

            }
            //}));
        }



        private string GetCaptcha(string proxy, ref SoftBucketHttpUtillity httphelper)
        {
            //WebClient web1 = new WebClient();
            string proxyip = proxy.Split(':')[0];
            string proxyport = proxy.Split(':')[1];
            string proxyüsername = proxy.Split(':')[2];
            string proxypassword = proxy.Split(':')[3];

            string ImageURL = "http://www.washwasha.org/regcode.aspx?wid=140&hei=40&rand=636457242312266207";

            byte[] ImagePageSource = httphelper.GetPageSourceImage(new Uri(ImageURL), proxyip, proxyport, proxyüsername, proxypassword, referer, "");

            //Image newImage = byteArrayToImage(ImagePageSource);
            //newImage.Save(@"C:\Users\RESILIENCE\Desktop\myimg.jpg");

            //string[] arr1 = new string[] { "prateek", "prateek", "" };
            string[] arr1 = new string[] { ApplicationData.Captcha_Username, ApplicationData.Captcha_Password, "" };
            DeathByCaptcha.Captcha CaptchaText = DecodeDBC_OBJ(arr1, ImagePageSource);
            return CaptchaText.Text;
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            //try
            //{
            MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
            ms.Write(byteArrayIn, 0, byteArrayIn.Length);
            Image returnImage = Image.FromStream(ms, true);//Exception occurs here
            //}
            //catch { }
            return returnImage;
        }

        static private DeathByCaptcha.Captcha DecodeDBC_OBJ(string[] args, byte[] imageBytes)
        {

            try
            {
                // Put your DBC username & password here:
                //Client client = (Client)new HttpClient(args[0], args[1]);
                DeathByCaptcha.Client client = (DeathByCaptcha.Client)new DeathByCaptcha.SocketClient(args[0], args[1]);
                client.Verbose = true;

                Console.WriteLine("Your balance is {0:F2} US cents", client.Balance);

                for (int i = 2, l = args.Length; i < l; i++)
                {
                    Console.WriteLine("Solving CAPTCHA {0}", args[i]);

                    // Upload a CAPTCHA and poll for its status.  Put the CAPTCHA image
                    // file name, file object, stream, or a vector of bytes, and desired
                    // solving timeout (in seconds) here:
                    DeathByCaptcha.Captcha captcha = client.Decode(imageBytes, 2 * DeathByCaptcha.Client.DefaultTimeout);
                    if (null != captcha)
                    {
                        Console.WriteLine("CAPTCHA {0:D} solved: {1}", captcha.Id, captcha.Text);

                        //// Report an incorrectly solved CAPTCHA.
                        //// Make sure the CAPTCHA was in fact incorrectly solved, do not
                        //// just report it at random, or you might be banned as abuser.
                        //if (client.Report(captcha))
                        //{
                        //    Console.WriteLine("Reported as incorrectly solved");
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Failed reporting as incorrectly solved");
                        //}

                        return captcha;
                    }
                    else
                    {
                        Console.WriteLine("CAPTCHA was not solved");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public static string ToUnixTimeMiliSeconds()
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long epochtime = (long)(DateTime.Now.ToUniversalTime() - epoch).TotalMilliseconds;
            return epochtime.ToString();
        }

        public static string ToUnixTimeSeconds()
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long epochtime = (long)(DateTime.Now.ToUniversalTime() - epoch).TotalSeconds;
            return epochtime.ToString();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            #region MyRegion

            try
            {

                //if (!string.IsNullOrEmpty(txt_username.Text.Trim()))
                //{
                //    ApplicationData.Captcha_Username = txt_username.Text.Trim();
                //}
                //else
                //{
                //    MessageBox.Show("Please Enter Username.");
                //    return;
                //}
                //if (!string.IsNullOrEmpty(txt_password.Text.Trim()))
                //{
                //    ApplicationData.Captcha_Password = txt_password.Text.Trim();
                //}
                //else
                //{
                //    MessageBox.Show("Please Enter Password.");
                //    return;
                //}


                //SingleImageDelay

               

                if (!string.IsNullOrEmpty(txt_Title.Text.Trim()))
                {
                    //TitleText = txt_Title.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Please Enter Title.");
                    return;
                }

                if (!string.IsNullOrEmpty(c.Text.Trim()))
                {
                    //Guid guid = new Guid();


                    //string val = GetChar(Guid.NewGuid().ToString());

                    ApplicationData.Description = c.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Please Enter Description.");
                    return;
                }

                if (!string.IsNullOrEmpty(txt_Price.Text.Trim()))
                {
                    ApplicationData.Price = txt_Price.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Please Enter Price.");
                    return;
                }

                if (rb_England.Checked)
                {
                    ApplicationData.PlaceId = "10000393";
                }
                else if (rb_scotland.Checked)
                {
                    ApplicationData.PlaceId = "10000395";
                }
                else if (rb_Wales.Checked)
                {
                    ApplicationData.PlaceId = "10000394";
                }
                else if (rb_NI.Checked)
                {
                    ApplicationData.PlaceId = "10000396";
                }
                else
                {
                    MessageBox.Show("Please Select Place.");
                    return;
                }

                if (!string.IsNullOrEmpty(txt_delaysingleimage.Text.Trim()))
                {
                    ApplicationData.SingleImageDelay = txt_delaysingleimage.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Please Enter Single Image Delay.");
                    return;
                }

                ApplicationData.SearchData = "home and garden/living room furniture/sofas, suites and arm chairs";//"For Sale / Home & Garden / Dining, Living Room Furniture / Chairs, Stools & Other Seating";

                Thread thread1 = new Thread(StartProcess);
                thread1.Start();
            }
            catch (Exception ex)
            {

            }

            #endregion
        }

        public string GetChar(string value)
        {
            //string input = "OneTwoThree";

            // Get first three characters.
            string sub = value.Substring(0, 8);
            //Console.WriteLine("Substring: {0}", sub);
            return sub;
        }

        private void browse_ip_Click(object sender, EventArgs e)
        {
            txtpath.Text = FileBrowseHelper.UploadTextFile(Application.StartupPath);

            listproxy = FileBrowseHelper.ReadFileLineByLine(txtpath.Text);

        }

        private void btn_AddImages_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();

                folderBrowserDlg.ShowNewFolderButton = true;

                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                string ImagePath = string.Empty;

                if (dlgResult.Equals(DialogResult.OK))
                {
                    ImagePath = folderBrowserDlg.SelectedPath;
                    txt_ImagePath.Text = ImagePath;
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }

                ApplicationData.lstImages = Directory.GetFiles(ImagePath).ToList();
                int ImageIndex = 0;

                AddToLogger(DateTime.Now.ToString() + "   " + "Total Images Added : " + ApplicationData.lstImages.Count);

            }
            catch (Exception ex)
            {

            }
        }

        private void cmb_Place2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tempselected = cmb_Place2.SelectedValue.ToString();
                txt_Place2.Text = tempselected;

                try
                {
                    ChildernData resultData = GetChildLocation(tempselected);
                    List<NameId> lstnd = GetNameId(resultData);

                    cmb_Place3.Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            cmb_Place3.DataSource = new BindingSource(lstnd, null);
                            cmb_Place3.DisplayMember = "name";
                            cmb_Place3.ValueMember = "Id";
                        }
                        catch (Exception ex)
                        {

                        }
                    }));
                }
                catch (Exception ex)
                {


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cmb_Place3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tempselected = cmb_Place3.SelectedValue.ToString();
                txt_Place3.Text = tempselected;

            }
            catch (Exception ex)
            {

            }
        }

        private void rb_England_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //ChildernData resultData = GetChildLocation("10000393");
                //List<NameId> lstnd = GetNameId(resultData);

                //cmb_locationid.Invoke(new MethodInvoker(() =>
                //{
                //    try
                //    {
                //        cmb_locationid.DataSource = new BindingSource(lstnd, null);
                //        cmb_locationid.DisplayMember = "name";
                //        cmb_locationid.ValueMember = "Id";
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                //}));
            }
            catch (Exception ex)
            {


            }
        }


        private ChildernData GetChildLocation(string LocationId)
        {
            try
            {
                #region Request 1

                SoftBucketHttpUtillity httphelper = new SoftBucketHttpUtillity();

                //Convert DateTime to Epouch
                double DT = DateTimeToUnixTimestamp(DateTime.Now);
                double ConvertDate = double.Parse(DT.ToString().Split('.')[0].Replace(".0", string.Empty));
                string EpouchTime = ConvertDate.ToString();
                EpouchTime = EpouchTime.Replace(".0", string.Empty);

                string R1 = httphelper.GetPageSource(new Uri("https://my.gumtree.com/ajax/location/children?id=" + LocationId + "&_=" + EpouchTime), "https://my.gumtree.com/postad/" + PostId, nvc);
                ChildernData result = JsonConvert.DeserializeObject<ChildernData>(R1);
                return result;

                #endregion
            }
            catch (Exception ex)
            {
                return new ChildernData();
            }
        }

        private List<NameId> GetNameId(ChildernData chld)
        {

            List<NameId> lstnd = new List<NameId>();
            try
            {
                foreach (var item in chld.childrenItems)
                {
                    try
                    {
                        NameId nd = new NameId();

                        nd.name = item.name;
                        nd.Id = item.id.ToString();

                        lstnd.Add(nd);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return lstnd;
        }

        private void rb_scotland_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //ChildernData resultData = GetChildLocation("10000395");
                //List<NameId> lstnd = GetNameId(resultData);

                //cmb_locationid.Invoke(new MethodInvoker(() =>
                //{
                //    try
                //    {
                //        cmb_locationid.DataSource = new BindingSource(lstnd, null);
                //        cmb_locationid.DisplayMember = "name";
                //        cmb_locationid.ValueMember = "Id";
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                //}));
            }
            catch (Exception ex)
            {


            }
        }

        private void rb_Wales_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //ChildernData resultData = GetChildLocation("10000394");
                //List<NameId> lstnd = GetNameId(resultData);

                //cmb_locationid.Invoke(new MethodInvoker(() =>
                //{
                //    try
                //    {
                //        cmb_locationid.DataSource = new BindingSource(lstnd, null);
                //        cmb_locationid.DisplayMember = "name";
                //        cmb_locationid.ValueMember = "Id";
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                //}));
            }
            catch (Exception ex)
            {


            }
        }

        private void rb_NI_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //ChildernData resultData = GetChildLocation("10000396");
                //List<NameId> lstnd = GetNameId(resultData);

                //cmb_locationid.Invoke(new MethodInvoker(() =>
                //{
                //    try
                //    {
                //        cmb_locationid.DataSource = new BindingSource(lstnd, null);
                //        cmb_locationid.DisplayMember = "name";
                //        cmb_locationid.ValueMember = "Id";
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                //}));
            }
            catch (Exception ex)
            {


            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            txt_Title.Text = FileBrowseHelper.UploadTextFile(Application.StartupPath);
            listTitle = FileBrowseHelper.ReadFileLineByLine(txt_Title.Text);
        }
    }

    public class NameId
    {
        public string name { get; set; }
        public string Id { get; set; }
    }
}
