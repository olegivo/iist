using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using NLog;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.WAGO.Forms;

namespace Oleg_ivo.Plc
{
    ///<summary>
    ///
    ///</summary>
    public partial class DataManager : Component
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        ///<summary>
        ///
        ///</summary>
        public DataManager()
        {
            InitializeComponent();
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="container"></param>
        public DataManager(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        ///<summary>
        ///
        ///</summary>
        public DataSet DataSet { get; set; }

        ///<summary>
        ///
        ///</summary>
        public IDbDataAdapter DataAdapter { get; set; }

        ///<summary>
        ///
        ///</summary>
        public void Fill()
        {
            ProcessDataAdapter(DataAdapter, DateAdapterProcessType.Fill, DataSet);
        }

        ///<summary>
        ///
        ///</summary>
        public void Save()
        {
            ProcessDataAdapter(DataAdapter, DateAdapterProcessType.Update, DataSet);
            Fill();//обратный селект
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="adapter"></param>
        ///<param name="processType"></param>
        ///<param name="dataSource"></param>
        ///<exception cref="ArgumentOutOfRangeException"></exception>
        public static void ProcessDataAdapter(IDbDataAdapter adapter, DateAdapterProcessType processType, DataSet dataSource)
        {
            if (adapter == null)
                throw new ArgumentNullException("adapter",
                                                "В компоненте DataManager необходимо задать адаптер данных");

            DbConnectionProvider.Instance.OpenConnection(adapter);
            try
            {
                int i = 0;
                string type;
                switch (processType)
                {
                    case DateAdapterProcessType.Unknown:
                        type = " (неизвестно, как обрабатывать)";
                        break;
                    case DateAdapterProcessType.Fill:
                        if (adapter.TableMappings.Count == 1)
                        {
                            DataTableMapping mapping = adapter.TableMappings[0] as DataTableMapping;
                            if (mapping != null)
                            {
                                DataTable table = dataSource.Tables[mapping.DataSetTable];
                                if (table == null)
                                    throw new Exception("Маппинг для таблицы задан неверно");
                                dataSource.Tables[mapping.DataSetTable].Rows.Clear();
                            }
                        }

                        //если не инициализирован параметр, ставим ему значение DBNull.Value
                        foreach (DbParameter parameter in adapter.SelectCommand.Parameters)
                        {
                            if (parameter.Value == null) parameter.Value = DBNull.Value;
                        }

                        i = adapter.Fill(dataSource);
                        type = " (загружено)";
                        break;
                    case DateAdapterProcessType.Update:
                        i = adapter.Update(dataSource);
                        type = " (сохранено)";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("processType");
                }

                Log.Debug("записей обработано{0} : {1}", type, i);
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                throw;
            }
            finally
            {
                DbConnectionProvider.Instance.CloseConnection(adapter);
            }
        }
    }
}
