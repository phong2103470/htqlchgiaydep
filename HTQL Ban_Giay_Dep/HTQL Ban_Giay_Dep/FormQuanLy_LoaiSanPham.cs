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
    public partial class FormQuanLy_LoaiSanPham : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham fun = new Ham();
        public FormQuanLy_LoaiSanPham()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        private void FormQuanLy_LoaiSanPham_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            fun.KetNoi(conn);
            fun.HienThiDG(dataGridView1, "Select * from LOAI_SAN_PHAM", conn);
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
            button8.Enabled = true;
            string max_ml = "Select max(substring(MALOAI, 4, 4))+1 from LOAI_SAN_PHAM";
            SqlCommand cmd = new SqlCommand(max_ml, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int new_ml = int.Parse(reader[0].ToString());
                if (new_ml < 10)
                    textBox1.Text = "LSP0" + new_ml;
                else
                    textBox1.Text = "LSP" + new_ml;
            }
            reader.Close();
            textBox2.Text = string.Empty;
            richTextBox1.Text = string.Empty;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string ml = textBox1.Text;
            string tl = "";
            string pattern = @"^[a-zA-ZÀ-ỹ0-9\s,]+$";
            if (Regex.IsMatch(textBox2.Text, pattern))
            {
                tl = textBox2.Text;
                string mt = richTextBox1.Text;
                string query = "Insert into LOAI_SAN_PHAM values('" + ml + "', N'" + tl + "', N'" + mt + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                fun.HienThiDG(dataGridView1, "Select * from LOAI_SAN_PHAM", conn);
                MessageBox.Show("Thêm loại sản phẩm mới thành công");
                button11_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên loại không chứa ký tự đặc biệt");
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button8.Enabled = false;
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string ml = textBox1.Text;
            string tl = textBox2.Text;
            string mt = richTextBox1.Text;
            string query = "Update LOAI_SAN_PHAM set TENLOAI = N'" + tl + "', MOTA = N'" + mt + "' where MALOAI = '" + ml + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            fun.HienThiDG(dataGridView1, "Select * from LOAI_SAN_PHAM", conn);
            MessageBox.Show("Sửa thông tin sản phẩm thành công");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string ml = textBox1.Text;
            string query = "Delete from LOAI_SAN_PHAM where MALOAI = '" + ml + "'";
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            fun.HienThiDG(dataGridView1, "Select * from LOAI_SAN_PHAM", conn);
            button11_Click(sender, e);
            MessageBox.Show("Xóa loại sản phẩm thành công");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            richTextBox1.Text = string.Empty;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button8_Click(sender, e);
            }
        }
    }
}
