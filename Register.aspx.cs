using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreCreator
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                pnlUserEmailAlreadyTaken.Visible = false;
                pnlUserNameAlreadyTaken.Visible = false;
                pnlRegisterIsSuccessful.Visible = false;
               
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                RegisterAndLogin.RegularMember m = new RegisterAndLogin.RegularMember();
                m.FirstName = txtFirstName.Text.Trim();
                m.LastName = txtLastName.Text.Trim();
                m.PassWord = txtPassWord.Text.Trim();
                m.UserEmail = txtUserEmail.Text.Trim();
                m.UserName = txtUserName.Text.Trim().ToString();
                m.Address = txtAddress.Text.Trim();
                m.City = txtCity.Text.Trim();

                if (ddlStates.SelectedIndex != 0)
                {
                    m.State = ddlStates.SelectedItem.ToString();
                }
                else
                {
                    m.State = string.Empty;
                }

                int? ZipCode;

                if (txtZipCode.Text.Trim().ToString() != string.Empty)
                {
                    ZipCode = Convert.ToInt32(txtZipCode.Text.Trim().ToString());
                }
                else
                {
                    ZipCode = null;
                }

                m.ZipCode = ZipCode;

                int UserId;
                int Status;
                RegisterAndLogin.RegistrationUserAndStatus(m, out UserId, out Status);

                if (Status == 1)
                {
                    pnlRegisterIsSuccessful.Visible = true;
                    RegisterAndLogin.UploadDefaultMemberAvatar(UserId);
                }
                else if (Status == 2)
                {
                    pnlUserEmailAlreadyTaken.Visible = true;
                }
                else if (Status == 3)
                {
                    pnlUserNameAlreadyTaken.Visible = true;
                }
                else if (Status == 4)
                {
                    pnlUserEmailAlreadyTaken.Visible = true;
                    pnlUserNameAlreadyTaken.Visible = true;
                }
            }
        }
    }
}