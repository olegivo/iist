using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Autofac;
using Autofac.Core.Activators.Reflection;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Entities;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.Ports;
using Oleg_ivo.Tools.ConnectionProvider;
using FieldBusType = Oleg_ivo.Plc.FieldBus.FieldBusType;

namespace Oleg_ivo.Plc
{
    ///<summary>
    /// Диспетчер всех ПЛК, включенных в конфигурацию системы (на всех портах)
    ///</summary>
    public class PlcManager : Component, IPlcManager
    {
        #region fields

        protected IDistributedMeasurementInformationSystem DMIS;
        protected IComponentContext Context { get; private set; }

        #endregion

        #region properties

        ///<summary>
        /// Диспетчеры полевых шин
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(true)]
        public List<FieldBusManager> FieldBusManagers { get; private set; }

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
        public IFieldBusNodeFactory FieldBusNodesFactory { get; private set; }

        ///<summary>
        /// Фабрика ПЛК
        ///</summary>
        ///<returns></returns>
        private IPlcFactory PlcFactory { get; set; }

        ///<summary>
        /// Фабрика полевых шин
        ///</summary>
        ///<returns></returns>
        public IFieldBusFactory FieldBusFactory { get; private set; }

        /// <summary>
        /// Фабрика физических каналов
        /// </summary>
        public IPhysicalChannelsFactory PhysicalChannelsFactory { get; private set; }

        /// <summary>
        /// Фабрика логических каналов
        /// </summary>
        public ILogicalChannelsFactory LogicalChannelsFactory { get; private set; }

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"></see> class.
        /// </summary>
        /// <param name="context"></param>
        public PlcManager(IComponentContext context)
        {
            Context = Enforce.ArgumentNotNull(context, "context");
            DMIS = context.Resolve<IDistributedMeasurementInformationSystem>();

            FieldBusManagers = new List<FieldBusManager>();

            FieldBusNodesFactory = Context.Resolve<IFieldBusNodeFactory>();
            PlcFactory = Context.Resolve<IPlcFactory>();
            FieldBusFactory = Context.Resolve<IFieldBusFactory>();
            PhysicalChannelsFactory = Context.Resolve<IPhysicalChannelsFactory>();
            LogicalChannelsFactory = Context.Resolve<ILogicalChannelsFactory>();
        }

/*
        ///<summary>
        ///
        ///</summary>
        protected virtual void InitFactories()
        {
        }
*/

        #endregion


        #region methods

        ///<summary>
        /// Построить конфигурацию полевых шин системы
        ///</summary>
        ///<param name="cleanBeforeBuild">Очищать перед построением</param>
        ///<param name="fieldBusType">Тип полевой шины</param>
        public void BuildFieldBuses(bool cleanBeforeBuild, FieldBusType fieldBusType)
        {
            var dataContext = Context.ResolveUnregistered<PlcDataContext>(new TypedParameter(typeof(IDbConnection), DbConnectionProvider.Instance.GetConnection()));
            object[] ports = FieldBusFactory.FindPorts(fieldBusType);
            
            if (cleanBeforeBuild) FieldBusManagers = new List<FieldBusManager>();

            if (ports!=null)
            {
                // добавление диспетчеров полевых шин:
                foreach (object port in ports)
                {
                    FieldBusPortParameters fieldBusPortParameters = FieldBusFactory.CreatePortParameters(fieldBusType, port);
                    var serialPortParameters = fieldBusPortParameters as SerialPortParameters;
                    if (serialPortParameters != null)
                    {
                        serialPortParameters.Port = port;
                    }

                    var fieldBusAccessor = FieldBusFactory.CreateFieldbusAccessor(fieldBusPortParameters.FieldBusType, port);
                    if (fieldBusAccessor!=null)
                    {
                        var fieldBus = dataContext.FieldBus.FirstOrDefault(fb => fb.FieldBusTypeId.Value == (int)fieldBusType);//TODO: FirstOrDefault
                        var fieldBusManager = FieldBusFactory.CreateFieldBusManager(fieldBusAccessor, fieldBus);
                        fieldBusManager.BuildFieldBusNodes(FieldBusNodesFactory);
                        FieldBusManagers.Add(fieldBusManager);
                    }
                }
            }
        }

        private PlcDataContext dataContext;

        protected PlcDataContext DataContext
        {
            get
            {
                return dataContext ??
                       (dataContext =
                           Context.Resolve<PlcDataContext>(new TypedParameter(typeof(string),
                               Context.Resolve<DbConnectionProvider>().DefaultConnectionString)));
            }
        }

        public void Save()
        {
            DataContext.SubmitChanges();
        }

        #endregion


        #region Фабрики по умолчанию

        #endregion

    }
}