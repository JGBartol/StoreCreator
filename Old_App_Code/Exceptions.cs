using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Net.Mail;


public class Exceptions
{
    public abstract class BaseException
    {
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string TargetSite { get; set; }
        public string InnerException { get; set; }
        public Type ExceptionType { get; set; }
    }

    public class BasicException : BaseException { }

    public class ExceptionSql : BaseException
    {
        public string ClassName { get; set; }
        public int LineNumber { get; set; }
        public string Procedure { get; set; }

    }

    public static void LogException(Exception error)
    {
        try
        {
            if (error is SqlException)
            {
                LogBasicExceptionToDB(error, false);

            }
            else
            {
                LogBasicExceptionToDB(error, true);
            }                     
        }
        catch(SqlException err)
        {
            LogExceptionToFileLog(err);
        }            
    }

    public static bool LogExceptionToFileLog(Exception err)
    {
        FileStream fs = new FileStream("C:\\Users\\HP ENVY\\Documents\\Visual Studio 2012\\Projects\\StoreCreator\\StoreCreator\\ExceptionLogs\\ExceptionLog.txt", FileMode.Open);
        bool IsLogged = false;
       
        try
        {

            using (fs)
            {
                StreamWriter sw = new StreamWriter(fs);

                StreamWriter w = new StreamWriter(fs);
                w.WriteLine(DateTime.Now);
                w.WriteLine(err.Message);
                w.WriteLine(err.Source);
                w.WriteLine(err.StackTrace);
                w.WriteLine(err.TargetSite);
                w.WriteLine(err.InnerException);
                w.WriteLine(err.GetType().ToString());

                if (err is SqlException && err.GetType() == typeof(SqlException))
                {
                    w.WriteLine(((SqlException)err).Class.ToString());
                    w.WriteLine(((SqlException)err).LineNumber.ToString());
                    w.WriteLine(((SqlException)err).Procedure.ToString());
                }

                w.Flush();
                w.Close();

                IsLogged = true;
            }
        }
        finally
        {
            fs.Close();
        }

        return IsLogged;

    }

    private static string strSqlConnection
    {
        get { return ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString; }
    }

    public static bool LogBasicExceptionToDB(object objError, bool IsBasic)
    {
        bool IsSuccessful = false;

        SqlConnection con = new SqlConnection(strSqlConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;



        if (IsBasic)
        {

            cmd.CommandText = "spInsertBasicException";

            BasicException err = (BasicException)objError;

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strMessage", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.Message });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strSource", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.Source });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strStackTrace", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.StackTrace });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strTargetSite", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.TargetSite });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strInnerException", SqlDbType = System.Data.SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.InnerException });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strTypeOfException", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.ExceptionType });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datDateTimeOccur", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = DateTime.Now });

        }
        else
        {
            cmd.CommandText = "spInsertSqlException";

            ExceptionSql err = (ExceptionSql)objError;


            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strMessage", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.Message });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strSource", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.Source });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strStackTrace", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.StackTrace });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strTargetSite", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.TargetSite });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strInnerException", SqlDbType = System.Data.SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.InnerException });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strTypeOfException", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.ExceptionType });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datDateTimeOccur", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = DateTime.Now });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strClassName", SqlDbType = System.Data.SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.ClassName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intLineNumber", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = err.LineNumber });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strProcedure", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = err.Procedure});

        }

        con.Open();

        if (Convert.ToBoolean((int)cmd.ExecuteNonQuery()) == true)
        {
            IsSuccessful = true;
        }


        con.Close();

        return IsSuccessful;


    }

    public static void SendEmailToExceptionUser(int UserId, string Subject, string ReplyTo, string Body, string EmailAddressTo, string EmailNameTo, string EmailAddressFrom)
    {
        try
        {
            SmtpClient client = new SmtpClient("localhost");
            client.UseDefaultCredentials = false;

            MailAddress from = new MailAddress(EmailAddressFrom, "StoreCreator.com");
            MailAddress to = new MailAddress(EmailAddressTo, EmailNameTo);

            MailMessage message = new MailMessage(from, to);

            MailAddress replyTo = new MailAddress(ReplyTo);

            message.ReplyToList.Add(replyTo);

            message.Subject = Subject;
            message.Body = Body;

            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            client.Send(message);

        }
        catch(SmtpException err)
        {

        }
        finally
        {

        }
    }

}
