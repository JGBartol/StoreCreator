using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.IO;


    public class RegisterAndLogin
    {

        public abstract class BaseMember
        {
            public string UserEmail { get; set; }
            public string UserName { get; set; }
            public string PassWord { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

        }

        public class BasicMember : BaseMember { }

        public class RegularMember : BaseMember
        {

            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public int? ZipCode { get; set; }
        }

        private static string strSqlConnection
        {
            get { return ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString; }
        }


        public static void RegistrationUserAndStatus(RegularMember m, out int outMemberId, out int outStatus)
        {
            int MemberId = 0;
            int Status = 0;

            SqlConnection con = new SqlConnection(strSqlConnection);

            SqlCommand cmd = new SqlCommand("spRegisterUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (m != null)
            {

                string encPassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(m.PassWord, "SHA1");

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strUserEmail", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = m.UserEmail });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strUserName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = m.UserName });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strPassWord", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = encPassWord });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strFirstName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = m.FirstName });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strLastName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = m.LastName });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strAddress", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = m.Address });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strCity", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = m.City });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strState", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = m.State });

                if (m.ZipCode == null)
                {
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intZipCode", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = DBNull.Value });
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intZipCode", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = m.ZipCode });
                }

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intStatus", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@autMemberId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output, });


            }


            con.Open();
            cmd.ExecuteNonQuery();

            MemberId += Convert.ToInt16(cmd.Parameters["@autMemberId"].Value);
            Status += Convert.ToInt16(cmd.Parameters["@intStatus"].Value);

            con.Close();


            outMemberId = MemberId;
            outStatus = Status;

        }


        public static int AuthenticaUser(string UserName, string PassWord, out string Message)
        {

            int UserId = 0;
            Message = string.Empty;

            using (SqlConnection con = new SqlConnection(strSqlConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = "spAuthenticateUser";

                string EncPassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PassWord, "SHA1");

                List<SqlParameter> Parameters = new List<SqlParameter>
                {
                    new SqlParameter{ParameterName = "@strUserName", Value = UserName, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter{ParameterName = "@strPassWord", Value = EncPassWord, SqlDbType = SqlDbType.NVarChar}
                };

                foreach (SqlParameter parameter in Parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                try
                {
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        int NumberOfRetries = Convert.ToInt32(rdr["intRetryAttempts"]);

                        if (Convert.ToBoolean(rdr["blnAuthenticated"]))
                        {
                            UserId += Convert.ToInt32(rdr["autMemberId"]);
                            Message = "blnAuthenticated";
                            FormsAuthentication.RedirectFromLoginPage(UserName, false);
                            
                            
                        }
                        else if (Convert.ToBoolean(rdr["blnAccountLocked"]) && NumberOfRetries == 0)
                        {
                            Message = "Incorrect UserName or PassWord";
                        }
                        else
                        {
                            Message = (4 - NumberOfRetries).ToString() + " Retries Until Account Is Locked";
                        }

                    }

                }
                catch (SqlException exc)
                {
                    throw new Exception(exc.Message);
                    
                }
                finally
                {
                    con.Close();
                }


                return UserId;
            }
        }

        public static void UploadDefaultMemberAvatar(int UserId)
        {

            FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("Icons\\default-avatar.jpg"), FileMode.Open);
            BinaryReader rdr = new BinaryReader(fs);
           
            using (rdr)
            {
                byte[] ImageBytes = rdr.ReadBytes((int)fs.Length);

                string cs = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        Connection = con,
                        CommandText = "spUploadAvatarDefault",
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intUserId", Value = UserId, SqlDbType = SqlDbType.Int });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@bytUserAvatar", Value = ImageBytes, SqlDbType = SqlDbType.VarBinary });

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();

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


        }
   

    }
