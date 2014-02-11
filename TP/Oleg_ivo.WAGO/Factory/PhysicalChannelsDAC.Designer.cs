namespace Oleg_ivo.WAGO.Factory
{
    partial class PhysicalChannelsDAC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhysicalChannelsDAC));
            this.dtsChannelConfiguration1 = new Oleg_ivo.WAGO.DtsChannelConfiguration();
            this.dataManager1 = new Oleg_ivo.Plc.DataManager(this.components);
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.SDA = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            ((System.ComponentModel.ISupportInitialize)(this.dtsChannelConfiguration1)).BeginInit();
            // 
            // dtsChannelConfiguration1
            // 
            this.dtsChannelConfiguration1.DataSetName = "DtsChannelConfiguration";
            this.dtsChannelConfiguration1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataManager1
            // 
            this.dataManager1.DataAdapter = this.SDA;
            this.dataManager1.DataSet = this.dtsChannelConfiguration1;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.sqlConnection1;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@FieldNodeId", System.Data.SqlDbType.Int, 4, "FieldNodeId"),
            new System.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.Int, 4, "Id")});
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.sqlConnection1;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
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
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.sqlConnection1;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
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
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.sqlConnection1;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
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
            // SDA
            // 
            this.SDA.DeleteCommand = this.sqlDeleteCommand1;
            this.SDA.InsertCommand = this.sqlInsertCommand1;
            this.SDA.SelectCommand = this.sqlSelectCommand1;
            this.SDA.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
            this.SDA.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=AM2\\SQLEXPRESS;Initial Catalog=Plc;Integrated Security=True";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            ((System.ComponentModel.ISupportInitialize)(this.dtsChannelConfiguration1)).EndInit();

        }

        #endregion

        private DtsChannelConfiguration dtsChannelConfiguration1;
        private Oleg_ivo.Plc.DataManager dataManager1;
        private System.Data.SqlClient.SqlDataAdapter SDA;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;

    }
}