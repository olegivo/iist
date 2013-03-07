using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    ///<summary>
    ///
    ///</summary>
    public partial class IoModuleEditControl : UserControl, IDbEditor
    {
        ///<summary>
        ///
        ///</summary>
        public IoModuleEditControl()
        {
            InitializeComponent();
        }

        ///<summary>
        /// ���������
        ///</summary>
        public void Save()
        {
            CurrencyManager cm = GetCurrencyManager();
            if (cm != null) cm.EndCurrentEdit();

            dataManager1.Save();
        }

        private CurrencyManager GetCurrencyManager()
        {
            return BindingContext[dtsChannelConfiguration1, textBox1.DataBindings[0].BindingMemberInfo.BindingPath] as CurrencyManager;
        }

        ///<summary>
        /// �������� ��� �������
        ///</summary>
        public int Id
        {
            get
            {
                object value = adIoModules.SelectCommand.Parameters[0].Value;
                return (int)(value is DBNull ? 0 : value);
            }
            set
            {
                adIoModules.SelectCommand.Parameters[0].Value = value > 0 ? (object)value : DBNull.Value;
            }
        }

        ///<summary>
        /// ���������
        ///</summary>
        ///<param name="editValue"></param>
        public void Fill(object editValue)
        {
            throw new NotImplementedException("editValue �� ����������");
            dataManager1.Fill();
        }
    }
}