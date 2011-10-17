using System.Collections.Generic;
using System.Windows.Forms;

namespace Oleg_ivo.Plc.Ports
{
    public class ucPairCombobox : ComboBox
    {
        protected ucPairCombobox()
        {
            InitializeComponent();
            InitItems();
        }

        /// <summary>
        /// ���������������� ��������
        /// </summary>
        protected void InitItems()
        {
            List<ValueDescriptionPair> items = CreateItems();
            DataSource = items;
        }

        /// <summary>
        /// ������� ��������
        /// </summary>
        /// <returns></returns>
        protected virtual List<ValueDescriptionPair> CreateItems()
        {
            return new List<ValueDescriptionPair>();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // ucDevicePortCombobox
            // 
            ValueMember = "Value";
            DisplayMember = "Description";
            ResumeLayout(false);

        }

        
    }
}