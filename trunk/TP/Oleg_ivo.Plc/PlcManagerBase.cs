using System.ComponentModel;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.Ports;

namespace Oleg_ivo.Plc
{
    ///<summary>
    /// Диспетчер всех ПЛК, включенных в конфигурацию системы (на всех портах)
    ///</summary>
    public class PlcManagerBase : Component
    {
        #region fields

        private readonly IFieldBusNodeFactory _fieldBusNodesFactory;
        private readonly IFieldBusFactory _fieldBusFactory;
        private readonly IPlcFactory _plcFactory;
        private readonly IPhysicalChannelsFactory _physicalChannelsFactory;
        private readonly ILogicalChannelsFactory _logicalChannelsFactory;

        #endregion

        #region properties

        ///<summary>
        /// Диспетчеры полевых шин
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(true)]
        public FieldBusManagerCollection FieldBusManagers { get; private set; }

        ///<summary>
        ///
        ///</summary>
        public LogicalChannelCollection LogicalChannels
        {
            get
            {
                LogicalChannelCollection collection = new LogicalChannelCollection();
                
                if (FieldBusManagers != null)
                    foreach (FieldBusManager fieldBusManager in FieldBusManagers)
                        collection.AddRange(fieldBusManager.LogicalChannels);

                return collection;
            }
        }

        ///<summary>
        /// Фабрика узлов полевой шины
        ///</summary>
        ///<returns></returns>
        public IFieldBusNodeFactory FieldBusNodesFactory
        {
            get { return _fieldBusNodesFactory; }
        }

        ///<summary>
        /// Фабрика ПЛК
        ///</summary>
        ///<returns></returns>
        private IPlcFactory PlcFactory
        {
            get { return _plcFactory; }
        }

        ///<summary>
        /// Фабрика полевых шин
        ///</summary>
        ///<returns></returns>
        public IFieldBusFactory FieldBusFactory
        {
            get { return _fieldBusFactory; }
        }

        /// <summary>
        /// Фабрика физических каналов
        /// </summary>
        public IPhysicalChannelsFactory PhysicalChannelsFactory
        {
            get { return _physicalChannelsFactory; }
        }

        /// <summary>
        /// Фабрика логических каналов
        /// </summary>
        public ILogicalChannelsFactory LogicalChannelsFactory
        {
            get { return _logicalChannelsFactory; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"></see> class.
        /// </summary>
        public PlcManagerBase()
        {
            FieldBusManagers = new FieldBusManagerCollection();

            _fieldBusNodesFactory = GetFieldBusNodeFactory();
            _plcFactory = GetPlcFactory();
            if (FieldBusNodesFactory!=null)
            {
                //фабрика узлов должна знать, какие ПЛК ей строить
                FieldBusNodesFactory.PlcFactory = PlcFactory;
            }
            _fieldBusFactory = GetFieldBusFactory();
            _physicalChannelsFactory = GetPhysicalChannelsFactory();
            _logicalChannelsFactory = GetLogicalChannelsFactory();
        }

/*
        ///<summary>
        ///
        ///</summary>
        protected virtual void InitFactories()
        {
        }
*/

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        private /*virtual*/ IFieldBusFactory GetFieldBusFactory()
        {
            return FieldBus.FieldBusFactory.Instance;
        }

        ///<summary>
        ///
        ///</summary>
        protected virtual IPlcFactory GetPlcFactory()
        {
            return null;
        }

        ///<summary>
        ///
        ///</summary>
        protected virtual IFieldBusNodeFactory GetFieldBusNodeFactory()
        {
            return null;
        }

        ///<summary>
        ///
        ///</summary>
        protected virtual IPhysicalChannelsFactory GetPhysicalChannelsFactory()
        {
            return null;
        }

        ///<summary>
        ///
        ///</summary>
        protected virtual ILogicalChannelsFactory GetLogicalChannelsFactory()
        {
            return null;
        }

        #endregion


        #region methods

        ///<summary>
        /// Построить конфигурацию полевых шин системы
        ///</summary>
        ///<param name="cleanBeforeBuild">Очищать перед построением</param>
        ///<param name="fieldBusType">Тип полевой шины</param>
        public void BuildFieldBuses(bool cleanBeforeBuild, FieldBusType fieldBusType)
        {
            object[] ports = FieldBusFactory.FindPorts(fieldBusType);
            
            if (cleanBeforeBuild) FieldBusManagers = new FieldBusManagerCollection();

            if (ports!=null)
            {
                // добавление диспетчеров полевых шин:
                foreach (object port in ports)
                {
                    FieldBusPortParameters fieldBusPortParameters = FieldBusFactory.CreatePortParameters(fieldBusType, port);
                    if (fieldBusPortParameters is SerialPortParameters)
                    {
                        ((SerialPortParameters) fieldBusPortParameters).Port = port;
                    }

                    IFieldBusAccessor fieldBusAccessor = FieldBusFactory.CreateFieldbusAccessor(fieldBusPortParameters.FieldBusType, port);
                    
                    if (fieldBusAccessor!=null)
                    {
                        FieldBusManager fieldBusManager = FieldBusFactory.CreateFieldBusManager(fieldBusAccessor);

                        fieldBusManager.BuildFieldBusNodes(FieldBusNodesFactory);
                        
                        FieldBusManagers.Add(fieldBusManager);
                    }
                }
            }
        }

        #endregion


        #region Фабрики по умолчанию

        #endregion

    }
}