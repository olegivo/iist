namespace Oleg_ivo.WAGO.Controls
{
    partial class FieldBusAddressEditForm
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
            this.fieldBusAddressEditControl1 = new Oleg_ivo.WAGO.Controls.FieldBusAddressEditControl();
            this.SuspendLayout();
            // 
            // fieldBusAddressEditControl1
            // 
            this.fieldBusAddressEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldBusAddressEditControl1.Location = new System.Drawing.Point(0, 0);
            this.fieldBusAddressEditControl1.Name = "fieldBusAddressEditControl1";
            this.fieldBusAddressEditControl1.Size = new System.Drawing.Size(516, 347);
            this.fieldBusAddressEditControl1.TabIndex = 0;
            // 
            // FieldBusAddressEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 347);
            this.Controls.Add(this.fieldBusAddressEditControl1);
            this.Name = "FieldBusAddressEditForm";
            this.Text = "Адреса полевых шин";
            this.ResumeLayout(false);

        }

        #endregion

        private FieldBusAddressEditControl fieldBusAddressEditControl1;
    }
}