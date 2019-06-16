using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;

public class FrontEndProdInfo
{

    public class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewBody { get; set; }
        public int ReviewRating { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class CheckOutInfo
    {
        public int UserId {get;set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string TypeOfTransaction { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string FullAddress
        {
            get { return Address + " " + City + " " + State + " " + ZipCode.ToString(); }
        }
    }

    public class TransactionLineItems 
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime DateProcessed { get; set; }
        public int StoreId { get; set; }

    }

    public class CreditCardInfo
    {
        public string TypeOfCardName { get; set; }
        public string NameOnCardName { get; set; }
        public byte[] CreditCardNumber { get; set; }
        public DateTime CreditCardExpiryDate { get; set; }
    }


    public class TotalTransaction
    {
        public int TransactionId { get; set; }

    }

    public static bool InsertTransaction(List<object> TransactionClasses)
    {

        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spStoreInsertTransaction", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dtLineItems = new DataTable();
            dtLineItems.Columns.Add("ProductId", typeof(int));
            dtLineItems.Columns.Add("Quantity", typeof(int));
            dtLineItems.Columns.Add("TransactionAmount", typeof(decimal));
            dtLineItems.Columns.Add("DateProcessed", typeof(DateTime));
            dtLineItems.Columns.Add("StoreId", typeof(int));

            foreach (object obj in TransactionClasses)
            {
                if (obj is CheckOutInfo && obj.GetType() == typeof(CheckOutInfo))
                {
                    CheckOutInfo i = (CheckOutInfo)obj;

                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intUserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = i.UserId });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strFirstName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = i.FirstName });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strLastName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = i.LastName });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strUserEmail", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = i.UserEmail });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strAddress", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = i.Address });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strCity", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = i.City });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strState", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = i.State });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intZipCode", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = i.ZipCode });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@TransactionTypeName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = i.TypeOfTransaction });

                }
                else if (obj is TransactionLineItems && obj.GetType() == typeof(TransactionLineItems))
                {
         
                    TransactionLineItems LineItem = (TransactionLineItems)obj;
                    dtLineItems.Rows.Add(LineItem.ProductId, LineItem.Quantity, LineItem.TransactionAmount, DateTime.Now, LineItem.StoreId);
                }
                else if (obj is CreditCardInfo && obj.GetType() == typeof(CreditCardInfo))
                {
                    CreditCardInfo c = (CreditCardInfo)obj;

                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@bytCardNumber", SqlDbType = SqlDbType.VarBinary, Direction = ParameterDirection.Input, Value = c.CreditCardNumber });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strNameOnCard", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = c.NameOnCardName });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strTypeOfCard", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = c.TypeOfCardName });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datCardExpiration", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = c.CreditCardExpiryDate });
                }
            }

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@dtLineItems", SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input, Value = dtLineItems });



                con.Open();
                return Convert.ToBoolean((int)cmd.ExecuteNonQuery());
            /*
            catch (SqlException err)
            {
                throw new ApplicationException(err.Message);
            }
            finally
            {
                con.Close();
            }
            */
        }
    }





    private static string strSqlConnection
    {
        get { return ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString; }
    }

    public static bool CreateReview(int UserId, int ProductId, string ReviewTitle, string ReviewBody, int ReviewRating)
    {
        SqlConnection con = new SqlConnection(strSqlConnection);
        bool IsUpdateSuccessful = false;

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spInsertProductReview", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = UserId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = ProductId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ReviewTitle", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = ReviewTitle });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ReviewBody", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = ReviewBody });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ReviewRating", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = ReviewRating });

            try
            {

                con.Open();
                if ((int)cmd.ExecuteNonQuery() == 1)
                {
                    IsUpdateSuccessful = true;
                }


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

        return IsUpdateSuccessful;
    }

    public static bool InsertGenericMethod(SqlParameter[] Parameters, string Sproc)
    {
        SqlConnection con = new SqlConnection(strSqlConnection);
        bool IsUpdateSuccessful = false;

        using (con)
        {
            SqlCommand cmd = new SqlCommand(Sproc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < Parameters.Length; i++)
            {
                cmd.Parameters.Add(Parameters[i]);
            }
            try
            {
                con.Open();
                if ((int)cmd.ExecuteNonQuery() == 1)
                {
                    IsUpdateSuccessful = true;
                }

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

        return IsUpdateSuccessful;
    }

    public static DataSet SelectShoppingCart(List<string> ProductIds, out string sql)
    {


        string sqlCommandText = "select * from tblProducts where pkProductId in ('";

        string sqlClause = string.Join("', '", ProductIds);

         sqlCommandText += sqlClause;

         sqlCommandText += "')";

         sql = sqlCommandText;

        using (SqlConnection con = new SqlConnection(strSqlConnection))
        {


            SqlCommand cmd = new SqlCommand(sqlCommandText, con);
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                DataSet ds = new DataSet();
                da.Fill(ds, "tblShoppingCart");

                return ds;
              
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

    public static DataSet SelectGenericDataSet(SqlParameter[] Parameters, string Sproc)
    {
        using (SqlConnection con = new SqlConnection(strSqlConnection))
        {
            SqlCommand cmd = new SqlCommand(Sproc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            for (int i = 0; i < Parameters.Length; i++)
            {
                cmd.Parameters.Add(Parameters[i]);
            }
            try
            {
                DataSet ds = new DataSet();
                da.Fill(ds, "tblGenericSelect");

                return ds;
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

    public static string ProductImage(int ProductId)
    {
        string base64image = string.Empty;

        using (SqlConnection con = new SqlConnection(strSqlConnection))
        {
            SqlCommand cmd = new SqlCommand("spGetProductImageById", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@intProductId", ProductId);
            cmd.Parameters.Add(param);
           
            try
            {
                con.Open();


                if (cmd.ExecuteScalar() == DBNull.Value)
                {
                    base64image = string.Empty;
                }
                else
                {
                    byte[] ImageBytes = (byte[])cmd.ExecuteScalar();
                    base64image = "data:Image/png;base64," + Convert.ToBase64String(ImageBytes);
                }
                    return base64image;
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

    public static List<string> SelectProductImages(int ProductId)
    {
        List<string> base64images = new List<string>();

        using (SqlConnection con = new SqlConnection(strSqlConnection))
        {
            SqlCommand cmd = new SqlCommand("spSelectProductImagesById", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@intProductId", ProductId);
            cmd.Parameters.Add(param);

            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    byte[] ImageBytes = ((byte[])rdr["bytImageBytes"]);
                    string base64 = "data:Image/png;base64," + Convert.ToBase64String(ImageBytes);
                    base64images.Add(base64);
                }

                return base64images;
               
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

    public static void PostToProductBulletinBoard(int UserPostId, int ProductId, string Topic, string Message, bool IsParent, int BulletinParentId)
    {

        using (SqlConnection con = new SqlConnection(strSqlConnection))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandText = "spInsertProductParentOrChildBulletinBoard";

            List<SqlParameter> BoardParams = new List<SqlParameter>
                    {

                        new SqlParameter{ParameterName = "@UserId", Value = UserPostId, SqlDbType = SqlDbType.Int},
                        new SqlParameter{ParameterName = "@Topic", Value = Topic, SqlDbType = SqlDbType.NVarChar},
                        new SqlParameter{ParameterName = "@Message", Value = Message, SqlDbType = SqlDbType.NVarChar},
                        new SqlParameter{ParameterName = "@ProductId", Value = ProductId, SqlDbType = SqlDbType.Int},
                        new SqlParameter{ParameterName = "@IsParent", Value = IsParent, SqlDbType = SqlDbType.Bit},
                        new SqlParameter{ParameterName = "@NumOfLikes", Value = 0, SqlDbType = SqlDbType.Int},
                        new SqlParameter{ParameterName = "@DateTimeCreated", Value = DateTime.Now, SqlDbType = SqlDbType.DateTime},
                        new SqlParameter{ParameterName = "@BulletinParentId", Value = BulletinParentId, SqlDbType = SqlDbType.Int}

                    };

            foreach (SqlParameter param in BoardParams)
            {
                cmd.Parameters.Add(param);
            }

            try
            {
                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (SqlException err)
            {
                throw new ApplicationException(err.Message.ToString());
            }
        }
    }

    

    public static StringBuilder GetBulletinBoard(int ProductId)
    {
        StringBuilder s = new StringBuilder();

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(strSqlConnection);

        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "	Select * from tblProductParentBulletinBoard pb left join tblUserAvatar a on pb.intBoardCreatorUserId = a.intUserId inner join tblProducts p on pb.intProductId = p.pkProductId where pb.intProductId = @ProductId";

        cmd.Parameters.Add(new SqlParameter { ParameterName = "@ProductId", Value = ProductId, SqlDbType = SqlDbType.Int });
        //@ParentBoardId
        using (con)
        {
            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblParentBoard");

                da.SelectCommand.CommandText = "select * from tblProductChildBulletinBoard cb left join tblUserAvatar a on cb.intBoardCreatorUserId = a.intUserId inner join tblProductParentBulletinBoard p on cb.intParentBoardId = p.intParentBoardId";
     
                da.Fill(ds, "SecondPost");

                DataRelation drelate = new DataRelation("BulletinBoardRelation", ds.Tables["tblParentBoard"].Columns["intParentBoardId"], ds.Tables["SecondPost"].Columns["intParentBoardId"], false);
                ds.Relations.Add(drelate);

                int i = 0;


                foreach (DataRow d in ds.Tables["tblParentBoard"].Rows)
                {

                    s.Append("<div class=\"row\" style=\"border:1px solid #999999; border-radius:8px; background-color:white;\"  >");
                    s.Append("<div class=\"col-sm-3\" >");

                    s.Append("Posted By: ");
                    s.Append("<img src=\"data:Image/png;base64," + Convert.ToBase64String((byte[])d["bytUserAvatar"]) + "\"  class=\"img-circle\" width=\"60\"  height=\"60\" />");

                    s.Append("<br />");

                    s.Append("@ " + d["datDateTimeCreated"].ToString());

                    s.Append("<br />");
                    s.Append("Like Bulletin");

                    s.Append("<a href=\"FrontEndProducts.aspx?ProductId=" + ProductId.ToString() + "&BulletinBoardMethod=LikeParentBoardItem&BulletinId=" + d["intParentBoardId"].ToString() + "\"><img src=\"Like.png\" width=\"20\" height=\"20\" ></img></a>");
       
                    s.Append("<br />");         
                    s.Append("Number of Likes :");

                    s.Append("<b>");
                    s.Append(UserBulletinLikeCount(Convert.ToInt32(d["intParentBoardId"]), true).ToString());
                    s.Append("</b>");

                    s.Append("<br />");

                    s.Append("<a href=\"FrontEndProducts.aspx?ProductId=" + ProductId.ToString() + "&BulletinBoardMethod=Reply&BulletinId=" + d["intParentBoardId"].ToString() + "\" .com\" >Reply</a>");


                    s.Append("</div>");
                    s.Append("<div class=\"col-sm-9\" >");


                    s.Append("<b>" + d["strBoardTitle"].ToString() + "</b>");
                    s.Append("<br />");

                    s.Append("<p>" + d["strBoardBody"].ToString() + "</p>");

               
                    s.Append("</div>");
                    s.Append("</div>");

                  
                    i++;

                    DataRow[] dataRows = d.GetChildRows(drelate);

                    foreach (DataRow dRow in dataRows)
                    {

                        s.Append("<div class=\"row\" style=\"border:1px solid #999999; background-color:white; border-radius:8px; width:80%;margin-left:100px;\" >");

                        s.Append("<div class=\"col-sm-6\" >");

                        s.Append("Posted By: ");

                        s.Append("<img src=\"data:Image/png;base64," + Convert.ToBase64String((byte[])d["bytUserAvatar"]) + "\" width=\"40\"  height=\"40\" />");

                        s.Append("<br />");

                        s.Append("@ " + dRow["datDateTimeCreated"].ToString());
                     
                        s.Append("<br />");

                        s.Append("Number of Likes :");

                        s.Append("<b>");
                        s.Append(UserBulletinLikeCount(Convert.ToInt32(dRow["intChildBoardId"]), false).ToString());
                        s.Append("</b>");

                        s.Append("<br />");

                        s.Append("Like Bulletin");
                        s.Append("<a href=\"FrontEndProducts.aspx?ProductId=" + ProductId.ToString() + "&BulletinBoardMethod=LikeChildBoardItem&BulletinId=" + dRow["intChildBoardId"].ToString() + "\"><img src=\"Like.png\" width=\"16\" height=\"16\" ></img></a>");

                        s.Append("<br />");

                        s.Append("</div>");
                        s.Append("<div class=\"col-sm-6\" >");

                        s.Append("<b>" + dRow["strBoardTitle"].ToString() + "</b>");
                        s.Append("<br />");

                        s.Append("<p>" + dRow["strBoardBody"].ToString() + "</p>");

                  

                        s.Append("</div>");
                        s.Append("</div>");


                    }

                }

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        return s;
    }


    public static void LikeBulletinBoard(int BoardId, bool IsParentBoard)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(strSqlConnection);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spInsertBulletinLike";
        cmd.Connection = con;

        cmd.Parameters.Add
            (
                new SqlParameter
                {
                    ParameterName = "@BoardId",
                    Value = BoardId,
                    SqlDbType = SqlDbType.Int
                });
        cmd.Parameters.Add
          (
           new SqlParameter
           {
               ParameterName = "@IsParentBoard",
               Value = IsParentBoard,
               SqlDbType = SqlDbType.Bit
           });
     


        try
        {
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            con.Close();
        }

    }


    public static int UserBulletinLikeCount(int BoardId, bool IsParentBoard)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(strSqlConnection);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spGetProductBulletinLikes";
        cmd.Connection = con;

        cmd.Parameters.Add
            (
                new SqlParameter
                {
                    ParameterName = "@BoardId",
                    Value = BoardId,
                    SqlDbType = SqlDbType.Int
                });
        cmd.Parameters.Add
          (
           new SqlParameter
           {
               ParameterName = "@IsParentBoard",
               Value = IsParentBoard,
               SqlDbType = SqlDbType.Bit
           });


        try
        {
            using (con)
            {
                con.Open();

                return (int)cmd.ExecuteScalar();
            }
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            con.Close();
        }

    }

    public static void AttachSingleParameter(SqlCommand cmd, string sqlParamName, WebControl ctrl)
    {
        if (ctrl is DropDownList && ctrl.GetType() == typeof(DropDownList))
        {
            DropDownList ddl = (DropDownList)ctrl;

            if (ddl.SelectedIndex != 0)
            {
                SqlParameter param = new SqlParameter(sqlParamName, ddl.SelectedItem.ToString());
                cmd.Parameters.Add(param);
            }
        }
        else if (ctrl is TextBox && ctrl.GetType() == typeof(TextBox))
        {
            TextBox txt = (TextBox)ctrl;

            if (txt.Text != string.Empty)
            {
                SqlParameter param = new SqlParameter(sqlParamName, txt.Text.Trim());
                cmd.Parameters.Add(param);
            }
        }
        
    }

    public static DataSet GetProductSearchGridView(int PageIndex, int PageSize, string SortExpression, string SortDirection, DataTable dtSearchTerms, int StoreId, string Category, out int TotalRows)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(strSqlConnection);

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "spGetFrontEndProductsSearchWithSortingPaging";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        
            List<SqlParameter> Parameters = new List<SqlParameter>
            {
                new SqlParameter{ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Value = PageIndex, Direction = ParameterDirection.Input},
                new SqlParameter{ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Value = PageSize, Direction = ParameterDirection.Input},
                new SqlParameter{ParameterName = "@SortExpression", SqlDbType = SqlDbType.NChar, Value = SortExpression, Direction = ParameterDirection.Input},
                new SqlParameter{ParameterName = "@SortDirection", SqlDbType = SqlDbType.NChar, Value = SortDirection, Direction = ParameterDirection.Input},
                new SqlParameter{ParameterName = "@TotalRows", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output},
                new SqlParameter{ParameterName = "@dtSearchTerms", SqlDbType = System.Data.SqlDbType.Structured, Value= dtSearchTerms},
                new SqlParameter{ParameterName = "@StoreId", SqlDbType = System.Data.SqlDbType.Int, Value= StoreId},    
            };

            if (Category == string.Empty)
            {
                Parameters.Add(new SqlParameter { ParameterName = "@Category", SqlDbType = System.Data.SqlDbType.NVarChar, Value = DBNull.Value });
            }
            else
            {
                Parameters.Add(new SqlParameter { ParameterName = "@Category", SqlDbType = System.Data.SqlDbType.NVarChar, Value = Category });             
            }

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

    public static void OnGridViewSorting(GridView gridView, GridViewSortEventArgs e, Repeater repeater, int storeId, DataTable dt, string category)
    {
        SortDirection direction = SortDirection.Ascending;
        string sortField = string.Empty;

        int totalRows = 0;
        int totalrowsInput = 0;

        SortGridView(gridView, e, out direction, out sortField);

        string strDirection = direction == SortDirection.Ascending ? "ASC" : "DESC";

        gridView.DataSource = GetProductSearchGridView(gridView.PageIndex, gridView.PageSize, e.SortExpression, strDirection, dt, storeId, category, out totalRows);
        gridView.DataBind();

        totalrowsInput = totalRows;
        DataBindRepeater(gridView.PageIndex, gridView.PageSize, totalRows, repeater);

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

    public static DataSet CompareProducts(List<string> intGridViewId)
    {
        string CS = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;
        using (SqlConnection con = new SqlConnection(CS))
        {
            List<string> parameters = intGridViewId.Select((s, i) => "@Parameter" + i.ToString()).ToList();
            string inClause = string.Join(", ", parameters);

            string compareProducts = "Select " +
            " (SELECT ROUND(AVG(CAST(intProductRating AS FLOAT)), 2) from tblProductReviews where fkProductId = p.pkProductId) as AverageRating, " +
            " (SELECT IsNull(ROUND(AVG(intProductRating), 0, 0), 0) from tblProductReviews where fkProductId = p.pkProductId) as ProductRating, " +
            " p.strProductDescription, p.strProductCategory, p.strProductName, p.pkProductId, p.decProductPrice, p.blnFeaturedProduct, p.bytImage, p.intDiscountId, p.strGender " +
            " from tblProducts p where p.pkProductId IN (" + inClause + "); " ;


            SqlCommand cmd = new SqlCommand(compareProducts, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            for (int i = 0; i < parameters.Count; i++)
            {
                cmd.Parameters.AddWithValue(parameters[i], intGridViewId[i]);
            }

            try
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            finally
            {
                con.Close();
            }
        }
    }

    public void DeleteProductOrders(int OrderId)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(strSqlConnection);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "spDeleteStoreOrder";
        cmd.Connection = con;

        cmd.Parameters.Add
            (new SqlParameter
                {
                    ParameterName = "@OrderId",
                    Value = OrderId,
                    SqlDbType = SqlDbType.Int
                });

        con.Open();
        cmd.ExecuteNonQuery();

    }


    public static List<string> SelectCheckBoxesForCompareFromGridView(object sender, EventArgs e, GridView gView)
    {
        List<string> Ids = new List<string>();

        foreach (GridViewRow gridViewRow in gView.Rows)
        {
            if (((CheckBox)gridViewRow.FindControl("cb" + gView.ID.ToString())).Checked)
            {
                string objectId =
                    ((Label)gridViewRow.FindControl("lblCB" + gView.ID.ToString())).Text;
                Ids.Add(objectId);

            }
        }

        return Ids;
    }

    public static void cbDeleteHeaderCheckedChanged(object sender, EventArgs e, GridView gridView)
    {
        foreach (GridViewRow gridViewRow in gridView.Rows)
        {
            ((CheckBox)gridViewRow.FindControl("cb" + gridView.ID.ToString())).Checked = ((CheckBox)sender).Checked;
        }
    }

   

}