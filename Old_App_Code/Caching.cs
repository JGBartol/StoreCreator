using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Caching;
using System.Threading;
using System.Messaging;


public class Caching
{
    public static void InsertCacheExpiry()
    {


    }

    private static string strSqlConnection
    {
        get { return ConfigurationManager.ConnectionStrings["StoreCreatorCS"].ConnectionString; }
    }


    public static void InsertCache(string Key, bool IsSlidingExpiration, object CacheObject , int Duration, string DurationType)
    {
       
        switch(DurationType)
        {
            case "Seconds":

                if (IsSlidingExpiration)
                {
                    HttpRuntime.Cache.Insert(Key, CacheObject, null, DateTime.MaxValue, TimeSpan.FromSeconds(Duration));
                }
                else
                {
                    HttpRuntime.Cache.Insert(Key, CacheObject, null, DateTime.Now.AddSeconds(Duration), TimeSpan.Zero);
                }

                break;

              case "Minutes":

                if (IsSlidingExpiration)
                {
                    HttpRuntime.Cache.Insert(Key, CacheObject, null, DateTime.MaxValue, TimeSpan.FromSeconds(Duration));
                }                
                else
                {
                    HttpRuntime.Cache.Insert(Key, CacheObject, null, DateTime.Now.AddMinutes(Duration), TimeSpan.Zero);
                }

                break;

              case "Days":

                if (IsSlidingExpiration)
                {
                    HttpRuntime.Cache.Insert(Key, CacheObject, null, DateTime.MaxValue, TimeSpan.FromSeconds(Duration));
                }
                else
                {
                    HttpRuntime.Cache.Insert(Key, CacheObject, null, DateTime.Now.AddDays(Duration), TimeSpan.Zero);
                }

                break;
             case "Months":

                if (IsSlidingExpiration)
                {
                    HttpRuntime.Cache.Insert(Key, CacheObject, null, DateTime.MaxValue, TimeSpan.FromSeconds(Duration));
                }
                else
                {
                    HttpRuntime.Cache.Insert(Key, CacheObject, null, DateTime.Now.AddMonths(Duration), TimeSpan.Zero);
                }

                break;
              case "Years":

                if (IsSlidingExpiration)
                {
                    HttpRuntime.Cache.Insert(Key, CacheObject, null, DateTime.MaxValue, TimeSpan.FromSeconds(Duration));
                }
                else
                {
                    HttpRuntime.Cache.Insert(Key, CacheObject, null, DateTime.Now.AddYears(Duration), TimeSpan.Zero);
                }

                break;

            default:

                break;

        }
    }


    private enum DurationType
    {
        Seconds,
        Minutes,
        Days,
        Months,
        Years,
    }

    public static void InsertAbsoluteCacheWithPriority(string key, object obj, int seconds, CacheItemPriority priority)
    {
        HttpRuntime.Cache.Insert(key, obj, null, DateTime.Now.AddSeconds(seconds), TimeSpan.Zero, null);

    }

    public static bool IsCacheObjectInCache(string Key)
    {
        bool IsInCache = false;

        foreach (DictionaryEntry entry in HttpRuntime.Cache)
        {
            if (Key == entry.Key.ToString())
            {
                return true;
            }                    
        }

        return IsInCache;
    }

