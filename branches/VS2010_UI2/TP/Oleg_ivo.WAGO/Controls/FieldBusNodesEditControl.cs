using System.ComponentModel;
using System.Windows.Forms;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.WAGO.Controls
{
    ///<summary>
    ///
    ///</summary>
    public partial class FieldBusNodesEditControl : UserControl//todo: FieldBusDAC
    {
        private FieldBusType _fieldBusType;

        ///<summary>
        ///
        ///</summary>
        public FieldBusNodesEditControl()
        {
            InitializeComponent();
        }

        ///<summary>
        /// Тип полевой шины
        ///</summary>
        [DefaultValue(typeof(FieldBusType), "Unknown"), 
        Description("Тип полевой шины")]
        public FieldBusType FieldBusType
        {
            get { return _fieldBusType; }
            set { _fieldBusType = value; }
        }

        private void btnLoad_Click(object sender, System.EventArgs e)
        {
            Fill();
        }

        ///<summary>
        ///
        ///</summary>
        public void Fill()
        {
            SDA.SelectCommand.Parameters["@FieldBusTypeId"].Value = (int)FieldBusType;
            dataManager1.Fill();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Save();
        }

        ///<summary>
        ///
        ///</summary>
        public void Save()
        {
            dataManager1.Save();
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (dataGridView1.Columns[e.ColumnIndex].DataPropertyName)
            {
                case "IPAddress":
                    string address = e.FormattedValue.ToString();
                    
                    if (!dataGridView1.Rows[e.RowIndex].IsNewRow || address != "")
                    {
                        //Regex regex = new Regex(@"(0-9){1,3}");

                        //if (!regex.IsMatch(address))
                        //{
                        //    if (MessageBox.Show("Продолжить редактирование?", "Несоответствие формату IP-адреса",
                        //                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        //    {
                        //        e.Cancel = true;
                        //    }
                        //    else
                        //    {
                        //        dataGridView1.CancelEdit();
                        //        //отменяем изменения
                        //    }
                            
                                
                            
                        //}
                    }
                    break;
                case "FieldBusTypeId":
                    int fieldBusTypeId = System.Convert.ToInt32(e.FormattedValue);
                    
                    if (dataGridView1.Rows[e.RowIndex].IsNewRow || fieldBusTypeId == 0)
                    {
                        fieldBusTypeId = 1;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = fieldBusTypeId;
                        //Regex regex = new Regex(@"(0-9){1,3}");

                        //if (!regex.IsMatch(address))
                        //{
                        //    if (MessageBox.Show("Продолжить редактирование?", "Несоответствие формату IP-адреса",
                        //                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        //    {
                        //        e.Cancel = true;
                        //    }
                        //    else
                        //    {
                        //        dataGridView1.CancelEdit();
                        //        //отменяем изменения
                        //    }
                            
                                
                            
                        //}
                    }
                    break;
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            DataGridViewRow row = e.Row;
            if (row.IsNewRow)
            {

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    switch (column.DataPropertyName)
                    {
                        case "FieldBusTypeId":
                            row.Cells[column.Index].Value = (int)FieldBusType;
                            break;
                    }
                }
                //dataGridView1..["FieldBusTypeId"]
                //e.Row.Cells["FieldBusTypeId"]
            }
        }

    }
}