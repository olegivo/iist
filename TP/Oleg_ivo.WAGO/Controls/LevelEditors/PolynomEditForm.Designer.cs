using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    partial class PolynomEditForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dtsPolynom1 = new DtsPolynom();
            this.powerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coefficientDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtsPolynom1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.powerDataGridViewTextBoxColumn,
            this.coefficientDataGridViewTextBoxColumn});
            this.dataGridView1.DataMember = "PolynomCoefficients";
            this.dataGridView1.DataSource = this.dtsPolynom1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(257, 358);
            this.dataGridView1.TabIndex = 0;
            // 
            // dtsPolynom1
            // 
            this.dtsPolynom1.DataSetName = "DtsPolynom";
            this.dtsPolynom1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // powerDataGridViewTextBoxColumn
            // 
            this.powerDataGridViewTextBoxColumn.DataPropertyName = "Power";
            this.powerDataGridViewTextBoxColumn.HeaderText = "Power";
            this.powerDataGridViewTextBoxColumn.Name = "powerDataGridViewTextBoxColumn";
            // 
            // coefficientDataGridViewTextBoxColumn
            // 
            this.coefficientDataGridViewTextBoxColumn.DataPropertyName = "Coefficient";
            this.coefficientDataGridViewTextBoxColumn.HeaderText = "Coefficient";
            this.coefficientDataGridViewTextBoxColumn.Name = "coefficientDataGridViewTextBoxColumn";
            // 
            // PolynomEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(467, 394);
            this.Controls.Add(this.dataGridView1);
            this.Name = "PolynomEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор коэффициентов полинома";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PolynomEditForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtsPolynom1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private DtsPolynom dtsPolynom1;
        private System.Windows.Forms.DataGridViewTextBoxColumn powerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn coefficientDataGridViewTextBoxColumn;
    }
}