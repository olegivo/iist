namespace Oleg_ivo.WAGO.Forms
{
    partial class ChannelsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelsControl));
            this.tpLChannels = new System.Windows.Forms.TabPage();
            this.dgvLChannels = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.physicalChannelIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtsChannelConfiguration1 = new Oleg_ivo.WAGO.DtsChannelConfiguration();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFill = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tpFieldNodes = new System.Windows.Forms.TabPage();
            this.dgvFieldNodes = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldBusIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpFieldBuses = new System.Windows.Forms.TabPage();
            this.dgvFieldBuses = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldBusTypeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldBusNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpPChannels = new System.Windows.Forms.TabPage();
            this.dgvPChannels = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataManager1 = new Oleg_ivo.Plc.DataManager(this.components);
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sdaFieldBusNodes = new System.Data.SqlClient.SqlDataAdapter();
            this.sdaPChannels = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sdaLCChannels = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlCommand5 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand6 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand7 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand8 = new System.Data.SqlClient.SqlCommand();
            this.sdaFieldBuses = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlCommand9 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand10 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand11 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand12 = new System.Data.SqlClient.SqlCommand();
            this.sdaIoModules = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlCommand13 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand14 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand15 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand16 = new System.Data.SqlClient.SqlCommand();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.tpLChannels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLChannels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtsChannelConfiguration1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.tpFieldNodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFieldNodes)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tpFieldBuses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFieldBuses)).BeginInit();
            this.tpPChannels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPChannels)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpLChannels
            // 
            this.tpLChannels.Controls.Add(this.dgvLChannels);
            this.tpLChannels.Location = new System.Drawing.Point(4, 22);
            this.tpLChannels.Name = "tpLChannels";
            this.tpLChannels.Padding = new System.Windows.Forms.Padding(3);
            this.tpLChannels.Size = new System.Drawing.Size(536, 205);
            this.tpLChannels.TabIndex = 1;
            this.tpLChannels.Text = "Лог. каналы";
            this.tpLChannels.UseVisualStyleBackColor = true;
            // 
            // dgvLChannels
            // 
            this.dgvLChannels.AllowUserToOrderColumns = true;
            this.dgvLChannels.AutoGenerateColumns = false;
            this.dgvLChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLChannels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.physicalChannelIdDataGridViewTextBoxColumn});
            this.dgvLChannels.DataMember = "LogicalChannel";
            this.dgvLChannels.DataSource = this.dtsChannelConfiguration1;
            this.dgvLChannels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLChannels.Location = new System.Drawing.Point(3, 3);
            this.dgvLChannels.Name = "dgvLChannels";
            this.dgvLChannels.Size = new System.Drawing.Size(530, 199);
            this.dgvLChannels.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn2.HeaderText = "Id";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // physicalChannelIdDataGridViewTextBoxColumn
            // 
            this.physicalChannelIdDataGridViewTextBoxColumn.DataPropertyName = "PhysicalChannelId";
            this.physicalChannelIdDataGridViewTextBoxColumn.HeaderText = "PhysicalChannelId";
            this.physicalChannelIdDataGridViewTextBoxColumn.Name = "physicalChannelIdDataGridViewTextBoxColumn";
            // 
            // dtsChannelConfiguration1
            // 
            this.dtsChannelConfiguration1.DataSetName = "DtsChannelConfiguration";
            this.dtsChannelConfiguration1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnFill);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(544, 28);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point(3, 3);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(50, 23);
            this.btnFill.TabIndex = 6;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(59, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(45, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tpFieldNodes
            // 
            this.tpFieldNodes.Controls.Add(this.dgvFieldNodes);
            this.tpFieldNodes.Location = new System.Drawing.Point(4, 22);
            this.tpFieldNodes.Name = "tpFieldNodes";
            this.tpFieldNodes.Padding = new System.Windows.Forms.Padding(3);
            this.tpFieldNodes.Size = new System.Drawing.Size(536, 205);
            this.tpFieldNodes.TabIndex = 2;
            this.tpFieldNodes.Text = "Узлы";
            this.tpFieldNodes.UseVisualStyleBackColor = true;
            // 
            // dgvFieldNodes
            // 
            this.dgvFieldNodes.AllowUserToOrderColumns = true;
            this.dgvFieldNodes.AutoGenerateColumns = false;
            this.dgvFieldNodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFieldNodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.fieldBusIdDataGridViewTextBoxColumn});
            this.dgvFieldNodes.DataMember = "FieldBusNode";
            this.dgvFieldNodes.DataSource = this.dtsChannelConfiguration1;
            this.dgvFieldNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFieldNodes.Location = new System.Drawing.Point(3, 3);
            this.dgvFieldNodes.Name = "dgvFieldNodes";
            this.dgvFieldNodes.Size = new System.Drawing.Size(530, 199);
            this.dgvFieldNodes.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // fieldBusIdDataGridViewTextBoxColumn
            // 
            this.fieldBusIdDataGridViewTextBoxColumn.DataPropertyName = "FieldBusId";
            this.fieldBusIdDataGridViewTextBoxColumn.HeaderText = "FieldBusId";
            this.fieldBusIdDataGridViewTextBoxColumn.Name = "fieldBusIdDataGridViewTextBoxColumn";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpFieldBuses);
            this.tabControl1.Controls.Add(this.tpFieldNodes);
            this.tabControl1.Controls.Add(this.tpPChannels);
            this.tabControl1.Controls.Add(this.tpLChannels);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(544, 231);
            this.tabControl1.TabIndex = 6;
            // 
            // tpFieldBuses
            // 
            this.tpFieldBuses.Controls.Add(this.dgvFieldBuses);
            this.tpFieldBuses.Location = new System.Drawing.Point(4, 22);
            this.tpFieldBuses.Name = "tpFieldBuses";
            this.tpFieldBuses.Padding = new System.Windows.Forms.Padding(3);
            this.tpFieldBuses.Size = new System.Drawing.Size(536, 205);
            this.tpFieldBuses.TabIndex = 3;
            this.tpFieldBuses.Text = "Полевые шины";
            this.tpFieldBuses.UseVisualStyleBackColor = true;
            // 
            // dgvFieldBuses
            // 
            this.dgvFieldBuses.AllowUserToOrderColumns = true;
            this.dgvFieldBuses.AutoGenerateColumns = false;
            this.dgvFieldBuses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFieldBuses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.fieldBusTypeIdDataGridViewTextBoxColumn,
            this.fieldBusNameDataGridViewTextBoxColumn});
            this.dgvFieldBuses.DataMember = "FieldBus";
            this.dgvFieldBuses.DataSource = this.dtsChannelConfiguration1;
            this.dgvFieldBuses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFieldBuses.Location = new System.Drawing.Point(3, 3);
            this.dgvFieldBuses.Name = "dgvFieldBuses";
            this.dgvFieldBuses.Size = new System.Drawing.Size(530, 199);
            this.dgvFieldBuses.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn7.HeaderText = "Id";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // fieldBusTypeIdDataGridViewTextBoxColumn
            // 
            this.fieldBusTypeIdDataGridViewTextBoxColumn.DataPropertyName = "FieldBusTypeId";
            this.fieldBusTypeIdDataGridViewTextBoxColumn.HeaderText = "FieldBusTypeId";
            this.fieldBusTypeIdDataGridViewTextBoxColumn.Name = "fieldBusTypeIdDataGridViewTextBoxColumn";
            // 
            // fieldBusNameDataGridViewTextBoxColumn
            // 
            this.fieldBusNameDataGridViewTextBoxColumn.DataPropertyName = "FieldBusName";
            this.fieldBusNameDataGridViewTextBoxColumn.HeaderText = "FieldBusName";
            this.fieldBusNameDataGridViewTextBoxColumn.Name = "fieldBusNameDataGridViewTextBoxColumn";
            // 
            // tpPChannels
            // 
            this.tpPChannels.Controls.Add(this.dgvPChannels);
            this.tpPChannels.Location = new System.Drawing.Point(4, 22);
            this.tpPChannels.Name = "tpPChannels";
            this.tpPChannels.Padding = new System.Windows.Forms.Padding(3);
            this.tpPChannels.Size = new System.Drawing.Size(536, 205);
            this.tpPChannels.TabIndex = 0;
            this.tpPChannels.Text = "Физ. каналы";
            this.tpPChannels.UseVisualStyleBackColor = true;
            // 
            // dgvPChannels
            // 
            this.dgvPChannels.AllowUserToOrderColumns = true;
            this.dgvPChannels.AutoGenerateColumns = false;
            this.dgvPChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPChannels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dgvPChannels.DataMember = "PhysicalChannel";
            this.dgvPChannels.DataSource = this.dtsChannelConfiguration1;
            this.dgvPChannels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPChannels.Location = new System.Drawing.Point(3, 3);
            this.dgvPChannels.Name = "dgvPChannels";
            this.dgvPChannels.Size = new System.Drawing.Size(530, 199);
            this.dgvPChannels.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn3.HeaderText = "Id";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "FieldNodeId";
            this.dataGridViewTextBoxColumn5.HeaderText = "FieldNodeId";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn6.HeaderText = "Description";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(544, 263);
            this.splitContainer1.SplitterDistance = 231;
            this.splitContainer1.TabIndex = 9;
            // 
            // dataManager1
            // 
            this.dataManager1.DataAdapter = null;
            this.dataManager1.DataSet = this.dtsChannelConfiguration1;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT     *\r\nFROM         FieldBusNode";
            this.sqlSelectCommand1.Connection = this.sqlConnection1;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.sqlConnection1;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@FieldBusTypeId", System.Data.SqlDbType.Int, 0, "FieldBusTypeId"),
            new System.Data.SqlClient.SqlParameter("@AddressPart1", System.Data.SqlDbType.NVarChar, 0, "AddressPart1"),
            new System.Data.SqlClient.SqlParameter("@AddressPart2", System.Data.SqlDbType.Int, 0, "AddressPart2")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.sqlConnection1;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@FieldBusTypeId", System.Data.SqlDbType.Int, 0, "FieldBusTypeId"),
            new System.Data.SqlClient.SqlParameter("@AddressPart1", System.Data.SqlDbType.NVarChar, 0, "AddressPart1"),
            new System.Data.SqlClient.SqlParameter("@AddressPart2", System.Data.SqlDbType.Int, 0, "AddressPart2"),
            new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_FieldBusTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "FieldBusTypeId", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_FieldBusTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "FieldBusTypeId", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AddressPart1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AddressPart1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AddressPart1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AddressPart1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AddressPart2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AddressPart2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AddressPart2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AddressPart2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.Int, 4, "Id")});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.sqlConnection1;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_FieldBusTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "FieldBusTypeId", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_FieldBusTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "FieldBusTypeId", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AddressPart1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AddressPart1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AddressPart1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AddressPart1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AddressPart2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AddressPart2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AddressPart2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AddressPart2", System.Data.DataRowVersion.Original, null)});
            // 
            // sdaFieldBusNodes
            // 
            this.sdaFieldBusNodes.DeleteCommand = this.sqlDeleteCommand1;
            this.sdaFieldBusNodes.InsertCommand = this.sqlInsertCommand1;
            this.sdaFieldBusNodes.SelectCommand = this.sqlSelectCommand1;
            this.sdaFieldBusNodes.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "FieldBusNode", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Id", "Id"),
                        new System.Data.Common.DataColumnMapping("FieldBusTypeId", "FieldBusTypeId"),
                        new System.Data.Common.DataColumnMapping("AddressPart1", "AddressPart1"),
                        new System.Data.Common.DataColumnMapping("AddressPart2", "AddressPart2")})});
            this.sdaFieldBusNodes.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sdaPChannels
            // 
            this.sdaPChannels.DeleteCommand = this.sqlCommand1;
            this.sdaPChannels.InsertCommand = this.sqlCommand2;
            this.sdaPChannels.SelectCommand = this.sqlCommand3;
            this.sdaPChannels.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PhysicalChannel", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Id", "Id"),
                        new System.Data.Common.DataColumnMapping("FieldNodeId", "FieldNodeId"),
                        new System.Data.Common.DataColumnMapping("AddressShift", "AddressShift"),
                        new System.Data.Common.DataColumnMapping("ReadAddress", "ReadAddress"),
                        new System.Data.Common.DataColumnMapping("WriteAddress", "WriteAddress"),
                        new System.Data.Common.DataColumnMapping("PhysicalChannelSize", "PhysicalChannelSize"),
                        new System.Data.Common.DataColumnMapping("IsInput", "IsInput"),
                        new System.Data.Common.DataColumnMapping("IsOutput", "IsOutput"),
                        new System.Data.Common.DataColumnMapping("IsAnalog", "IsAnalog"),
                        new System.Data.Common.DataColumnMapping("IsDiscrete", "IsDiscrete"),
                        new System.Data.Common.DataColumnMapping("Description", "Description"),
                        new System.Data.Common.DataColumnMapping("Register", "Register")})});
            this.sdaPChannels.UpdateCommand = this.sqlCommand4;
            // 
            // sqlCommand1
            // 
            this.sqlCommand1.CommandText = resources.GetString("sqlCommand1.CommandText");
            this.sqlCommand1.Connection = this.sqlConnection1;
            this.sqlCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_FieldNodeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "FieldNodeId", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_FieldNodeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "FieldNodeId", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AddressShift", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AddressShift", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AddressShift", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AddressShift", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ReadAddress", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ReadAddress", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ReadAddress", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ReadAddress", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WriteAddress", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WriteAddress", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WriteAddress", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WriteAddress", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PhysicalChannelSize", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PhysicalChannelSize", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PhysicalChannelSize", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PhysicalChannelSize", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_IsInput", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsInput", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_IsOutput", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsOutput", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_IsAnalog", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsAnalog", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_IsDiscrete", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsDiscrete", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Description", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Register", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Register", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Register", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Register", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlCommand2
            // 
            this.sqlCommand2.CommandText = resources.GetString("sqlCommand2.CommandText");
            this.sqlCommand2.Connection = this.sqlConnection1;
            this.sqlCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@FieldNodeId", System.Data.SqlDbType.Int, 0, "FieldNodeId"),
            new System.Data.SqlClient.SqlParameter("@AddressShift", System.Data.SqlDbType.SmallInt, 0, "AddressShift"),
            new System.Data.SqlClient.SqlParameter("@ReadAddress", System.Data.SqlDbType.Int, 0, "ReadAddress"),
            new System.Data.SqlClient.SqlParameter("@WriteAddress", System.Data.SqlDbType.Int, 0, "WriteAddress"),
            new System.Data.SqlClient.SqlParameter("@PhysicalChannelSize", System.Data.SqlDbType.Int, 0, "PhysicalChannelSize"),
            new System.Data.SqlClient.SqlParameter("@IsInput", System.Data.SqlDbType.Bit, 0, "IsInput"),
            new System.Data.SqlClient.SqlParameter("@IsOutput", System.Data.SqlDbType.Bit, 0, "IsOutput"),
            new System.Data.SqlClient.SqlParameter("@IsAnalog", System.Data.SqlDbType.Bit, 0, "IsAnalog"),
            new System.Data.SqlClient.SqlParameter("@IsDiscrete", System.Data.SqlDbType.Bit, 0, "IsDiscrete"),
            new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, "Description"),
            new System.Data.SqlClient.SqlParameter("@Register", System.Data.SqlDbType.Int, 0, "Register")});
            // 
            // sqlCommand3
            // 
            this.sqlCommand3.CommandText = "SELECT     PhysicalChannel.*\r\nFROM         PhysicalChannel";
            this.sqlCommand3.Connection = this.sqlConnection1;
            // 
            // sqlCommand4
            // 
            this.sqlCommand4.CommandText = resources.GetString("sqlCommand4.CommandText");
            this.sqlCommand4.Connection = this.sqlConnection1;
            this.sqlCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@FieldNodeId", System.Data.SqlDbType.Int, 0, "FieldNodeId"),
            new System.Data.SqlClient.SqlParameter("@AddressShift", System.Data.SqlDbType.SmallInt, 0, "AddressShift"),
            new System.Data.SqlClient.SqlParameter("@ReadAddress", System.Data.SqlDbType.Int, 0, "ReadAddress"),
            new System.Data.SqlClient.SqlParameter("@WriteAddress", System.Data.SqlDbType.Int, 0, "WriteAddress"),
            new System.Data.SqlClient.SqlParameter("@PhysicalChannelSize", System.Data.SqlDbType.Int, 0, "PhysicalChannelSize"),
            new System.Data.SqlClient.SqlParameter("@IsInput", System.Data.SqlDbType.Bit, 0, "IsInput"),
            new System.Data.SqlClient.SqlParameter("@IsOutput", System.Data.SqlDbType.Bit, 0, "IsOutput"),
            new System.Data.SqlClient.SqlParameter("@IsAnalog", System.Data.SqlDbType.Bit, 0, "IsAnalog"),
            new System.Data.SqlClient.SqlParameter("@IsDiscrete", System.Data.SqlDbType.Bit, 0, "IsDiscrete"),
            new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, "Description"),
            new System.Data.SqlClient.SqlParameter("@Register", System.Data.SqlDbType.Int, 0, "Register"),
            new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_FieldNodeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "FieldNodeId", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_FieldNodeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "FieldNodeId", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AddressShift", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AddressShift", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AddressShift", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AddressShift", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ReadAddress", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ReadAddress", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ReadAddress", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ReadAddress", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WriteAddress", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WriteAddress", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WriteAddress", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WriteAddress", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PhysicalChannelSize", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PhysicalChannelSize", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PhysicalChannelSize", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PhysicalChannelSize", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_IsInput", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsInput", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_IsOutput", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsOutput", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_IsAnalog", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsAnalog", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_IsDiscrete", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsDiscrete", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Description", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Register", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Register", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Register", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Register", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.Int, 4, "Id")});
            // 
            // sdaLCChannels
            // 
            this.sdaLCChannels.DeleteCommand = this.sqlCommand5;
            this.sdaLCChannels.InsertCommand = this.sqlCommand6;
            this.sdaLCChannels.SelectCommand = this.sqlCommand7;
            this.sdaLCChannels.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "LogicalChannel", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Id", "Id"),
                        new System.Data.Common.DataColumnMapping("PhysicalChannelId", "PhysicalChannelId"),
                        new System.Data.Common.DataColumnMapping("DirectPolynom", "DirectPolynom"),
                        new System.Data.Common.DataColumnMapping("ReversePolynom", "ReversePolynom"),
                        new System.Data.Common.DataColumnMapping("Size", "Size"),
                        new System.Data.Common.DataColumnMapping("PollPeriod", "PollPeriod"),
                        new System.Data.Common.DataColumnMapping("Description", "Description")})});
            this.sdaLCChannels.UpdateCommand = this.sqlCommand8;
            // 
            // sqlCommand5
            // 
            this.sqlCommand5.CommandText = resources.GetString("sqlCommand5.CommandText");
            this.sqlCommand5.Connection = this.sqlConnection1;
            this.sqlCommand5.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PhysicalChannelId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PhysicalChannelId", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PhysicalChannelId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PhysicalChannelId", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Size", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Size", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Size", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Size", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PollPeriod", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PollPeriod", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PollPeriod", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(18)), ((byte)(6)), "PollPeriod", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlCommand6
            // 
            this.sqlCommand6.CommandText = resources.GetString("sqlCommand6.CommandText");
            this.sqlCommand6.Connection = this.sqlConnection1;
            this.sqlCommand6.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PhysicalChannelId", System.Data.SqlDbType.Int, 0, "PhysicalChannelId"),
            new System.Data.SqlClient.SqlParameter("@DirectPolynom", System.Data.SqlDbType.NVarChar, 0, "DirectPolynom"),
            new System.Data.SqlClient.SqlParameter("@ReversePolynom", System.Data.SqlDbType.NVarChar, 0, "ReversePolynom"),
            new System.Data.SqlClient.SqlParameter("@Size", System.Data.SqlDbType.Int, 0, "Size"),
            new System.Data.SqlClient.SqlParameter("@PollPeriod", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(18)), ((byte)(6)), "PollPeriod", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, "Description")});
            // 
            // sqlCommand7
            // 
            this.sqlCommand7.CommandText = "SELECT     LogicalChannel.*\r\nFROM         LogicalChannel";
            this.sqlCommand7.Connection = this.sqlConnection1;
            // 
            // sqlCommand8
            // 
            this.sqlCommand8.CommandText = resources.GetString("sqlCommand8.CommandText");
            this.sqlCommand8.Connection = this.sqlConnection1;
            this.sqlCommand8.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PhysicalChannelId", System.Data.SqlDbType.Int, 0, "PhysicalChannelId"),
            new System.Data.SqlClient.SqlParameter("@DirectPolynom", System.Data.SqlDbType.NVarChar, 0, "DirectPolynom"),
            new System.Data.SqlClient.SqlParameter("@ReversePolynom", System.Data.SqlDbType.NVarChar, 0, "ReversePolynom"),
            new System.Data.SqlClient.SqlParameter("@Size", System.Data.SqlDbType.Int, 0, "Size"),
            new System.Data.SqlClient.SqlParameter("@PollPeriod", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(18)), ((byte)(6)), "PollPeriod", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, "Description"),
            new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PhysicalChannelId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PhysicalChannelId", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PhysicalChannelId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PhysicalChannelId", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Size", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Size", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Size", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Size", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PollPeriod", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PollPeriod", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PollPeriod", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(18)), ((byte)(6)), "PollPeriod", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.Int, 4, "Id")});
            // 
            // sdaFieldBuses
            // 
            this.sdaFieldBuses.DeleteCommand = this.sqlCommand9;
            this.sdaFieldBuses.InsertCommand = this.sqlCommand10;
            this.sdaFieldBuses.SelectCommand = this.sqlCommand11;
            this.sdaFieldBuses.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "FieldBusNode", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Id", "Id"),
                        new System.Data.Common.DataColumnMapping("FieldBusTypeId", "FieldBusTypeId"),
                        new System.Data.Common.DataColumnMapping("AddressPart1", "AddressPart1"),
                        new System.Data.Common.DataColumnMapping("AddressPart2", "AddressPart2")})});
            this.sdaFieldBuses.UpdateCommand = this.sqlCommand12;
            // 
            // sqlCommand9
            // 
            this.sqlCommand9.CommandText = resources.GetString("sqlCommand9.CommandText");
            this.sqlCommand9.Connection = this.sqlConnection1;
            this.sqlCommand9.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_FieldBusTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "FieldBusTypeId", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_FieldBusTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "FieldBusTypeId", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AddressPart1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AddressPart1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AddressPart1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AddressPart1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AddressPart2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AddressPart2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AddressPart2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AddressPart2", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlCommand10
            // 
            this.sqlCommand10.CommandText = resources.GetString("sqlCommand10.CommandText");
            this.sqlCommand10.Connection = this.sqlConnection1;
            this.sqlCommand10.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@FieldBusTypeId", System.Data.SqlDbType.Int, 0, "FieldBusTypeId"),
            new System.Data.SqlClient.SqlParameter("@AddressPart1", System.Data.SqlDbType.NVarChar, 0, "AddressPart1"),
            new System.Data.SqlClient.SqlParameter("@AddressPart2", System.Data.SqlDbType.Int, 0, "AddressPart2")});
            // 
            // sqlCommand11
            // 
            this.sqlCommand11.CommandText = "SELECT     *\r\nFROM         FieldBusNode";
            this.sqlCommand11.Connection = this.sqlConnection1;
            // 
            // sqlCommand12
            // 
            this.sqlCommand12.CommandText = resources.GetString("sqlCommand12.CommandText");
            this.sqlCommand12.Connection = this.sqlConnection1;
            this.sqlCommand12.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@FieldBusTypeId", System.Data.SqlDbType.Int, 0, "FieldBusTypeId"),
            new System.Data.SqlClient.SqlParameter("@AddressPart1", System.Data.SqlDbType.NVarChar, 0, "AddressPart1"),
            new System.Data.SqlClient.SqlParameter("@AddressPart2", System.Data.SqlDbType.Int, 0, "AddressPart2"),
            new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_FieldBusTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "FieldBusTypeId", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_FieldBusTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "FieldBusTypeId", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AddressPart1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AddressPart1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AddressPart1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AddressPart1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AddressPart2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AddressPart2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AddressPart2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AddressPart2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.Int, 4, "Id")});
            // 
            // sdaIoModules
            // 
            this.sdaIoModules.DeleteCommand = this.sqlCommand13;
            this.sdaIoModules.InsertCommand = this.sqlCommand14;
            this.sdaIoModules.SelectCommand = this.sqlCommand15;
            this.sdaIoModules.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "IoModule", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Id", "Id"),
                        new System.Data.Common.DataColumnMapping("PhisylcaIChanneld", "PhisylcaIChanneld"),
                        new System.Data.Common.DataColumnMapping("Description", "Description")})});
            this.sdaIoModules.UpdateCommand = this.sqlCommand16;
            // 
            // sqlCommand13
            // 
            this.sqlCommand13.CommandText = resources.GetString("sqlCommand13.CommandText");
            this.sqlCommand13.Connection = this.sqlConnection1;
            this.sqlCommand13.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PhisylcaIChanneld", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PhisylcaIChanneld", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PhisylcaIChanneld", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PhisylcaIChanneld", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Description", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlCommand14
            // 
            this.sqlCommand14.CommandText = "INSERT INTO [IoModule] ([Id], [PhisylcaIChanneld], [Description]) VALUES (@Id, @P" +
                "hisylcaIChanneld, @Description);\r\nSELECT Id, PhisylcaIChanneld, Description FROM" +
                " IoModule WHERE (Id = @Id)";
            this.sqlCommand14.Connection = this.sqlConnection1;
            this.sqlCommand14.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.Int, 0, "Id"),
            new System.Data.SqlClient.SqlParameter("@PhisylcaIChanneld", System.Data.SqlDbType.Int, 0, "PhisylcaIChanneld"),
            new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, "Description")});
            // 
            // sqlCommand15
            // 
            this.sqlCommand15.CommandText = "SELECT     IoModule.*\r\nFROM         IoModule";
            this.sqlCommand15.Connection = this.sqlConnection1;
            // 
            // sqlCommand16
            // 
            this.sqlCommand16.CommandText = resources.GetString("sqlCommand16.CommandText");
            this.sqlCommand16.Connection = this.sqlConnection1;
            this.sqlCommand16.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.Int, 0, "Id"),
            new System.Data.SqlClient.SqlParameter("@PhisylcaIChanneld", System.Data.SqlDbType.Int, 0, "PhisylcaIChanneld"),
            new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, "Description"),
            new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PhisylcaIChanneld", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PhisylcaIChanneld", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PhisylcaIChanneld", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PhisylcaIChanneld", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Description", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=AM2\\SQLEXPRESS;Initial Catalog=Plc;Integrated Security=True";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // ChannelsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ChannelsControl";
            this.Size = new System.Drawing.Size(544, 263);
            this.tpLChannels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLChannels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtsChannelConfiguration1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tpFieldNodes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFieldNodes)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tpFieldBuses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFieldBuses)).EndInit();
            this.tpPChannels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPChannels)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpLChannels;
        private System.Windows.Forms.DataGridView dgvLChannels;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvPChannels;
        private System.Windows.Forms.TabPage tpFieldNodes;
        private System.Windows.Forms.DataGridView dgvFieldNodes;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpPChannels;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DtsChannelConfiguration dtsChannelConfiguration1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldBusIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldNodeAddressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn physicalChannelIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.TabPage tpFieldBuses;
        private System.Windows.Forms.DataGridView dgvFieldBuses;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldBusTypeIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldBusNameDataGridViewTextBoxColumn;
        private Oleg_ivo.Plc.DataManager dataManager1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter sdaFieldBusNodes;
        private System.Data.SqlClient.SqlDataAdapter sdaPChannels;
        private System.Data.SqlClient.SqlCommand sqlCommand1;
        private System.Data.SqlClient.SqlCommand sqlCommand2;
        private System.Data.SqlClient.SqlCommand sqlCommand3;
        private System.Data.SqlClient.SqlCommand sqlCommand4;
        private System.Data.SqlClient.SqlDataAdapter sdaLCChannels;
        private System.Data.SqlClient.SqlCommand sqlCommand5;
        private System.Data.SqlClient.SqlCommand sqlCommand6;
        private System.Data.SqlClient.SqlCommand sqlCommand7;
        private System.Data.SqlClient.SqlCommand sqlCommand8;
        private System.Data.SqlClient.SqlDataAdapter sdaFieldBuses;
        private System.Data.SqlClient.SqlCommand sqlCommand9;
        private System.Data.SqlClient.SqlCommand sqlCommand10;
        private System.Data.SqlClient.SqlCommand sqlCommand11;
        private System.Data.SqlClient.SqlCommand sqlCommand12;
        private System.Data.SqlClient.SqlDataAdapter sdaIoModules;
        private System.Data.SqlClient.SqlCommand sqlCommand13;
        private System.Data.SqlClient.SqlCommand sqlCommand14;
        private System.Data.SqlClient.SqlCommand sqlCommand15;
        private System.Data.SqlClient.SqlCommand sqlCommand16;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
    }
}
