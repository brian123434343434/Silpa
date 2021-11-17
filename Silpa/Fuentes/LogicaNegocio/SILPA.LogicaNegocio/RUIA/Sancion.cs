using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.RUIA;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using System.Data;
using System.Collections;

namespace SILPA.LogicaNegocio.RUIA
{
    public class Sancion
    {

        public SancionIdentity Identity;
        public SancionType RuiaIdentity;
        public RelacionSancionIdentity RelacionIdentity;
        private SancionDalc Dalc;
        private RelacionSancionDalc RelacionDalc;        


        /// <summary>
        /// Objeto de configuración del sitio, con las  variables globales
        /// </summary>
        private Configuracion objConfiguracion;

        public Sancion()
        {
            objConfiguracion = new Configuracion();
            Identity = new SancionIdentity();
            RuiaIdentity = new SancionType();
            RelacionIdentity = new RelacionSancionIdentity();
            RelacionDalc = new RelacionSancionDalc();
            Dalc = new SancionDalc();
        }

        /// <summary>
        /// Método para la inserción de una sanciónen la base de datos
        /// </summary>
        /// <param name="_tipoPersona">Tipo de Persona</param>
        /// <param name="_tipoFalta">Tipo de Falta</param>
        /// <param name="_lugarConcurrencia">Lugar de concurrencia</param>
        /// <param name="_descripcionNorma">Descripción de la norma</param>
        /// <param name="_numeroExpediente">Número del expediente</param>
        /// <param name="_numeroActo">Número del acto</param>
        /// <param name="_fechaExpedicion">Fecha de expedición del acto administrativo</param>
        /// <param name="_fechaEjecutoria">Fecha de ejecutoria del acto administrativo</param>
        /// <param name="_fechaEjecucion">Fecha de ejecución o de cumplimiento de la sanción</param>
        /// <param name="_razonSocial">Razón social</param>
        /// <param name="_nit">Nit de la Razón social</param>
        /// <param name="_primerNombre">Primer nombre del sancionado</param>
        /// <param name="_segundoNombre">Segundo nombre del sancionado</param>
        /// <param name="_primerApellido">Primer apellido del sancionado</param>
        /// <param name="_segundoApellido">Segundo apellido del sancionado</param>
        /// <param name="_tipoIdentificacion">Tipo de identificación del sancionado</param>
        /// <param name="_numeroIdentificacion">Número de identificación del sancionado</param>
        /// <param name="_idMunicipio">Identificador del municipio</param>
        /// <param name="_idAutoridad">Identificador de la autoridad ambiental</param>
        /// <param name="_fechaDesde">Fecha de inicio de la sanción</param>
        /// <param name="_fechaHasta">Fecha final de la sanción</param>
        /// <param name="_descripcionDesfijacion">Descripción de la desfijación de la publicación</param>
        public long InsertarSancion(int _tipoPersona, int _tipoFalta, 
            string _descripcionNorma, string _numeroExpediente, string _numeroActo, string _fechaExpedicion,
            string _fechaEjecutoria, string _fechaEjecucion, string _razonSocial, string _nit, 
            string _primerNombre, string _segundoNombre, string _primerApellido, string _segundoApellido,
            int _tipoIdentificacion, string _numeroIdentificacion, int? _idMunicipio, int? _idAutoridad,
            string _descripcionDesfijacion, int munId, int corID, int verId, int _idSancionPrincipal, int _tramiteModificacion, string observaciones)
        {         
                this.Identity.TipoPersona = _tipoPersona;
                this.Identity.IdFalta = _tipoFalta;
                this.Identity.DescripcionNorma = _descripcionNorma;
                this.Identity.NumeroExpediente = _numeroExpediente;
                this.Identity.NumeroActo = _numeroActo;
                this.Identity.FechaExpedicion = _fechaExpedicion;
                this.Identity.FechaEjecutoria = _fechaEjecutoria;
                this.Identity.FechaEjecucion = _fechaEjecucion;
                this.Identity.RazonSocial = _razonSocial;
                this.Identity.Nit = _nit;
                this.Identity.PrimerNombre = _primerNombre;
                this.Identity.SegundoNombre = _segundoNombre;
                this.Identity.PrimerApellido = _primerApellido;
                this.Identity.SegundoApellido = _segundoApellido;
                this.Identity.IdTipoIdentificacion = _tipoIdentificacion;
                this.Identity.NumeroIdentificacion = _numeroIdentificacion;
                this.Identity.IdMunicipio = _idMunicipio;
                this.Identity.IdAutoridad = _idAutoridad;
                this.Identity.DescripcionDesfijacion = _descripcionDesfijacion;
                this.Identity.IdSancionPrincipal = _idSancionPrincipal;
                // 30-sep-2010 - aegb : incidencia 2284
                this.Identity.TramiteModificacion = _tramiteModificacion;

                // mardila 05/04/2010 insertamos el lugar de ocurrencia
                //this.Identity.DepId = depId;
                this.Identity.MunId = munId;
                this.Identity.CorId = corID;
                this.Identity.VerId = verId;
                this.Identity.Observaciones = observaciones;

                Dalc.InsertarSancion(ref this.Identity);
                return this.Identity.IdSancion;
           
        }

