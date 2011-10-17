namespace Oleg_ivo.WAGO.Forms
{
    partial class TestIOForm
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
            this.btnRead = new System.Windows.Forms.Button();
            this.tbReadAddress = new System.Windows.Forms.TextBox();
            this.tbWriteAddress = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnWrite = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSyncW2R = new System.Windows.Forms.Button();
            this.tbWriteValue = new System.Windows.Forms.TextBox();
            this.tbReadValue = new System.Windows.Forms.TextBox();
            this.cbReadType = new System.Windows.Forms.ComboBox();
            this.tbNumbers = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbWriteType = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSyncR2W = new System.Windows.Forms.Button();
            this.wagoPlcManager1 = new Oleg_ivo.WAGO.WagoPlcManager();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Location = new System.Drawing.Point(7, 40);
            this.btnRead.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(99, 122);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "Read ->";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // tbReadAddress
            // 
            this.tbReadAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReadAddress.Location = new System.Drawing.Point(110, 17);
            this.tbReadAddress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbReadAddress.Name = "tbReadAddress";
            this.tbReadAddress.Size = new System.Drawing.Size(60, 20);
            this.tbReadAddress.TabIndex = 1;
            // 
            // tbWriteAddress
            // 
            this.tbWriteAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWriteAddress.Location = new System.Drawing.Point(110, 12);
            this.tbWriteAddress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbWriteAddress.Name = "tbWriteAddress";
            this.tbWriteAddress.Size = new System.Drawing.Size(67, 20);
            this.tbWriteAddress.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 383);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(436, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusText
            // 
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(0, 17);
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWrite.Location = new System.Drawing.Point(7, 37);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(99, 114);
            this.btnWrite.TabIndex = 0;
            this.btnWrite.Text = "Write <-";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Адрес для чтения";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Адрес для записи";
            // 
            // btnSyncW2R
            // 
            this.btnSyncW2R.Location = new System.Drawing.Point(314, 184);
            this.btnSyncW2R.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSyncW2R.Name = "btnSyncW2R";
            this.btnSyncW2R.Size = new System.Drawing.Size(112, 34);
            this.btnSyncW2R.TabIndex = 4;
            this.btnSyncW2R.Text = "Адрес записи-> адрес чтения";
            this.btnSyncW2R.UseVisualStyleBackColor = true;
            this.btnSyncW2R.Click += new System.EventHandler(this.btnSyncW2R_Click);
            // 
            // tbWriteValue
            // 
            this.tbWriteValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWriteValue.Location = new System.Drawing.Point(110, 37);
            this.tbWriteValue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbWriteValue.Multiline = true;
            this.tbWriteValue.Name = "tbWriteValue";
            this.tbWriteValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbWriteValue.Size = new System.Drawing.Size(304, 114);
            this.tbWriteValue.TabIndex = 1;
            // 
            // tbReadValue
            // 
            this.tbReadValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReadValue.Location = new System.Drawing.Point(110, 40);
            this.tbReadValue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbReadValue.Multiline = true;
            this.tbReadValue.Name = "tbReadValue";
            this.tbReadValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbReadValue.Size = new System.Drawing.Size(304, 123);
            this.tbReadValue.TabIndex = 1;
            // 
            // cbReadType
            // 
            this.cbReadType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbReadType.FormattingEnabled = true;
            this.cbReadType.Location = new System.Drawing.Point(174, 17);
            this.cbReadType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbReadType.Name = "cbReadType";
            this.cbReadType.Size = new System.Drawing.Size(150, 21);
            this.cbReadType.TabIndex = 5;
            // 
            // tbNumbers
            // 
            this.tbNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNumbers.Location = new System.Drawing.Point(327, 17);
            this.tbNumbers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNumbers.Name = "tbNumbers";
            this.tbNumbers.Size = new System.Drawing.Size(87, 20);
            this.tbNumbers.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnRead);
            this.groupBox1.Controls.Add(this.cbReadType);
            this.groupBox1.Controls.Add(this.tbReadAddress);
            this.groupBox1.Controls.Add(this.tbNumbers);
            this.groupBox1.Controls.Add(this.tbReadValue);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(418, 170);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Чтение";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnWrite);
            this.groupBox2.Controls.Add(this.cbWriteType);
            this.groupBox2.Controls.Add(this.tbWriteValue);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.tbWriteAddress);
            this.groupBox2.Location = new System.Drawing.Point(9, 223);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(418, 161);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Запись";
            // 
            // cbWriteType
            // 
            this.cbWriteType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWriteType.FormattingEnabled = true;
            this.cbWriteType.Location = new System.Drawing.Point(181, 12);
            this.cbWriteType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbWriteType.Name = "cbWriteType";
            this.cbWriteType.Size = new System.Drawing.Size(143, 21);
            this.cbWriteType.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(327, 14);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(87, 20);
            this.textBox1.TabIndex = 1;
            // 
            // btnSyncR2W
            // 
            this.btnSyncR2W.Location = new System.Drawing.Point(9, 184);
            this.btnSyncR2W.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSyncR2W.Name = "btnSyncR2W";
            this.btnSyncR2W.Size = new System.Drawing.Size(112, 34);
            this.btnSyncR2W.TabIndex = 4;
            this.btnSyncR2W.Text = "Адрес чтения-> адрес записи";
            this.btnSyncR2W.UseVisualStyleBackColor = true;
            this.btnSyncR2W.Click += new System.EventHandler(this.btnSyncR2W_Click);
            // 
            // TestIOForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 405);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSyncR2W);
            this.Controls.Add(this.btnSyncW2R);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(422, 326);
            this.Name = "TestIOForm";
            this.Text = "TestIOForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox tbReadAddress;
        private System.Windows.Forms.TextBox tbWriteAddress;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusText;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSyncW2R;
        private System.Windows.Forms.TextBox tbWriteValue;
        private System.Windows.Forms.TextBox tbReadValue;
        private System.Windows.Forms.ComboBox cbReadType;
        private System.Windows.Forms.TextBox tbNumbers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbWriteType;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSyncR2W;
        private WagoPlcManager wagoPlcManager1;
    }
}