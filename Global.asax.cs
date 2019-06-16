using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace StoreCreator
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["UsersOnline"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["UsersOnline"] = (int)Application["UsersOnline"] + 1;
            Application.UnLock();

         
           
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpRequest req = Context.Request;
            HttpResponse res = Context.Response;

            int HttpStatusCode = res.StatusCode;
            string StatusResponse = res.StatusDescription;
            NameValueCollection coll = res.Headers;
            string ContentType = res.ContentType;


            try
            {
                using (FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("Logs\\HttpResponses.txt"), FileMode.Append))
                {
                    StreamWriter w = new StreamWriter(fs as Stream);
                    w.Write(HttpStatusCode.ToString());
                    w.Write(w.NewLine);
                    w.Write(StatusResponse);
                    w.Write(w.NewLine);
                    w.Write(ContentType);
                    w.Write(w.NewLine);

                    for (int i = 0; i < coll.Count; i++)
                    {
                        w.Write(coll[i]);
                    }

                    w.Write(w.NewLine);

                    w.Flush();
                    w.Dispose();
                }
            }
            catch (FileNotFoundException err)
            {
                throw new ApplicationException(err.Message);
            }
          
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
         
        }

        protected void Application_Error(object sender, EventArgs e)
        {

            Exception err = Server.GetLastError();
            Exceptions.LogExceptionToFileLog(err);
            Server.ClearError();
            Server.Transfer("ErrorPage.aspx");


        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["UsersOnline"] = (int)Application["UsersOnline"] - 1;
            Application.UnLock();
        }

        protected void Application_End(object sender, EventArgs e)
        {
           
        }
    }
}