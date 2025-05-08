using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSDLWinform
{
  public static class DataProvider2
    {
        public static SqlConnection cn;
        public static SqlDataAdapter da;
        public static SqlCommand cmd;

        public static void  moketnoi()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["QLNhanSu"].ConnectionString);
            cn.Open();
        }

        public static void dongketnoi()
        {
            cn.Close();
        }

        public static DataTable getTable(string sql)
        {
            da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


    }

}
