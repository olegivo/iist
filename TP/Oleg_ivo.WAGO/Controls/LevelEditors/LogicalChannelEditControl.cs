using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using NLog;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.WAGO.Factory;

namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    ///<summary>
    ///
    ///</summary>
    public partial class LogicalChannelEditControl : UserControl, IDbEditor
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly LogicalChannelsDAC logicalChannelsDac = new LogicalChannelsDAC();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ILogicalChannelsFactory LogicalChannelsFactory { set { logicalChannelsDac.LogicalChannelsFactory = value; } }

        ///<summary>
        ///
        ///</summary>
        public LogicalChannelEditControl()
        {
            InitializeComponent();
            logicalChannelsDac.DataSet = dtsChannelConfiguration1;
        }

        ///<summary>
        /// ���������
        ///</summary>
        public void Save()
        {
            CurrencyManager cm = GetCurrencyManager();
            if (cm != null) cm.EndCurrentEdit();

            logicalChannelsDac.Save();
        }

        private CurrencyManager GetCurrencyManager()
        {
            return BindingContext[dtsChannelConfiguration1, textBox1.DataBindings[0].BindingMemberInfo.BindingPath] as CurrencyManager;
        }

        ///<summary>
        /// �������� ��� �������
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
            return (SqlDataAdapter)logicalChannelsDac.DataAdapter;
        }

        ///<summary>
        /// ���������
        ///</summary>
        ///<param name="editValue"></param>
        public void Fill(object editValue)
        {
            LogicalChannel logicalChannel = editValue as LogicalChannel;
            if (logicalChannel != null)
            {
                IEnumerable<LogicalChannel> logicalChannels =
                    logicalChannelsDac.GetChannels(logicalChannel.PhysicalChannel).Where(
                        LogicalChannel.GetEqualsPredicate(logicalChannel.Id, 
                                                            logicalChannel.PhysicalChannel.Id,
                                                            logicalChannel.AddressShift,
                                                            logicalChannel.ChannelSize));
                switch (logicalChannels.Count())
                {
                    case 0://������ ����� - �����
                        //TODO: ��� �������������� ������ �������������� ������ - ��������� �� �������
                        dtsChannelConfiguration1.LogicalChannel.Clear();
                        logicalChannelsDac.FillLogicalChannelsData(new []{logicalChannel},
                                                                    dtsChannelConfiguration1.LogicalChannel);
                        break;
                    case 1://������ ����������� �����
                        //TODO: ��� �������������� ������������ ������ - ��������� �� �������
                        Log.Debug("LogicalChannel �� ������: �� ����������");
                        //TODO: LogicalChannel �� ������: �� ����������
                        break;
                    default://������� ��������������
                        throw new InvalidOperationException(string.Format("������� ��������� ����������� ������ {0}", logicalChannel));
                }
                
            }

            CurrencyManager cm = GetCurrencyManager();
            cm.SuspendBinding();
            //physicalChannelsDAC1.DataSet
            //logicalChannelsDac.FillChannelsFromDb(0, 0);
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