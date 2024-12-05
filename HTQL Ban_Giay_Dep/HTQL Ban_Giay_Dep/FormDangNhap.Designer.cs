namespace HTQL_Ban_Giay_Dep
{
    partial class FormDangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDangNhap));
            panel2 = new Panel();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            button1 = new Button();
            textBox2 = new TextBox();
            label3 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            panel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.InactiveCaption;
            panel2.Controls.Add(checkBox2);
            panel2.Controls.Add(checkBox1);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(467, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(458, 467);
            panel2.TabIndex = 1;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox2.Location = new Point(251, 317);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(94, 32);
            checkBox2.TabIndex = 7;
            checkBox2.Text = "Admin";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox1.Location = new Point(51, 317);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(168, 32);
            checkBox1.TabIndex = 6;
            checkBox1.Text = "Hiện mật khẩu";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button1
            // 
            button1.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(51, 369);
            button1.Name = "button1";
            button1.Size = new Size(148, 38);
            button1.TabIndex = 5;
            button1.Text = "ĐĂNG NHẬP";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(51, 259);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(292, 27);
            textBox2.TabIndex = 4;
            textBox2.KeyDown += textBox2_KeyDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(45, 228);
            label3.Name = "label3";
            label3.Size = new Size(107, 28);
            label3.TabIndex = 3;
            label3.Text = "Mật khẩu:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(53, 169);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(292, 27);
            textBox1.TabIndex = 2;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(45, 138);
            label2.Name = "label2";
            label2.Size = new Size(154, 28);
            label2.TabIndex = 1;
            label2.Text = "Tên đăng nhập:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(41, 43);
            label1.Name = "label1";
            label1.Size = new Size(224, 49);
            label1.TabIndex = 0;
            label1.Text = "ĐĂNG NHẬP";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panel2, 1, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(928, 473);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Honeydew;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(458, 467);
            panel1.TabIndex = 2;
            // 
            // FormDangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(958, 499);
            Controls.Add(tableLayoutPanel1);
            Name = "FormDangNhap";
            Text = "FormDangNhap";
            Load += FormDangNhap_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textBox2;
        private Label label3;
        private TextBox textBox1;
        private Label label2;
        private Label label1;
        private CheckBox checkBox1;
        private Button button1;
        private Panel panel1;
        private CheckBox checkBox2;
    }
}