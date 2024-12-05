using System;
using System.Collections;
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
    public partial class FormThongKe : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham func = new Ham();
        SqlCommand cmd = new SqlCommand();
        public FormThongKe()
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

        private void button7_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;
            string sql = @"SELECT MAHD, NGAYLAP, TONGTIEN
                             FROM HOA_DON
                             WHERE NGAYLAP BETWEEN @startDate AND @endDate";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                // Add the date range parameters to the SQL query
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    decimal totalAmount = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        totalAmount += Convert.ToDecimal(row["TONGTIEN"]);
                    }
                    label5.Text = totalAmount.ToString("N0") + " VND";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);
            conn.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Create a DataTable for the invoices
            DataTable dataTable = new DataTable("Invoices");
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dataTable.Columns.Add(column.HeaderText, typeof(string));
            }

            // Add rows from the DataGridView to the DataTable
            decimal totalAmount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dataRow = dataTable.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dataRow[cell.ColumnIndex] = cell.Value.ToString();
                    }
                    totalAmount += Convert.ToDecimal(row.Cells["TONGTIEN"].Value); // Summing up TONGTIEN values
                    dataTable.Rows.Add(dataRow);
                }
            }

            // Add the total amount as a new row
            DataRow totalRow = dataTable.NewRow();
            totalRow["MAHD"] = "Total";
            totalRow["TONGTIEN"] = totalAmount.ToString("N0") + " VND";
            dataTable.Rows.Add(totalRow);

            // Prepare the DataSet and save to XML
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                Title = "Lưu file XML",
                FileName = "ThongKe.xml"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataSet.WriteXml(saveFileDialog.FileName);
                MessageBox.Show("Xuất file XML thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
