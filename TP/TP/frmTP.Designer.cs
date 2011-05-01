using DevExpress.XtraBars;
using TP.CyclonAndScrubber;

namespace TP
{
    partial class frmTP
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
            this.components = new System.ComponentModel.Container();
            this.drumTypeFurnace1 = new TP.DrumTypeFurnace.DrumTypeFurnace();
            this.ucFinishCleaning1 = new TP.FinishCleaning.ucFinishCleaning();
            this.ucAllHeatExchanger1 = new TP.HeatExchanger.ucAllHeatExchanger();
            this.ucReheatChamber1 = new TP.ReheatChamber.ucReheatChamber();
            this.ucCyclonAndScrubber1 = new TP.CyclonAndScrubber.ucCyclonAndScrubber();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.miSkin = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.miLookAndFeel = new DevExpress.XtraBars.BarSubItem();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.channelController1 = new TP.ChannelController(this.components);
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.sbUnregister = new DevExpress.XtraEditors.SimpleButton();
            this.sbRegister = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            this.xtraTabPage5.SuspendLayout();
            this.xtraTabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // drumTypeFurnace1
            // 
            this.drumTypeFurnace1.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            this.drumTypeFurnace1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.drumTypeFurnace1.Appearance.Options.UseBackColor = true;
            this.drumTypeFurnace1.Appearance.Options.UseBorderColor = true;
            this.drumTypeFurnace1.Appearance.Options.UseFont = true;
            this.drumTypeFurnace1.Appearance.Options.UseForeColor = true;
            this.drumTypeFurnace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drumTypeFurnace1.Location = new System.Drawing.Point(0, 0);
            this.drumTypeFurnace1.Name = "drumTypeFurnace1";
            this.drumTypeFurnace1.Size = new System.Drawing.Size(1252, 656);
            this.drumTypeFurnace1.TabIndex = 9;
            // 
            // ucFinishCleaning1
            // 
            this.ucFinishCleaning1.AutoScroll = true;
            this.ucFinishCleaning1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFinishCleaning1.GasConcentration_NO = 0F;
            this.ucFinishCleaning1.GasConcentration_NO2 = 0F;
            this.ucFinishCleaning1.GasConcentration_O2 = 0F;
            this.ucFinishCleaning1.GasConcentration_SO2 = 0F;
            this.ucFinishCleaning1.Temperature_TC6 = 0F;
            this.ucFinishCleaning1.Temperature_TC7 = 0F;
            this.ucFinishCleaning1.Location = new System.Drawing.Point(0, 0);
            this.ucFinishCleaning1.Name = "ucFinishCleaning1";
            this.ucFinishCleaning1.Size = new System.Drawing.Size(1252, 656);
            this.ucFinishCleaning1.TabIndex = 8;
            // 
            // ucAllHeatExchanger1
            // 
            this.ucAllHeatExchanger1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucAllHeatExchanger1.Appearance.Options.UseBackColor = true;
            this.ucAllHeatExchanger1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAllHeatExchanger1.Location = new System.Drawing.Point(0, 0);
            this.ucAllHeatExchanger1.Name = "ucAllHeatExchanger1";
            this.ucAllHeatExchanger1.Size = new System.Drawing.Size(1252, 656);
            this.ucAllHeatExchanger1.TabIndex = 5;
            // 
            // ucReheatChamber1
            // 
            this.ucReheatChamber1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucReheatChamber1.Location = new System.Drawing.Point(0, 0);
            this.ucReheatChamber1.Name = "ucReheatChamber1";
            this.ucReheatChamber1.Size = new System.Drawing.Size(1252, 656);
            this.ucReheatChamber1.TabIndex = 4;
            // 
            // ucCyclonAndScrubber1
            // 
            this.ucCyclonAndScrubber1.AutoScroll = true;
            this.ucCyclonAndScrubber1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCyclonAndScrubber1.Location = new System.Drawing.Point(0, 0);
            this.ucCyclonAndScrubber1.LookAndFeel.SkinName = "Caramel";
            this.ucCyclonAndScrubber1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ucCyclonAndScrubber1.Name = "ucCyclonAndScrubber1";
            this.ucCyclonAndScrubber1.Size = new System.Drawing.Size(1252, 656);
            this.ucCyclonAndScrubber1.TabIndex = 0;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem1,
            this.miLookAndFeel,
            this.miSkin});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 3;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            this.bar1.Visible = false;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1, true)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Look and feel";
            this.barSubItem1.Id = 0;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miSkin)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // miSkin
            // 
            this.miSkin.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.miSkin.Caption = "Skin";
            this.miSkin.Id = 2;
            this.miSkin.Name = "miSkin";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            this.bar3.Visible = false;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1264, 51);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 739);
            this.barDockControlBottom.Size = new System.Drawing.Size(1264, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 51);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 688);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1264, 51);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 688);
            // 
            // miLookAndFeel
            // 
            this.miLookAndFeel.Caption = "&Look and Feel";
            this.miLookAndFeel.Id = 1;
            this.miLookAndFeel.Name = "miLookAndFeel";
            // 
            // styleController1
            // 
            this.styleController1.LookAndFeel.SkinName = "iMaginary";
            this.styleController1.LookAndFeel.UseDefaultLookAndFeel = false;
            // 
            // channelController1
            // 
            this.channelController1.AutoSubscribeChannels = true;
            // 
            // groupControl1
            // 
            this.groupControl1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.groupControl1.Controls.Add(this.xtraTabControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 51);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(1264, 688);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "layoutControlGroup1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1260, 684);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4,
            this.xtraTabPage5,
            this.xtraTabPage6});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.textBox1);
            this.xtraTabPage1.Controls.Add(this.sbUnregister);
            this.xtraTabPage1.Controls.Add(this.sbRegister);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1252, 656);
            this.xtraTabPage1.Text = "Общий вид";
            // 
            // sbUnregister
            // 
            this.sbUnregister.Location = new System.Drawing.Point(89, 5);
            this.sbUnregister.Name = "sbUnregister";
            this.sbUnregister.Size = new System.Drawing.Size(75, 23);
            this.sbUnregister.TabIndex = 0;
            this.sbUnregister.Text = "Unregister";
            this.sbUnregister.Click += new System.EventHandler(this.sbUnregister_Click);
            // 
            // sbRegister
            // 
            this.sbRegister.Location = new System.Drawing.Point(8, 5);
            this.sbRegister.Name = "sbRegister";
            this.sbRegister.Size = new System.Drawing.Size(75, 23);
            this.sbRegister.TabIndex = 0;
            this.sbRegister.Text = "Register";
            this.sbRegister.Click += new System.EventHandler(this.sbRegister_Click);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.drumTypeFurnace1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1252, 656);
            this.xtraTabPage2.Text = "Барабанная вращающаяся печь ";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.ucReheatChamber1);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1252, 656);
            this.xtraTabPage3.Text = "Камера дожигания ";
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.ucAllHeatExchanger1);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1252, 656);
            this.xtraTabPage4.Text = "Теплообменник ";
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Controls.Add(this.ucCyclonAndScrubber1);
            this.xtraTabPage5.Name = "xtraTabPage5";
            this.xtraTabPage5.Size = new System.Drawing.Size(1252, 656);
            this.xtraTabPage5.Text = "Циклон и скруббер";
            // 
            // xtraTabPage6
            // 
            this.xtraTabPage6.Controls.Add(this.ucFinishCleaning1);
            this.xtraTabPage6.Name = "xtraTabPage6";
            this.xtraTabPage6.Size = new System.Drawing.Size(1252, 656);
            this.xtraTabPage6.Text = "Финишная очистка ";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(170, 5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1078, 648);
            this.textBox1.TabIndex = 2;
            // 
            // frmTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 762);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.SkinName = "The Asphalt World";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frmTP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Технологический процесс термического уничтожения отходов";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage5.ResumeLayout(false);
            this.xtraTabPage6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ucCyclonAndScrubber ucCyclonAndScrubber1;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private BarSubItem miLookAndFeel;
        private BarButtonItem miSkin;
        private HeatExchanger.ucAllHeatExchanger ucAllHeatExchanger1;
        //todo:private TP.FinishCleaning.ucFinishCleaning ucFinishCleaning1;
        internal TP.ReheatChamber.ucReheatChamber ucReheatChamber1;
        private ChannelController channelController1;
        private TP.FinishCleaning.ucFinishCleaning ucFinishCleaning1;
        private TP.DrumTypeFurnace.DrumTypeFurnace drumTypeFurnace1;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage6;
        private DevExpress.XtraEditors.SimpleButton sbUnregister;
        private DevExpress.XtraEditors.SimpleButton sbRegister;
        private System.Windows.Forms.TextBox textBox1;
    }
}