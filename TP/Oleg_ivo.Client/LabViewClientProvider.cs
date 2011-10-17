#if IIST
#if WSHTTP_BINDING
using Oleg_ivo.HighLevelClient.ServiceReferenceIISTwsDualHttp;
#else
#if TCP_BINDING
using Oleg_ivo.HighLevelClient.ServiceReferenceHomeTcp;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
#else
using Oleg_ivo.HighLevelClient.ServiceReferenceIIST;
#endif
#endif
#else
#if WSHTTP_BINDING
using Oleg_ivo.HighLevelClient.ServiceReferenceHomeWsDualHttp;
#else
using Oleg_ivo.HighLevelClient.ServiceReferenceHome;
#endif
#endif

namespace Oleg_ivo.HighLevelClient
{
    /// <summary>
    /// 
    /// </summary>
    public class LabViewClientProvider : ClientProvider
    {
        #region Singleton

        private static LabViewClientProvider _instance;

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public new static LabViewClientProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LabViewClientProvider();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LabViewClientProvider" />.
        /// </summary>
        protected LabViewClientProvider()
        {
        }

        static LabViewClientProvider()
        {
            Binding = null;
            RemoteAddress = null;
        }

        #endregion

        /// <summary>
        /// Создать Proxy
        /// </summary>
        /// <remarks>
        /// По умолчанию параметры для инициализации канала берутся из App.Config. 
        /// В перегрузке можно использовать инициализацию дополнительными параметрами
        /// </remarks>
        protected override HighLevelMessageExchangeSystemClient CreateProxy(InstanceContext instanceContext)
        {
            return new HighLevelMessageExchangeSystemClient(instanceContext, Binding, RemoteAddress);
        }

        /// <summary>
        /// 
        /// </summary>
        private static Binding Binding { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public static void InitBinding(BindingType bindingType, bool transportSecurity)
        {
            switch (bindingType)
            {
                case BindingType.NetNamePype:
                    Binding =
                        new NetNamedPipeBinding(transportSecurity
                                                    ? NetNamedPipeSecurityMode.Transport
                                                    : NetNamedPipeSecurityMode.None);
                    break;

                case BindingType.WSDualHttpBinding:
                    Binding =
                        new WSDualHttpBinding(transportSecurity
                                                    ? WSDualHttpSecurityMode.Message
                                                    : WSDualHttpSecurityMode.None);

                    break;
                default:
                    throw new ArgumentOutOfRangeException("bindingType", bindingType, "Неподдерживаемый тип привязки");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static EndpointAddress RemoteAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public static void InitRemoteAddress(string uri)
        {
            RemoteAddress = new EndpointAddress(uri);
        }

        /// <summary>
        /// 
        /// </summary>
        public enum BindingType
        {
            /// <summary>
            /// 
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// 
            /// </summary>
            NetNamePype = 1,
            
            /// <summary>
            /// 
            /// </summary>
            WSDualHttpBinding = 2
        }
    }
}