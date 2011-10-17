namespace Oleg_ivo.MES
{
    partial class MesForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSendMessageHigh = new System.Windows.Forms.Button();
            this.lbRegisteredHigh = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbRegisteredLow = new System.Windows.Forms.ListBox();
            this.btnSendMessageLow = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(341, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(573, 414);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnSendMessageHigh
            // 
            this.btnSendMessageHigh.Enabled = false;
            this.btnSendMessageHigh.Location = new System.Drawing.Point(8, 129);
            this.btnSendMessageHigh.Name = "btnSendMessageHigh";
            this.btnSendMessageHigh.Size = new System.Drawing.Size(323, 23);
            this.btnSendMessageHigh.TabIndex = 3;
            this.btnSendMessageHigh.Text = "Send Time to clients";
            this.btnSendMessageHigh.UseVisualStyleBackColor = true;
            this.btnSendMessageHigh.Click += new System.EventHandler(this.btnSendMessageHigh_Click);
            // 
            // lbRegisteredHigh
            // 
            this.lbRegisteredHigh.DisplayMember = "Name";
            this.lbRegisteredHigh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRegisteredHigh.FormattingEnabled = true;
            this.lbRegisteredHigh.Location = new System.Drawing.Point(3, 16);
            this.lbRegisteredHigh.Name = "lbRegisteredHigh";
            this.lbRegisteredHigh.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbRegisteredHigh.Size = new System.Drawing.Size(321, 95);
            this.lbRegisteredHigh.TabIndex = 4;
            this.lbRegisteredHigh.ValueMember = "Value";
            this.lbRegisteredHigh.SelectedIndexChanged += new System.EventHandler(this.lbRegisteredHigh_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbRegisteredHigh);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 115);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Клиенты верхнего уровня";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbRegisteredLow);
            this.groupBox2.Location = new System.Drawing.Point(8, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(327, 115);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Клиенты верхнего уровня";
            // 
            // lbRegisteredLow
            // 
            this.lbRegisteredLow.DisplayMember = "Name";
            this.lbRegisteredLow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRegisteredLow.FormattingEnabled = true;
            this.lbRegisteredLow.Location = new System.Drawing.Point(3, 16);
            this.lbRegisteredLow.Name = "lbRegisteredLow";
            this.lbRegisteredLow.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbRegisteredLow.Size = new System.Drawing.Size(321, 95);
            this.lbRegisteredLow.TabIndex = 4;
            this.lbRegisteredLow.ValueMember = "Value";
            this.lbRegisteredLow.SelectedIndexChanged += new System.EventHandler(this.lbRegisteredLow_SelectedIndexChanged);
            // 
            // btnSendMessageLow
            // 
            this.btnSendMessageLow.Enabled = false;
            this.btnSendMessageLow.Location = new System.Drawing.Point(11, 275);
            this.btnSendMessageLow.Name = "btnSendMessageLow";
            this.btnSendMessageLow.Size = new System.Drawing.Size(323, 23);
            this.btnSendMessageLow.TabIndex = 12;
            this.btnSendMessageLow.Text = "Send Time to clients";
            this.btnSendMessageLow.UseVisualStyleBackColor = true;
            this.btnSendMessageLow.Click += new System.EventHandler(this.btnSendMessageLow_Click);
            // 
            // MesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 467);
            this.Controls.Add(this.btnSendMessageLow);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSendMessageHigh);
            this.Controls.Add(this.textBox1);
            this.Name = "MesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message Exchange System (MES)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSendMessageHigh;
        private System.Windows.Forms.ListBox lbRegisteredHigh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbRegisteredLow;
        private System.Windows.Forms.Button btnSendMessageLow;
    }
}

