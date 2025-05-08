using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data; // Lấy file cấu hình->References


namespace CSDLWinform
{
    public static class DataProvider
    {
        public static SqlConnection cn;
        public static SqlDataAdapter da;

        public static SqlCommand command;


        //Phuong thuc mo ket noi den co so du lieu
        public static void moketNoi()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["QLNhanSu"].ConnectionString);
            cn.Open();
        }

        public static void dongketnoi()
        {
            if (cn != null && cn.State == ConnectionState.Open)
                cn.Close();
        }

        public static DataTable getTable(string sql)
        {
            da = new SqlDataAdapter(sql, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
 
        }
        // Phương thức cập nhật dữ liệu   // Truyền câu lệnh truy vấn // Mảng các control // các trường dưới database(tên cột)
        public static void upDateData(string sql, Object[] value = null, string[] name = null) // Thông tin đưa vào là các câu lệnh và các box lưu dưới dạng mảng, dưới database sẽ có các trường 
        {// = null vì nếu delete thì 2 tham số ở sau không cần vì không cần các mảng và tên cột

            command = new SqlCommand(sql, cn);
            command.Parameters.Clear();
            if (value != null)
            {
                for(int i = 0; i < value.Length; i++)
                {
                    command.Parameters.AddWithValue(name[i], value[i]); // Tham so hóa để dữ liệu khớp với nhau (match với nhau) (Kiểu số nguyên trên nhập = kiểu số nguyên dưới database)
                }
            }
            command.ExecuteNonQuery(); //Thực thi câu lệnh khi truy vấn 
            command.Dispose(); // giải phóng
        }

    }
}
