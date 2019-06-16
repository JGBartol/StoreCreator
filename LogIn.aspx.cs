using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreCreator
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                pnlLoginIsSuccessful.Visible = false;
                pnlLoginIsNotSuccessful.Visible = false;
                pnlForgotPassWordSend.Visible = false;

                HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["LogInCookie"];

                if (currentUserCookie != null)
                {

                    txtPassWord.Text = currentUserCookie["PassWord"];
                    txtUserName.Text = currentUserCookie["UserName"];

                    chkRememberMe.Checked = true;
                }
            }


        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {

            if (chkRememberMe.Checked)
            {
                if (HttpContext.Current.Request.Cookies["LogInCookie"] != null)
                {

                    HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["LogInCookie"];
                    HttpContext.Current.Response.Cookies.Remove("LogInCookie");
                    currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                    currentUserCookie.Value = null;
                    HttpContext.Current.Response.SetCookie(currentUserCookie);

                }

                HttpCookie cookie = new HttpCookie("LogInCookie");

                cookie["UserName"] = txtUserName.Text;
                cookie["PassWord"] = txtPassWord.Text;

                cookie.Expires = DateTime.Now.AddDays(30);


                HttpContext.Current.Response.SetCookie(cookie);
            }
            else if (!chkRememberMe.Checked)
            {
                if (HttpContext.Current.Request.Cookies["LogInCookie"] != null)
                {
                    HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["LogInCookie"];
                    HttpContext.Current.Response.Cookies.Remove("LogInCookie");
                    currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                    currentUserCookie.Value = null;
                    HttpContext.Current.Response.SetCookie(currentUserCookie);
                }
            }


            Page.Validate();

            if (this.Page.IsValid)
            {
                string Message;
                int UserId = RegisterAndLogin.AuthenticaUser(txtUserName.Text, txtPassWord.Text, out Message);

                if (Message == "blnAuthenticated")
                {
                    pnlLoginIsSuccessful.Visible = true;
                    pnlLoginIsNotSuccessful.Visible = false;

                    Session["UserId"] = UserId;
                }
                else
                {
                    pnlLoginIsNotSuccessful.Visible = true;
                    pnlLoginIsSuccessful.Visible = false;

                    lblStatus.Text = Message;
                }
            }
        }

     
      

    }
}