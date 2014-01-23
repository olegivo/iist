namespace TP.CommonView
{
    partial class ucCommonCharts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCommonCharts));
            this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage9 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucChart1 = new UICommon.ucChart();
            this.ucChart2 = new UICommon.ucChart();
            this.xtraTabPage10 = new DevExpress.XtraTab.XtraTabPage();
            this.ucChart3 = new UICommon.ucChart();
            this.xtraTabPage11 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucChart6 = new UICommon.ucChart();
            this.ucChart5 = new UICommon.ucChart();
            this.ucChart4 = new UICommon.ucChart();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl5 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucChart7 = new UICommon.ucChart();
            this.ucChart8 = new UICommon.ucChart();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).BeginInit();
            this.xtraTabControl2.SuspendLayout();
            this.xtraTabPage9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.xtraTabPage10.SuspendLayout();
            this.xtraTabPage11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            this.splitContainerControl3.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl5)).BeginInit();
            this.splitContainerControl5.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl2.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl2.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtraTabControl2.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.xtraTabPage9;
            this.xtraTabControl2.Size = new System.Drawing.Size(640, 480);
            this.xtraTabControl2.TabIndex = 1;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage9,
            this.xtraTabPage10,
            this.xtraTabPage11,
            this.xtraTabPage1});
            // 
            // xtraTabPage9
            // 
            this.xtraTabPage9.Controls.Add(this.splitContainerControl1);
            this.xtraTabPage9.Name = "xtraTabPage9";
            this.xtraTabPage9.Size = new System.Drawing.Size(556, 474);
            this.xtraTabPage9.Text = "Температура";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ucChart1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.ucChart2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(556, 474);
            this.splitContainerControl1.SplitterPosition = 191;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ucChart1
            // 
            this.ucChart1.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart1.ChannelsToDisplay")));
            this.ucChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart1.Location = new System.Drawing.Point(0, 0);
            this.ucChart1.Name = "ucChart1";
            this.ucChart1.Size = new System.Drawing.Size(556, 191);
            this.ucChart1.TabIndex = 0;
            // 
            // ucChart2
            // 
            this.ucChart2.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart2.ChannelsToDisplay")));
            this.ucChart2.ChartDataStorageTime = 40;
            this.ucChart2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart2.Location = new System.Drawing.Point(0, 0);
            this.ucChart2.Name = "ucChart2";
            this.ucChart2.Size = new System.Drawing.Size(556, 278);
            this.ucChart2.TabIndex = 0;
            // 
            // xtraTabPage10
            // 
            this.xtraTabPage10.Controls.Add(this.ucChart3);
            this.xtraTabPage10.Name = "xtraTabPage10";
            this.xtraTabPage10.Size = new System.Drawing.Size(556, 474);
            this.xtraTabPage10.Text = "Уровень";
            // 
            // ucChart3
            // 
            this.ucChart3.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart3.ChannelsToDisplay")));
            this.ucChart3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart3.Location = new System.Drawing.Point(0, 0);
            this.ucChart3.Name = "ucChart3";
            this.ucChart3.Size = new System.Drawing.Size(556, 474);
            this.ucChart3.TabIndex = 0;
            // 
            // xtraTabPage11
            // 
            this.xtraTabPage11.Controls.Add(this.splitContainerControl2);
            this.xtraTabPage11.Name = "xtraTabPage11";
            this.xtraTabPage11.Size = new System.Drawing.Size(556, 474);
            this.xtraTabPage11.Text = "Газ";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.splitContainerControl3);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.ucChart4);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainerControl2.Size = new System.Drawing.Size(556, 474);
            this.splitContainerControl2.SplitterPosition = 241;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // splitContainerControl3
            // 
            this.splitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl3.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl3.Horizontal = false;
            this.splitContainerControl3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl3.Name = "splitContainerControl3";
            this.splitContainerControl3.Panel1.Controls.Add(this.ucChart6);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            this.splitContainerControl3.Panel2.Controls.Add(this.ucChart5);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.splitContainerControl3.Size = new System.Drawing.Size(556, 241);
            this.splitContainerControl3.SplitterPosition = 123;
            this.splitContainerControl3.TabIndex = 0;
            this.splitContainerControl3.Text = "splitContainerControl3";
            // 
            // ucChart6
            // 
            this.ucChart6.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart6.ChannelsToDisplay")));
            this.ucChart6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart6.Location = new System.Drawing.Point(0, 0);
            this.ucChart6.Name = "ucChart6";
            this.ucChart6.Size = new System.Drawing.Size(556, 123);
            this.ucChart6.TabIndex = 1;
            // 
            // ucChart5
            // 
            this.ucChart5.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart5.ChannelsToDisplay")));
            this.ucChart5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart5.Location = new System.Drawing.Point(0, 0);
            this.ucChart5.Name = "ucChart5";
            this.ucChart5.Size = new System.Drawing.Size(556, 113);
            this.ucChart5.TabIndex = 0;
            // 
            // ucChart4
            // 
            this.ucChart4.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart4.ChannelsToDisplay")));
            this.ucChart4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart4.Location = new System.Drawing.Point(0, 0);
            this.ucChart4.Name = "ucChart4";
            this.ucChart4.Size = new System.Drawing.Size(556, 228);
            this.ucChart4.TabIndex = 1;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.splitContainerControl5);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(556, 474);
            this.xtraTabPage1.Text = "Разное";
            // 
            // splitContainerControl5
            // 
            this.splitContainerControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl5.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl5.Horizontal = false;
            this.splitContainerControl5.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl5.Name = "splitContainerControl5";
            this.splitContainerControl5.Panel1.Controls.Add(this.ucChart7);
            this.splitContainerControl5.Panel1.Text = "Panel1";
            this.splitContainerControl5.Panel2.Controls.Add(this.ucChart8);
            this.splitContainerControl5.Panel2.Text = "Panel2";
            this.splitContainerControl5.Size = new System.Drawing.Size(556, 474);
            this.splitContainerControl5.SplitterPosition = 237;
            this.splitContainerControl5.TabIndex = 0;
            this.splitContainerControl5.Text = "splitContainerControl5";
            // 
            // ucChart7
            // 
            this.ucChart7.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart7.ChannelsToDisplay")));
            this.ucChart7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart7.Location = new System.Drawing.Point(0, 0);
            this.ucChart7.Name = "ucChart7";
            this.ucChart7.Size = new System.Drawing.Size(556, 237);
            this.ucChart7.TabIndex = 1;
            // 
            // ucChart8
            // 
            this.ucChart8.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart8.ChannelsToDisplay")));
            this.ucChart8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart8.Location = new System.Drawing.Point(0, 0);
            this.ucChart8.Name = "ucChart8";
            this.ucChart8.Size = new System.Drawing.Size(556, 232);
            this.ucChart8.TabIndex = 2;
            // 
            // ucCommonCharts
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl2);
            this.Name = "ucCommonCharts";
            this.Size = new System.Drawing.Size(640, 480);
            this.ParentChanged += new System.EventHandler(this.ucCommonParentChanged);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).EndInit();
            this.xtraTabControl2.ResumeLayout(false);
            this.xtraTabPage9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.xtraTabPage10.ResumeLayout(false);
            this.xtraTabPage11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl5)).EndInit();
            this.splitContainerControl5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage9;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private UICommon.ucChart ucChart1;
        private UICommon.ucChart ucChart2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage10;
        private UICommon.ucChart ucChart3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage11;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl3;
        private UICommon.ucChart ucChart5;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl5;
        private UICommon.ucChart ucChart7;
        private UICommon.ucChart ucChart8;
        private UICommon.ucChart ucChart6;
        private UICommon.ucChart ucChart4;
    }
}
