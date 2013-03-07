using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Ports
{
    partial class frmFieldBus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFieldBus));
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ucFieldBusTypeCombobox1 = new ucFieldBusTypeCombobox();
            this.label2 = new System.Windows.Forms.Label();
            this.ucDevicePortCombobox1 = new ucDevicePortCombobox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тип полевой шины";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(317, 72);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 39);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(398, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 39);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ucFieldBusTypeCombobox1
            // 
            this.ucFieldBusTypeCombobox1.DisplayMember = "Description";
            this.ucFieldBusTypeCombobox1.FormattingEnabled = true;
            this.ucFieldBusTypeCombobox1.Location = new System.Drawing.Point(151, 12);
            this.ucFieldBusTypeCombobox1.Name = "ucFieldBusTypeCombobox1";
            this.ucFieldBusTypeCombobox1.Size = new System.Drawing.Size(322, 24);
            this.ucFieldBusTypeCombobox1.TabIndex = 6;
            this.ucFieldBusTypeCombobox1.ValueMember = "Value";
            this.ucFieldBusTypeCombobox1.SelectedIndexChanged += new System.EventHandler(this.ucFieldBusTypeCombobox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Порт подключения к шине";
            // 
            // ucDevicePortCombobox1
            // 
            this.ucDevicePortCombobox1.DisplayMember = "Description";
            this.ucDevicePortCombobox1.Enabled = false;
            this.ucDevicePortCombobox1.FormattingEnabled = true;
            this.ucDevicePortCombobox1.Location = new System.Drawing.Point(203, 42);
            this.ucDevicePortCombobox1.Name = "ucDevicePortCombobox1";
            this.ucDevicePortCombobox1.Size = new System.Drawing.Size(270, 24);
            this.ucDevicePortCombobox1.TabIndex = 8;
            this.ucDevicePortCombobox1.ValueMember = "Value";
            this.ucDevicePortCombobox1.SelectedIndexChanged += new System.EventHandler(this.ucFieldBusTypeCombobox1_SelectedIndexChanged);
            // 
            // frmFieldBus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 119);
            this.Controls.Add(this.ucDevicePortCombobox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucFieldBusTypeCombobox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Name = "frmFieldBus";
            this.Text = "Полевая шина";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private ucFieldBusTypeCombobox ucFieldBusTypeCombobox1;
        private System.Windows.Forms.Label label2;
        private ucDevicePortCombobox ucDevicePortCombobox1;
    }
}