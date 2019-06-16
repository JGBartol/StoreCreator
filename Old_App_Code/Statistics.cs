using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Web.Management;
using System.Web.SessionState;

public class Statistics
{


    private static string strSqlConnection
    {
        get { return ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString; }
    }


    public static void InsertMemberPageVisit(string PageUrl, int UserId, string QueryStringId)
    {
        SqlConnection con = new SqlConnection(strSqlConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "spInsertProductVisit";
       
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter[] parameters = new SqlParameter[3];
        parameters[0] = new SqlParameter() { ParameterName = "@PageUrl", SqlDbType = SqlDbType.NVarChar, Value = PageUrl };
        parameters[1] = new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.Int, Value = UserId };     
        parameters[2] = new SqlParameter() { ParameterName = "@QueryStringId", SqlDbType = SqlDbType.NVarChar, Value = QueryStringId };

        for (int i = 0; i < parameters.Length; i++)
        {
            cmd.Parameters.Add(parameters[i]);
        }
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (SqlException exc)
        {

        }
        finally
        {
            con.Close();
        }
    }

    public static int GetPageVisits(string PageUrl, string QueryString)
    {
        SqlConnection con = new SqlConnection(strSqlConnection);
        SqlCommand cmd = new SqlCommand("spGetPageVisitCount", con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter() { ParameterName = "@PageUrl", SqlDbType = SqlDbType.NVarChar, Value = PageUrl };
        parameters[1] = new SqlParameter() { ParameterName = "@QueryStringId", SqlDbType = SqlDbType.NVarChar, Value = QueryString };

        for (int i = 0; i < parameters.Length; i++)
        {
            cmd.Parameters.Add(parameters[i]);
        }

        try
        {
            con.Open();
            return (int)cmd.ExecuteScalar();
        }
        catch (SqlException exc)
        {
            throw new Exception(exc.Message);
        }
        finally
        {
            con.Close();
        }
    }

    public static void InsertProductVisitedCookie(string ProductCategory, string Gender, decimal Price)
    {


        HttpCookie cookie = new HttpCookie("ProductVisited");
       
            cookie.Values.Add("ProductCategory", ProductCategory);
            cookie.Values.Add("Gender", Gender);
            cookie.Values.Add("Price", Price.ToString());
             
        HttpContext.Current.Response.SetCookie(cookie);


    }

    public static void AddLastLogin(string UserName, DateTime DateLastLogin)
    {


    }

 
}

