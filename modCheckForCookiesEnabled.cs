using System;
using System.Web;


    public class modCheckForCookiesEnabled : IHttpModule
    {
            

        public void Init(HttpApplication context)
        {
            context.LogRequest += new EventHandler(TestCookies);
            
        }

        private void TestCookies(object sender, EventArgs e)
        {

            HttpRequest req = HttpContext.Current.Request;
            HttpResponse res = HttpContext.Current.Response;

            if (req.Browser.Cookies)
            {
                if (req.Cookies["TestCookie"] == null)
                {
                    HttpCookie testCookie = new HttpCookie("TestCookie", "1");

                    res.Cookies.Add(testCookie);

                    res.Redirect(req.Url.ToString());
                }
                else
                {
                    
                    HttpCookie testCookie = req.Cookies["TestCookie"];

                    if (testCookie == null)
                    {
                        res.Redirect("CookiesDisabled.aspx");
                    }
                  
                }
            }
            else
            {
                res.Redirect("CookiesDisabled.aspx?Browser=" + req.Browser.Type.ToString());
            }


        }


    
        public void Dispose(){}
       
    }

