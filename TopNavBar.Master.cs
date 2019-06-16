using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace StoreCreator
{
    public partial class TopNavBar : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateProducts.Visible = false;
           // Label1.Text = Application["UsersOnline"].ToString() + "Users Online";

            if (Session["UserId"] != null)
            {
                if ((int)Session["UserId"] == 12)
                {
                    CreateProducts.Visible = true;
                }              
            }

            if (!this.Page.IsPostBack)
            {
                if (Request.RawUrl.Contains("Products"))
                {

                    Products.Attributes.CssStyle.Add("color", "#669999");
                    spanProducts.Attributes.CssStyle.Add("color", "#669999");
                }
                else if (Request.RawUrl.Contains("Register"))
                {

                    Register.Attributes.CssStyle.Add("color", "#669999");
                    spanRegister.Attributes.CssStyle.Add("color", "#669999");
                }

                else if (Request.RawUrl.Contains("LogIn"))
                {
                    Login.Attributes.CssStyle.Add("color", "#669999");
                    spanLogin.Attributes.CssStyle.Add("color", "#669999");
                }
                else if (Request.RawUrl.Contains("LogOut"))
                {
                    LogOut.Attributes.CssStyle.Add("color", "#669999");
                    spanLogOut.Attributes.CssStyle.Add("color", "#669999");
                }
                else if (Request.RawUrl.Contains("Profile"))
                {
                    MyProfile.Attributes.CssStyle.Add("color", "#669999");
                    spanMyProfile.Attributes.CssStyle.Add("color", "#669999");
                }
            }

            if (Session["ShoppingCart"] != null)
            {
                ShoppingCart.Visible = true;
            }
            else
            {
                ShoppingCart.Visible = false;
            }

            if (Application["UserIdsOnline"] != null)
            {
                foreach (int UserId in (List<int>)Application["UserIdsOnline"])
                {
                    Label1.Text += "<b>" + UserId.ToString() + "</b>";
                }
            }
    
        }

        public Label LabelOnMasterPage
        {
            get
            {
                return this.Label1;
            }
        }

    }
}