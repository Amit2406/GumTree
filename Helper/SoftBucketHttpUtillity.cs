using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;


namespace GumTree.Helper
{
    public class SoftBucketHttpUtillity
    {

        public CookieCollection gCookies;
        public HttpWebRequest gRequest;
        public HttpWebResponse gResponse;

        string proxyAddress = string.Empty;
        int port = 80;
        string proxyUsername = string.Empty;
        string proxyPassword = string.Empty;

        public Uri GetResponseData()
        {
            return gResponse.ResponseUri;
        }

        public string GetPageSourceUsingProxy(Uri url, string proxyAddress, string strport, string proxyUsername, string proxyPassword, string referer, NameValueCollection nvc)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);

            #region HeaderSettings
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.167 Safari/537.36";
            gRequest.CookieContainer = new CookieContainer();
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.9";
            //gRequest.Headers["X-Requested-With"] = "XMLHttpRequest";
            gRequest.KeepAlive = true;
            //gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //gRequest.ContentType = @"application/json; charset=UTF-8";
            gRequest.Method = "GET";

            if (!string.IsNullOrEmpty(referer))
            {
                gRequest.Referer = referer;
            }

            if (nvc != null)
            {
                foreach (string item in nvc.Keys)
                {
                    if (item == "Origin")
                    {
                        gRequest.Headers["Origin"] = nvc[item];
                    }

                    else if (item == "Upgrade-Insecure-Requests")
                    {
                        gRequest.Headers["Upgrade-Insecure-Requests"] = "1";
                    }
                }
            } 
            #endregion



