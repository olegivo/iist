using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Oleg_ivo.LowLevelClient
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CallbackErrorHandlerBehaviorAttribute : Attribute, IEndpointBehavior
    {
        /// <summary>
        /// ���������� ������
        /// </summary>
        private readonly IErrorHandler _errorHandler;

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="typeErrorHandler">��� ����������� ������</param>
        public CallbackErrorHandlerBehaviorAttribute(Type typeErrorHandler)
        {
            if (typeErrorHandler == null)
                throw new ArgumentNullException();

            _errorHandler = (IErrorHandler)Activator.CreateInstance(typeErrorHandler);
        }

        #region IEndpointBehavior Members

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // ����� ����� ������������ ������ � ����������� callback-������
            clientRuntime.CallbackDispatchRuntime.ChannelDispatcher.ErrorHandlers.Add(_errorHandler);
        }

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        { }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        { }

        #endregion
    }
}