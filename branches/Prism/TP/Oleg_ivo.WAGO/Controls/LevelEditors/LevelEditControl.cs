using System;
using System.ComponentModel;
using System.Windows.Forms;
using Autofac;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;
using Oleg_ivo.WAGO.Forms;

namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    ///<summary>
    ///
    ///</summary>
    public partial class LevelEditControl : UserControl, IDbEditor
    {

        ///<summary>
        ///
        ///</summary>
        public LevelEditControl()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IComponentContext Context
        {
            set
            {
                var logicalChannelsFactory = value.Resolve<ILogicalChannelsFactory>();
                logicalChannelEditControl1.LogicalChannelsFactory = logicalChannelsFactory;
                physicalChannelEditControl1.LogicalChannelsFactory = logicalChannelsFactory;
                fieldBusNodeEditControl1.FieldBusNodeFactory = value.Resolve<IFieldBusNodeFactory>();
            }
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
            }
        }

        ///<summary>
        /// Установить фильтр по редактируемому элементу
        ///</summary>
        ///<param name="editObject"></param>
        public void SetEditorFilter(object editObject)
        {
            if(editObject is FieldBusManager)
            {
                FieldBusManager fieldBusManager = (FieldBusManager)editObject;
                ActiveLevel = Level.FieldBuses;
                fieldBusEditControl1.Id = GetFieldBusFilter(fieldBusManager);
            }
            else if (editObject is FieldBusNode)
            {
                ActiveLevel = Level.FieldBusNodes;
                FieldBusNode fieldBusNode = (FieldBusNode) editObject;
                fieldBusNodeEditControl1.FieldBusNodeId = fieldBusNode.Id;
            }
            else if (editObject is PhysicalChannel)
            {
                ActiveLevel = Level.PChannels;
                PhysicalChannel physicalChannel = (PhysicalChannel) editObject;
                physicalChannelEditControl1.Id = physicalChannel.Id;
            }
            else if (editObject is LogicalChannel)
            {
                ActiveLevel = Level.LChannels;
                LogicalChannel logicalChannel = (LogicalChannel) editObject;
                logicalChannelEditControl1.Id = logicalChannel.Id;
            }

        }

        private int GetFieldBusNodeFilter(FieldBusNode fieldBusNode)
        {
            int filter = 0;

            if (fieldBusNode != null && fieldBusNode.FieldBusNodeAddress != null)
                filter = fieldBusNode.FieldBusNodeAddress.SlaveAddress;
            
            return filter;
        }

        private int GetFieldBusFilter(FieldBusManager fieldBusManager)
        {
            //подфильтрация по выбранной шине
            int filter = 0;
            switch (fieldBusManager.FieldBusType)
            {
                case FieldBusType.Unknown:
                    break;
                case FieldBusType.RS232:
                    break;
                case FieldBusType.RS485:
                    filter = 1;//todo: фильтрацию!!!
                    break;
                case FieldBusType.Ethernet:
                    filter = 2;//todo: фильтрацию!!!
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return filter;
        }

        ///<summary>
        ///
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDbEditor ActiveEditor
        {
            get
            {
                IDbEditor dbEditor = null;

                switch (ActiveLevel)
                {
                    case Level.FieldBuses:
                        dbEditor = fieldBusEditControl1;
                        break;
                    case Level.FieldBusNodes:
                        dbEditor = fieldBusNodeEditControl1;
                        break;
                    case Level.PChannels:
                        dbEditor = physicalChannelEditControl1;
                        break;
                    case Level.LChannels:
                        dbEditor = logicalChannelEditControl1;
                        break;
                        //default:
                        //    throw new ArgumentOutOfRangeException();
                }

                return dbEditor;
            }
        }

        #region IDbEditor

        ///<summary>
        /// Заполнить
        ///</summary>
        ///<param name="editValue"></param>
        public void Fill(object editValue)
        {
            IDbEditor dbEditor = ActiveEditor;
            if (dbEditor != null)
                dbEditor.Fill(editValue);
        }

        ///<summary>
        /// Сохранить
        ///</summary>
        public void Save()
        {
            IDbEditor dbEditor = ActiveEditor;
            if (dbEditor != null)
                dbEditor.Save();
        }

        #endregion
        private void btnFill_Click(object sender, EventArgs e)
        {
            Fill(null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}