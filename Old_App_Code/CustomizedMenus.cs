using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;


namespace StoreCreator
{
    public class CustomizedMenus
    {


        public static TreeView GetTreeViewItems(int StoreId)
        {

            TreeView tv = new TreeView();

            string cs = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter();

            

                param.ParameterName = "@intStoreId";
                param.Value = StoreId;

                cmd.CommandText = "spGetTreeViewItems";


            

            SqlDataAdapter da = new SqlDataAdapter(cmd);
          //  da.SelectCommand.Parameters.Add(param);

            DataSet ds = new DataSet();
            da.Fill(ds);
            ds.Relations.Add("ChildRows", ds.Tables[0].Columns["strCategoryName"],
                ds.Tables[0].Columns["strProductCategoryName"], false);

            foreach (DataRow level1DataRow in ds.Tables[0].Rows)
            {
                if (!string.IsNullOrEmpty(level1DataRow["strCategoryName"].ToString()))
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Text = level1DataRow["strCategoryName"].ToString();
                    treeNode.NavigateUrl = level1DataRow["strCategoryName"].ToString();

                    DataRow[] level2DataRows = level1DataRow.GetChildRows("ChildRows");

                    foreach (DataRow level2DataRow in level2DataRows)
                    {
                        TreeNode childTreeNode = new TreeNode();
                        childTreeNode.Text = level2DataRow["strProductName"].ToString();
                        childTreeNode.NavigateUrl = "FrontEndProducts.aspx?ProductId=" + level2DataRow["intProductId"].ToString();
                        treeNode.ChildNodes.Add(childTreeNode);
                    }

                    tv.Nodes.Add(treeNode);
                }
            }
            return tv;
        }

        public static Menu GetMenuItems()
        {
            Menu menu = new Menu();

            string cs = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter();
        
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.Parameters.Add(param);


            DataSet ds = new DataSet();
            da.Fill(ds);

            ds.Relations.Add("ChildRows", ds.Tables[0].Columns["tblCategoryName"],
                ds.Tables[0].Columns["tblProductCategory"], false);


            foreach (DataRow level1DataRow in ds.Tables[0].Rows)
            {
                if (!string.IsNullOrEmpty(level1DataRow["tblCategoryName"].ToString()))
                {
                    MenuItem item = new MenuItem();
                    item.Text = level1DataRow["tblCategoryName"].ToString();
                    item.NavigateUrl = level1DataRow["tblCategoryName"].ToString();

                    DataRow[] level2DataRows = level1DataRow.GetChildRows("ChildRows");

                    foreach (DataRow level2DataRow in level2DataRows)
                    {
                        MenuItem childitem = new MenuItem();
                        childitem.Text = level2DataRow["ProductName"].ToString();
                        childitem.NavigateUrl = "FrontEndProducts.aspx?ProductId=" + level2DataRow["ProductId"].ToString();
                        item.ChildItems.Add(childitem);
                    }



                    menu.Items.Add(item);

                }
            }


            return menu;
        }

    }
}