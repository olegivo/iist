namespace Oleg_ivo.LabViewTest
{
    partial class Form1
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
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            this.Form1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.beSentValue = new DevExpress.XtraEditors.ButtonEdit();
            this.sbRaiseEvent = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciSend = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.Form1ConvertedLayout)).BeginInit();
            this.Form1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beSentValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // Form1ConvertedLayout
            // 
            this.Form1ConvertedLayout.Controls.Add(this.beSentValue);
            this.Form1ConvertedLayout.Controls.Add(this.sbRaiseEvent);
            this.Form1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.Form1ConvertedLayout.Name = "Form1ConvertedLayout";
            this.Form1ConvertedLayout.Root = this.layoutControlGroup1;
            this.Form1ConvertedLayout.Size = new System.Drawing.Size(297, 86);
            this.Form1ConvertedLayout.TabIndex = 1;
            // 
            // beSentValue
            // 
            this.beSentValue.Location = new System.Drawing.Point(72, 38);
            this.beSentValue.Name = "beSentValue";
            this.beSentValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right)});
            this.beSentValue.Properties.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beSentValue_Properties_ButtonPressed);
            this.beSentValue.Size = new System.Drawing.Size(213, 20);
            this.beSentValue.StyleController = this.Form1ConvertedLayout;
            this.beSentValue.TabIndex = 5;
            this.beSentValue.EditValueChanged += new System.EventHandler(this.beSentValue_EditValueChanged);
            // 
            // sbRaiseEvent
            // 
            this.sbRaiseEvent.Location = new System.Drawing.Point(12, 12);
            this.sbRaiseEvent.Name = "sbRaiseEvent";
            this.sbRaiseEvent.Size = new System.Drawing.Size(273, 22);
            this.sbRaiseEvent.StyleController = this.Form1ConvertedLayout;
            this.sbRaiseEvent.TabIndex = 4;
            this.sbRaiseEvent.Text = "Сгенерировать событие";
            this.sbRaiseEvent.Click += new System.EventHandler(this.sbRaiseEvent_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.lciSend,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(297, 86);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbRaiseEvent;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(277, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // lciSend
            // 
            this.lciSend.Control = this.beSentValue;
            this.lciSend.CustomizationFormText = "Отправить";
            this.lciSend.Location = new System.Drawing.Point(0, 26);
            this.lciSend.Name = "lciSend";
            this.lciSend.Size = new System.Drawing.Size(277, 24);
            this.lciSend.Text = "Отправить";
            this.lciSend.TextSize = new System.Drawing.Size(56, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 50);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(277, 16);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 86);
            this.Controls.Add(this.Form1ConvertedLayout);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Form1ConvertedLayout)).EndInit();
            this.Form1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.beSentValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraLayout.LayoutControl Form1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton sbRaiseEvent;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.ButtonEdit beSentValue;
        private DevExpress.XtraLayout.LayoutControlItem lciSend;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}

