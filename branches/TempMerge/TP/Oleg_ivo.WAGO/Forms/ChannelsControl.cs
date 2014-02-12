using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using NLog;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Devices.Modules;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.WAGO.Forms
{
    ///<summary>
    ///
    ///</summary>
    public partial class ChannelsControl : UserControl
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        ///<summary>
        ///
        ///</summary>
        public ChannelsControl()
        {
            InitializeComponent();
        }

        ///<summary>
        ///
        ///</summary>
        ///<exception cref="ArgumentOutOfRangeException"></exception>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Level ActiveLevel
        {
            get
            {
                TabPage tab = tabControl1.SelectedTab;
                
                if (tab == tpFieldBuses)
                    return Level.FieldBuses;
                if (tab == tpFieldNodes)
                    return Level.FieldBusNodes;
                if (tab == tpPChannels)
                    return Level.PChannels;
                if (tab == tpLChannels)
                    return Level.LChannels;

                return Level.Unknown;
                //throw new ArgumentOutOfRangeException("tab", tab != null ? tab.Name : null);
            }
            set
            {
                TabPage tab = null;
                switch(value)
                {
                    case Level.FieldBuses:
                        tab = tpFieldBuses;
                        break;
                    case Level.FieldBusNodes:
                        tab = tpFieldNodes;
                        break;
                    case Level.PChannels:
                        tab = tpPChannels;
                        break;
                    case Level.LChannels:
                        tab = tpLChannels;
                        break;
                    default:
                        break;
                        //throw new ArgumentOutOfRangeException("value", value, "нельзя установить данное значение в качестве текущего");
                }

                tabControl1.SelectedTab = tab;

                Fill();//и заполняем выбранный уровень
            }
        }

        ///<summary>
        ///
        ///</summary>
        ///<exception cref="ArgumentOutOfRangeException"></exception>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDbDataAdapter ActiveAdapter
        {
            get
            {
                switch (ActiveLevel)
                {
                    case Level.FieldBuses:
                        return sdaFieldBuses;
                    case Level.FieldBusNodes:
                        return sdaFieldBusNodes;
                    case Level.PChannels:
                        return sdaPChannels;
                    case Level.LChannels:
                        return sdaLCChannels;
                    default:
                        return null;
                        //throw new ArgumentOutOfRangeException("ActiveLevel", ActiveLevel, "нельзя определить активный адаптер данных");
                }
            }
        }

        ///<summary>
        /// Установить фильтр по родительскому элементу
        ///</summary>
        ///<param name="parent"></param>
        public void SetParentFilter(object parent)
        {
            Level level = Level.Unknown;

            if(parent is FieldBusManager)
            {
                FieldBusManager fieldBusManager = (FieldBusManager)parent;
                //подфильтрация по выбранной шине
                switch (fieldBusManager.FieldBusType)
                {
                    case FieldBusType.Unknown:
                        break;
                    case FieldBusType.RS232:
                        break;
                    case FieldBusType.RS485:
                        break;
                    case FieldBusType.Ethernet:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (parent is FieldBusNode)
            {
                FieldBusNode fieldBusNode = (FieldBusNode) parent;
            }
            else if (parent is IOModule)
            {
                IOModule ioModule = (IOModule) parent;
            }
            else if (parent is PhysicalChannel)
            {
                PhysicalChannel physicalChannel = (PhysicalChannel) parent;
            }
            else if (parent is LogicalChannel)
            {
                LogicalChannel logicalChannel = (LogicalChannel) parent;
            }

            level = (Level)(((int)level) << 1);//повышаем уровень
            ActiveLevel = level;
        }

        private void Fill()
        {
            dataManager1.DataAdapter = ActiveAdapter;
            if (dataManager1.DataAdapter!=null)
                dataManager1.Fill();
            else
                Log.Debug("ChannelsControl: На уровне {0} не задан ActiveAdapter", ActiveLevel);
        }

        private void Save()
        {
            dataManager1.DataAdapter = ActiveAdapter;
            dataManager1.Save();
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            Fill();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
