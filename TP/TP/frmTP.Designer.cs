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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.drumTypeFurnace1 = new TP.DrumTypeFurnace.DrumTypeFurnace();
            this.ucFinishCleaning1 = new TP.FinishCleaning.ucFinishCleaning();
            this.spinEdit2 = new DevExpress.XtraEditors.SpinEdit();
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
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.ucAllHeatExchanger1 = new TP.HeatExchanger.ucAllHeatExchanger();
            this.ucReheatChamber1 = new TP.ReheatChamber.ucReheatChamber();
            this.ucCyclonAndScrubber1 = new TP.CyclonAndScrubber.ucCyclonAndScrubber();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
            this.lcgFinishCleaning = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgCommonView = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgDrumTypeFurnace = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgReheatChamber = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgHeatExchanger = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgCyclonAndScrubber = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.channelController1 = new TP.ChannelController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgFinishCleaning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCommonView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDrumTypeFurnace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReheatChamber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgHeatExchanger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCyclonAndScrubber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.drumTypeFurnace1);
            this.layoutControl1.Controls.Add(this.ucFinishCleaning1);
            this.layoutControl1.Controls.Add(this.spinEdit2);
            this.layoutControl1.Controls.Add(this.spinEdit1);
            this.layoutControl1.Controls.Add(this.ucAllHeatExchanger1);
            this.layoutControl1.Controls.Add(this.ucReheatChamber1);
            this.layoutControl1.Controls.Add(this.ucCyclonAndScrubber1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControl1.Location = new System.Drawing.Point(0, 51);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1072, 372, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1264, 688);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // drumTypeFurnace1
            // 
            this.drumTypeFurnace1.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            this.drumTypeFurnace1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.drumTypeFurnace1.Appearance.Options.UseBackColor = true;
            this.drumTypeFurnace1.Appearance.Options.UseBorderColor = true;
            this.drumTypeFurnace1.Appearance.Options.UseFont = true;
            this.drumTypeFurnace1.Appearance.Options.UseForeColor = true;
            this.drumTypeFurnace1.Location = new System.Drawing.Point(24, 44);
            this.drumTypeFurnace1.Name = "drumTypeFurnace1";
            this.drumTypeFurnace1.Size = new System.Drawing.Size(1216, 620);
            this.drumTypeFurnace1.TabIndex = 9;
            // 
            // ucFinishCleaning1
            // 
            this.ucFinishCleaning1.AutoScroll = true;
            this.ucFinishCleaning1.Level_NO = 0F;
            this.ucFinishCleaning1.Level_NO2 = 0F;
            this.ucFinishCleaning1.Level_O2 = 0F;
            this.ucFinishCleaning1.Level_SO2 = 0F;
            this.ucFinishCleaning1.Level_TC6 = 0F;
            this.ucFinishCleaning1.Level_TC7 = 0F;
            this.ucFinishCleaning1.Location = new System.Drawing.Point(24, 44);
            this.ucFinishCleaning1.Name = "ucFinishCleaning1";
            this.ucFinishCleaning1.Size = new System.Drawing.Size(1216, 620);
            this.ucFinishCleaning1.TabIndex = 8;
            // 
            // spinEdit2
            // 
            this.spinEdit2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit2.Location = new System.Drawing.Point(681, 44);
            this.spinEdit2.MenuManager = this.barManager1;
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit2.Size = new System.Drawing.Size(559, 20);
            this.spinEdit2.StyleController = this.layoutControl1;
            this.spinEdit2.TabIndex = 7;
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
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(121, 44);
            this.spinEdit1.MenuManager = this.barManager1;
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Size = new System.Drawing.Size(459, 20);
            this.spinEdit1.StyleController = this.layoutControl1;
            this.spinEdit1.TabIndex = 6;
            // 
            // ucAllHeatExchanger1
            // 
            this.ucAllHeatExchanger1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucAllHeatExchanger1.Appearance.Options.UseBackColor = true;
            this.ucAllHeatExchanger1.Location = new System.Drawing.Point(24, 44);
            this.ucAllHeatExchanger1.Name = "ucAllHeatExchanger1";
            this.ucAllHeatExchanger1.Size = new System.Drawing.Size(1216, 620);
            this.ucAllHeatExchanger1.TabIndex = 5;
            // 
            // ucReheatChamber1
            // 
            this.ucReheatChamber1.Location = new System.Drawing.Point(24, 68);
            this.ucReheatChamber1.Name = "ucReheatChamber1";
            this.ucReheatChamber1.Size = new System.Drawing.Size(1216, 596);
            this.ucReheatChamber1.TabIndex = 4;
            // 
            // ucCyclonAndScrubber1
            // 
            this.ucCyclonAndScrubber1.AutoScroll = true;
            this.ucCyclonAndScrubber1.Location = new System.Drawing.Point(24, 44);
            this.ucCyclonAndScrubber1.LookAndFeel.SkinName = "Caramel";
            this.ucCyclonAndScrubber1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ucCyclonAndScrubber1.Name = "ucCyclonAndScrubber1";
            this.ucCyclonAndScrubber1.Size = new System.Drawing.Size(1216, 620);
            this.ucCyclonAndScrubber1.TabIndex = 0;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(527, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(528, 510);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabbedControlGroup1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1264, 688);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // tabbedControlGroup1
            // 
            this.tabbedControlGroup1.CustomizationFormText = "tabbedControlGroup1";
            this.tabbedControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.tabbedControlGroup1.Name = "tabbedControlGroup1";
            this.tabbedControlGroup1.SelectedTabPage = this.lcgCommonView;
            this.tabbedControlGroup1.SelectedTabPageIndex = 0;
            this.tabbedControlGroup1.Size = new System.Drawing.Size(1244, 668);
            this.tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgCommonView,
            this.lcgDrumTypeFurnace,
            this.lcgReheatChamber,
            this.lcgHeatExchanger,
            this.lcgCyclonAndScrubber,
            this.lcgFinishCleaning});
            this.tabbedControlGroup1.Text = "tabbedControlGroup1";
            // 
            // lcgFinishCleaning
            // 
            this.lcgFinishCleaning.CustomizationFormText = "Финишная очистка ";
            this.lcgFinishCleaning.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.lcgFinishCleaning.Location = new System.Drawing.Point(0, 0);
            this.lcgFinishCleaning.Name = "lcgFinishCleaning";
            this.lcgFinishCleaning.Size = new System.Drawing.Size(1220, 624);
            this.lcgFinishCleaning.Text = "Финишная очистка ";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.ucFinishCleaning1;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(1220, 624);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // lcgCommonView
            // 
            this.lcgCommonView.CustomizationFormText = "Общий вид";
            this.lcgCommonView.Location = new System.Drawing.Point(0, 0);
            this.lcgCommonView.Name = "lcgCommonView";
            this.lcgCommonView.Size = new System.Drawing.Size(1220, 624);
            this.lcgCommonView.Text = "Общий вид";
            // 
            // lcgDrumTypeFurnace
            // 
            this.lcgDrumTypeFurnace.CustomizationFormText = "Барабанная вращающаяся печь ";
            this.lcgDrumTypeFurnace.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
            this.lcgDrumTypeFurnace.Location = new System.Drawing.Point(0, 0);
            this.lcgDrumTypeFurnace.Name = "lcgDrumTypeFurnace";
            this.lcgDrumTypeFurnace.Size = new System.Drawing.Size(1220, 624);
            this.lcgDrumTypeFurnace.Text = "Барабанная вращающаяся печь ";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.drumTypeFurnace1;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(1220, 624);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // lcgReheatChamber
            // 
            this.lcgReheatChamber.CustomizationFormText = "Камера дожигания ";
            this.lcgReheatChamber.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.lcgReheatChamber.Location = new System.Drawing.Point(0, 0);
            this.lcgReheatChamber.Name = "lcgReheatChamber";
            this.lcgReheatChamber.Size = new System.Drawing.Size(1220, 624);
            this.lcgReheatChamber.Text = "Камера дожигания ";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucReheatChamber1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1220, 600);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.spinEdit1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(560, 24);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(93, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.spinEdit2;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(560, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(660, 24);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(93, 13);
            // 
            // lcgHeatExchanger
            // 
            this.lcgHeatExchanger.CustomizationFormText = "Теплообменник ";
            this.lcgHeatExchanger.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcgHeatExchanger.Location = new System.Drawing.Point(0, 0);
            this.lcgHeatExchanger.Name = "lcgHeatExchanger";
            this.lcgHeatExchanger.Size = new System.Drawing.Size(1220, 624);
            this.lcgHeatExchanger.Text = "Теплообменник ";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ucAllHeatExchanger1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1220, 624);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcgCyclonAndScrubber
            // 
            this.lcgCyclonAndScrubber.CustomizationFormText = "Циклон и скруббер";
            this.lcgCyclonAndScrubber.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcgCyclonAndScrubber.Location = new System.Drawing.Point(0, 0);
            this.lcgCyclonAndScrubber.Name = "lcgCyclonAndScrubber";
            this.lcgCyclonAndScrubber.Size = new System.Drawing.Size(1220, 624);
            this.lcgCyclonAndScrubber.Text = "Циклон и скруббер";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucCyclonAndScrubber1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1220, 624);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // styleController1
            // 
            this.styleController1.LookAndFeel.SkinName = "iMaginary";
            this.styleController1.LookAndFeel.UseDefaultLookAndFeel = false;
            // 
            // frmTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 762);
            this.Controls.Add(this.layoutControl1);
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgFinishCleaning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCommonView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDrumTypeFurnace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReheatChamber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgHeatExchanger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCyclonAndScrubber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ucCyclonAndScrubber ucCyclonAndScrubber1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcgCyclonAndScrubber;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lcgCommonView;
        private DevExpress.XtraLayout.LayoutControlGroup lcgDrumTypeFurnace;
        private DevExpress.XtraLayout.LayoutControlGroup lcgReheatChamber;
        private DevExpress.XtraLayout.LayoutControlGroup lcgHeatExchanger;
        private DevExpress.XtraLayout.LayoutControlGroup lcgFinishCleaning;
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
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private HeatExchanger.ucAllHeatExchanger ucAllHeatExchanger1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        //todo:private TP.FinishCleaning.ucFinishCleaning ucFinishCleaning1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        internal DevExpress.XtraEditors.SpinEdit spinEdit1;
        internal DevExpress.XtraEditors.SpinEdit spinEdit2;
        internal TP.ReheatChamber.ucReheatChamber ucReheatChamber1;
        private ChannelController channelController1;
        private TP.FinishCleaning.ucFinishCleaning ucFinishCleaning1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private TP.DrumTypeFurnace.DrumTypeFurnace drumTypeFurnace1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}