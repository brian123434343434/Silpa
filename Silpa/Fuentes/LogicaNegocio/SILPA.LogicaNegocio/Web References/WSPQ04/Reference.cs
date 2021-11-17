﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.36366
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.36366.
// 
#pragma warning disable 1591

namespace SILPA.LogicaNegocio.WSPQ04 {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.36366")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WSPQ04Soap", Namespace="http://tempuri.org/")]
    public partial class WSPQ04 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback EnviarDatosPagoOperationCompleted;
        
        private System.Threading.SendOrPostCallback EnviarDatosPago1OperationCompleted;
        
        private System.Threading.SendOrPostCallback MonitorearPagoPSEOperationCompleted;
        
        private System.Threading.SendOrPostCallback TestOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WSPQ04() {
            this.Url = global::SILPA.LogicaNegocio.Properties.Settings.Default.SILPA_LogicaNegocio_WSPQ04_WSPQ04;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event EnviarDatosPagoCompletedEventHandler EnviarDatosPagoCompleted;
        
        /// <remarks/>
        public event EnviarDatosPago1CompletedEventHandler EnviarDatosPago1Completed;
        
        /// <remarks/>
        public event MonitorearPagoPSECompletedEventHandler MonitorearPagoPSECompleted;
        
        /// <remarks/>
        public event TestCompletedEventHandler TestCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CU-NG-07", RequestElementName="CU-NG-07", RequestNamespace="http://tempuri.org/", ResponseElementName="CU-NG-07Response", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("CU-NG-07Result")]
        public string EnviarDatosPago(string numReferencia) {
            object[] results = this.Invoke("EnviarDatosPago", new object[] {
                        numReferencia});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void EnviarDatosPagoAsync(string numReferencia) {
            this.EnviarDatosPagoAsync(numReferencia, null);
        }
        
        /// <remarks/>
        public void EnviarDatosPagoAsync(string numReferencia, object userState) {
            if ((this.EnviarDatosPagoOperationCompleted == null)) {
                this.EnviarDatosPagoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarDatosPagoOperationCompleted);
            }
            this.InvokeAsync("EnviarDatosPago", new object[] {
                        numReferencia}, this.EnviarDatosPagoOperationCompleted, userState);
        }
        
        private void OnEnviarDatosPagoOperationCompleted(object arg) {
            if ((this.EnviarDatosPagoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarDatosPagoCompleted(this, new EnviarDatosPagoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="EnviarDatosPago1")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CU-I-EXPI-10", RequestElementName="CU-I-EXPI-10", RequestNamespace="http://tempuri.org/", ResponseElementName="CU-I-EXPI-10Response", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("CU-I-EXPI-10Result")]
        public string EnviarDatosPago(string numReferencia, string codigoExpediente, string estado) {
            object[] results = this.Invoke("EnviarDatosPago1", new object[] {
                        numReferencia,
                        codigoExpediente,
                        estado});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void EnviarDatosPago1Async(string numReferencia, string codigoExpediente, string estado) {
            this.EnviarDatosPago1Async(numReferencia, codigoExpediente, estado, null);
        }
        
        /// <remarks/>
        public void EnviarDatosPago1Async(string numReferencia, string codigoExpediente, string estado, object userState) {
            if ((this.EnviarDatosPago1OperationCompleted == null)) {
                this.EnviarDatosPago1OperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarDatosPago1OperationCompleted);
            }
            this.InvokeAsync("EnviarDatosPago1", new object[] {
                        numReferencia,
                        codigoExpediente,
                        estado}, this.EnviarDatosPago1OperationCompleted, userState);
        }
        
        private void OnEnviarDatosPago1OperationCompleted(object arg) {
            if ((this.EnviarDatosPago1Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarDatosPago1Completed(this, new EnviarDatosPago1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CU-NG-08", RequestElementName="CU-NG-08", RequestNamespace="http://tempuri.org/", ResponseElementName="CU-NG-08Response", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("CU-NG-08Result")]
        public string MonitorearPagoPSE(string strNumeroTransaccionCUS) {
            object[] results = this.Invoke("MonitorearPagoPSE", new object[] {
                        strNumeroTransaccionCUS});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void MonitorearPagoPSEAsync(string strNumeroTransaccionCUS) {
            this.MonitorearPagoPSEAsync(strNumeroTransaccionCUS, null);
        }
        
        /// <remarks/>
        public void MonitorearPagoPSEAsync(string strNumeroTransaccionCUS, object userState) {
            if ((this.MonitorearPagoPSEOperationCompleted == null)) {
                this.MonitorearPagoPSEOperationCompleted = new System.Threading.SendOrPostCallback(this.OnMonitorearPagoPSEOperationCompleted);
            }
            this.InvokeAsync("MonitorearPagoPSE", new object[] {
                        strNumeroTransaccionCUS}, this.MonitorearPagoPSEOperationCompleted, userState);
        }
        
        private void OnMonitorearPagoPSEOperationCompleted(object arg) {
            if ((this.MonitorearPagoPSECompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.MonitorearPagoPSECompleted(this, new MonitorearPagoPSECompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Test", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Test() {
            this.Invoke("Test", new object[0]);
        }
        
        /// <remarks/>
        public void TestAsync() {
            this.TestAsync(null);
        }
        
        /// <remarks/>
        public void TestAsync(object userState) {
            if ((this.TestOperationCompleted == null)) {
                this.TestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTestOperationCompleted);
            }
            this.InvokeAsync("Test", new object[0], this.TestOperationCompleted, userState);
        }
        
        private void OnTestOperationCompleted(object arg) {
            if ((this.TestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TestCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.36366")]
    public delegate void EnviarDatosPagoCompletedEventHandler(object sender, EnviarDatosPagoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.36366")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnviarDatosPagoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnviarDatosPagoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.36366")]
    public delegate void EnviarDatosPago1CompletedEventHandler(object sender, EnviarDatosPago1CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.36366")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnviarDatosPago1CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnviarDatosPago1CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.36366")]
    public delegate void MonitorearPagoPSECompletedEventHandler(object sender, MonitorearPagoPSECompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.36366")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MonitorearPagoPSECompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal MonitorearPagoPSECompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.36366")]
    public delegate void TestCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591