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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTP));
            this.ucDrumTypeFurnace1 = new TP.DrumTypeFurnace.ucDrumTypeFurnace();
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
            this.ucCommonView1 = new TP.CommonView.ucCommonView();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage7 = new DevExpress.XtraTab.XtraTabPage();
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
            this.ucChart4 = new UICommon.ucChart();
            this.ucChart5 = new UICommon.ucChart();
            this.splitContainerControl4 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucChart6 = new UICommon.ucChart();
            this.ucChart7 = new UICommon.ucChart();
            this.xtraTabPage8 = new DevExpress.XtraTab.XtraTabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sbUnregister = new DevExpress.XtraEditors.SimpleButton();
            this.sbRegister = new DevExpress.XtraEditors.SimpleButton();
            this.dtsChart1 = new UICommon.dtsChart();
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
            this.xtraTabPage7.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).BeginInit();
            this.splitContainerControl4.SuspendLayout();
            this.xtraTabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtsChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // ucDrumTypeFurnace1
            // 
            this.ucDrumTypeFurnace1.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ucDrumTypeFurnace1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ucDrumTypeFurnace1.Appearance.Options.UseBackColor = true;
            this.ucDrumTypeFurnace1.Appearance.Options.UseBorderColor = true;
            this.ucDrumTypeFurnace1.Appearance.Options.UseFont = true;
            this.ucDrumTypeFurnace1.Appearance.Options.UseForeColor = true;
            this.ucDrumTypeFurnace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDrumTypeFurnace1.DU9 = 10F;
            this.ucDrumTypeFurnace1.Location = new System.Drawing.Point(0, 0);
            this.ucDrumTypeFurnace1.Name = "ucDrumTypeFurnace1";
            this.ucDrumTypeFurnace1.S = 5F;
            this.ucDrumTypeFurnace1.Size = new System.Drawing.Size(1016, 479);
            this.ucDrumTypeFurnace1.T2 = 10F;
            this.ucDrumTypeFurnace1.T8 = 10F;
            this.ucDrumTypeFurnace1.TabIndex = 9;
            // 
            // ucFinishCleaning1
            // 
            this.ucFinishCleaning1.AutoScroll = true;
            this.ucFinishCleaning1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFinishCleaning1.Location = new System.Drawing.Point(0, 0);
            this.ucFinishCleaning1.Name = "ucFinishCleaning1";
            this.ucFinishCleaning1.Size = new System.Drawing.Size(1016, 479);
            this.ucFinishCleaning1.TabIndex = 8;
            // 
            // ucAllHeatExchanger1
            // 
            this.ucAllHeatExchanger1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucAllHeatExchanger1.Appearance.Options.UseBackColor = true;
            this.ucAllHeatExchanger1.Concentration_CO = 10F;
            this.ucAllHeatExchanger1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAllHeatExchanger1.Location = new System.Drawing.Point(0, 0);
            this.ucAllHeatExchanger1.Name = "ucAllHeatExchanger1";
            this.ucAllHeatExchanger1.Size = new System.Drawing.Size(1016, 479);
            this.ucAllHeatExchanger1.TabIndex = 5;
            this.ucAllHeatExchanger1.Temperature_TP4 = 10F;
            this.ucAllHeatExchanger1.Temperature_TP5 = 10F;
            // 
            // ucReheatChamber1
            // 
            this.ucReheatChamber1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucReheatChamber1.Location = new System.Drawing.Point(0, 0);
            this.ucReheatChamber1.Name = "ucReheatChamber1";
            this.ucReheatChamber1.Size = new System.Drawing.Size(1016, 479);
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
            this.ucCyclonAndScrubber1.Size = new System.Drawing.Size(1016, 479);
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
            this.barDockControlTop.Size = new System.Drawing.Size(1028, 51);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 562);
            this.barDockControlBottom.Size = new System.Drawing.Size(1028, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 51);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 511);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1028, 51);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 511);
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
            this.channelController1.CanRegister = false;
            this.channelController1.CanRegisterChanged += new System.EventHandler(this.channelController1_CanRegisterChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.groupControl1.Controls.Add(this.xtraTabControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 51);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(1028, 511);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "layoutControlGroup1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1024, 507);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4,
            this.xtraTabPage5,
            this.xtraTabPage6,
            this.xtraTabPage7,
            this.xtraTabPage8});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.ucCommonView1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1016, 479);
            this.xtraTabPage1.Text = "Общий вид";
            // 
            // ucCommonView1
            // 
            this.ucCommonView1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCommonView1.Appearance.Options.UseBackColor = true;
            this.ucCommonView1.Location = new System.Drawing.Point(3, 3);
            this.ucCommonView1.Name = "ucCommonView1";
            this.ucCommonView1.Size = new System.Drawing.Size(1252, 656);
            this.ucCommonView1.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.ucDrumTypeFurnace1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1016, 479);
            this.xtraTabPage2.Text = "Барабанная вращающаяся печь ";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.ucReheatChamber1);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1016, 479);
            this.xtraTabPage3.Text = "Камера дожигания ";
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.ucAllHeatExchanger1);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1016, 479);
            this.xtraTabPage4.Text = "Теплообменник ";
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Controls.Add(this.ucCyclonAndScrubber1);
            this.xtraTabPage5.Name = "xtraTabPage5";
            this.xtraTabPage5.Size = new System.Drawing.Size(1016, 479);
            this.xtraTabPage5.Text = "Циклон и скруббер";
            // 
            // xtraTabPage6
            // 
            this.xtraTabPage6.Controls.Add(this.ucFinishCleaning1);
            this.xtraTabPage6.Name = "xtraTabPage6";
            this.xtraTabPage6.Size = new System.Drawing.Size(1016, 479);
            this.xtraTabPage6.Text = "Финишная очистка ";
            // 
            // xtraTabPage7
            // 
            this.xtraTabPage7.Controls.Add(this.xtraTabControl2);
            this.xtraTabPage7.Name = "xtraTabPage7";
            this.xtraTabPage7.Size = new System.Drawing.Size(1016, 479);
            this.xtraTabPage7.Text = "Графики";
            this.xtraTabPage7.Paint += new System.Windows.Forms.PaintEventHandler(this.xtraTabPage7_Paint);
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl2.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl2.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtraTabControl2.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.xtraTabPage9;
            this.xtraTabControl2.Size = new System.Drawing.Size(1016, 479);
            this.xtraTabControl2.TabIndex = 0;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage9,
            this.xtraTabPage10,
            this.xtraTabPage11});
            // 
            // xtraTabPage9
            // 
            this.xtraTabPage9.Controls.Add(this.splitContainerControl1);
            this.xtraTabPage9.Name = "xtraTabPage9";
            this.xtraTabPage9.Size = new System.Drawing.Size(930, 471);
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
            this.splitContainerControl1.Size = new System.Drawing.Size(930, 471);
            this.splitContainerControl1.SplitterPosition = 189;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ucChart1
            // 
            this.ucChart1.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart1.ChannelsToDisplay")));
            this.ucChart1.ChartTitle = "";
            this.ucChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart1.Location = new System.Drawing.Point(0, 0);
            this.ucChart1.Name = "ucChart1";
            this.ucChart1.Size = new System.Drawing.Size(930, 189);
            this.ucChart1.TabIndex = 0;
            // 
            // ucChart2
            // 
            this.ucChart2.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart2.ChannelsToDisplay")));
            this.ucChart2.ChartTitle = "";
            this.ucChart2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart2.Location = new System.Drawing.Point(0, 0);
            this.ucChart2.Name = "ucChart2";
            this.ucChart2.Size = new System.Drawing.Size(930, 276);
            this.ucChart2.TabIndex = 0;
            // 
            // xtraTabPage10
            // 
            this.xtraTabPage10.Controls.Add(this.ucChart3);
            this.xtraTabPage10.Name = "xtraTabPage10";
            this.xtraTabPage10.Size = new System.Drawing.Size(930, 471);
            this.xtraTabPage10.Text = "Уровень";
            // 
            // ucChart3
            // 
            this.ucChart3.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart3.ChannelsToDisplay")));
            this.ucChart3.ChartTitle = "";
            this.ucChart3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart3.Location = new System.Drawing.Point(0, 0);
            this.ucChart3.Name = "ucChart3";
            this.ucChart3.Size = new System.Drawing.Size(930, 471);
            this.ucChart3.TabIndex = 0;
            // 
            // xtraTabPage11
            // 
            this.xtraTabPage11.Controls.Add(this.splitContainerControl2);
            this.xtraTabPage11.Name = "xtraTabPage11";
            this.xtraTabPage11.Size = new System.Drawing.Size(930, 471);
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
            this.splitContainerControl2.Panel2.Controls.Add(this.splitContainerControl4);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainerControl2.Size = new System.Drawing.Size(930, 471);
            this.splitContainerControl2.SplitterPosition = 237;
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
            this.splitContainerControl3.Panel1.Controls.Add(this.ucChart4);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            this.splitContainerControl3.Panel2.Controls.Add(this.ucChart5);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.splitContainerControl3.Size = new System.Drawing.Size(930, 237);
            this.splitContainerControl3.SplitterPosition = 119;
            this.splitContainerControl3.TabIndex = 0;
            this.splitContainerControl3.Text = "splitContainerControl3";
            // 
            // ucChart4
            // 
            this.ucChart4.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart4.ChannelsToDisplay")));
            this.ucChart4.ChartTitle = "";
            this.ucChart4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart4.Location = new System.Drawing.Point(0, 0);
            this.ucChart4.Name = "ucChart4";
            this.ucChart4.Size = new System.Drawing.Size(930, 119);
            this.ucChart4.TabIndex = 0;
            // 
            // ucChart5
            // 
            this.ucChart5.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart5.ChannelsToDisplay")));
            this.ucChart5.ChartTitle = "";
            this.ucChart5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart5.Location = new System.Drawing.Point(0, 0);
            this.ucChart5.Name = "ucChart5";
            this.ucChart5.Size = new System.Drawing.Size(930, 112);
            this.ucChart5.TabIndex = 0;
            // 
            // splitContainerControl4
            // 
            this.splitContainerControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl4.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl4.Horizontal = false;
            this.splitContainerControl4.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl4.Name = "splitContainerControl4";
            this.splitContainerControl4.Panel1.Controls.Add(this.ucChart6);
            this.splitContainerControl4.Panel1.Text = "Panel1";
            this.splitContainerControl4.Panel2.Controls.Add(this.ucChart7);
            this.splitContainerControl4.Panel2.Text = "Panel2";
            this.splitContainerControl4.Size = new System.Drawing.Size(930, 228);
            this.splitContainerControl4.SplitterPosition = 115;
            this.splitContainerControl4.TabIndex = 0;
            this.splitContainerControl4.Text = "splitContainerControl4";
            // 
            // ucChart6
            // 
            this.ucChart6.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart6.ChannelsToDisplay")));
            this.ucChart6.ChartTitle = "";
            this.ucChart6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart6.Location = new System.Drawing.Point(0, 0);
            this.ucChart6.Name = "ucChart6";
            this.ucChart6.Size = new System.Drawing.Size(930, 115);
            this.ucChart6.TabIndex = 0;
            // 
            // ucChart7
            // 
            this.ucChart7.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart7.ChannelsToDisplay")));
            this.ucChart7.ChartTitle = "";
            this.ucChart7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart7.Location = new System.Drawing.Point(0, 0);
            this.ucChart7.Name = "ucChart7";
            this.ucChart7.Size = new System.Drawing.Size(930, 107);
            this.ucChart7.TabIndex = 0;
            // 
            // xtraTabPage8
            // 
            this.xtraTabPage8.Controls.Add(this.textBox1);
            this.xtraTabPage8.Controls.Add(this.sbUnregister);
            this.xtraTabPage8.Controls.Add(this.sbRegister);
            this.xtraTabPage8.Name = "xtraTabPage8";
            this.xtraTabPage8.Size = new System.Drawing.Size(1016, 479);
            this.xtraTabPage8.Text = "Отладочная информация";
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
            // sbUnregister
            // 
            this.sbUnregister.Enabled = false;
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
            // dtsChart1
            // 
            this.dtsChart1.DataSetName = "dtsChart";
            this.dtsChart1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 585);
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
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage5.ResumeLayout(false);
            this.xtraTabPage6.ResumeLayout(false);
            this.xtraTabPage7.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).EndInit();
            this.splitContainerControl4.ResumeLayout(false);
            this.xtraTabPage8.ResumeLayout(false);
            this.xtraTabPage8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtsChart1)).EndInit();
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
        private TP.DrumTypeFurnace.ucDrumTypeFurnace ucDrumTypeFurnace1;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage8;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage6;
        private DevExpress.XtraEditors.SimpleButton sbUnregister;
        private DevExpress.XtraEditors.SimpleButton sbRegister;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage7;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private TP.CommonView.ucCommonView ucCommonView1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage9;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage10;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage11;
        private UICommon.dtsChart dtsChart1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private UICommon.ucChart ucChart1;
        private UICommon.ucChart ucChart2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private UICommon.ucChart ucChart3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl4;
        private UICommon.ucChart ucChart4;
        private UICommon.ucChart ucChart5;
        private UICommon.ucChart ucChart6;
        private UICommon.ucChart ucChart7;
    }
}