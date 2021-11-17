﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18449
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.18449.
// 
#pragma warning disable 1591

namespace SILPA.LogicaNegocio.NotificacionPDI {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="NotificacionPDISoap", Namespace="http://tempuri.org/")]
    public partial class NotificacionPDI : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback EjecutarNotificacionOperationCompleted;
        
        private System.Threading.SendOrPostCallback testPDIOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public NotificacionPDI() {
            this.Url = global::SILPA.LogicaNegocio.Properties.Settings.Default.SILPA_LogicaNegocio_NotificacionPDI_NotificacionPDI;
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
        public event EjecutarNotificacionCompletedEventHandler EjecutarNotificacionCompleted;
        
        /// <remarks/>
        public event testPDICompletedEventHandler testPDICompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EjecutarNotificacion", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string EjecutarNotificacion(string XMLDatos, string codigoTramite, string versionTramite, string rutaArchivo) {
            object[] results = this.Invoke("EjecutarNotificacion", new object[] {
                        XMLDatos,
                        codigoTramite,
                        versionTramite,
                        rutaArchivo});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void EjecutarNotificacionAsync(string XMLDatos, string codigoTramite, string versionTramite, string rutaArchivo) {
            this.EjecutarNotificacionAsync(XMLDatos, codigoTramite, versionTramite, rutaArchivo, null);
        }
        
        /// <remarks/>
        public void EjecutarNotificacionAsync(string XMLDatos, string codigoTramite, string versionTramite, string rutaArchivo, object userState) {
            if ((this.EjecutarNotificacionOperationCompleted == null)) {
                this.EjecutarNotificacionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEjecutarNotificacionOperationCompleted);
            }
            this.InvokeAsync("EjecutarNotificacion", new object[] {
                        XMLDatos,
                        codigoTramite,
                        versionTramite,
                        rutaArchivo}, this.EjecutarNotificacionOperationCompleted, userState);
        }
        
        private void OnEjecutarNotificacionOperationCompleted(object arg) {
            if ((this.EjecutarNotificacionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EjecutarNotificacionCompleted(this, new EjecutarNotificacionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/testPDI", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool testPDI() {
            object[] results = this.Invoke("testPDI", new object[0]);
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void testPDIAsync() {
            this.testPDIAsync(null);
        }
        
        /// <remarks/>
        public void testPDIAsync(object userState) {
            if ((this.testPDIOperationCompleted == null)) {
                this.testPDIOperationCompleted = new System.Threading.SendOrPostCallback(this.OntestPDIOperationCompleted);
            }
            this.InvokeAsync("testPDI", new object[0], this.testPDIOperationCompleted, userState);
        }
        
        private void OntestPDIOperationCompleted(object arg) {
            if ((this.testPDICompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.testPDICompleted(this, new testPDICompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void EjecutarNotificacionCompletedEventHandler(object sender, EjecutarNotificacionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EjecutarNotificacionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EjecutarNotificacionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void testPDICompletedEventHandler(object sender, testPDICompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class testPDICompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal testPDICompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591