using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;


namespace StoreCreator
{
    public partial class FrontEndSearchProducts : System.Web.UI.Page
    {
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
           //  Session["UserId"] = 12;
          //   Session["gg"] = 122;

            if (Session["JustAuthenticated"] != null)
            {
                pnlLoginIsSuccessful.Visible = true;
                Session["JustAuthenticated"] = null;
            }
            else
            {
                pnlLoginIsSuccessful.Visible = false;
            }

            if (!this.Page.IsPostBack)
            {
                List<SqlParameter> Parameters = new List<SqlParameter>();
                ProductInformation info = new ProductInformation();

                lvPopularProducts.DataSource = ProductInformation.GetGenericDataTablesUsingParams("spSelectMostPopularProducts", new List<SqlParameter>());
                lvPopularProducts.DataBind();

                lvMostViewProducts.DataSource = ProductInformation.GetGenericDataTablesUsingParams("spGetMostViewProducts", new List<SqlParameter>());
                lvMostViewProducts.DataBind();

                if (Session["UserId"] != null)
                {
                    SqlParameter paramUserId = new SqlParameter("@UserId", (int)Session["UserId"]);
                    List<SqlParameter> UserIdList = new List<SqlParameter>();
                    UserIdList.Add(paramUserId);

                    lvRecProducts.DataSource = ProductInformation.GetGenericDataTablesUsingParams("spSelectSuggestedProducts", UserIdList);
                    lvRecProducts.DataBind();
                }

                if (Request.QueryString["Gender"] != null)
                {

                    string Gender = Request.QueryString["Gender"];
                    Parameters.Add(new SqlParameter() { ParameterName = "@Gender", Value = Gender });

                    if (Gender == "Male")
                    {
                        RadioButtonList1.SelectedIndex = 0;
                    }
                    else
                    {
                        RadioButtonList1.SelectedIndex = 1;
                    }
                }

                if (Request.QueryString["Category"] != null)
                {
                    string Category = Request.QueryString["Category"];                  
                    Parameters.Add(new SqlParameter() { ParameterName = "@Category", Value = Category });
                    ddlCategory.SelectedValue = Category;
                }   
              
                if(Request.QueryString["BegPrice"] != null)
                {
                        string BegPrice = Request.QueryString["BegPrice"];                  
                        Parameters.Add(new SqlParameter() { ParameterName = "@dblPriceBeg", Value = BegPrice });
                        txtPriceBegin.Text = BegPrice;
                }
                if(Request.QueryString["EndPrice"] != null)
                {

                    string EndPrice = Request.QueryString["EndPrice"];
                    Parameters.Add(new SqlParameter() { ParameterName = "@dblPriceEnd", Value = EndPrice });
                    txtPriceEnd.Text = EndPrice;
                }
                if (Request.QueryString["InventoryEnd"] != null)
                {
                    string InventoryEnd = Request.QueryString["InventoryEnd"];
                    Parameters.Add(new SqlParameter() { ParameterName = "@InventoryEnd", Value = InventoryEnd });
                    txtInventoryEnd.Text = InventoryEnd;
                }
                if (Request.QueryString["InventoryBeg"] != null)
                {
                    string InventoryBeg = Request.QueryString["InventoryBeg"];
                    Parameters.Add(new SqlParameter() { ParameterName = "@InventoryBeg", Value = InventoryBeg });
                    txtInventoryBeg.Text = InventoryBeg;
                }

                    int totalrows = 0;

                    gvStoreProducts.DataSource = UserProfileItems.GetMemberProfileGridViews(gvStoreProducts.PageIndex, gvStoreProducts.PageSize, gvStoreProducts.Attributes["CurrentSortField"], gvStoreProducts.Attributes["CurrentSortDirection"], "spGetFrontEndProductsWithSortingPaging", 2, Parameters, out totalrows);
                    gvStoreProducts.DataBind();

                    UserProfileItems.DataBindRepeater(gvStoreProducts.PageIndex, gvStoreProducts.PageSize, totalrows, rptStoreProducts);


                }
               
            }
        

