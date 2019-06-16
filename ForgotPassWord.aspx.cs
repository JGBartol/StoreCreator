using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreCreator
{
    public partial class ForgotPassWord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                pnlUserNameNotFound.Visible = false;
                pnlRegisterIsSuccessful.Visible = false;
            }
        }

        private void SendResetLink()
        {

            Page.Validate();
            if (this.Page.IsValid)
            {
                string[] UserEmails = EmailUsers.RetrieveUserEmail(txtUserName.Text.Trim());

                if (UserEmails[0] != null)
                {
                    lblUserEmail.Text = UserEmails[1];

                    string RandomPassWord = CryptoMethods.RandomNumber();
                    Guid guid = Guid.NewGuid();
                    string guidForQString = guid.ToString();

                    EmailUsers.SendPassWordResetLink(UserEmails[0], txtUserName.Text.Trim(), guidForQString, RandomPassWord);

                    EmailUsers.InsertPassWordResetRequest(txtUserName.Text.Trim(), guid, RandomPassWord);

                    pnlUserNameNotFound.Visible = false;
                    pnlRegisterIsSuccessful.Visible = true;
                }
                else
                {
                    pnlUserNameNotFound.Visible = true;
                }
            }
        }

        protected void btnSendResetLink_Click(object sender, EventArgs e)
        {
            SendResetLink();
        }
    }
}