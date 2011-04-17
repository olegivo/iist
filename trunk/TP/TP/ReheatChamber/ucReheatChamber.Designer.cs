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
            this.ucPump1 = new UICommon.ucPump();
            this.ucValve10 = new UICommon.ucValve();
            this.ucValve11 = new UICommon.ucValve();
            this.ucGauge2 = new UICommon.ucIndicator();
            this.ucGauge1 = new UICommon.ucIndicator();
            this.ucTube1 = new UICommon.ucTube();
            this.ucLine33 = new UICommon.ucLine();
            this.ucLine28 = new UICommon.ucLine();
            this.ucLine18 = new UICommon.ucLine();
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
            this.ucLine9 = new UICommon.ucLine();
            this.ucLine23 = new UICommon.ucLine();
            this.ucLine32 = new UICommon.ucLine();
            this.ucLine26 = new UICommon.ucLine();
            this.ucLine31 = new UICommon.ucLine();
            this.ucLine25 = new UICommon.ucLine();
            this.ucLine30 = new UICommon.ucLine();
            this.ucLine27 = new UICommon.ucLine();
            this.ucLine29 = new UICommon.ucLine();
            this.ucLine24 = new UICommon.ucLine();
            this.ucLine22 = new UICommon.ucLine();
            this.ucLine20 = new UICommon.ucLine();
            this.ucLine6 = new UICommon.ucLine();
            this.ucLine43 = new UICommon.ucLine();
            this.ucLine42 = new UICommon.ucLine();
            this.ucLine40 = new UICommon.ucLine();
            this.ucLine41 = new UICommon.ucLine();
            this.ucLine13 = new UICommon.ucLine();
            this.ucLine12 = new UICommon.ucLine();
            this.ucLine15 = new UICommon.ucLine();
            this.ucLine14 = new UICommon.ucLine();
            this.ucLine34 = new UICommon.ucLine();
            this.ucLine36 = new UICommon.ucLine();
            this.ucLine4 = new UICommon.ucLine();
            this.ucCaption1 = new UICommon.ucCaptioned();
            this.ucCaption2 = new UICommon.ucCaptioned();
            this.ucLine21 = new UICommon.ucLine();
            this.ucCaption3 = new UICommon.ucCaptioned();
            this.ucCaption4 = new UICommon.ucCaptioned();
            this.ucLine35 = new UICommon.ucLine();
            this.ucLine8 = new UICommon.ucLine();
            this.ucLine7 = new UICommon.ucLine();
            this.ucLine11 = new UICommon.ucLine();
            this.ucLine10 = new UICommon.ucLine();
            this.ucBox1 = new UICommon.ucBox();
            this.ucLine5 = new UICommon.ucLine();
            this.ucLine17 = new UICommon.ucLine();
            this.ucLine19 = new UICommon.ucLine();
            this.ucLine2 = new UICommon.ucLine();
            this.ucLine1 = new UICommon.ucLine();
            this.ucLine38 = new UICommon.ucLine();
            this.SuspendLayout();
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
            // ucGauge2
            // 
            this.ucGauge2.AllowedMaxValue = 6F;
            this.ucGauge2.AllowedMinValue = 4F;
            this.ucGauge2.Caption = "ДУ5:";
            this.ucGauge2.EditValue = 0.7F;
            this.ucGauge2.Location = new System.Drawing.Point(426, 611);
            this.ucGauge2.MaxValue = 0.7F;
            this.ucGauge2.MinValue = 0.5F;
            this.ucGauge2.Name = "ucGauge2";
            this.ucGauge2.Size = new System.Drawing.Size(72, 131);
            this.ucGauge2.TabIndex = 19;
            this.ucGauge2.TickCount = 10;
            // 
            // ucGauge1
            // 
            this.ucGauge1.AllowedMaxValue = 6F;
            this.ucGauge1.AllowedMinValue = 4F;
            this.ucGauge1.Caption = "Ph1:";
            this.ucGauge1.EditValue = 3F;
            this.ucGauge1.Location = new System.Drawing.Point(48, 192);
            this.ucGauge1.MaxValue = 14F;
            this.ucGauge1.MinValue = 3F;
            this.ucGauge1.Name = "ucGauge1";
            this.ucGauge1.Size = new System.Drawing.Size(75, 138);
            this.ucGauge1.TabIndex = 18;
            this.ucGauge1.TickCount = 10;
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
            // ucLine33
            // 
            this.ucLine33.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine33.Appearance.Options.UseBackColor = true;
            this.ucLine33.Color = UICommon.LineColor.Green;
            this.ucLine33.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine33.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine33.Location = new System.Drawing.Point(453, 448);
            this.ucLine33.Name = "ucLine33";
            this.ucLine33.Size = new System.Drawing.Size(12, 26);
            this.ucLine33.TabIndex = 10;
            // 
            // ucLine28
            // 
            this.ucLine28.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine28.Appearance.Options.UseBackColor = true;
            this.ucLine28.Color = UICommon.LineColor.Green;
            this.ucLine28.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine28.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine28.Location = new System.Drawing.Point(286, 448);
            this.ucLine28.Name = "ucLine28";
            this.ucLine28.Size = new System.Drawing.Size(13, 26);
            this.ucLine28.TabIndex = 10;
            // 
            // ucLine18
            // 
            this.ucLine18.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine18.Appearance.Options.UseBackColor = true;
            this.ucLine18.Color = UICommon.LineColor.Green;
            this.ucLine18.Location = new System.Drawing.Point(35, 522);
            this.ucLine18.Name = "ucLine18";
            this.ucLine18.Size = new System.Drawing.Size(40, 13);
            this.ucLine18.TabIndex = 14;
            // 
            // ucLine39
            // 
            this.ucLine39.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine39.Appearance.Options.UseBackColor = true;
            this.ucLine39.Color = UICommon.LineColor.LightBlue;
            this.ucLine39.Location = new System.Drawing.Point(426, 231);
            this.ucLine39.Name = "ucLine39";
            this.ucLine39.Size = new System.Drawing.Size(49, 13);
            this.ucLine39.TabIndex = 11;
            // 
            // ucLine37
            // 
            this.ucLine37.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine37.Appearance.Options.UseBackColor = true;
            this.ucLine37.Color = UICommon.LineColor.LightBlue;
            this.ucLine37.Location = new System.Drawing.Point(421, 125);
            this.ucLine37.Name = "ucLine37";
            this.ucLine37.Size = new System.Drawing.Size(55, 10);
            this.ucLine37.TabIndex = 11;
            // 
            // ucLine16
            // 
            this.ucLine16.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine16.Appearance.Options.UseBackColor = true;
            this.ucLine16.Color = UICommon.LineColor.LightBlue;
            this.ucLine16.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine16.Location = new System.Drawing.Point(677, 130);
            this.ucLine16.Name = "ucLine16";
            this.ucLine16.Size = new System.Drawing.Size(10, 108);
            this.ucLine16.TabIndex = 11;
            // 
            // ucBurner3
            // 
            this.ucBurner3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBurner3.Appearance.Options.UseBackColor = true;
            this.ucBurner3.BurnerStatus = true;
            this.ucBurner3.Caption = null;
            this.ucBurner3.Location = new System.Drawing.Point(375, 273);
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
            this.ucBurner2.Location = new System.Drawing.Point(375, 166);
            this.ucBurner2.Name = "ucBurner2";
            this.ucBurner2.Size = new System.Drawing.Size(60, 40);
            this.ucBurner2.TabIndex = 1;
            // 
            // ucBox4
            // 
            this.ucBox4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBox4.Appearance.Options.UseBackColor = true;
            this.ucBox4.Caption = "ПТ";
            this.ucBox4.Location = new System.Drawing.Point(558, 470);
            this.ucBox4.Name = "ucBox4";
            this.ucBox4.Size = new System.Drawing.Size(117, 96);
            this.ucBox4.TabIndex = 9;
            // 
            // ucBox3
            // 
            this.ucBox3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBox3.Appearance.Options.UseBackColor = true;
            this.ucBox3.Caption = "НЕ";
            this.ucBox3.Location = new System.Drawing.Point(379, 470);
            this.ucBox3.Name = "ucBox3";
            this.ucBox3.Size = new System.Drawing.Size(117, 96);
            this.ucBox3.TabIndex = 9;
            // 
            // ucBox2
            // 
            this.ucBox2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBox2.Appearance.Options.UseBackColor = true;
            this.ucBox2.Caption = "РЕ";
            this.ucBox2.Location = new System.Drawing.Point(200, 470);
            this.ucBox2.Name = "ucBox2";
            this.ucBox2.Size = new System.Drawing.Size(117, 96);
            this.ucBox2.TabIndex = 9;
            // 
            // ucValve13
            // 
            this.ucValve13.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve13.Appearance.Options.UseBackColor = true;
            this.ucValve13.Location = new System.Drawing.Point(73, 374);
            this.ucValve13.Name = "ucValve13";
            this.ucValve13.Size = new System.Drawing.Size(40, 20);
            this.ucValve13.TabIndex = 8;
            // 
            // ucValve12
            // 
            this.ucValve12.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve12.Appearance.Options.UseBackColor = true;
            this.ucValve12.Location = new System.Drawing.Point(73, 421);
            this.ucValve12.Name = "ucValve12";
            this.ucValve12.Size = new System.Drawing.Size(40, 20);
            this.ucValve12.TabIndex = 8;
            // 
            // ucValve8
            // 
            this.ucValve8.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve8.Appearance.Options.UseBackColor = true;
            this.ucValve8.Location = new System.Drawing.Point(146, 533);
            this.ucValve8.Name = "ucValve8";
            this.ucValve8.Size = new System.Drawing.Size(40, 20);
            this.ucValve8.TabIndex = 8;
            // 
            // ucValve9
            // 
            this.ucValve9.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve9.Appearance.Options.UseBackColor = true;
            this.ucValve9.Location = new System.Drawing.Point(590, 378);
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
            this.ucValve6.Location = new System.Drawing.Point(475, 293);
            this.ucValve6.Name = "ucValve6";
            this.ucValve6.Size = new System.Drawing.Size(40, 20);
            this.ucValve6.TabIndex = 8;
            // 
            // ucValve5
            // 
            this.ucValve5.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve5.Appearance.Options.UseBackColor = true;
            this.ucValve5.Location = new System.Drawing.Point(475, 267);
            this.ucValve5.Name = "ucValve5";
            this.ucValve5.Size = new System.Drawing.Size(40, 20);
            this.ucValve5.TabIndex = 8;
            // 
            // ucValve4
            // 
            this.ucValve4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve4.Appearance.Options.UseBackColor = true;
            this.ucValve4.Location = new System.Drawing.Point(475, 229);
            this.ucValve4.Name = "ucValve4";
            this.ucValve4.Size = new System.Drawing.Size(40, 20);
            this.ucValve4.TabIndex = 8;
            // 
            // ucValve3
            // 
            this.ucValve3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve3.Appearance.Options.UseBackColor = true;
            this.ucValve3.Location = new System.Drawing.Point(475, 120);
            this.ucValve3.Name = "ucValve3";
            this.ucValve3.Size = new System.Drawing.Size(40, 20);
            this.ucValve3.TabIndex = 7;
            // 
            // ucValve2
            // 
            this.ucValve2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve2.Appearance.Options.UseBackColor = true;
            this.ucValve2.Location = new System.Drawing.Point(475, 187);
            this.ucValve2.Name = "ucValve2";
            this.ucValve2.Size = new System.Drawing.Size(40, 20);
            this.ucValve2.TabIndex = 6;
            // 
            // ucValve1
            // 
            this.ucValve1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucValve1.Appearance.Options.UseBackColor = true;
            this.ucValve1.Location = new System.Drawing.Point(475, 161);
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
            this.ucLine3.Location = new System.Drawing.Point(52, 424);
            this.ucLine3.Name = "ucLine3";
            this.ucLine3.Size = new System.Drawing.Size(803, 13);
            this.ucLine3.TabIndex = 10;
            // 
            // ucLine9
            // 
            this.ucLine9.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine9.Appearance.Options.UseBackColor = true;
            this.ucLine9.Color = UICommon.LineColor.Red;
            this.ucLine9.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine9.Location = new System.Drawing.Point(819, 192);
            this.ucLine9.Name = "ucLine9";
            this.ucLine9.Size = new System.Drawing.Size(10, 335);
            this.ucLine9.TabIndex = 10;
            // 
            // ucLine23
            // 
            this.ucLine23.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine23.Appearance.Options.UseBackColor = true;
            this.ucLine23.Color = UICommon.LineColor.Green;
            this.ucLine23.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine23.Location = new System.Drawing.Point(48, 429);
            this.ucLine23.Name = "ucLine23";
            this.ucLine23.Size = new System.Drawing.Size(13, 45);
            this.ucLine23.TabIndex = 10;
            // 
            // ucLine32
            // 
            this.ucLine32.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine32.Appearance.Options.UseBackColor = true;
            this.ucLine32.Color = UICommon.LineColor.Green;
            this.ucLine32.Location = new System.Drawing.Point(518, 582);
            this.ucLine32.Name = "ucLine32";
            this.ucLine32.Size = new System.Drawing.Size(49, 13);
            this.ucLine32.TabIndex = 10;
            // 
            // ucLine26
            // 
            this.ucLine26.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine26.Appearance.Options.UseBackColor = true;
            this.ucLine26.Color = UICommon.LineColor.Green;
            this.ucLine26.Location = new System.Drawing.Point(351, 582);
            this.ucLine26.Name = "ucLine26";
            this.ucLine26.Size = new System.Drawing.Size(49, 13);
            this.ucLine26.TabIndex = 10;
            // 
            // ucLine31
            // 
            this.ucLine31.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine31.Appearance.Options.UseBackColor = true;
            this.ucLine31.Color = UICommon.LineColor.Green;
            this.ucLine31.Location = new System.Drawing.Point(457, 443);
            this.ucLine31.Name = "ucLine31";
            this.ucLine31.Size = new System.Drawing.Size(65, 13);
            this.ucLine31.TabIndex = 10;
            // 
            // ucLine25
            // 
            this.ucLine25.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine25.Appearance.Options.UseBackColor = true;
            this.ucLine25.Color = UICommon.LineColor.Green;
            this.ucLine25.Location = new System.Drawing.Point(290, 443);
            this.ucLine25.Name = "ucLine25";
            this.ucLine25.Size = new System.Drawing.Size(65, 13);
            this.ucLine25.TabIndex = 10;
            // 
            // ucLine30
            // 
            this.ucLine30.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine30.Appearance.Options.UseBackColor = true;
            this.ucLine30.Color = UICommon.LineColor.Green;
            this.ucLine30.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine30.Location = new System.Drawing.Point(560, 563);
            this.ucLine30.Name = "ucLine30";
            this.ucLine30.Size = new System.Drawing.Size(10, 28);
            this.ucLine30.TabIndex = 10;
            // 
            // ucLine27
            // 
            this.ucLine27.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine27.Appearance.Options.UseBackColor = true;
            this.ucLine27.Color = UICommon.LineColor.Green;
            this.ucLine27.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine27.Location = new System.Drawing.Point(393, 562);
            this.ucLine27.Name = "ucLine27";
            this.ucLine27.Size = new System.Drawing.Size(10, 29);
            this.ucLine27.TabIndex = 10;
            // 
            // ucLine29
            // 
            this.ucLine29.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine29.Appearance.Options.UseBackColor = true;
            this.ucLine29.Color = UICommon.LineColor.Green;
            this.ucLine29.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine29.Location = new System.Drawing.Point(513, 450);
            this.ucLine29.Name = "ucLine29";
            this.ucLine29.Size = new System.Drawing.Size(13, 141);
            this.ucLine29.TabIndex = 10;
            // 
            // ucLine24
            // 
            this.ucLine24.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine24.Appearance.Options.UseBackColor = true;
            this.ucLine24.Color = UICommon.LineColor.Green;
            this.ucLine24.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine24.Location = new System.Drawing.Point(346, 450);
            this.ucLine24.Name = "ucLine24";
            this.ucLine24.Size = new System.Drawing.Size(13, 141);
            this.ucLine24.TabIndex = 10;
            // 
            // ucLine22
            // 
            this.ucLine22.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine22.Appearance.Options.UseBackColor = true;
            this.ucLine22.Color = UICommon.LineColor.Green;
            this.ucLine22.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine22.Location = new System.Drawing.Point(137, 382);
            this.ucLine22.Name = "ucLine22";
            this.ucLine22.Size = new System.Drawing.Size(13, 50);
            this.ucLine22.TabIndex = 10;
            // 
            // ucLine20
            // 
            this.ucLine20.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine20.Appearance.Options.UseBackColor = true;
            this.ucLine20.Color = UICommon.LineColor.Green;
            this.ucLine20.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine20.Location = new System.Drawing.Point(31, 383);
            this.ucLine20.Name = "ucLine20";
            this.ucLine20.Size = new System.Drawing.Size(13, 148);
            this.ucLine20.TabIndex = 10;
            // 
            // ucLine6
            // 
            this.ucLine6.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine6.Appearance.Options.UseBackColor = true;
            this.ucLine6.Color = UICommon.LineColor.Red;
            this.ucLine6.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine6.Location = new System.Drawing.Point(776, 169);
            this.ucLine6.Name = "ucLine6";
            this.ucLine6.Size = new System.Drawing.Size(10, 320);
            this.ucLine6.TabIndex = 10;
            // 
            // ucLine43
            // 
            this.ucLine43.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine43.Appearance.Options.UseBackColor = true;
            this.ucLine43.Color = UICommon.LineColor.Red;
            this.ucLine43.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine43.Location = new System.Drawing.Point(436, 298);
            this.ucLine43.Name = "ucLine43";
            this.ucLine43.Size = new System.Drawing.Size(38, 10);
            this.ucLine43.TabIndex = 10;
            // 
            // ucLine42
            // 
            this.ucLine42.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine42.Appearance.Options.UseBackColor = true;
            this.ucLine42.Color = UICommon.LineColor.Red;
            this.ucLine42.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine42.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine42.Location = new System.Drawing.Point(435, 272);
            this.ucLine42.Name = "ucLine42";
            this.ucLine42.Size = new System.Drawing.Size(39, 10);
            this.ucLine42.TabIndex = 10;
            // 
            // ucLine40
            // 
            this.ucLine40.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine40.Appearance.Options.UseBackColor = true;
            this.ucLine40.Color = UICommon.LineColor.Red;
            this.ucLine40.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine40.Location = new System.Drawing.Point(437, 192);
            this.ucLine40.Name = "ucLine40";
            this.ucLine40.Size = new System.Drawing.Size(38, 10);
            this.ucLine40.TabIndex = 10;
            // 
            // ucLine41
            // 
            this.ucLine41.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine41.Appearance.Options.UseBackColor = true;
            this.ucLine41.Color = UICommon.LineColor.Red;
            this.ucLine41.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine41.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine41.Location = new System.Drawing.Point(436, 166);
            this.ucLine41.Name = "ucLine41";
            this.ucLine41.Size = new System.Drawing.Size(39, 10);
            this.ucLine41.TabIndex = 10;
            // 
            // ucLine13
            // 
            this.ucLine13.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine13.Appearance.Options.UseBackColor = true;
            this.ucLine13.Color = UICommon.LineColor.Red;
            this.ucLine13.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine13.Location = new System.Drawing.Point(513, 297);
            this.ucLine13.Name = "ucLine13";
            this.ucLine13.Size = new System.Drawing.Size(314, 10);
            this.ucLine13.TabIndex = 10;
            // 
            // ucLine12
            // 
            this.ucLine12.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine12.Appearance.Options.UseBackColor = true;
            this.ucLine12.Color = UICommon.LineColor.Red;
            this.ucLine12.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine12.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine12.Location = new System.Drawing.Point(513, 272);
            this.ucLine12.Name = "ucLine12";
            this.ucLine12.Size = new System.Drawing.Size(270, 10);
            this.ucLine12.TabIndex = 10;
            // 
            // ucLine15
            // 
            this.ucLine15.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine15.Appearance.Options.UseBackColor = true;
            this.ucLine15.Color = UICommon.LineColor.LightBlue;
            this.ucLine15.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine15.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine15.Location = new System.Drawing.Point(513, 231);
            this.ucLine15.Name = "ucLine15";
            this.ucLine15.Size = new System.Drawing.Size(173, 10);
            this.ucLine15.TabIndex = 10;
            // 
            // ucLine14
            // 
            this.ucLine14.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine14.Appearance.Options.UseBackColor = true;
            this.ucLine14.Color = UICommon.LineColor.LightBlue;
            this.ucLine14.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine14.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine14.Location = new System.Drawing.Point(513, 125);
            this.ucLine14.Name = "ucLine14";
            this.ucLine14.Size = new System.Drawing.Size(342, 10);
            this.ucLine14.TabIndex = 10;
            // 
            // ucLine34
            // 
            this.ucLine34.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine34.Appearance.Options.UseBackColor = true;
            this.ucLine34.Color = UICommon.LineColor.Orange;
            this.ucLine34.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine34.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine34.Location = new System.Drawing.Point(89, 117);
            this.ucLine34.Name = "ucLine34";
            this.ucLine34.Size = new System.Drawing.Size(97, 21);
            this.ucLine34.TabIndex = 10;
            // 
            // ucLine36
            // 
            this.ucLine36.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine36.Appearance.Options.UseBackColor = true;
            this.ucLine36.Color = UICommon.LineColor.LightBlue;
            this.ucLine36.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine36.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine36.Location = new System.Drawing.Point(418, 130);
            this.ucLine36.Name = "ucLine36";
            this.ucLine36.Size = new System.Drawing.Size(13, 38);
            this.ucLine36.TabIndex = 10;
            // 
            // ucLine4
            // 
            this.ucLine4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine4.Appearance.Options.UseBackColor = true;
            this.ucLine4.Color = UICommon.LineColor.Green;
            this.ucLine4.Location = new System.Drawing.Point(391, 317);
            this.ucLine4.Name = "ucLine4";
            this.ucLine4.Size = new System.Drawing.Size(211, 10);
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
            // ucLine21
            // 
            this.ucLine21.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine21.Appearance.Options.UseBackColor = true;
            this.ucLine21.Color = UICommon.LineColor.Green;
            this.ucLine21.Location = new System.Drawing.Point(35, 378);
            this.ucLine21.Name = "ucLine21";
            this.ucLine21.Size = new System.Drawing.Size(111, 13);
            this.ucLine21.TabIndex = 14;
            // 
            // ucCaption3
            // 
            this.ucCaption3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaption3.Appearance.Options.UseBackColor = true;
            this.ucCaption3.Caption = "невтешлам";
            this.ucCaption3.Location = new System.Drawing.Point(53, 395);
            this.ucCaption3.Name = "ucCaption3";
            this.ucCaption3.Size = new System.Drawing.Size(83, 24);
            this.ucCaption3.TabIndex = 15;
            // 
            // ucCaption4
            // 
            this.ucCaption4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaption4.Appearance.Options.UseBackColor = true;
            this.ucCaption4.Caption = "к ТО";
            this.ucCaption4.Location = new System.Drawing.Point(137, 97);
            this.ucCaption4.Name = "ucCaption4";
            this.ucCaption4.Size = new System.Drawing.Size(61, 41);
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
            this.ucLine35.Size = new System.Drawing.Size(462, 21);
            this.ucLine35.TabIndex = 10;
            // 
            // ucLine8
            // 
            this.ucLine8.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine8.Appearance.Options.UseBackColor = true;
            this.ucLine8.Color = UICommon.LineColor.Red;
            this.ucLine8.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine8.Location = new System.Drawing.Point(668, 520);
            this.ucLine8.Name = "ucLine8";
            this.ucLine8.Size = new System.Drawing.Size(187, 13);
            this.ucLine8.TabIndex = 10;
            // 
            // ucLine7
            // 
            this.ucLine7.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine7.Appearance.Options.UseBackColor = true;
            this.ucLine7.Color = UICommon.LineColor.Red;
            this.ucLine7.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine7.Location = new System.Drawing.Point(668, 480);
            this.ucLine7.Name = "ucLine7";
            this.ucLine7.Size = new System.Drawing.Size(187, 13);
            this.ucLine7.TabIndex = 10;
            // 
            // ucLine11
            // 
            this.ucLine11.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine11.Appearance.Options.UseBackColor = true;
            this.ucLine11.Color = UICommon.LineColor.Red;
            this.ucLine11.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine11.Location = new System.Drawing.Point(513, 190);
            this.ucLine11.Name = "ucLine11";
            this.ucLine11.Size = new System.Drawing.Size(314, 10);
            this.ucLine11.TabIndex = 10;
            // 
            // ucLine10
            // 
            this.ucLine10.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine10.Appearance.Options.UseBackColor = true;
            this.ucLine10.Color = UICommon.LineColor.Red;
            this.ucLine10.Direction = System.Windows.Forms.AnchorStyles.Left;
            this.ucLine10.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine10.Location = new System.Drawing.Point(513, 165);
            this.ucLine10.Name = "ucLine10";
            this.ucLine10.Size = new System.Drawing.Size(270, 10);
            this.ucLine10.TabIndex = 10;
            // 
            // ucBox1
            // 
            this.ucBox1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucBox1.Appearance.Options.UseBackColor = true;
            this.ucBox1.Caption = "Камера дожигания";
            this.ucBox1.Location = new System.Drawing.Point(192, 106);
            this.ucBox1.Name = "ucBox1";
            this.ucBox1.Size = new System.Drawing.Size(208, 251);
            this.ucBox1.TabIndex = 9;
            // 
            // ucLine5
            // 
            this.ucLine5.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine5.Appearance.Options.UseBackColor = true;
            this.ucLine5.Color = UICommon.LineColor.Green;
            this.ucLine5.Location = new System.Drawing.Point(391, 108);
            this.ucLine5.Name = "ucLine5";
            this.ucLine5.Size = new System.Drawing.Size(211, 10);
            this.ucLine5.TabIndex = 10;
            // 
            // ucLine17
            // 
            this.ucLine17.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine17.Appearance.Options.UseBackColor = true;
            this.ucLine17.Color = UICommon.LineColor.Green;
            this.ucLine17.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine17.Location = new System.Drawing.Point(596, 110);
            this.ucLine17.Name = "ucLine17";
            this.ucLine17.Size = new System.Drawing.Size(10, 322);
            this.ucLine17.TabIndex = 10;
            // 
            // ucLine19
            // 
            this.ucLine19.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine19.Appearance.Options.UseBackColor = true;
            this.ucLine19.Color = UICommon.LineColor.Green;
            this.ucLine19.Location = new System.Drawing.Point(52, 467);
            this.ucLine19.Name = "ucLine19";
            this.ucLine19.Size = new System.Drawing.Size(25, 10);
            this.ucLine19.TabIndex = 14;
            // 
            // ucLine2
            // 
            this.ucLine2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine2.Appearance.Options.UseBackColor = true;
            this.ucLine2.Color = UICommon.LineColor.Green;
            this.ucLine2.Location = new System.Drawing.Point(112, 537);
            this.ucLine2.Name = "ucLine2";
            this.ucLine2.Size = new System.Drawing.Size(94, 13);
            this.ucLine2.TabIndex = 10;
            // 
            // ucLine1
            // 
            this.ucLine1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine1.Appearance.Options.UseBackColor = true;
            this.ucLine1.Color = UICommon.LineColor.Green;
            this.ucLine1.Location = new System.Drawing.Point(112, 484);
            this.ucLine1.Name = "ucLine1";
            this.ucLine1.Size = new System.Drawing.Size(94, 13);
            this.ucLine1.TabIndex = 10;
            // 
            // ucLine38
            // 
            this.ucLine38.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucLine38.Appearance.Options.UseBackColor = true;
            this.ucLine38.Color = UICommon.LineColor.LightBlue;
            this.ucLine38.Direction = System.Windows.Forms.AnchorStyles.Bottom;
            this.ucLine38.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.ucLine38.Location = new System.Drawing.Point(421, 234);
            this.ucLine38.Name = "ucLine38";
            this.ucLine38.Size = new System.Drawing.Size(13, 38);
            this.ucLine38.TabIndex = 10;
            // 
            // ucReheatChamber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucPump1);
            this.Controls.Add(this.ucValve10);
            this.Controls.Add(this.ucValve11);
            this.Controls.Add(this.ucGauge2);
            this.Controls.Add(this.ucGauge1);
            this.Controls.Add(this.ucTube1);
            this.Controls.Add(this.ucLine33);
            this.Controls.Add(this.ucLine28);
            this.Controls.Add(this.ucLine18);
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
            this.Controls.Add(this.ucLine9);
            this.Controls.Add(this.ucLine23);
            this.Controls.Add(this.ucLine32);
            this.Controls.Add(this.ucLine26);
            this.Controls.Add(this.ucLine31);
            this.Controls.Add(this.ucLine25);
            this.Controls.Add(this.ucLine30);
            this.Controls.Add(this.ucLine27);
            this.Controls.Add(this.ucLine29);
            this.Controls.Add(this.ucLine24);
            this.Controls.Add(this.ucLine22);
            this.Controls.Add(this.ucLine20);
            this.Controls.Add(this.ucLine6);
            this.Controls.Add(this.ucLine43);
            this.Controls.Add(this.ucLine42);
            this.Controls.Add(this.ucLine40);
            this.Controls.Add(this.ucLine41);
            this.Controls.Add(this.ucLine13);
            this.Controls.Add(this.ucLine12);
            this.Controls.Add(this.ucLine15);
            this.Controls.Add(this.ucLine14);
            this.Controls.Add(this.ucLine34);
            this.Controls.Add(this.ucLine38);
            this.Controls.Add(this.ucLine36);
            this.Controls.Add(this.ucLine4);
            this.Controls.Add(this.ucCaption1);
            this.Controls.Add(this.ucCaption2);
            this.Controls.Add(this.ucLine21);
            this.Controls.Add(this.ucCaption3);
            this.Controls.Add(this.ucCaption4);
            this.Controls.Add(this.ucLine35);
            this.Controls.Add(this.ucLine8);
            this.Controls.Add(this.ucLine7);
            this.Controls.Add(this.ucLine11);
            this.Controls.Add(this.ucLine10);
            this.Controls.Add(this.ucLine5);
            this.Controls.Add(this.ucLine17);
            this.Controls.Add(this.ucLine19);
            this.Controls.Add(this.ucLine2);
            this.Controls.Add(this.ucLine1);
            this.Controls.Add(this.ucBox1);
            this.Name = "ucReheatChamber";
            this.Size = new System.Drawing.Size(1024, 768);
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
        private UICommon.ucBox ucBox3;
        private UICommon.ucBox ucBox4;
        private UICommon.ucPump ucPump3;
        private UICommon.ucValve ucValve7;
        private UICommon.ucPump ucPump4;
        private UICommon.ucValve ucValve8;
        private UICommon.ucLine ucLine1;
        private UICommon.ucLine ucLine2;
        private UICommon.ucLine ucLine3;
        private UICommon.ucValve ucValve9;
        private UICommon.ucLine ucLine4;
        private UICommon.ucLine ucLine5;
        private UICommon.ucLine ucLine6;
        private UICommon.ucValve ucValve10;
        private UICommon.ucValve ucValve11;
        private UICommon.ucLine ucLine7;
        private UICommon.ucLine ucLine8;
        private UICommon.ucLine ucLine9;
        private UICommon.ucLine ucLine10;
        private UICommon.ucLine ucLine11;
        private UICommon.ucLine ucLine13;
        private UICommon.ucLine ucLine12;
        private UICommon.ucLine ucLine14;
        private UICommon.ucLine ucLine15;
        private UICommon.ucLine ucLine16;
        private UICommon.ucCaptioned ucCaption1;
        private UICommon.ucCaptioned ucCaption2;
        private UICommon.ucLine ucLine17;
        private UICommon.ucValve ucValve12;
        private UICommon.ucValve ucValve13;
        private UICommon.ucLine ucLine18;
        private UICommon.ucLine ucLine19;
        private UICommon.ucLine ucLine20;
        private UICommon.ucLine ucLine21;
        private UICommon.ucLine ucLine22;
        private UICommon.ucLine ucLine23;
        private UICommon.ucCaptioned ucCaption3;
        private UICommon.ucLine ucLine24;
        private UICommon.ucLine ucLine25;
        private UICommon.ucLine ucLine26;
        private UICommon.ucLine ucLine27;
        private UICommon.ucLine ucLine28;
        private UICommon.ucLine ucLine29;
        private UICommon.ucLine ucLine30;
        private UICommon.ucLine ucLine31;
        private UICommon.ucLine ucLine32;
        private UICommon.ucLine ucLine33;
        private UICommon.ucLine ucLine34;
        private UICommon.ucCaptioned ucCaption4;
        private UICommon.ucLine ucLine35;
        private UICommon.ucLine ucLine36;
        private UICommon.ucLine ucLine37;
        private UICommon.ucLine ucLine39;
        private UICommon.ucLine ucLine41;
        private UICommon.ucLine ucLine40;
        private UICommon.ucLine ucLine42;
        private UICommon.ucLine ucLine43;
        private ucTube ucTube1;
        private ucIndicator ucGauge1;
        private ucIndicator ucGauge2;
        private ucLine ucLine38;

    }
}
