using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreCreator
{
    public partial class LeftNavBar : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                pnlProducts.Visible = false;
                pnlMyProfile.Visible = false;

            }



            if (Request.RawUrl.Contains("Products"))
            {
                pnlProducts.Visible = true;

            }
            else if (Request.RawUrl.Contains("MyProfile"))
            {
                pnlMyProfile.Visible = true;
                A1.Attributes.CssStyle.Add("color", "#669999");
                spanMyProfile.Attributes.CssStyle.Add("color", "#669999");

            }
            else if (Request.RawUrl.Contains("MyOrders"))
            {
                pnlMyProfile.Visible = true;
                A2.Attributes.CssStyle.Add("color", "#669999");
                spanMyOrders.Attributes.CssStyle.Add("color", "#669999");

            }
            else if (Request.RawUrl.Contains("InsertStoreproducts"))
            {
                pnlMyProfile.Visible = true;
                MyStore.Attributes.CssStyle.Add("color", "#669999");
                spanMyStore.Attributes.CssStyle.Add("color", "#669999");
            }

         


        }

        


    }
}