        private List<SqlParameter> AttachParameters(List<string> ParameterNames, List<Control> Controls)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is TextBox && ((TextBox)Controls[i]).Text != string.Empty)
                {
                    Parameters.Add(new SqlParameter(ParameterNames[i], ((TextBox)Controls[i]).Text));

                }
                else if (Controls[i] is DropDownList && ((DropDownList)Controls[i]).SelectedValue != "-1")
                {
                    Parameters.Add(new SqlParameter(ParameterNames[i], ((DropDownList)Controls[i]).SelectedItem.ToString()));

                }
            }

            return Parameters;
        }


        protected void btnSearchProducts_Click(object sender, EventArgs e)
        {
           
            Page.Validate();
            if (Page.IsValid)
            {
                Dictionary<string, string> QStringParameters = new Dictionary<string, string>();

                if (txtPriceBegin.Text != string.Empty)
                {
                    QStringParameters.Add("BegPrice", txtPriceBegin.Text);
                }

                if (txtPriceEnd.Text != string.Empty)
                {
                    QStringParameters.Add("EndPrice", txtPriceEnd.Text);
                }

                if (txtInventoryBeg.Text != string.Empty)
                {
                    QStringParameters.Add("InventoryBeg", txtInventoryBeg.Text);
                }

                if (txtInventoryEnd.Text != string.Empty)
                {
                    QStringParameters.Add("InventoryEnd", txtInventoryEnd.Text);
                }

                if (ddlCategory.SelectedIndex != 0)
                {
                    QStringParameters.Add("Category", ddlCategory.SelectedItem.ToString());
                }

                if (RadioButtonList1.SelectedItem != null)
                {
                    QStringParameters.Add("Gender", RadioButtonList1.SelectedItem.ToString());
                }

                StringBuilder q = new StringBuilder();

                if (QStringParameters.Count > 0)
                {
                    q.Append("FrontEndSearchProducts.aspx?");

                    foreach (KeyValuePair<string, string> kvp in QStringParameters)
                    {
                        q.Append(kvp.Key + "=" + kvp.Value + "&");
                    }

                    q.Remove((q.Length - 1), 1);

                }
                else
                {
                    q.Append("FrontEndSearchProducts.aspx");
                }

                Response.Redirect(q.ToString());

            }
        }

       
        protected void gvStoreProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() != "sort")
            {
                int RowIndex = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;

                if (e.CommandName == "QuickView")
                {
                    GetModal("modalQuickView");

                    int ProductId = Convert.ToInt32(e.CommandArgument);

                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter paramProductId = new SqlParameter("@ProductId", ProductId);
                    parameters.Add(paramProductId);

                    DataTable dTable = ProductInformation.GetGenericDataTablesUsingParams("spSelectProductByProductId", parameters);
                    fvQuickViewProduct.DataSource = dTable;
                    fvQuickViewProduct.DataBind();

                    List<SqlParameter> parametersReviews = new List<SqlParameter>();
                    parametersReviews.Add(new SqlParameter("@ProductId", ProductId));

                    DataTable dTableReviews = ProductInformation.GetGenericDataTablesUsingParams("spSelectProductReviews", parametersReviews);
                    lvReviews.DataSource = dTableReviews;
                    lvReviews.DataBind();

                }
            }

        }

        protected void gvStoreProducts_DataBound(object sender, EventArgs e)
        {


                
        }

        protected void gvStoreProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
      
        

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Green;
                e.Row.Cells[7].Font.Bold = true;
                e.Row.Cells[5].Font.Bold = true;
                
                DataRowView drv = (DataRowView)e.Row.DataItem;

                int columnIndex = drv.DataView.Table.Columns["blnFeaturedProduct"].Ordinal;
                int Inventory = drv.DataView.Table.Columns["intProductInventory"].Ordinal;

                e.Row.Cells[8].Controls.Clear();

                if (drv[columnIndex].ToString().ToLower() == "true")
                {
                    e.Row.Cells[8].Controls.Clear();
                    e.Row.Cells[8].Controls.Add(new Image(){ImageUrl="checkmark.png", Width= Unit.Pixel(20), Height= Unit.Pixel(20) });
                }

                string Price = DataBinder.Eval(e.Row.DataItem, "decProductPrice").ToString();
                decimal RPrice = Convert.ToDecimal(Price.Remove(0, 1));

                if (Inventory < 10)
                {
                   e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                }

            }
            
        }

        protected void gvStoreProducts_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<string> ParamNames = new List<string>() { "@Category", "@dblPriceBeg", "@dblPriceEnd" };

            List<Control> Controls = new List<Control>() { ddlCategory, txtPriceBegin, txtPriceEnd };

            List<SqlParameter> Parameters = new List<SqlParameter>();

            Parameters.AddRange(AttachParameters(ParamNames, Controls));

            UserProfileItems.OnGridViewSorting(gvStoreProducts, e, "spGetFrontEndProductsWithSortingPaging", rptStoreProducts, Parameters, 1);
        }

        protected void gvStoreProducts_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].Visible = false;
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[4].Visible = false;
            }
        }

        protected void rptProductRepeater_Click(object sender, EventArgs e)
        {
            List<string> ParamNames = new List<string>() { "@Category", "@dblPriceBeg", "@dblPriceEnd" };

            List<Control> Controls = new List<Control>() { ddlCategory, txtPriceBegin, txtPriceEnd };

            List<SqlParameter> Parameters = new List<SqlParameter>();

            Parameters.AddRange(AttachParameters(ParamNames, Controls));


            UserProfileItems.RepeaterLink(gvStoreProducts, sender, e, "spGetFrontEndProductsWithSortingPaging", 2, Parameters, rptStoreProducts);
        }

        private void GetModal(string ModalId)
        {
            string jquery = "$('#" + ModalId + "').modal('show');";
            ClientScript.RegisterStartupScript(typeof(Page), "a key",
           "<script type=\"text/javascript\">" + jquery + "</script>");
        }

        protected void cbHeadergvStoreProducts_CheckedChanged(object sender, EventArgs e)
        {
            FrontEndProdInfo.cbDeleteHeaderCheckedChanged(sender, e, gvStoreProducts);
        }

        protected void btnCompareProducts_Click(object sender, EventArgs e)
        {
           List<string> ProductIds = FrontEndProdInfo.SelectCheckBoxesForCompareFromGridView(sender, e, gvStoreProducts);
           List<CheckBox> chkBoxes = new List<CheckBox>();

            foreach (GridViewRow grow in gvStoreProducts.Rows)
            {
               if (grow.RowType == DataControlRowType.DataRow)
               {
                   CheckBox chk = ((CheckBox)grow.FindControl("cbgvStoreProducts"));
                   chkBoxes.Add(chk);
               }
            }

           if (ProductIds.Count > 0)
           {
               lvCompareProducts.DataSource = FrontEndProdInfo.CompareProducts(ProductIds);
               lvCompareProducts.DataBind();

               GetModal("modalCompareProducts");

               foreach (CheckBox chk in chkBoxes)
               {
                   chk.BorderWidth = Unit.Pixel(0);
               }
           }
           else
           {
               lblCompareStatus.ForeColor = System.Drawing.Color.Red;
               lblCompareStatus.Text = "Must Select Items To Compare";

               foreach (CheckBox chk in chkBoxes)
               {
                   chk.BorderWidth = Unit.Pixel(2);
                   chk.ForeColor = System.Drawing.Color.Red;
               }
           }
        }

     

    }
}