using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

public class ProductInformation
{

    public class Product
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime ProductCreationDate { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public bool IsFeatured { get; set; }
        public Guid ProductGuid { get; set; }

    }

    public class ProductCategory
    {
        public int UserId { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public DateTime CategoryCreationDate { get; set; }
        public string CategoryDescription { get; set; }

    }

    public class ProductPage
    {
        public int UserId { get; set; }
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageContent { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
    public class ProductsSettings
    {
        public int UserId { get; set; }
        public int TransactionId { get; set; }
        public bool UseLowStockLabel { get; set; }
        public bool UseProductRatingSystem { get; set; }
        public bool UseReferAFriend { get; set; }
        public bool UseBulletinBoard { get; set; }
        public DateTime DateUpdated { get; set; }

    }

    public class GetProductInformation
    {
        public int UserId { get; set; }

      

        public List<ProductCategory> GetProductCategories
        {
            get { return GetProductCategories(UserId); }

        }
    }


    private static string strSqlConnection
    {
        get { return ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString; }
    }

    public static bool CreateProduct(Product p, DataTable dtKeyWords, DataTable dtOptions, DataTable dtImages)
    {
        bool IsSuccessful = false;
        SqlConnection con = new SqlConnection(strSqlConnection);
        SqlCommand cmd = new SqlCommand("spInsertProduct", con);
        cmd.CommandType = CommandType.StoredProcedure;


        if (p != null)
        {


            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intUserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = p.UserId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strProductName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.ProductName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@decProductPrice", SqlDbType = SqlDbType.Money, Direction = ParameterDirection.Input, Value = p.ProductPrice });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strProductDescription", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.ProductDescription });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@blnIsFeatured", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = p.IsFeatured });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@dtKeyWords", SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input, Value = dtKeyWords });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@dtOptions", SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input, Value = dtOptions });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@dtImageInfo", SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input, Value = dtImages });


        }

        con.Open();

        if ((int)cmd.ExecuteNonQuery() == 1)
        {
            IsSuccessful = true;
        }


        con.Close();


        return IsSuccessful;
    }

    public DataSet dsGetAllProducts() 
    {
        SqlConnection con = new SqlConnection(strSqlConnection);

        SqlCommand cmd = new SqlCommand("spSelectAllProducts", con);
        cmd.CommandType = CommandType.StoredProcedure;


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        try
        {
            da.Fill(ds);
            return ds;
        }
        catch (SqlException e)
        {
            throw new ApplicationException(e.Message);
        }

    }

    public DataSet dsGetProductsByStoreId(int StoreId)
    {
        SqlConnection con = new SqlConnection(strSqlConnection);

        SqlCommand cmd = new SqlCommand("spSelectStoreProductsByStoreId", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intStoreId", Value = StoreId });

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        try
        {
            da.Fill(ds);
            return ds;
        }
        catch (SqlException e)
        {
            throw new ApplicationException(e.Message);
        }

    }

    public static bool CreateCategory(ProductCategory c)
    {
        bool IsSuccessful = false;
        SqlConnection con = new SqlConnection(strSqlConnection);
        SqlCommand cmd = new SqlCommand("spInsertProductCategory", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (c != null)
        {

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intUserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = c.UserId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strProductCategory", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = c.ProductCategoryName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strProductCategoryDescription", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = c.CategoryDescription });

        }

        con.Open();

        if ((int)cmd.ExecuteNonQuery() == 1)
        {
            IsSuccessful = true;
        }


        con.Close();

        return IsSuccessful;
    }

    public static List<ProductCategory> GetProductCategories(int UserId)
    {
        List<ProductCategory> Categories = new List<ProductCategory>();
        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spSelectProductCategories", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserId = new SqlParameter("@UserId", SqlDbType.Int);
            paramUserId.Value = UserId;

            cmd.Parameters.Add(paramUserId);

            try
            {

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (!rdr.HasRows)
                    {
                        return null;
                    }
                    else
                    {
                        ProductCategory c = new ProductCategory()
                        {
                            ProductCategoryId = Convert.ToInt32(rdr["pkCategoryId"]),
                            ProductCategoryName = rdr["strCategoryName"].ToString(),
                            CategoryDescription = rdr["strCategoryDescription"].ToString(),
                            CategoryCreationDate = Convert.ToDateTime(rdr["datDateTimeAdded"]),

                        };

                        Categories.Add(c);

                    }

                }

                return Categories;
            }
            catch (SqlException error)
            {
                throw new ApplicationException(error.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }

    public DataSet GetGenericDataSet(List<SqlParameter> Params, string Proc)
    {

        SqlConnection con = new SqlConnection(strSqlConnection);

        SqlCommand cmd = new SqlCommand(Proc, con);
        cmd.CommandType = CommandType.StoredProcedure;

        foreach (SqlParameter Param in Params)
        {
            cmd.Parameters.Add(Param);
        }

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

    public static bool InsertProductPage(int UserId, ProductPage p)
    {
        bool IsSuccessful = false;

        SqlConnection con = new SqlConnection(strSqlConnection);
        SqlCommand cmd = new SqlCommand("spInsertProductPage", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (p != null)
        {


            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intUserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = UserId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strProductPageTitle", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.PageName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strProductPageContent", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.PageContent });

        }

        con.Open();

        if ((int)cmd.ExecuteNonQuery() == 1)
        {
            IsSuccessful = true;
        }


        con.Close();

        return IsSuccessful;
    }

    public static ProductPage GetProductPageNameAndContent(int ProductPageId)
    {

        ProductPage page = new ProductPage();
        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spSelectProductPageByPageId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@PageId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = ProductPageId });

            try
            {
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        page.PageName = rdr["PageName"].ToString();
                        page.PageContent = rdr["PageContent"].ToString();

                    }
                    else
                    {
                        page = null;
                    }
                }

            }
            catch (SqlException err)
            {

            }
            finally
            {
                con.Close();
            }

            return page;
        }

    }

    public static bool UpdateProductPage(ProductPage p)
    {
        bool IsSuccessful = false;

        SqlConnection con = new SqlConnection(strSqlConnection);
        SqlCommand cmd = new SqlCommand("spUpdateProductPage", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (p != null)
        {


            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intPageId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = p.PageId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strProductPageTitle", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.PageName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strProductPageContent", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.PageContent });

        }

        con.Open();

        if ((int)cmd.ExecuteNonQuery() == 1)
        {
            IsSuccessful = true;
        }


        con.Close();

        return IsSuccessful;
    }

    public static bool DeleteProductPage(int PageId)
    {
        SqlConnection con = new SqlConnection(strSqlConnection);
        bool IsSuccessful = false;

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spDeleteProductPageByPageId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@PageId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageId });

            try
            {
                con.Open();

                if ((int)cmd.ExecuteNonQuery() == 1)
                {
                    IsSuccessful = true;
                }

            }
            catch (SqlException err)
            {

            }
            finally
            {
                con.Close();
            }


        }

        return IsSuccessful;

    }

    public static Dictionary<int, string> GetProductIdsAndNames(int UserId)
    {
        Dictionary<int, string> dic = new Dictionary<int, string>();
        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spSelectProductIdAndName", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = UserId });

            try
            {
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        dic.Add(
                                 Convert.ToInt32(rdr["pkProductId"]),
                                 rdr["strProductName"].ToString()
                               );
                    }
                }
            }
            catch (SqlException err)
            {

            }
            finally
            {
                con.Close();
            }

            return dic;
        }
    }

    public static ProductCategory GetCategory(int CategoryId)
    {

        ProductCategory category = new ProductCategory();
        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spSelectCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intCategoryId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = CategoryId });

            try
            {
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        category.ProductCategoryName = rdr["pkCategoryId"].ToString();
                        category.ProductCategoryName = rdr["strCategoryName"].ToString();
                        category.CategoryDescription = rdr["strCategoryDescription"].ToString();
                        category.CategoryCreationDate = Convert.ToDateTime(rdr["datDateTimeAdded"]);
                    }
                    else
                    {
                        category = null;
                    }
                }

            }
            catch (SqlException err)
            {

            }
            finally
            {
                con.Close();
            }

            return category;
        }

    }


    public void DeleteProduct(int ProductId)
    {
        ProductCategory category = new ProductCategory();
        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spDeleteProduct", con);
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intProductId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = ProductId });

            cmd.CommandType = CommandType.StoredProcedure;


            try
            {

                con.Open();
                cmd.ExecuteNonQuery();


            }
            catch (SqlException err)
            {

            }
            finally
            {
                con.Close();
            }

        }
    }


    public static void UpdateProduct(int ProductId, string CategoryName, string ProductName, string ProductDescription, decimal ProductPrice, bool IsFeatured, int Inventory)
    {
        ProductCategory category = new ProductCategory();
        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spUpdateProduct", con);
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intProductId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = ProductId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@CategoryName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = CategoryName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ProductName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = ProductName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ProductDescription", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = ProductDescription });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ProductPrice", SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input, Value = ProductPrice });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IsFeatured", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = IsFeatured });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Inventory", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = Inventory });

            cmd.CommandType = CommandType.StoredProcedure;


            try
            {

                con.Open();
                cmd.ExecuteNonQuery();


            }
            catch (SqlException err)
            {

            }
            finally
            {
                con.Close();
            }

        }
    }

    public static void InsertProduct(string CategoryName, string ProductName, string ProductDescription, decimal ProductPrice, bool IsFeatured, int Inventory, int UserId)
    {
        ProductCategory category = new ProductCategory();
        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spInsertProduct", con);
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ProductCategory", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = CategoryName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ProductName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = ProductName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ProductDescription", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = ProductDescription });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ProductPrice", SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input, Value = ProductPrice });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IsFeatured", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = IsFeatured });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Inventory", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = Inventory });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@StoreId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = UserId });

            SqlParameter paramProductId = new SqlParameter("@outProductId", SqlDbType.Int);
            paramProductId.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paramProductId);

            cmd.CommandType = CommandType.StoredProcedure;


           

                con.Open();
                cmd.ExecuteNonQuery();

                int ProductId = Convert.ToInt32(cmd.Parameters["@outProductId"].Value);

                InsertDefaultImage(ProductId);
          
        }
    }

    public static void InsertDefaultImage(int ProductId)
    {

                FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("Icons\\DefaultProductImage.png"), FileMode.Open);
                using (fs)
                {
                    using (BinaryReader binaryReader = new BinaryReader(fs))
                    {
                        Byte[] bytes = binaryReader.ReadBytes((int)fs.Length);

                        using (SqlConnection con = new SqlConnection(strSqlConnection))
                        {
                            SqlCommand cmd = new SqlCommand("spUploadDefaultProductImage", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlParameter paramImageData = new SqlParameter()
                            {
                                ParameterName = "@bytImage",
                                Value = bytes
                            };
                            SqlParameter paramProductId = new SqlParameter()
                            {
                                ParameterName = "@intProductId",
                                Value = ProductId
                            };

                            cmd.Parameters.Add(paramImageData);
                            cmd.Parameters.Add(paramProductId);

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                fs.Close();

    }


    public static bool EditCategory(int CategoryId, string CategoryName, string CategoryDescription)
    {

        ProductCategory category = new ProductCategory();
        SqlConnection con = new SqlConnection(strSqlConnection);
        bool IsUpdateSuccessful = false;

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spUpdateProductCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intCategoryId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = CategoryId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strCategoryName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = CategoryName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strCategoryDescription", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = CategoryDescription });

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

            }
            finally
            {
                con.Close();
            }
        }

        return IsUpdateSuccessful;
    }

    public static bool UpdateProductSettings(int UserId, bool UseLowStockLabel, bool UseProductRatingSystem, bool UseReferAFriend, bool UseBulletinBoard)
    {
        SqlConnection con = new SqlConnection(strSqlConnection);
        bool IsUpdateSuccessful = false;

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spUpdateProductSettings", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intUserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = UserId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@blnUseLowStockLabel", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = UseLowStockLabel });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@blnUseProductRatingSystem", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = UseProductRatingSystem });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@blnUseReferAFriend", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = UseReferAFriend });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@blnUseBulletinBoard", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = UseBulletinBoard });

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

            }
            finally
            {
                con.Close();
            }
        }

        return IsUpdateSuccessful;

    }

    public static DataSet UserProductSettings(int UserId)
    {
        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand("spSelectProductSettings", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intUserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = UserId });

            try
            {

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;

            }
            catch (SqlException err)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
    }

    public static DataTable GetGenericDataTable(string SqlSproc, int UserId)
    {

        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand(SqlSproc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@intUserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = UserId });

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds.Tables[0];

            }
            catch (SqlException err)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
    }

    public static DataTable GetGenericDataTablesUsingParams(string SqlSproc, List<SqlParameter> Parameters)
    {

        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand(SqlSproc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter Param in Parameters)
            {
                cmd.Parameters.Add(Param);
            }

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds.Tables[0];

            }
            catch (SqlException err)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
    }

    public static int ApplyProductPagesToGridView(DataTable dtPromotionOrDiscount)
    {

        string CS = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;
        int ReturnCount = 0;

        using (SqlConnection con = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("spLinkProductToProductPage", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@dtProductPages", SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input, Value = dtPromotionOrDiscount });

            try
            {
                con.Open();
                ReturnCount += (int)cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();

            }

        }

        return ReturnCount;

    }


    public static DataSet SearchProducts(DataTable dtSearchTerms)
    {

        string CS = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;

        using (SqlConnection con = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("spSearchProducts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@dtSearchTerms", SqlDbType = SqlDbType.Structured, Value = dtSearchTerms });
            SqlDataAdapter da = new SqlDataAdapter(cmd);

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
    public static bool InsertMainProductImage(int ProductId, byte[] ImageData)
    {
        using (SqlConnection con = new SqlConnection(strSqlConnection))
        {
            SqlCommand cmd = new SqlCommand("spInsertProductMainImage", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramImageData = new SqlParameter()
            {
                ParameterName = "@bytImageData",
                Value = ImageData,
                SqlDbType = SqlDbType.VarBinary
            };
            SqlParameter paramProductId = new SqlParameter()
            {
                ParameterName = "@intProductId",
                Value = ProductId,
                SqlDbType = SqlDbType.Int
            };

            cmd.Parameters.Add(paramImageData);
            cmd.Parameters.Add(paramProductId);

            con.Open();
            int ReturnCount = cmd.ExecuteNonQuery();

            return Convert.ToBoolean(ReturnCount);
        }

    }

    public static int InsertProductImages(DataTable dtImages, int ProductId)
    {

        using (SqlConnection con = new SqlConnection(strSqlConnection))
        {
            SqlCommand cmd = new SqlCommand("spInsertProductImages", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramImageData = new SqlParameter()
            {
                ParameterName = "@dtProductImages",
                Value = dtImages,
                SqlDbType = SqlDbType.Structured
            };
            SqlParameter paramProductId = new SqlParameter()
            {
                ParameterName = "@intProductId",
                Value = ProductId,
                SqlDbType = SqlDbType.Int
            };

            cmd.Parameters.Add(paramImageData);
            cmd.Parameters.Add(paramProductId);

            con.Open();
            int ReturnCount = cmd.ExecuteNonQuery();

            return ReturnCount;
        }
    }

}



