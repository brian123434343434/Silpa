﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=2.0.50727.42.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI", IsNullable=false)]
public partial class Ejecutar {
    
    private string codigoTramiteField;
    
    private decimal versionTramiteField;
    
    private System.Xml.XmlNode xmlEntradaEjecucionField;
    
    private documento documentoField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="integer")]
    public string codigoTramite {
        get {
            return this.codigoTramiteField;
        }
        set {
            this.codigoTramiteField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal versionTramite {
        get {
            return this.versionTramiteField;
        }
        set {
            this.versionTramiteField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public System.Xml.XmlNode xmlEntradaEjecucion {
        get {
            return this.xmlEntradaEjecucionField;
        }
        set {
            this.xmlEntradaEjecucionField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public documento documento {
        get {
            return this.documentoField;
        }
        set {
            this.documentoField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI")]
public partial class documento {
    
    private string nombreArchivoField;
    
    private byte[] archivoField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string nombreArchivo {
        get {
            return this.nombreArchivoField;
        }
        set {
            this.nombreArchivoField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
    public byte[] archivo {
        get {
            return this.archivoField;
        }
        set {
            this.archivoField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI", IsNullable=false)]
public partial class EjecutarResponse {
    
    private bool resultadoField;
    
    private TipoEnumResultadoEjecucion resultadoEjecucionField;
    
    private string motivoRechazoField;
    
    private System.Xml.XmlNode xmlRespuestaField;
    
    private documento documentoField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public bool resultado {
        get {
            return this.resultadoField;
        }
        set {
            this.resultadoField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public TipoEnumResultadoEjecucion resultadoEjecucion {
        get {
            return this.resultadoEjecucionField;
        }
        set {
            this.resultadoEjecucionField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string motivoRechazo {
        get {
            return this.motivoRechazoField;
        }
        set {
            this.motivoRechazoField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public System.Xml.XmlNode XmlRespuesta {
        get {
            return this.xmlRespuestaField;
        }
        set {
            this.xmlRespuestaField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public documento documento {
        get {
            return this.documentoField;
        }
        set {
            this.documentoField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI")]
public enum TipoEnumResultadoEjecucion {
    
    /// <remarks/>
    EjecucionSatisfactoria,
    
    /// <remarks/>
    ErrorInterno,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI", IsNullable=false)]
public partial class ConsultarResultado {
    
    private string tiqueteField;
    
    private string numeroRastreoField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="nonNegativeInteger")]
    public string tiquete {
        get {
            return this.tiqueteField;
        }
        set {
            this.tiqueteField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="nonNegativeInteger")]
    public string numeroRastreo {
        get {
            return this.numeroRastreoField;
        }
        set {
            this.numeroRastreoField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI", IsNullable=false)]
public partial class ConsultarResultadoResponse {
    
    private bool resultadoField;
    
    private TipoEnumResultadoConsultarResultado resultadoConsultaField;
    
    private string motivoRechazoField;
    
    private System.Xml.XmlNode xmlRespuestaField;
    
    private documento documentoField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public bool resultado {
        get {
            return this.resultadoField;
        }
        set {
            this.resultadoField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public TipoEnumResultadoConsultarResultado resultadoConsulta {
        get {
            return this.resultadoConsultaField;
        }
        set {
            this.resultadoConsultaField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string motivoRechazo {
        get {
            return this.motivoRechazoField;
        }
        set {
            this.motivoRechazoField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public System.Xml.XmlNode XmlRespuesta {
        get {
            return this.xmlRespuestaField;
        }
        set {
            this.xmlRespuestaField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public documento documento {
        get {
            return this.documentoField;
        }
        set {
            this.documentoField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI")]
public enum TipoEnumResultadoConsultarResultado {
    
    /// <remarks/>
    EnEjecucion,
    
    /// <remarks/>
    SiExisteDocumento,
    
    /// <remarks/>
    NoExisteDocumento,
    
    /// <remarks/>
    ErrorInterno,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI", IsNullable=false)]
public partial class EjecutarAsync {
    
    private string codigoTramiteField;
    
    private decimal versionTramiteField;
    
    private System.Xml.XmlNode xmlEntradaEjecucionField;
    
    private documento documentoField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="integer")]
    public string codigoTramite {
        get {
            return this.codigoTramiteField;
        }
        set {
            this.codigoTramiteField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal versionTramite {
        get {
            return this.versionTramiteField;
        }
        set {
            this.versionTramiteField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public System.Xml.XmlNode xmlEntradaEjecucion {
        get {
            return this.xmlEntradaEjecucionField;
        }
        set {
            this.xmlEntradaEjecucionField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public documento documento {
        get {
            return this.documentoField;
        }
        set {
            this.documentoField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://PDI.GOBIERNOENLINEA.GOV/WSPDI", IsNullable=false)]
public partial class EjecutarAsyncResponse {
    
    private bool resultadoField;
    
    private TipoEnumResultadoEjecucion resultadoEjecucionField;
    
    private string motivoRechazoField;
    
    private string tiqueteField;
    
    private string numeroRastreoField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public bool resultado {
        get {
            return this.resultadoField;
        }
        set {
            this.resultadoField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public TipoEnumResultadoEjecucion resultadoEjecucion {
        get {
            return this.resultadoEjecucionField;
        }
        set {
            this.resultadoEjecucionField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string motivoRechazo {
        get {
            return this.motivoRechazoField;
        }
        set {
            this.motivoRechazoField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="nonNegativeInteger")]
    public string tiquete {
        get {
            return this.tiqueteField;
        }
        set {
            this.tiqueteField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="nonNegativeInteger")]
    public string numeroRastreo {
        get {
            return this.numeroRastreoField;
        }
        set {
            this.numeroRastreoField = value;
        }
    }
}