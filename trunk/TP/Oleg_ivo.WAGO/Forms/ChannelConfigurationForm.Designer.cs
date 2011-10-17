namespace Oleg_ivo.WAGO.Forms
{
    partial class ChannelConfigurationForm
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
            this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            this.channelsControl1 = new Oleg_ivo.WAGO.Forms.ChannelsControl();
            this.SuspendLayout();
            // 
            // oleDbConnection1
            // 
            this.oleDbConnection1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\\WORK\\Oleg_ivo\\Oleg_ivo.WAGO\\Oleg_" +
                "ivo.WAGO\\test.mdb;Persist Security Info=True";
            // 
            // channelsControl1
            // 
            this.channelsControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.channelsControl1.Location = new System.Drawing.Point(12, 12);
            this.channelsControl1.Name = "channelsControl1";
            this.channelsControl1.Size = new System.Drawing.Size(512, 300);
            this.channelsControl1.TabIndex = 0;
            // 
            // ChannelConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 324);
            this.Controls.Add(this.channelsControl1);
            this.Name = "ChannelConfigurationForm";
            this.Text = "Настройка каналов";
            this.Load += new System.EventHandler(this.ChannelConfigurationForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.OleDb.OleDbConnection oleDbConnection1;
        private ChannelsControl channelsControl1;
    }
}