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
    public partial class MyOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["UserId"] = 1;

            if (!this.Page.IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    GetUserOrders((int)Session["UserId"]);

                 }

                pnlDeleteOrder.Visible = false;
            }
        }

        protected void lvMyOrders_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
          
        }

        protected void lvMyOrders_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                if (Session["UserId"] != null)
                {
                    FrontEndProdInfo info = new FrontEndProdInfo();
                    info.DeleteProductOrders(Convert.ToInt32(e.CommandArgument));

                    GetUserOrders((int)Session["UserId"]);

                    pnlDeleteOrder.Visible = true;
                }
            }
        }

        private void GetUserOrders(int UserId)
        {
            if (Session["UserId"] != null)
            {
                List<SqlParameter> Parameters = new List<SqlParameter>();
                Parameters.Add(new SqlParameter() { ParameterName = "@UserId", Value = UserId });

                lvMyOrders.DataSource = FrontEndProdInfo.SelectGenericDataSet(Parameters.ToArray(), "spSelectUserOrders");
                lvMyOrders.DataBind();

                List<SqlParameter> ParametersTwo = new List<SqlParameter>();
                ParametersTwo.Add(new SqlParameter() { ParameterName = "@StoreId", Value = UserId });

                lvMySoldOrders.DataSource = FrontEndProdInfo.SelectGenericDataSet(ParametersTwo.ToArray(), "spSelectUserSoldOrders");
                lvMySoldOrders.DataBind(); 
            }
        }
    }
}