﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SILPA.LogicaNegocio.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://192.168.0.7:8000/ClienteTramitadorAAC/NotificacionPDI.asmx")]
        public string SILPA_LogicaNegocio_NotificacionPDI_NotificacionPDI {
            get {
                return ((string)(this["SILPA_LogicaNegocio_NotificacionPDI_NotificacionPDI"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://172.17.0.160/xcars/XSilpaServicios/WSPQ04.asmx")]
        public string SILPA_LogicaNegocio_WSPQ04_WSPQ04 {
            get {
                return ((string)(this["SILPA_LogicaNegocio_WSPQ04_WSPQ04"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://192.168.0.7:8182/WSPQ02.asmx")]
        public string SILPA_LogicaNegocio_WSPQ02_WSPQ02 {
            get {
                return ((string)(this["SILPA_LogicaNegocio_WSPQ02_WSPQ02"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://192.168.0.7:8182/bpmServices.asmx")]
        public string SILPA_LogicaNegocio_bpmServices_GattacaBPMServices9000 {
            get {
                return ((string)(this["SILPA_LogicaNegocio_bpmServices_GattacaBPMServices9000"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://192.168.0.7:8000/silpa/bpm/services/bpmservices.asmx")]
        public string SILPA_LogicaNegocio_bpmServices_Gattaca_x0020_BPM_x0020_Services_x0020_9_0_0_0 {
            get {
                return ((string)(this["SILPA_LogicaNegocio_bpmServices_Gattaca_x0020_BPM_x0020_Services_x0020_9_0_0_0"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://172.17.0.62:8182/WSPQ03.asmx")]
        public string SILPA_LogicaNegocio_WSPQ03_WSPQ03 {
            get {
                return ((string)(this["SILPA_LogicaNegocio_WSPQ03_WSPQ03"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://vital.anla.gov.co:8182/WSSUN.asmx")]
        public string SILPA_LogicaNegocio_WSSUN_WSSUN {
            get {
                return ((string)(this["SILPA_LogicaNegocio_WSSUN_WSSUN"]));
            }
        }
    }
}