    public static void CreateCacheDependency(string SqlStatement, string TableName, bool IsStoredProcedure)
    {
        SqlConnection con = new SqlConnection(strSqlConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        if (IsStoredProcedure)
        {
            cmd.CommandType = CommandType.StoredProcedure;
        }
        else
        {
            cmd.CommandType = CommandType.Text;
        }

        cmd.CommandText = SqlStatement;

        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, TableName);

            SqlCacheDependency dep = new SqlCacheDependency(cmd);

            HttpRuntime.Cache.Insert(TableName, ds, dep);

            SqlDependency.Start(con.ConnectionString);
        }
        catch(Exception error)
        {
            throw new ApplicationException(error.Message);
        }
    }

    public static void CreateCacheDepen(string SqlStatement, string TableName)
    {
        SqlConnection con = new SqlConnection(strSqlConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = SqlStatement;
        DataSet ds = new DataSet();
        
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(ds);

        SqlCacheDependency dep = new SqlCacheDependency(cmd);

        HttpRuntime.Cache.Insert(TableName, ds, dep);

        SqlDependency.Start(con.ConnectionString);
    }




    public static DataSet CreateCacheDataSet(string Sproc)
    {

        SqlConnection con = new SqlConnection(strSqlConnection);
      
        using (con)
        {
             SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;   
            cmd.CommandText = Sproc;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            SqlCacheDependency dep = new SqlCacheDependency(cmd);

            HttpRuntime.Cache.Insert("tbl", ds);

            SqlDependency.Start(con.ConnectionString);

            return ds;

        }

    }

    public static void GetCacheItem(string key, object objToRetrieve)
    {
        if (HttpRuntime.Cache[key] != null)
        {
            

        }

    }

    #region CustomCacheDependencies

    public class TimerDependency : System.Web.Caching.CacheDependency
    {
        private System.Threading.Timer timer;
        private int PollTime = 5000;
        private int Count = 0;

        public TimerDependency()
        {
            timer = new System.Threading.Timer(new System.Threading.TimerCallback(CheckDep), this, 0, PollTime);

        }

        private void CheckDep(object sender)
        {
            Count++;

            if (Count > 4)
            {
                base.NotifyDependencyChanged(this, EventArgs.Empty);

                timer.Dispose();
            }

        }

        protected override void DependencyDispose()
        {
            if (timer != null)
            {
                timer.Dispose();
            }
        }
    }





    public class MessageQueueCacheDep : CacheDependency
    {
        private MessageQueue queue;

        public MessageQueueCacheDep(string QueueName)
        {
            WaitCallback callback = new WaitCallback(WaitForMessage);
            ThreadPool.QueueUserWorkItem(callback);
        }

        private void WaitForMessage(object state)
        {
            Message msg = queue.Receive();
            base.NotifyDependencyChanged(this, EventArgs.Empty);
        }

        private string QueueName;
    }

    public class CacheDependencyOnOtherObjectFiles
    {
        public static void CreateAggregateDependency(string DepCacheKey, DataSet ds, string DepFile, string FileTwo)
        {

        CacheDependency dep = new CacheDependency(System.Web.HttpContext.Current.Server.MapPath(DepFile));
        CacheDependency dep2 = new CacheDependency(System.Web.HttpContext.Current.Server.MapPath(FileTwo));

        CacheDependency[] deps = new CacheDependency[] { dep, dep2 };

        AggregateCacheDependency agg = new AggregateCacheDependency();
        agg.Add(deps);

        HttpRuntime.Cache.Insert(DepCacheKey, ds, agg);

        }
    }

    public class ItemRemovedCallBack
    {
        public static object ItemOne;
        private static object ItemTwo;
        public static bool IsSliding;
        public static int Seconds;
        public static string KeyDependenyOn;
        public static string KeyTwo;

        public static void InsertIntoCache()
        {
            if (IsSliding)
            {
                HttpRuntime.Cache.Insert(KeyDependenyOn, ItemOne, null, DateTime.MaxValue, TimeSpan.FromSeconds(Seconds), CacheItemPriority.Default, new CacheItemRemovedCallback(ItemRemovedCallback));
                HttpRuntime.Cache.Insert(KeyTwo, ItemTwo, null, DateTime.MaxValue, TimeSpan.FromSeconds(Seconds), CacheItemPriority.Default, new CacheItemRemovedCallback(ItemRemovedCallback));
                HttpRuntime.Cache.Insert(KeyTwo, ItemTwo, null, DateTime.MaxValue, TimeSpan.FromDays(Seconds), CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(ItemRemovedCallback));
            }
            else
            {
                HttpRuntime.Cache.Insert(KeyTwo, ItemTwo, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(ItemRemovedCallback));
            }
        }

        private static void ItemRemovedCallback(string key, object obj, CacheItemRemovedReason reason)
        {
            if (reason == CacheItemRemovedReason.DependencyChanged)
            {
                if (key == KeyDependenyOn || key == KeyTwo)
                {
                    HttpRuntime.Cache.Remove(KeyDependenyOn);
                    HttpRuntime.Cache.Remove(KeyTwo);
                }
            }
        }

        private static void ItemRemoveCallBackReason(string key, object obj, CacheItemRemovedReason reason)
        {
            switch (reason)
            {
                case CacheItemRemovedReason.DependencyChanged:


                    break;

                case CacheItemRemovedReason.Expired:


                    break;

                case CacheItemRemovedReason.Removed:


                    break;

                case CacheItemRemovedReason.Underused:


                    break;

                default:

                    throw new NotImplementedException();

            }

        }

        public class SetCacheability
        {
            public static void SetCache()
            {
                HttpResponse res = HttpContext.Current.Response;
                res.Cache.SetCacheability(HttpCacheability.Public);
                res.Cache.SetExpires(DateTime.Now.AddSeconds(60));

                res.Cache.SetValidUntilExpires(true);

            }


        }

    #endregion


    }

}
