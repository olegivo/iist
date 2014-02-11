namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    partial class IoModuleEditControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IoModuleEditControl));
            this.dataManager1 = new Oleg_ivo.Plc.DataManager(this.components);
            this.dtsChannelConfiguration1 = new Oleg_ivo.WAGO.DtsChannelConfiguration();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.adIoModules = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand5 = new System.Data.OleDb.OleDbCommand();
            this.oleDbConnection5 = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand5 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand5 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand5 = new System.Data.OleDb.OleDbCommand();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtsChannelConfiguration1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataManager1
            // 
            this.dataManager1.DataAdapter = this.adIoModules;
            this.dataManager1.DataSet = this.dtsChannelConfiguration1;
            // 
            // dtsChannelConfiguration1
            // 
            this.dtsChannelConfiguration1.DataSetName = "DtsChannelConfiguration";
            this.dtsChannelConfiguration1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                          | System.Windows.Forms.AnchorStyles.Left)
                                                                         | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dtsChannelConfiguration1, "IoModule.Id", true));
            this.textBox1.Location = new System.Drawing.Point(84, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(188, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Id";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                          | System.Windows.Forms.AnchorStyles.Left)
                                                                         | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dtsChannelConfiguration1, "IoModule.FieldNodeId", true));
            this.textBox2.Location = new System.Drawing.Point(84, 29);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(188, 20);
            this.textBox2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "FieldNodeId";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                          | System.Windows.Forms.AnchorStyles.Left)
                                                                         | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dtsChannelConfiguration1, "IoModule.ModuleAddress", true));
            this.textBox3.Location = new System.Drawing.Point(84, 55);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(188, 20);
            this.textBox3.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "ModuleAddress";
            // 
            // adIoModules
            // 
            this.adIoModules.DeleteCommand = this.oleDbDeleteCommand5;
            this.adIoModules.InsertCommand = this.oleDbInsertCommand5;
            this.adIoModules.SelectCommand = this.oleDbSelectCommand5;
            this.adIoModules.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                                                                                                  new System.Data.Common.DataTableMapping("Table", "IoModule", new System.Data.Common.DataColumnMapping[] {
                                                                                                                                                                                                              new System.Data.Common.DataColumnMapping("Id", "Id"),
                                                                                                                                                                                                              new System.Data.Common.DataColumnMapping("FieldNodeId", "FieldNodeId"),
                                                                                                                                                                                                              new System.Data.Common.DataColumnMapping("ModuleAddress", "ModuleAddress"),
                                                                                                                                                                                                              new System.Data.Common.DataColumnMapping("Description", "Description")})});
            this.adIoModules.UpdateCommand = this.oleDbUpdateCommand5;
            // 
            // oleDbDeleteCommand5
            // 
            this.oleDbDeleteCommand5.CommandText = resources.GetString("oleDbDeleteCommand5.CommandText");
            this.oleDbDeleteCommand5.Connection = this.oleDbConnection5;
            this.oleDbDeleteCommand5.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
                                                                                                    new System.Data.OleDb.OleDbParameter("Original_Id", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("IsNull_FieldNodeId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "FieldNodeId", System.Data.DataRowVersion.Original, true, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("Original_FieldNodeId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "FieldNodeId", System.Data.DataRowVersion.Original, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("IsNull_ModuleAddress", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ModuleAddress", System.Data.DataRowVersion.Original, true, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("Original_ModuleAddress", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ModuleAddress", System.Data.DataRowVersion.Original, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("IsNull_Description", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, true, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("Original_Description", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, null)});
            // 
            // oleDbConnection5
            // 
            this.oleDbConnection5.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\test.mdb;Persist Sec" +
                                                     "urity Info=True";
            // 
            // oleDbInsertCommand5
            // 
            this.oleDbInsertCommand5.CommandText = "INSERT INTO `IoModule` (`FieldNodeId`, `ModuleAddress`, `Description`) VALUES (?," +
                                                   " ?, ?)";
            this.oleDbInsertCommand5.Connection = this.oleDbConnection5;
            this.oleDbInsertCommand5.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
                                                                                                    new System.Data.OleDb.OleDbParameter("FieldNodeId", System.Data.OleDb.OleDbType.Integer, 0, "FieldNodeId"),
                                                                                                    new System.Data.OleDb.OleDbParameter("ModuleAddress", System.Data.OleDb.OleDbType.Integer, 0, "ModuleAddress"),
                                                                                                    new System.Data.OleDb.OleDbParameter("Description", System.Data.OleDb.OleDbType.VarWChar, 0, "Description")});
            // 
            // oleDbSelectCommand5
            // 
            this.oleDbSelectCommand5.CommandText = "SELECT     IoModule.*\r\nFROM         IoModule";
            this.oleDbSelectCommand5.Connection = this.oleDbConnection5;
            // 
            // oleDbUpdateCommand5
            // 
            this.oleDbUpdateCommand5.CommandText = resources.GetString("oleDbUpdateCommand5.CommandText");
            this.oleDbUpdateCommand5.Connection = this.oleDbConnection5;
            this.oleDbUpdateCommand5.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
                                                                                                    new System.Data.OleDb.OleDbParameter("FieldNodeId", System.Data.OleDb.OleDbType.Integer, 0, "FieldNodeId"),
                                                                                                    new System.Data.OleDb.OleDbParameter("ModuleAddress", System.Data.OleDb.OleDbType.Integer, 0, "ModuleAddress"),
                                                                                                    new System.Data.OleDb.OleDbParameter("Description", System.Data.OleDb.OleDbType.VarWChar, 0, "Description"),
                                                                                                    new System.Data.OleDb.OleDbParameter("Original_Id", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("IsNull_FieldNodeId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "FieldNodeId", System.Data.DataRowVersion.Original, true, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("Original_FieldNodeId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "FieldNodeId", System.Data.DataRowVersion.Original, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("IsNull_ModuleAddress", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ModuleAddress", System.Data.DataRowVersion.Original, true, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("Original_ModuleAddress", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ModuleAddress", System.Data.DataRowVersion.Original, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("IsNull_Description", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, true, null),
                                                                                                    new System.Data.OleDb.OleDbParameter("Original_Description", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, null)});
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                          | System.Windows.Forms.AnchorStyles.Left)
                                                                         | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dtsChannelConfiguration1, "IoModule.Description", true));
            this.textBox4.Location = new System.Drawing.Point(84, 81);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(188, 20);
            this.textBox4.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Description";
            // 
            // IoModuleEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "IoModuleEditControl";
            this.Size = new System.Drawing.Size(275, 124);
            ((System.ComponentModel.ISupportInitialize)(this.dtsChannelConfiguration1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Oleg_ivo.Plc.DataManager dataManager1;
        private DtsChannelConfiguration dtsChannelConfiguration1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Data.OleDb.OleDbDataAdapter adIoModules;
        private System.Data.OleDb.OleDbCommand oleDbDeleteCommand5;
        private System.Data.OleDb.OleDbConnection oleDbConnection5;
        private System.Data.OleDb.OleDbCommand oleDbInsertCommand5;
        private System.Data.OleDb.OleDbCommand oleDbSelectCommand5;
        private System.Data.OleDb.OleDbCommand oleDbUpdateCommand5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
    }
}