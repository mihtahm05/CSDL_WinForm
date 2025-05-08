using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSDLWinform
{
    public partial class Form2: Form
    {
        SqlConnection cn;
        SqlCommand command;
        // Cách 1: Với Encrypt + TrustServerCertificate
        string str = @"Data Source=LAPTOP-ORJTNLBS\SQLSERVER;
                   Initial Catalog=QLThanhVien;
                   Integrated Security=True;
                   TrustServerCertificate=True";

        // // Cách 2: Chỉ Integrated Security
        // string str = @"Data Source=LAPTOP-ORJTNLBS\SQLSERVER;
        //                Initial Catalog=QLThanhVien;
        //                Integrated Security=True;";

        SqlDataAdapter dt = new SqlDataAdapter();
        DataTable table = new DataTable();

        void loaddata()
        {

            command = cn.CreateCommand();
            command.CommandText = "select * from ThongTinNhanVien";
            dt.SelectCommand = command;
            table.Clear();
            dt.Fill(table);
            dgv.DataSource = table;
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(str);
            cn.Open();
            loaddata();

        }

    }
}
