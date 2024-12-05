using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTQL_Ban_Giay_Dep
{
    public partial class FormQuanLy : Form
    {
        string MANV;
        SqlConnection conn = new SqlConnection();
        Ham fun = new Ham();
        public FormQuanLy(string mnv)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            fun.KetNoi(conn);
            MANV = mnv;
            string query = "Select HOTENNV from NHAN_VIEN where MANV = '" + MANV + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string user = "";
            if (reader.Read())
            {
                user = reader["HOTENNV"].ToString();
            }
            reader.Close();
            conn.Close();
            label7.Text = "Hello, " + user;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLy_KhachHang ql_kh = new FormQuanLy_KhachHang();
            ql_kh.Show();
            ql_kh.FormClosed += (s, args) => this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLy_NhanVien ql_nv = new FormQuanLy_NhanVien();
            ql_nv.Show();
            ql_nv.FormClosed += (s, args) => this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLy_LoaiSanPham lsp = new FormQuanLy_LoaiSanPham();
            lsp.Show();
            lsp.FormClosed += (s, args) => this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLy_SanPham ql_sp = new FormQuanLy_SanPham();
            ql_sp.Show();
            ql_sp.FormClosed += (s, args) => this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormThongKe tk = new FormThongKe();
            tk.Show();
            tk.FormClosed += (s, args) => this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDangNhap dn = new FormDangNhap();
            dn.Show();
            dn.FormClosed += (s, args) => this.Close();
        }
    }
}
