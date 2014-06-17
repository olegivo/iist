using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Autofac;
using DMS.Common.MessageExchangeSystem;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.MES.Services;
using Oleg_ivo.Plc.Entities;
using Oleg_ivo.Tools.ConnectionProvider;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// ������������������ ������
    /// </summary>
    public abstract class RegisteredClient<TClientCallback> : IRegisteredChannelsHolder where TClientCallback : IClientCallback
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private PlcDataContext dataContext;

        #region fields

        #endregion

        #region constructors
        /// <summary>
        /// 
        /// </summary>
        protected RegisteredClient()
        {
            Callbacks = new List<TClientCallback>();
            RegisteredLogicalChannels = new Dictionary<int, RegisteredLogicalChannelExtended>();
        }

        #endregion

        #region properties
        /// <summary>
        /// �������� ������
        /// </summary>
        protected List<TClientCallback> Callbacks { get; private set; }

        /// <summary>
        /// ���� �������� ������
        /// </summary>
        public bool HasCallbacks
        {
            get { return Callbacks.Count > 0; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RegName { get; set; }

        /// <summary>
        /// ������������������ ���������� ������
        /// </summary>
        public Dictionary<int, RegisteredLogicalChannelExtended> RegisteredLogicalChannels { get; private set; }

        /// <summary>
        /// ���� ������������������ ���������� ������
        /// </summary>
        public bool HasRegisteredLogicalChannels
        {
            get { return RegisteredLogicalChannels.Count > 0; }
        }

        [Dependency(Required = true)]
        public InternalMessageLogger InternalMessageLogger { get; set; }

        [Dependency(Required = true)]
        public IComponentContext Context { get; set; }

        public PlcDataContext DataContext { get
        {
            return dataContext ?? (dataContext = Context.Resolve<PlcDataContext>(new TypedParameter(typeof (string),
                                                                      Context
                                                                          .Resolve
                                                                          <DbConnectionProvider>()
                                                                          .DefaultConnectionString)));
        } }

        #endregion

        #region methods
        /// <summary>
        /// �������� �������� ����� �������
        /// </summary>
        /// <param name="callback"></param>
        public void AddCallback(TClientCallback callback)
        {
            lock (Callbacks)
                Callbacks.Add(callback);
        }

        /// <summary>
        /// ������� �������� ����� �������
        /// </summary>
        /// <param name="callback"></param>
        public void RemoveCallback(TClientCallback callback)
        {
            lock (Callbacks)
                Callbacks.Remove(callback);
        }

        /// <summary>
        /// �������� ��������� �������
        /// </summary>
        /// <param name="message"></param>
        public void SendMessageToClient(InternalMessage message)
        {
            try
            {
                IterateCallbacks(c=> c.SendMessageToClient(message));
            }
            catch (FaultException<InvalidOperationException> ex)
            {
                throw;
            }
            catch (FaultException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                log.ErrorException("������ ��� �������� ������������� �������� �������: {0}", ex);
                throw;
            }
        }

        #endregion

        /// <summary>
        /// ����� ������������������� ����������� ������ ��������������� � ������� (�� ������ �� ��� �����)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public RegisteredLogicalChannelExtended GetRegisteredLogicalChannel(Func<RegisteredLogicalChannelExtended, bool> predicate)
        {
            return RegisteredLogicalChannels.Values.FirstOrDefault(predicate);
        }

        /// <summary>
        /// �������� ����� � ������������������ ��� �������
        /// </summary>
        /// <param name="registeredLogicalChannel"></param>
        protected void AddRegisteredChannel(RegisteredLogicalChannelExtended registeredLogicalChannel)
        {
            RegisteredLogicalChannels.Add(registeredLogicalChannel.Id, registeredLogicalChannel);
        }

        /// <summary>
        /// ������� ����� �� ������������������ ��� �������
        /// </summary>
        /// <param name="registeredLogicalChannel"></param>
        protected void RemoveRegisteredChannel(RegisteredLogicalChannelExtended registeredLogicalChannel)
        {
            RegisteredLogicalChannels.Remove(registeredLogicalChannel.Id);
        }

        protected void IterateCallbacks(Action<TClientCallback> callbackAction)
        {
            lock (Callbacks)
                foreach (var callback in Callbacks.ToList())
                    try
                    {
                        callbackAction(callback);
                    }
                    catch (CommunicationObjectFaultedException ex)
                    {
                        var error =
                            string.Format(
                                "�������� ����� � �������� [{0}] �������. ������� �������� ����� � ��� ��������� ����������� �������.",
                                RegName);
                        log.ErrorException(error, ex);
                        Callbacks.Remove(callback);
                    }
        }
    }
}