namespace GumTree
{
    partial class Form1
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
            this.txtpath = new System.Windows.Forms.TextBox();
            this.browse_ip = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.lstboxLogger = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.c = new System.Windows.Forms.TextBox();
            this.txt_Title = new System.Windows.Forms.TextBox();
            this.txt_Price = new System.Windows.Forms.TextBox();
            this.Title = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_ImagePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_locationid = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rb_England = new System.Windows.Forms.RadioButton();
            this.rb_scotland = new System.Windows.Forms.RadioButton();
            this.rb_Wales = new System.Windows.Forms.RadioButton();
            this.rb_NI = new System.Windows.Forms.RadioButton();
            this.txt_Place = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.btn_AddImages = new System.Windows.Forms.Button();
            this.cmb_Place2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Place2 = new System.Windows.Forms.TextBox();
            this.btn_grabplace = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_Place3 = new System.Windows.Forms.ComboBox();
            this.txt_Place3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDelay = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txt_delaysingleimage = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtpath
            // 
            this.txtpath.Location = new System.Drawing.Point(154, 55);
            this.txtpath.Name = "txtpath";
            this.txtpath.Size = new System.Drawing.Size(251, 20);
            this.txtpath.TabIndex = 0;
            // 
            // browse_ip
            // 
            this.browse_ip.Location = new System.Drawing.Point(427, 53);
            this.browse_ip.Name = "browse_ip";
            this.browse_ip.Size = new System.Drawing.Size(75, 23);
            this.browse_ip.TabIndex = 1;
            this.browse_ip.Text = "Browse";
            this.browse_ip.UseVisualStyleBackColor = true;
            this.browse_ip.Click += new System.EventHandler(this.browse_ip_Click);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(154, 284);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(82, 33);
            this.btn_start.TabIndex = 2;
            this.btn_start.Text = "Login";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // lstboxLogger
            // 
            this.lstboxLogger.FormattingEnabled = true;
            this.lstboxLogger.Location = new System.Drawing.Point(3, 325);
            this.lstboxLogger.Name = "lstboxLogger";
            this.lstboxLogger.Size = new System.Drawing.Size(965, 173);
            this.lstboxLogger.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Accounts";
            // 
            // c
            // 
            this.c.Location = new System.Drawing.Point(154, 121);
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(251, 20);
            this.c.TabIndex = 9;
            this.c.Text = "The term couch is predominantly used in Ireland, North America, South Africa and " +
                "Australia whereas the terms sofa and settee (U and non-U) are generally used in " +
                "the United Kingdom.";
            // 
            // txt_Title
            // 
            this.txt_Title.Location = new System.Drawing.Point(154, 95);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(251, 20);
            this.txt_Title.TabIndex = 10;
            // 
            // txt_Price
            // 
            this.txt_Price.Location = new System.Drawing.Point(154, 147);
            this.txt_Price.Name = "txt_Price";
            this.txt_Price.Size = new System.Drawing.Size(251, 20);
            this.txt_Price.TabIndex = 11;
            this.txt_Price.Text = "30";
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Location = new System.Drawing.Point(14, 89);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(27, 13);
            this.Title.TabIndex = 12;
            this.Title.Text = "Title";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Description";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Amount";
            // 
            // txt_ImagePath
            // 
            this.txt_ImagePath.Location = new System.Drawing.Point(154, 173);
            this.txt_ImagePath.Name = "txt_ImagePath";
            this.txt_ImagePath.Size = new System.Drawing.Size(251, 20);
            this.txt_ImagePath.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Images";
            // 
            // cmb_locationid
            // 
            this.cmb_locationid.FormattingEnabled = true;
            this.cmb_locationid.Location = new System.Drawing.Point(670, 69);
            this.cmb_locationid.Name = "cmb_locationid";
            this.cmb_locationid.Size = new System.Drawing.Size(165, 21);
            this.cmb_locationid.TabIndex = 17;
            this.cmb_locationid.Visible = false;
            this.cmb_locationid.SelectedIndexChanged += new System.EventHandler(this.cmb_locationid_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(580, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Location Id";
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(580, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Location Place";
            this.label8.Visible = false;
            // 
            // rb_England
            // 
            this.rb_England.AutoSize = true;
            this.rb_England.Location = new System.Drawing.Point(154, 207);
            this.rb_England.Name = "rb_England";
            this.rb_England.Size = new System.Drawing.Size(64, 17);
            this.rb_England.TabIndex = 21;
            this.rb_England.TabStop = true;
            this.rb_England.Text = "England";
            this.rb_England.UseVisualStyleBackColor = true;
            this.rb_England.CheckedChanged += new System.EventHandler(this.rb_England_CheckedChanged);
            // 
            // rb_scotland
            // 
            this.rb_scotland.AutoSize = true;
            this.rb_scotland.Location = new System.Drawing.Point(248, 207);
            this.rb_scotland.Name = "rb_scotland";
            this.rb_scotland.Size = new System.Drawing.Size(67, 17);
            this.rb_scotland.TabIndex = 22;
            this.rb_scotland.TabStop = true;
            this.rb_scotland.Text = "Scotland";
            this.rb_scotland.UseVisualStyleBackColor = true;
            this.rb_scotland.CheckedChanged += new System.EventHandler(this.rb_scotland_CheckedChanged);
            // 
            // rb_Wales
            // 
            this.rb_Wales.AutoSize = true;
            this.rb_Wales.Location = new System.Drawing.Point(347, 208);
            this.rb_Wales.Name = "rb_Wales";
            this.rb_Wales.Size = new System.Drawing.Size(55, 17);
            this.rb_Wales.TabIndex = 23;
            this.rb_Wales.TabStop = true;
            this.rb_Wales.Text = "Wales";
            this.rb_Wales.UseVisualStyleBackColor = true;
            this.rb_Wales.CheckedChanged += new System.EventHandler(this.rb_Wales_CheckedChanged);
            // 
            // rb_NI
            // 
            this.rb_NI.AutoSize = true;
            this.rb_NI.Location = new System.Drawing.Point(427, 208);
            this.rb_NI.Name = "rb_NI";
            this.rb_NI.Size = new System.Drawing.Size(101, 17);
            this.rb_NI.TabIndex = 24;
            this.rb_NI.TabStop = true;
            this.rb_NI.Text = "Northern Ireland";
            this.rb_NI.UseVisualStyleBackColor = true;
            this.rb_NI.CheckedChanged += new System.EventHandler(this.rb_NI_CheckedChanged);
            // 
            // txt_Place
            // 
            this.txt_Place.Enabled = false;
            this.txt_Place.Location = new System.Drawing.Point(670, 94);
            this.txt_Place.Name = "txt_Place";
            this.txt_Place.Size = new System.Drawing.Size(251, 20);
            this.txt_Place.TabIndex = 25;
            this.txt_Place.Visible = false;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Font = new System.Drawing.Font("Cambria", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(971, 38);
            this.label25.TabIndex = 26;
            this.label25.Text = "Post Add";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_AddImages
            // 
            this.btn_AddImages.Location = new System.Drawing.Point(427, 171);
            this.btn_AddImages.Name = "btn_AddImages";
            this.btn_AddImages.Size = new System.Drawing.Size(75, 23);
            this.btn_AddImages.TabIndex = 27;
            this.btn_AddImages.Text = "Browse";
            this.btn_AddImages.UseVisualStyleBackColor = true;
            this.btn_AddImages.Click += new System.EventHandler(this.btn_AddImages_Click);
            // 
            // cmb_Place2
            // 
            this.cmb_Place2.FormattingEnabled = true;
            this.cmb_Place2.Location = new System.Drawing.Point(670, 120);
            this.cmb_Place2.Name = "cmb_Place2";
            this.cmb_Place2.Size = new System.Drawing.Size(165, 21);
            this.cmb_Place2.TabIndex = 28;
            this.cmb_Place2.Visible = false;
            this.cmb_Place2.SelectedIndexChanged += new System.EventHandler(this.cmb_Place2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(580, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Location Id 2";
            this.label2.Visible = false;
            // 
            // txt_Place2
            // 
            this.txt_Place2.Enabled = false;
            this.txt_Place2.Location = new System.Drawing.Point(670, 148);
            this.txt_Place2.Name = "txt_Place2";
            this.txt_Place2.Size = new System.Drawing.Size(251, 20);
            this.txt_Place2.TabIndex = 30;
            this.txt_Place2.Visible = false;
            // 
            // btn_grabplace
            // 
            this.btn_grabplace.Location = new System.Drawing.Point(670, 227);
            this.btn_grabplace.Name = "btn_grabplace";
            this.btn_grabplace.Size = new System.Drawing.Size(82, 33);
            this.btn_grabplace.TabIndex = 31;
            this.btn_grabplace.Text = "Start";
            this.btn_grabplace.UseVisualStyleBackColor = true;
            this.btn_grabplace.Visible = false;
            this.btn_grabplace.Click += new System.EventHandler(this.btn_grabplace_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(580, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Location Place 2";
            this.label3.Visible = false;
            // 
            // cmb_Place3
            // 
            this.cmb_Place3.FormattingEnabled = true;
            this.cmb_Place3.Location = new System.Drawing.Point(670, 174);
            this.cmb_Place3.Name = "cmb_Place3";
            this.cmb_Place3.Size = new System.Drawing.Size(165, 21);
            this.cmb_Place3.TabIndex = 33;
            this.cmb_Place3.Visible = false;
            this.cmb_Place3.SelectedIndexChanged += new System.EventHandler(this.cmb_Place3_SelectedIndexChanged);
            // 
            // txt_Place3
            // 
            this.txt_Place3.Enabled = false;
            this.txt_Place3.Location = new System.Drawing.Point(670, 201);
            this.txt_Place3.Name = "txt_Place3";
            this.txt_Place3.Size = new System.Drawing.Size(251, 20);
            this.txt_Place3.TabIndex = 34;
            this.txt_Place3.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(580, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Location Id 3";
            this.label9.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(580, 204);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "Location Place 3";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(153, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(198, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "Title should be less than 100 characters.";
            // 
            // txtDelay
            // 
            this.txtDelay.Location = new System.Drawing.Point(154, 257);
            this.txtDelay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(82, 20);
            this.txtDelay.TabIndex = 38;
            this.txtDelay.Text = "10";
            this.txtDelay.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 260);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "Delay";
            this.label12.Visible = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(427, 92);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 40;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txt_delaysingleimage
            // 
            this.txt_delaysingleimage.Location = new System.Drawing.Point(155, 233);
            this.txt_delaysingleimage.Margin = new System.Windows.Forms.Padding(2);
            this.txt_delaysingleimage.Name = "txt_delaysingleimage";
            this.txt_delaysingleimage.Size = new System.Drawing.Size(81, 20);
            this.txt_delaysingleimage.TabIndex = 41;
            this.txt_delaysingleimage.Text = "10";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 233);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 13);
            this.label13.TabIndex = 42;
            this.label13.Text = "Single Image Delay";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(245, 237);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 13);
            this.label14.TabIndex = 43;
            this.label14.Text = "Min";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(245, 263);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(24, 13);
            this.label15.TabIndex = 44;
            this.label15.Text = "Min";
            this.label15.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 502);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txt_delaysingleimage);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtDelay);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_Place3);
            this.Controls.Add(this.cmb_Place3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_grabplace);
            this.Controls.Add(this.txt_Place2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_Place2);
            this.Controls.Add(this.btn_AddImages);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txt_Place);
            this.Controls.Add(this.rb_NI);
            this.Controls.Add(this.rb_Wales);
            this.Controls.Add(this.rb_scotland);
            this.Controls.Add(this.rb_England);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmb_locationid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_ImagePath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.txt_Price);
            this.Controls.Add(this.txt_Title);
            this.Controls.Add(this.c);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstboxLogger);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.browse_ip);
            this.Controls.Add(this.txtpath);
            this.Name = "Form1";
            this.Text = "Post Add";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtpath;
        private System.Windows.Forms.Button browse_ip;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.ListBox lstboxLogger;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox c;
        private System.Windows.Forms.TextBox txt_Title;
        private System.Windows.Forms.TextBox txt_Price;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_ImagePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_locationid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rb_England;
        private System.Windows.Forms.RadioButton rb_scotland;
        private System.Windows.Forms.RadioButton rb_Wales;
        private System.Windows.Forms.RadioButton rb_NI;
        private System.Windows.Forms.TextBox txt_Place;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btn_AddImages;
        private System.Windows.Forms.ComboBox cmb_Place2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Place2;
        private System.Windows.Forms.Button btn_grabplace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_Place3;
        private System.Windows.Forms.TextBox txt_Place3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDelay;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txt_delaysingleimage;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}