        /// <summary>
        /// AEGB
        /// Método para la modificacion de una sanciónen la base de datos
        /// </summary>     
        /// <param name="_tipoFalta">Tipo de Falta</param>
        /// <param name="_lugarConcurrencia">Lugar de concurrencia</param>
        /// <param name="_descripcionNorma">Descripción de la norma</param>
        /// <param name="_numeroExpediente">Número del expediente</param>
        /// <param name="_numeroActo">Número del acto</param>
        /// <param name="_fechaExpedicion">Fecha de expedición del acto administrativo</param>
        /// <param name="_fechaEjecutoria">Fecha de ejecutoria del acto administrativo</param>
        /// <param name="_fechaEjecucion">Fecha de ejecución o de cumplimiento de la sanción</param>       
        /// <param name="_idMunicipio">Identificador del municipio</param>
        /// <param name="_idAutoridad">Identificador de la autoridad ambiental</param>
        /// <param name="_fechaDesde">Fecha de inicio de la sanción</param>
        /// <param name="_fechaHasta">Fecha final de la sanción</param>
        /// <param name="_descripcionDesfijacion">Descripción de la desfijación de la publicación</param>
        public void ActualizarSancion(long _idSancion, int _tipoFalta,
            string _descripcionNorma, string _numeroExpediente, string _numeroActo, string _fechaExpedicion,
            string _fechaEjecutoria, string _fechaEjecucion, int? _idAutoridad,
            string _descripcionDesfijacion, int munId, int corID, int verId,
            string _motivoModificacion, int _idSancionPrincipal, int _tramiteModificacion, string observaciones, string usuarioModifica)
        {
            this.Identity.IdSancion = _idSancion;
            this.Identity.IdFalta = _tipoFalta;
            this.Identity.DescripcionNorma = _descripcionNorma;
            this.Identity.NumeroExpediente = _numeroExpediente;
            this.Identity.NumeroActo = _numeroActo;
            this.Identity.FechaExpedicion = _fechaExpedicion;
            this.Identity.FechaEjecutoria = _fechaEjecutoria;
            this.Identity.FechaEjecucion = _fechaEjecucion;          
            this.Identity.IdAutoridad = _idAutoridad;
            this.Identity.DescripcionDesfijacion = _descripcionDesfijacion;
            this.Identity.MotivoModificacion = _motivoModificacion;
            this.Identity.IdSancionPrincipal = _idSancionPrincipal;
            // 30-sep-2010 - aegb : incidencia 2284
            this.Identity.TramiteModificacion = _tramiteModificacion;

            // mardila 05/04/2010 insertamos el lugar de ocurrencia
            //this.Identity.DepId = depId;
            this.Identity.MunId = munId;
            this.Identity.CorId = corID;
            this.Identity.VerId = verId;
            this.Identity.Observaciones = observaciones;
            this.Identity.UsuarioModifica = usuarioModifica;

            Dalc.ActualizarSancion(this.Identity);        
        }

