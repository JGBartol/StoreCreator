using System;
using System.Web;
using System.IO;


public class modHttpResponseInfo : IHttpModule
{

    public void Init(HttpApplication context)
    {
        context.LogRequest += new EventHandler(LogRequest);
    }

    private void LogRequest(object sender, EventArgs e)
    {
        HttpRequest req = HttpContext.Current.Request;
        HttpResponse res = HttpContext.Current.Response;

        FileStream fs = null;

        try
        {
            using (fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("FrontEndUserActions\\ModuleActions\\HttpResponseInfoBinary.txt"), FileMode.Open))
            {
                using (BinaryWriter w = new BinaryWriter(fs))
                {
                    w.Write("ContentType: " + res.ContentType.ToString());
                    w.Write("Cookies: " + res.Cookies.ToString());
                    w.Write("Expires: " + res.Expires.ToString());
                    w.Write("ExpiresAbsolute: " + res.ExpiresAbsolute.ToShortDateString());
                    w.Write("Headers: " + res.Headers.ToString());
                    w.Write("Status: " + res.Status.ToString());
                    w.Write(":StatusCode " + res.StatusCode.ToString());
                    w.Write(":StatusDescription " + res.StatusDescription.ToString());                   
                }

            }


        }
        finally
        {
            if (fs != null)
            {
                fs.Close();
            }
        }
        





    }

    public void Dispose() { }

}

