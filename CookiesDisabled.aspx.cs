using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreCreator
{
    public partial class CookiesDisabled : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlCookiesDisabled.Visible = false;
            pnlBrowser.Visible = false;

            if (Request.QueryString["Browser"] != null)
            {
                lblBrowser.Text = Request.QueryString["Browser"];
                pnlBrowser.Visible = true;
            }
            else
            {
                pnlCookiesDisabled.Visible = true;
            }
        }
    }
}