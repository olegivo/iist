using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UICommon;

namespace TP.ReheatChamber
{
    partial class ucReheatChamber
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> if managed resources should be dis<see langword="false"/>langword="<see langword="false"<see langword="false"/>; otherwise, false.</param>
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
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.ucValve10 = new UICommon.ucValve();
            this.ucValve11 = new UICommon.ucValve();
            this.ucIndicator5 = new UICommon.ucIndicator();
            this.ucIndicator4 = new UICommon.ucIndicator();
            this.ucIndicator3 = new UICommon.ucIndicator();
            this.ucIndicator2 = new UICommon.ucIndicator();
            this.ucIndicator1 = new UICommon.ucIndicator();
            this.ucPump1 = new UICommon.ucPump();
            this.ucTube1 = new UICommon.ucTube();
            this.ucLine23 = new UICommon.ucLine();
            this.ucLine28 = new UICommon.ucLine();
            this.ucLine39 = new UICommon.ucLine();
            this.ucLine37 = new UICommon.ucLine();
            this.ucLine16 = new UICommon.ucLine();
            this.ucBurner3 = new UICommon.ucBurner();
            this.ucBurner2 = new UICommon.ucBurner();
            this.ucBox4 = new UICommon.ucBox();
            this.ucBox3 = new UICommon.ucBox();
            this.ucBox2 = new UICommon.ucBox();
            this.ucValve13 = new UICommon.ucValve();
            this.ucValve12 = new UICommon.ucValve();
            this.ucValve8 = new UICommon.ucValve();
            this.ucValve9 = new UICommon.ucValve();
            this.ucValve7 = new UICommon.ucValve();
            this.ucValve6 = new UICommon.ucValve();
            this.ucValve5 = new UICommon.ucValve();
            this.ucValve4 = new UICommon.ucValve();
            this.ucValve3 = new UICommon.ucValve();
            this.ucValve2 = new UICommon.ucValve();
            this.ucValve1 = new UICommon.ucValve();
            this.ucPump4 = new UICommon.ucPump();
            this.ucPump2 = new UICommon.ucPump();
            this.ucPump3 = new UICommon.ucPump();
            this.ucLine3 = new UICommon.ucLine();
            this.ucLine21 = new UICommon.ucLine();
            this.ucLine32 = new UICommon.ucLine();
            this.ucLine31 = new UICommon.ucLine();
            this.ucLine27 = new UICommon.ucLine();
            this.ucLine25 = new UICommon.ucLine();
            this.ucLine17 = new UICommon.ucLine();
            this.ucLine33 = new UICommon.ucLine();
            this.ucLine20 = new UICommon.ucLine();
            this.ucLine9 = new UICommon.ucLine();
            this.ucLine6 = new UICommon.ucLine();
            this.ucLine43 = new UICommon.ucLine();
            this.ucLine40 = new UICommon.ucLine();
            this.ucLine42 = new UICommon.ucLine();
            this.ucLine41 = new UICommon.ucLine();
            this.ucLine15 = new UICommon.ucLine();
            this.ucLine14 = new UICommon.ucLine();
            this.ucLine34 = new UICommon.ucLine();
            this.ucLine38 = new UICommon.ucLine();
            this.ucLine36 = new UICommon.ucLine();
            this.ucLine2 = new UICommon.ucLine();
            this.ucLine26 = new UICommon.ucLine();
            this.ucLine30 = new UICommon.ucLine();
            this.ucLine29 = new UICommon.ucLine();
            this.ucLine24 = new UICommon.ucLine();
            this.ucLine1 = new UICommon.ucLine();
            this.ucLine19 = new UICommon.ucLine();
            this.ucLine18 = new UICommon.ucLine();
            this.ucLine22 = new UICommon.ucLine();
            this.ucLine4 = new UICommon.ucLine();
            this.ucCaption1 = new UICommon.ucCaptioned();
            this.ucCaptioned1 = new UICommon.ucCaptioned();
            this.ucCaption2 = new UICommon.ucCaptioned();
            this.ucCaption3 = new UICommon.ucCaptioned();
            this.ucCaption4 = new UICommon.ucCaptioned();
            this.ucLine35 = new UICommon.ucLine();
            this.ucLine13 = new UICommon.ucLine();
            this.ucLine11 = new UICommon.ucLine();
            this.ucLine12 = new UICommon.ucLine();
            this.ucLine10 = new UICommon.ucLine();
            this.ucLine5 = new UICommon.ucLine();
            this.ucBox1 = new UICommon.ucBox();
            this.ucLine7 = new UICommon.ucLine();
            this.ucLine8 = new UICommon.ucLine();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(521, 712);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(78, 21);
            this.numericUpDown4.TabIndex = 25;
            this.numericUpDown4.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(371, 712);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(78, 21);
            this.numericUpDown3.TabIndex = 25;
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(211, 712);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(78, 21);
            this.numericUpDown2.TabIndex = 25;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(8, 304);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(78, 21);
            this.numericUpDown1.TabIndex = 25;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(88, 304);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(78, 21);
            this.numericUpDown5.TabIndex = 25;
            this.numericUpDown5.ValueChanged += new System.EventHandler(this.numericUpDown5_ValueChanged);
            // 
            // ucValve10
            // 
            this.ucValve10.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve10.Appearance.Options.UseBackColor = true;
            this.ucValve10.Location = new System.Drawing.Point(718, 476);
            this.ucValve10.Name = "ucValve10";
            this.ucValve10.Size = new System.Drawing.Size(40, 20);
            this.ucValve10.TabIndex = 8;
            // 
            // ucValve11
            // 
            this.ucValve11.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve11.Appearance.Options.UseBackColor = true;
            this.ucValve11.Location = new System.Drawing.Point(718, 517);
            this.ucValve11.Name = "ucValve11";
            this.ucValve11.Size = new System.Drawing.Size(40, 20);
            this.ucValve11.TabIndex = 8;
            // 
            // ucIndicator5
            // 
            this.ucIndicator5.AllowedMaxValue = 1050F;
            this.ucIndicator5.AllowedMinValue = 0F;
            this.ucIndicator5.Caption = "TP3:";
            this.ucIndicator5.EditValue = 0F;
            this.ucIndicator5.Location = new System.Drawing.Point(88, 160);
            this.ucIndicator5.MaxValue = 1500F;
            this.ucIndicator5.MinValue = 0F;
            this.ucIndicator5.Name = "ucIndicator5";
            this.ucIndicator5.Size = new System.Drawing.Size(70, 120);
            this.ucIndicator5.TabIndex = 24;
            this.ucIndicator5.TickCount = 0;
            // 
            // ucIndicator4
            // 
            this.ucIndicator4.AllowedMaxValue = 300F;
            this.ucIndicator4.AllowedMinValue = 0F;
            this.ucIndicator4.Caption = "ДУ11:";
            this.ucIndicator4.EditValue = 0F;
            this.ucIndicator4.Location = new System.Drawing.Point(520, 592);
            this.ucIndicator4.MaxValue = 1500F;
            this.ucIndicator4.MinValue = 0F;
            this.ucIndicator4.Name = "ucIndicator4";
            this.ucIndicator4.Size = new System.Drawing.Size(70, 120);
            this.ucIndicator4.TabIndex = 23;
            this.ucIndicator4.TickCount = 0;
            // 
            // ucIndicator3
            // 
            this.ucIndicator3.AllowedMaxValue = 300F;
            this.ucIndicator3.AllowedMinValue = 0F;
            this.ucIndicator3.Caption = "ДУ1:";
            this.ucIndicator3.EditValue = 0F;
            this.ucIndicator3.Location = new System.Drawing.Point(370, 592);
            this.ucIndicator3.MaxValue = 1500F;
            this.ucIndicator3.MinValue = 0F;
            this.ucIndicator3.Name = "ucIndicator3";
            this.ucIndicator3.Size = new System.Drawing.Size(70, 120);
            this.ucIndicator3.TabIndex = 22;
            this.ucIndicator3.TickCount = 0;
            // 
            // ucIndicator2
            // 
            this.ucIndicator2.AllowedMaxValue = 300F;
            this.ucIndicator2.AllowedMinValue = 0F;
            this.ucIndicator2.Caption = "ДУ4:";
            this.ucIndicator2.EditValue = 0F;
            this.ucIndicator2.Location = new System.Drawing.Point(210, 592);
            this.ucIndicator2.MaxValue = 1500F;
            this.ucIndicator2.MinValue = 0F;
            this.ucIndicator2.Name = "ucIndicator2";
            this.ucIndicator2.Size = new System.Drawing.Size(70, 120);
            this.ucIndicator2.TabIndex = 21;
            this.ucIndicator2.TickCount = 0;
            // 
            // ucIndicator1
            // 
            this.ucIndicator1.AllowedMaxValue = 0F;
            this.ucIndicator1.AllowedMinValue = -5F;
            this.ucIndicator1.Caption = "P:";
            this.ucIndicator1.EditValue = 0F;
            this.ucIndicator1.Location = new System.Drawing.Point(10, 160);
            this.ucIndicator1.MaxValue = 0F;
            this.ucIndicator1.MinValue = -25F;
            this.ucIndicator1.Name = "ucIndicator1";
            this.ucIndicator1.Size = new System.Drawing.Size(70, 120);
            this.ucIndicator1.TabIndex = 20;
            this.ucIndicator1.TickCount = 0;
            // 
            // ucPump1
            // 
            this.ucPump1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucPump1.Appearance.Options.UseBackColor = true;
            this.ucPump1.Location = new System.Drawing.Point(73, 470);
            this.ucPump1.Name = "ucPump1";
            this.ucPump1.Size = new System.Drawing.Size(40, 40);
            this.ucPump1.TabIndex = 3;
            // 
            // ucTube1
            // 
            this.ucTube1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucTube1.Appearance.Options.UseBackColor = true;
            this.ucTube1.IsSmokes = false;
            this.ucTube1.Location = new System.Drawing.Point(872, 39);
            this.ucTube1.Name = "ucTube1";
            this.ucTube1.Size = new System.Drawing.Size(121, 527);
            this.ucTube1.TabIndex = 17;
            // 
            // ucLine23
            // 
            this.ucLine23.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine23.Appearance.Options.UseBackColor = true;
            this.ucLine23.Color = UICommon.LineColor.Green;
            this.ucLine23.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine23.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine23.LineWidth = 5;
            this.ucLine23.Location = new System.Drawing.Point(448, 440);
            this.ucLine23.Name = "ucLine23";
            this.ucLine23.Size = new System.Drawing.Size(8, 32);
            this.ucLine23.TabIndex = 10;
            // 
            // ucLine28
            // 
            this.ucLine28.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine28.Appearance.Options.UseBackColor = true;
            this.ucLine28.Color = UICommon.LineColor.Green;
            this.ucLine28.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine28.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine28.LineWidth = 5;
            this.ucLine28.Location = new System.Drawing.Point(288, 440);
            this.ucLine28.Name = "ucLine28";
            this.ucLine28.Size = new System.Drawing.Size(8, 32);
            this.ucLine28.TabIndex = 10;
            // 
            // ucLine39
            // 
            this.ucLine39.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine39.Appearance.Options.UseBackColor = true;
            this.ucLine39.Color = UICommon.LineColor.LightBlue;
            this.ucLine39.LineWidth = 5;
            this.ucLine39.Location = new System.Drawing.Point(424, 232);
            this.ucLine39.Name = "ucLine39";
            this.ucLine39.Size = new System.Drawing.Size(55, 8);
            this.ucLine39.TabIndex = 11;
            // 
            // ucLine37
            // 
            this.ucLine37.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine37.Appearance.Options.UseBackColor = true;
            this.ucLine37.Color = UICommon.LineColor.LightBlue;
            this.ucLine37.LineWidth = 5;
            this.ucLine37.Location = new System.Drawing.Point(424, 125);
            this.ucLine37.Name = "ucLine37";
            this.ucLine37.Size = new System.Drawing.Size(55, 8);
            this.ucLine37.TabIndex = 11;
            // 
            // ucLine16
            // 
            this.ucLine16.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine16.Appearance.Options.UseBackColor = true;
            this.ucLine16.Color = UICommon.LineColor.LightBlue;
            this.ucLine16.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine16.LineWidth = 5;
            this.ucLine16.Location = new System.Drawing.Point(680, 130);
            this.ucLine16.Name = "ucLine16";
            this.ucLine16.Size = new System.Drawing.Size(8, 108);
            this.ucLine16.TabIndex = 11;
            // 
            // ucBurner3
            // 
            this.ucBurner3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBurner3.Appearance.Options.UseBackColor = true;
            this.ucBurner3.BurnerStatus = true;
            this.ucBurner3.Caption = null;
            this.ucBurner3.Location = new System.Drawing.Point(376, 272);
            this.ucBurner3.Name = "ucBurner3";
            this.ucBurner3.Size = new System.Drawing.Size(60, 40);
            this.ucBurner3.TabIndex = 1;
            // 
            // ucBurner2
            // 
            this.ucBurner2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBurner2.Appearance.Options.UseBackColor = true;
            this.ucBurner2.BurnerStatus = false;
            this.ucBurner2.Caption = null;
            this.ucBurner2.Location = new System.Drawing.Point(376, 168);
            this.ucBurner2.Name = "ucBurner2";
            this.ucBurner2.Size = new System.Drawing.Size(60, 40);
            this.ucBurner2.TabIndex = 1;
            // 
            // ucBox4
            // 
            this.ucBox4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBox4.Appearance.Options.UseBackColor = true;
            this.ucBox4.Caption = "ПТ";
            this.ucBox4.Location = new System.Drawing.Point(530, 470);
            this.ucBox4.Name = "ucBox4";
            this.ucBox4.Size = new System.Drawing.Size(100, 100);
            this.ucBox4.TabIndex = 9;
            // 
            // ucBox3
            // 
            this.ucBox3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBox3.Appearance.Options.UseBackColor = true;
            this.ucBox3.Caption = "НЕ";
            this.ucBox3.Location = new System.Drawing.Point(370, 470);
            this.ucBox3.Name = "ucBox3";
            this.ucBox3.Size = new System.Drawing.Size(100, 100);
            this.ucBox3.TabIndex = 9;
            // 
            // ucBox2
            // 
            this.ucBox2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBox2.Appearance.Options.UseBackColor = true;
            this.ucBox2.Caption = "РЕ";
            this.ucBox2.Location = new System.Drawing.Point(210, 470);
            this.ucBox2.Name = "ucBox2";
            this.ucBox2.Size = new System.Drawing.Size(100, 100);
            this.ucBox2.TabIndex = 9;
            // 
            // ucValve13
            // 
            this.ucValve13.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve13.Appearance.Options.UseBackColor = true;
            this.ucValve13.Location = new System.Drawing.Point(64, 368);
            this.ucValve13.Name = "ucValve13";
            this.ucValve13.Size = new System.Drawing.Size(40, 20);
            this.ucValve13.TabIndex = 8;
            // 
            // ucValve12
            // 
            this.ucValve12.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve12.Appearance.Options.UseBackColor = true;
            this.ucValve12.Location = new System.Drawing.Point(64, 416);
            this.ucValve12.Name = "ucValve12";
            this.ucValve12.Size = new System.Drawing.Size(40, 20);
            this.ucValve12.TabIndex = 8;
            // 
            // ucValve8
            // 
            this.ucValve8.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve8.Appearance.Options.UseBackColor = true;
            this.ucValve8.Location = new System.Drawing.Point(146, 536);
            this.ucValve8.Name = "ucValve8";
            this.ucValve8.Size = new System.Drawing.Size(40, 20);
            this.ucValve8.TabIndex = 8;
            // 
            // ucValve9
            // 
            this.ucValve9.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve9.Appearance.Options.UseBackColor = true;
            this.ucValve9.Location = new System.Drawing.Point(544, 368);
            this.ucValve9.Name = "ucValve9";
            this.ucValve9.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ucValve9.Size = new System.Drawing.Size(20, 40);
            this.ucValve9.TabIndex = 8;
            // 
            // ucValve7
            // 
            this.ucValve7.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve7.Appearance.Options.UseBackColor = true;
            this.ucValve7.Location = new System.Drawing.Point(146, 480);
            this.ucValve7.Name = "ucValve7";
            this.ucValve7.Size = new System.Drawing.Size(40, 20);
            this.ucValve7.TabIndex = 8;
            // 
            // ucValve6
            // 
            this.ucValve6.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve6.Appearance.Options.UseBackColor = true;
            this.ucValve6.Location = new System.Drawing.Point(472, 293);
            this.ucValve6.Name = "ucValve6";
            this.ucValve6.Size = new System.Drawing.Size(40, 20);
            this.ucValve6.TabIndex = 8;
            // 
            // ucValve5
            // 
            this.ucValve5.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve5.Appearance.Options.UseBackColor = true;
            this.ucValve5.Location = new System.Drawing.Point(472, 267);
            this.ucValve5.Name = "ucValve5";
            this.ucValve5.Size = new System.Drawing.Size(40, 20);
            this.ucValve5.TabIndex = 8;
            // 
            // ucValve4
            // 
            this.ucValve4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve4.Appearance.Options.UseBackColor = true;
            this.ucValve4.Location = new System.Drawing.Point(472, 229);
            this.ucValve4.Name = "ucValve4";
            this.ucValve4.Size = new System.Drawing.Size(40, 20);
            this.ucValve4.TabIndex = 8;
            // 
            // ucValve3
            // 
            this.ucValve3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve3.Appearance.Options.UseBackColor = true;
            this.ucValve3.Location = new System.Drawing.Point(472, 120);
            this.ucValve3.Name = "ucValve3";
            this.ucValve3.Size = new System.Drawing.Size(40, 20);
            this.ucValve3.TabIndex = 7;
            // 
            // ucValve2
            // 
            this.ucValve2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve2.Appearance.Options.UseBackColor = true;
            this.ucValve2.Location = new System.Drawing.Point(472, 187);
            this.ucValve2.Name = "ucValve2";
            this.ucValve2.Size = new System.Drawing.Size(40, 20);
            this.ucValve2.TabIndex = 6;
            // 
            // ucValve1
            // 
            this.ucValve1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve1.Appearance.Options.UseBackColor = true;
            this.ucValve1.Location = new System.Drawing.Point(472, 161);
            this.ucValve1.Name = "ucValve1";
            this.ucValve1.Size = new System.Drawing.Size(40, 20);
            this.ucValve1.TabIndex = 5;
            // 
            // ucPump4
            // 
            this.ucPump4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucPump4.Appearance.Options.UseBackColor = true;
            this.ucPump4.Location = new System.Drawing.Point(-266, 718);
            this.ucPump4.Name = "ucPump4";
            this.ucPump4.Size = new System.Drawing.Size(40, 40);
            this.ucPump4.TabIndex = 3;
            // 
            // ucPump2
            // 
            this.ucPump2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucPump2.Appearance.Options.UseBackColor = true;
            this.ucPump2.Location = new System.Drawing.Point(73, 526);
            this.ucPump2.Name = "ucPump2";
            this.ucPump2.Size = new System.Drawing.Size(40, 40);
            this.ucPump2.TabIndex = 3;
            // 
            // ucPump3
            // 
            this.ucPump3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucPump3.Appearance.Options.UseBackColor = true;
            this.ucPump3.Location = new System.Drawing.Point(-266, 676);
            this.ucPump3.Name = "ucPump3";
            this.ucPump3.Size = new System.Drawing.Size(40, 40);
            this.ucPump3.TabIndex = 3;
            // 
            // ucLine3
            // 
            this.ucLine3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine3.Appearance.Options.UseBackColor = true;
            this.ucLine3.Color = UICommon.LineColor.Green;
            this.ucLine3.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine3.LineWidth = 5;
            this.ucLine3.Location = new System.Drawing.Point(32, 424);
            this.ucLine3.Name = "ucLine3";
            this.ucLine3.Size = new System.Drawing.Size(823, 8);
            this.ucLine3.TabIndex = 10;
            // 
            // ucLine21
            // 
            this.ucLine21.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine21.Appearance.Options.UseBackColor = true;
            this.ucLine21.Color = UICommon.LineColor.Green;
            this.ucLine21.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine21.LineWidth = 5;
            this.ucLine21.Location = new System.Drawing.Point(120, 376);
            this.ucLine21.Name = "ucLine21";
            this.ucLine21.Size = new System.Drawing.Size(8, 56);
            this.ucLine21.TabIndex = 10;
            // 
            // ucLine32
            // 
            this.ucLine32.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine32.Appearance.Options.UseBackColor = true;
            this.ucLine32.Color = UICommon.LineColor.Green;
            this.ucLine32.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine32.LineWidth = 5;
            this.ucLine32.Location = new System.Drawing.Point(544, 568);
            this.ucLine32.Name = "ucLine32";
            this.ucLine32.Size = new System.Drawing.Size(8, 24);
            this.ucLine32.TabIndex = 10;
            // 
            // ucLine31
            // 
            this.ucLine31.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine31.Appearance.Options.UseBackColor = true;
            this.ucLine31.Color = UICommon.LineColor.Green;
            this.ucLine31.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine31.LineWidth = 5;
            this.ucLine31.Location = new System.Drawing.Point(384, 568);
            this.ucLine31.Name = "ucLine31";
            this.ucLine31.Size = new System.Drawing.Size(8, 24);
            this.ucLine31.TabIndex = 10;
            // 
            // ucLine27
            // 
            this.ucLine27.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine27.Appearance.Options.UseBackColor = true;
            this.ucLine27.Color = UICommon.LineColor.Green;
            this.ucLine27.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine27.LineWidth = 5;
            this.ucLine27.Location = new System.Drawing.Point(496, 440);
            this.ucLine27.Name = "ucLine27";
            this.ucLine27.Size = new System.Drawing.Size(8, 152);
            this.ucLine27.TabIndex = 10;
            // 
            // ucLine25
            // 
            this.ucLine25.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine25.Appearance.Options.UseBackColor = true;
            this.ucLine25.Color = UICommon.LineColor.Green;
            this.ucLine25.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine25.LineWidth = 5;
            this.ucLine25.Location = new System.Drawing.Point(336, 440);
            this.ucLine25.Name = "ucLine25";
            this.ucLine25.Size = new System.Drawing.Size(8, 152);
            this.ucLine25.TabIndex = 10;
            // 
            // ucLine17
            // 
            this.ucLine17.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine17.Appearance.Options.UseBackColor = true;
            this.ucLine17.Color = UICommon.LineColor.Green;
            this.ucLine17.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine17.LineWidth = 5;
            this.ucLine17.Location = new System.Drawing.Point(552, 104);
            this.ucLine17.Name = "ucLine17";
            this.ucLine17.Size = new System.Drawing.Size(8, 328);
            this.ucLine17.TabIndex = 10;
            // 
            // ucLine33
            // 
            this.ucLine33.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine33.Appearance.Options.UseBackColor = true;
            this.ucLine33.Color = UICommon.LineColor.Green;
            this.ucLine33.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine33.LineWidth = 5;
            this.ucLine33.Location = new System.Drawing.Point(30, 424);
            this.ucLine33.Name = "ucLine33";
            this.ucLine33.Size = new System.Drawing.Size(10, 56);
            this.ucLine33.TabIndex = 10;
            // 
            // ucLine20
            // 
            this.ucLine20.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine20.Appearance.Options.UseBackColor = true;
            this.ucLine20.Color = UICommon.LineColor.Green;
            this.ucLine20.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine20.LineWidth = 5;
            this.ucLine20.Location = new System.Drawing.Point(8, 376);
            this.ucLine20.Name = "ucLine20";
            this.ucLine20.Size = new System.Drawing.Size(8, 155);
            this.ucLine20.TabIndex = 10;
            // 
            // ucLine9
            // 
            this.ucLine9.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine9.Appearance.Options.UseBackColor = true;
            this.ucLine9.Color = UICommon.LineColor.Red;
            this.ucLine9.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine9.LineWidth = 5;
            this.ucLine9.Location = new System.Drawing.Point(824, 192);
            this.ucLine9.Name = "ucLine9";
            this.ucLine9.Size = new System.Drawing.Size(8, 336);
            this.ucLine9.TabIndex = 10;
            // 
            // ucLine6
            // 
            this.ucLine6.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine6.Appearance.Options.UseBackColor = true;
            this.ucLine6.Color = UICommon.LineColor.Red;
            this.ucLine6.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine6.LineWidth = 5;
            this.ucLine6.Location = new System.Drawing.Point(776, 169);
            this.ucLine6.Name = "ucLine6";
            this.ucLine6.Size = new System.Drawing.Size(8, 320);
            this.ucLine6.TabIndex = 10;
            // 
            // ucLine43
            // 
            this.ucLine43.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine43.Appearance.Options.UseBackColor = true;
            this.ucLine43.Color = UICommon.LineColor.Red;
            this.ucLine43.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine43.LineWidth = 5;
            this.ucLine43.Location = new System.Drawing.Point(432, 296);
            this.ucLine43.Name = "ucLine43";
            this.ucLine43.Size = new System.Drawing.Size(38, 8);
            this.ucLine43.TabIndex = 10;
            // 
            // ucLine40
            // 
            this.ucLine40.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine40.Appearance.Options.UseBackColor = true;
            this.ucLine40.Color = UICommon.LineColor.Red;
            this.ucLine40.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine40.LineWidth = 5;
            this.ucLine40.Location = new System.Drawing.Point(432, 192);
            this.ucLine40.Name = "ucLine40";
            this.ucLine40.Size = new System.Drawing.Size(38, 8);
            this.ucLine40.TabIndex = 10;
            // 
            // ucLine42
            // 
            this.ucLine42.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine42.Appearance.Options.UseBackColor = true;
            this.ucLine42.Color = UICommon.LineColor.Red;
            this.ucLine42.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine42.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine42.LineWidth = 5;
            this.ucLine42.Location = new System.Drawing.Point(432, 272);
            this.ucLine42.Name = "ucLine42";
            this.ucLine42.Size = new System.Drawing.Size(39, 8);
            this.ucLine42.TabIndex = 10;
            // 
            // ucLine41
            // 
            this.ucLine41.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine41.Appearance.Options.UseBackColor = true;
            this.ucLine41.Color = UICommon.LineColor.Red;
            this.ucLine41.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine41.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine41.LineWidth = 5;
            this.ucLine41.Location = new System.Drawing.Point(432, 168);
            this.ucLine41.Name = "ucLine41";
            this.ucLine41.Size = new System.Drawing.Size(39, 8);
            this.ucLine41.TabIndex = 10;
            // 
            // ucLine15
            // 
            this.ucLine15.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine15.Appearance.Options.UseBackColor = true;
            this.ucLine15.Color = UICommon.LineColor.LightBlue;
            this.ucLine15.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine15.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine15.LineWidth = 5;
            this.ucLine15.Location = new System.Drawing.Point(513, 232);
            this.ucLine15.Name = "ucLine15";
            this.ucLine15.Size = new System.Drawing.Size(173, 8);
            this.ucLine15.TabIndex = 10;
            // 
            // ucLine14
            // 
            this.ucLine14.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine14.Appearance.Options.UseBackColor = true;
            this.ucLine14.Color = UICommon.LineColor.LightBlue;
            this.ucLine14.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine14.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine14.LineWidth = 5;
            this.ucLine14.Location = new System.Drawing.Point(513, 125);
            this.ucLine14.Name = "ucLine14";
            this.ucLine14.Size = new System.Drawing.Size(342, 8);
            this.ucLine14.TabIndex = 10;
            // 
            // ucLine34
            // 
            this.ucLine34.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine34.Appearance.Options.UseBackColor = true;
            this.ucLine34.Color = UICommon.LineColor.Orange;
            this.ucLine34.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine34.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine34.Location = new System.Drawing.Point(90, 117);
            this.ucLine34.Name = "ucLine34";
            this.ucLine34.Size = new System.Drawing.Size(97, 21);
            this.ucLine34.TabIndex = 10;
            // 
            // ucLine38
            // 
            this.ucLine38.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine38.Appearance.Options.UseBackColor = true;
            this.ucLine38.Color = UICommon.LineColor.LightBlue;
            this.ucLine38.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine38.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine38.LineWidth = 5;
            this.ucLine38.Location = new System.Drawing.Point(424, 232);
            this.ucLine38.Name = "ucLine38";
            this.ucLine38.Size = new System.Drawing.Size(8, 38);
            this.ucLine38.TabIndex = 10;
            // 
            // ucLine36
            // 
            this.ucLine36.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine36.Appearance.Options.UseBackColor = true;
            this.ucLine36.Color = UICommon.LineColor.LightBlue;
            this.ucLine36.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine36.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine36.LineWidth = 5;
            this.ucLine36.Location = new System.Drawing.Point(424, 130);
            this.ucLine36.Name = "ucLine36";
            this.ucLine36.Size = new System.Drawing.Size(8, 38);
            this.ucLine36.TabIndex = 10;
            // 
            // ucLine2
            // 
            this.ucLine2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine2.Appearance.Options.UseBackColor = true;
            this.ucLine2.Color = UICommon.LineColor.Green;
            this.ucLine2.LineWidth = 5;
            this.ucLine2.Location = new System.Drawing.Point(112, 544);
            this.ucLine2.Name = "ucLine2";
            this.ucLine2.Size = new System.Drawing.Size(104, 8);
            this.ucLine2.TabIndex = 10;
            // 
            // ucLine26
            // 
            this.ucLine26.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine26.Appearance.Options.UseBackColor = true;
            this.ucLine26.Color = UICommon.LineColor.Green;
            this.ucLine26.LineWidth = 5;
            this.ucLine26.Location = new System.Drawing.Point(448, 440);
            this.ucLine26.Name = "ucLine26";
            this.ucLine26.Size = new System.Drawing.Size(56, 8);
            this.ucLine26.TabIndex = 10;
            // 
            // ucLine30
            // 
            this.ucLine30.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine30.Appearance.Options.UseBackColor = true;
            this.ucLine30.Color = UICommon.LineColor.Green;
            this.ucLine30.LineWidth = 5;
            this.ucLine30.Location = new System.Drawing.Point(496, 584);
            this.ucLine30.Name = "ucLine30";
            this.ucLine30.Size = new System.Drawing.Size(56, 8);
            this.ucLine30.TabIndex = 10;
            // 
            // ucLine29
            // 
            this.ucLine29.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine29.Appearance.Options.UseBackColor = true;
            this.ucLine29.Color = UICommon.LineColor.Green;
            this.ucLine29.LineWidth = 5;
            this.ucLine29.Location = new System.Drawing.Point(336, 584);
            this.ucLine29.Name = "ucLine29";
            this.ucLine29.Size = new System.Drawing.Size(56, 8);
            this.ucLine29.TabIndex = 10;
            // 
            // ucLine24
            // 
            this.ucLine24.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine24.Appearance.Options.UseBackColor = true;
            this.ucLine24.Color = UICommon.LineColor.Green;
            this.ucLine24.LineWidth = 5;
            this.ucLine24.Location = new System.Drawing.Point(288, 440);
            this.ucLine24.Name = "ucLine24";
            this.ucLine24.Size = new System.Drawing.Size(56, 8);
            this.ucLine24.TabIndex = 10;
            // 
            // ucLine1
            // 
            this.ucLine1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine1.Appearance.Options.UseBackColor = true;
            this.ucLine1.Color = UICommon.LineColor.Green;
            this.ucLine1.LineWidth = 5;
            this.ucLine1.Location = new System.Drawing.Point(112, 488);
            this.ucLine1.Name = "ucLine1";
            this.ucLine1.Size = new System.Drawing.Size(104, 8);
            this.ucLine1.TabIndex = 10;
            // 
            // ucLine19
            // 
            this.ucLine19.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine19.Appearance.Options.UseBackColor = true;
            this.ucLine19.Color = UICommon.LineColor.Green;
            this.ucLine19.LineWidth = 5;
            this.ucLine19.Location = new System.Drawing.Point(32, 472);
            this.ucLine19.Name = "ucLine19";
            this.ucLine19.Size = new System.Drawing.Size(48, 8);
            this.ucLine19.TabIndex = 10;
            // 
            // ucLine18
            // 
            this.ucLine18.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine18.Appearance.Options.UseBackColor = true;
            this.ucLine18.Color = UICommon.LineColor.Green;
            this.ucLine18.LineWidth = 5;
            this.ucLine18.Location = new System.Drawing.Point(8, 528);
            this.ucLine18.Name = "ucLine18";
            this.ucLine18.Size = new System.Drawing.Size(72, 8);
            this.ucLine18.TabIndex = 10;
            // 
            // ucLine22
            // 
            this.ucLine22.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine22.Appearance.Options.UseBackColor = true;
            this.ucLine22.Color = UICommon.LineColor.Green;
            this.ucLine22.LineWidth = 5;
            this.ucLine22.Location = new System.Drawing.Point(8, 376);
            this.ucLine22.Name = "ucLine22";
            this.ucLine22.Size = new System.Drawing.Size(120, 8);
            this.ucLine22.TabIndex = 10;
            // 
            // ucLine4
            // 
            this.ucLine4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine4.Appearance.Options.UseBackColor = true;
            this.ucLine4.Color = UICommon.LineColor.Green;
            this.ucLine4.LineWidth = 5;
            this.ucLine4.Location = new System.Drawing.Point(391, 317);
            this.ucLine4.Name = "ucLine4";
            this.ucLine4.Size = new System.Drawing.Size(169, 8);
            this.ucLine4.TabIndex = 10;
            // 
            // ucCaption1
            // 
            this.ucCaption1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaption1.Appearance.Options.UseBackColor = true;
            this.ucCaption1.Caption = "газ";
            this.ucCaption1.Location = new System.Drawing.Point(815, 100);
            this.ucCaption1.Name = "ucCaption1";
            this.ucCaption1.Size = new System.Drawing.Size(40, 42);
            this.ucCaption1.TabIndex = 12;
            // 
            // ucCaptioned1
            // 
            this.ucCaptioned1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaptioned1.Appearance.Options.UseBackColor = true;
            this.ucCaptioned1.Caption = "к ЦВТ";
            this.ucCaptioned1.Location = new System.Drawing.Point(780, 400);
            this.ucCaptioned1.Name = "ucCaptioned1";
            this.ucCaptioned1.Size = new System.Drawing.Size(50, 19);
            this.ucCaptioned1.TabIndex = 13;
            // 
            // ucCaption2
            // 
            this.ucCaption2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaption2.Appearance.Options.UseBackColor = true;
            this.ucCaption2.Caption = "к горелкам";
            this.ucCaption2.Location = new System.Drawing.Point(753, 495);
            this.ucCaption2.Name = "ucCaption2";
            this.ucCaption2.Size = new System.Drawing.Size(91, 19);
            this.ucCaption2.TabIndex = 13;
            // 
            // ucCaption3
            // 
            this.ucCaption3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaption3.Appearance.Options.UseBackColor = true;
            this.ucCaption3.Caption = "невтешлам";
            this.ucCaption3.Location = new System.Drawing.Point(48, 395);
            this.ucCaption3.Name = "ucCaption3";
            this.ucCaption3.Size = new System.Drawing.Size(83, 24);
            this.ucCaption3.TabIndex = 15;
            // 
            // ucCaption4
            // 
            this.ucCaption4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaption4.Appearance.Options.UseBackColor = true;
            this.ucCaption4.Caption = "к ТО";
            this.ucCaption4.Location = new System.Drawing.Point(130, 90);
            this.ucCaption4.Name = "ucCaption4";
            this.ucCaption4.Size = new System.Drawing.Size(53, 23);
            this.ucCaption4.TabIndex = 16;
            // 
            // ucLine35
            // 
            this.ucLine35.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine35.Appearance.Options.UseBackColor = true;
            this.ucLine35.Color = UICommon.LineColor.Orange;
            this.ucLine35.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine35.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine35.Location = new System.Drawing.Point(393, 336);
            this.ucLine35.Name = "ucLine35";
            this.ucLine35.Size = new System.Drawing.Size(457, 24);
            this.ucLine35.TabIndex = 10;
            // 
            // ucLine13
            // 
            this.ucLine13.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine13.Appearance.Options.UseBackColor = true;
            this.ucLine13.Color = UICommon.LineColor.Red;
            this.ucLine13.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine13.LineWidth = 5;
            this.ucLine13.Location = new System.Drawing.Point(512, 296);
            this.ucLine13.Name = "ucLine13";
            this.ucLine13.Size = new System.Drawing.Size(314, 8);
            this.ucLine13.TabIndex = 10;
            // 
            // ucLine11
            // 
            this.ucLine11.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine11.Appearance.Options.UseBackColor = true;
            this.ucLine11.Color = UICommon.LineColor.Red;
            this.ucLine11.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine11.LineWidth = 5;
            this.ucLine11.Location = new System.Drawing.Point(512, 192);
            this.ucLine11.Name = "ucLine11";
            this.ucLine11.Size = new System.Drawing.Size(314, 8);
            this.ucLine11.TabIndex = 10;
            // 
            // ucLine12
            // 
            this.ucLine12.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine12.Appearance.Options.UseBackColor = true;
            this.ucLine12.Color = UICommon.LineColor.Red;
            this.ucLine12.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine12.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine12.LineWidth = 5;
            this.ucLine12.Location = new System.Drawing.Point(512, 272);
            this.ucLine12.Name = "ucLine12";
            this.ucLine12.Size = new System.Drawing.Size(270, 8);
            this.ucLine12.TabIndex = 10;
            // 
            // ucLine10
            // 
            this.ucLine10.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine10.Appearance.Options.UseBackColor = true;
            this.ucLine10.Color = UICommon.LineColor.Red;
            this.ucLine10.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine10.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine10.LineWidth = 5;
            this.ucLine10.Location = new System.Drawing.Point(513, 165);
            this.ucLine10.Name = "ucLine10";
            this.ucLine10.Size = new System.Drawing.Size(270, 8);
            this.ucLine10.TabIndex = 10;
            // 
            // ucLine5
            // 
            this.ucLine5.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine5.Appearance.Options.UseBackColor = true;
            this.ucLine5.Color = UICommon.LineColor.Green;
            this.ucLine5.LineWidth = 5;
            this.ucLine5.Location = new System.Drawing.Point(400, 104);
            this.ucLine5.Name = "ucLine5";
            this.ucLine5.Size = new System.Drawing.Size(160, 8);
            this.ucLine5.TabIndex = 10;
            // 
            // ucBox1
            // 
            this.ucBox1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBox1.Appearance.Options.UseBackColor = true;
            this.ucBox1.Caption = "Камера дожигания";
            this.ucBox1.Location = new System.Drawing.Point(176, 88);
            this.ucBox1.Name = "ucBox1";
            this.ucBox1.Size = new System.Drawing.Size(224, 272);
            this.ucBox1.TabIndex = 9;
            // 
            // ucLine7
            // 
            this.ucLine7.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine7.Appearance.Options.UseBackColor = true;
            this.ucLine7.Color = UICommon.LineColor.Red;
            this.ucLine7.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine7.LineWidth = 5;
            this.ucLine7.Location = new System.Drawing.Point(632, 480);
            this.ucLine7.Name = "ucLine7";
            this.ucLine7.Size = new System.Drawing.Size(223, 8);
            this.ucLine7.TabIndex = 10;
            // 
            // ucLine8
            // 
            this.ucLine8.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine8.Appearance.Options.UseBackColor = true;
            this.ucLine8.Color = UICommon.LineColor.Red;
            this.ucLine8.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine8.LineWidth = 5;
            this.ucLine8.Location = new System.Drawing.Point(632, 520);
            this.ucLine8.Name = "ucLine8";
            this.ucLine8.Size = new System.Drawing.Size(223, 8);
            this.ucLine8.TabIndex = 10;
            // 
            // ucReheatChamber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucValve10);
            this.Controls.Add(this.ucValve11);
            this.Controls.Add(this.numericUpDown5);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.ucIndicator5);
            this.Controls.Add(this.ucIndicator4);
            this.Controls.Add(this.ucIndicator3);
            this.Controls.Add(this.ucIndicator2);
            this.Controls.Add(this.ucIndicator1);
            this.Controls.Add(this.ucPump1);
            this.Controls.Add(this.ucTube1);
            this.Controls.Add(this.ucLine23);
            this.Controls.Add(this.ucLine28);
            this.Controls.Add(this.ucLine39);
            this.Controls.Add(this.ucLine37);
            this.Controls.Add(this.ucLine16);
            this.Controls.Add(this.ucBurner3);
            this.Controls.Add(this.ucBurner2);
            this.Controls.Add(this.ucBox4);
            this.Controls.Add(this.ucBox3);
            this.Controls.Add(this.ucBox2);
            this.Controls.Add(this.ucValve13);
            this.Controls.Add(this.ucValve12);
            this.Controls.Add(this.ucValve8);
            this.Controls.Add(this.ucValve9);
            this.Controls.Add(this.ucValve7);
            this.Controls.Add(this.ucValve6);
            this.Controls.Add(this.ucValve5);
            this.Controls.Add(this.ucValve4);
            this.Controls.Add(this.ucValve3);
            this.Controls.Add(this.ucValve2);
            this.Controls.Add(this.ucValve1);
            this.Controls.Add(this.ucPump4);
            this.Controls.Add(this.ucPump2);
            this.Controls.Add(this.ucPump3);
            this.Controls.Add(this.ucLine3);
            this.Controls.Add(this.ucLine21);
            this.Controls.Add(this.ucLine32);
            this.Controls.Add(this.ucLine31);
            this.Controls.Add(this.ucLine27);
            this.Controls.Add(this.ucLine25);
            this.Controls.Add(this.ucLine17);
            this.Controls.Add(this.ucLine33);
            this.Controls.Add(this.ucLine20);
            this.Controls.Add(this.ucLine9);
            this.Controls.Add(this.ucLine6);
            this.Controls.Add(this.ucLine43);
            this.Controls.Add(this.ucLine40);
            this.Controls.Add(this.ucLine42);
            this.Controls.Add(this.ucLine41);
            this.Controls.Add(this.ucLine15);
            this.Controls.Add(this.ucLine14);
            this.Controls.Add(this.ucLine34);
            this.Controls.Add(this.ucLine38);
            this.Controls.Add(this.ucLine36);
            this.Controls.Add(this.ucLine2);
            this.Controls.Add(this.ucLine26);
            this.Controls.Add(this.ucLine30);
            this.Controls.Add(this.ucLine29);
            this.Controls.Add(this.ucLine24);
            this.Controls.Add(this.ucLine1);
            this.Controls.Add(this.ucLine19);
            this.Controls.Add(this.ucLine18);
            this.Controls.Add(this.ucLine22);
            this.Controls.Add(this.ucLine4);
            this.Controls.Add(this.ucCaption1);
            this.Controls.Add(this.ucCaptioned1);
            this.Controls.Add(this.ucCaption2);
            this.Controls.Add(this.ucCaption3);
            this.Controls.Add(this.ucCaption4);
            this.Controls.Add(this.ucLine35);
            this.Controls.Add(this.ucLine13);
            this.Controls.Add(this.ucLine11);
            this.Controls.Add(this.ucLine12);
            this.Controls.Add(this.ucLine10);
            this.Controls.Add(this.ucLine5);
            this.Controls.Add(this.ucBox1);
            this.Controls.Add(this.ucLine7);
            this.Controls.Add(this.ucLine8);
            this.Name = "ucReheatChamber";
            this.Size = new System.Drawing.Size(1024, 768);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UICommon.ucPump ucPump1;
        private UICommon.ucValve ucValve1;
        private UICommon.ucValve ucValve2;
        private UICommon.ucValve ucValve3;
        private UICommon.ucValve ucValve4;
        private UICommon.ucValve ucValve5;
        private UICommon.ucValve ucValve6;
        private UICommon.ucPump ucPump2;
        private UICommon.ucBox ucBox1;
        private UICommon.ucBurner ucBurner2;
        private UICommon.ucBurner ucBurner3;
        private UICommon.ucBox ucBox2;
        private UICommon.ucPump ucPump3;
        private UICommon.ucValve ucValve7;
        private UICommon.ucPump ucPump4;
        private UICommon.ucValve ucValve8;
        private UICommon.ucLine ucLine3;
        private UICommon.ucValve ucValve9;
        private UICommon.ucLine ucLine4;
        private UICommon.ucLine ucLine5;
        private UICommon.ucLine ucLine6;
        private UICommon.ucValve ucValve10;
        private UICommon.ucValve ucValve11;
        private UICommon.ucLine ucLine7;
        private UICommon.ucLine ucLine8;
        private UICommon.ucLine ucLine10;
        private UICommon.ucLine ucLine11;
        private UICommon.ucLine ucLine14;
        private UICommon.ucLine ucLine15;
        private UICommon.ucLine ucLine16;
        private UICommon.ucCaptioned ucCaption1;
        private UICommon.ucCaptioned ucCaption2;
        private UICommon.ucValve ucValve12;
        private UICommon.ucValve ucValve13;
        private UICommon.ucLine ucLine20;
        private UICommon.ucCaptioned ucCaption3;
        private UICommon.ucLine ucLine28;
        private UICommon.ucLine ucLine34;
        private UICommon.ucCaptioned ucCaption4;
        private UICommon.ucLine ucLine35;
        private UICommon.ucLine ucLine36;
        private UICommon.ucLine ucLine37;
        private UICommon.ucLine ucLine41;
        private UICommon.ucLine ucLine40;
        private ucTube ucTube1;
        private ucBox ucBox3;
        private ucBox ucBox4;
        private ucIndicator ucIndicator1;
        private ucIndicator ucIndicator2;
        private ucIndicator ucIndicator3;
        private ucIndicator ucIndicator4;
        private ucIndicator ucIndicator5;
        private NumericUpDown numericUpDown4;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown2;
        private ucLine ucLine39;
        private ucLine ucLine38;
        private ucLine ucLine12;
        private ucLine ucLine13;
        private ucLine ucLine42;
        private ucLine ucLine43;
        private ucLine ucLine9;
        private ucLine ucLine21;
        private ucLine ucLine22;
        private ucLine ucLine18;
        private ucLine ucLine19;
        private ucLine ucLine1;
        private ucLine ucLine2;
        private ucLine ucLine23;
        private ucLine ucLine24;
        private ucLine ucLine25;
        private ucLine ucLine26;
        private ucLine ucLine29;
        private ucLine ucLine30;
        private ucLine ucLine31;
        private ucLine ucLine32;
        private ucLine ucLine27;
        private ucLine ucLine17;
        private ucCaptioned ucCaptioned1;
        private ucLine ucLine33;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown5;

    }
}
