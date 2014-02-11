using Oleg_ivo.WAGO.Controls.LevelEditors;

namespace Oleg_ivo.WAGO.Forms
{
    partial class DeviceConfigurationForm
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
            this.tree = new System.Windows.Forms.TreeView();
            this.btnPorts = new System.Windows.Forms.Button();
            this.btnFieldNodes = new System.Windows.Forms.Button();
            this.btnModules = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.levelEditor1 = new Oleg_ivo.WAGO.Controls.LevelEditors.LevelEditControl();
            this.channelsControl1 = new Oleg_ivo.WAGO.Forms.ChannelsControl();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdateInfo = new System.Windows.Forms.Button();
            this.btnObtainInfo = new System.Windows.Forms.Button();
            this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.FullRowSelect = true;
            this.tree.HideSelection = false;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(569, 507);
            this.tree.TabIndex = 0;
            this.tree.DoubleClick += new System.EventHandler(this.tree_DoubleClick);
            this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
            // 
            // btnPorts
            // 
            this.btnPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPorts.Location = new System.Drawing.Point(12, 592);
            this.btnPorts.Name = "btnPorts";
            this.btnPorts.Size = new System.Drawing.Size(53, 23);
            this.btnPorts.TabIndex = 1;
            this.btnPorts.Text = "Порты";
            this.btnPorts.UseVisualStyleBackColor = true;
            this.btnPorts.Click += new System.EventHandler(this.btnPorts_Click);
            // 
            // btnFieldNodes
            // 
            this.btnFieldNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFieldNodes.Location = new System.Drawing.Point(71, 592);
            this.btnFieldNodes.Name = "btnFieldNodes";
            this.btnFieldNodes.Size = new System.Drawing.Size(123, 23);
            this.btnFieldNodes.TabIndex = 1;
            this.btnFieldNodes.Text = "Узлы полевой шины";
            this.btnFieldNodes.UseVisualStyleBackColor = true;
            this.btnFieldNodes.Click += new System.EventHandler(this.btnFieldNodes_Click);
            // 
            // btnModules
            // 
            this.btnModules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnModules.Location = new System.Drawing.Point(200, 592);
            this.btnModules.Name = "btnModules";
            this.btnModules.Size = new System.Drawing.Size(129, 23);
            this.btnModules.TabIndex = 1;
            this.btnModules.Text = "Модули ввода-вывода";
            this.btnModules.UseVisualStyleBackColor = true;
            this.btnModules.Click += new System.EventHandler(this.btnModules_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTest.Location = new System.Drawing.Point(335, 592);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(94, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Тестирование";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnSave);
            this.splitContainer1.Panel2.Controls.Add(this.btnUpdateInfo);
            this.splitContainer1.Panel2.Controls.Add(this.btnObtainInfo);
            this.splitContainer1.Size = new System.Drawing.Size(851, 574);
            this.splitContainer1.SplitterDistance = 507;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tree);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(851, 507);
            this.splitContainer2.SplitterDistance = 569;
            this.splitContainer2.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.levelEditor1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.channelsControl1);
            this.splitContainer3.Size = new System.Drawing.Size(278, 507);
            this.splitContainer3.SplitterDistance = 346;
            this.splitContainer3.TabIndex = 3;
            // 
            // levelEditor1
            // 
            this.levelEditor1.ActiveLevel = Oleg_ivo.WAGO.Forms.Level.FieldBuses;
            this.levelEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.levelEditor1.Location = new System.Drawing.Point(0, 0);
            this.levelEditor1.Name = "levelEditor1";
            this.levelEditor1.Size = new System.Drawing.Size(278, 346);
            this.levelEditor1.TabIndex = 2;
            // 
            // channelsControl1
            // 
            this.channelsControl1.ActiveLevel = Oleg_ivo.WAGO.Forms.Level.FieldBuses;
            this.channelsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelsControl1.Location = new System.Drawing.Point(0, 0);
            this.channelsControl1.Name = "channelsControl1";
            this.channelsControl1.Size = new System.Drawing.Size(278, 157);
            this.channelsControl1.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 37);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdateInfo
            // 
            this.btnUpdateInfo.Location = new System.Drawing.Point(272, 2);
            this.btnUpdateInfo.Name = "btnUpdateInfo";
            this.btnUpdateInfo.Size = new System.Drawing.Size(85, 38);
            this.btnUpdateInfo.TabIndex = 1;
            this.btnUpdateInfo.Text = "<<Обновить <<описание";
            this.btnUpdateInfo.UseVisualStyleBackColor = true;
            this.btnUpdateInfo.Click += new System.EventHandler(this.btnUpdateInfo_Click);
            // 
            // btnObtainInfo
            // 
            this.btnObtainInfo.Location = new System.Drawing.Point(178, 3);
            this.btnObtainInfo.Name = "btnObtainInfo";
            this.btnObtainInfo.Size = new System.Drawing.Size(88, 38);
            this.btnObtainInfo.TabIndex = 0;
            this.btnObtainInfo.Text = "Получить>> описание>>";
            this.btnObtainInfo.UseVisualStyleBackColor = true;
            this.btnObtainInfo.Click += new System.EventHandler(this.btnObtainInfo_Click);
            // 
            // oleDbConnection1
            // 
            this.oleDbConnection1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\\WORK\\Oleg_ivo\\Oleg_ivo.WAGO\\Oleg_" +
                "ivo.WAGO\\test.mdb;Persist Security Info=True";
            // 
            // DeviceConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 627);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnModules);
            this.Controls.Add(this.btnFieldNodes);
            this.Controls.Add(this.btnPorts);
            this.Name = "DeviceConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройка конфигурации системы полевых шин";
            this.Load += new System.EventHandler(this.SystemConfigurationForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SystemConfigurationForm_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.Button btnPorts;
        private System.Windows.Forms.Button btnFieldNodes;
        private System.Windows.Forms.Button btnModules;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Data.OleDb.OleDbConnection oleDbConnection1;
        private ChannelsControl channelsControl1;
        private System.Windows.Forms.Button btnObtainInfo;
        private System.Windows.Forms.Button btnUpdateInfo;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private LevelEditControl levelEditor1;
        private System.Windows.Forms.Button btnSave;
    }
}