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
            this.ucCaptioned1 = new UICommon.ucCaptioned();
            this.ucLine13 = new UICommon.ucLine();
            this.ucLine12 = new UICommon.ucLine();
            this.ucLine11 = new UICommon.ucLine();
            this.ucLine10 = new UICommon.ucLine();
            this.ucLine9 = new UICommon.ucLine();
            this.ucLine8 = new UICommon.ucLine();
            this.ucLine7 = new UICommon.ucLine();
            this.ucLine6 = new UICommon.ucLine();
            this.ucLine5 = new UICommon.ucLine();
            this.ucLine4 = new UICommon.ucLine();
            this.ucLine2 = new UICommon.ucLine();
            this.ucLine1 = new UICommon.ucLine();
            this.ucIndicator3 = new UICommon.ucIndicator();
            this.ucIndicator4 = new UICommon.ucIndicator();
            this.ucIndicator2 = new UICommon.ucIndicator();
            this.ucIndicator1 = new UICommon.ucIndicator();
            this.ucBox1 = new UICommon.ucBox();
            this.ucBurner1 = new UICommon.ucBurner();
            this.ucTransporter1 = new UICommon.ucTransporter();
            this.ucCrater1 = new TP.FinishCleaning.ucCrater();
            this.ucFilter1 = new TP.FinishCleaning.ucFilter();
            this.ucPump1 = new UICommon.ucPump();
            this.ucTube1 = new UICommon.ucTube();
            this.ucCommonView1 = new TP.CommonView.ucCommonView();
            this.ucLine3 = new UICommon.ucLine();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage7 = new DevExpress.XtraTab.XtraTabPage();
            this.ucChart1 = new UICommon.ucChart();
            this.xtraTabPage8 = new DevExpress.XtraTab.XtraTabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sbUnregister = new DevExpress.XtraEditors.SimpleButton();
            this.sbRegister = new DevExpress.XtraEditors.SimpleButton();
            this.ucLine14 = new UICommon.ucLine();
            this.ucLine15 = new UICommon.ucLine();
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
            this.xtraTabPage8.SuspendLayout();
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
            this.ucDrumTypeFurnace1.Size = new System.Drawing.Size(1252, 656);
            this.ucDrumTypeFurnace1.T2 = 10F;
            this.ucDrumTypeFurnace1.T8 = 10F;
            this.ucDrumTypeFurnace1.TabIndex = 9;
            this.ucDrumTypeFurnace1.Load += new System.EventHandler(this.ucDrumTypeFurnace_Load);
            // 
            // ucFinishCleaning1
            // 
            this.ucFinishCleaning1.AutoScroll = true;
            this.ucFinishCleaning1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFinishCleaning1.Location = new System.Drawing.Point(0, 0);
            this.ucFinishCleaning1.Name = "ucFinishCleaning1";
            this.ucFinishCleaning1.Size = new System.Drawing.Size(1252, 656);
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
            this.ucAllHeatExchanger1.Size = new System.Drawing.Size(1252, 656);
            this.ucAllHeatExchanger1.TabIndex = 5;
            this.ucAllHeatExchanger1.Temperature_TP4 = 10F;
            this.ucAllHeatExchanger1.Temperature_TP5 = 10F;
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
            this.xtraTabPage6,
            this.xtraTabPage7,
            this.xtraTabPage8});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.ucLine15);
            this.xtraTabPage1.Controls.Add(this.ucLine14);
            this.xtraTabPage1.Controls.Add(this.ucCaptioned1);
            this.xtraTabPage1.Controls.Add(this.ucLine13);
            this.xtraTabPage1.Controls.Add(this.ucLine12);
            this.xtraTabPage1.Controls.Add(this.ucLine11);
            this.xtraTabPage1.Controls.Add(this.ucLine10);
            this.xtraTabPage1.Controls.Add(this.ucLine9);
            this.xtraTabPage1.Controls.Add(this.ucLine8);
            this.xtraTabPage1.Controls.Add(this.ucLine7);
            this.xtraTabPage1.Controls.Add(this.ucLine6);
            this.xtraTabPage1.Controls.Add(this.ucLine5);
            this.xtraTabPage1.Controls.Add(this.ucLine4);
            this.xtraTabPage1.Controls.Add(this.ucLine2);
            this.xtraTabPage1.Controls.Add(this.ucLine1);
            this.xtraTabPage1.Controls.Add(this.ucIndicator3);
            this.xtraTabPage1.Controls.Add(this.ucIndicator4);
            this.xtraTabPage1.Controls.Add(this.ucIndicator2);
            this.xtraTabPage1.Controls.Add(this.ucIndicator1);
            this.xtraTabPage1.Controls.Add(this.ucBox1);
            this.xtraTabPage1.Controls.Add(this.ucBurner1);
            this.xtraTabPage1.Controls.Add(this.ucTransporter1);
            this.xtraTabPage1.Controls.Add(this.ucCrater1);
            this.xtraTabPage1.Controls.Add(this.ucFilter1);
            this.xtraTabPage1.Controls.Add(this.ucPump1);
            this.xtraTabPage1.Controls.Add(this.ucTube1);
            this.xtraTabPage1.Controls.Add(this.ucCommonView1);
            this.xtraTabPage1.Controls.Add(this.ucLine3);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1252, 656);
            this.xtraTabPage1.Text = "Общий вид";
            // 
            // ucCaptioned1
            // 
            this.ucCaptioned1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaptioned1.Appearance.Options.UseBackColor = true;
            this.ucCaptioned1.Caption = "Адсорбент";
            this.ucCaptioned1.Location = new System.Drawing.Point(123, 144);
            this.ucCaptioned1.Name = "ucCaptioned1";
            this.ucCaptioned1.Size = new System.Drawing.Size(36, 19);
            this.ucCaptioned1.TabIndex = 25;
            // 
            // ucLine13
            // 
            this.ucLine13.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine13.Appearance.Options.UseBackColor = true;
            this.ucLine13.Color = UICommon.LineColor.Orange;
            this.ucLine13.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine13.LineWidth = 5;
            this.ucLine13.Location = new System.Drawing.Point(258, 56);
            this.ucLine13.Name = "ucLine13";
            this.ucLine13.Size = new System.Drawing.Size(13, 21);
            this.ucLine13.TabIndex = 24;
            // 
            // ucLine12
            // 
            this.ucLine12.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine12.Appearance.Options.UseBackColor = true;
            this.ucLine12.Color = UICommon.LineColor.Orange;
            this.ucLine12.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine12.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine12.LineWidth = 5;
            this.ucLine12.Location = new System.Drawing.Point(212, 1);
            this.ucLine12.Name = "ucLine12";
            this.ucLine12.Size = new System.Drawing.Size(55, 147);
            this.ucLine12.TabIndex = 23;
            // 
            // ucLine11
            // 
            this.ucLine11.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine11.Appearance.Options.UseBackColor = true;
            this.ucLine11.Color = UICommon.LineColor.Black;
            this.ucLine11.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine11.LineWidth = 3;
            this.ucLine11.Location = new System.Drawing.Point(210, 163);
            this.ucLine11.Name = "ucLine11";
            this.ucLine11.Size = new System.Drawing.Size(35, 147);
            this.ucLine11.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine11.TabIndex = 22;
            // 
            // ucLine10
            // 
            this.ucLine10.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine10.Appearance.Options.UseBackColor = true;
            this.ucLine10.Color = UICommon.LineColor.Black;
            this.ucLine10.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine10.LineWidth = 3;
            this.ucLine10.Location = new System.Drawing.Point(216, 82);
            this.ucLine10.Name = "ucLine10";
            this.ucLine10.Size = new System.Drawing.Size(13, 66);
            this.ucLine10.TabIndex = 21;
            // 
            // ucLine9
            // 
            this.ucLine9.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine9.Appearance.Options.UseBackColor = true;
            this.ucLine9.Color = UICommon.LineColor.Black;
            this.ucLine9.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine9.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine9.LineWidth = 3;
            this.ucLine9.Location = new System.Drawing.Point(211, 10);
            this.ucLine9.Name = "ucLine9";
            this.ucLine9.Size = new System.Drawing.Size(12, 147);
            this.ucLine9.TabIndex = 20;
            // 
            // ucLine8
            // 
            this.ucLine8.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine8.Appearance.Options.UseBackColor = true;
            this.ucLine8.Color = UICommon.LineColor.Black;
            this.ucLine8.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine8.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine8.LineWidth = 3;
            this.ucLine8.Location = new System.Drawing.Point(159, 199);
            this.ucLine8.Name = "ucLine8";
            this.ucLine8.Size = new System.Drawing.Size(10, 27);
            this.ucLine8.TabIndex = 19;
            // 
            // ucLine7
            // 
            this.ucLine7.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine7.Appearance.Options.UseBackColor = true;
            this.ucLine7.Color = UICommon.LineColor.Black;
            this.ucLine7.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine7.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine7.LineWidth = 3;
            this.ucLine7.Location = new System.Drawing.Point(159, 120);
            this.ucLine7.Name = "ucLine7";
            this.ucLine7.Size = new System.Drawing.Size(10, 27);
            this.ucLine7.TabIndex = 18;
            // 
            // ucLine6
            // 
            this.ucLine6.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine6.Appearance.Options.UseBackColor = true;
            this.ucLine6.Color = UICommon.LineColor.Black;
            this.ucLine6.Direction = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLine6.LineWidth = 3;
            this.ucLine6.Location = new System.Drawing.Point(196, 147);
            this.ucLine6.Name = "ucLine6";
            this.ucLine6.Size = new System.Drawing.Size(28, 10);
            this.ucLine6.TabIndex = 17;
            // 
            // ucLine5
            // 
            this.ucLine5.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine5.Appearance.Options.UseBackColor = true;
            this.ucLine5.Color = UICommon.LineColor.Orange;
            this.ucLine5.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine5.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine5.LineWidth = 5;
            this.ucLine5.Location = new System.Drawing.Point(57, 114);
            this.ucLine5.Name = "ucLine5";
            this.ucLine5.Size = new System.Drawing.Size(17, 147);
            this.ucLine5.TabIndex = 16;
            // 
            // ucLine4
            // 
            this.ucLine4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine4.Appearance.Options.UseBackColor = true;
            this.ucLine4.Color = UICommon.LineColor.Orange;
            this.ucLine4.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine4.LineWidth = 5;
            this.ucLine4.Location = new System.Drawing.Point(94, 123);
            this.ucLine4.Name = "ucLine4";
            this.ucLine4.Size = new System.Drawing.Size(27, 147);
            this.ucLine4.TabIndex = 15;
            // 
            // ucLine2
            // 
            this.ucLine2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine2.Appearance.Options.UseBackColor = true;
            this.ucLine2.Color = UICommon.LineColor.Orange;
            this.ucLine2.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine2.LineWidth = 5;
            this.ucLine2.Location = new System.Drawing.Point(32, 18);
            this.ucLine2.Name = "ucLine2";
            this.ucLine2.Size = new System.Drawing.Size(10, 27);
            this.ucLine2.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine2.TabIndex = 13;
            // 
            // ucLine1
            // 
            this.ucLine1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine1.Appearance.Options.UseBackColor = true;
            this.ucLine1.Color = UICommon.LineColor.Orange;
            this.ucLine1.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine1.LineWidth = 5;
            this.ucLine1.Location = new System.Drawing.Point(112, 30);
            this.ucLine1.Name = "ucLine1";
            this.ucLine1.Size = new System.Drawing.Size(13, 170);
            this.ucLine1.TabIndex = 12;
            // 
            // ucIndicator3
            // 
            this.ucIndicator3.AllowedMaxValue = 0F;
            this.ucIndicator3.AllowedMinValue = 0F;
            this.ucIndicator3.Caption = "Ph1:";
            this.ucIndicator3.EditValue = 10F;
            this.ucIndicator3.Location = new System.Drawing.Point(291, 246);
            this.ucIndicator3.MaxValue = 10F;
            this.ucIndicator3.MinValue = 0F;
            this.ucIndicator3.Name = "ucIndicator3";
            this.ucIndicator3.Size = new System.Drawing.Size(44, 62);
            this.ucIndicator3.TabIndex = 11;
            this.ucIndicator3.TickCount = 0;
            // 
            // ucIndicator4
            // 
            this.ucIndicator4.AllowedMaxValue = 0F;
            this.ucIndicator4.AllowedMinValue = 0F;
            this.ucIndicator4.Caption = "Ph1:";
            this.ucIndicator4.EditValue = 10F;
            this.ucIndicator4.Location = new System.Drawing.Point(241, 246);
            this.ucIndicator4.MaxValue = 10F;
            this.ucIndicator4.MinValue = 0F;
            this.ucIndicator4.Name = "ucIndicator4";
            this.ucIndicator4.Size = new System.Drawing.Size(44, 62);
            this.ucIndicator4.TabIndex = 10;
            this.ucIndicator4.TickCount = 0;
            // 
            // ucIndicator2
            // 
            this.ucIndicator2.AllowedMaxValue = 0F;
            this.ucIndicator2.AllowedMinValue = 0F;
            this.ucIndicator2.Caption = "Ph1:";
            this.ucIndicator2.EditValue = 10F;
            this.ucIndicator2.Location = new System.Drawing.Point(98, 246);
            this.ucIndicator2.MaxValue = 10F;
            this.ucIndicator2.MinValue = 0F;
            this.ucIndicator2.Name = "ucIndicator2";
            this.ucIndicator2.Size = new System.Drawing.Size(44, 62);
            this.ucIndicator2.TabIndex = 9;
            this.ucIndicator2.TickCount = 0;
            // 
            // ucIndicator1
            // 
            this.ucIndicator1.AllowedMaxValue = 0F;
            this.ucIndicator1.AllowedMinValue = 0F;
            this.ucIndicator1.Caption = "Ph1:";
            this.ucIndicator1.EditValue = 10F;
            this.ucIndicator1.Location = new System.Drawing.Point(48, 246);
            this.ucIndicator1.MaxValue = 10F;
            this.ucIndicator1.MinValue = 0F;
            this.ucIndicator1.Name = "ucIndicator1";
            this.ucIndicator1.Size = new System.Drawing.Size(44, 62);
            this.ucIndicator1.TabIndex = 8;
            this.ucIndicator1.TickCount = 0;
            // 
            // ucBox1
            // 
            this.ucBox1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBox1.Appearance.Options.UseBackColor = true;
            this.ucBox1.Location = new System.Drawing.Point(242, 30);
            this.ucBox1.Name = "ucBox1";
            this.ucBox1.Size = new System.Drawing.Size(43, 38);
            this.ucBox1.TabIndex = 7;
            // 
            // ucBurner1
            // 
            this.ucBurner1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBurner1.Appearance.Options.UseBackColor = true;
            this.ucBurner1.BurnerStatus = false;
            this.ucBurner1.Location = new System.Drawing.Point(273, 30);
            this.ucBurner1.Name = "ucBurner1";
            this.ucBurner1.Size = new System.Drawing.Size(60, 40);
            this.ucBurner1.TabIndex = 6;
            // 
            // ucTransporter1
            // 
            this.ucTransporter1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucTransporter1.Appearance.Options.UseBackColor = true;
            this.ucTransporter1.Location = new System.Drawing.Point(127, 232);
            this.ucTransporter1.Name = "ucTransporter1";
            this.ucTransporter1.Size = new System.Drawing.Size(77, 8);
            this.ucTransporter1.TabIndex = 5;
            // 
            // ucCrater1
            // 
            this.ucCrater1.Location = new System.Drawing.Point(149, 152);
            this.ucCrater1.Name = "ucCrater1";
            this.ucCrater1.Size = new System.Drawing.Size(41, 41);
            this.ucCrater1.TabIndex = 4;
            // 
            // ucFilter1
            // 
            this.ucFilter1.Location = new System.Drawing.Point(135, 17);
            this.ucFilter1.Name = "ucFilter1";
            this.ucFilter1.Size = new System.Drawing.Size(77, 99);
            this.ucFilter1.TabIndex = 3;
            // 
            // ucPump1
            // 
            this.ucPump1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucPump1.Appearance.Options.UseBackColor = true;
            this.ucPump1.Location = new System.Drawing.Point(79, 186);
            this.ucPump1.Name = "ucPump1";
            this.ucPump1.Size = new System.Drawing.Size(29, 29);
            this.ucPump1.TabIndex = 2;
            // 
            // ucTube1
            // 
            this.ucTube1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucTube1.Appearance.Options.UseBackColor = true;
            this.ucTube1.IsSmokes = false;
            this.ucTube1.Location = new System.Drawing.Point(17, 46);
            this.ucTube1.Name = "ucTube1";
            this.ucTube1.Size = new System.Drawing.Size(39, 189);
            this.ucTube1.TabIndex = 1;
            // 
            // ucCommonView1
            // 
            this.ucCommonView1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCommonView1.Appearance.Options.UseBackColor = true;
            this.ucCommonView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCommonView1.Location = new System.Drawing.Point(0, 0);
            this.ucCommonView1.Name = "ucCommonView1";
            this.ucCommonView1.Size = new System.Drawing.Size(1252, 656);
            this.ucCommonView1.TabIndex = 0;
            // 
            // ucLine3
            // 
            this.ucLine3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine3.Appearance.Options.UseBackColor = true;
            this.ucLine3.Color = UICommon.LineColor.Orange;
            this.ucLine3.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine3.LineWidth = 5;
            this.ucLine3.Location = new System.Drawing.Point(116, -46);
            this.ucLine3.Name = "ucLine3";
            this.ucLine3.Size = new System.Drawing.Size(27, 147);
            this.ucLine3.TabIndex = 14;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.ucDrumTypeFurnace1);
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
            // xtraTabPage7
            // 
            this.xtraTabPage7.Controls.Add(this.ucChart1);
            this.xtraTabPage7.Name = "xtraTabPage7";
            this.xtraTabPage7.Size = new System.Drawing.Size(1252, 656);
            this.xtraTabPage7.Text = "Графики";
            // 
            // ucChart1
            // 
            this.ucChart1.ChannelsToDisplay = ((System.Collections.Generic.List<int>)(resources.GetObject("ucChart1.ChannelsToDisplay")));
            this.ucChart1.ChartTitle = "Уровни";
            this.ucChart1.Location = new System.Drawing.Point(192, 40);
            this.ucChart1.Name = "ucChart1";
            this.ucChart1.Size = new System.Drawing.Size(640, 480);
            this.ucChart1.TabIndex = 0;
            // 
            // xtraTabPage8
            // 
            this.xtraTabPage8.Controls.Add(this.textBox1);
            this.xtraTabPage8.Controls.Add(this.sbUnregister);
            this.xtraTabPage8.Controls.Add(this.sbRegister);
            this.xtraTabPage8.Name = "xtraTabPage8";
            this.xtraTabPage8.Size = new System.Drawing.Size(1252, 656);
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
            // ucLine14
            // 
            this.ucLine14.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine14.Appearance.Options.UseBackColor = true;
            this.ucLine14.Color = UICommon.LineColor.Orange;
            this.ucLine14.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine14.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine14.LineWidth = 5;
            this.ucLine14.Location = new System.Drawing.Point(270, -63);
            this.ucLine14.Name = "ucLine14";
            this.ucLine14.Size = new System.Drawing.Size(80, 147);
            this.ucLine14.TabIndex = 26;
            // 
            // ucLine15
            // 
            this.ucLine15.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine15.Appearance.Options.UseBackColor = true;
            this.ucLine15.Color = UICommon.LineColor.Orange;
            this.ucLine15.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine15.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine15.LineWidth = 5;
            this.ucLine15.Location = new System.Drawing.Point(258, 8);
            this.ucLine15.Name = "ucLine15";
            this.ucLine15.Size = new System.Drawing.Size(13, 21);
            this.ucLine15.TabIndex = 27;
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
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage5.ResumeLayout(false);
            this.xtraTabPage6.ResumeLayout(false);
            this.xtraTabPage7.ResumeLayout(false);
            this.xtraTabPage8.ResumeLayout(false);
            this.xtraTabPage8.PerformLayout();
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
        private UICommon.ucChart ucChart1;
        private UICommon.ucTube ucTube1;
        private UICommon.ucIndicator ucIndicator3;
        private UICommon.ucIndicator ucIndicator4;
        private UICommon.ucIndicator ucIndicator2;
        private UICommon.ucIndicator ucIndicator1;
        private UICommon.ucBox ucBox1;
        private UICommon.ucBurner ucBurner1;
        private UICommon.ucTransporter ucTransporter1;
        private TP.FinishCleaning.ucCrater ucCrater1;
        private TP.FinishCleaning.ucFilter ucFilter1;
        private UICommon.ucPump ucPump1;
        private TP.CommonView.ucCommonView ucCommonView1;
        private UICommon.ucLine ucLine5;
        private UICommon.ucLine ucLine4;
        private UICommon.ucLine ucLine3;
        private UICommon.ucLine ucLine2;
        private UICommon.ucLine ucLine1;
        private UICommon.ucLine ucLine9;
        private UICommon.ucLine ucLine8;
        private UICommon.ucLine ucLine7;
        private UICommon.ucLine ucLine6;
        private UICommon.ucCaptioned ucCaptioned1;
        private UICommon.ucLine ucLine13;
        private UICommon.ucLine ucLine12;
        private UICommon.ucLine ucLine11;
        private UICommon.ucLine ucLine10;
        private UICommon.ucLine ucLine15;
        private UICommon.ucLine ucLine14;
    }
}