using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HTQL_Ban_Giay_Dep
{
    public partial class FormDangNhap : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham fun = new Ham();
        public static string MaNV;
        public FormDangNhap()
        {
            InitializeComponent();
        }
        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            fun.KetNoi(conn);
            textBox2.UseSystemPasswordChar = true;
        }
        public void loginWithAdmin()
        {
            string userName = textBox1.Text;
            string passWord = textBox2.Text;
            Boolean loginSuccess = false;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                MessageBox.Show("Không được để trống");
            }
            else
            {
                string query = "Select MANV, MATKHAU from NHAN_VIEN where VAITRO = N'" + "Quản lý" + "'";
                SqlCommand comd = new SqlCommand(query, conn);
                SqlDataReader reader = comd.ExecuteReader();
                List<Tuple<string, string>> danhSachTaiKhoan = new List<Tuple<string, string>>();
                while (reader.Read())
                {
                    string MANV = reader["MANV"].ToString();
                    string MATKHAU = reader["MATKHAU"].ToString();
                    danhSachTaiKhoan.Add(new Tuple<string, string>(MANV, MATKHAU));
                }

                foreach (var item in danhSachTaiKhoan)
                {
                    if (userName == item.Item1 && passWord == item.Item2)
                    {
                        loginSuccess = true;
                        this.Hide();
                        FormQuanLy ql = new FormQuanLy(textBox1.Text);
                        ql.Show();
                        ql.FormClosed += (s, args) => this.Close();
                        break;
                    }
                }
                reader.Close();
                if (loginSuccess == false)
                {
                    MessageBox.Show("Không đúng tài khoản hoặc mật khẩu. Vui lòng nhập lại!");
                }
            }
        }

        public void loginWithSaler()
        {
            string userName = textBox1.Text;
            string passWord = textBox2.Text;
            Boolean loginSuccess = false;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                MessageBox.Show("Không được để trống");
            }
            else
            {
                string query = "Select MANV, MATKHAU from NHAN_VIEN where VAITRO = N'" + "Bán hàng" + "'";
                SqlCommand comd = new SqlCommand(query, conn);
                SqlDataReader reader = comd.ExecuteReader();
                List<Tuple<string, string>> danhSachTaiKhoan = new List<Tuple<string, string>>();
                while (reader.Read())
                {
                    string MANV = reader["MANV"].ToString();
                    string MATKHAU = reader["MATKHAU"].ToString();
                    danhSachTaiKhoan.Add(new Tuple<string, string>(MANV, MATKHAU));
                }

                foreach (var item in danhSachTaiKhoan)
                {
                    if (userName == item.Item1 && passWord == item.Item2)
                    {
                        loginSuccess = true;
                        // Lưu mã nhân viên vào biến tĩnh
                        FormDangNhap.MaNV = item.Item1; // Lưu mã nhân viên
                        this.Hide();
                        FormNhanVien nv = new FormNhanVien(textBox1.Text);
                        nv.Show();
                        nv.FormClosed += (s, args) => this.Close();
                        break;
                    }
                }
                if (loginSuccess == false)
                {
                    MessageBox.Show("Không đúng tài khoản hoặc mật khẩu. Vui lòng nhập lại!");
                }
                reader.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                loginWithAdmin();
            }
            else
            {
                loginWithSaler();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (checkBox2.Checked)
                {
                    loginWithAdmin();
                }
                else
                {
                    loginWithSaler();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Hiện mật khẩu
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                // Ẩn mật khẩu
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /*private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }*/
    }
}
