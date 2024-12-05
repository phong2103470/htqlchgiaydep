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

namespace HTQL_Ban_Giay_Dep
{
    public partial class FormTimHoaDon : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham func = new Ham();
        public FormTimHoaDon()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
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

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDangNhap dn = new FormDangNhap();
            dn.Show();
            dn.FormClosed += (s, args) => this.Close();
        }

        private void FormTimHoaDon_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra xem phím nhấn có phải là Enter không
            if (e.KeyCode == Keys.Enter)
            {
                // Gọi phương thức để tìm thông tin hành khách
                FindCustomerInfo(textBox1.Text);
                e.SuppressKeyPress = true; // Ngăn chặn âm thanh "ding" khi nhấn Enter
            }
        }
        private void FindCustomerInfo(string phoneNumber)
        {
            string query = "SELECT * FROM KHACH_HANG WHERE SDT = '" + textBox1.Text + "'";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {

                try
                {

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Kiểm tra nếu có dữ liệu
                    if (reader.Read())
                    {
                        // Hiển thị thông tin hành khách vào các TextBox khác (giả sử bạn có các TextBox để hiển thị thông tin)
                        textBox2.Text = reader["MAKH"].ToString();
                        textBox3.Text = reader["HOTENKH"].ToString();
                        textBox4.Text = reader["DIACHI"].ToString();

                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin khách hàng với số điện thoại này.");
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi lấy dữ liệu: " + ex.Message);
                }

            }



        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "select MAHD, HOTENNV, FORMAT(NGAYLAP, 'dd/MM/yyyy') AS NGAYLAP, TONGTIEN  from HOA_DON h, NHAN_VIEN n where h.MANV = n.MANV and h.MAKH = '" + textBox2.Text + "'";
            func.HienThiDG(dataGridView1, sql, conn);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) & !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
