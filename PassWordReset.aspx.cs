using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreCreator
{
    public partial class PassWordReset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Request.QueryString["UniqueId"] != null)
            {
                if (!this.Page.IsPostBack)
                {
                    pnlIsSuccessFul.Visible = false;
                }

                if (Request.QueryString["UniqueId"] != null)
                {
                    pnlResetPassWordTextBoxes.Visible = true;
                }
                else
                {
                    pnlResetPassWordTextBoxes.Visible = false;
                }
            }
        }

        protected void btnNewPassWord_Click(object sender, EventArgs e)
        {

           EmailUsers.ResetPassWordRequest r = EmailUsers.RetrieveResetPassWordInformation(Request.QueryString["UniqueId"]);
           Response.Write(r.TemporaryPassWord.ToString());
           
           if (txtTempPass.Text.Trim() == r.TemporaryPassWord && (Convert.ToDateTime(r.datRequest) < DateTime.Now))
           {
               if (EmailUsers.UpdateNewPassWord(txtNewPass.Text.Trim(), r.UserName.ToString()))
               {
                   pnlIsSuccessFul.Visible = true;
               }
               Response.Write("yoyoyyo");
           }
           else if (Convert.ToDateTime(r.datRequest).AddMinutes(60) < DateTime.Now)
           {

           }

        }
    }
}