using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

public class modHttpInformation : IHttpModule
{

    public void Init(HttpApplication context)
    {
        context.EndRequest += new EventHandler(LogReq);
    }


    private void LogReq(object sender, EventArgs e)
    {

        HttpRequest req = HttpContext.Current.Request;
        HttpResponse res = HttpContext.Current.Response;

        FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("FrontEndUserActions\\ModuleActions\\HttpRequestInfo.txt"), FileMode.Open);

        try
        {

            using (fs)
            {
                using (StreamWriter wr = new StreamWriter(fs))
                {
                    wr.WriteLine("Url: " + req.Url.ToString());
                    wr.WriteLine("UserAgent: " + req.UserAgent.ToString());
                    wr.WriteLine("UserHostAddress: " + req.UserHostAddress.ToString());
                    wr.WriteLine("RequestType: " + req.RequestType.ToString());
                    wr.WriteLine("QueryString: " + req.QueryString.ToString());
                    wr.WriteLine("Headers :" + req.Headers.ToString());

                    wr.WriteLine(DateTime.Now);
                    wr.WriteLine(" ");

                    wr.Flush();
                    wr.Close();

                }
            }
        }
        finally
        {
            fs.Close();
        }

        



    

    }
    



    public void Dispose() { }





}
