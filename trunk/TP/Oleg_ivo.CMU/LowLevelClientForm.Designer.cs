namespace Oleg_ivo.CMU
{
    partial class LowLevelClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.doubleListBoxControl1 = new Oleg_ivo.Tools.UI.DoubleListBoxControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChannelRead = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.btnUnregister = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // doubleListBoxControl1
            // 
            this.doubleListBoxControl1.Location = new System.Drawing.Point(135, 19);
            this.doubleListBoxControl1.Name = "doubleListBoxControl1";
            this.doubleListBoxControl1.Size = new System.Drawing.Size(486, 159);
            this.doubleListBoxControl1.TabIndex = 7;
            this.doubleListBoxControl1.SelectionRightChanged += new System.EventHandler(this.doubleListBoxControl1_SelectionRightChanged);
            this.doubleListBoxControl1.ItemMoved += new System.EventHandler<Oleg_ivo.Tools.UI.MovedEventArgs>(this.doubleListBoxControl1_ItemMoved);
            this.doubleListBoxControl1.ItemMoving += new System.EventHandler<Oleg_ivo.Tools.UI.MovingEventArgs>(this.doubleListBoxControl1_ItemMoving);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnChannelRead);
            this.groupBox1.Controls.Add(this.doubleListBoxControl1);
            this.groupBox1.Controls.Add(this.btnRegister);
            this.groupBox1.Controls.Add(this.btnSendMessage);
            this.groupBox1.Controls.Add(this.btnUnregister);
            this.groupBox1.Location = new System.Drawing.Point(12, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(629, 186);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Доступные действия";
            // 
            // btnChannelRead
            // 
            this.btnChannelRead.Enabled = false;
            this.btnChannelRead.Location = new System.Drawing.Point(6, 155);
            this.btnChannelRead.Name = "btnChannelRead";
            this.btnChannelRead.Size = new System.Drawing.Size(123, 23);
            this.btnChannelRead.TabIndex = 10;
            this.btnChannelRead.Text = "ChannelRead";
            this.btnChannelRead.UseVisualStyleBackColor = true;
            this.btnChannelRead.Click += new System.EventHandler(this.btnChannelRead_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(6, 19);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(123, 23);
            this.btnRegister.TabIndex = 3;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Enabled = false;
            this.btnSendMessage.Location = new System.Drawing.Point(6, 77);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(123, 23);
            this.btnSendMessage.TabIndex = 2;
            this.btnSendMessage.Text = "Send Time to service";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // btnUnregister
            // 
            this.btnUnregister.Enabled = false;
            this.btnUnregister.Location = new System.Drawing.Point(6, 48);
            this.btnUnregister.Name = "btnUnregister";
            this.btnUnregister.Size = new System.Drawing.Size(123, 23);
            this.btnUnregister.TabIndex = 4;
            this.btnUnregister.Text = "Unregister";
            this.btnUnregister.UseVisualStyleBackColor = true;
            this.btnUnregister.Click += new System.EventHandler(this.btnUnregister_Click);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(12, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(629, 20);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "LowLevelClient1";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.HideSelection = false;
            this.textBox1.Location = new System.Drawing.Point(12, 38);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(629, 152);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // LowLevelClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 394);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "LowLevelClientForm";
            this.Text = "LowLevelClientForm";
            this.Load += new System.EventHandler(this.LowLevelClientForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Oleg_ivo.Tools.UI.DoubleListBoxControl doubleListBoxControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Button btnUnregister;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnChannelRead;
    }
}

