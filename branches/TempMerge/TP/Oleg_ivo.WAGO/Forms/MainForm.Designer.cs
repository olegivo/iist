namespace Oleg_ivo.WAGO.Forms
{
    partial class MainForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbRS485 = new System.Windows.Forms.RadioButton();
            this.rbEthernet = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.rbRS485);
            this.flowLayoutPanel1.Controls.Add(this.rbEthernet);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(9, 10);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(72, 50);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // rbRS485
            // 
            this.rbRS485.AutoSize = true;
            this.rbRS485.Location = new System.Drawing.Point(2, 2);
            this.rbRS485.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbRS485.Name = "rbRS485";
            this.rbRS485.Size = new System.Drawing.Size(61, 17);
            this.rbRS485.TabIndex = 0;
            this.rbRS485.TabStop = true;
            this.rbRS485.Text = "RS-485";
            this.rbRS485.UseVisualStyleBackColor = true;
            this.rbRS485.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbEthernet
            // 
            this.rbEthernet.AutoSize = true;
            this.rbEthernet.Location = new System.Drawing.Point(2, 23);
            this.rbEthernet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbEthernet.Name = "rbEthernet";
            this.rbEthernet.Size = new System.Drawing.Size(65, 17);
            this.rbEthernet.TabIndex = 1;
            this.rbEthernet.TabStop = true;
            this.rbEthernet.Text = "Ethernet";
            this.rbEthernet.UseVisualStyleBackColor = true;
            this.rbEthernet.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(86, 10);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(56, 50);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Начать";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 67);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rbRS485;
        private System.Windows.Forms.RadioButton rbEthernet;
        private System.Windows.Forms.Button btnStart;
    }
}