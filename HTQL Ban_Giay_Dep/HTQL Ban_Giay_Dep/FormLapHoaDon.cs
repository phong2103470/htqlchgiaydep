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
using System.Data;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HTQL_Ban_Giay_Dep
{
    public partial class FormLapHoaDon : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham func = new Ham();
        public FormLapHoaDon()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuanLy_KH_NV ql_kh_nv = new FormQuanLy_KH_NV();
            ql_kh_nv.Show();
            ql_kh_nv.FormClosed += (s, args) => this.Close();
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

        private void FormLapHoaDon_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);

            //Load khach hang len combobox
            func.LoadComboBox("select * from KHACH_HANG", comboBox1, conn, comboBox1.DisplayMember, comboBox1.ValueMember);
            comboBox1.Text = string.Empty;
            func.LoadComboBox("select * from LOAI_SAN_PHAM", comboBox2, conn, comboBox2.DisplayMember, comboBox2.ValueMember);
            comboBox2.Text = string.Empty;
            // Thiết lập comboBox3
            comboBox3.DataSource = null;
            comboBox3.Items.Clear();

            textBox5.Text = string.Empty;
            //Lay ngay hien tai
            textBox2.Text = DateTime.Now.ToString("dd-MM-yyyy");
            textBox2.Enabled = false;

            //Lay ma nhan vien dang xet
            textBox3.Text = FormDangNhap.MaNV;
            textBox3.Enabled = false;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null) // Kiểm tra xem có giá trị được chọn không
            {
                string query = "SELECT * FROM SAN_PHAM WHERE MALOAI = @MALOAI";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MALOAI", comboBox2.SelectedValue.ToString());
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    comboBox3.DataSource = dt; // Đặt DataSource cho comboBox3
                    comboBox3.DisplayMember = "TENSP"; // Tên hiển thị trong comboBox3
                    comboBox3.ValueMember = "MASP"; // Giá trị của comboBox3
                    comboBox3.Text = string.Empty; // Đặt lại giá trị cho comboBox3
                }
            }

            // Đặt lại giá sản phẩm khi loại sản phẩm thay đổi
            textBox5.Text = string.Empty;
            textBox5.Enabled = false; // Tắt ô giá sản phẩm cho đến khi chọn sản phẩm
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) & !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private int GetTon(string msp)
        {
            int ton = 0;
            string query = "SELECT TON FROM SAN_PHAM WHERE MASP = @productID";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@productID", msp);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                ton = Convert.ToInt32(reader["TON"]);
            }
            reader.Close();
            return ton;
        }


        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if (!Char.IsDigit(e.KeyChar) & !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }*/
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            // Kiểm tra nếu ô nhập rỗng hoặc giá trị không hợp lệ
            if (int.TryParse(textBox4.Text, out int soLuong))
            {
                if (soLuong == 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox4.Focus(); // Đưa con trỏ lại ô nhập
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            // Kiểm tra nếu ô nhập rỗng hoặc giá trị không hợp lệ
            /*if (int.TryParse(textBox5.Text, out int gia))
            {
                if (gia == 0)
                {
                    MessageBox.Show("Đơn giá phải lớn hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox5.Focus(); // Đưa con trỏ lại ô nhập
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
            }*/
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            if (comboBox1 == null || string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn hành khách!");
            }
            else
            {
                string sql_them = "insert into HOA_DON values ('" + textBox1.Text + "', '" + comboBox1.SelectedValue.ToString() + "', '" + textBox3.Text + "', '" + day + "', '" + DBNull.Value + "')";
                //MessageBox.Show(sql_them);
                SqlCommand comd_them = new SqlCommand(sql_them, conn);
                comd_them.ExecuteNonQuery();
                comboBox1.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                MessageBox.Show("Thêm hóa đơn thành công!");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu số lượng nhập vào lớn hơn tồn kho
            string mahd = textBox1.Text;
            string msp = comboBox3.SelectedValue.ToString();
            int ton = GetTon(msp);
            string query_get_SL = "Select SOLUONG from CHI_TIET_HOA_DON where MAHD = '" + mahd + "' and MASP = '" + msp + "'";
            SqlCommand cmd_kt = new SqlCommand(query_get_SL, conn);
            SqlDataReader reader = cmd_kt.ExecuteReader();
            int soluong = 0;
            if (reader.Read())
            {
                int.TryParse(reader["SOLUONG"].ToString(), out soluong);
            }
            reader.Close();
            int ton_TT = soluong + ton;
            // Ghép giá trị hiện tại của textbox với ký tự mới nhập để kiểm tra
            if (int.TryParse(textBox4.Text, out int newQuantity) && newQuantity > ton_TT)
            {
                MessageBox.Show($"Số lượng nhập vào không được vượt quá tồn kho: {ton_TT}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Đặt lại giá trị trong textBox4 để bắt nhập lại
                textBox4.Clear();

            }
            else
            {
                //insert
                soluong = int.Parse(textBox4.Text);
                int thanh_tien = int.Parse(textBox5.Text) * soluong;
                string sql_them = "insert into CHI_TIET_HOA_DON values ('" + comboBox3.SelectedValue.ToString() + "', '" + textBox1.Text + "', " + textBox4.Text + ", " + thanh_tien + ")";
                string sql_update = "UPDATE HOA_DON SET TONGTIEN = (SELECT SUM(THANHTIEN) FROM CHI_TIET_HOA_DON WHERE MAHD = '" + textBox1.Text + "') WHERE MAHD = '" + textBox1.Text + "'";
                //MessageBox.Show(sql_them);
                SqlCommand comd_them = new SqlCommand(sql_them, conn);
                comd_them.ExecuteNonQuery();

                SqlCommand comd_update = new SqlCommand(sql_update, conn);
                comd_update.ExecuteNonQuery();

                //Load datagidview
                string sql = "select c.MAHD, s.MASP, s.TENSP, l.TENLOAI, c.SOLUONG, s.GIA,c.THANHTIEN from HOA_DON h, CHI_TIET_HOA_DON c, SAN_PHAM s, LOAI_SAN_PHAM l where h.MAHD = c.MAHD and c.MASP = s.MASP and s.MALOAI = l.MALOAI and h.MAHD = '" + textBox1.Text + "'";
                func.HienThiDG(dataGridView1, sql, conn);

                //hien tong tien
                string sql_select = "SELECT TONGTIEN FROM HOA_DON WHERE MAHD = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sql_select, conn);
                cmd.ExecuteNonQuery();
                object result = cmd.ExecuteScalar();
                textBox6.Text = result.ToString();
                textBox6.Enabled = false;

                //Update Ton kho trong bang San_Pham
                msp = comboBox3.SelectedValue.ToString();
                string query = "Select TON from SAN_PHAM where MASP = '" + msp + "'";
                SqlCommand comd_ton = new SqlCommand(query, conn);
                int ton_Hientai = Convert.ToInt32(comd_ton.ExecuteScalar());

                if (soluong <= ton_Hientai)
                {
                    int ton_moi = ton_Hientai - soluong;
                    string query_Update_Ton = "Update SAN_PHAM SET TON = " + ton_moi + " where MASP = '" + msp + "'";
                    SqlCommand cmdd = new SqlCommand(query_Update_Ton, conn);
                    cmdd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Số lượng tồn trong kho không đủ so với số lượng nhập. Vui lòng kiểm tra tồn kho");
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedValue != null)
            {
                string lay_gia = "SELECT GIA FROM SAN_PHAM WHERE MASP = @MASP"; // Sử dụng tham số

                // Mở kết nối nếu chưa mở
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                using (SqlCommand comd = new SqlCommand(lay_gia, conn))
                {
                    // Thêm tham số vào câu lệnh
                    comd.Parameters.AddWithValue("@MASP", comboBox3.SelectedValue.ToString());

                    // Thực thi câu lệnh và đọc kết quả
                    using (SqlDataReader reader = comd.ExecuteReader())
                    {
                        if (reader.Read()) // Kiểm tra xem có dữ liệu trả về không
                        {
                            int gia = reader.GetInt32(0); // Lấy giá dưới dạng int
                            textBox5.Text = gia.ToString(); // Hiển thị giá trong TextBox5
                        }
                        else
                        {
                            textBox5.Text = "N/A"; // Không tìm thấy giá
                        }
                    }
                    textBox5.Enabled = false;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Ma hoa don tu dong tang 
            string max_ms = "select max (substring(MAHD,3,5)) + 1 from HOA_DON";
            SqlCommand comd = new SqlCommand(max_ms, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read())
            {
                int new_ms = int.Parse(reader[0].ToString());
                if (new_ms < 10)
                    textBox1.Text = "HD00" + new_ms;
                else if (new_ms < 100)
                    textBox1.Text = "HD0" + new_ms;
                else if (new_ms < 1000)
                    textBox1.Text = "HD" + new_ms;
                else
                    MessageBox.Show("Can reset ma hoa don");
            }
            textBox1.Enabled = false;
            reader.Close();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            comboBox2.Text = string.Empty;
            comboBox2.Enabled = true;
            comboBox3.Text = string.Empty;
            comboBox3.Enabled = true;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            button5.Enabled = true;
        }

        /*private void button7_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu DataGridView có ít nhất một dòng không
            if (dataGridView1.Rows.Count > 0)
            {
                // Lấy dòng cuối cùng trên DataGridView
                DataGridViewRow lastRow = dataGridView1.Rows[dataGridView1.Rows.Count - 2];

                // Kiểm tra xem các ô trong dòng có giá trị không
                if (lastRow.Cells[0].Value != null && lastRow.Cells[1].Value != null)
                {
                    string mahd = lastRow.Cells[0].Value.ToString();
                    string masp = lastRow.Cells[1].Value.ToString();

                    // Câu lệnh SQL để xóa dòng dựa trên mã hóa đơn và mã sản phẩm
                    string sql_delete = "DELETE FROM CHI_TIET_HOA_DON WHERE MAHD = @mahd AND MASP = @masp";

                    try
                    {
                        // Tạo và thực thi lệnh xóa
                        SqlCommand cmd = new SqlCommand(sql_delete, conn);
                        cmd.Parameters.AddWithValue("@mahd", mahd);
                        cmd.Parameters.AddWithValue("@masp", masp);
                        cmd.ExecuteNonQuery();

                        // Xóa dòng cuối cùng khỏi DataGridView
                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);

                        MessageBox.Show("Xóa dòng gần nhất thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Dòng cuối không có giá trị hợp lệ.");
                }
            }
            else
            {
                MessageBox.Show("Không có dòng nào để xóa.");
            }

        }*/

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            button9.Enabled = true;
            comboBox1.Text = string.Empty;
            comboBox1.Enabled = true;
            button8.Enabled = true;
            comboBox2.Text = string.Empty;
            comboBox2.Enabled = true;
            comboBox3.Text = string.Empty;
            comboBox3.Enabled = true;
            textBox4.Text = string.Empty;
            textBox4.Enabled = true;
            textBox5.Text = string.Empty;
            button5.Enabled = true;
            button7.Enabled = true;
            button11.Enabled = true;
            button6.Enabled = true;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            label13.Text = string.Empty;
            ResetDataGridView();

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboBox2.Enabled = false;
            comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBox3.Enabled = false;
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            button5.Enabled = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {

            string mahd = textBox1.Text;
            string masp = comboBox3.SelectedValue.ToString();

            //Cập nhật lại số lượng tồn cho sản phẩm sau khi xóa
            string query = "Select ct.SOLUONG, sp.TON from CHI_TIET_HOA_DON ct join SAN_PHAM sp on ct.MASP = sp.MASP where ct.MAHD = '" + mahd + "' and sp.MASP = '" + masp + "'";
            SqlCommand cmd2 = new SqlCommand(query, conn);
            SqlDataReader reader = cmd2.ExecuteReader();
            int soluong = 0;
            int ton = 0;
            int ton_Update = 0;
            if (reader.Read())
            {
                int.TryParse(reader["SOLUONG"].ToString(), out soluong);
                int.TryParse(reader["TON"].ToString(), out ton);
            }
            reader.Close();
            ton_Update = ton + soluong;
            string query_update_Ton = "Update SAN_PHAM set TON = '" + ton_Update + "' where MASP = '" + masp + "'";
            SqlCommand cmd4 = new SqlCommand(query_update_Ton, conn);
            cmd4.ExecuteNonQuery();


            string sql = "delete from CHI_TIET_HOA_DON where MAHD = '" + mahd + "' and MASP = '" + masp + "'";
            SqlCommand comd = new SqlCommand(sql, conn);
            //MessageBox.Show(sql);
            comd.ExecuteNonQuery();
            MessageBox.Show("Xóa thông tin chi tiết hóa đơn thành công!");

            //cap nhap lai tong tien cho hoa don
            int tongtien = TinhTongTien(mahd);
            string sql_update = "UPDATE HOA_DON SET TONGTIEN = " + tongtien + "  WHERE MAHD = '" + textBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_update, conn);
            cmd.ExecuteNonQuery();

            //Load datagidview
            string sql_hien = "select c.MAHD, s.MASP, s.TENSP, l.TENLOAI, c.SOLUONG, s.GIA,c.THANHTIEN from HOA_DON h, CHI_TIET_HOA_DON c, SAN_PHAM s, LOAI_SAN_PHAM l where h.MAHD = c.MAHD and c.MASP = s.MASP and s.MALOAI = l.MALOAI and h.MAHD = '" + textBox1.Text + "'";
            func.HienThiDG(dataGridView1, sql_hien, conn);

            //tự đặt lại
            comboBox2.Text = string.Empty;
            comboBox2.Enabled = true;
            comboBox3.Text = string.Empty;
            comboBox3.Enabled = true;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            button5.Enabled = true;

            //hien tong tien
            string sql_select = "SELECT TONGTIEN FROM HOA_DON WHERE MAHD = '" + textBox1.Text + "'";
            SqlCommand cmd1 = new SqlCommand(sql_select, conn);
            cmd1.ExecuteNonQuery();
            object result = cmd1.ExecuteScalar();
            textBox6.Text = result.ToString();
            textBox6.Enabled = false;
        }
        private int TinhTongTien(string mahd)
        {
            int tongTien = 0;
            string sqlSelect = "SELECT SUM(THANHTIEN) FROM CHI_TIET_HOA_DON WHERE MAHD = '" + textBox1.Text + "'";
            SqlCommand cmd1 = new SqlCommand(sqlSelect, conn);
            cmd1.ExecuteNonQuery();
            object result = cmd1.ExecuteScalar();
            if (result != DBNull.Value)
            {
                tongTien = Convert.ToInt32(result);
            }

            return tongTien;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //cap nhat lai co luong san pham
            string mahd = textBox1.Text;
            string msp = comboBox3.SelectedValue.ToString();
            //Lay SoLuong bang CTHD va TON bang SAN PHAM
            string query1 = "Select ct.SOLUONG, sp.TON from CHI_TIET_HOA_DON ct join SAN_PHAM sp on ct.MASP = sp.MASP where ct.MAHD = '" + mahd + "' and sp.MASP = '" + msp + "'";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            SqlDataReader reader = cmd1.ExecuteReader();
            int soluong_NhapVao = 0;
            int.TryParse(textBox4.Text, out soluong_NhapVao);
            int sl_cu = 0;
            int ton_Hientai = 0;
            if (reader.Read())
            {
                int.TryParse(reader["SOLUONG"].ToString(), out sl_cu);
                int.TryParse(reader["TON"].ToString(), out ton_Hientai);
                reader.Close();
            }
            int ton_Tong = ton_Hientai + sl_cu;
            if (soluong_NhapVao <= ton_Tong)
            {
                string query_recover_Ton = "Update SAN_PHAM set TON = '" + ton_Tong + "' where MASP = '" + msp + "'";
                SqlCommand cmd3 = new SqlCommand(query_recover_Ton, conn);
                cmd3.ExecuteNonQuery();
                int ton_Moi = ton_Tong - soluong_NhapVao;
                string query_Ton_moi = "Update SAN_PHAM set TON = '" + ton_Moi + "' where MASP = '" + msp + "'";
                SqlCommand cmd4 = new SqlCommand(query_Ton_moi, conn);
                cmd4.ExecuteNonQuery();

                //Sửa
                int soluong = int.Parse(textBox4.Text);
                int thanh_tien = int.Parse(textBox5.Text) * soluong;
                string sql_update = "UPDATE CHI_TIET_HOA_DON SET SOLUONG = " + soluong + ", THANHTIEN = " + thanh_tien + "  WHERE MAHD = '" + mahd + "' and MASP = '" + msp + "'";
                SqlCommand cmd = new SqlCommand(sql_update, conn);
                cmd.ExecuteNonQuery();

                //cap nhap lai tong tien cho hoa don
                int tongtien = TinhTongTien(mahd);
                string sql_up = "UPDATE HOA_DON SET TONGTIEN = " + tongtien + "  WHERE MAHD = '" + textBox1.Text + "'";
                SqlCommand cmd2 = new SqlCommand(sql_up, conn);
                cmd2.ExecuteNonQuery();

                //Load datagidview
                string sql_hien = "select c.MAHD, s.MASP, s.TENSP, l.TENLOAI, c.SOLUONG, s.GIA,c.THANHTIEN from HOA_DON h, CHI_TIET_HOA_DON c, SAN_PHAM s, LOAI_SAN_PHAM l where h.MAHD = c.MAHD and c.MASP = s.MASP and s.MALOAI = l.MALOAI and h.MAHD = '" + textBox1.Text + "'";
                func.HienThiDG(dataGridView1, sql_hien, conn);

                //hien lai tong tien
                string sql_select = "SELECT TONGTIEN FROM HOA_DON WHERE MAHD = '" + textBox1.Text + "'";
                SqlCommand cmd5 = new SqlCommand(sql_select, conn);
                cmd5.ExecuteNonQuery();
                object result = cmd5.ExecuteScalar();
                textBox6.Text = result.ToString();
                textBox6.Enabled = false;
            }
            else
            {
                MessageBox.Show("Số lượng tồn kho không đủ cho số lượng đặt hàng, vui lòng kiểm tra tồn kho");
                textBox4.Text = string.Empty;
                textBox4.Focus();
            }
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            int soTienNhap = 0;
            int soTienDaTra = 0;

            // Kiểm tra nếu textBox7 không rỗng và có thể chuyển đổi sang decimal
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(textBox7.Text) &&
                int.TryParse(textBox7.Text, out soTienNhap) &&
                int.TryParse(textBox6.Text, out soTienDaTra))
                {
                    // Kiểm tra điều kiện số tiền nhập vào phải lớn hơn số tiền đã trả
                    if (soTienNhap > soTienDaTra)
                    {
                        // Tính số tiền cần trả và hiển thị ở textBox8
                        decimal soTienCanTra = soTienNhap - soTienDaTra;
                        label13.Text = soTienCanTra.ToString(); // Định dạng với 2 chữ số thập phân
                    }
                    else
                    {
                        // Nếu số tiền nhập vào không lớn hơn, thì xóa giá trị trong textBox8
                        label13.Text = string.Empty;
                        MessageBox.Show("Số tiền nhập vào phải lớn hơn tổng số tiền của hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Nếu textBox7 rỗng hoặc không phải là số, xóa giá trị trong textBox8
                    label13.Text = string.Empty;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDangNhap dn = new FormDangNhap();
            dn.Show();
            dn.FormClosed += (s, args) => this.Close();
        }
        private void ResetDataGridView()
        {
            // Tạo một DataTable mới để lưu trữ dữ liệu
            DataTable dataTable = new DataTable();

            // Truy vấn lại dữ liệu từ cơ sở dữ liệu
            string query = "SELECT c.MAHD, s.MASP, s.TENSP, l.TENLOAI, c.SOLUONG, s.GIA, c.THANHTIEN " +
                           "FROM HOA_DON h " +
                           "JOIN CHI_TIET_HOA_DON c ON h.MAHD = c.MAHD " +
                           "JOIN SAN_PHAM s ON c.MASP = s.MASP " +
                           "JOIN LOAI_SAN_PHAM l ON s.MALOAI = l.MALOAI " +
                           "WHERE h.MAHD = @MAHD"; // Thêm điều kiện theo nhu cầu

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MAHD", textBox1.Text); // Gán giá trị MAHD từ textBox1

                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataTable); // Điền dữ liệu vào DataTable
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi lấy dữ liệu: " + ex.Message);
                }

            }

            // Gán DataTable làm DataSource cho DataGridView
            dataGridView1.DataSource = dataTable;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
