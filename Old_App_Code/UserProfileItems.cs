using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.UI;

namespace StoreCreator
{
    public class UserProfileItems
    {


        #region Functions To Get All Member GridViews With Paging, Sorting, and Sorting Arrows
    

        private static string strSqlConnection
        {
            get { return ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString; }
        }


        public static void DataBindRepeater(int PageIndex, int PageSize, int TotalRows, Repeater repeater)
        {
            int TotalPages = (TotalRows / PageSize);
            List<ListItem> repeaterPages = new List<ListItem>();

            if (TotalPages > 0)
            {
                for (int i = 1; i < TotalPages + 2; i++)
                {
                    repeaterPages.Add(new ListItem(i.ToString(), i.ToString()));
                }

            }

            repeater.DataSource = repeaterPages;
            repeater.DataBind();
        }


        public static void RepeaterLink(GridView grid, object sender, EventArgs e, string sp, int UserId, List<SqlParameter> Parameters, Repeater repeat)
        {
            int totalRows = 0;
            int PageIndex = int.Parse((sender as LinkButton).CommandArgument);

            PageIndex -= 1;
            grid.PageIndex = PageIndex;

            grid.DataSource = GetMemberProfileGridViews(grid.PageIndex, grid.PageSize, grid.Attributes["CurrentSortField"], grid.Attributes["CurrentSortDirection"], sp, UserId, Parameters, out totalRows);
            grid.DataBind();

            DataBindRepeater(grid.PageIndex, grid.PageSize, totalRows, repeat);


        }


