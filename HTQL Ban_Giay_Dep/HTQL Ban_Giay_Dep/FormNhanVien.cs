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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HTQL_Ban_Giay_Dep
{
    public partial class FormNhanVien : Form
    {
        string MANV;
        SqlConnection conn = new SqlConnection();
        Ham fun = new Ham();
        public FormNhanVien(string mnv)
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
            FormQuanLy_KH_NV ql_kh = new FormQuanLy_KH_NV();
            ql_kh.Show();
            ql_kh.FormClosed += (s, args) => this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLapHoaDon lap_hd = new FormLapHoaDon();
            lap_hd.Show();
            lap_hd.FormClosed += (s, args) => this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormTimHoaDon tim_hd = new FormTimHoaDon();
            tim_hd.Show();
            tim_hd.FormClosed += (s, args) => this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDangNhap dn = new FormDangNhap();
            dn.Show();
            dn.FormClosed += (s, args) => this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
