namespace TP.CommonView
{
    partial class ucCommonView
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
            this.ucCaptioned1 = new UICommon.ucCaptioned();
            this.ucCaptioned2 = new UICommon.ucCaptioned();
            this.ucCaptioned3 = new UICommon.ucCaptioned();
            this.ucCaptioned4 = new UICommon.ucCaptioned();
            this.ucCaptioned5 = new UICommon.ucCaptioned();
            this.SuspendLayout();
            // 
            // ucCaptioned1
            // 
            this.ucCaptioned1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaptioned1.Appearance.Options.UseBackColor = true;
            this.ucCaptioned1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucCaptioned1.Caption = "Финишная очистка";
            this.ucCaptioned1.Location = new System.Drawing.Point(3, 0);
            this.ucCaptioned1.Name = "ucCaptioned1";
            this.ucCaptioned1.Size = new System.Drawing.Size(347, 255);
            this.ucCaptioned1.TabIndex = 0;
            // 
            // ucCaptioned2
            // 
            this.ucCaptioned2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaptioned2.Appearance.Options.UseBackColor = true;
            this.ucCaptioned2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucCaptioned2.Caption = "Циклон и скруббер";
            this.ucCaptioned2.Location = new System.Drawing.Point(356, 0);
            this.ucCaptioned2.Name = "ucCaptioned2";
            this.ucCaptioned2.Size = new System.Drawing.Size(383, 255);
            this.ucCaptioned2.TabIndex = 0;
            // 
            // ucCaptioned3
            // 
            this.ucCaptioned3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaptioned3.Appearance.Options.UseBackColor = true;
            this.ucCaptioned3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucCaptioned3.Caption = "Теплообменник";
            this.ucCaptioned3.Location = new System.Drawing.Point(745, 0);
            this.ucCaptioned3.Name = "ucCaptioned3";
            this.ucCaptioned3.Size = new System.Drawing.Size(507, 296);
            this.ucCaptioned3.TabIndex = 0;
            // 
            // ucCaptioned4
            // 
            this.ucCaptioned4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaptioned4.Appearance.Options.UseBackColor = true;
            this.ucCaptioned4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucCaptioned4.Caption = "Печь";
            this.ucCaptioned4.Location = new System.Drawing.Point(0, 261);
            this.ucCaptioned4.Name = "ucCaptioned4";
            this.ucCaptioned4.Size = new System.Drawing.Size(739, 285);
            this.ucCaptioned4.TabIndex = 0;
            // 
            // ucCaptioned5
            // 
            this.ucCaptioned5.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucCaptioned5.Appearance.Options.UseBackColor = true;
            this.ucCaptioned5.AutoScroll = true;
            this.ucCaptioned5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucCaptioned5.Caption = "Камера дожигания";
            this.ucCaptioned5.Location = new System.Drawing.Point(745, 302);
            this.ucCaptioned5.Name = "ucCaptioned5";
            this.ucCaptioned5.Size = new System.Drawing.Size(504, 241);
            this.ucCaptioned5.TabIndex = 0;
            // 
            // ucCommonView
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.Controls.Add(this.ucCaptioned5);
            this.Controls.Add(this.ucCaptioned4);
            this.Controls.Add(this.ucCaptioned3);
            this.Controls.Add(this.ucCaptioned2);
            this.Controls.Add(this.ucCaptioned1);
            this.Name = "ucCommonView";
            this.Size = new System.Drawing.Size(1252, 656);
            this.ResumeLayout(false);

        }

        #endregion

        private UICommon.ucCaptioned ucCaptioned1;
        private UICommon.ucCaptioned ucCaptioned2;
        private UICommon.ucCaptioned ucCaptioned3;
        private UICommon.ucCaptioned ucCaptioned4;
        private UICommon.ucCaptioned ucCaptioned5;
    }
}
