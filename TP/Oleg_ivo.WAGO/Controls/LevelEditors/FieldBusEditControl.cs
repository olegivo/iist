using System;
using  System.ComponentModel;
using System.Windows.Forms;
using NLog;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;

namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    ///<summary>
    ///
    ///</summary>
    public partial class FieldBusEditControl : UserControl, IDbEditor
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        ///<summary>
        ///
        ///</summary>
        public FieldBusEditControl()
        {
            InitializeComponent();
        }

        ///<summary>
        /// Сохранить
        ///</summary>
        public void Save()
        {
            CurrencyManager cm = GetCurrencyManager();
            if (cm != null)
            {
                cm.EndCurrentEdit();
            }

            dataManager1.Save();
        }

        private CurrencyManager GetCurrencyManager()
        {
            return BindingContext[dtsChannelConfiguration1, textBox1.DataBindings[0].BindingMemberInfo.BindingPath] as CurrencyManager;
        }

        ///<summary>
        /// Параметр для выборки
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Id
        {
            get
            {
                object value = SDA.SelectCommand.Parameters[0].Value;
                return (int) (value is DBNull ? 0 : value);
            }
            set
            {
                SDA.SelectCommand.Parameters[0].Value = value > 0 ? (object) value : DBNull.Value;
            }
        }

        ///<summary>
        /// Заполнить
        ///</summary>
        ///<param name="editValue"></param>
        public void Fill(object editValue)
        {
            FieldBusManager fieldBusManager = editValue as FieldBusManager;
            if (fieldBusManager!=null)
            {
                Log.Debug("FieldBusManager из дерева: не обработано");
                //TODO: FieldBusManager из дерева: не обработано
            }

            dataManager1.Fill();
        }
    }
}