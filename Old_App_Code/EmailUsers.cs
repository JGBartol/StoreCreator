using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Security;

public class EmailUsers
{

    private static string strSqlConnection
    {
        get { return ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString; }
    }

    public class ResetPassWordRequest
    {
       public string UserName { get; set; }
       public DateTime datRequest { get; set; }
       public string TemporaryPassWord { get; set; }
       public Guid guidQString { get; set; }
    }


    public static void SendPassWordResetLink(string ToEmail, string UserName, string UniqueId, string TemporaryPassWord)
    {
        string[] UserEmails = RetrieveUserEmail(UserName);

      
            MailMessage mail = new MailMessage("jbartol@mail.usf.edu", ToEmail);

            StringBuilder message = new StringBuilder();

            message.Append("Dear" + UserName + "<br /><br />");
            message.Append("Please click on the following link to reset your password:");
            message.Append("http://localhost:16399/PassWordReset.aspx?UniqueId=" + UniqueId);
            message.Append("Temporary PassWord: " + TemporaryPassWord);


            mail.IsBodyHtml = true;
            mail.Body = message.ToString();
            mail.Subject = "Store Creator PassWord Reset Link";

            mail.From = new MailAddress("NoReply@StoreCreator", "StoreCreator");
            mail.To.Add(new MailAddress(ToEmail));



            SmtpClient c = new SmtpClient();
            c.Port = 587;
            c.Host = "smtp.gmail.com";

            c.Credentials = new System.Net.NetworkCredential("testerjohn@gmail.com", "111111111");
            c.DeliveryMethod = SmtpDeliveryMethod.Network;
            c.EnableSsl = true;

            c.Send(mail);

        
      
    }

    public static void SendProductEmailToFriend(string ToEmail,string ToUserName, string Subject, int ProductId, string FromFirstName, string FromLastName)
    {

        MailMessage mail = new MailMessage("DoNotReply@StoreBuilder.com", ToEmail);

        StringBuilder s = new StringBuilder();


        s.Append("Dear " + ToUserName + ", the following product link is sent to you from: " + FromFirstName + " " + FromLastName);
        s.Append("<br />");
        s.Append("Please click on the following link to view the product http://localhost:16399/FrontEndProducts.aspx?ProductId=" + ProductId.ToString());



        mail.IsBodyHtml = true;
        mail.Body = s.ToString();
        mail.Subject = Subject;

        mail.From = new MailAddress("NoReply@StoreCreator", "StoreCreator");
        mail.To.Add(new MailAddress(ToEmail));



        SmtpClient c = new SmtpClient();
        c.Port = 587;
        c.Host = "smtp.gmail.com";

        //Email Does Not Exist
        c.Credentials = new System.Net.NetworkCredential("testerjohn@gmail.com", "111111111111");
        c.DeliveryMethod = SmtpDeliveryMethod.Network;
        c.EnableSsl = true;

        c.Send(mail);

    }


    public static void SendGenericEmail(string FromAddress, string FromName, string ToAddress, string ToName, string Subject)
    {
        SmtpClient c = new SmtpClient("smtp.gmail.com", 587);

        //Email Does Not Exist
        c.Credentials = new System.Net.NetworkCredential("testerjohn@gmail.com", "1111111111111");
        c.DeliveryMethod = SmtpDeliveryMethod.Network;
        c.EnableSsl = true;

        MailAddress From = new MailAddress(FromAddress, FromName);
        MailAddress To = new MailAddress(ToAddress, ToName);
     //   MailAddress ReplyTo = new MailAddress("StoreCreateReply@mail.com");

        StringBuilder s = new StringBuilder();

        MailMessage m = new MailMessage(From, To);
     //   m.ReplyTo = ReplyTo;
        m.Subject = Subject;
        m.BodyEncoding = System.Text.Encoding.UTF8;
        m.Body = s.ToString();
        m.IsBodyHtml = true;

        c.Send(m);

    }

    public static void SendRegistrationEmail()
    {
      

    }


    public static string[] RetrieveUserEmail(string UserName)
    {
        string[] Emails = new string[2];

        using (SqlConnection con = new SqlConnection(strSqlConnection))
        {
            SqlCommand cmd = new SqlCommand("spSelectUserEmailForPassWordReset", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UserName", SqlDbType = SqlDbType.NVarChar, Value = UserName });

            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    Emails[0] = rdr["UserEmail"].ToString();
                    Emails[1] = rdr["AsteriskEmail"].ToString();
                }

                return Emails;
            }
            catch (SqlException error)
            {
                throw new ApplicationException(error.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }

    public static void InsertPassWordResetRequest(string UserName, Guid guid, string TemporaryPassWord)
    {
      

        using (SqlConnection con = new SqlConnection(strSqlConnection))
        {
            SqlCommand cmd = new SqlCommand("spInsertResetPassword", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUsername = new SqlParameter("@strUserName", UserName);
            SqlParameter paramDateTime = new SqlParameter("@datResetRequestDateTime", DateTime.Now);
            SqlParameter paramTemporaryPassWord = new SqlParameter("@strTempPassWord", TemporaryPassWord);
            SqlParameter paramGuid = new SqlParameter("@guidQueryStringArgument", guid);

            cmd.Parameters.Add(paramUsername);
            cmd.Parameters.Add(paramDateTime);
            cmd.Parameters.Add(paramTemporaryPassWord);
            cmd.Parameters.Add(paramGuid);

            try
            {
                con.Open();
                cmd.ExecuteScalar();

            }
            finally
            {
                con.Close();
            }
        }
    }

    public static ResetPassWordRequest RetrieveResetPassWordInformation(string guid)
    {

        using (SqlConnection con = new SqlConnection(strSqlConnection))
        {
            SqlCommand cmd = new SqlCommand("spSelectResetPassWordInformation", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Guid", SqlDbType = SqlDbType.NVarChar, Value = guid });

            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                ResetPassWordRequest r = new ResetPassWordRequest();

                if (rdr.Read())
                {
                    r.UserName = rdr["strUserName"].ToString();
                    r.datRequest = Convert.ToDateTime(rdr["datResetRequestDateTime"]);
                    r.TemporaryPassWord = rdr["strTemporaryPassWord"].ToString();
                }

                return r;
            }
            catch (SqlException error)
            {
                throw new ApplicationException(error.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }

    public static bool UpdateNewPassWord(string NewPassWord, string UserName)
    {
          SqlConnection con = new SqlConnection(strSqlConnection);

            SqlCommand cmd = new SqlCommand("spUpdateUserPassWord", con);
            cmd.CommandType = CommandType.StoredProcedure;

            string encPassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(NewPassWord, "SHA1");

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@NewPassWord", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = encPassWord });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UserName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = UserName });

            try
            {
                con.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                return Convert.ToBoolean(RowsAffected);
            }
            catch(SqlException err)
            {
                throw new Exception(err.Message);
            }
            finally
            {

            }
    }
}
