using System;
using System.Web;
using System.IO;


public class modLogIn : IHttpModule
{

    public void Init(HttpApplication context)
    {
        context.PostAuthenticateRequest += new EventHandler(OnAuthenticate);

    }

    public void OnAuthenticate(Object source, EventArgs e)
    {
        HttpRequest req = HttpContext.Current.Request;
        HttpResponse res = HttpContext.Current.Response;

        FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("FrontEndUserActions\\ModuleActions\\LogIn.txt"), FileMode.Open);

        using (fs)
        {
            using (StreamWriter w = new StreamWriter(fs))
            {
                w.WriteLine("Browser " + req.Browser.Browser);
                w.WriteLine("IsAuthenticated " + req.IsAuthenticated.ToString());
                w.WriteLine("AnonymousID " + req.AnonymousID);
                w.WriteLine("IsSecure " + req.IsSecureConnection.ToString());

                w.WriteLine(DateTime.Now);

                w.Flush();
                w.Close();
            }

        }

    }

    public void Dispose() { }
}
