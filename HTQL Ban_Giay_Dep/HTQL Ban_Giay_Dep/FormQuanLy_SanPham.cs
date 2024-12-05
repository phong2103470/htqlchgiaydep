using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace HTQL_Ban_Giay_Dep
{
    public partial class FormQuanLy_SanPham : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham func = new Ham();
        SqlCommand cmd = new SqlCommand();
        string path = AppDomain.CurrentDomain.BaseDirectory;
        string sourceFilePath = "";
        public FormQuanLy_SanPham()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
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

        private void FormQuanLy_SanPham_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);
            func.HienThiDG(dataGridView1, "select MASP, MALOAI, TENSP, KICHCO, GIA, HINHANH, MOTA, TON from SAN_PHAM", conn);
            string truyvan = "SELECT maloai, tenloai FROM LOAI_SAN_PHAM";
            func.LoadComboBox(truyvan, comboBox1, conn, "tenloai", "maloai");
            comboBox1.SelectedIndex = -1;
            conn.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button13.Enabled = true;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string max_ms = "select MAX(substring(masp,3,3))+1 from SAN_PHAM;";
            SqlCommand comd = new SqlCommand(max_ms, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read())
            {
                int new_ms = int.Parse(reader[0].ToString());
                if (new_ms < 10)
                    textBox1.Text = "SP00" + new_ms;
                else if (new_ms < 100)
                    textBox1.Text = "SP0" + new_ms;
                else if (new_ms < 1000)
                    textBox1.Text = "SP" + new_ms;
                else
                    MessageBox.Show("Can reset ma san pham");
            }
            reader.Close();
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox5.Text = String.Empty;
            richTextBox1.Text = String.Empty;
            pictureBox1.Image = null;
            comboBox1.SelectedIndex = -1;
            label2.Text = String.Empty;
            textBox4.Text = String.Empty;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sourceFilePath = openFileDialog.FileName;

                // Hiển thị ảnh trong PictureBox
                pictureBox1.Image = new Bitmap(sourceFilePath);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button13.Enabled = true;
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox5.Text = String.Empty;
            richTextBox1.Text = String.Empty;
            pictureBox1.Image = null;
            comboBox1.SelectedIndex = -1;
            label2.Text = String.Empty;
            textBox4.Text = String.Empty;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string destinationFolder = path + @"\Picture";
            string destinationFilePath = System.IO.Path.Combine(destinationFolder, textBox1.Text + ".jpg");
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();  // Giải phóng hình ảnh khỏi PictureBox
                pictureBox1.Image = null;     // Đặt PictureBox không hiển thị hình ảnh
            }
            if (File.Exists(destinationFilePath))
            {
                try
                {
                    File.Delete(destinationFilePath);
                    MessageBox.Show("Xoá sản phẩm và ảnh thành công!");
                }
                catch
                {
                    MessageBox.Show("Loi khi xoa file");
                }
            }
            string sql = "delete SAN_PHAM where masp = '" + textBox1.Text + "'";
            SqlCommand comd = new SqlCommand(sql, conn);
            comd.ExecuteNonQuery();
            MessageBox.Show("Xoá sản phẩm thành công");
            func.HienThiDG(dataGridView1, "select MASP, MALOAI, TENSP, KICHCO, GIA, HINHANH, MOTA, TON from SAN_PHAM", conn);
            button11_Click(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button13.Enabled = false;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string maLoai = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string truyvan = "SELECT tenloai FROM LOAI_SAN_PHAM WHERE maloai = '" + maLoai + "'";
            SqlCommand cmd = new SqlCommand(truyvan, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Gán tên loại vào ComboBox
                comboBox1.Text = reader["tenloai"].ToString();
            }
            reader.Close();
            conn.Close();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            string imagePath = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            func.LoadImageToPictureBox1(textBox1.Text, pictureBox1);
            richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            label2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            bool hopLe = false;
            int gia;
            while (!hopLe)
            {
                if (int.TryParse(textBox5.Text, out gia))
                {
                    if (gia <= 0)
                    {
                        MessageBox.Show("Nhập giá không hợp lệ, vui lòng nhập lại !!!");
                        textBox5.Focus(); // Đưa con trỏ về ô nhập giá
                        return;

                    }
                    else
                    {
                        hopLe = true; // Thoát khỏi vòng lặp khi giá hợp lệ
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số hợp lệ.");
                    textBox5.Focus(); // Đưa con trỏ về ô nhập giá
                    return;
                }
            }
            int kichco;
            hopLe = false;
            while (!hopLe)
            {
                if (int.TryParse(textBox3.Text, out kichco))
                {
                    if (kichco <= 0)
                    {
                        MessageBox.Show("Nhập kích cỡ không hợp lệ, vui lòng nhập lại !!!");
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
                    MessageBox.Show("Vui lòng nhập số hợp lệ.");
                    textBox3.Focus(); // Đưa con trỏ về ô nhập giá
                    return;
                }
            }
            string sql = "update SAN_PHAM set maloai = '" + comboBox1.SelectedValue.ToString() + "',tensp = N'" + textBox2.Text + "', kichco = '" + textBox3.Text + "', gia = '" + textBox5.Text + "', hinhanh = '" + path + "\\Picture\\" + textBox1.Text + ".jpg" + "', mota = N'" + richTextBox1.Text + "', ton = '"+textBox4.Text+"'  where masp = '" + textBox1.Text + "'";
            SqlCommand comd = new SqlCommand(sql, conn);
            string destinationFolder = path + @"\Picture";  // Tạo thư mục 'hinh' trong thư mục project của bạn
            string destinationFilePath = System.IO.Path.Combine(destinationFolder, textBox1.Text + ".jpg");
            if (!string.IsNullOrEmpty(sourceFilePath))
            {
                // Sao chép ảnh vào thư mục 'images' của project
                File.Copy(sourceFilePath, destinationFilePath, true);
            }
            comd.ExecuteNonQuery();
            MessageBox.Show("Sửa sản phẩm thành công");
            conn.Close();
            func.HienThiDG(dataGridView1, "select MASP, MALOAI, TENSP, KICHCO, GIA, HINHANH, MOTA, TON from SAN_PHAM", conn);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            bool hopLe = false;
            int gia;
            while (!hopLe)
            {
                if (int.TryParse(textBox5.Text, out gia))
                {
                    if (gia <= 0)
                    {
                        MessageBox.Show("Nhập giá không hợp lệ, vui lòng nhập lại !!!");
                        textBox5.Focus();
                        return;
                    }
                    else
                    {
                        hopLe = true;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số hợp lệ.");
                    textBox5.Focus();
                    return;
                }
            }

            int kichco;
            hopLe = false;
            while (!hopLe)
            {
                if (int.TryParse(textBox3.Text, out kichco))
                {
                    if (kichco <= 0)
                    {
                        MessageBox.Show("Nhập kích cỡ không hợp lệ, vui lòng nhập lại !!!");
                        textBox3.Focus();
                        return;
                    }
                    else
                    {
                        hopLe = true;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số hợp lệ.");
                    textBox3.Focus();
                    return;
                }
            }

            string def_path = path + "\\Picture\\img_def.jpg"; // Đường dẫn đến ảnh mặc định
            string finalImagePath;

            // Kiểm tra xem người dùng có chọn ảnh không
            if (pictureBox1.Image != null && !string.IsNullOrEmpty(sourceFilePath))
            {
                // Đường dẫn đến thư mục và tên file ảnh mới
                finalImagePath = path + "\\Picture\\" + textBox1.Text + ".jpg";

                // Sao chép ảnh đã chọn vào thư mục dự án
                string destinationFilePath = System.IO.Path.Combine(path + "\\Picture", textBox1.Text + ".jpg");
                File.Copy(sourceFilePath, destinationFilePath, true);
            }
            else
            {
                // Sử dụng ảnh mặc định nếu không có ảnh nào được chọn
                finalImagePath = def_path;
            }

            // Thực hiện lệnh SQL để thêm sản phẩm
            string sql = "INSERT INTO SAN_PHAM VALUES (@masp, @maloai, @tensp, @kichco, @gia, @hinhanh, @mota, @ton)";
            SqlCommand comd = new SqlCommand(sql, conn);
            comd.Parameters.AddWithValue("@masp", textBox1.Text);
            comd.Parameters.AddWithValue("@maloai", comboBox1.SelectedValue.ToString());
            comd.Parameters.AddWithValue("@tensp", textBox2.Text);
            comd.Parameters.AddWithValue("@kichco", textBox3.Text);
            comd.Parameters.AddWithValue("@gia", textBox5.Text);
            comd.Parameters.AddWithValue("@hinhanh", finalImagePath);
            comd.Parameters.AddWithValue("@mota", richTextBox1.Text);
            comd.Parameters.AddWithValue("@ton", textBox4.Text);

            comd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Thêm sản phẩm thành công!");

            // Cập nhật DataGridView
            func.HienThiDG(dataGridView1, "select MASP, MALOAI, TENSP, KICHCO, GIA, HINHANH, MOTA, TON from SAN_PHAM", conn);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) & !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) & !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            // Kiểm tra nếu ô nhập rỗng hoặc giá trị không hợp lệ
            if (int.TryParse(textBox3.Text, out int kichCo))
            {
                if (kichCo == 0 || kichCo > 49)
                {
                    MessageBox.Show("Kích cỡ phải lớn hơn 0 và nhỏ hơn 49", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox3.Focus(); // Đưa con trỏ lại ô nhập
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            // Kiểm tra nếu ô nhập rỗng hoặc giá trị không hợp lệ
            if (int.TryParse(textBox5.Text, out int gia))
            {
                if (gia == 0)
                {
                    MessageBox.Show("Giá phải lớn hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox5.Focus(); // Đưa con trỏ lại ô nhập
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) & !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            // Kiểm tra nếu ô nhập rỗng hoặc giá trị không hợp lệ
            if (int.TryParse(textBox4.Text, out int soLuong))
            {
                if (soLuong < 0)
                {
                    MessageBox.Show("Số lượng tồn phải lớn hơn hoặc bằng 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox4.Focus(); // Đưa con trỏ lại ô nhập
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
            }
        }
    }
}
