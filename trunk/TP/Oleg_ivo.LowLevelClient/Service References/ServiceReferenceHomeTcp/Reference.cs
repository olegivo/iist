﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5448
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Oleg_ivo.LowLevelClient.ServiceReferenceHomeTcp {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceHomeTcp.ILowLevelMessageExchangeSystem", CallbackContract=typeof(Oleg_ivo.LowLevelClient.ServiceReferenceHomeTcp.ILowLevelMessageExchangeSystemCallback))]
    public interface ILowLevelMessageExchangeSystem {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageReceiver/SendMessage", ReplyAction="http://tempuri.org/IMessageReceiver/SendMessageResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.InternalServiceMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.InternalErrorMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.RegistrationMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.ChannelRegistrationMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.ChannelSubscribeMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.InternalDataMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.InternalLogicalChannelDataMessage))]
        void SendMessage(DMS.Common.Messages.InternalMessage message);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IMessageReceiver/SendMessage", ReplyAction="http://tempuri.org/IMessageReceiver/SendMessageResponse")]
        System.IAsyncResult BeginSendMessage(DMS.Common.Messages.InternalMessage message, System.AsyncCallback callback, object asyncState);
        
        void EndSendMessage(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageReceiver/SendError", ReplyAction="http://tempuri.org/IMessageReceiver/SendErrorResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(DMS.Common.Exceptions.TestException), Action="http://tempuri.org/IMessageReceiver/SendErrorTestExceptionFault", Name="TestException", Namespace="http://schemas.datacontract.org/2004/07/DMS.Common.Exceptions")]
        [System.ServiceModel.FaultContractAttribute(typeof(DMS.Common.Exceptions.RegistrationException), Action="http://tempuri.org/IMessageReceiver/SendErrorRegistrationExceptionFault", Name="RegistrationException", Namespace="http://schemas.datacontract.org/2004/07/DMS.Common.Exceptions")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.ApplicationException))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Exceptions.InternalException))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Exceptions.TestException))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Exceptions.InternalServiceException))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Exceptions.RegistrationException))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.SystemException))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.ArgumentException))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.ArgumentOutOfRangeException))]
        void SendError(DMS.Common.Messages.InternalErrorMessage message);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IMessageReceiver/SendError", ReplyAction="http://tempuri.org/IMessageReceiver/SendErrorResponse")]
        System.IAsyncResult BeginSendError(DMS.Common.Messages.InternalErrorMessage message, System.AsyncCallback callback, object asyncState);
        
        void EndSendError(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageExchangeSystem/Register", ReplyAction="http://tempuri.org/IMessageExchangeSystem/RegisterResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(DMS.Common.Exceptions.RegistrationException), Action="http://tempuri.org/IMessageExchangeSystem/RegisterRegistrationExceptionFault", Name="RegistrationException", Namespace="http://schemas.datacontract.org/2004/07/DMS.Common.Exceptions")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.ChannelRegistrationMessage))]
        void Register(DMS.Common.Messages.RegistrationMessage message);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IMessageExchangeSystem/Register", ReplyAction="http://tempuri.org/IMessageExchangeSystem/RegisterResponse")]
        System.IAsyncResult BeginRegister(DMS.Common.Messages.RegistrationMessage message, System.AsyncCallback callback, object asyncState);
        
        void EndRegister(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageExchangeSystem/Unregister", ReplyAction="http://tempuri.org/IMessageExchangeSystem/UnregisterResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(DMS.Common.Exceptions.RegistrationException), Action="http://tempuri.org/IMessageExchangeSystem/UnregisterRegistrationExceptionFault", Name="RegistrationException", Namespace="http://schemas.datacontract.org/2004/07/DMS.Common.Exceptions")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.ChannelRegistrationMessage))]
        void Unregister(DMS.Common.Messages.RegistrationMessage message);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IMessageExchangeSystem/Unregister", ReplyAction="http://tempuri.org/IMessageExchangeSystem/UnregisterResponse")]
        System.IAsyncResult BeginUnregister(DMS.Common.Messages.RegistrationMessage message, System.AsyncCallback callback, object asyncState);
        
        void EndUnregister(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelRegister", ReplyAction="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelRegisterResponse")]
        void ChannelRegister(DMS.Common.Messages.ChannelRegistrationMessage message);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelRegister", ReplyAction="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelRegisterResponse")]
        System.IAsyncResult BeginChannelRegister(DMS.Common.Messages.ChannelRegistrationMessage message, System.AsyncCallback callback, object asyncState);
        
        void EndChannelRegister(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelUnRegister", ReplyAction="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelUnRegisterResponse")]
        void ChannelUnRegister(DMS.Common.Messages.ChannelRegistrationMessage message);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelUnRegister", ReplyAction="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelUnRegisterResponse")]
        System.IAsyncResult BeginChannelUnRegister(DMS.Common.Messages.ChannelRegistrationMessage message, System.AsyncCallback callback, object asyncState);
        
        void EndChannelUnRegister(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevelMessageExchangeSystem/ReadChannel", ReplyAction="http://tempuri.org/ILowLevelMessageExchangeSystem/ReadChannelResponse")]
        void ReadChannel(DMS.Common.Messages.InternalLogicalChannelDataMessage message);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/ILowLevelMessageExchangeSystem/ReadChannel", ReplyAction="http://tempuri.org/ILowLevelMessageExchangeSystem/ReadChannelResponse")]
        System.IAsyncResult BeginReadChannel(DMS.Common.Messages.InternalLogicalChannelDataMessage message, System.AsyncCallback callback, object asyncState);
        
        void EndReadChannel(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ILowLevelMessageExchangeSystemCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageReceiver/SendMessageToClient", ReplyAction="http://tempuri.org/IMessageReceiver/SendMessageToClientResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(DMS.Common.Exceptions.InternalException), Action="http://tempuri.org/IMessageReceiver/SendMessageToClientInternalExceptionFault", Name="InternalException", Namespace="http://schemas.datacontract.org/2004/07/DMS.Common.Exceptions")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.InternalServiceMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.InternalErrorMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.RegistrationMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.ChannelRegistrationMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.ChannelSubscribeMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.InternalDataMessage))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DMS.Common.Messages.InternalLogicalChannelDataMessage))]
        void SendMessageToClient(DMS.Common.Messages.InternalMessage message);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IMessageReceiver/SendMessageToClient", ReplyAction="http://tempuri.org/IMessageReceiver/SendMessageToClientResponse")]
        System.IAsyncResult BeginSendMessageToClient(DMS.Common.Messages.InternalMessage message, System.AsyncCallback callback, object asyncState);
        
        void EndSendMessageToClient(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelSubscribe")]
        void ChannelSubscribe(DMS.Common.Messages.ChannelSubscribeMessage message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelSubscribe")]
        System.IAsyncResult BeginChannelSubscribe(DMS.Common.Messages.ChannelSubscribeMessage message, System.AsyncCallback callback, object asyncState);
        
        void EndChannelSubscribe(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelUnSubscribe")]
        void ChannelUnSubscribe(DMS.Common.Messages.ChannelSubscribeMessage message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="http://tempuri.org/ILowLevelMessageExchangeSystem/ChannelUnSubscribe")]
        System.IAsyncResult BeginChannelUnSubscribe(DMS.Common.Messages.ChannelSubscribeMessage message, System.AsyncCallback callback, object asyncState);
        
        void EndChannelUnSubscribe(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILowLevelMessageExchangeSystem/SendWriteToClient")]
        void SendWriteToClient(DMS.Common.Messages.InternalLogicalChannelDataMessage message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="http://tempuri.org/ILowLevelMessageExchangeSystem/SendWriteToClient")]
        System.IAsyncResult BeginSendWriteToClient(DMS.Common.Messages.InternalLogicalChannelDataMessage message, System.AsyncCallback callback, object asyncState);
        
        void EndSendWriteToClient(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ILowLevelMessageExchangeSystemChannel : Oleg_ivo.LowLevelClient.ServiceReferenceHomeTcp.ILowLevelMessageExchangeSystem, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class LowLevelMessageExchangeSystemClient : System.ServiceModel.DuplexClientBase<Oleg_ivo.LowLevelClient.ServiceReferenceHomeTcp.ILowLevelMessageExchangeSystem>, Oleg_ivo.LowLevelClient.ServiceReferenceHomeTcp.ILowLevelMessageExchangeSystem {
        
        private BeginOperationDelegate onBeginSendMessageDelegate;
        
        private EndOperationDelegate onEndSendMessageDelegate;
        
        private System.Threading.SendOrPostCallback onSendMessageCompletedDelegate;
        
        private BeginOperationDelegate onBeginSendErrorDelegate;
        
        private EndOperationDelegate onEndSendErrorDelegate;
        
        private System.Threading.SendOrPostCallback onSendErrorCompletedDelegate;
        
        private BeginOperationDelegate onBeginRegisterDelegate;
        
        private EndOperationDelegate onEndRegisterDelegate;
        
        private System.Threading.SendOrPostCallback onRegisterCompletedDelegate;
        
        private BeginOperationDelegate onBeginUnregisterDelegate;
        
        private EndOperationDelegate onEndUnregisterDelegate;
        
        private System.Threading.SendOrPostCallback onUnregisterCompletedDelegate;
        
        private BeginOperationDelegate onBeginChannelRegisterDelegate;
        
        private EndOperationDelegate onEndChannelRegisterDelegate;
        
        private System.Threading.SendOrPostCallback onChannelRegisterCompletedDelegate;
        
        private BeginOperationDelegate onBeginChannelUnRegisterDelegate;
        
        private EndOperationDelegate onEndChannelUnRegisterDelegate;
        
        private System.Threading.SendOrPostCallback onChannelUnRegisterCompletedDelegate;
        
        private BeginOperationDelegate onBeginReadChannelDelegate;
        
        private EndOperationDelegate onEndReadChannelDelegate;
        
        private System.Threading.SendOrPostCallback onReadChannelCompletedDelegate;
        
        public LowLevelMessageExchangeSystemClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public LowLevelMessageExchangeSystemClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public LowLevelMessageExchangeSystemClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LowLevelMessageExchangeSystemClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LowLevelMessageExchangeSystemClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> SendMessageCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> SendErrorCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> RegisterCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> UnregisterCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> ChannelRegisterCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> ChannelUnRegisterCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> ReadChannelCompleted;
        
        public void SendMessage(DMS.Common.Messages.InternalMessage message) {
            base.Channel.SendMessage(message);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginSendMessage(DMS.Common.Messages.InternalMessage message, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSendMessage(message, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndSendMessage(System.IAsyncResult result) {
            base.Channel.EndSendMessage(result);
        }
        
        private System.IAsyncResult OnBeginSendMessage(object[] inValues, System.AsyncCallback callback, object asyncState) {
            DMS.Common.Messages.InternalMessage message = ((DMS.Common.Messages.InternalMessage)(inValues[0]));
            return this.BeginSendMessage(message, callback, asyncState);
        }
        
        private object[] OnEndSendMessage(System.IAsyncResult result) {
            this.EndSendMessage(result);
            return null;
        }
        
        private void OnSendMessageCompleted(object state) {
            if ((this.SendMessageCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SendMessageCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SendMessageAsync(DMS.Common.Messages.InternalMessage message) {
            this.SendMessageAsync(message, null);
        }
        
        public void SendMessageAsync(DMS.Common.Messages.InternalMessage message, object userState) {
            if ((this.onBeginSendMessageDelegate == null)) {
                this.onBeginSendMessageDelegate = new BeginOperationDelegate(this.OnBeginSendMessage);
            }
            if ((this.onEndSendMessageDelegate == null)) {
                this.onEndSendMessageDelegate = new EndOperationDelegate(this.OnEndSendMessage);
            }
            if ((this.onSendMessageCompletedDelegate == null)) {
                this.onSendMessageCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSendMessageCompleted);
            }
            base.InvokeAsync(this.onBeginSendMessageDelegate, new object[] {
                        message}, this.onEndSendMessageDelegate, this.onSendMessageCompletedDelegate, userState);
        }
        
        public void SendError(DMS.Common.Messages.InternalErrorMessage message) {
            base.Channel.SendError(message);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginSendError(DMS.Common.Messages.InternalErrorMessage message, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSendError(message, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndSendError(System.IAsyncResult result) {
            base.Channel.EndSendError(result);
        }
        
        private System.IAsyncResult OnBeginSendError(object[] inValues, System.AsyncCallback callback, object asyncState) {
            DMS.Common.Messages.InternalErrorMessage message = ((DMS.Common.Messages.InternalErrorMessage)(inValues[0]));
            return this.BeginSendError(message, callback, asyncState);
        }
        
        private object[] OnEndSendError(System.IAsyncResult result) {
            this.EndSendError(result);
            return null;
        }
        
        private void OnSendErrorCompleted(object state) {
            if ((this.SendErrorCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SendErrorCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SendErrorAsync(DMS.Common.Messages.InternalErrorMessage message) {
            this.SendErrorAsync(message, null);
        }
        
        public void SendErrorAsync(DMS.Common.Messages.InternalErrorMessage message, object userState) {
            if ((this.onBeginSendErrorDelegate == null)) {
                this.onBeginSendErrorDelegate = new BeginOperationDelegate(this.OnBeginSendError);
            }
            if ((this.onEndSendErrorDelegate == null)) {
                this.onEndSendErrorDelegate = new EndOperationDelegate(this.OnEndSendError);
            }
            if ((this.onSendErrorCompletedDelegate == null)) {
                this.onSendErrorCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSendErrorCompleted);
            }
            base.InvokeAsync(this.onBeginSendErrorDelegate, new object[] {
                        message}, this.onEndSendErrorDelegate, this.onSendErrorCompletedDelegate, userState);
        }
        
        public void Register(DMS.Common.Messages.RegistrationMessage message) {
            base.Channel.Register(message);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginRegister(DMS.Common.Messages.RegistrationMessage message, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginRegister(message, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndRegister(System.IAsyncResult result) {
            base.Channel.EndRegister(result);
        }
        
        private System.IAsyncResult OnBeginRegister(object[] inValues, System.AsyncCallback callback, object asyncState) {
            DMS.Common.Messages.RegistrationMessage message = ((DMS.Common.Messages.RegistrationMessage)(inValues[0]));
            return this.BeginRegister(message, callback, asyncState);
        }
        
        private object[] OnEndRegister(System.IAsyncResult result) {
            this.EndRegister(result);
            return null;
        }
        
        private void OnRegisterCompleted(object state) {
            if ((this.RegisterCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.RegisterCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void RegisterAsync(DMS.Common.Messages.RegistrationMessage message) {
            this.RegisterAsync(message, null);
        }
        
        public void RegisterAsync(DMS.Common.Messages.RegistrationMessage message, object userState) {
            if ((this.onBeginRegisterDelegate == null)) {
                this.onBeginRegisterDelegate = new BeginOperationDelegate(this.OnBeginRegister);
            }
            if ((this.onEndRegisterDelegate == null)) {
                this.onEndRegisterDelegate = new EndOperationDelegate(this.OnEndRegister);
            }
            if ((this.onRegisterCompletedDelegate == null)) {
                this.onRegisterCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnRegisterCompleted);
            }
            base.InvokeAsync(this.onBeginRegisterDelegate, new object[] {
                        message}, this.onEndRegisterDelegate, this.onRegisterCompletedDelegate, userState);
        }
        
        public void Unregister(DMS.Common.Messages.RegistrationMessage message) {
            base.Channel.Unregister(message);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginUnregister(DMS.Common.Messages.RegistrationMessage message, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginUnregister(message, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndUnregister(System.IAsyncResult result) {
            base.Channel.EndUnregister(result);
        }
        
        private System.IAsyncResult OnBeginUnregister(object[] inValues, System.AsyncCallback callback, object asyncState) {
            DMS.Common.Messages.RegistrationMessage message = ((DMS.Common.Messages.RegistrationMessage)(inValues[0]));
            return this.BeginUnregister(message, callback, asyncState);
        }
        
        private object[] OnEndUnregister(System.IAsyncResult result) {
            this.EndUnregister(result);
            return null;
        }
        
        private void OnUnregisterCompleted(object state) {
            if ((this.UnregisterCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.UnregisterCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void UnregisterAsync(DMS.Common.Messages.RegistrationMessage message) {
            this.UnregisterAsync(message, null);
        }
        
        public void UnregisterAsync(DMS.Common.Messages.RegistrationMessage message, object userState) {
            if ((this.onBeginUnregisterDelegate == null)) {
                this.onBeginUnregisterDelegate = new BeginOperationDelegate(this.OnBeginUnregister);
            }
            if ((this.onEndUnregisterDelegate == null)) {
                this.onEndUnregisterDelegate = new EndOperationDelegate(this.OnEndUnregister);
            }
            if ((this.onUnregisterCompletedDelegate == null)) {
                this.onUnregisterCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnUnregisterCompleted);
            }
            base.InvokeAsync(this.onBeginUnregisterDelegate, new object[] {
                        message}, this.onEndUnregisterDelegate, this.onUnregisterCompletedDelegate, userState);
        }
        
        public void ChannelRegister(DMS.Common.Messages.ChannelRegistrationMessage message) {
            base.Channel.ChannelRegister(message);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginChannelRegister(DMS.Common.Messages.ChannelRegistrationMessage message, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginChannelRegister(message, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndChannelRegister(System.IAsyncResult result) {
            base.Channel.EndChannelRegister(result);
        }
        
        private System.IAsyncResult OnBeginChannelRegister(object[] inValues, System.AsyncCallback callback, object asyncState) {
            DMS.Common.Messages.ChannelRegistrationMessage message = ((DMS.Common.Messages.ChannelRegistrationMessage)(inValues[0]));
            return this.BeginChannelRegister(message, callback, asyncState);
        }
        
        private object[] OnEndChannelRegister(System.IAsyncResult result) {
            this.EndChannelRegister(result);
            return null;
        }
        
        private void OnChannelRegisterCompleted(object state) {
            if ((this.ChannelRegisterCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ChannelRegisterCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ChannelRegisterAsync(DMS.Common.Messages.ChannelRegistrationMessage message) {
            this.ChannelRegisterAsync(message, null);
        }
        
        public void ChannelRegisterAsync(DMS.Common.Messages.ChannelRegistrationMessage message, object userState) {
            if ((this.onBeginChannelRegisterDelegate == null)) {
                this.onBeginChannelRegisterDelegate = new BeginOperationDelegate(this.OnBeginChannelRegister);
            }
            if ((this.onEndChannelRegisterDelegate == null)) {
                this.onEndChannelRegisterDelegate = new EndOperationDelegate(this.OnEndChannelRegister);
            }
            if ((this.onChannelRegisterCompletedDelegate == null)) {
                this.onChannelRegisterCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnChannelRegisterCompleted);
            }
            base.InvokeAsync(this.onBeginChannelRegisterDelegate, new object[] {
                        message}, this.onEndChannelRegisterDelegate, this.onChannelRegisterCompletedDelegate, userState);
        }
        
        public void ChannelUnRegister(DMS.Common.Messages.ChannelRegistrationMessage message) {
            base.Channel.ChannelUnRegister(message);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginChannelUnRegister(DMS.Common.Messages.ChannelRegistrationMessage message, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginChannelUnRegister(message, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndChannelUnRegister(System.IAsyncResult result) {
            base.Channel.EndChannelUnRegister(result);
        }
        
        private System.IAsyncResult OnBeginChannelUnRegister(object[] inValues, System.AsyncCallback callback, object asyncState) {
            DMS.Common.Messages.ChannelRegistrationMessage message = ((DMS.Common.Messages.ChannelRegistrationMessage)(inValues[0]));
            return this.BeginChannelUnRegister(message, callback, asyncState);
        }
        
        private object[] OnEndChannelUnRegister(System.IAsyncResult result) {
            this.EndChannelUnRegister(result);
            return null;
        }
        
        private void OnChannelUnRegisterCompleted(object state) {
            if ((this.ChannelUnRegisterCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ChannelUnRegisterCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ChannelUnRegisterAsync(DMS.Common.Messages.ChannelRegistrationMessage message) {
            this.ChannelUnRegisterAsync(message, null);
        }
        
        public void ChannelUnRegisterAsync(DMS.Common.Messages.ChannelRegistrationMessage message, object userState) {
            if ((this.onBeginChannelUnRegisterDelegate == null)) {
                this.onBeginChannelUnRegisterDelegate = new BeginOperationDelegate(this.OnBeginChannelUnRegister);
            }
            if ((this.onEndChannelUnRegisterDelegate == null)) {
                this.onEndChannelUnRegisterDelegate = new EndOperationDelegate(this.OnEndChannelUnRegister);
            }
            if ((this.onChannelUnRegisterCompletedDelegate == null)) {
                this.onChannelUnRegisterCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnChannelUnRegisterCompleted);
            }
            base.InvokeAsync(this.onBeginChannelUnRegisterDelegate, new object[] {
                        message}, this.onEndChannelUnRegisterDelegate, this.onChannelUnRegisterCompletedDelegate, userState);
        }
        
        public void ReadChannel(DMS.Common.Messages.InternalLogicalChannelDataMessage message) {
            base.Channel.ReadChannel(message);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginReadChannel(DMS.Common.Messages.InternalLogicalChannelDataMessage message, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginReadChannel(message, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndReadChannel(System.IAsyncResult result) {
            base.Channel.EndReadChannel(result);
        }
        
        private System.IAsyncResult OnBeginReadChannel(object[] inValues, System.AsyncCallback callback, object asyncState) {
            DMS.Common.Messages.InternalLogicalChannelDataMessage message = ((DMS.Common.Messages.InternalLogicalChannelDataMessage)(inValues[0]));
            return this.BeginReadChannel(message, callback, asyncState);
        }
        
        private object[] OnEndReadChannel(System.IAsyncResult result) {
            this.EndReadChannel(result);
            return null;
        }
        
        private void OnReadChannelCompleted(object state) {
            if ((this.ReadChannelCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ReadChannelCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ReadChannelAsync(DMS.Common.Messages.InternalLogicalChannelDataMessage message) {
            this.ReadChannelAsync(message, null);
        }
        
        public void ReadChannelAsync(DMS.Common.Messages.InternalLogicalChannelDataMessage message, object userState) {
            if ((this.onBeginReadChannelDelegate == null)) {
                this.onBeginReadChannelDelegate = new BeginOperationDelegate(this.OnBeginReadChannel);
            }
            if ((this.onEndReadChannelDelegate == null)) {
                this.onEndReadChannelDelegate = new EndOperationDelegate(this.OnEndReadChannel);
            }
            if ((this.onReadChannelCompletedDelegate == null)) {
                this.onReadChannelCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnReadChannelCompleted);
            }
            base.InvokeAsync(this.onBeginReadChannelDelegate, new object[] {
                        message}, this.onEndReadChannelDelegate, this.onReadChannelCompletedDelegate, userState);
        }
    }
}