            ///Set Proxy
            this.proxyAddress = proxyAddress;
            //if (Globals.IdCheck.IsMatch(strport) && !string.IsNullOrEmpty(strport))
            //{
            this.port = int.Parse(strport);
            //}
            this.proxyUsername = proxyUsername;
            this.proxyPassword = proxyPassword;

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            gRequest.Method = "GET";
            //gRequest.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";

            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);

                //try
                //{
                //    gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0-2078004405-1321685323158", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1321697619.1321858563.3", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                //}
                //catch (Exception ex)
                //{

                //}
            }
            //Get Response for this request url

            setExpect100Continue();
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            ((sender, certificate, chain, sslPolicyErrors) => true);

            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (WebException wex)
            {
                StreamReader reader = new StreamReader(wex.Response.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                return responseString;
            }

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                return responseString;
            }
            else
            {
                return "Error";
            }

        }

        public string PostFormData(Uri formActionUrl, string postData, string Referes, NameValueCollection nameValueCollection)
        {

            gRequest = (HttpWebRequest)WebRequest.Create(formActionUrl);

            #region HeaderSettings
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.167 Safari/537.36";
            gRequest.CookieContainer = new CookieContainer();
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.9";
            //gRequest.Headers["X-Requested-With"] = "XMLHttpRequest";
            gRequest.KeepAlive = true;
            gRequest.AllowAutoRedirect = true;
            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            gRequest.ContentType = @"application/x-www-form-urlencoded";
            gRequest.Method = "POST";
            gRequest.Host = "my.gumtree.com";

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }

            if (nameValueCollection != null)
            {
                foreach (string item in nameValueCollection.Keys)
                {
                    if (item == "Origin")
                    {
                        gRequest.Headers["Origin"] = nameValueCollection[item];
                    }

                    else if (item == "Upgrade-Insecure-Requests")
                    {
                        gRequest.Headers["Upgrade-Insecure-Requests"] = "1";
                    }
                }
            }
            #endregion

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            #region CookieManagement
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);
            }

            //logic to postdata to the form
            try
            {
                //System.Net.ServicePointManager.ServerCertificateValidationCallback =
                //((sender, certificate, chain, sslPolicyErrors) => true);
                setExpect100Continue();
                string postdata = string.Format(postData);
                byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
                gRequest.ContentLength = postBuffer.Length;
                Stream postDataStream = gRequest.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Logger.LogText("Internet Connectivity Exception : "+ ex.Message,null);
            }
            //post data logic ends

            //Get Response for this request url
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (WebException wex)
            {
                Console.WriteLine(wex);

                string responseString1 = string.Empty;

                using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream()))
                {
                    responseString1 = reader.ReadToEnd();
                }

                return responseString1;

                //return ex.Message;
                //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
            }



            //check if the status code is http 200 or http ok

            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);
                //check if response object has any cookies or not
                //Added by sandeep pathak
                //gCookiesContainer = gRequest.CookieContainer;  

                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion



                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                //Console.Write("Response String:" + responseString);
                return responseString;
            }
            else
            {
                return "Error in posting data";
            }
        }

        public string PostFormData1(Uri formActionUrl, string postData, string Referes, NameValueCollection nameValueCollection)
        {

            gRequest = (HttpWebRequest)WebRequest.Create(formActionUrl);

            #region HeaderSettings
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.167 Safari/537.36";
            gRequest.CookieContainer = new CookieContainer();
            gRequest.Accept = "application/json, text/javascript, */*; q=0.01";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.9";
            gRequest.Headers["X-Requested-With"] = "XMLHttpRequest";
            gRequest.KeepAlive = true;
            gRequest.AllowAutoRedirect = true;
            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            gRequest.ContentType = @"application/json; charset=UTF-8";
            gRequest.Method = "POST";
            gRequest.Host = "my.gumtree.com";

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }

            if (nameValueCollection != null)
            {
                foreach (string item in nameValueCollection.Keys)
                {
                    if (item == "Origin")
                    {
                        gRequest.Headers["Origin"] = nameValueCollection[item];
                    }

                    else if (item == "Upgrade-Insecure-Requests")
                    {
                        gRequest.Headers["Upgrade-Insecure-Requests"] = "1";
                    }
                }
            }
            #endregion

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            #region CookieManagement
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);
            }

            //logic to postdata to the form
            try
            {
                //System.Net.ServicePointManager.ServerCertificateValidationCallback =
                //((sender, certificate, chain, sslPolicyErrors) => true);
                setExpect100Continue();
                //string postdata = string.Format(postData);
                byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
                gRequest.ContentLength = postBuffer.Length;
                Stream postDataStream = gRequest.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Logger.LogText("Internet Connectivity Exception : "+ ex.Message,null);
            }
            //post data logic ends

            //Get Response for this request url
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (WebException wex)
            {
                Console.WriteLine(wex);

                string responseString1 = string.Empty;

                using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream()))
                {
                    responseString1 = reader.ReadToEnd();
                }

                return responseString1;

                //return ex.Message;
                //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
            }



            //check if the status code is http 200 or http ok

            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);
                //check if response object has any cookies or not
                //Added by sandeep pathak
                //gCookiesContainer = gRequest.CookieContainer;  

                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion



                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                //Console.Write("Response String:" + responseString);
                return responseString;
            }
            else
            {
                return "Error in posting data";
            }
        }

        public string PostFormData2(Uri formActionUrl, string postData, string Referes, NameValueCollection nameValueCollection)
        {

            gRequest = (HttpWebRequest)WebRequest.Create(formActionUrl);

            #region HeaderSettings
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.167 Safari/537.36";
            gRequest.CookieContainer = new CookieContainer();
            gRequest.Accept = "*/*";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.9";
            gRequest.Headers["X-Requested-With"] = "XMLHttpRequest";
            gRequest.KeepAlive = true;
            gRequest.AllowAutoRedirect = true;
            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //gRequest.ContentType = @"application/json; charset=UTF-8";
            gRequest.Method = "POST";
            gRequest.Host = "my.gumtree.com";

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }

            if (nameValueCollection != null)
            {
                foreach (string item in nameValueCollection.Keys)
                {
                    if (item == "Origin")
                    {
                        gRequest.Headers["Origin"] = nameValueCollection[item];
                    }

                    else if (item == "Upgrade-Insecure-Requests")
                    {
                        gRequest.Headers["Upgrade-Insecure-Requests"] = "1";
                    }
                }
            }
            #endregion

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            #region CookieManagement
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);
            }

            //logic to postdata to the form
            try
            {
                //System.Net.ServicePointManager.ServerCertificateValidationCallback =
                //((sender, certificate, chain, sslPolicyErrors) => true);
                setExpect100Continue();
                //string postdata = string.Format(postData);
                byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
                gRequest.ContentLength = postBuffer.Length;
                Stream postDataStream = gRequest.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Logger.LogText("Internet Connectivity Exception : "+ ex.Message,null);
            }
            //post data logic ends

            //Get Response for this request url
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (WebException wex)
            {
                Console.WriteLine(wex);

                string responseString1 = string.Empty;

                using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream()))
                {
                    responseString1 = reader.ReadToEnd();
                }

                return responseString1;

                //return ex.Message;
                //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
            }



            //check if the status code is http 200 or http ok

            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);
                //check if response object has any cookies or not
                //Added by sandeep pathak
                //gCookiesContainer = gRequest.CookieContainer;  

                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion



                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                //Console.Write("Response String:" + responseString);
                return responseString;
            }
            else
            {
                return "Error in posting data";
            }
        }

        public string GetPageSource(Uri url, string Referes, string Token, NameValueCollection nameValueCollection)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);

            #region HeaderSettings
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.167 Safari/537.36";
            gRequest.CookieContainer = new CookieContainer();
            gRequest.Method = "GET";
            gRequest.Accept = "*/*";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.9";
            gRequest.Headers["X-Requested-With"] = "XMLHttpRequest";
            gRequest.KeepAlive = true;
            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //gRequest.ContentType = @"application/json; charset=UTF-8";


            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }

            if (nameValueCollection != null)
            {
                foreach (string item in nameValueCollection.Keys)
                {
                    if (item == "Origin")
                    {
                        gRequest.Headers["Origin"] = nameValueCollection[item];
                    }

                    else if (item == "Upgrade-Insecure-Requests")
                    {
                        gRequest.Headers["Upgrade-Insecure-Requests"] = "1";
                    }
                }
            }
            #endregion

            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:54.0) Gecko/20100101 Firefox/54.0";//"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.5";


            //gRequest.Connection = "keep-alive";
            //gRequest.Headers["Cache-Control"] = "max-age=0";

            gRequest.KeepAlive = true;
            
            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            gRequest.Method = "GET";
            //gRequest.AllowAutoRedirect = false;
            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }
            if (!string.IsNullOrEmpty(Token))
            {
                gRequest.Headers.Add("X-CSRFToken", Token);
                gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            }
            
            

            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);

                try
                {
                    //gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0 - 2078004405 - 1321685323158", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1322116799.1322206824.9", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                }
                catch (Exception ex)
                {

                }
            }
            //Get Response for this request url

            setExpect100Continue();
            gResponse = (HttpWebResponse)gRequest.GetResponse();

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                return responseString;
            }
            else
            {
                return "Error";
            }

        }

        public string newGetPageSource(Uri url, string Referes, NameValueCollection nameValueCollection)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);

            #region HeaderSettings
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.167 Safari/537.36";
            gRequest.CookieContainer = new CookieContainer();
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.9";
            //gRequest.Headers["X-Requested-With"] = "XMLHttpRequest";
            gRequest.KeepAlive = true;
            //gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //gRequest.ContentType = @"application/json; charset=UTF-8";
            gRequest.Method = "Get";

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }

            if (nameValueCollection != null)
            {
                foreach (string item in nameValueCollection.Keys)
                {
                    if (item == "Origin")
                    {
                        gRequest.Headers["Origin"] = nameValueCollection[item];
                    }

                    else if (item == "Upgrade-Insecure-Requests")
                    {
                        gRequest.Headers["Upgrade-Insecure-Requests"] = "1";
                    }
                }
            }
            #endregion

           
            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);

            }
            //Get Response for this request url

            setExpect100Continue();
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            ((sender, certificate, chain, sslPolicyErrors) => true);

            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (WebException wex)
            {
                Console.WriteLine(wex);

                string responseString1 = string.Empty;

                using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream()))
                {
                    responseString1 = reader.ReadToEnd();
                }

                return responseString1;
            }

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                return responseString;
            }
            else
            {
                return "Error";
            }
        }










        //public string GetPageSource(Uri url, string Referes, NameValueCollection nameValueCollection)
        //{
        //    setExpect100Continue();
        //    gRequest = (HttpWebRequest)WebRequest.Create(url);

        //    gRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
        //    gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
        //    gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
        //    //gRequest.Headers["Cache-Control"] = "max-age=0";
        //    gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";
        //    //gRequest.Connection = "keep-alive";

        //    gRequest.KeepAlive = true;

        //    gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        //    gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

        //    gRequest.Method = "GET";
        //    //gRequest.AllowAutoRedirect = false;
        //    ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

        //    if (!string.IsNullOrEmpty(Referes))
        //    {
        //        gRequest.Referer = Referes;
        //    }

        //    if (nameValueCollection != null)
        //    {
        //        foreach (string item in nameValueCollection.Keys)
        //        {
        //            gRequest.Headers.Add(item, nameValueCollection[item]);
        //        }
        //    }

        //    //if (!string.IsNullOrEmpty(Referes))
        //    //{
        //    //    gRequest.Referer = Referes;
        //    //}
        //    //if (!string.IsNullOrEmpty(Token))
        //    //{
        //    //    //gRequest.Headers.Add("X-CSRFToken", Token);
        //    //    //gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
        //    //    gRequest.Headers.Add("Origin", Token);
        //    //    // gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
        //    //}



        //    #region CookieManagment

        //    if (this.gCookies != null && this.gCookies.Count > 0)
        //    {
        //        setExpect100Continue();
        //        gRequest.CookieContainer.Add(gCookies);

        //        try
        //        {
        //            //gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0 - 2078004405 - 1321685323158", "/"));
        //            //gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1322116799.1322206824.9", "/"));
        //            //gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
        //            //gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
        //            //gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //    //Get Response for this request url

        //    setExpect100Continue();
        //    gResponse = (HttpWebResponse)gRequest.GetResponse();

        //    //check if the status code is http 200 or http ok
        //    if (gResponse.StatusCode == HttpStatusCode.OK)
        //    {
        //        //get all the cookies from the current request and add them to the response object cookies
        //        setExpect100Continue();
        //        gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


        //        //check if response object has any cookies or not
        //        if (gResponse.Cookies.Count > 0)
        //        {
        //            //check if this is the first request/response, if this is the response of first request gCookies
        //            //will be null
        //            if (this.gCookies == null)
        //            {
        //                gCookies = gResponse.Cookies;
        //            }
        //            else
        //            {
        //                foreach (Cookie oRespCookie in gResponse.Cookies)
        //                {
        //                    bool bMatch = false;
        //                    foreach (Cookie oReqCookie in this.gCookies)
        //                    {
        //                        if (oReqCookie.Name == oRespCookie.Name)
        //                        {
        //                            oReqCookie.Value = oRespCookie.Value;
        //                            bMatch = true;
        //                            break; // 
        //                        }
        //                    }
        //                    if (!bMatch)
        //                        this.gCookies.Add(oRespCookie);
        //                }
        //            }
        //        }
        //    #endregion

        //        StreamReader reader = new StreamReader(gResponse.GetResponseStream());
        //        string responseString = reader.ReadToEnd();
        //        reader.Close();
        //        return responseString;
        //    }
        //    else
        //    {
        //        return "Error";
        //    }

        //}

        //public string newGetPageSource(Uri url, string Referes, NameValueCollection nameValueCollection)
        //{
        //    setExpect100Continue();
        //    gRequest = (HttpWebRequest)WebRequest.Create(url);

        //    #region HeaderSettings
        //    gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.167 Safari/537.36";
        //    gRequest.CookieContainer = new CookieContainer();
        //    gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
        //    gRequest.Headers["Accept-Language"] = "en-US,en;q=0.9";
        //    //gRequest.Headers["X-Requested-With"] = "XMLHttpRequest";
        //    gRequest.KeepAlive = true;
        //    //gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        //    //gRequest.ContentType = @"application/json; charset=UTF-8";
        //    gRequest.Method = "Get";

        //    if (!string.IsNullOrEmpty(Referes))
        //    {
        //        gRequest.Referer = Referes;
        //    }

        //    if (nameValueCollection != null)
        //    {
        //        foreach (string item in nameValueCollection.Keys)
        //        {
        //            if (item == "Origin")
        //            {
        //                gRequest.Headers["Origin"] = nameValueCollection[item];
        //            }

        //            else if (item == "Upgrade-Insecure-Requests")
        //            {
        //                gRequest.Headers["Upgrade-Insecure-Requests"] = "1";
        //            }
        //        }
        //    }
        //    #endregion

        //    //if (!string.IsNullOrEmpty(Referes))
        //    //{
        //    //    gRequest.Referer = Referes;
        //    //}
        //    //if (!string.IsNullOrEmpty(Token))
        //    //{
        //    //    //gRequest.Headers.Add("X-CSRFToken", Token);
        //    //    //gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
        //    //    gRequest.Headers.Add("Origin", Token);
        //    //    // gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
        //    //}



        //    #region CookieManagment

        //    if (this.gCookies != null && this.gCookies.Count > 0)
        //    {
        //        setExpect100Continue();
        //        gRequest.CookieContainer.Add(gCookies);

        //        //foreach (Cookie oReqCookie in this.gCookies)
        //        //{
        //        //if (oReqCookie.Name == "ds_user_id")
        //        //{
        //        //    gRequest.CookieContainer.Add(new Cookie("ds_user_id", oReqCookie.Value) { Domain = "www.instagram.com" });
        //        //    //  gRequest.CookieContainer.Add(new Cookie("ig_pr", "1") { Domain = "www.instagram.com" });
        //        //    //  gRequest.CookieContainer.Add(new Cookie("ig_vw", "1360") { Domain = "www.instagram.com" });
        //        //    //  break;
        //        //}

        //        //if (oReqCookie.Name == "mid")
        //        //{
        //        //    gRequest.CookieContainer.Add(new Cookie("mid", oReqCookie.Value) { Domain = "www.instagram.com" });
        //        //    //break;
        //        //}

        //        //if (oReqCookie.Name == "csrftoken")
        //        //{
        //        //    gRequest.CookieContainer.Add(new Cookie("csrftoken", oReqCookie.Value) { Domain = "www.instagram.com" });
        //        //    //break;
        //        //}
        //        //}

        //        try
        //        {
        //            //gRequest.CookieContainer.Add(url, new Cookie("__atuvc", "7%7C45", "/"));
        //            //gRequest.CookieContainer.Add(url, new Cookie("__atuvs", "5a029a6133922f9c006", "/"));
        //            //gRequest.CookieContainer.Add(url, new Cookie("_ga", "GA1.2.179047183.1510120038", "/"));
        //            //gRequest.CookieContainer.Add(url, new Cookie("_gid", "GA1.2.1263374909.1510120038", "/"));
        //            //string sessionid = Guid.NewGuid().ToString();
        //            //sessionid = sessionid.Substring(0, sessionid.LastIndexOf('-')).Replace("-",string.Empty);
        //            //gRequest.CookieContainer.Add(url, new Cookie("ASP.NET_SessionId", "sp3i022hdk0zeb12z2btnqex", "/"));
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //    //Get Response for this request url

        //    setExpect100Continue();
        //    System.Net.ServicePointManager.ServerCertificateValidationCallback =
        //    ((sender, certificate, chain, sslPolicyErrors) => true);

        //    try
        //    {
        //        gResponse = (HttpWebResponse)gRequest.GetResponse();
        //    }
        //    catch (WebException wex)
        //    {
        //        Console.WriteLine(wex);

        //        string responseString1 = string.Empty;

        //        using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream()))
        //        {
        //            responseString1 = reader.ReadToEnd();
        //        }

        //        return responseString1;
        //    }

        //    //check if the status code is http 200 or http ok
        //    if (gResponse.StatusCode == HttpStatusCode.OK)
        //    {
        //        //get all the cookies from the current request and add them to the response object cookies
        //        setExpect100Continue();
        //        gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


        //        //check if response object has any cookies or not
        //        if (gResponse.Cookies.Count > 0)
        //        {
        //            //check if this is the first request/response, if this is the response of first request gCookies
        //            //will be null
        //            if (this.gCookies == null)
        //            {
        //                gCookies = gResponse.Cookies;
        //            }
        //            else
        //            {
        //                foreach (Cookie oRespCookie in gResponse.Cookies)
        //                {
        //                    bool bMatch = false;
        //                    foreach (Cookie oReqCookie in this.gCookies)
        //                    {
        //                        if (oReqCookie.Name == oRespCookie.Name)
        //                        {
        //                            oReqCookie.Value = oRespCookie.Value;
        //                            bMatch = true;
        //                            break; // 
        //                        }
        //                    }
        //                    if (!bMatch)
        //                        this.gCookies.Add(oRespCookie);
        //                }
        //            }
        //        }
        //    #endregion

        //        StreamReader reader = new StreamReader(gResponse.GetResponseStream());
        //        string responseString = reader.ReadToEnd();
        //        reader.Close();
        //        return responseString;
        //    }
        //    else
        //    {
        //        return "Error";
        //    }
        //}

        public string GetPageSource1(Uri url, string Referes, NameValueCollection nameValueCollection)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:56.0) Gecko/20100101 Firefox/56.0";//"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            //gRequest.Headers["Cache-Control"] = "max-age=0";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.5";
            gRequest.AllowAutoRedirect = true;
            gRequest.KeepAlive = true;
            
            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            gRequest.Method = "GET";
            //gRequest.AllowAutoRedirect = false;
            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }

            if (nameValueCollection != null)
            {
                foreach (string item in nameValueCollection.Keys)
                {
                    if (item == "Host")
                    {
                        gRequest.Host = nameValueCollection[item];
                    }
                    else if (item == "Upgrade-Insecure-Requests")
                    {
                        gRequest.Headers.Add(item, nameValueCollection[item]);
                    }
                }
            }

            //if (!string.IsNullOrEmpty(Referes))
            //{
            //    gRequest.Referer = Referes;
            //}
            //if (!string.IsNullOrEmpty(Token))
            //{
            //    //gRequest.Headers.Add("X-CSRFToken", Token);
            //    //gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //    gRequest.Headers.Add("Origin", Token);
            //    // gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //}



            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);
                
                //foreach (Cookie oReqCookie in this.gCookies)
                //{
                    //if (oReqCookie.Name == "ds_user_id")
                    //{
                    //    gRequest.CookieContainer.Add(new Cookie("ds_user_id", oReqCookie.Value) { Domain = "www.instagram.com" });
                    //    //  gRequest.CookieContainer.Add(new Cookie("ig_pr", "1") { Domain = "www.instagram.com" });
                    //    //  gRequest.CookieContainer.Add(new Cookie("ig_vw", "1360") { Domain = "www.instagram.com" });
                    //    //  break;
                    //}

                    //if (oReqCookie.Name == "mid")
                    //{
                    //    gRequest.CookieContainer.Add(new Cookie("mid", oReqCookie.Value) { Domain = "www.instagram.com" });
                    //    //break;
                    //}

                    //if (oReqCookie.Name == "csrftoken")
                    //{
                    //    gRequest.CookieContainer.Add(new Cookie("csrftoken", oReqCookie.Value) { Domain = "www.instagram.com" });
                    //    //break;
                    //}
                //}

                try
                {
                    //gRequest.CookieContainer.Add(url, new Cookie("__atuvc", "7%7C45", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__atuvs", "5a029a6133922f9c006", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("_ga", "GA1.2.179047183.1510120038", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("_gid", "GA1.2.1263374909.1510120038", "/"));
                    //string sessionid = Guid.NewGuid().ToString();
                    //sessionid = sessionid.Substring(0, sessionid.LastIndexOf('-')).Replace("-",string.Empty);
                    //gRequest.CookieContainer.Add(url, new Cookie("ASP.NET_SessionId", "sp3i022hdk0zeb12z2btnqex", "/"));
                }
                catch (Exception ex)
                {

                }
            }
            //Get Response for this request url

            setExpect100Continue();
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            ((sender, certificate, chain, sslPolicyErrors) => true);

            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (WebException wex)
            {
                Console.WriteLine(wex);

                string responseString1 = string.Empty;

                using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream()))
                {
                    responseString1 = reader.ReadToEnd();
                }

                return responseString1;
            }

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                return responseString;
            }
            else
            {
                return "Error";
            }
        }

        public string GetPageSource(Uri url, string Referes, NameValueCollection nameValueCollection)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:56.0) Gecko/20100101 Firefox/56.0";//"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            //gRequest.Headers["Cache-Control"] = "max-age=0";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.5";
            gRequest.AllowAutoRedirect = true;
            gRequest.KeepAlive = true;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            gRequest.Method = "GET";
            //gRequest.AllowAutoRedirect = false;
            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }

            if (nameValueCollection != null)
            {
                foreach (string item in nameValueCollection.Keys)
                {
                    if (item == "Host")
                    {
                        gRequest.Host = nameValueCollection[item];
                    }
                    else if (item == "Upgrade-Insecure-Requests")
                    {
                        gRequest.Headers.Add(item, nameValueCollection[item]);
                    }
                }
            }

            //if (!string.IsNullOrEmpty(Referes))
            //{
            //    gRequest.Referer = Referes;
            //}
            //if (!string.IsNullOrEmpty(Token))
            //{
            //    //gRequest.Headers.Add("X-CSRFToken", Token);
            //    //gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //    gRequest.Headers.Add("Origin", Token);
            //    // gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //}



            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);

                //foreach (Cookie oReqCookie in this.gCookies)
                //{
                //if (oReqCookie.Name == "ds_user_id")
                //{
                //    gRequest.CookieContainer.Add(new Cookie("ds_user_id", oReqCookie.Value) { Domain = "www.instagram.com" });
                //    //  gRequest.CookieContainer.Add(new Cookie("ig_pr", "1") { Domain = "www.instagram.com" });
                //    //  gRequest.CookieContainer.Add(new Cookie("ig_vw", "1360") { Domain = "www.instagram.com" });
                //    //  break;
                //}

                //if (oReqCookie.Name == "mid")
                //{
                //    gRequest.CookieContainer.Add(new Cookie("mid", oReqCookie.Value) { Domain = "www.instagram.com" });
                //    //break;
                //}

                //if (oReqCookie.Name == "csrftoken")
                //{
                //    gRequest.CookieContainer.Add(new Cookie("csrftoken", oReqCookie.Value) { Domain = "www.instagram.com" });
                //    //break;
                //}
                //}

                try
                {
                    //gRequest.CookieContainer.Add(url, new Cookie("__atuvc", "7%7C45", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__atuvs", "5a029a6133922f9c006", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("_ga", "GA1.2.179047183.1510120038", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("_gid", "GA1.2.1263374909.1510120038", "/"));
                    //string sessionid = Guid.NewGuid().ToString();
                    //sessionid = sessionid.Substring(0, sessionid.LastIndexOf('-')).Replace("-",string.Empty);
                    //gRequest.CookieContainer.Add(url, new Cookie("ASP.NET_SessionId", "sp3i022hdk0zeb12z2btnqex", "/"));
                }
                catch (Exception ex)
                {

                }
            }
            //Get Response for this request url

            setExpect100Continue();
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            ((sender, certificate, chain, sslPolicyErrors) => true);

            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (WebException wex)
            {
                Console.WriteLine(wex);

                string responseString1 = string.Empty;

                using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream()))
                {
                    responseString1 = reader.ReadToEnd();
                }

                return responseString1;
            }

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                return responseString;
            }
            else
            {
                return "Error";
            }
        }

        public string GetPageSourceForView(Uri url, string Referes, NameValueCollection nameValueCollection)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);

            gRequest.UserAgent = "Instagram 7.17.1 (iPhone5,2; iPhone OS 9_2_1; en_IN; en-IN; scale=2.00;640x1136) AppleWebKit/420+";
            //gRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
            //gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            //gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";

            //gRequest.Connection = "keep-alive";
            //gRequest.Headers["Cache-Control"] = "max-age=0";
            #region New
            //gRequest.Host = "instagram.fbom1-1.fna.fbcdn.net";
            gRequest.Headers["X-IG-Capabilities"] = "nw==";
            //gRequest.Headers["Range"] = "bytes=0-262143";
            //gRequest.Headers["Accept"] = "/";
            gRequest.Headers["Accept-Language"] = "en-IN;q=1";
            gRequest.ContentType = "video/mp4";
            #endregion
            gRequest.KeepAlive = true;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            gRequest.Method = "GET";
            //gRequest.AllowAutoRedirect = false;
            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }

            if (nameValueCollection != null)
            {
                foreach (string item in nameValueCollection.Keys)
                {
                    if (item != "Host" && item != "ig_pr" && item != "ig_vw")
                    {
                        gRequest.Headers.Add(item, nameValueCollection[item]);
                    }
                    else if (item == "ig_pr" || item == "ig_vw")
                    {
                        gRequest.CookieContainer.Add(new Cookie(item, nameValueCollection[item]) { Domain = "www.instagram.com" });
                    }
                    else
                    {
                        gRequest.Host = nameValueCollection[item];
                    }
                }
            }

            //if (!string.IsNullOrEmpty(Referes))
            //{
            //    gRequest.Referer = Referes;
            //}
            //if (!string.IsNullOrEmpty(Token))
            //{
            //    //gRequest.Headers.Add("X-CSRFToken", Token);
            //    //gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //    gRequest.Headers.Add("Origin", Token);
            //    // gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //}



            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);

                foreach (Cookie oReqCookie in this.gCookies)
                {
                    if (oReqCookie.Name == "ds_user_id")
                    {
                        gRequest.CookieContainer.Add(new Cookie("ds_user_id", oReqCookie.Value) { Domain = "www.instagram.com" });
                        //  gRequest.CookieContainer.Add(new Cookie("ig_pr", "1") { Domain = "www.instagram.com" });
                        //  gRequest.CookieContainer.Add(new Cookie("ig_vw", "1360") { Domain = "www.instagram.com" });
                        //  break;
                    }

                    if (oReqCookie.Name == "mid")
                    {
                        gRequest.CookieContainer.Add(new Cookie("mid", oReqCookie.Value) { Domain = "www.instagram.com" });
                        //break;
                    }

                    if (oReqCookie.Name == "csrftoken")
                    {
                        gRequest.CookieContainer.Add(new Cookie("csrftoken", oReqCookie.Value) { Domain = "www.instagram.com" });
                        //break;
                    }
                }

                try
                {
                    //gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0 - 2078004405 - 1321685323158", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1322116799.1322206824.9", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                }
                catch (Exception ex)
                {

                }
            }
            //Get Response for this request url

            setExpect100Continue();
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            ((sender, certificate, chain, sslPolicyErrors) => true);

            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (WebException wex)
            {
                Console.WriteLine(wex);

                string responseString1 = string.Empty;

                using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream()))
                {
                    responseString1 = reader.ReadToEnd();
                }

                return responseString1;
            }

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                return responseString;
            }
            else
            {
                return "Error";
            }
        }

        public string GetPageSourceForImage(Uri url,string proxyAddress, string strport, string proxyUsername, string proxyPassword, string Referes, string Token)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:52.0) Gecko/20100101 Firefox/52.0"; //"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.5";

            //gRequest.Connection = "keep-alive";
            //gRequest.Headers["Cache-Control"] = "max-age=0";

            gRequest.KeepAlive = true;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            gRequest.Method = "GET";
            //gRequest.AllowAutoRedirect = false;
            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }
            if (!string.IsNullOrEmpty(Token))
            {
                gRequest.Headers.Add("X-CSRFToken", Token);
                gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            }



            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);

                try
                {
                    //gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0 - 2078004405 - 1321685323158", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1322116799.1322206824.9", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                }
                catch (Exception ex)
                {

                }
            }
            //Get Response for this request url

            setExpect100Continue();
            gResponse = (HttpWebResponse)gRequest.GetResponse();

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                string responseString = string.Empty;

                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();
                }

                return responseString;
            }
            else
            {
                return "Error";
            }

        }

        public byte[] GetPageSourceImage(Uri url, string proxyAddress, string strport, string proxyUsername, string proxyPassword, string Referes, string Token)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:52.0) Gecko/20100101 Firefox/52.0"; //"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
            gRequest.Accept = "*/*";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.5";
            gRequest.Host = "www.washwasha.org";
            //gRequest.Connection = "keep-alive";
            //gRequest.Headers["Cache-Control"] = "max-age=0";

            gRequest.KeepAlive = true;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            gRequest.Method = "GET";
            //gRequest.AllowAutoRedirect = false;
            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }
            if (!string.IsNullOrEmpty(Token))
            {
                gRequest.Headers.Add("X-CSRFToken", Token);
                gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            }



            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);

                try
                {
                    //gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0 - 2078004405 - 1321685323158", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1322116799.1322206824.9", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                }
                catch (Exception ex)
                {

                }
            }
            //Get Response for this request url

            setExpect100Continue();
            gResponse = (HttpWebResponse)gRequest.GetResponse();

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion
                Byte[] lnByte = new Byte[1 * 1024 * 1024 * 10];
                using (BinaryReader reader = new BinaryReader(gResponse.GetResponseStream()))
                {
                    lnByte = reader.ReadBytes(1 * 1024 * 1024 * 10);
                    using (FileStream lxFS = new FileStream("34891.jpg", FileMode.Create))
                    {
                        lxFS.Write(lnByte, 0, lnByte.Length);
                    }
                }

                return lnByte;
                //string responseString = string.Empty;

                //using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                //{
                //    responseString = reader.ReadToEnd();
                //}

                //return responseString;
            }
            else
            {
                return null;
            }

        }

        public string OneTimeGetPageSource(Uri url, string Referes, NameValueCollection nameValueCollection)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:52.0) Gecko/20100101 Firefox/52.0"; //"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            //gRequest.Headers["Cache-Control"] = "max-age=0";
            gRequest.Headers["Accept-Language"] = "en-US,en;q=0.5";
            //gRequest.Connection = "keep-alive";

            gRequest.KeepAlive = true;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            gRequest.Method = "GET";
            //gRequest.AllowAutoRedirect = false;
            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }

            if (nameValueCollection != null)
            {
                foreach (string item in nameValueCollection.Keys)
                {
                    if (item != "Host")
                    {
                        gRequest.Headers.Add(item, nameValueCollection[item]);
                    }
                    else
                    {
                        gRequest.Host = nameValueCollection[item];
                    }
                }
            }

            //if (!string.IsNullOrEmpty(Referes))
            //{
            //    gRequest.Referer = Referes;
            //}
            //if (!string.IsNullOrEmpty(Token))
            //{
            //    //gRequest.Headers.Add("X-CSRFToken", Token);
            //    //gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //    gRequest.Headers.Add("Origin", Token);
            //    // gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //}



            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);

                try
                {
                    //gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0 - 2078004405 - 1321685323158", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1322116799.1322206824.9", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                    //gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                }
                catch (Exception ex)
                {

                }
            }
            //Get Response for this request url

            setExpect100Continue();
            //gResponse = (HttpWebResponse)gRequest.GetResponse();
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (WebException wex)
            {
                Console.WriteLine(wex);

                string responseString1 = string.Empty;

                using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream()))
                {
                    responseString1 = reader.ReadToEnd();
                }

                return responseString1;

                //return ex.Message;
                //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
            }

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                //gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                //if (gResponse.Cookies.Count > 0)
                //{
                //    //check if this is the first request/response, if this is the response of first request gCookies
                //    //will be null
                //    if (this.gCookies == null)
                //    {
                //        gCookies = gResponse.Cookies;
                //    }
                //    else
                //    {
                //        foreach (Cookie oRespCookie in gResponse.Cookies)
                //        {
                //            bool bMatch = false;
                //            foreach (Cookie oReqCookie in this.gCookies)
                //            {
                //                if (oReqCookie.Name == oRespCookie.Name)
                //                {
                //                    oReqCookie.Value = oRespCookie.Value;
                //                    bMatch = true;
                //                    break; // 
                //                }
                //            }
                //            if (!bMatch)
                //                this.gCookies.Add(oRespCookie);
                //        }
                //    }
                //}
            #endregion

                string responseString = string.Empty;

                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();
                }

                return responseString;
            }
            else
            {
                return "Error";
            }

        }

        

        public string MultiPartImageUpload(string postid,string profileUsername, string profileLocation, string profileURL, string profileDescription, string localImagePath, string authenticity_token,string refrer, ref string status)
        {
            NameValueCollection nvc = new NameValueCollection();

            nvc.Add("position", "0");
            //nvc.Add("image" + ";  filename="+"\"blob", "");
            //nvc.Add("Filename", "blob");

            //nvc.Add("authenticity_token", authenticity_token);
            //nvc.Add("media_file_name", "");
            //nvc.Add("media_data[]", "");
            //nvc.Add("media[]_Filename", "");
            //nvc.Add("media_file_name-Filename", "");
            //nvc.Add("media_data_empty", "");
            //nvc.Add("media_empty_Filename", "");
            //nvc.Add("user[profile_header_image_name]", "");
            //nvc.Add("user[profile_header_image]", "");
            //nvc.Add("user[profile_header_image]_Filename", "");
            //nvc.Add("user[name]", profileUsername);
            //nvc.Add("user[location]", profileLocation);
            //nvc.Add("user[url]", profileURL);
            //nvc.Add("user[description]", profileDescription);

            //HttpUploadFile("https://my.gumtree.com/postad/" + postid + "/images", localImagePath, "profile_image[uploaded_data]", "image/jpeg", nvc, false, refrer, ref status);

           string source = UploadFileUsingToWeb("https://my.gumtree.com/postad/" + postid + "/images", localImagePath, "blob", "image/jpeg", nvc, true);
           return source;
        }

        public string UploadFileUsingToWeb(string url, string file, string paramName, string contentType, NameValueCollection nvc, bool IsLocalFile)
        {
            try
            {
                ////log.Debug(string.Format("Uploading {0} to {1}", file, url));
                //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString();
                string boundary = "--" + "0xKhTmLbOuNdArY";
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                gRequest = (HttpWebRequest)WebRequest.Create(url);
                gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36"; ;
                gRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                gRequest.Method = "POST";
                gRequest.ProtocolVersion = HttpVersion.Version11;
                gRequest.KeepAlive = true;
                gRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                gRequest.Accept = "*/*";
                gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";
                gRequest.Host = "my.gumtree.com";
                gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                //ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

                ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

                gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

                #region CookieManagment

                if (this.gCookies != null && this.gCookies.Count > 0)
                {
                    gRequest.CookieContainer.Add(gCookies);
                }
                #endregion

                Stream rs = gRequest.GetRequestStream();

                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (string key in nvc.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
                rs.Write(boundarybytes, 0, boundarybytes.Length);



                if (IsLocalFile)
                {
                    //string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                    //string header = string.Format(headerTemplate, paramName, file, contentType);

                    string headerTemplate = "Content-Disposition: form-data; name=\"image\"; filename=\"blob\"\r\nContent-Type: {1}\r\n\r\n";
                    string header = string.Format(headerTemplate, paramName, contentType);

                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);


                    rs.Write(headerbytes, 0, headerbytes.Length);

                    FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                    //BinaryReader br = new BinaryReader(fileStream);
                    //byte[] buffer = br.ReadBytes((int)fileStream.Length);

                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        rs.Write(buffer, 0, bytesRead);
                    }
                    fileStream.Close();
                }

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
                rs.Close();


                WebResponse wresp = null;
                try
                {
                    //wresp = gRequest.GetResponse();
                    gResponse = (HttpWebResponse)gRequest.GetResponse();
                    StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                    string responseString = reader.ReadToEnd();
                    //reader.Close();
                    return responseString;

                    //wresp = gRequest.GetResponse();
                    //Stream stream2 = wresp.GetResponseStream();
                    //StreamReader reader2 = new StreamReader(stream2);
                    //log.Debug(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
                    //return true;
                    //return null;
                }
                catch (WebException ex)
                {
                    StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
                    string responseString = reader.ReadToEnd();
                    //reader.Close();
                    return responseString;
                }
                //finally
                //{
                //    gRequest = null;
                //}
                //}
            }
            catch (Exception ex)
            {

                return "Error";
            }
            

        }

        public string HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc, bool IsLocalFile, string refrer, ref string status)
        {
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            ////log.Debug(string.Format("Uploading {0} to {1}", file, url));
            //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString();
            string boundary = "----WebKitFormBoundary93iQNkOkLO12l9uh";//"---------------------------" + DateTime.Now.Ticks.ToString();
            //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString();
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            gRequest = (HttpWebRequest)WebRequest.Create(url);//"https://twitter.com/settings/profile");

            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36";
            gRequest.Accept = "/";
            //gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";
            gRequest.Host = "my.gumtree.com";

            gRequest.KeepAlive = true;



            //gRequest.AllowAutoRedirect = false;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            string tempBoundary = boundary + "\r\n";
            byte[] tempBoundarybytes = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

            //gRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            gRequest.ContentType = "multipart/form-data; boundary=" + tempBoundary;
            gRequest.Method = "POST";
            gRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;


            if (!string.IsNullOrEmpty(refrer))
            {
                gRequest.Referer = refrer;
            }

            //ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                gRequest.CookieContainer.Add(gCookies);

                //gRequest.CookieContainer.Add(new Cookie("__utma", "43838368.370306257.1336542481.1336542481.1336542481.1", "/", "twitter.com"));
                //gRequest.CookieContainer.Add(new Cookie("__utmb", "43838368.25.10.1336542481", "/", "twitter.com"));
                //gRequest.CookieContainer.Add(new Cookie("__utmc", "43838368", "/", "twitter.com"));
                //gRequest.CookieContainer.Add(new Cookie("__utmz", "43838368.1336542481.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/", "twitter.com"));
            }

            //#region Profile Photo Upload
            //Image img = new Image();
            //img.ImageUrl = file;
            //double h = img.Height.Value;

            //string[] pathArr = file.Split('\\');
            ////string[] fileArr = pathArr.Last().Split('.');
            ////string fileName = fileArr.Last().ToString();
            //string bytes = Convert.ToBase64String(File.ReadAllBytes(file));
            //string imageInByte = bytes.Replace("/", "%");
            //string m = "";
            //string PhotoUploadData = "authenticity_token=" + nvc.Keys[0] + "&fileData=" + imageInByte + "&fileName=" + pathArr[5] + "&height=240&offsetLeft=0&offsetTop=0&page_context=settings&scribeContext%5Bcomponent%5D=profile_image_upload&scribeElement=upload&section_context=profile&uploadType=avatar&width=240";
            ////postFormData("https://twitter.com/settings/profile/profile_image_update",
            //#endregion

            #endregion

            Stream rs = gRequest.GetRequestStream();


            int temp = 0;

            foreach (string key in nvc.Keys)
            {
                string HeaderTemplate = string.Empty;
                string TempKey = key;
                if (key.Contains("filename"))
                {
                    HeaderTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"" + file + "\"\r\nContent-Type: {2}\r\n\r\n";
                    HeaderTemplate = string.Format(HeaderTemplate, "profile_image[uploaded_data]", file, contentType);
                    //TempKey = TempKey.Replace("_Filename", string.Empty);
                }
                else
                {
                    if (!key.Contains("-Filename"))
                    {
                        HeaderTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                        HeaderTemplate = string.Format(HeaderTemplate, key, nvc[key]);
                    }
                    else
                    {
                        //TempKey = TempKey.Replace("-Filename", string.Empty);
                        HeaderTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                        HeaderTemplate = string.Format(HeaderTemplate, TempKey, nvc[key]);
                    }
                }

                if (temp == 0)
                {
                    rs.Write(tempBoundarybytes, 0, tempBoundarybytes.Length);
                    temp++;
                }
                else
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                }

                string formitem = string.Format(HeaderTemplate, TempKey, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
                //if (!key.Contains("user[description]") )
                //{
                //    rs.Write(formitembytes, 0, formitembytes.Length);  
                //}
            }

            //rs.Write(boundarybytes, 0, boundarybytes.Length);



            //if (IsLocalFile)
            //{
            //    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\""+ file +"\"\r\nContent-Type: {2}\r\n\r\n";
            //    string header = string.Format(headerTemplate, "profile_image[uploaded_data]", file, contentType);
            //    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            //    rs.Write(headerbytes, 0, headerbytes.Length);

            //    FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            //    byte[] buffer = new byte[4096];
            //    int bytesRead = 0;
            //    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            //    {
            //        rs.Write(buffer, 0, bytesRead);
            //    }
            //    fileStream.Close();
            //}

            //-----------------------------
            //byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            //rs.Write(trailer, 0, trailer.Length);
            //rs.Close();
            //-----------------------------


            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                gRequest.CookieContainer.Add(gCookies);
            }

            #endregion

            WebResponse wresp = null;
            try
            {
                //wresp = gRequest.GetResponse();
                gResponse = (HttpWebResponse)gRequest.GetResponse();
                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                status = "okay";
                return responseString;
            }
            catch (Exception ex)
            {
                //log.Error("Error uploading file", ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
                return null;
            }
            finally
            {
                gRequest = null;
            }
            //}

        }
        
        //public string PostFormData(Uri formActionUrl, string postData, string Referes, NameValueCollection nameValueCollection)
        //{

        //    gRequest = (HttpWebRequest)WebRequest.Create(formActionUrl);
        //    gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:39.0) Gecko/20100101 Firefox/39.0";//"User-Agent: Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.16) Gecko/20110319 Firefox/3.6.16";

        //    gRequest.CookieContainer = new CookieContainer();// gCookiesContainer;
        //    gRequest.Method = "POST";
        //    gRequest.Accept = "*/*";// " text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8, */*";
        //    gRequest.Headers["Accept-Language"] = "en-US,en;q=0.5";
        //    gRequest.KeepAlive = true;
        //    gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        //    gRequest.ContentType = @"application/x-www-form-urlencoded";

        //    if (!string.IsNullOrEmpty(Referes))
        //    {
        //        gRequest.Referer = Referes;
        //    }

        //    if (nameValueCollection != null)
        //    {
        //        foreach (string item in nameValueCollection.Keys)
        //        {
        //            gRequest.Headers.Add(item, nameValueCollection[item]);
        //        }
        //    }
        //    ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

        //    #region CookieManagement
        //    if (this.gCookies != null && this.gCookies.Count > 0)
        //    {
        //        setExpect100Continue();
        //        gRequest.CookieContainer.Add(gCookies);
        //    }

        //    //logic to postdata to the form
        //    try
        //    {
        //        setExpect100Continue();
        //        string postdata = string.Format(postData);
        //        byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
        //        gRequest.ContentLength = postBuffer.Length;
        //        Stream postDataStream = gRequest.GetRequestStream();
        //        postDataStream.Write(postBuffer, 0, postBuffer.Length);
        //        postDataStream.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        // Logger.LogText("Internet Connectivity Exception : "+ ex.Message,null);
        //    }
        //    //post data logic ends

        //    //Get Response for this request url
        //    try
        //    {
        //        gResponse = (HttpWebResponse)gRequest.GetResponse();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //        Console.WriteLine(ex);
        //        //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
        //    }



        //    //check if the status code is http 200 or http ok

        //    if (gResponse.StatusCode == HttpStatusCode.OK)
        //    {
        //        //get all the cookies from the current request and add them to the response object cookies
        //        setExpect100Continue();
        //        gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);
        //        //check if response object has any cookies or not
        //        //Added by sandeep pathak
        //        //gCookiesContainer = gRequest.CookieContainer;  

        //        if (gResponse.Cookies.Count > 0)
        //        {
        //            //check if this is the first request/response, if this is the response of first request gCookies
        //            //will be null
        //            if (this.gCookies == null)
        //            {
        //                gCookies = gResponse.Cookies;
        //            }
        //            else
        //            {
        //                foreach (Cookie oRespCookie in gResponse.Cookies)
        //                {
        //                    bool bMatch = false;
        //                    foreach (Cookie oReqCookie in this.gCookies)
        //                    {
        //                        if (oReqCookie.Name == oRespCookie.Name)
        //                        {
        //                            oReqCookie.Value = oRespCookie.Value;
        //                            bMatch = true;
        //                            break; // 
        //                        }
        //                    }
        //                    if (!bMatch)
        //                        this.gCookies.Add(oRespCookie);
        //                }
        //            }
        //        }
        //    #endregion



        //        StreamReader reader = new StreamReader(gResponse.GetResponseStream());
        //        string responseString = reader.ReadToEnd();
        //        reader.Close();
        //        //Console.Write("Response String:" + responseString);
        //        return responseString;
        //    }
        //    else
        //    {
        //        return "Error in posting data";
        //    }

        //}

       

        

        public void setExpect100Continue()
        {
            if (ServicePointManager.Expect100Continue==true)
            {
                ServicePointManager.Expect100Continue = false; 
            }
        }

        public void ChangeProxy(string proxyAddress, int port, string proxyUsername, string proxyPassword)
        {
            try
            {
                WebProxy myproxy = new WebProxy(proxyAddress, port);
                myproxy.BypassProxyOnLocal = false;

                if (!string.IsNullOrEmpty(proxyUsername) && !string.IsNullOrEmpty(proxyPassword))
                {
                    myproxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                }
                gRequest.Proxy = myproxy;
            }
            catch (Exception ex)
            {
              
            }
        }

        public static string ParseUserJson(string data, string paramName)
        {
            int startIndx = data.IndexOf(paramName) + paramName.Length + 3;
            int endIndx = data.IndexOf("\"", startIndx);

            string value = data.Substring(startIndx, endIndx - startIndx).Replace(",", "");
            return value;
        }

        public static string ParseUserJson(string data, string paramName,string lastName)
        {
            int startIndx = data.IndexOf(paramName);
            int endIndx = data.IndexOf(lastName, startIndx);

            string value = data.Substring(startIndx, endIndx - startIndx);
            return value;
        }

        public static string ParseJsonForUserId(string data, string firsparamName, string secondparamName)
        {
            int startIndx = data.IndexOf(firsparamName) + firsparamName.Length;
            int endIndx = data.IndexOf(secondparamName, startIndx + 1);

            string value = data.Substring(startIndx, endIndx - startIndx).Trim();
            return value;
        }

        public static string ParseUrlJson(string data, string paramName)
        {
            int startIndx = data.IndexOf(paramName) + paramName.Length;
            int endIndx = data.IndexOf('"', startIndx + 1);

            string value = data.Substring(startIndx, endIndx - startIndx).Replace(",", "").Replace("\"", "").Trim();
            return value;
        }

        
    }
}