        /// <summary>
        /// AEGB
        /// Método que actualiza la fecha de cumplimiento de una sanción en la base de datos
        /// </summary>
        /// <param name="objIdentity">Onjeto con los datos de la sanción</param>
        public void ActualizarSancion(long _idSancion, string _fechaEjecucion, int _idSancionPrincipal)
        {
            Dalc.ActualizarSancion(_idSancion, _fechaEjecucion, _idSancionPrincipal);
        }

        /// <summary>
        /// Método que inserta las diferentes opciones para la sancion aplicada
        /// </summary>
        /// <param name="_idSancion">Identificador de la sanción</param>
        /// <param name="_idTipoSancion">Identificador del tipo de sanción</param>
        /// <param name="_idOpcionSancion">Identificador de la opción de la sanción</param>
        public void InsertarRelacionOpcion(long _idSancion, int _idTipoSancion, int _idOpcionSancion, string sancionAplicada)
        {
            try
            {
                this.RelacionIdentity.IdSancion = _idSancion;
                this.RelacionIdentity.IdTipoSancion = _idTipoSancion;
                this.RelacionIdentity.IdOpcionSancion = _idOpcionSancion;
                this.RelacionIdentity.SancionAplicada = sancionAplicada;
                RelacionDalc.InsertarOpciones(ref RelacionIdentity);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al insertar las diferentes opciones para la sanción aplicada.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>
        /// AEGB
        /// Elimina que inserta todas las opciones para la sancion aplicada
        /// </summary>
        /// <param name="_idSancion">Identificador de la sanción</param>
        public void EliminarRelacionOpcion(long _idSancion)        {
           
            RelacionDalc.EliminarOpciones(_idSancion);
        }

        /// <summary>
        /// Metodo de consulta de sanciones con opciones de busqueda
        /// </summary>
        /// <param name="_idSancion">Identificador de la sancion</param>
        /// <param name="_idAutoridad">Identificador de la Autoridad Ambiental</param>
        /// <param name="_idFalta">IIdentificador del tipo de falta</param>
        /// <param name="_numeroExpediente">Número del expediente</param>
        /// <param name="_numeroActo">Número del Acto administrativo</param>
        /// <param name="_numeroIdentificacion">Número de identificación</param>
        /// <param name="_fechaDesde">Fecha de consulta incial</param>
        /// <param name="_fechaHasta">Fecha de consulta final</param>
        /// <returns>Conjunto de Datos: [CODIGO] - [AUTORIDAD] - [FALTA] - [CONCURRENCIA] - [DESCRIPCION_NORMA]
        /// [EXPEDIENTE] - [ACTO] - [FECHA_EXP_ACTO] - [FECHA_EJECUT_ACTO] - [FECHA_EJECU_ACTO] - [PRIMER_NOMBRE]
        /// - [SEGUNDO_NOMBRE] - [PRIMER_APELLIDO] - [SEGUNDO_APELLIDO] - [RAZON_SOCIAL] - [NIT]
        /// - [IDENTIFICACION] - [TIPO_ID] - [DEPARTAMENTO] - [MUNICIPIO] - [FECHA_DESF] - [DESCRIPCION_DESF]
        /// </returns>
        public DataSet ListaSancion(long? _idSancion, int? _idAutoridad,
            int? _idFalta, int? _idTipoSancion, string _numeroExpediente, string _numeroActo, string _nombreResponsable,
            string _fechaDesde, string _fechaHasta, int? _idDepartamento, int? _idMunicipio, int? _idCorregimiento, int? _idVereda,
            string _fechaEjecucion, string _numeroIdentificacion, string estadoSancion)
        {
            return Dalc.ListaSancion(_idSancion, _idAutoridad, _idFalta, _idTipoSancion, _numeroExpediente,
                    _numeroActo, _nombreResponsable, _fechaDesde, _fechaHasta, _idDepartamento, _idMunicipio, _idCorregimiento, _idVereda,
                    _fechaEjecucion, _numeroIdentificacion, estadoSancion);
        }

        /// <summary>
        /// Método que retorna las opciones de una sanción aplicada
        /// </summary>
        /// <param name="_idSancion">Identificador de la Sanción</param>
        /// <returns>Conjunto de Datos: [RSA_ID_SANCION] - [TIS_NOMBRE] - [RSA_ID_TIPO_SANCION]
        ///  - [OPS_NOMBRE_OPCION]</returns>
        public DataSet ListaOpcionesSancion(long? _idSancion)
        {
            return RelacionDalc.ListaOpcionesSancion(_idSancion);
        }

        /// <summary>
        /// Metodo de consulta de sanciones con opciones de busqueda
        /// </summary>
        /// <param name="_idSancion">Identificador de la sancion</param>
        /// <param name="_idAutoridad">Identificador de la Autoridad Ambiental</param>
        /// <param name="_idFalta">IIdentificador del tipo de falta</param>
        /// <param name="_numeroExpediente">Número del expediente</param>
        /// <param name="_numeroActo">Número del Acto administrativo</param>
        /// <param name="_numeroIdentificacion">Número de identificación</param>
        /// <param name="_fechaDesde">Fecha de consulta incial</param>
        /// <param name="_fechaHasta">Fecha de consulta final</param>
        /// <returns>Conjunto de Datos: [CODIGO] - [AUTORIDAD] - [FALTA] - [CONCURRENCIA] - [DESCRIPCION_NORMA]
        /// [EXPEDIENTE] - [ACTO] - [FECHA_EXP_ACTO] - [FECHA_EJECUT_ACTO] - [FECHA_EJECU_ACTO] - [PRIMER_NOMBRE]
        /// - [SEGUNDO_NOMBRE] - [PRIMER_APELLIDO] - [SEGUNDO_APELLIDO] - [RAZON_SOCIAL] - [NIT]
        /// - [IDENTIFICACION] - [TIPO_ID] - [DEPARTAMENTO] - [MUNICIPIO] - [FECHA_DESF] - [DESCRIPCION_DESF]
        /// </returns>
        public DataSet ListaSancionDetalle(long? _idSancion, int? _idAutoridad, string _fechaEjecucion)
        {
            return Dalc.ListaSancionDetalle(_idSancion, _idAutoridad, _fechaEjecucion);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strXmlDatos"></param>
        /// <returns>DAtos en Xml</returns>
        public void GuardarSancion(string strXmlDatos)
        {
            try
            {
	            RuiaIdentity = (SancionType)RuiaIdentity.Deserializar(strXmlDatos);
	            Identity.IdSancion = 0;
	            Identity.CorId = RuiaIdentity.corregimientoId;
	            Identity.DescripcionNorma = RuiaIdentity.descripcionNorma;
	            if (RuiaIdentity.fechaExpActo=="01/01/0001")
	                Identity.FechaExpedicion = null;
	            else
	                Identity.FechaExpedicion = RuiaIdentity.fechaExpActo;
	            Identity.IdAutoridad = RuiaIdentity.autoridadId;
	            Identity.IdFalta = RuiaIdentity.tipoFaltaId;
	            Identity.IdMunicipio = RuiaIdentity.municipioIdentificacionId;
	            Identity.IdSancionPrincipal = RuiaIdentity.tipoSancionPrincipalId;
	            Identity.IdTipoIdentificacion = RuiaIdentity.tipoIdentificacionId;
	            Identity.MunId = RuiaIdentity.municipioId;
	            Identity.Nit = RuiaIdentity.nit;
	            Identity.NumeroActo = RuiaIdentity.numeroActo;
	            Identity.FechaEjecutoria = string.Empty;  
	            Identity.NumeroExpediente = RuiaIdentity.numeroExp;
	            Identity.NumeroIdentificacion = RuiaIdentity.numeroIdentificacion;
	            Identity.PrimerApellido = RuiaIdentity.primerApellido;
	            Identity.PrimerNombre = RuiaIdentity.primerNombre;
	            Identity.RazonSocial = RuiaIdentity.razonSocial;
	            Identity.SancionPrincipal = RuiaIdentity.sancionAplicada;
	            Identity.SegundoApellido = RuiaIdentity.segundoApellido;
	            Identity.SegundoNombre = RuiaIdentity.segundoNombre;
	            Identity.TipoPersona = RuiaIdentity.tipoPersonaId;
	            Identity.VerId = RuiaIdentity.veredaId;
	            Identity.TipoDocumento = RuiaIdentity.tipoDocumento;
	            Identity.Observaciones = RuiaIdentity.Observaciones;
	            if (RuiaIdentity.fechaCumplimiento == "")
	                Identity.FechaEjecucion = null;
	            else
	                Identity.FechaEjecucion = RuiaIdentity.fechaCumplimiento;
	            // 30-sep-2010 - aegb : incidencia 2284
	            Identity.TramiteModificacion = RuiaIdentity.tramiteModificacion;
	            if (RuiaIdentity.fechaEjecutoriaActo != null)
	                Identity.FechaEjecutoria = RuiaIdentity.fechaEjecutoriaActo;
	            this.Dalc.InsertarSancion(ref Identity);
				
	            //Se agrega la sancion aplicada
	            Sancion _sancion = new Sancion();
	            _sancion.InsertarRelacionOpcion(Identity.IdSancion, TipoSancion.Principal, Identity.IdSancionPrincipal, Identity.SancionPrincipal);
	
	            if (RuiaIdentity.sancionAccesoria != null)
	            {
	                for (int i = 0; i < RuiaIdentity.sancionAccesoria.Length; i++)
	                {
	                    SancionAccesoriaType tipo = (SancionAccesoriaType)RuiaIdentity.sancionAccesoria[i];
	                    _sancion.InsertarRelacionOpcion(Identity.IdSancion, TipoSancion.Accesoria, tipo.tipoSancionAccesoriaId, tipo.sancionAccesoriaNombre);
	                }
	            }
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Guardar Sanción.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>
        /// Elimina una sancion, la inactiva en la base de datos
        /// </summary>
        /// <param name="idSancion">Identificador de la sancion</param>
        public void EliminarSancion(long idSancion, string motivoEliminacion, string usuario)
        {
            SancionDalc obj = new SancionDalc();
            obj.EliminarSancion(idSancion, motivoEliminacion, usuario);
        }

        /// <summary>
        /// Metodo de consulta de una sancion
        /// </summary>
        /// <param name="idSancion">Identificador de la sancion</param>
        public SancionIdentity ListaSancionDetalles(long _idSancion, int? _idAutoridad, string _fechaEjecucion)
        {
            SancionDalc obj = new SancionDalc();
            return obj.ListaSancionDetalles(_idSancion, _idAutoridad, _fechaEjecucion);
        }

        /// <summary>
        /// AEGB
        /// Método que actualiza la fecha ejecutoria de una sanción en la base de datos      
        /// </summary>
        /// <param name="strXmlDatos"></param>
        /// <returns>DAtos en Xml</returns>
        public void ActualizarSancionEjecutoria(string strXmlDatos)
        {
            RuiaIdentity = (SancionType)RuiaIdentity.Deserializar(strXmlDatos);
            Dalc.ActualizarSancionEjecutoria(RuiaIdentity.autoridadId, RuiaIdentity.numeroExp, RuiaIdentity.numeroActo, DateTime.Parse(RuiaIdentity.fechaCumplimiento), RuiaIdentity.tipoDocumento);
        }

        /// <summary>
        /// AEGB
        /// Método que actualiza la fecha cumplimiento de una sanción en la base de datos      
        /// </summary>
        /// <param name="strXmlDatos"></param>
        /// <returns>DAtos en Xml</returns>
        public void ActualizarSancion(string strXmlDatos)
        {
            RuiaIdentity = (SancionType)RuiaIdentity.Deserializar(strXmlDatos);
            Dalc.ActualizarSancion(RuiaIdentity.autoridadId, RuiaIdentity.numeroExp, RuiaIdentity.numeroActo, DateTime.Parse(RuiaIdentity.fechaCumplimiento),RuiaIdentity.tipoDocumento);
        }

    }
}
