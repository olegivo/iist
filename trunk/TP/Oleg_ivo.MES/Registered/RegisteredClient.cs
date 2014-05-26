using System;
using System.Collections.Generic;
using System.Linq;
using DMS.Common.MessageExchangeSystem;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.MES.Logging;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// ������������������ ������
    /// </summary>
    public abstract class RegisteredClient<TClientCallback> : IRegisteredChannelsHolder where TClientCallback : IClientCallback
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

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
        public string Ticker { get; set; }

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
            lock (Callbacks)
                foreach (TClientCallback callback in Callbacks)
                    try
                    {
//                            double d = 100.00 + p.NextDouble() * 10;
//                            Instance.OnUpdate(d);
                        callback.SendMessageToClient(message);
                    }
                    catch (System.ServiceModel.FaultException<InvalidOperationException> ex)
                    {
                        throw;
                    }
                    catch (System.ServiceModel.FaultException ex)
                    {
                        throw;
                    }
                    catch (System.ServiceModel.CommunicationException ex)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        log.ErrorException("������ ��� �������� ������������� �������� �������: {0}",
                                          ex);
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
    }
}