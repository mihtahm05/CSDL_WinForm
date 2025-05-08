using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CSDLWinform
{
    public partial class Form1 : Form
    {
        private DataSet ds;
        private SqlDataAdapter da;

        public Form1()
        {
            InitializeComponent();
            Load += new EventHandler(Form1_Load);

            btnThem.Click += new EventHandler(btnThem_Click);
            btnXoa.Click += new EventHandler(btnXoa_Click);
            btnSua.Click += new EventHandler(btnSua_Click);
            dataGridView2.CellClick += new DataGridViewCellEventHandler(Data_Click);
        }

       

        //private void textBox3_TextChanged(object sender, EventArgs e)
        //{
        //}

        //private void txtHoTen_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void button3_Click(object sender, EventArgs e)
        //{

        //}



        private void Form1_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            loadPB();
            loadCV();
            loadNhanVien();
         
        }

        private void Data_Click(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView2.CurrentCell.RowIndex;
            txtMaSV.Text = dataGridView2.Rows[i].Cells[0].Value.ToString();
            txtHoTen.Text = dataGridView2.Rows[i].Cells[1].Value.ToString();
            dpNgaySinh.Text = dataGridView2.Rows[i].Cells[2].Value.ToString();


            string gt = dataGridView2.Rows[i].Cells[3].Value.ToString();
            if (gt == "True")
            {
                rdNam.Checked = true;
            }
            else
            {
                rdNu.Checked = true;
            }
            txtsdt.Text = dataGridView2.Rows[i].Cells[4].Value.ToString();
            txthesoluong.Text = dataGridView2.Rows[i].Cells[5].Value.ToString();

            cbotenphong.SelectedValue = dataGridView2.Rows[i].Cells[6].Value.ToString();
            cbochucvu.SelectedValue = dataGridView2.Rows[i].Cells[7].Value.ToString(); ;

        }

        public void loadPB()
        {
            DataProvider.moketNoi();
            string sql = "SELECT * FROM DMPHONG";
            da = new SqlDataAdapter(sql, DataProvider.cn);
            if (ds.Tables.Contains("DMPHONG"))
                ds.Tables["DMPHONG"].Clear();

            da.Fill(ds, "DMPHONG");

            cbotenphong.DataSource = ds.Tables["DMPHONG"];
            cbotenphong.DisplayMember = "TenPhong";
            cbotenphong.ValueMember = "MaPhong";

            DataProvider.dongketnoi();
        }
        public void loadCV()
        {
            DataProvider.moketNoi();
            string sql = "SELECT * FROM CHUCVU";
            da = new SqlDataAdapter(sql, DataProvider.cn);

            if (ds.Tables.Contains("CHUCVU"))
                ds.Tables["CHUCVU"].Clear();

            da.Fill(ds, "CHUCVU");

            cbochucvu.DataSource = ds.Tables["CHUCVU"];
            cbochucvu.DisplayMember = "TenChucVu";
            cbochucvu.ValueMember = "MaChucVu";
        }

        public void loadNhanVien()
        {
            DataProvider.moketNoi();
            string sql = "SELECT * FROM NhanVien";
            da = new SqlDataAdapter(sql, DataProvider.cn);

            if (ds.Tables.Contains("NhanVien"))
                ds.Tables["NhanVien"].Clear();

            da.Fill(ds, "NhanVien");
            dataGridView2.DataSource = ds.Tables["NhanVien"];
            DataProvider.dongketnoi();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO NhanVien(Hoten, NgaySinh, GioiTinh, HeSoLuong, MaPhong, MaChucVu) " +
                "VALUES(@ht, @ns, @gt, @hsl, @maP, @maCV)";

            bool gt = rdNam.Checked ? true : false;

            object[] value = {
             txtHoTen.Text,
             dpNgaySinh.Value,
             gt,
            float.Parse(txthesoluong.Text),
            cbotenphong.SelectedValue.ToString(),
             cbochucvu.SelectedValue.ToString()
         };

            string[] name = { "@ht", "@ns", "@gt", "@hsl", "@maP", "@maCV" };
            DataProvider.moketNoi();
            DataProvider.upDateData(sql, value, name);
            MessageBox.Show("Thêm thành công!");
            
            DataProvider.dongketnoi();
            loadNhanVien();
        }
        // Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            int ma = Convert.ToInt32(txtMaSV.Text);
            string sql = string.Format("UPDATE NhanVien SET Hoten = @ht, " +
              "NgaySinh = @ns, " +
              "GioiTinh = @gt, " +
              "HeSoLuong = @hsl, " +
              "MaPhong = @maP, " +
              "MaChucVu = @maCV WHERE MaNV = '{0}'", ma);


            bool gt = rdNam.Checked ? true : false;

            object[] value = {
             txtHoTen.Text,
             dpNgaySinh.Value,
             gt,
            float.Parse(txthesoluong.Text),
            cbotenphong.SelectedValue.ToString(),
             cbochucvu.SelectedValue.ToString()
            };

            string[] name = { "@ht", "@ns", "@gt", "@hsl", "@maP", "@maCV" };
            DataProvider.moketNoi();
            DataProvider.upDateData(sql, value, name);
           
            DataProvider.dongketnoi();
            loadNhanVien();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) {
                int i = dataGridView2.CurrentCell.RowIndex;
                int ma = Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString());

                string sql = string.Format("Delete FROM NhanVien WHERE MaNV = '{0}'", ma);
                DataProvider.moketNoi();
                DataProvider.upDateData(sql);
                MessageBox.Show("Đã xóa thành công!");
                loadNhanVien();
                DataProvider.dongketnoi();
            }
        }
    }
}
