namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    partial class LevelEditControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpFieldBuses = new System.Windows.Forms.TabPage();
            this.tpFieldNodes = new System.Windows.Forms.TabPage();
            this.tpPChannels = new System.Windows.Forms.TabPage();
            this.tpLChannels = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFill = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.fieldBusEditControl1 = new Oleg_ivo.WAGO.Controls.LevelEditors.FieldBusEditControl();
            this.fieldBusNodeEditControl1 = new Oleg_ivo.WAGO.Controls.LevelEditors.FieldBusNodeEditControl();
            this.physicalChannelEditControl1 = new Oleg_ivo.WAGO.Controls.LevelEditors.PhysicalChannelEditControl();
            this.logicalChannelEditControl1 = new Oleg_ivo.WAGO.Controls.LevelEditors.LogicalChannelEditControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpFieldBuses.SuspendLayout();
            this.tpFieldNodes.SuspendLayout();
            this.tpPChannels.SuspendLayout();
            this.tpLChannels.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(544, 263);
            this.splitContainer1.SplitterDistance = 231;
            this.splitContainer1.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpFieldBuses);
            this.tabControl1.Controls.Add(this.tpFieldNodes);
            this.tabControl1.Controls.Add(this.tpPChannels);
            this.tabControl1.Controls.Add(this.tpLChannels);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(544, 231);
            this.tabControl1.TabIndex = 6;
            // 
            // tpFieldBuses
            // 
            this.tpFieldBuses.AutoScroll = true;
            this.tpFieldBuses.Controls.Add(this.fieldBusEditControl1);
            this.tpFieldBuses.Location = new System.Drawing.Point(4, 22);
            this.tpFieldBuses.Name = "tpFieldBuses";
            this.tpFieldBuses.Padding = new System.Windows.Forms.Padding(3);
            this.tpFieldBuses.Size = new System.Drawing.Size(536, 205);
            this.tpFieldBuses.TabIndex = 3;
            this.tpFieldBuses.Text = "Полевые шины";
            this.tpFieldBuses.UseVisualStyleBackColor = true;
            // 
            // tpFieldNodes
            // 
            this.tpFieldNodes.AutoScroll = true;
            this.tpFieldNodes.Controls.Add(this.fieldBusNodeEditControl1);
            this.tpFieldNodes.Location = new System.Drawing.Point(4, 22);
            this.tpFieldNodes.Name = "tpFieldNodes";
            this.tpFieldNodes.Padding = new System.Windows.Forms.Padding(3);
            this.tpFieldNodes.Size = new System.Drawing.Size(536, 205);
            this.tpFieldNodes.TabIndex = 2;
            this.tpFieldNodes.Text = "Узлы";
            this.tpFieldNodes.UseVisualStyleBackColor = true;
            // 
            // tpPChannels
            // 
            this.tpPChannels.AutoScroll = true;
            this.tpPChannels.Controls.Add(this.physicalChannelEditControl1);
            this.tpPChannels.Location = new System.Drawing.Point(4, 22);
            this.tpPChannels.Name = "tpPChannels";
            this.tpPChannels.Padding = new System.Windows.Forms.Padding(3);
            this.tpPChannels.Size = new System.Drawing.Size(536, 205);
            this.tpPChannels.TabIndex = 0;
            this.tpPChannels.Text = "Физ. каналы";
            this.tpPChannels.UseVisualStyleBackColor = true;
            // 
            // tpLChannels
            // 
            this.tpLChannels.AutoScroll = true;
            this.tpLChannels.Controls.Add(this.logicalChannelEditControl1);
            this.tpLChannels.Location = new System.Drawing.Point(4, 22);
            this.tpLChannels.Name = "tpLChannels";
            this.tpLChannels.Padding = new System.Windows.Forms.Padding(3);
            this.tpLChannels.Size = new System.Drawing.Size(536, 205);
            this.tpLChannels.TabIndex = 1;
            this.tpLChannels.Text = "Лог. каналы";
            this.tpLChannels.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnFill);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(544, 28);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point(3, 3);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(50, 23);
            this.btnFill.TabIndex = 6;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(59, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(45, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // fieldBusEditControl1
            // 
            this.fieldBusEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldBusEditControl1.Location = new System.Drawing.Point(3, 3);
            this.fieldBusEditControl1.Name = "fieldBusEditControl1";
            this.fieldBusEditControl1.Size = new System.Drawing.Size(530, 199);
            this.fieldBusEditControl1.TabIndex = 0;
            // 
            // fieldBusNodeEditControl1
            // 
            this.fieldBusNodeEditControl1.AutoScroll = true;
            this.fieldBusNodeEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldBusNodeEditControl1.Location = new System.Drawing.Point(3, 3);
            this.fieldBusNodeEditControl1.Name = "fieldBusNodeEditControl1";
            this.fieldBusNodeEditControl1.Size = new System.Drawing.Size(530, 199);
            this.fieldBusNodeEditControl1.TabIndex = 0;
            // 
            // physicalChannelEditControl1
            // 
            this.physicalChannelEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.physicalChannelEditControl1.Location = new System.Drawing.Point(3, 3);
            this.physicalChannelEditControl1.Name = "physicalChannelEditControl1";
            this.physicalChannelEditControl1.Size = new System.Drawing.Size(530, 199);
            this.physicalChannelEditControl1.TabIndex = 0;
            // 
            // logicalChannelEditControl1
            // 
            this.logicalChannelEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logicalChannelEditControl1.Location = new System.Drawing.Point(3, 3);
            this.logicalChannelEditControl1.Name = "logicalChannelEditControl1";
            this.logicalChannelEditControl1.Size = new System.Drawing.Size(530, 199);
            this.logicalChannelEditControl1.TabIndex = 0;
            // 
            // LevelEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "LevelEditControl";
            this.Size = new System.Drawing.Size(544, 263);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpFieldBuses.ResumeLayout(false);
            this.tpFieldNodes.ResumeLayout(false);
            this.tpPChannels.ResumeLayout(false);
            this.tpLChannels.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpLChannels;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabPage tpFieldNodes;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpPChannels;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tpFieldBuses;
        private FieldBusEditControl fieldBusEditControl1;
        private FieldBusNodeEditControl fieldBusNodeEditControl1;
        private LogicalChannelEditControl logicalChannelEditControl1;
        private PhysicalChannelEditControl physicalChannelEditControl1;

    }
}