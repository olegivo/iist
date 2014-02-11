namespace Oleg_ivo.WAGO.Forms
{
    partial class TestChannelsForm
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
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.tbWriteValue = new System.Windows.Forms.TextBox();
            this.tbReadValue = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWrite.Location = new System.Drawing.Point(12, 13);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(2);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(99, 94);
            this.btnWrite.TabIndex = 2;
            this.btnWrite.Text = "Write <-";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRead.Location = new System.Drawing.Point(12, 13);
            this.btnRead.Margin = new System.Windows.Forms.Padding(2);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(99, 102);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "Read ->";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // tbWriteValue
            // 
            this.tbWriteValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWriteValue.Location = new System.Drawing.Point(115, 13);
            this.tbWriteValue.Margin = new System.Windows.Forms.Padding(2);
            this.tbWriteValue.Multiline = true;
            this.tbWriteValue.Name = "tbWriteValue";
            this.tbWriteValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbWriteValue.Size = new System.Drawing.Size(675, 94);
            this.tbWriteValue.TabIndex = 4;
            // 
            // tbReadValue
            // 
            this.tbReadValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReadValue.Location = new System.Drawing.Point(115, 13);
            this.tbReadValue.Margin = new System.Windows.Forms.Padding(2);
            this.tbReadValue.Multiline = true;
            this.tbReadValue.Name = "tbReadValue";
            this.tbReadValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbReadValue.Size = new System.Drawing.Size(675, 103);
            this.tbReadValue.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbWriteValue);
            this.splitContainer1.Panel1.Controls.Add(this.btnWrite);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbReadValue);
            this.splitContainer1.Panel2.Controls.Add(this.btnRead);
            this.splitContainer1.Size = new System.Drawing.Size(810, 259);
            this.splitContainer1.SplitterDistance = 129;
            this.splitContainer1.TabIndex = 5;
            // 
            // TestChannelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 292);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TestChannelsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestChannelsForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox tbWriteValue;
        private System.Windows.Forms.TextBox tbReadValue;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}