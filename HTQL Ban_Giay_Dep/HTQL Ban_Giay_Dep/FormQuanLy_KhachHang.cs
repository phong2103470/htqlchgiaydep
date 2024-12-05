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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace HTQL_Ban_Giay_Dep
{
    public partial class FormQuanLy_KhachHang : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham func = new Ham();
        SqlCommand cmd = new SqlCommand();
        public FormQuanLy_KhachHang()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
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

        private void button6_Click_1(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void FormQuanLy_KhachHang_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);
            func.HienThiDG(dataGridView1, "select MAKH, HOTENKH, FORMAT(NGAYSINH, 'dd/MM/yyyy') as NGAYSINH, GIOITINH, DIACHI, SDT from KHACH_HANG", conn);
            conn.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button12.Enabled = true;
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox6.Text = String.Empty;
            dateTimePicker1.Text = null;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button12.Enabled = true;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string max_ms = "select MAX(substring(makh,3,3))+1 from KHACH_HANG;";
            SqlCommand comd = new SqlCommand(max_ms, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read())
            {
                int new_ms = int.Parse(reader[0].ToString());
                if (new_ms < 10)
                    textBox1.Text = "KH00" + new_ms;
                else if (new_ms < 100)
                    textBox1.Text = "KH0" + new_ms;
                else if (new_ms < 1000)
                    textBox1.Text = "KH" + new_ms;
                else
                    MessageBox.Show("Can reset ma khach hang");
            }
            reader.Close();
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox6.Text = String.Empty;
            dateTimePicker1.Text = null;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string gioiTinh = "";
            if (radioButton1.Checked)
            {
                gioiTinh = "Nam";
            }
            else if (radioButton2.Checked)
            {
                gioiTinh = "Nữ";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn giới tính.");
                return;  // Thoát khỏi hàm nếu không chọn giới tính
            }
            string sql = "update KHACH_HANG set hotenkh = N'" + textBox2.Text + "',ngaysinh = '" + dateTimePicker1.Value + "', gioitinh = N'" + gioiTinh + "', diachi = N'" + textBox6.Text + "', sdt = '" + textBox3.Text + "' where makh = '" + textBox1.Text + "'";
            SqlCommand comd_them = new SqlCommand(sql, conn);
            comd_them.ExecuteNonQuery();
            MessageBox.Show("Sửa khách hàng thành công");
            func.HienThiDG(dataGridView1, "select makh, hotenkh, FORMAT(NGAYSINH, 'dd/MM/yyyy') as NGAYSINH, gioitinh, diachi, sdt from KHACH_HANG", conn);
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button12.Enabled = false;
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            if (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() == "Nam")
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "delete KHACH_HANG where makh = '" + textBox1.Text + "'";
            SqlCommand comd = new SqlCommand(sql, conn);
            comd.ExecuteNonQuery();
            MessageBox.Show("Xoá khách hàng thành công");
            func.HienThiDG(dataGridView1, "select makh, hotenkh, FORMAT(NGAYSINH, 'dd/MM/yyyy') as NGAYSINH, gioitinh, diachi, sdt from KHACH_HANG", conn);
            conn.Close();
        }
        private void button12_click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string gioiTinh = "";
            if (radioButton1.Checked)
            {
                gioiTinh = "Nam";
            }
            else if (radioButton2.Checked)
            {
                gioiTinh = "Nữ";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn giới tính.");
                return;  // Thoát khỏi hàm nếu không chọn giới tính
            }
            // Ràng buộc tên khách hàng chỉ chứa chữ và khoảng trắng (bao gồm tiếng Việt có dấu)
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, @"^[\p{L}\s]+$"))
            {
                MessageBox.Show("Tên khách hàng không hợp lệ, chỉ sử dụng chữ cái và khoảng trắng.");
                textBox2.Focus();
                return;
            }

            // Ràng buộc số điện thoại chỉ chứa đúng 10 số
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, @"^[0-9]{10}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ, vui lòng nhập đúng 10 chữ số.");
                textBox3.Focus();
                return;
            }
            /*bool hopLe = false;
            int sdt;
            while (!hopLe)
            {
                if (int.TryParse(textBox3.Text, out sdt))
                {
                    if (textBox3.Text.Length != 10)
                    {
                        MessageBox.Show("Nhập số điện thoại không hợp lệ, vui lòng nhập lại !!!");
                        textBox3.Focus(); // Đưa con trỏ về ô nhập giá
                        return;

                    }
                    else
                    {
                        hopLe = true; // Thoát khỏi vòng lặp khi giá hợp lệ
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại hợp lệ.");
                    textBox3.Focus(); // Đưa con trỏ về ô nhập giá
                    return;
                }
            }*/
            string sql = "insert into KHACH_HANG values ('" + textBox1.Text + "',N'" + textBox2.Text + "','" + dateTimePicker1.Value + "', N'" + gioiTinh + "',N'" + textBox6.Text + "','" + textBox3.Text + "')";
            SqlCommand comd_them = new SqlCommand(sql, conn);
            comd_them.ExecuteNonQuery();
            MessageBox.Show("Thêm khách hàng thành công");
            func.HienThiDG(dataGridView1, "select makh, hotenkh, FORMAT(NGAYSINH, 'dd/MM/yyyy') as NGAYSINH, gioitinh, diachi, sdt from KHACH_HANG", conn);
            conn.Close();
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date > DateTime.Now || dateTimePicker1.Value.Date < new DateTime(1924, 1, 1))
            {
                MessageBox.Show("Không thể chọn ngày lớn hơn hôm nay hoặc năm nhỏ hơn 1924");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) & !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
