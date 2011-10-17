using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    ///<summary>
    ///
    ///</summary>
    public partial class PhysicalChannelEditControl : UserControl, IDbEditor
    {
        ///<summary>
        ///
        ///</summary>
        public PhysicalChannelEditControl()
        {
            InitializeComponent();

            physicalChannelsDAC1.DataSet = dtsChannelConfiguration1;
        }

        ///<summary>
        /// Сохранить
        ///</summary>
        public void Save()
        {
            CurrencyManager cm = GetCurrencyManager();
            if (cm != null) cm.EndCurrentEdit();

            physicalChannelsDAC1.Save();
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
                object value = DataAdapter().SelectCommand.Parameters["@Id"].Value;//adPChannels.SelectCommand.Parameters[0].Value;
                return (int)(value is DBNull ? 0 : value);
            }
            set
            {
                DataAdapter().SelectCommand.Parameters["@Id"].Value = value > 0 ? (object)value : DBNull.Value;
                //adPChannels.SelectCommand.Parameters[0].Value = value > 0 ? (object)value : DBNull.Value;
            }
        }

        private SqlDataAdapter DataAdapter()
        {
            return (SqlDataAdapter) physicalChannelsDAC1.DataAdapter;
        }


        ///<summary>
        /// Заполнить
        ///</summary>
        ///<param name="editValue"></param>
        public void Fill(object editValue)
        {
            PhysicalChannel physicalChannel = editValue as PhysicalChannel;
            if (physicalChannel != null)
            {
                Console.WriteLine("PhysicalChannel из дерева: не обработано");
                //TODO: PhysicalChannel из дерева: не обработано
            }

            CurrencyManager cm = GetCurrencyManager();
            cm.SuspendBinding();
            //physicalChannelsDAC1.DataSet
            physicalChannelsDAC1.FillChannelsFromDb(0, 0);
            cm.ResumeBinding();
            //dataManager1.Fill();
        }
    }
}