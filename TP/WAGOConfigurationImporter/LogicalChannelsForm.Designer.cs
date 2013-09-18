namespace WAGOConfigurationImporter
{
    partial class LogicalChannelsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogicalChannelsForm));
            this.plc27DataSet = new WAGOConfigurationImporter.Plc27DataSet();
            this.logicalChannelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.logicalChannelTableAdapter = new WAGOConfigurationImporter.Plc27DataSetTableAdapters.LogicalChannelTableAdapter();
            this.tableAdapterManager = new WAGOConfigurationImporter.Plc27DataSetTableAdapters.TableAdapterManager();
            this.logicalChannelBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.logicalChannelBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.logicalChannelDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.plc27DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicalChannelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicalChannelBindingNavigator)).BeginInit();
            this.logicalChannelBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logicalChannelDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // plc27DataSet
            // 
            this.plc27DataSet.DataSetName = "Plc27DataSet";
            this.plc27DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // logicalChannelBindingSource
            // 
            this.logicalChannelBindingSource.DataMember = "LogicalChannel";
            this.logicalChannelBindingSource.DataSource = this.plc27DataSet;
            // 
            // logicalChannelTableAdapter
            // 
            this.logicalChannelTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.FieldBusNodeTableAdapter = null;
            this.tableAdapterManager.LogicalChannelTableAdapter = this.logicalChannelTableAdapter;
            this.tableAdapterManager.PhysicalChannelTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = WAGOConfigurationImporter.Plc27DataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // logicalChannelBindingNavigator
            // 
            this.logicalChannelBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.logicalChannelBindingNavigator.BindingSource = this.logicalChannelBindingSource;
            this.logicalChannelBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.logicalChannelBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.logicalChannelBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.logicalChannelBindingNavigatorSaveItem});
            this.logicalChannelBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.logicalChannelBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.logicalChannelBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.logicalChannelBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.logicalChannelBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.logicalChannelBindingNavigator.Name = "logicalChannelBindingNavigator";
            this.logicalChannelBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.logicalChannelBindingNavigator.Size = new System.Drawing.Size(508, 25);
            this.logicalChannelBindingNavigator.TabIndex = 0;
            this.logicalChannelBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 15);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // logicalChannelBindingNavigatorSaveItem
            // 
            this.logicalChannelBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.logicalChannelBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("logicalChannelBindingNavigatorSaveItem.Image")));
            this.logicalChannelBindingNavigatorSaveItem.Name = "logicalChannelBindingNavigatorSaveItem";
            this.logicalChannelBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 23);
            this.logicalChannelBindingNavigatorSaveItem.Text = "Save Data";
            this.logicalChannelBindingNavigatorSaveItem.Click += new System.EventHandler(this.logicalChannelBindingNavigatorSaveItem_Click);
            // 
            // logicalChannelDataGridView
            // 
            this.logicalChannelDataGridView.AutoGenerateColumns = false;
            this.logicalChannelDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.logicalChannelDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logicalChannelDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn9});
            this.logicalChannelDataGridView.DataSource = this.logicalChannelBindingSource;
            this.logicalChannelDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logicalChannelDataGridView.EnableHeadersVisualStyles = false;
            this.logicalChannelDataGridView.Location = new System.Drawing.Point(0, 25);
            this.logicalChannelDataGridView.Name = "logicalChannelDataGridView";
            this.logicalChannelDataGridView.RowHeadersVisible = false;
            this.logicalChannelDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.logicalChannelDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logicalChannelDataGridView.Size = new System.Drawing.Size(508, 311);
            this.logicalChannelDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "№ Канала";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn9.HeaderText = "Описание канала";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 400;
            // 
            // LogicalChannelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(508, 336);
            this.Controls.Add(this.logicalChannelDataGridView);
            this.Controls.Add(this.logicalChannelBindingNavigator);
            this.Name = "LogicalChannelsForm";
            this.ShowIcon = false;
            this.Text = "Редактор описания каналов";
            this.Load += new System.EventHandler(this.LogicalChannelsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.plc27DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicalChannelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicalChannelBindingNavigator)).EndInit();
            this.logicalChannelBindingNavigator.ResumeLayout(false);
            this.logicalChannelBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logicalChannelDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Plc27DataSet plc27DataSet;
        private System.Windows.Forms.BindingSource logicalChannelBindingSource;
        private Plc27DataSetTableAdapters.LogicalChannelTableAdapter logicalChannelTableAdapter;
        private Plc27DataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator logicalChannelBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton logicalChannelBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView logicalChannelDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

    }
}