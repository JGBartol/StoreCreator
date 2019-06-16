using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public class UserProductPromotion
{
    public class ProductDiscount
    {
        public int UserId { get; set; }
        public int DiscountId { get; set; }
        public string DiscountName { get; set; }
        public DateTime DiscountCreationDate { get; set; }
        public string DiscountDescription { get; set; }
        public decimal TotalAmountOff { get; set; }
        public decimal PercentageOff { get; set; }
        public DateTime DateStarts { get; set; }
        public DateTime DateEnds { get; set; }
    }

    public class ProductPromotion
    {
        public int UserId { get; set; }
        public int PromotionId { get; set; }
        public string TypeOfPromotion { get; set; }
        public string PromotionName { get; set; }
        public bool IsPercentageOff { get; set; }
        public decimal AmountOff { get; set; }
        public string PromotionDescription { get; set; }
        public string Operator { get; set; }
        public decimal OperatorAmount { get; set; }
        public DateTime DateStarts { get; set; }
        public DateTime DateEnds { get; set; }
        public DateTime PromotionCreationDate { get; set; }

    }


    private static string strSqlConnection
    {
        get { return ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString; }
    }


    public static bool InsertProductDiscount(int UserId, ProductDiscount d)
    {
        bool IsSuccessful = false;

        SqlConnection con = new SqlConnection(strSqlConnection);
        SqlCommand cmd = new SqlCommand("spInsertProductDiscount", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (d != null)
        {

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = d.UserId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strDiscountName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = d.DiscountName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strDiscountDescription", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = d.DiscountDescription });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@decPercentageOff", SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input, Value = d.PercentageOff });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@decAmountOff", SqlDbType = SqlDbType.Money, Direction = ParameterDirection.Input, Value = d.TotalAmountOff });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datDateBegins", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = d.DateStarts });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datDateEnds", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = d.DateEnds });

        }

        con.Open();

        if ((int)cmd.ExecuteNonQuery() == 1)
        {
            IsSuccessful = true;
        }


        con.Close();

        return IsSuccessful;
    }

    public static bool InsertProductPromotion(int UserId, ProductPromotion p)
    {
        bool IsSuccessful = false;

        SqlConnection con = new SqlConnection(strSqlConnection);
        SqlCommand cmd = new SqlCommand("spInsertProductPromotion", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (p != null)
        {
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strTypeOfPromotion", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.TypeOfPromotion });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = p.UserId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strPromotionName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.PromotionName });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@bnlIsPercentageOff", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = p.IsPercentageOff });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@decAmountOff", Direction = ParameterDirection.Input, Value = p.AmountOff });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strPromotionDescription", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.PromotionDescription });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strOperator", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.Operator });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@decOperatorAmount", Direction = ParameterDirection.Input, Value = p.OperatorAmount });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datDateBegins", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = p.DateStarts });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datDateEnds", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = p.DateEnds });

        }

        con.Open();

        if ((int)cmd.ExecuteNonQuery() == 1)
        {
            IsSuccessful = true;
        }


        con.Close();

        return IsSuccessful;
    }

    public static DataSet dsDiscountOrPromotion(string Sproc, int PromotionId)
    {

        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(strSqlConnection);

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = Sproc;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "@PromotionId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PromotionId });
       
        try
        {
            DataSet ds = new DataSet();
            da.Fill(ds);

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

    public static void DeleteSingularDiscountOrPromotion(int PromotionId, bool IsDiscount)
    {

        SqlConnection con = new SqlConnection(strSqlConnection);

        using (con)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if (IsDiscount)
            {
                cmd.CommandText = "spDeleteDiscount";
            }
            else
            {
                cmd.CommandText = "spDeletePromotion";
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@PromotionId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PromotionId });

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

    public static bool EditDiscountOrPromotion(object obj)
    {
        SqlConnection con = new SqlConnection(strSqlConnection);
        bool UpdateIsSuccessful = false;

        using (con)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            if (obj is ProductDiscount)
            {

                ProductDiscount d = (ProductDiscount)obj;

                cmd.CommandText = "spUpdateDiscount";

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@PromotionId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = d.DiscountId });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strDiscountName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = d.DiscountName });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strDiscountDescription", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = d.DiscountDescription });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@decPercentageOff", SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input, Value = d.PercentageOff });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@decAmountOff", SqlDbType = SqlDbType.Money, Direction = ParameterDirection.Input, Value = d.TotalAmountOff });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datDateBegins", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = d.DateStarts });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datDateEnds", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = d.DateEnds });
            
            }
            else if (obj is ProductPromotion && obj.GetType() == typeof(ProductPromotion))
            {

                ProductPromotion p = (ProductPromotion)obj;

                cmd.CommandText = "spUpdateProductPromotion";

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strTypeOfPromotion", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.TypeOfPromotion });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@PromotionId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = p.PromotionId });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strPromotionName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.PromotionName });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@bnlIsPercentageOff", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = p.IsPercentageOff });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@decAmountOff", Direction = ParameterDirection.Input, Value = p.AmountOff });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strPromotionDescription", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.PromotionDescription });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@strOperator", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = p.Operator });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@decOperatorAmount", Direction = ParameterDirection.Input, Value = p.OperatorAmount });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datDateBegins", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = p.DateStarts });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datDateEnds", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = p.DateEnds });

            }

           
                con.Open();
                UpdateIsSuccessful = Convert.ToBoolean((int)cmd.ExecuteNonQuery());
          
            
                con.Close();
            
        }

        return UpdateIsSuccessful;
    }

    public static int ApplyDiscountsOrPromotionsToGridView(bool IsDiscount, DataTable dtPromotionOrDiscount)
    {

        string CS = ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString;
        int ReturnCount = 0;

        using (SqlConnection con = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("spLinkProductToDiscountOrPromotion", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@blnIsDiscount", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input, Value = IsDiscount });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@dtPromotionOrDiscountType", SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input, Value = dtPromotionOrDiscount });

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


    
}
