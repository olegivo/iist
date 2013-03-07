using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    ///<summary>
    ///
    ///</summary>
    public partial class LogicalChannelEditControl : UserControl, IDbEditor
    {
        ///<summary>
        ///
        ///</summary>
        public LogicalChannelEditControl()
        {
            InitializeComponent();

            logicalChannelsDAC1.DataSet = dtsChannelConfiguration1;
        }

        ///<summary>
        /// —охранить
        ///</summary>
        public void Save()
        {
            CurrencyManager cm = GetCurrencyManager();
            if (cm != null) cm.EndCurrentEdit();

            logicalChannelsDAC1.Save();
        }

        private CurrencyManager GetCurrencyManager()
        {
            return BindingContext[dtsChannelConfiguration1, textBox1.DataBindings[0].BindingMemberInfo.BindingPath] as CurrencyManager;
        }

        ///<summary>
        /// ѕараметр дл€ выборки
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
            }
        }
        
        private SqlDataAdapter DataAdapter()
        {
            return (SqlDataAdapter)logicalChannelsDAC1.DataAdapter;
        }

        ///<summary>
        /// «аполнить
        ///</summary>
        ///<param name="editValue"></param>
        public void Fill(object editValue)
        {
            LogicalChannel logicalChannel = editValue as LogicalChannel;
            if (logicalChannel != null)
            {
                IEnumerable<LogicalChannel> logicalChannels =
                    logicalChannelsDAC1.GetChannels(logicalChannel.PhysicalChannel).Where(
                        LogicalChannel.GetEqualsPredicate(logicalChannel.Id, 
                                                            logicalChannel.PhysicalChannel.Id,
                                                            logicalChannel.AddressShift,
                                                            logicalChannel.ChannelSize));
                switch (logicalChannels.Count())
                {
                    case 0://данный канал - новый
                        //TODO: дл€ редактировани€ нового несохранЄнного канала - заполнить им датасет
                        dtsChannelConfiguration1.LogicalChannel.Clear();
                        logicalChannelsDAC1.FillLogicalChannelsData(new []{logicalChannel},
                                                                    dtsChannelConfiguration1.LogicalChannel);
                        break;
                    case 1://найден сохранЄнный канал
                        //TODO: дл€ редактировани€ сохранЄнного канала - заполнить им датасет
                        Console.WriteLine("LogicalChannel из дерева: не обработано");
                        //TODO: LogicalChannel из дерева: не обработано
                        break;
                    default://найдено несоответствие
                        throw new InvalidOperationException(string.Format("Ќайдены дубликаты логического канала {0}", logicalChannel));
                }
                
            }

            CurrencyManager cm = GetCurrencyManager();
            cm.SuspendBinding();
            //physicalChannelsDAC1.DataSet
            //logicalChannelsDAC1.FillChannelsFromDb(0, 0);
            cm.ResumeBinding();
            //dataManager1.Fill();
        }

        private void btnEditDirectPolynom_Click(object sender, EventArgs e)
        {
            PolynomEditForm polynomEditForm = new PolynomEditForm();
            DtsChannelConfiguration.LogicalChannelRow row = ((DtsChannelConfiguration.LogicalChannelRow)(((System.Data.DataRowView)(GetCurrencyManager().Current)).Row));
            Polynom polynom = !row.IsDirectPolynomNull() ? Polynom.DeSerializePolynom(row.DirectPolynom) : new Polynom();

            polynomEditForm.Polynom = polynom ;

            polynomEditForm.ShowDialog();

            row.DirectPolynom = Polynom.SerializePolynom(polynom);
            //row.AcceptChanges();
        }

        private void btnEditReversePolynom_Click(object sender, EventArgs e)
        {
            PolynomEditForm polynomEditForm = new PolynomEditForm();
            DtsChannelConfiguration.LogicalChannelRow row = ((DtsChannelConfiguration.LogicalChannelRow)(((System.Data.DataRowView)(GetCurrencyManager().Current)).Row));
            Polynom polynom = !row.IsDirectPolynomNull() ? Polynom.DeSerializePolynom(row.ReversePolynom) : new Polynom();

            polynomEditForm.Polynom = polynom;

            polynomEditForm.ShowDialog();

            row.ReversePolynom = Polynom.SerializePolynom(polynom);
            //row.AcceptChanges();
        }
    }
}