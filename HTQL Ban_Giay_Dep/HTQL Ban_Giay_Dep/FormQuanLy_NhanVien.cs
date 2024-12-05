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
using System.Text.RegularExpressions;

namespace HTQL_Ban_Giay_Dep
{
    public partial class FormQuanLy_NhanVien : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham fun = new Ham();
        public FormQuanLy_NhanVien()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        private void FormQuanLy_NhanVien_Load(object sender, EventArgs e)
        {
            fun.KetNoi(conn);
            string query = "Select MANV, HOTENNV, VAITRO, FORMAT(NGAYSINH, 'dd/MM/yyyy') AS NGAYSINH, GIOITINH, DIACHI, SDT, MATKHAU from NHAN_VIEN";
            fun.HienThiDG(dataGridView1, query, conn);
            textBox1.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLy_KhachHang ql_kh = new FormQuanLy_KhachHang();
            ql_kh.Show();
            ql_kh.FormClosed += (s, args) => this.Close();
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

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDangNhap dn = new FormDangNhap();
            dn.Show();
            dn.FormClosed += (s, args) => this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormThongKe tk = new FormThongKe();
            tk.Show();
            tk.FormClosed += (s, args) => this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string mnv = textBox1.Text;
            string ht = "";
            string pattern = @"^[a-zA-ZÀ-ỹ0-9\s]+$";
            if (Regex.IsMatch(textBox2.Text, pattern))
            {
                ht = textBox2.Text;
                string mk = textBox4.Text;
                string vt = comboBox1.Text;
                string gt;
                if (radioButton1.Checked)
                {
                    gt = "Nam";
                }
                else
                {
                    gt = "Nữ";
                }
                string ns = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string dc = "";
                string pattern2 = @"^[a-zA-ZÀ-ỹ0-9\s,]+$"; // Chỉ cho phép chữ cái, số và khoảng trắng
                if (Regex.IsMatch(textBox6.Text, pattern2))
                {
                    dc = textBox6.Text;
                    string sdt = textBox3.Text;
                    if (textBox3.Text.Length == 10 && (int.TryParse(sdt, out int sodienthoai)))
                    {
                        string query = "insert into NHAN_VIEN values ('" + mnv + "', N'" + ht + "', N'" + vt + "', '" + ns + "', N'" + gt + "', N'" + dc + "', '" + sdt + "', '" + mk + "')";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        fun.HienThiDG(dataGridView1, "Select * from NHAN_VIEN", conn);
                        MessageBox.Show("Thêm nhân viên mới thành công");
                        button10_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập số điện thoại không chứa ký tự alphabet");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ không chứa ký tự đặc biệt");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng không nhập số và ký tự đặc biệt vào họ tên");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button7.Enabled = false;
            textBox1.Enabled = false;
            textBox4.UseSystemPasswordChar = true;
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            string gioitinh = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            if (gioitinh == "Nam")
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string mnv = textBox1.Text;
            string ht = "";
            string pattern = @"^[a-zA-ZÀ-ỹ0-9\s]+$";
            if (Regex.IsMatch(textBox2.Text, pattern))
            {
                ht = textBox2.Text;
                string mk = textBox4.Text;
                string vt = comboBox1.Text;
                string gt;
                if (radioButton1.Checked)
                {
                    gt = "Nam";
                }
                else
                {
                    gt = "Nữ";
                }
                string ns = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string dc = "";
                string pattern2 = @"^[a-zA-ZÀ-ỹ0-9\s,]+$"; ; // Chỉ cho phép chữ cái, số và khoảng trắng
                if (Regex.IsMatch(textBox6.Text, pattern2))
                {
                    dc = textBox6.Text;
                    string sdt = textBox3.Text;
                    if (textBox3.Text.Length == 10 && (int.TryParse(sdt, out int sodienthoai)))
                    {
                        string query = "Update NHAN_VIEN set HOTENNV = N'" + ht + "', VAITRO = N'" + vt + "', NGAYSINH = '" + ns + "', GIOITINH = N'" + gt + "', DIACHI = N'" + dc + "', SDT = '" + sdt + "', MATKHAU = '" + mk + "' where MANV = '" + mnv + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        fun.HienThiDG(dataGridView1, "Select * from NHAN_VIEN", conn);
                        MessageBox.Show("Sửa nhân viên thành công");
                        button10_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập số điện thoại không chứa ký tự alphabet");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ không chứa ký tự đặc biệt");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập họ tên không chứa số và ký tự đặc biệt");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Hiện mật khẩu
                textBox4.UseSystemPasswordChar = false;
            }
            else
            {
                // Ẩn mật khẩu
                textBox4.UseSystemPasswordChar = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string mnv = textBox1.Text;
            string query = "Delete from NHAN_VIEN where MANV = '" + mnv + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            fun.HienThiDG(dataGridView1, "Select * from NHAN_VIEN", conn);
            MessageBox.Show("Xóa nhân viên thành công");
            textBox2.Text = string.Empty;
            textBox4.Text = string.Empty;
            comboBox1.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox3.Text = string.Empty;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox4.Text = string.Empty;
            comboBox1.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox3.Text = string.Empty;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker1.Text = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button7.Enabled = true;
            textBox2.Text = string.Empty;
            textBox4.Text = string.Empty;
            comboBox1.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox3.Text = string.Empty;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker1.Text = null;
            string max_mnv = "Select max(substring(MANV, 3, 4))+1 from NHAN_VIEN";
            SqlCommand cmd = new SqlCommand(max_mnv, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int new_mnv = int.Parse(reader[0].ToString());
                if (new_mnv < 10)
                    textBox1.Text = "NV00" + new_mnv;
                else if (new_mnv < 100)
                    textBox1.Text = "NV0" + new_mnv;
                else
                    textBox1.Text = "NV" + new_mnv;
            }
            reader.Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) & !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button7_Click(sender, e);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date > DateTime.Now || dateTimePicker1.Value.Date < new DateTime(1924, 1, 1))
            {
                MessageBox.Show("Không thể chọn ngày lớn hơn hôm nay hoặc năm nhỏ hơn 1924");
            }
        }
    }
}
