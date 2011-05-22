using UICommon;

namespace TP
{
    partial class XtraForm1
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
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.ovalShape1 = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.checkButton1 = new DevExpress.XtraEditors.CheckButton();
            this.ucTube1 = new ucTube();
            this.ovalShape2 = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape5 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.panel1 = new System.Windows.Forms.Panel();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.ovalShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(366, 410);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // ovalShape1
            // 
            this.ovalShape1.BackColor = System.Drawing.Color.Black;
            this.ovalShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.ovalShape1.Location = new System.Drawing.Point(140, 1);
            this.ovalShape1.Name = "ovalShape1";
            this.ovalShape1.Size = new System.Drawing.Size(217, 104);
            this.ovalShape1.Visible = false;
            // 
            // checkButton1
            // 
            this.checkButton1.Location = new System.Drawing.Point(9, 375);
            this.checkButton1.Name = "checkButton1";
            this.checkButton1.Size = new System.Drawing.Size(75, 23);
            this.checkButton1.TabIndex = 3;
            this.checkButton1.Text = "Курить";
            this.checkButton1.CheckedChanged += new System.EventHandler(this.checkButton1_CheckedChanged);
            // 
            // ucTube1
            // 
            this.ucTube1.Location = new System.Drawing.Point(90, 90);
            this.ucTube1.Name = "ucTube1";
            this.ucTube1.Size = new System.Drawing.Size(97, 308);
            this.ucTube1.TabIndex = 1;
            this.ucTube1.IsSmokesChanged += new System.EventHandler(this.ucTube1_IsSmokesChanged);
            // 
            // ovalShape2
            // 
            this.ovalShape2.Location = new System.Drawing.Point(5, 5);
            this.ovalShape2.Name = "ovalShape2";
            this.ovalShape2.Size = new System.Drawing.Size(36, 36);
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 22;
            this.lineShape1.X2 = 24;
            this.lineShape1.Y1 = 42;
            this.lineShape1.Y2 = 74;
            // 
            // lineShape2
            // 
            this.lineShape2.Cursor = System.Windows.Forms.Cursors.Default;
            this.lineShape2.Name = "lineShape1";
            this.lineShape2.X1 = 22;
            this.lineShape2.X2 = 12;
            this.lineShape2.Y1 = 73;
            this.lineShape2.Y2 = 102;
            // 
            // lineShape3
            // 
            this.lineShape3.Cursor = System.Windows.Forms.Cursors.Default;
            this.lineShape3.Name = "lineShape1";
            this.lineShape3.X1 = 23;
            this.lineShape3.X2 = 36;
            this.lineShape3.Y1 = 73;
            this.lineShape3.Y2 = 104;
            // 
            // lineShape4
            // 
            this.lineShape4.Cursor = System.Windows.Forms.Cursors.Default;
            this.lineShape4.Name = "lineShape1";
            this.lineShape4.X1 = 23;
            this.lineShape4.X2 = 36;
            this.lineShape4.Y1 = 45;
            this.lineShape4.Y2 = 76;
            // 
            // lineShape5
            // 
            this.lineShape5.Cursor = System.Windows.Forms.Cursors.Default;
            this.lineShape5.Name = "lineShape1";
            this.lineShape5.X1 = 20;
            this.lineShape5.X2 = 14;
            this.lineShape5.Y1 = 45;
            this.lineShape5.Y2 = 76;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.shapeContainer2);
            this.panel1.Location = new System.Drawing.Point(110, 274);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(51, 110);
            this.panel1.TabIndex = 4;
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape5,
            this.lineShape4,
            this.lineShape3,
            this.lineShape2,
            this.lineShape1,
            this.ovalShape2});
            this.shapeContainer2.Size = new System.Drawing.Size(51, 110);
            this.shapeContainer2.TabIndex = 0;
            this.shapeContainer2.TabStop = false;
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 410);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkButton1);
            this.Controls.Add(this.ucTube1);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "XtraForm1";
            this.Text = "XtraForm1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.OvalShape ovalShape1;
        private ucTube ucTube1;
        private DevExpress.XtraEditors.CheckButton checkButton1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.OvalShape ovalShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape5;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
    }
}