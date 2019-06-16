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
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserId"] = 13;

            int UserId = (int)Session["UserId"];

            ReadImageFromDB(UserId);
                    
            if (!this.Page.IsPostBack)
            {
                pnlUpdateInfo.Visible = false;
                pnlImageCreationIsSuccessful.Visible = false;

                PopulateUserInformation(UserId);
            
            }

            

        }


        private void PopulateUserInformation(int UserId)
        {
            DataSet dt = UserProfileItems.GetUserBasicInformation(UserId);

            if (dt.Tables[0].Rows.Count > 0)
            {
                txtFirstName.Text = dt.Tables[0].Rows[0]["strFirstName"].ToString();
                txtLastName.Text = dt.Tables[0].Rows[0]["strLastName"].ToString();
                txtAddress.Text = dt.Tables[0].Rows[0]["strAddress"].ToString();
                txtCity.Text = dt.Tables[0].Rows[0]["strCity"].ToString();
                ddlStates.SelectedValue = dt.Tables[0].Rows[0]["strState"].ToString();
                txtZipCode.Text = dt.Tables[0].Rows[0]["intZipCode"].ToString();
                txtUserEmail.Text = dt.Tables[0].Rows[0]["strEmail"].ToString();
                
            }

        }

        protected void btnUploadAvatar_Click(object sender, EventArgs e)
        {
            HttpPostedFile Avatar = filAvatarUpload.PostedFile;
            string FileName = Path.GetFileName(filAvatarUpload.PostedFile.FileName);
            string FileExtension = Path.GetExtension(FileName);
            int FileSize = Avatar.ContentLength;


            if (FileExtension == ".jpg" || FileExtension == ".gif"
                || FileExtension == ".png" || FileExtension == ".bmp")
            {
                Stream s = Avatar.InputStream;
                BinaryReader rdr = new BinaryReader(s);
                byte[] ImageBytes = rdr.ReadBytes((int)s.Length);

                string cs = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        Connection = con,
                        CommandText = "spUploadAvatar",
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intUserId", Value = (int)Session["UserId"], SqlDbType = SqlDbType.Int });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strFileName", Value = FileName, SqlDbType = SqlDbType.NVarChar });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@bytUserAvatar", Value = ImageBytes, SqlDbType = SqlDbType.VarBinary });

                    try
                    {
                        con.Open();

                        if ((int)cmd.ExecuteNonQuery() == 1)
                        {

                            pnlImageCreationIsSuccessful.Visible = true;
                            ReadImageFromDB((int)Session["UserId"]);
                        }
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
            else
            {
                lblUploadStatus.ForeColor = System.Drawing.Color.Red;
                lblUploadStatus.Text = "Only images are allowed";
            }
        }



        private void ReadImageFromDB(int UserId)
        {
            string cs = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spSelectUserAvatar", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter()
                {
                    ParameterName = "@intUserId",
                    Value = UserId

                };

                cmd.Parameters.Add(paramId);

                try
                {
                    con.Open();

                    byte[] bytImage = (byte[])cmd.ExecuteScalar();
                    string base64 = Convert.ToBase64String(bytImage);
                    imgAvatar.ImageUrl = "data:Image/png;base64," + base64;
                }
                finally
                {
                    con.Close();
                }

            }
        }

        protected void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            this.Page.Validate();
            int? ZipCode;

            if (txtZipCode.Text != string.Empty)
            {               
                  ZipCode = Convert.ToInt32(txtZipCode.Text);               
            }
            else
            {
                ZipCode = null;
            }

            if (this.Page.IsValid)
            {
                UserProfileItems.UpdateUserBasicInformation(Convert.ToInt32(Session["UserId"]), txtUserEmail.Text, txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtCity.Text, ddlStates.SelectedItem.ToString(), ZipCode);
                pnlUpdateInfo.Visible = true;
            }
        }


       
   
    }
}
    
