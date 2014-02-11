using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Oleg_ivo.MES
{
    partial class MesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new TextBox();
            this.btnSendMessageHigh = new Button();
            this.lbRegisteredHigh = new ListBox();
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.lbRegisteredLow = new ListBox();
            this.btnSendMessageLow = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.textBox1.Location = new Point(341, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = ScrollBars.Both;
            this.textBox1.Size = new Size(573, 414);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            // 
            // btnSendMessageHigh
            // 
            this.btnSendMessageHigh.Enabled = false;
            this.btnSendMessageHigh.Location = new Point(8, 129);
            this.btnSendMessageHigh.Name = "btnSendMessageHigh";
            this.btnSendMessageHigh.Size = new Size(323, 23);
            this.btnSendMessageHigh.TabIndex = 3;
            this.btnSendMessageHigh.Text = "Send Time to clients";
            this.btnSendMessageHigh.UseVisualStyleBackColor = true;
            this.btnSendMessageHigh.Click += new EventHandler(this.btnSendMessageHigh_Click);
            // 
            // lbRegisteredHigh
            // 
            this.lbRegisteredHigh.DisplayMember = "Name";
            this.lbRegisteredHigh.Dock = DockStyle.Fill;
            this.lbRegisteredHigh.FormattingEnabled = true;
            this.lbRegisteredHigh.Location = new Point(3, 16);
            this.lbRegisteredHigh.Name = "lbRegisteredHigh";
            this.lbRegisteredHigh.SelectionMode = SelectionMode.MultiExtended;
            this.lbRegisteredHigh.Size = new Size(321, 95);
            this.lbRegisteredHigh.TabIndex = 4;
            this.lbRegisteredHigh.ValueMember = "Value";
            this.lbRegisteredHigh.SelectedIndexChanged += new EventHandler(this.lbRegisteredHigh_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbRegisteredHigh);
            this.groupBox1.Location = new Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(327, 115);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "������� �������� ������";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbRegisteredLow);
            this.groupBox2.Location = new Point(8, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(327, 115);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "������� �������� ������";
            // 
            // lbRegisteredLow
            // 
            this.lbRegisteredLow.DisplayMember = "Name";
            this.lbRegisteredLow.Dock = DockStyle.Fill;
            this.lbRegisteredLow.FormattingEnabled = true;
            this.lbRegisteredLow.Location = new Point(3, 16);
            this.lbRegisteredLow.Name = "lbRegisteredLow";
            this.lbRegisteredLow.SelectionMode = SelectionMode.MultiExtended;
            this.lbRegisteredLow.Size = new Size(321, 95);
            this.lbRegisteredLow.TabIndex = 4;
            this.lbRegisteredLow.ValueMember = "Value";
            this.lbRegisteredLow.SelectedIndexChanged += new EventHandler(this.lbRegisteredLow_SelectedIndexChanged);
            // 
            // btnSendMessageLow
            // 
            this.btnSendMessageLow.Enabled = false;
            this.btnSendMessageLow.Location = new Point(11, 275);
            this.btnSendMessageLow.Name = "btnSendMessageLow";
            this.btnSendMessageLow.Size = new Size(323, 23);
            this.btnSendMessageLow.TabIndex = 12;
            this.btnSendMessageLow.Text = "Send Time to clients";
            this.btnSendMessageLow.UseVisualStyleBackColor = true;
            this.btnSendMessageLow.Click += new EventHandler(this.btnSendMessageLow_Click);
            // 
            // MesForm
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(926, 467);
            this.Controls.Add(this.btnSendMessageLow);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSendMessageHigh);
            this.Controls.Add(this.textBox1);
            this.Name = "MesForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Message Exchange System (MES)";
            this.Load += new EventHandler(this.MesForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox1;
        private Button btnSendMessageHigh;
        private ListBox lbRegisteredHigh;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ListBox lbRegisteredLow;
        private Button btnSendMessageLow;
    }
}

