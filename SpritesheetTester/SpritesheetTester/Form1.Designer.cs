namespace SpritesheetTester
{
    partial class animationEditor
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
            this.pathBx = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.xBx = new System.Windows.Forms.TextBox();
            this.yBx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.widthBx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.heightBx = new System.Windows.Forms.TextBox();
            this.viewBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.g1 = new System.Windows.Forms.GroupBox();
            this.g2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.frameDelayBx = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.columnWidthBx = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rowHeightBx = new System.Windows.Forms.TextBox();
            this.genAnimationBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.startYBx = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.startXBx = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.columnCntBx = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rowCntBx = new System.Windows.Forms.TextBox();
            this.mousePosLbl = new System.Windows.Forms.Label();
            this.JSonBx = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.drawPnl = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.spritesheetBx = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.exportBtn = new System.Windows.Forms.Button();
            this.animationNameBx = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.g1.SuspendLayout();
            this.g2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pathBx
            // 
            this.pathBx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pathBx.Location = new System.Drawing.Point(518, 12);
            this.pathBx.Name = "pathBx";
            this.pathBx.Size = new System.Drawing.Size(291, 20);
            this.pathBx.TabIndex = 1;
            // 
            // browseBtn
            // 
            this.browseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseBtn.Location = new System.Drawing.Point(896, 10);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 23);
            this.browseBtn.TabIndex = 2;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // xBx
            // 
            this.xBx.Location = new System.Drawing.Point(8, 34);
            this.xBx.Name = "xBx";
            this.xBx.Size = new System.Drawing.Size(100, 20);
            this.xBx.TabIndex = 3;
            this.xBx.Text = "0";
            // 
            // yBx
            // 
            this.yBx.Location = new System.Drawing.Point(114, 34);
            this.yBx.Name = "yBx";
            this.yBx.Size = new System.Drawing.Size(100, 20);
            this.yBx.TabIndex = 4;
            this.yBx.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Width";
            // 
            // widthBx
            // 
            this.widthBx.Location = new System.Drawing.Point(225, 34);
            this.widthBx.Name = "widthBx";
            this.widthBx.Size = new System.Drawing.Size(100, 20);
            this.widthBx.TabIndex = 9;
            this.widthBx.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(328, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Height";
            // 
            // heightBx
            // 
            this.heightBx.Location = new System.Drawing.Point(331, 34);
            this.heightBx.Name = "heightBx";
            this.heightBx.Size = new System.Drawing.Size(100, 20);
            this.heightBx.TabIndex = 11;
            this.heightBx.Text = "1";
            // 
            // viewBtn
            // 
            this.viewBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.viewBtn.Enabled = false;
            this.viewBtn.Location = new System.Drawing.Point(334, 66);
            this.viewBtn.Name = "viewBtn";
            this.viewBtn.Size = new System.Drawing.Size(113, 23);
            this.viewBtn.TabIndex = 13;
            this.viewBtn.Text = "View";
            this.viewBtn.UseVisualStyleBackColor = true;
            this.viewBtn.Click += new System.EventHandler(this.selectBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okBtn.Location = new System.Drawing.Point(815, 10);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 14;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // g1
            // 
            this.g1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.g1.Controls.Add(this.yBx);
            this.g1.Controls.Add(this.xBx);
            this.g1.Controls.Add(this.viewBtn);
            this.g1.Controls.Add(this.label1);
            this.g1.Controls.Add(this.label4);
            this.g1.Controls.Add(this.label2);
            this.g1.Controls.Add(this.heightBx);
            this.g1.Controls.Add(this.widthBx);
            this.g1.Controls.Add(this.label3);
            this.g1.Enabled = false;
            this.g1.Location = new System.Drawing.Point(518, 64);
            this.g1.Name = "g1";
            this.g1.Size = new System.Drawing.Size(453, 95);
            this.g1.TabIndex = 15;
            this.g1.TabStop = false;
            this.g1.Text = "View Specific Location";
            // 
            // g2
            // 
            this.g2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.g2.Controls.Add(this.label11);
            this.g2.Controls.Add(this.frameDelayBx);
            this.g2.Controls.Add(this.checkBox2);
            this.g2.Controls.Add(this.checkBox1);
            this.g2.Controls.Add(this.label9);
            this.g2.Controls.Add(this.columnWidthBx);
            this.g2.Controls.Add(this.label10);
            this.g2.Controls.Add(this.rowHeightBx);
            this.g2.Controls.Add(this.genAnimationBtn);
            this.g2.Controls.Add(this.label7);
            this.g2.Controls.Add(this.startYBx);
            this.g2.Controls.Add(this.label8);
            this.g2.Controls.Add(this.startXBx);
            this.g2.Controls.Add(this.label6);
            this.g2.Controls.Add(this.columnCntBx);
            this.g2.Controls.Add(this.label5);
            this.g2.Controls.Add(this.rowCntBx);
            this.g2.Enabled = false;
            this.g2.Location = new System.Drawing.Point(518, 165);
            this.g2.Name = "g2";
            this.g2.Size = new System.Drawing.Size(453, 149);
            this.g2.TabIndex = 14;
            this.g2.TabStop = false;
            this.g2.Text = "Create Animation";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(141, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "Delay Between Frames (MS)";
            // 
            // frameDelayBx
            // 
            this.frameDelayBx.Location = new System.Drawing.Point(6, 116);
            this.frameDelayBx.Name = "frameDelayBx";
            this.frameDelayBx.Size = new System.Drawing.Size(100, 20);
            this.frameDelayBx.TabIndex = 28;
            this.frameDelayBx.Text = "33";
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(144, 126);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(79, 17);
            this.checkBox2.TabIndex = 26;
            this.checkBox2.Text = "Reverse";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(229, 126);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(79, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Formatted";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(114, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Column Width";
            // 
            // columnWidthBx
            // 
            this.columnWidthBx.Location = new System.Drawing.Point(114, 74);
            this.columnWidthBx.Name = "columnWidthBx";
            this.columnWidthBx.Size = new System.Drawing.Size(100, 20);
            this.columnWidthBx.TabIndex = 24;
            this.columnWidthBx.Text = "0.2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Row Height";
            // 
            // rowHeightBx
            // 
            this.rowHeightBx.Location = new System.Drawing.Point(6, 74);
            this.rowHeightBx.Name = "rowHeightBx";
            this.rowHeightBx.Size = new System.Drawing.Size(100, 20);
            this.rowHeightBx.TabIndex = 22;
            this.rowHeightBx.Text = "0.2";
            // 
            // genAnimationBtn
            // 
            this.genAnimationBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.genAnimationBtn.Enabled = false;
            this.genAnimationBtn.Location = new System.Drawing.Point(334, 120);
            this.genAnimationBtn.Name = "genAnimationBtn";
            this.genAnimationBtn.Size = new System.Drawing.Size(113, 23);
            this.genAnimationBtn.TabIndex = 21;
            this.genAnimationBtn.Text = "Generate Animation";
            this.genAnimationBtn.UseVisualStyleBackColor = true;
            this.genAnimationBtn.Click += new System.EventHandler(this.genAnimationBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(331, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Start Y";
            // 
            // startYBx
            // 
            this.startYBx.Location = new System.Drawing.Point(331, 32);
            this.startYBx.Name = "startYBx";
            this.startYBx.Size = new System.Drawing.Size(100, 20);
            this.startYBx.TabIndex = 19;
            this.startYBx.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(223, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Start X";
            // 
            // startXBx
            // 
            this.startXBx.Location = new System.Drawing.Point(223, 32);
            this.startXBx.Name = "startXBx";
            this.startXBx.Size = new System.Drawing.Size(100, 20);
            this.startXBx.TabIndex = 17;
            this.startXBx.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(114, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Columns";
            // 
            // columnCntBx
            // 
            this.columnCntBx.Location = new System.Drawing.Point(114, 32);
            this.columnCntBx.Name = "columnCntBx";
            this.columnCntBx.Size = new System.Drawing.Size(100, 20);
            this.columnCntBx.TabIndex = 15;
            this.columnCntBx.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Rows";
            // 
            // rowCntBx
            // 
            this.rowCntBx.Location = new System.Drawing.Point(6, 32);
            this.rowCntBx.Name = "rowCntBx";
            this.rowCntBx.Size = new System.Drawing.Size(100, 20);
            this.rowCntBx.TabIndex = 0;
            this.rowCntBx.Text = "5";
            // 
            // mousePosLbl
            // 
            this.mousePosLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mousePosLbl.AutoSize = true;
            this.mousePosLbl.Location = new System.Drawing.Point(524, 499);
            this.mousePosLbl.Name = "mousePosLbl";
            this.mousePosLbl.Size = new System.Drawing.Size(30, 13);
            this.mousePosLbl.TabIndex = 16;
            this.mousePosLbl.Text = "X: Y:";
            // 
            // JSonBx
            // 
            this.JSonBx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JSonBx.Enabled = false;
            this.JSonBx.Location = new System.Drawing.Point(518, 320);
            this.JSonBx.Name = "JSonBx";
            this.JSonBx.Size = new System.Drawing.Size(453, 135);
            this.JSonBx.TabIndex = 17;
            this.JSonBx.Text = "";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(866, 461);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Play Animation";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // drawPnl
            // 
            this.drawPnl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawPnl.Location = new System.Drawing.Point(12, 12);
            this.drawPnl.Name = "drawPnl";
            this.drawPnl.Size = new System.Drawing.Size(500, 500);
            this.drawPnl.TabIndex = 0;
            this.drawPnl.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPnl_Paint);
            this.drawPnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPnl_MouseDown);
            this.drawPnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPnl_MouseMove);
            this.drawPnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawPnl_MouseUp);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(755, 461);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 23);
            this.button2.TabIndex = 28;
            this.button2.Text = "Play Once";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // spritesheetBx
            // 
            this.spritesheetBx.Location = new System.Drawing.Point(615, 38);
            this.spritesheetBx.Name = "spritesheetBx";
            this.spritesheetBx.Size = new System.Drawing.Size(134, 20);
            this.spritesheetBx.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(518, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "Spritesheet Name";
            // 
            // exportBtn
            // 
            this.exportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportBtn.Enabled = false;
            this.exportBtn.Location = new System.Drawing.Point(644, 461);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(105, 23);
            this.exportBtn.TabIndex = 29;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // animationNameBx
            // 
            this.animationNameBx.Location = new System.Drawing.Point(837, 38);
            this.animationNameBx.Name = "animationNameBx";
            this.animationNameBx.Size = new System.Drawing.Size(134, 20);
            this.animationNameBx.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(752, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "Animation Name";
            // 
            // animationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 526);
            this.Controls.Add(this.animationNameBx);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.spritesheetBx);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.JSonBx);
            this.Controls.Add(this.mousePosLbl);
            this.Controls.Add(this.g1);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.g2);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.pathBx);
            this.Controls.Add(this.drawPnl);
            this.Name = "animationEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Animation Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.animationEditor_FormClosing);
            this.g1.ResumeLayout(false);
            this.g1.PerformLayout();
            this.g2.ResumeLayout(false);
            this.g2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox pathBx;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TextBox xBx;
        private System.Windows.Forms.TextBox yBx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox widthBx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox heightBx;
        private System.Windows.Forms.Button viewBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.GroupBox g1;
        private System.Windows.Forms.GroupBox g2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox columnCntBx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox rowCntBx;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox startYBx;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox startXBx;
        private System.Windows.Forms.Button genAnimationBtn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox columnWidthBx;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox rowHeightBx;
        private System.Windows.Forms.Label mousePosLbl;
        private System.Windows.Forms.RichTextBox JSonBx;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel drawPnl;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox frameDelayBx;
        private System.Windows.Forms.TextBox spritesheetBx;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.TextBox animationNameBx;
        private System.Windows.Forms.Label label13;
    }
}