        public static void AddArrowsToGridViewSorting(GridView grid, GridViewRowEventArgs e)
        {
            if (grid.Attributes["CurrentSortField"] != null && grid.Attributes["CurrentSortDirection"] != null)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    foreach (TableCell tCell in e.Row.Cells)
                    {
                        if (tCell.HasControls())
                        {
                            LinkButton linkButton = null;
                            if (tCell.Controls[0] is LinkButton)
                            {
                                linkButton = (LinkButton)tCell.Controls[0];

                            }
                            if (linkButton != null && grid.Attributes["CurrentSortField"] == linkButton.CommandArgument)
                            {
                                Image arrowImage = new Image();
                                arrowImage.Width = Unit.Pixel(10);
                                arrowImage.Height = Unit.Pixel(10);
                                arrowImage.BorderWidth = Unit.Pixel(1);
                                arrowImage.BorderColor = System.Drawing.Color.Gray;


                                if (grid.Attributes["CurrentSortDirection"] == "ASC")
                                {
                                    arrowImage.ImageUrl = "Icons\\small_down_arrow.gif";
                                }
                                else
                                {
                                    arrowImage.ImageUrl = "";
                                }

                                tCell.Controls.Add(arrowImage);
                                tCell.Controls.Add(new LiteralControl("&nbsp"));
                            }
                        }
                    }
                }
            }
        }


        public static void SortGridView(GridView gridView, GridViewSortEventArgs e, out SortDirection direction, out string sortField)
        {
            sortField = e.SortExpression;
            direction = e.SortDirection;

            if (gridView.Attributes["CurrentSortDirection"] != null && gridView.Attributes["CurrentSortField"] != null)
            {
                if (sortField == gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"] == "ASC")
                    {
                        direction = SortDirection.Descending;
                    }
                    else
                    {
                        direction = SortDirection.Ascending;
                    }

                }

                gridView.Attributes["CurrentSortField"] = sortField;
                gridView.Attributes["CurrentSortDirection"] = direction == SortDirection.Ascending ? "ASC" : "DESC";
            }

        }


        public static void OnGridViewSorting(GridView gridView, GridViewSortEventArgs e, string sproc, Repeater repeater,  List<SqlParameter> SearchParameters, int userId)
        {
            SortDirection direction = SortDirection.Ascending;
            string sortField = string.Empty;

            int totalRows = 0;
            int totalrowsInput = 0;

            SortGridView(gridView, e, out direction, out sortField);

            string strDirection = direction == SortDirection.Ascending ? "ASC" : "DESC";

            gridView.DataSource = GetMemberProfileGridViews(gridView.PageIndex, gridView.PageSize, e.SortExpression, strDirection, sproc, userId, SearchParameters, out totalRows);
            gridView.DataBind();

            totalrowsInput = totalRows;
            DataBindRepeater(gridView.PageIndex, gridView.PageSize, totalrowsInput, repeater);

        }


        public static DataSet GetMemberProfileGridViews(int PageIndex, int PageSize, string SortExpression, string SortDirection, string StoreProcedure, int UserId, List<SqlParameter> SearchParameters, out int TotalRows)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(strSqlConnection);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoreProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            List<SqlParameter> Parameters = new List<SqlParameter>
            {
            new SqlParameter{ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Value = PageIndex, Direction = ParameterDirection.Input},
            new SqlParameter{ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Value = PageSize, Direction = ParameterDirection.Input},
            new SqlParameter{ParameterName = "@SortExpression", SqlDbType = SqlDbType.NChar, Value = SortExpression, Direction = ParameterDirection.Input},
            new SqlParameter{ParameterName = "@SortDirection", SqlDbType = SqlDbType.NChar, Value = SortDirection, Direction = ParameterDirection.Input},
            new SqlParameter{ParameterName = "@TotalRows", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output},
            new SqlParameter{ParameterName = "@UserId", SqlDbType = SqlDbType.Int, Value = UserId, Direction = ParameterDirection.Input},            
         
     
            
            };

           
             Parameters.AddRange(SearchParameters);
            

            for (int i = 0; i < Parameters.Count; i++)
            {
                cmd.Parameters.Add(Parameters[i]);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                DataSet ds = new DataSet();
                da.Fill(ds, "tblProfileGridView");

                TotalRows = (int)cmd.Parameters["@TotalRows"].Value;

                return ds;

            }
            catch (SqlException exception)
            {
                throw new ApplicationException(exception.Message);

            }
            finally
            {
                con.Close();
            }
        }

      


        public static void cbDeleteHeaderCheckedChanged(object sender, EventArgs e, GridView gridView)
        {
            foreach (GridViewRow gridViewRow in gridView.Rows)
            {
                ((CheckBox)gridViewRow.FindControl("cbDelete" + gridView.ID.ToString())).Checked = ((CheckBox)sender).Checked;
            }
        }


        #endregion

        public static void btnDeleteClick(object sender, EventArgs e, GridView gView, Label lblStatus, string table, string id)
        {
            List<string> objtoDelete = new List<string>();

            foreach (GridViewRow gridViewRow in gView.Rows)
            {
                if (((CheckBox)gridViewRow.FindControl("cbDelete" + gView.ID.ToString())).Checked)
                {
                    string objectId =
                        ((Label)gridViewRow.FindControl("lblCBDelete" + gView.ID.ToString())).Text;
                    objtoDelete.Add(objectId);

                }
            }

            if (objtoDelete.Count > 0)
            {
               DeleteGridViewItems(objtoDelete, table, id);
         
            }
            else
            {

                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "No rows selected to delete";

            }

        }

        public static int DeleteGridViewItems(List<string> intGridViewId, string tblName, string paramId)
        {
            string CS = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                List<string> parameters = intGridViewId.Select((s, i) => "@Parameter" + i.ToString()).ToList();
                string inClause = string.Join(", ", parameters);

                 string deleteCommandText = "Delete from tblLinkProductToKeyWord where fkProductId IN (" + inClause + "); ";
                 deleteCommandText += "Delete from tblLinkProductToOptions where fkProductId IN (" + inClause + "); ";

                 deleteCommandText += "Delete from " + tblName + " where " + paramId + " IN (" + inClause + "); ";

                SqlCommand cmd = new SqlCommand(deleteCommandText, con);

                for (int i = 0; i < parameters.Count; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i], intGridViewId[i]);
                }

                con.Open();

                return (int)cmd.ExecuteNonQuery();
            }
        }

        public static void btnSetAsActiveClick(object sender, EventArgs e, bool IsSetToActive, GridView gView, Label lblStatus, string table, string id)
        {
            List<string> objtoDelete = new List<string>();

            foreach (GridViewRow gridViewRow in gView.Rows)
            {
                if (((CheckBox)gridViewRow.FindControl("cbDelete" + gView.ID.ToString())).Checked)
                {
                    string objectId =
                        ((Label)gridViewRow.FindControl("lblCBDelete" + gView.ID.ToString())).Text;
                    objtoDelete.Add(objectId);

                }
            }

            if (objtoDelete.Count > 0)
            {
                SetGridViewItemsAsActive(IsSetToActive, objtoDelete, table, id);

            }
            else
            {

                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "No rows selected to delete";

            }

        }

        public static void btnSetAsInActiveClick(object sender, EventArgs e, bool IsSetToActive, GridView gView, Label lblStatus, string table, string id)
        {
            List<string> objtoDelete = new List<string>();

            foreach (GridViewRow gridViewRow in gView.Rows)
            {
                if (((CheckBox)gridViewRow.FindControl("cbDelete" + gView.ID.ToString())).Checked)
                {
                    string objectId =
                        ((Label)gridViewRow.FindControl("lblCBDelete" + gView.ID.ToString())).Text;
                    objtoDelete.Add(objectId);

                }
            }

            if (objtoDelete.Count > 0)
            {
                SetGridViewItemsAsInActive(IsSetToActive, objtoDelete, table, id);

            }
            else
            {

                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "No rows selected to delete";

            }

        }

        public static int SetGridViewItemsAsActive(bool SetToTrue, List<string> intGridViewId, string tblName, string paramId)
        {
            string CS = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                List<string> parameters = intGridViewId.Select((s, i) => "@Parameter" + i.ToString()).ToList();
                string inClause = string.Join(", ", parameters);
                string updateCommandText = string.Empty;

                if (SetToTrue)
                {
                    updateCommandText += "Update " + tblName + " set blnIsActive = 'true' where " + paramId + " IN (" + inClause + "); ";
                }
             

                SqlCommand cmd = new SqlCommand(updateCommandText, con);

                for (int i = 0; i < parameters.Count; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i], intGridViewId[i]);
                }

                con.Open();

                return (int)cmd.ExecuteNonQuery();
            }
        }
        public static int SetGridViewItemsAsInActive(bool SetToTrue, List<string> intGridViewId, string tblName, string paramId)
        {
            string CS = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                List<string> parameters = intGridViewId.Select((s, i) => "@Parameter" + i.ToString()).ToList();
                string inClause = string.Join(", ", parameters);
                string updateCommandText = string.Empty;

                if (SetToTrue)
                {
                    updateCommandText += "Update " + tblName + " set blnIsActive = 'false' where " + paramId + " IN (" + inClause + "); ";
                }             

                SqlCommand cmd = new SqlCommand(updateCommandText, con);

                for (int i = 0; i < parameters.Count; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i], intGridViewId[i]);
                }

                con.Open();

                return (int)cmd.ExecuteNonQuery();
            }
        }
        public static DataSet GetUserBasicInformation(int UserId)
        {
        
            SqlConnection con = new SqlConnection(strSqlConnection);

            SqlCommand cmd = new SqlCommand("spSelectBasicUserInformation", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserId = new SqlParameter("@intUserId", SqlDbType.Int);
            paramUserId.Value = UserId;

            cmd.Parameters.Add(paramUserId);

            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (SqlException err)
            {
                throw new ApplicationException(err.Message);
            }
          

        }


        public static void UpdateUserBasicInformation(int UserId, string UserEmail,  string FirstName, string LastName, string Address, string City, string State, int? ZipCode)
        {

            SqlConnection con = new SqlConnection(strSqlConnection);

            SqlCommand cmd = new SqlCommand("spUpdateBasicUserInformation", con);
            cmd.CommandType = CommandType.StoredProcedure;

        
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intUserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = UserId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strUserEmail", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = UserEmail });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strFirstName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = FirstName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strLastName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = LastName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strAddress", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = Address });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strCity", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = City });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strState", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = State });

            if (ZipCode.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intZipCode", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = ZipCode });
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intZipCode", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = DBNull.Value });
            }
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException err)
            {
                throw new ApplicationException(err.Message);
            }
            finally
            {
                con.Close();
            }

        }

    }
}