namespace Oleg_ivo.WAGO.Controls
{
    partial class FieldBusAddressEditControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpEthernet = new System.Windows.Forms.TabPage();
            this.fieldBusNodesEditControl1 = new Oleg_ivo.WAGO.Controls.FieldBusNodesEditControl();
            this.tpRS485 = new System.Windows.Forms.TabPage();
            this.fieldBusNodesEditControl2 = new Oleg_ivo.WAGO.Controls.FieldBusNodesEditControl();
            this.tpRS232 = new System.Windows.Forms.TabPage();
            this.fieldBusNodesEditControl3 = new Oleg_ivo.WAGO.Controls.FieldBusNodesEditControl();
            this.tabControl1.SuspendLayout();
            this.tpEthernet.SuspendLayout();
            this.tpRS485.SuspendLayout();
            this.tpRS232.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpEthernet);
            this.tabControl1.Controls.Add(this.tpRS485);
            this.tabControl1.Controls.Add(this.tpRS232);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(388, 315);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpEthernet
            // 
            this.tpEthernet.Controls.Add(this.fieldBusNodesEditControl1);
            this.tpEthernet.Location = new System.Drawing.Point(4, 22);
            this.tpEthernet.Name = "tpEthernet";
            this.tpEthernet.Padding = new System.Windows.Forms.Padding(3);
            this.tpEthernet.Size = new System.Drawing.Size(380, 289);
            this.tpEthernet.TabIndex = 0;
            this.tpEthernet.Text = "Ethernet";
            this.tpEthernet.UseVisualStyleBackColor = true;
            // 
            // fieldBusNodesEditControl1
            // 
            this.fieldBusNodesEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldBusNodesEditControl1.FieldBusType = Oleg_ivo.Plc.FieldBus.FieldBusType.Ethernet;
            this.fieldBusNodesEditControl1.Location = new System.Drawing.Point(3, 3);
            this.fieldBusNodesEditControl1.Name = "fieldBusNodesEditControl1";
            this.fieldBusNodesEditControl1.Size = new System.Drawing.Size(374, 283);
            this.fieldBusNodesEditControl1.TabIndex = 0;
            // 
            // tpRS485
            // 
            this.tpRS485.Controls.Add(this.fieldBusNodesEditControl2);
            this.tpRS485.Location = new System.Drawing.Point(4, 22);
            this.tpRS485.Name = "tpRS485";
            this.tpRS485.Padding = new System.Windows.Forms.Padding(3);
            this.tpRS485.Size = new System.Drawing.Size(380, 289);
            this.tpRS485.TabIndex = 1;
            this.tpRS485.Text = "RS-485";
            this.tpRS485.UseVisualStyleBackColor = true;
            // 
            // fieldBusNodesEditControl2
            // 
            this.fieldBusNodesEditControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldBusNodesEditControl2.FieldBusType = Oleg_ivo.Plc.FieldBus.FieldBusType.RS485;
            this.fieldBusNodesEditControl2.Location = new System.Drawing.Point(3, 3);
            this.fieldBusNodesEditControl2.Name = "fieldBusNodesEditControl2";
            this.fieldBusNodesEditControl2.Size = new System.Drawing.Size(374, 283);
            this.fieldBusNodesEditControl2.TabIndex = 1;
            // 
            // tpRS232
            // 
            this.tpRS232.Controls.Add(this.fieldBusNodesEditControl3);
            this.tpRS232.Location = new System.Drawing.Point(4, 22);
            this.tpRS232.Name = "tpRS232";
            this.tpRS232.Padding = new System.Windows.Forms.Padding(3);
            this.tpRS232.Size = new System.Drawing.Size(380, 289);
            this.tpRS232.TabIndex = 2;
            this.tpRS232.Text = "RS-232";
            this.tpRS232.UseVisualStyleBackColor = true;
            // 
            // fieldBusNodesEditControl3
            // 
            this.fieldBusNodesEditControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldBusNodesEditControl3.FieldBusType = Oleg_ivo.Plc.FieldBus.FieldBusType.RS232;
            this.fieldBusNodesEditControl3.Location = new System.Drawing.Point(3, 3);
            this.fieldBusNodesEditControl3.Name = "fieldBusNodesEditControl3";
            this.fieldBusNodesEditControl3.Size = new System.Drawing.Size(374, 283);
            this.fieldBusNodesEditControl3.TabIndex = 1;
            // 
            // FieldBusAddressEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "FieldBusAddressEditControl";
            this.Size = new System.Drawing.Size(388, 315);
            this.Load += new System.EventHandler(this.FieldBusAddressEditControl_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpEthernet.ResumeLayout(false);
            this.tpRS485.ResumeLayout(false);
            this.tpRS232.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FieldBusNodesEditControl fieldBusNodesEditControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpEthernet;
        private System.Windows.Forms.TabPage tpRS485;
        private System.Windows.Forms.TabPage tpRS232;
        private FieldBusNodesEditControl fieldBusNodesEditControl2;
        private FieldBusNodesEditControl fieldBusNodesEditControl3;
    }
}
