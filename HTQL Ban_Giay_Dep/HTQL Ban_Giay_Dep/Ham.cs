using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HTQL_Ban_Giay_Dep
{
    internal class Ham
    {
        string path = AppDomain.CurrentDomain.BaseDirectory;
        public void KetNoi(SqlConnection conn)
        {
            string chuoiketnoi = "Server = MSI\\CT281_BTNHOM; Database = CHQLGIAYDEP; Integrated Security = True";
            conn.ConnectionString = chuoiketnoi;
            conn.Open();
        }

        public void HienThiDG(DataGridView dg, string sql, SqlConnection conn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet table = new DataSet();
            adapter.Fill(table, "abc");
            dg.DataSource = table;
            dg.DataMember = "abc";
        }

        public void LoadComboBox(string sql, ComboBox cb, SqlConnection conn, string hienthi, string giatri)
        {
            SqlCommand comd = new SqlCommand(sql, conn);
            SqlDataReader reader = comd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            cb.DataSource = table;
            cb.ValueMember = giatri;
            cb.DisplayMember = hienthi;
        }

        public void LoadImageToPictureBox1(string mnv, PictureBox pb)
        {
            string folderPath = path + "\\Picture\\";
            string imagePath = Path.Combine(folderPath, mnv + ".jpg");
            string defaultImagePath = Path.Combine(folderPath, "img.jpg");
            if (File.Exists(imagePath))
            {
                // Tạo bản sao của ảnh trong bộ nhớ để ngắt kết nối với file gốc
                using (var img = Image.FromFile(imagePath))
                {
                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms, img.RawFormat);  // Lưu ảnh vào MemoryStream
                        pb.Image = Image.FromStream(ms);  // Load ảnh từ MemoryStream vào PictureBox
                    }
                }
            }
            else
            {
                using (var img = Image.FromFile(defaultImagePath))
                {
                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms, img.RawFormat);  // Lưu ảnh vào MemoryStream
                        pb.Image = Image.FromStream(ms);  // Load ảnh từ MemoryStream vào PictureBox
                    }
                }
            }
        }
    }
}
