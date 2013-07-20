using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using NLog;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;
using Oleg_ivo.WAGO.Factory;

namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    ///<summary>
    ///
    ///</summary>
    public partial class FieldBusNodeEditControl : UserControl, IDbEditor
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly FieldBusNodeDAC fieldBusNodeDAC;

        ///<summary>
        ///
        ///</summary>
        public FieldBusNodeEditControl()
        {
            InitializeComponent();

            fieldBusNodeDAC = new FieldBusNodeDAC {DataSet = dtsChannelConfiguration1};
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IFieldBusNodeFactory FieldBusNodeFactory { set { fieldBusNodeDAC.FieldBusNodesFactory = value; } }

        ///<summary>
        /// Сохранить
        ///</summary>
        public void Save()
        {
            CurrencyManager cm = GetCurrencyManager();
            if (cm != null) cm.EndCurrentEdit();

            fieldBusNodeDAC.Save();
        }

        private CurrencyManager GetCurrencyManager()
        {
            return BindingContext[dtsChannelConfiguration1, textBox1.DataBindings[0].BindingMemberInfo.BindingPath] as CurrencyManager;
        }

        /////<summary>
        ///// Параметр для выборки
        /////</summary>
        //public byte FieldBusNodeAddress
        //{
        //    get
        //    {
        //        object value = DataAdapter().SelectCommand.Parameters[0].Value;
        //        return (byte) (value is DBNull ? 0 : value);
        //    }
        //    set
        //    {
        //        DataAdapter().SelectCommand.Parameters[0].Value = value > 0 ? (object)value : DBNull.Value;
        //    }
        //}

        private SqlDataAdapter DataAdapter()
        {
            return (SqlDataAdapter)fieldBusNodeDAC.DataAdapter;
        }

        ///<summary>
        ///
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FieldBusNodeId
        {
            get
            {
                object value = DataAdapter().SelectCommand.Parameters["@Id"].Value;
                return (int)(value is DBNull ? 0 : value);
            }
            set
            {
                DataAdapter().SelectCommand.Parameters["@Id"].Value = value > 0 ? (object)value : DBNull.Value;
            }
        }

        ///<summary>
        /// Заполнить
        ///</summary>
        ///<param name="editValue"></param>
        public void Fill(object editValue)
        {
            FieldBusNode fieldBusNode = editValue as FieldBusNode;
            if (fieldBusNode != null)
            {
                Log.Debug("FieldBusNode из дерева: не обработано");
                //TODO: FieldBusNode из дерева: не обработано
            }

            CurrencyManager cm = GetCurrencyManager();
            cm.SuspendBinding();
            //physicalChannelsDAC1.DataSet
            fieldBusNodeDAC.FillFieldBusNodesFromDb(0, 0);
            cm.ResumeBinding();
            //dataManager1.Fill();
        }
    }
}