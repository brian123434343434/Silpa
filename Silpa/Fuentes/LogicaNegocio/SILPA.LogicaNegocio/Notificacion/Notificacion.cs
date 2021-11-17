using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.Notificacion;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.DAA;
using SILPA.LogicaNegocio;
using SILPA.AccesoDatos.DAA;
using System.Data;
using SILPA.AccesoDatos.Generico;
using tipoDatoProcesoNotificacionEnt;
using SoftManagement.Log;
using System.Configuration;
using System.Net;
using SILPA.AccesoDatos.Parametrizacion;
using SILPA.AccesoDatos.Publicacion;
using SILPA.AccesoDatos.RecursoReposicion;
using SILPA.LogicaNegocio.Recurso;
using SILPA.LogicaNegocio.Excepciones;
using System.IO;
using SILPA.LogicaNegocio.Formularios;
using SILPA.LogicaNegocio.Formularios.OrigenDatos;
using SILPA.LogicaNegocio.Utilidad;
using SILPA.LogicaNegocio.AdmTablasBasicas;
using System.Globalization;
using SILPA.LogicaNegocio.EstampaTiempo;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class Notificacion
    {

        #region Metodos Privados

            #region Sección GEL-XML

                /// <summary>
                /// Construye la cadena de XML que contiene los datos para Enviar a PDI
                /// </summary>
                /// <param name="notificacion">Objeto de Notificación Interno (Entity)</param>
                /// <param name="notificacionXMLAA">Objeto deserializado a partir de Cadena XML entregada por la AA</param>
                /// <returns></returns>
                private string SetXMLGELProcesoNotificacionEntrada(NotificacionEntity notificacion, NotificacionType notificacionXMLAA)
                {
                    // NotificacionType _xmlNotificacion = new NotificacionType();
                    //XmlSerializador _ser = new XmlSerializador();
                    //_xmlNotificacion = (NotificacionType)_ser.Deserializar(new NotificacionType(), documentoXML);
                    //escribir("inicia instancia de documento para GEL-XML");

                    tipoDatoProcesoNotificacionEnt.notnot.tipoDatoProcesoNotificacionEnt3 _objDoc = tipoDatoProcesoNotificacionEnt.notnot.tipoDatoProcesoNotificacionEnt3.CreateDocument();

                    tipoDatoProcesoNotificacionEnt.notnot.tipoDatoProcesoNotificacionEnt2 _objRoot = _objDoc.datoProcesoNotificacionEnt.Append();


                    //------------------Número de Acto Administrativo------------------------
                    tipoDatoProcesoNotificacionEnt.locdoc.tipoNumActoAdministrativoType numeroActo = _objRoot.numActoAdministrativoNotificacion.Append();

                    numeroActo.Value = notificacionXMLAA.numActoAdministrativo;
                    //------------------------------------------------------------------------

                    //------------------Número de Proceso Administración ------------------------
                    tipoDatoProcesoNotificacionEnt.locdoc.tipoNumExpedienteType numeroExpediente = _objRoot.numProcesoAdministracion.Append();

                    numeroExpediente.Value = notificacionXMLAA.numProcesoAdministracion;

                    //------------------------------------------------------------------------------

                    //------------------Parte Resolutiva (Descripción del Acto) ------------------------
                    tipoDatoProcesoNotificacionEnt.locdoc.tipoTextoActoAdministrativoType parteResolutiva = _objRoot.parteResolutiva.Append();

                    parteResolutiva.Value = notificacionXMLAA.parteResolutiva;

                    //------------------------------------------------------------------------------

                    //------------------Entidad Publica Notificacion ------------------------
                    tipoDatoProcesoNotificacionEnt.locorg.tipoCodUnicoIdEntidadPublicaType entidadPublica = _objRoot.entidadPublicaNotificacion.Append();

                    entidadPublica.Value = notificacion.CodigoEntidadPublica;

                    //------------------------------------------------------------------------------

                    //------------------Dependencia Entidad ----------------------------------------
                    tipoDatoProcesoNotificacionEnt.locorg.tipoDependenciaEntidad dependenciaEntidad = _objRoot.dependenciaEntidadNotificacion.Append();

                    dependenciaEntidad.idDependenciaEntidadNotificacion.Append().Value = notificacion.DependenciaEntidad.ID;

                    if (notificacion.DependenciaEntidad.Nombre != null)
                    {
                        dependenciaEntidad.nomDependenciaEntidadNotificacion.Append().Value = notificacion.DependenciaEntidad.Nombre;

                    }
                    //------------------------------------------------------------------------------

                    //------------------Sistema Entidad---------------------------------------------
                    tipoDatoProcesoNotificacionEnt.notcor.tipoIdSistemaEntidadType sistemaEntidad = _objRoot.sistemaEntidadPublicaNotificacion.Append();

                    sistemaEntidad.Value = notificacion.SistemaEntidadPublica;

                    //------------------------------------------------------------------------------

                    //------------------Funcionario Notificante ----------------------------------------
                    tipoDatoProcesoNotificacionEnt.locide.tipoIdentificacionNacionalPersona funcionario = _objRoot.idFuncionarioNotificante.Append();

                    tipoDatoProcesoNotificacionEnt.locide.tipoTipoIdNacionalPersona tipoIDFuncionario = funcionario.tipoIdentificacionNacionalPersona2.Append();

                    tipoIDFuncionario.codTipoIdentificacion.Append().Value = notificacion.TipoIdentificacionFuncionario.Sigla;

                    tipoIDFuncionario.nomTipoIdentificacion.Append().Value = notificacion.TipoIdentificacionFuncionario.Nombre;

                    tipoDatoProcesoNotificacionEnt.locide.grupoNumeroIdentificacion numeroIDFuncionario = funcionario.grupoNumeroIdentificacion2.Append();

                    numeroIDFuncionario.numeroCedulaCiudadania.Append().Value = notificacion.IdentificacionFuncionario;

                    //------------------------------------------------------------------------------
                    //escribir("inicia crear lista de personas en GEL-XML");
                    //------------------Lista personas Notificar----------------------------------------
                    tipoDatoProcesoNotificacionEnt.mndgen.tipoListaPersonaNotificar listaPersonas = _objRoot.listaPersonaEnvioNotificacion.Append();

                    foreach (PersonaNotificarEntity persona in notificacion.ListaPersonas)
                    {
                        tipoDatoProcesoNotificacionEnt.mndgen.tipoPersonaNotificar PersonaNotificar = listaPersonas.listadoPersonaNotificar.Append();

                        tipoDatoProcesoNotificacionEnt.locgen.tipoTipoPersona tipoPersona = PersonaNotificar.tipoPersonaNotificar2.Append();

                        tipoPersona.codTipoPersona.Append().Value = persona.TipoPersona.Codigo;

                        if (persona.TipoPersona.Nombre != null)
                        {
                            tipoPersona.nomTipoPersona.Append().Value = persona.TipoPersona.Nombre;

                        }
                        tipoDatoProcesoNotificacionEnt.locide.tipoIdentificacionNacionalPersona idPersona = PersonaNotificar.idPersonaNotificar.Append();

                        tipoDatoProcesoNotificacionEnt.locide.tipoTipoIdNacionalPersona tipoIDPersona = idPersona.tipoIdentificacionNacionalPersona2.Append();

                        tipoIDPersona.codTipoIdentificacion.Append().Value = persona.TipoIdentificacion.Sigla;

                        if (persona.TipoIdentificacion.Nombre != null)
                        {
                            tipoIDPersona.nomTipoIdentificacion.Append().Value = persona.TipoIdentificacion.Nombre;

                        }
                        tipoDatoProcesoNotificacionEnt.locide.grupoNumeroIdentificacion numeroIDPersona = idPersona.grupoNumeroIdentificacion2.Append();

                        numeroIDPersona.numeroCedulaCiudadania.Append().Value = persona.NumeroIdentificacion;

                        tipoDatoProcesoNotificacionEnt.comper.tipoNomPersona nombrePersona = PersonaNotificar.nombrePersonaNotificar.Append();

                        nombrePersona.primerApellido.Append().Value = persona.PrimerApellido;

                        nombrePersona.segundoApellido.Append().Value = persona.SegundoApellido;

                        nombrePersona.primerNombre.Append().Value = persona.PrimerNombre;

                        nombrePersona.segundoNombre.Append().Value = persona.SegundoNombre;

                        if (persona.RazonSocial != null)
                        {
                            PersonaNotificar.razonSocialPersonaNotificar.Append().Value = persona.RazonSocial;

                        }
                    }
                    //------------------------------------------------------------------------------

                    //------------------Plantilla ----------------------------------------
                    tipoDatoProcesoNotificacionEnt.notnot.tipoPlantillaProceso plantilla = _objRoot.plantillaProcesoNotificacion.Append();
                    plantilla.idPlantillaProcesoNotificacion.Append().Value = notificacion.IdPlantilla;
                    if (notificacion.NombrePlantilla != null)
                        plantilla.nomPlantillaProcesoNotificacion.Append().Value = notificacion.NombrePlantilla;
                    //------------------------------------------------------------------------------

                    //------------------Número Acto Administrativo Asociado ----------------------------------------
                    if (notificacion.NumeroActoAdministrativoAsociado != null)
                    {
                        tipoDatoProcesoNotificacionEnt.locdoc.tipoNumActoAdministrativoType actoAsociado = _objRoot.numActoAdministrativoAsociado.Append();
                        actoAsociado.Value = notificacion.NumeroActoAdministrativoAsociado;
                    }
                    //------------------------------------------------------------------------------
                    //escribir("inicia escritura de GEL-XML");

                    return _objDoc.SaveToString(true);

                }

                /// <summary>
                /// Obtiene los DAtos de una respuesta entregada por el proceso de notificación para asignar los valores
                /// a los datos de las personas que se guardarán en la base de datos como lista de personas a notificar
                /// </summary>
                /// <param name="XMLSalida">XML en forma GEL-XML que tiene los datos de respuesta</param>
                /// <param name="notificacion">Objeto de Notificación que contiene la información de las personas</param>
                /// <returns>Cantidad de Personas modificadas</returns>
                /// <remarks>Este método es privado ya que solo puede ser llamado desde el método de crear proceso de notificación</remarks>
                private int GetXMLGELProcesoNotificacionSalida(string XMLSalida, ref NotificacionEntity notificacion, out string mensajeConfirmacion)
                {
                    int _conteo = 0;
                    mensajeConfirmacion = "Error en la respuesta del sistema de notificación en línea";
                    try
                    {
                        tipoDatoProcesoNotificacionSal.notnot.tipoDatoProcesoNotificacionSal3 _objDoc = tipoDatoProcesoNotificacionSal.notnot.tipoDatoProcesoNotificacionSal3.LoadFromString(XMLSalida);
                        tipoDatoProcesoNotificacionSal.notnot.tipoDatoProcesoNotificacionSal2 _objRoot = _objDoc.datoProcesoNotificacionSal.First;
                        //Verificamos que Existan datos dentro del XML Cargado
                        if (_objRoot.datoListaProcesoNotificacion.Exists)
                        {

                            string numeroProcesoAdministracion = _objRoot.numProcesoAdministracion.First.Value;
                            string numeroActoAdministrativo = _objRoot.numActoAdministrativoNotificacion.First.Value;
                            mensajeConfirmacion = _objRoot.mensajeConfirmacionNotificacion.First.Value;
                            //tipoDatoProcesoNotificacionSal.notnot.tipoListaDatoProcesoNotificacion lista1 = _objRoot.datoListaProcesoNotificacion.Append();
                            //tipoDatoProcesoNotificacionSal.notnot.tipoDatoProcesoNotificacion internoLista1 = lista1.listadoDatoProcesoNotificacion.Append();


                            //Se recorre cada persona notificada
                            foreach (tipoDatoProcesoNotificacionSal.notnot.tipoDatoProcesoNotificacion _objDato in _objRoot.datoListaProcesoNotificacion.First.listadoDatoProcesoNotificacion)
                            {
                                // se busca que la persona retornada coincida con alguna de las personas de la lista
                                PersonaNotificarEntity resultado = notificacion.ListaPersonas.Find(
                                    delegate(PersonaNotificarEntity per)
                                    {
                                        //Esto se debe cambiar por una lógica que asigne dependiendo de 
                                        return per.NumeroIdentificacion == _objDato.datoPersonaNotificar.First.idPersonaNotificar.First.grupoNumeroIdentificacion2.First.numeroCedulaCiudadania.First.Value;
                                    });
                                if (resultado != null)
                                {
                                    tipoDatoProcesoNotificacionSal.locgen.tipoNumNotificacion numeroNotificacion = _objDato.numeroNotificacionConsulta.First;
                                    resultado.AnoNotificacion = numeroNotificacion.anoCreacionNumeroNotificacion.First.Value;
                                    resultado.ConsecutivoNotificacion = numeroNotificacion.consecutivoNumeroNotificacion.First.Value;
                                    resultado.EstadoNotificado = new EstadoNotificacionEntity();
                                    resultado.EstadoNotificado.ID = Convert.ToInt32(_objDato.estadoProcesoNotificacion.First.codEstadoProcesoNotificacion.First.Value);
                                    if (_objDato.estadoProcesoNotificacion.First.nomEstadoProcesoNotificacion.Exists)
                                        resultado.EstadoNotificado.Estado = _objDato.estadoProcesoNotificacion.First.nomEstadoProcesoNotificacion.First.Value;



                                }
                                //Se busca el objeto resultado en la lista de notificación y se asigna este a ella
                                int index = -1;
                                index = notificacion.ListaPersonas.FindIndex(
                                    delegate(PersonaNotificarEntity per)
                                    {
                                        return per.NumeroIdentificacion == resultado.NumeroIdentificacion;
                                    });
                                if (index > -1)
                                {
                                    notificacion.ListaPersonas[index] = resultado;
                                    _conteo++;
                                }



                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        SMLog.Escribir(Severidad.Informativo, "----Error del trámite Notificacion:" + ex.ToString());
                        mensajeConfirmacion = "Error del trámite:" + XMLSalida;

                    }
                    return _conteo;
                }

                /// <summary>
                /// Crea la estructura en XML para enviar una consulta de Proceso de Notificación a PDI
                /// </summary>
                /// <param name="persona">Objeto de persona asociada al acto y al proceso de Notificación</param>
                /// <param name="acto">Acto o Proceso de Notificación que se desea consultar</param>
                /// <returns>XML para enviar a PDI</returns>
                public string SetXMLGELEstadoNotificacionEntrada(PersonaNotificarEntity persona, NotificacionEntity acto)
                {
                    //--------------------------------------Creación de Documento----------------------------------------
                    tipoConsultaEstadoNotificacionEnt.notnot.tipoConsultaEstadoNotificacionEnt3 _objConsulta = tipoConsultaEstadoNotificacionEnt.notnot.tipoConsultaEstadoNotificacionEnt3.CreateDocument();
                    tipoConsultaEstadoNotificacionEnt.notnot.tipoConsultaEstadoNotificacionEnt2 _objRoot = _objConsulta.consultaEstadoNotificacionEnt.Append();

                    //--------------------------------------Entidad Pública----------------------------------------
                    tipoConsultaEstadoNotificacionEnt.locorg.tipoCodUnicoIdEntidadPublicaType codEntidadPublica = _objRoot.entidadPublicaNotificacion.Append();
                    codEntidadPublica.Value = acto.CodigoEntidadPublica;

                    //--------------------------------------Sistema Entidad Pública----------------------------------------
                    tipoConsultaEstadoNotificacionEnt.notcor.tipoIdSistemaEntidadType sistemaEntidad = _objRoot.sistemaEntidadPublicaNotificacion.Append();
                    sistemaEntidad.Value = acto.SistemaEntidadPublica;

                    //--------------------------------------Número Notificación Consulta----------------------------------------
                    tipoConsultaEstadoNotificacionEnt.locgen.tipoNumNotificacion numeroNotificacion = _objRoot.numeroNotificacionConsulta.Append();
                    tipoConsultaEstadoNotificacionEnt.locgen.tipoTipoTramiteNotificacion tramite = numeroNotificacion.tipoTramiteNumeroNotificacion.Append();
                    tipoConsultaEstadoNotificacionEnt.locgen.enumCodTipoTramiteNotificacionType enumCodTipoTramite = tramite.codTipoTramiteNumeroNotificacion.Append();
                    enumCodTipoTramite.Value = "NF";

                    //--------------------------------------Entidad Pública Interno----------------------------------------
                    tipoConsultaEstadoNotificacionEnt.locorg.tipoCodUnicoIdEntidadPublicaType codEntidadInterno = numeroNotificacion.entidadPublicaNotificacion.Append();
                    codEntidadInterno.Value = acto.CodigoEntidadPublica;

                    tipoConsultaEstadoNotificacionEnt.comtem.tipoAnoType anoNotificacion = numeroNotificacion.anoCreacionNumeroNotificacion.Append();
                    anoNotificacion.Value = persona.AnoNotificacion;

                    tipoConsultaEstadoNotificacionEnt.notcor.tipoConsecutivoNotificacionType numeroConsecutivo = numeroNotificacion.consecutivoNumeroNotificacion.Append();
                    numeroConsecutivo.Value = persona.ConsecutivoNotificacion;

                    return _objConsulta.SaveToString(true);

                }


                /*
                /// <summary>
                /// Analiza la Estructura de Respuesta de la Consulta de Notificación y determina si corresponde o no
                /// al acto asociado a la persona, adicionalmente asigna el estado a la persona entregada
                /// </summary>
                /// <param name="XMLSalida">XML en GEL-XML con los Datos de Respuesta de PDI</param>
                /// <param name="persona">Objeto de la Persona asociada a la consutla</param>
                /// <param name="acto">Acto, o Notificación asociada a la persona</param>
                /// <returns>Verdadero si el acto y el numero de expediente corresponden al acto o proceso de notificación consultados</returns>
                public bool GetXMLGELEstadoNotificacionSalida(string XMLSalida, ref PersonaNotificarEntity persona, NotificacionEntity acto)
                {
                    try
                    {
                        tipoConsultaEstadoNotificacionSal.notnot.tipoConsultaEstadoNotificacionSal3 _objConsulta = tipoConsultaEstadoNotificacionSal.notnot.tipoConsultaEstadoNotificacionSal3.LoadFromString(XMLSalida);
                        tipoConsultaEstadoNotificacionSal.notnot.tipoConsultaEstadoNotificacionSal2 _objRoot = _objConsulta.consultaEstadoNotificacionSal.First;

                        string numeroProcesoAdministracion = _objRoot.numProcesoAdministracion.First.Value;
                        string numeroActo = _objRoot.numActoAdministrativoNotificacion.First.Value;
                        DateTime fechaEmisionActo = _objRoot.fechaEmisionActoAdministrativo.First.Value.Value;
                        tipoConsultaEstadoNotificacionSal.locgen.tipoEstadoNotificacion estado = _objRoot.estadoConsultaNotificacion.First;
                        int codigoEstado = Convert.ToInt32(estado.codEstadoProcesoNotificacion.First.Value);
                        if (estado.nomEstadoProcesoNotificacion.Exists)
                        {
                            string nombreEstado = estado.nomEstadoProcesoNotificacion.First.Value;
                            persona.EstadoNotificado.Estado = nombreEstado;
                        }
                        int entidadPublica = _objRoot.entidadPublicaNotificacion.First.Value;
                        //StringBuilder str = new StringBuilder();
                        //str.Append("La persona: " + persona.PrimerNombre + " " + persona.PrimerApellido);
                        //str.Append(" se encuentra en estado: " + codigoEstado + " - " + nombreEstado);
                        //str.Append(" para el acto:" + numeroActo);
                        //str.Append(" del expediente:" + numeroProcesoAdministracion);
                        //str.Append(" en la entidad:" + entidadPublica);
                        //str.Append(" emitido en:" + fechaEmisionActo.ToString());
                        //return str.ToString();
                        persona.EstadoNotificado.ID = codigoEstado;

                        if (!acto.NumeroActoAdministrativo.Equals(numeroActo) || !acto.ProcesoAdministracion.Equals(numeroProcesoAdministracion))
                            return false;
                        else
                            return true;
                    }
                    catch
                    {
                        //Escribir Log
                        return false;
                    }
                }
                */


                public bool GetXMLGELEstadoNotificacionSalida(string XMLSalida, ref PersonaNotificarEntity persona, NotificacionEntity acto)
                {
                    // JALCALA 2010-07-26 Había un try catch que ignoraba el error si ocurría.  Se eliminó porque oculta errores que sí deben ser corregidos
                    tipoConsultaEstadoNotificacionSal.notnot.tipoConsultaEstadoNotificacionSal3 _objConsulta = tipoConsultaEstadoNotificacionSal.notnot.tipoConsultaEstadoNotificacionSal3.LoadFromString(XMLSalida);
                    tipoConsultaEstadoNotificacionSal.notnot.tipoConsultaEstadoNotificacionSal2 _objRoot = _objConsulta.consultaEstadoNotificacionSal.First;

                    string numeroProcesoAdministracion = _objRoot.numProcesoAdministracion.First.Value;
                    string numeroActo = _objRoot.numActoAdministrativoNotificacion.First.Value;
                    DateTime fechaEmisionActo = _objRoot.fechaEmisionActoAdministrativo.First.Value.Value;
                    tipoConsultaEstadoNotificacionSal.locgen.tipoEstadoNotificacion estado = _objRoot.estadoConsultaNotificacion.First;
                    int codigoEstado = Convert.ToInt32(estado.codEstadoProcesoNotificacion.First.Value);
                    if (estado.nomEstadoProcesoNotificacion.Exists)
                    {
                        string nombreEstado = estado.nomEstadoProcesoNotificacion.First.Value;
                        persona.EstadoNotificado.Estado = nombreEstado;
                    }
                    int entidadPublica = _objRoot.entidadPublicaNotificacion.First.Value;
                    //StringBuilder str = new StringBuilder();
                    //str.Append("La persona: " + persona.PrimerNombre + " " + persona.PrimerApellido);
                    //str.Append(" se encuentra en estado: " + codigoEstado + " - " + nombreEstado);
                    //str.Append(" para el acto:" + numeroActo);
                    //str.Append(" del expediente:" + numeroProcesoAdministracion);
                    //str.Append(" en la entidad:" + entidadPublica);
                    //str.Append(" emitido en:" + fechaEmisionActo.ToString());
                    //return str.ToString();
                    persona.EstadoNotificado.ID = codigoEstado;

                    if (!acto.NumeroActoAdministrativo.Equals(numeroActo) || !acto.ProcesoAdministracion.Equals(numeroProcesoAdministracion))
                    {
                        // JALCALA 2010-07-26 Se agregó el log porque en teoría siempre deberían coincidir estos valores aunque el código hace la validación
                        // es necesario hacer seguimiento al log generado en pruebas
                        //SMLog.Escribir((Severidad.Critico, string.Format("El número de acto o proceso no coinciden en la respuesta de GetXMLGELEstadoNotificacionSalida.  Valor original '{0}' '{1}'.  Valor recibido '{2}' '{3}'.", acto.NumeroActoAdministrativo, acto.ProcesoAdministracion, numeroActo, numeroProcesoAdministracion));
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                /// <summary>
                /// Genera el XML que se enviará en formato GEL-XML a PDI para ejecutoriar un Acto Administrativo
                /// </summary>
                /// <param name="acto">Objeto con los datos del Acto Administrativo</param>
                /// <param name="notificacionXMLAA">Objeto con datos adicionales (archivo) que se obtiene de la AA</param>
                /// <returns>Cadena XML para enviar a PDI</returns>
                private string SetXMLGELEjecutoriarActoEntrada(NotificacionEntity acto, NotificacionType notificacionXMLAA)
                {
                    tipoEjecutoriarActoAdministratiEnt.notnot.tipoEjecutoriarActoAdministratiEnt3 _objEjecutoriar = tipoEjecutoriarActoAdministratiEnt.notnot.tipoEjecutoriarActoAdministratiEnt3.CreateDocument();
                    tipoEjecutoriarActoAdministratiEnt.notnot.tipoEjecutoriarActoAdministratiEnt2 _objRoot = _objEjecutoriar.ejecutoriarActoAdministratiEnt.Append();

                    //---------------------------- Entidad Pública ---------------------------------------------------
                    tipoEjecutoriarActoAdministratiEnt.locorg.tipoCodUnicoIdEntidadPublicaType codigoEntidad = _objRoot.entidadPublicaNotificacion.Append();
                    codigoEntidad.Value = acto.CodigoEntidadPublica;

                    //---------------------------- Dependencia Entidad ---------------------------------------------------
                    tipoEjecutoriarActoAdministratiEnt.locorg.tipoDependenciaEntidad dependenciaEntidad = _objRoot.dependenciaEntidadNotificacion.Append();
                    tipoEjecutoriarActoAdministratiEnt.locorg.tipoIdDependenciaEntidadType idDependencia = dependenciaEntidad.idDependenciaEntidadNotificacion.Append();
                    idDependencia.Value = acto.DependenciaEntidad.ID;
                    tipoEjecutoriarActoAdministratiEnt.locorg.tipoNomDependenciaEntidadType nombreDependencia = dependenciaEntidad.nomDependenciaEntidadNotificacion.Append();
                    nombreDependencia.Value = acto.DependenciaEntidad.Nombre;

                    //---------------------------- Sistema Entidad ---------------------------------------------------
                    tipoEjecutoriarActoAdministratiEnt.notcor.tipoIdSistemaEntidadType sistemaEntidad = _objRoot.sistemaEntidadPublicaNotificacion.Append();
                    sistemaEntidad.Value = acto.SistemaEntidadPublica;

                    //---------------------------- Archivo (para ejecutoriar, esta parte no se desarrollará - JDG 05-10-2010)---------------------------------------------------
                    //tipoEjecutoriarActoAdministratiEnt.comdoc.tipoArchivoAdjuntoTramitadorLinea archivo = _objRoot.datoArchivoAdjuntoActoAdministrativo.Append();
                    ////Nombre del Archivo
                    //archivo.nomArchivoAdjunto.Append().Value = notificacionXMLAA.nombreArchivo;
                    ////Tipo de Archivo
                    //tipoEjecutoriarActoAdministratiEnt.comdoc.tipoTipoArchivo tipoArchivo = archivo.tipoArchivoAdjunto2.Append();
                    //string formatoArchivo = System.IO.Path.GetExtension(notificacionXMLAA.nombreArchivo);
                    //formatoArchivo = formatoArchivo.Remove(0, 1);
                    //tipoArchivo.codTipoArchivo.Append().Value = formatoArchivo.ToUpper();
                    //switch (formatoArchivo.ToUpper())
                    //{
                    //    case "TXT":
                    //        tipoArchivo.nomTipoArchivo.Append().Value = "TEXTO PLANO";
                    //        break;
                    //    case "ZIP":
                    //        tipoArchivo.nomTipoArchivo.Append().Value = "ARCHIVO COMPRIMIDO EN FORMATO ZIP";
                    //        break;
                    //    case "RAR":
                    //        tipoArchivo.nomTipoArchivo.Append().Value = "ARCHIVO COMPRIMIDO EN FORMATO RAR";
                    //        break;
                    //    case "DOC":
                    //        tipoArchivo.nomTipoArchivo.Append().Value = "DOCUMENTOS DE TEXTO ENRIQUECIDOS";
                    //        break;
                    //    case "PDF":
                    //        tipoArchivo.nomTipoArchivo.Append().Value = "DOCUMENTO DE FORMATO PORTATIL";
                    //        break;
                    //    case "CSV":
                    //        tipoArchivo.nomTipoArchivo.Append().Value = "FORMATO USADO PARA VALORES SEPARADOS POR COMAS";
                    //        break;
                    //    case "XML":
                    //        tipoArchivo.nomTipoArchivo.Append().Value = "FORMATO LENGUAJE DE MARCAS EXTENSIBLE";
                    //        break;
                    //    case "RTF":
                    //        tipoArchivo.nomTipoArchivo.Append().Value = "FORMATO DE TEXTO ENRIQUECIDO";
                    //        break;
                    //    case "P7Z":
                    //        tipoArchivo.nomTipoArchivo.Append().Value = "ARCHIVO FIRMADO DIGITALMENTE";
                    //        break;
                    //    default:
                    //        tipoArchivo.codTipoArchivo.Append().Value = "TXT";
                    //        tipoArchivo.nomTipoArchivo.Append().Value = "TEXTO PLANO";
                    //        break;
                    //}

                    ////Se agrega valor por defecto
                    //archivo.contenido.Append().pdidoc.Value = 1M;

                    //---------------------------- Número de Acto ---------------------------------------------------
                    tipoEjecutoriarActoAdministratiEnt.locdoc.tipoNumActoAdministrativoType numeroActo = _objRoot.numActoAdministrativoNotificacion.Append();
                    numeroActo.Value = acto.NumeroActoAdministrativo;

                    //---------------------------- Número de Proceso de Administración (Expediente) ---------------------------------------------------
                    tipoEjecutoriarActoAdministratiEnt.locdoc.tipoNumExpedienteType numeroProceso = _objRoot.numProcesoAdministracion.Append();
                    numeroProceso.Value = acto.ProcesoAdministracion;

                    return _objEjecutoriar.SaveToString(true);
                }

                /// <summary>
                /// Captura los Datos de la respuesta de Salida del proceso de Ejecutoria, en caso de que el código de estado
                /// de Respuesta sea diferente al asignado a CON_ERROR, se actualiza el estado en todas las personas del acto
                /// </summary>
                /// <param name="XMLSalida">Cadena con XML de Respuesta del Trámite</param>
                /// <param name="acto">Objeto con información del acto y la personas</param>
                /// <returns>mensaje de confirmación de PDI</returns>
                private string GetXMLGELEjecutoriarActoSalida(string XMLSalida, ref NotificacionEntity acto, out bool resultado)
                {
                    string mensaje = string.Empty;
                    try
                    {
                        tipoEjecutoriarActoAdministratiSal.notnot.tipoEjecutoriarActoAdministratiSal3 _objEjecutoriar = tipoEjecutoriarActoAdministratiSal.notnot.tipoEjecutoriarActoAdministratiSal3.LoadFromString(XMLSalida);
                        tipoEjecutoriarActoAdministratiSal.notnot.tipoEjecutoriarActoAdministratiSal2 _objRoot = _objEjecutoriar.ejecutoriarActoAdministratiSal.First;

                        tipoEjecutoriarActoAdministratiSal.locgen.tipoEstadoNotificacion estadoActo = _objRoot.estadoConsultaNotificacion.First;
                        string codigoEstado = estadoActo.codEstadoProcesoNotificacion.First.Value;
                        //string nombreEstado = estadoActo.nomEstadoProcesoNotificacion.First.Value;
                        string numeroProceso = _objRoot.numProcesoAdministracion.First.Value;
                        string numeroActo = _objRoot.numActoAdministrativoNotificacion.First.Value;
                        mensaje = _objRoot.mensajeConfirmacionNotificacion.First.Value;
                        ParametroDalc _parametroDalc = new ParametroDalc();
                        ParametroEntity _parametro = new ParametroEntity();
                        _parametro.NombreParametro = "CON_ERROR";
                        _parametroDalc.obtenerParametros(ref _parametro);
                        if (!codigoEstado.Equals(_parametro.Parametro))
                        {
                            PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
                            foreach (PersonaNotificarEntity persona in acto.ListaPersonas)
                            {
                                PersonaNotificarEntity personaActual = new PersonaNotificarEntity();
                                personaActual = persona;
                                personaActual.EstadoNotificado.ID = Convert.ToInt32(codigoEstado);
                                _personaDalc.Actualizar(ref personaActual);
                            }
                            resultado = true;
                        }
                        else
                        {
                            resultado = false;
                        }
                    }
                    catch
                    {
                        mensaje = "Error en la respuesta de Notificación:" + XMLSalida;
                        resultado = false;
                    }
                    return mensaje;
                }

                private bool SetXMLGELRevocarActoEntrada(NotificacionEntity acto, NotificacionType notificacionXMLAA)
                {

                    tipoDatoRevocatoriaNotificacionEnt.notnot.tipoDatoRevocatoriaNotificacionEnt3 _objRevocatoria = tipoDatoRevocatoriaNotificacionEnt.notnot.tipoDatoRevocatoriaNotificacionEnt3.CreateDocument();
                    tipoDatoRevocatoriaNotificacionEnt.notnot.tipoDatoRevocatoriaNotificacionEnt2 _objRoot = _objRevocatoria.datoRevocatoriaNotificacionEnt.Append();

                    //---------------------------- Número de Acto ---------------------------------------------------
                    tipoDatoRevocatoriaNotificacionEnt.locdoc.tipoNumActoAdministrativoType numeroActo = _objRoot.numActoAdministrativoRevocatoria.Append();
                    numeroActo.Value = acto.NumeroActoAdministrativo;

                    //---------------------------- Número de Proceso de Administración (Expediente) ---------------------------------------------------
                    tipoDatoRevocatoriaNotificacionEnt.locdoc.tipoNumExpedienteType numeroProceso = _objRoot.numProcesoAdministracion.Append();
                    numeroProceso.Value = acto.ProcesoAdministracion;

                    //--------------------------------- Entidad Pública -------------------------------------
                    tipoDatoRevocatoriaNotificacionEnt.locorg.tipoCodUnicoIdEntidadPublicaType entidadPublica = _objRoot.entidadPublicaNotificacion.Append();
                    entidadPublica.Value = acto.CodigoEntidadPublica;

                    //---------------------------- Dependencia Entidad ---------------------------------------------------
                    tipoDatoRevocatoriaNotificacionEnt.locorg.tipoDependenciaEntidad dependenciaEntidad = _objRoot.dependenciaEntidadNotificacion.Append();
                    tipoDatoRevocatoriaNotificacionEnt.locorg.tipoIdDependenciaEntidadType idDependencia = dependenciaEntidad.idDependenciaEntidadNotificacion.Append();
                    idDependencia.Value = acto.DependenciaEntidad.ID;
                    tipoDatoRevocatoriaNotificacionEnt.locorg.tipoNomDependenciaEntidadType nombreDependencia = dependenciaEntidad.nomDependenciaEntidadNotificacion.Append();
                    nombreDependencia.Value = acto.DependenciaEntidad.Nombre;

                    //---------------------------- Sistema Entidad ---------------------------------------------------
                    tipoDatoRevocatoriaNotificacionEnt.notcor.tipoIdSistemaEntidadType sistemaEntidad = _objRoot.sistemaEntidadPublicaNotificacion.Append();
                    sistemaEntidad.Value = acto.SistemaEntidadPublica;

                    //--------------------------- Observaciones ------------------------------------------------------
                    tipoDatoRevocatoriaNotificacionEnt.tipdat.tipoCadena2048Type observaciones = _objRoot.observacionRevocatoriaNotificacion.Append();
                    observaciones.Value = notificacionXMLAA.parteResolutiva;



                    return false;
                }

            #endregion

            #region Temporales para Pruebas

                private string ConstruirXMLGELRespuestaCrearProcesoPrueba()
                {
                    tipoDatoProcesoNotificacionSal.notnot.tipoDatoProcesoNotificacionSal3 _objDoc = tipoDatoProcesoNotificacionSal.notnot.tipoDatoProcesoNotificacionSal3.CreateDocument();
                    tipoDatoProcesoNotificacionSal.notnot.tipoDatoProcesoNotificacionSal2 _objRoot = _objDoc.datoProcesoNotificacionSal.Append();
                    //---------------------------------------------------------------------------------------------------------------------------------------
                    tipoDatoProcesoNotificacionSal.locdoc.tipoNumActoAdministrativoType numeroActoAdminsitrativo = _objRoot.numActoAdministrativoNotificacion.Append();
                    numeroActoAdminsitrativo.Value = "ACT-1771";
                    //---------------------------------------------------------------------------------------------------------------------------------------
                    tipoDatoProcesoNotificacionSal.locdoc.tipoNumExpedienteType numeroProcesoAdministracion = _objRoot.numProcesoAdministracion.Append();
                    numeroProcesoAdministracion.Value = "DAA-1771";
                    //---------------------------------------------------------------------------------------------------------------------------------------
                    tipoDatoProcesoNotificacionSal.tipdat.tipoCadena512Type mensajeConfirmacion = _objRoot.mensajeConfirmacionNotificacion.Append();
                    mensajeConfirmacion.Value = "MENSAJE CONFIRMACIÓN DE PRUEBA";
                    //---------------------------------------------------------------------------------------------------------------------------------------
                    tipoDatoProcesoNotificacionSal.notnot.tipoListaDatoProcesoNotificacion lista1 = _objRoot.datoListaProcesoNotificacion.Append();
                    tipoDatoProcesoNotificacionSal.notnot.tipoDatoProcesoNotificacion internoLista1 = lista1.listadoDatoProcesoNotificacion.Append();
                    //---------------------------------------------------------------------------------------------------------------------------------------
                    tipoDatoProcesoNotificacionSal.locgen.tipoEstadoNotificacion estado1 = internoLista1.estadoProcesoNotificacion.Append();
                    tipoDatoProcesoNotificacionSal.locgen.enumCodEstadoNotificacionType enumEstado1 = estado1.codEstadoProcesoNotificacion.Append();
                    enumEstado1.Value = "1";
                    tipoDatoProcesoNotificacionSal.locgen.enumNomEstadoNotificacionType enumNombre1 = estado1.nomEstadoProcesoNotificacion.Append();
                    enumNombre1.Value = "PENDIENTE_DE_ACUSE_DE_NOTIFICACION";
                    //---------------------------------------------------------------------------------------------------------------------------------------

                    return _objDoc.SaveToString(true);
                }

                public string ConstruirXMLGELPrueba(string XMLDatos)
                {
                    // NotificacionType _xmlNotificacion = new NotificacionType();
                    //XmlSerializador _ser = new XmlSerializador();
                    //_xmlNotificacion = (NotificacionType)_ser.Deserializar(new NotificacionType(), documentoXML);

                    tipoDatoProcesoNotificacionEnt.notnot.tipoDatoProcesoNotificacionEnt3 _objDoc = tipoDatoProcesoNotificacionEnt.notnot.tipoDatoProcesoNotificacionEnt3.CreateDocument();
                    tipoDatoProcesoNotificacionEnt.notnot.tipoDatoProcesoNotificacionEnt2 _objRoot = _objDoc.datoProcesoNotificacionEnt.Append();

                    //------------------------DATOS DEL ARCHIVO (archivo ingresado por PDI, no viene incluido en este documento)-----------------
                    tipoDatoProcesoNotificacionEnt.comdoc.tipoArchivoAdjuntoTramitadorLinea archivo = _objRoot.datoArchivoAdjuntoActoAdministrativo.Append();
                    //Nombre del Archivo
                    archivo.nomArchivoAdjunto.Append().Value = "Nombre del Archivo";
                    //Tipo de Archivo
                    tipoDatoProcesoNotificacionEnt.comdoc.tipoTipoArchivo tipoArchivo = archivo.tipoArchivoAdjunto2.Append();
                    tipoArchivo.codTipoArchivo.Append().Value = "TXT";
                    tipoArchivo.nomTipoArchivo.Append().Value = "TEXTO_PLANO";
                    archivo.contenido.Append().pdidoc.Value = 1M;
                    //-----------------------------------------------------------------------

                    //------------------Número de Acto Administrativo------------------------
                    tipoDatoProcesoNotificacionEnt.locdoc.tipoNumActoAdministrativoType numeroActo = _objRoot.numActoAdministrativoNotificacion.Append();
                    numeroActo.Value = "ACT01";
                    //------------------------------------------------------------------------

                    //------------------Número de Proceso Administración ------------------------
                    tipoDatoProcesoNotificacionEnt.locdoc.tipoNumExpedienteType numeroExpediente = _objRoot.numProcesoAdministracion.Append();
                    numeroExpediente.Value = "EXP-01";
                    //------------------------------------------------------------------------------

                    //------------------Parte Resolutiva (Descripción del Acto) ------------------------
                    tipoDatoProcesoNotificacionEnt.locdoc.tipoTextoActoAdministrativoType parteResolutiva = _objRoot.parteResolutiva.Append();
                    parteResolutiva.Value = "Descripción del Acto Administrativo de Prueba";
                    //------------------------------------------------------------------------------

                    //------------------Entidad Publica Notificacion ------------------------
                    tipoDatoProcesoNotificacionEnt.locorg.tipoCodUnicoIdEntidadPublicaType entidadPublica = _objRoot.entidadPublicaNotificacion.Append();
                    entidadPublica.Value = 11000001;
                    //------------------------------------------------------------------------------

                    //------------------Dependencia Entidad ----------------------------------------
                    tipoDatoProcesoNotificacionEnt.locorg.tipoDependenciaEntidad dependenciaEntidad = _objRoot.dependenciaEntidadNotificacion.Append();
                    dependenciaEntidad.idDependenciaEntidadNotificacion.Append().Value = "IDEntidad";
                    dependenciaEntidad.nomDependenciaEntidadNotificacion.Append().Value = "Nombre Dependencia Entidad";
                    //------------------------------------------------------------------------------

                    //------------------Sistema Entidad---------------------------------------------
                    tipoDatoProcesoNotificacionEnt.notcor.tipoIdSistemaEntidadType sistemaEntidad = _objRoot.sistemaEntidadPublicaNotificacion.Append();
                    sistemaEntidad.Value = 15;
                    //------------------------------------------------------------------------------

                    //------------------Funcionario Notificante ----------------------------------------
                    tipoDatoProcesoNotificacionEnt.locide.tipoIdentificacionNacionalPersona funcionario = _objRoot.idFuncionarioNotificante.Append();
                    funcionario.tipoIdentificacionNacionalPersona2.Append().codTipoIdentificacion.Append().Value = "CC";
                    funcionario.tipoIdentificacionNacionalPersona2.Append().nomTipoIdentificacion.Append().Value = "CEDULA_CIUDADANIA";
                    funcionario.grupoNumeroIdentificacion2.Append().numeroCedulaCiudadania.Append().Value = "10203030503";
                    //------------------------------------------------------------------------------

                    //------------------Lista personas Notificar----------------------------------------
                    tipoDatoProcesoNotificacionEnt.mndgen.tipoListaPersonaNotificar listaPersonas = _objRoot.listaPersonaEnvioNotificacion.Append();
                    tipoDatoProcesoNotificacionEnt.mndgen.tipoPersonaNotificar Persona1 = listaPersonas.listadoPersonaNotificar.Append();
                    Persona1.tipoPersonaNotificar2.Append().codTipoPersona.Append().Value = "1";
                    Persona1.tipoPersonaNotificar2.Append().nomTipoPersona.Append().Value = "NATURAL";
                    tipoDatoProcesoNotificacionEnt.locide.tipoIdentificacionNacionalPersona idPersona = Persona1.idPersonaNotificar.Append();
                    idPersona.tipoIdentificacionNacionalPersona2.Append().codTipoIdentificacion.Append().Value = "CC";
                    idPersona.tipoIdentificacionNacionalPersona2.Append().nomTipoIdentificacion.Append().Value = "CEDULA_CIUDADANIA";
                    idPersona.grupoNumeroIdentificacion2.Append().numeroCedulaCiudadania.Append().Value = "12345";
                    Persona1.nombrePersonaNotificar.Append().primerNombre.Append().Value = "Juan";
                    Persona1.nombrePersonaNotificar.Append().segundoNombre.Append().Value = "Carlos";
                    Persona1.nombrePersonaNotificar.Append().primerApellido.Append().Value = "Méndez";
                    Persona1.nombrePersonaNotificar.Append().segundoApellido.Append().Value = "Rodríguez";
                    Persona1.razonSocialPersonaNotificar.Append().Value = "Razón Social Persona";
                    //------------------------------------------------------------------------------

                    //------------------Plantilla ----------------------------------------
                    tipoDatoProcesoNotificacionEnt.notnot.tipoPlantillaProceso plantilla = _objRoot.plantillaProcesoNotificacion.Append();
                    plantilla.idPlantillaProcesoNotificacion.Append().Value = "1023";
                    plantilla.nomPlantillaProcesoNotificacion.Append().Value = "Plantilla Pruebas";
                    //------------------------------------------------------------------------------

                    //------------------Número Acto Administrativo Asociado ----------------------------------------
                    tipoDatoProcesoNotificacionEnt.locdoc.tipoNumActoAdministrativoType actoAsociado = _objRoot.numActoAdministrativoAsociado.Append();
                    actoAsociado.Value = "ACT-ASC01";
                    //------------------------------------------------------------------------------

                    return _objDoc.SaveToString(true);

                }

            #endregion

            private void Escribir(string mensaje)
            {
                if (System.IO.Directory.Exists(@"e:\temp"))
                    using (System.IO.StreamWriter esc = new System.IO.StreamWriter(@"e:\temp\gelxml_" + DateTime.Now.Date.ToString("yyyyMMdd") + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + ".dat", true))
                    {
                        //esc.WriteLine(DateTime.Now.Date.ToShortTimeString() + ": Archivo");
                        esc.WriteLine(DateTime.Now.Date.ToShortTimeString() + ": " + mensaje);
                    }
            }


            /// <summary>
            /// Obtener la información necesaria para generar documento PDF
            /// </summary>
            /// <param name="p_lngIdActo">long con la identificación del acto administrativo</param>
            /// <param name="p_intPlantillaID">int con la identificación de la plantilla</param>
            /// <param name="p_intIdPersona">int con la identificación de la persona</param>
            /// <param name="p_intAutoridadID">int con la identificación de la autoridad ambiental</param>
            /// <param name="p_intFlujoID">int con el identificador del flujo</param>
            /// <param name="p_intIdEstado">int con el identificador del estado</param>
            /// <param name="p_intIdUsuario">int con el identificador del usuario que se encuentra avanzando la actividad</param>            
            /// <param name="p_blConsultarConceptos">bool que indica si se consulta información de conceptos asociados. Opcional</param>
            /// <returns>DataSet con la información</returns>
            private DataSet ConsultarInformacionActoNotificacion(long p_lngIdActo, int p_intPlantillaID, long p_intIdPersona, int p_intAutoridadID,
                                                                 int p_intFlujoID, int p_intIdEstado, int p_intIdUsuario, int p_intFirmaID = 0, bool p_blConsultarConceptos = false)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ConsultarInformacionActoNotificacion(p_lngIdActo, p_intPlantillaID, p_intIdPersona, p_intAutoridadID, p_intFlujoID, p_intIdEstado, p_intIdUsuario, p_intFirmaID, p_blConsultarConceptos);
            }


            /// <summary>
            /// Cargar la información del acto en DataSet para mostrar plantilla
            /// </summary>
            /// <param name="p_lngIdActo">long con el identifcador del acto administrativo</param>
            /// <param name="p_lngIdPersona">long con el idenftificador de la persona</param>
            /// <param name="p_objDatosActo">DataSet con la informacion del acto</param>
            /// <param name="p_objFechaEstadoNuevo">DateTime con la fecha de realización del nuevo estado</param>
            /// <param name="p_strReferenciaEstado">string con la referencia capturada en el estado</param>
            /// <param name="p_objFechaReferencia">Datetime con la fecha de referencia capturada</param>
            /// <param name="p_strObservacionEstado">string con la observación del estado</param>
            /// <param name="p_intTipoIdentficacionPersonaNotificar">int con el tipo de identificación persona notificada</param>
            /// <param name="p_strNumeroIdentificacionPersonaNotificar">string con el número de identificacion persona notificada</param>
            /// <param name="p_strNombrePersonaNotificar">string con el nombre de la persona notificada</param>
            /// <param name="p_strCalidadPersonaNotificar">string con la calidad de la persona notificada</param>
            /// <param name="p_objPlantilla">PlantillaNotificacionEntity con la información de la plantilla</param>
            /// <param name="p_objMarcas">List con la información de las marcas a reemplazar</param>
            /// <param name="p_intIdUsuario">int con el usuario que se encuentra generando la plantilla</param>
            /// <param name="p_objPlantillaNotificacion">NOTPlantilla por referencia que contendrá la plantilla a generar</param>
            /// <returns>DataSet con la información generación plantilla</returns>
            private DataSet CargarDataSetPlantilla(long p_lngIdActo, long p_lngIdPersona,
                                                   DataSet p_objDatosActo, DateTime p_objFechaEstadoNuevo, string p_strReferenciaEstado, DateTime p_objFechaReferencia, string p_strObservacionEstado,
                                                   int p_intTipoIdentficacionPersonaNotificar, string p_strNumeroIdentificacionPersonaNotificar, string p_strNombrePersonaNotificar, string p_strCalidadPersonaNotificar,
                                                   List<DireccionNotificacionEntity> p_lstDirecciones, List<CorreoNotificacionEntity> p_lstCorreos,
                                                   PlantillaNotificacionEntity p_objPlantilla, List<MarcaPlantillaNotificacionEntity> p_objMarcas, ref NOTPlantilla p_objPlantillaNotificacion)
            {
                DataSet objInformacionPlantilla = null;
                DataRow objDatos = null;
                NotificacionDalc objNotificacionDalc = null;
                NOT_TipoIdentificacion objTipoIdentificacion = null;
                DataTable objTipoIdentificacionDatos = null;
                DateTime objFechaActual = DateTime.Now;

                try
                {
                    //Crear DataSet
                    objInformacionPlantilla = new DSNotificacion();

                    //Adicionar Datos Basicos
                    p_objDatosActo.Tables.Add("DATOS_BASICOS");
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_HORA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_NOMBRE_DIA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_NOMBRE_MES", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_SOLO_HORA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_SOLO_AÑO", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_SOLO_DIA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_SOLO_MES", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_DIA_LETRAS", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_AÑO_LETRAS", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_LARGA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_ESTADO", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_ESTADO_LARGA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_ESTADO_HORA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_ESTADO_NOMBRE_DIA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_ESTADO_NOMBRE_MES", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_ESTADO_SOLO_HORA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_ESTADO_SOLO_AÑO", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_ESTADO_SOLO_DIA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_ESTADO_SOLO_MES", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_ESTADO_DIA_SIGUIENTE", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("REFERENCIA_ESTADO", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_REFERENCIA_ESTADO", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("FECHA_REFERENCIA_ESTADO_LARGA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("OBSERVACION", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("TIPO_IDENTIFICACION_PERSONA_NOTIFICAR", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("IDENTIFICACION_PERSONA_NOTIFICAR", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("NOMBRE_PERSONA_NOTIFICAR", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("CALIDAD_PERSONA_NOTIFICAR", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("CORREO_CAPTURADO", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("DIRECCION_CAPTURADA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("DEPARTAMENTO_CIUDAD_CAPTURADA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["DATOS_BASICOS"].Columns.Add("LINK_DOCUMENTOS_ACTO", Type.GetType("System.String"));
                    objDatos = p_objDatosActo.Tables["DATOS_BASICOS"].NewRow();
                    objDatos["FECHA"] = objFechaActual.ToString("dd/MM/yyyy");
                    objDatos["FECHA_HORA"] = objFechaActual.ToString("dd/MM/yyyy") + " a las " + objFechaActual.ToString("HH:mm:ss");
                    objDatos["FECHA_NOMBRE_DIA"] = objFechaActual.ToString("dddd");
                    objDatos["FECHA_NOMBRE_DIA"] = objDatos["FECHA_NOMBRE_DIA"].ToString().Substring(0, 1).ToUpper() + objDatos["FECHA_NOMBRE_DIA"].ToString().Substring(1);
                    objDatos["FECHA_NOMBRE_MES"] = objFechaActual.ToString("MMMM");
                    objDatos["FECHA_NOMBRE_MES"] = (objDatos["FECHA_NOMBRE_MES"].ToString().Substring(0, 1).ToUpper() + objDatos["FECHA_NOMBRE_MES"].ToString().Substring(1)).ToLower();
                    objDatos["FECHA_SOLO_HORA"] = objFechaActual.ToString("HH:mm:ss");
                    objDatos["FECHA_SOLO_AÑO"] = objFechaActual.ToString("yyyy");
                    objDatos["FECHA_SOLO_DIA"] = objFechaActual.ToString("dd");
                    objDatos["FECHA_SOLO_MES"] = objFechaActual.ToString("MM");
                    objDatos["FECHA_LARGA"] = objFechaActual.ToString("dd") + " de " + objFechaActual.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-CO")) + " de " + objFechaActual.ToString("yyyy");
                    objDatos["FECHA_DIA_LETRAS"] = Utilidades.NumeroALetras(objFechaActual.ToString("dd"));
                    objDatos["FECHA_AÑO_LETRAS"] = Utilidades.NumeroALetras(objFechaActual.ToString("yyyy"));
                    objDatos["FECHA_ESTADO"] = p_objFechaEstadoNuevo.ToString("dd/MM/yyyy");
                    objDatos["FECHA_ESTADO_LARGA"] = p_objFechaEstadoNuevo.ToString("dd") + " de " + p_objFechaEstadoNuevo.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-CO")) + " de " + p_objFechaEstadoNuevo.ToString("yyyy");
                    objDatos["FECHA_ESTADO_HORA"] = p_objFechaEstadoNuevo.ToString("dd/MM/yyyy") + " a las " + p_objFechaEstadoNuevo.ToString("HH:mm:ss"); ;
                    objDatos["FECHA_ESTADO_NOMBRE_DIA"] = p_objFechaEstadoNuevo.ToString("dddd");
                    objDatos["FECHA_ESTADO_NOMBRE_DIA"] = objDatos["FECHA_ESTADO_NOMBRE_DIA"].ToString().Substring(0, 1).ToUpper() + objDatos["FECHA_ESTADO_NOMBRE_DIA"].ToString().Substring(1);
                    objDatos["FECHA_ESTADO_NOMBRE_MES"] = p_objFechaEstadoNuevo.ToString("MMMM");
                    objDatos["FECHA_ESTADO_NOMBRE_MES"] = objDatos["FECHA_ESTADO_NOMBRE_MES"].ToString().Substring(0, 1).ToUpper() + objDatos["FECHA_ESTADO_NOMBRE_MES"].ToString().Substring(1);
                    objDatos["FECHA_ESTADO_SOLO_HORA"] = p_objFechaEstadoNuevo.ToString("HH:mm");
                    objDatos["FECHA_ESTADO_SOLO_AÑO"] = p_objFechaEstadoNuevo.ToString("yyyy");
                    objDatos["FECHA_ESTADO_SOLO_DIA"] = p_objFechaEstadoNuevo.ToString("dd");
                    objDatos["FECHA_ESTADO_SOLO_MES"] = p_objFechaEstadoNuevo.ToString("MM");
                    objDatos["OBSERVACION"] = (p_strObservacionEstado != null ? p_strObservacionEstado : "");
                    if (p_intTipoIdentficacionPersonaNotificar > 0)
                    {
                        //Consultar descripción tipo de identificación
                        objTipoIdentificacion = new NOT_TipoIdentificacion();
                        objTipoIdentificacionDatos = objTipoIdentificacion.Consultar_Tipo_Identificacion(p_intTipoIdentficacionPersonaNotificar);
                        if (objTipoIdentificacionDatos != null && objTipoIdentificacionDatos.Rows.Count > 0)
                        {
                            objDatos["TIPO_IDENTIFICACION_PERSONA_NOTIFICAR"] = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(objTipoIdentificacionDatos.Rows[0]["NTI_DESCRIPCION"].ToString().Trim().ToLower());
                        }
                        else
                        {
                            objDatos["TIPO_IDENTIFICACION_PERSONA_NOTIFICAR"] = "";
                        }
                    }
                    else
                    {
                        objDatos["TIPO_IDENTIFICACION_PERSONA_NOTIFICAR"] = "";
                    }
                    objDatos["IDENTIFICACION_PERSONA_NOTIFICAR"] = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p_strNumeroIdentificacionPersonaNotificar.ToLower());
                    objDatos["NOMBRE_PERSONA_NOTIFICAR"] = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p_strNombrePersonaNotificar.ToLower());
                    objDatos["CALIDAD_PERSONA_NOTIFICAR"] = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p_strCalidadPersonaNotificar.ToLower());

                    //Agregar link
                    if ((!string.IsNullOrEmpty(p_objPlantilla.Encabezado) && p_objPlantilla.Encabezado.Contains("{LINK_DOCUMENTOS_ACTO}")) ||
                        (!string.IsNullOrEmpty(p_objPlantilla.Cuerpo) && p_objPlantilla.Cuerpo.Contains("{LINK_DOCUMENTOS_ACTO}")) ||
                        (!string.IsNullOrEmpty(p_objPlantilla.Pie) && p_objPlantilla.Pie.Contains("{LINK_DOCUMENTOS_ACTO}")) ||
                        (!string.IsNullOrEmpty(p_objPlantilla.PieFirma) && p_objPlantilla.PieFirma.Contains("{LINK_DOCUMENTOS_ACTO}")))
                    {
                        objDatos["LINK_DOCUMENTOS_ACTO"] = this.GenerarEnlacePlantillaNotificacion(p_lngIdActo, p_lngIdPersona);
                    }
                    else
                    {
                        objDatos["LINK_DOCUMENTOS_ACTO"] = "";
                    }

                    //Agregar fila
                    p_objDatosActo.Tables["DATOS_BASICOS"].Rows.Add(objDatos);

                    //Formatear fecha de acto
                    p_objDatosActo.Tables["ACTO"].Columns.Add("FECHA_ACTO_LARGA", Type.GetType("System.String"));
                    p_objDatosActo.Tables["ACTO"].Rows[0]["FECHA_ACTO_LARGA"] = Convert.ToDateTime(p_objDatosActo.Tables["ACTO"].Rows[0]["NOT_FECHA_ACTO_COMPLETA"]).ToString("dd") + " de " + Convert.ToDateTime(p_objDatosActo.Tables["ACTO"].Rows[0]["NOT_FECHA_ACTO_COMPLETA"]).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-CO")) + " de " + Convert.ToDateTime(p_objDatosActo.Tables["ACTO"].Rows[0]["NOT_FECHA_ACTO_COMPLETA"]).ToString("yyyy");

                    //Cargar fecha siguiente estado
                    objNotificacionDalc = new NotificacionDalc();
                    objDatos["FECHA_ESTADO_DIA_SIGUIENTE"] = objNotificacionDalc.CalcularFechaHabil(p_objFechaEstadoNuevo, 1).ToString("dd/MM/yyyy");
                    objDatos["REFERENCIA_ESTADO"] = p_strReferenciaEstado;

                    //JNS 20190911 - Se incorpora fecha de referencia
                    objDatos["REFERENCIA_ESTADO"] = p_strReferenciaEstado;
                    objDatos["FECHA_REFERENCIA_ESTADO"] = (p_objFechaReferencia != default(DateTime) ? p_objFechaReferencia.ToString("dd/MM/yyyy") : "");
                    objDatos["FECHA_REFERENCIA_ESTADO_LARGA"] = (p_objFechaReferencia != default(DateTime) ? p_objFechaReferencia.ToString("dd") + " de " + p_objFechaReferencia.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-CO")) + " de " + p_objFechaReferencia.ToString("yyyy") : "");

                    //Adicionar correos y direcciones
                    if (p_lstCorreos != null && p_lstCorreos.Count > 0)
                    {
                        //Ciclo que carga correos
                        objDatos["CORREO_CAPTURADO"] = "";
                        foreach (CorreoNotificacionEntity objCorreo in p_lstCorreos)
                        {
                            if (string.IsNullOrEmpty(objDatos["CORREO_CAPTURADO"].ToString()))
                                objDatos["CORREO_CAPTURADO"] = objCorreo.Correo;
                            else
                                objDatos["CORREO_CAPTURADO"] = objDatos["CORREO_CAPTURADO"].ToString() + "; " + objCorreo.Correo;
                        }
                    }
                    else
                    {
                        objDatos["CORREO_CAPTURADO"] = "-";
                    }
                    if (p_lstDirecciones != null && p_lstDirecciones.Count > 0)
                    {
                        objDatos["DIRECCION_CAPTURADA"] = p_lstDirecciones[0].Direccion;
                        objDatos["DEPARTAMENTO_CIUDAD_CAPTURADA"] = (p_lstDirecciones[0].Municipio.ToUpper().StartsWith("BOGOT") ? p_lstDirecciones[0].Municipio : p_lstDirecciones[0].Departamento + " / " + p_lstDirecciones[0].Municipio);
                    }
                    else
                    {
                        objDatos["DIRECCION_CAPTURADA"] = "-";
                        objDatos["DEPARTAMENTO_CIUDAD_CAPTURADA"] = "-";
                    }

                    //Cargar fecha de estado dependiente larga                    
                    if (p_objDatosActo.Tables.Contains("ESTADO_DEPENDIENTE"))
                    {
                        p_objDatosActo.Tables["ESTADO_DEPENDIENTE"].Columns.Add("FECHA_ESTADO_DEPENDIENTE_LARGA", Type.GetType("System.String"));

                        if (p_objDatosActo.Tables["ESTADO_DEPENDIENTE"].Rows[0]["FECHA_ESTADO_DEPENDIENTE"] != System.DBNull.Value && p_objDatosActo.Tables["ESTADO_DEPENDIENTE"].Rows[0]["FECHA_ESTADO_DEPENDIENTE"].ToString() != "")
                            p_objDatosActo.Tables["ESTADO_DEPENDIENTE"].Rows[0]["FECHA_ESTADO_DEPENDIENTE_LARGA"] = Convert.ToDateTime(p_objDatosActo.Tables["ESTADO_DEPENDIENTE"].Rows[0]["FECHA_ESTADO_DEPENDIENTE"]).ToString("dd") + " de " + Convert.ToDateTime(p_objDatosActo.Tables["ESTADO_DEPENDIENTE"].Rows[0]["FECHA_ESTADO_DEPENDIENTE"]).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-CO")) + " de " + Convert.ToDateTime(p_objDatosActo.Tables["ESTADO_DEPENDIENTE"].Rows[0]["FECHA_ESTADO_DEPENDIENTE"]).ToString("yyyy");
                        else
                            p_objDatosActo.Tables["ESTADO_DEPENDIENTE"].Rows[0]["FECHA_ESTADO_DEPENDIENTE_LARGA"] = "";

                    }

                    //Cargar la plantilla a generar
                    if (p_objPlantilla.Formato.Formato == NOTPlantilla.FormularioNotificacion.ToString())
                        p_objPlantillaNotificacion = NOTPlantilla.FormularioNotificacion;
                    else if (p_objPlantilla.Formato.Formato == NOTPlantilla.FormularioNotificacionNoFirma.ToString())
                        p_objPlantillaNotificacion = NOTPlantilla.FormularioNotificacionNoFirma;
                    else if (p_objPlantilla.Formato.Formato == NOTPlantilla.FormularioNotificacionANLA.ToString())
                        p_objPlantillaNotificacion = NOTPlantilla.FormularioNotificacionANLA;
                    else if (p_objPlantilla.Formato.Formato == NOTPlantilla.FormularioNotificacionNoFirmaANLA.ToString())
                        p_objPlantillaNotificacion = NOTPlantilla.FormularioNotificacionNoFirmaANLA;
                    else if (p_objPlantilla.Formato.Formato == NOTPlantilla.FormularioNotificacionANLAFirmaIzquierda.ToString())
                        p_objPlantillaNotificacion = NOTPlantilla.FormularioNotificacionANLAFirmaIzquierda;

                    //Cargar DataSet segun plantilla
                    if (p_objPlantillaNotificacion == NOTPlantilla.FormularioNotificacion || p_objPlantillaNotificacion == NOTPlantilla.FormularioNotificacionNoFirma ||
                        p_objPlantillaNotificacion == NOTPlantilla.FormularioNotificacionANLA || p_objPlantillaNotificacion == NOTPlantilla.FormularioNotificacionNoFirmaANLA ||
                        p_objPlantillaNotificacion == NOTPlantilla.FormularioNotificacionANLAFirmaIzquierda)
                    {
                        //Reemplazar datos de encabezado
                        foreach (MarcaPlantillaNotificacionEntity objMarca in p_objMarcas)
                        {
                            //Verificar que la tabla exista
                            if (p_objDatosActo.Tables.Contains(objMarca.Tabla))
                            {
                                if (p_objDatosActo.Tables[objMarca.Tabla].Rows.Count > 0 && p_objDatosActo.Tables[objMarca.Tabla].Rows[0][objMarca.Campo] != System.DBNull.Value)
                                {
                                    if (!string.IsNullOrEmpty(p_objPlantilla.Encabezado))
                                        p_objPlantilla.Encabezado = p_objPlantilla.Encabezado.Replace(objMarca.Marca, p_objDatosActo.Tables[objMarca.Tabla].Rows[0][objMarca.Campo].ToString());
                                    if (!string.IsNullOrEmpty(p_objPlantilla.Cuerpo))
                                        p_objPlantilla.Cuerpo = p_objPlantilla.Cuerpo.Replace(objMarca.Marca, p_objDatosActo.Tables[objMarca.Tabla].Rows[0][objMarca.Campo].ToString());
                                    if (!string.IsNullOrEmpty(p_objPlantilla.Pie))
                                        p_objPlantilla.Pie = p_objPlantilla.Pie.Replace(objMarca.Marca, p_objDatosActo.Tables[objMarca.Tabla].Rows[0][objMarca.Campo].ToString());
                                    if (!string.IsNullOrEmpty(p_objPlantilla.PieFirma))
                                        p_objPlantilla.PieFirma = p_objPlantilla.PieFirma.Replace(objMarca.Marca, p_objDatosActo.Tables[objMarca.Tabla].Rows[0][objMarca.Campo].ToString());
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(p_objPlantilla.Encabezado))
                                        p_objPlantilla.Encabezado = p_objPlantilla.Encabezado.Replace(objMarca.Marca, "");
                                    if (!string.IsNullOrEmpty(p_objPlantilla.Cuerpo))
                                        p_objPlantilla.Cuerpo = p_objPlantilla.Cuerpo.Replace(objMarca.Marca, "");
                                    if (!string.IsNullOrEmpty(p_objPlantilla.Pie))
                                        p_objPlantilla.Pie = p_objPlantilla.Pie.Replace(objMarca.Marca, "");
                                    if (!string.IsNullOrEmpty(p_objPlantilla.PieFirma))
                                        p_objPlantilla.PieFirma = p_objPlantilla.PieFirma.Replace(objMarca.Marca, "");
                                }
                            }
                        }

                        //Cargar datos
                        objDatos = objInformacionPlantilla.Tables["PLANTILLA"].NewRow();
                        objDatos["PLANTILLA_ID"] = p_objPlantilla.PlantillaID;
                        objDatos["ENCABEZADO"] = p_objPlantilla.Encabezado;
                        objDatos["CUERPO"] = p_objPlantilla.Cuerpo;
                        objDatos["PIE"] = p_objPlantilla.Pie;
                        if (p_objPlantillaNotificacion == NOTPlantilla.FormularioNotificacion || p_objPlantillaNotificacion == NOTPlantilla.FormularioNotificacionANLA || p_objPlantillaNotificacion == NOTPlantilla.FormularioNotificacionANLAFirmaIzquierda)
                            objDatos["PIE_FIRMA"] = p_objPlantilla.PieFirma;
                        objInformacionPlantilla.Tables["PLANTILLA"].Rows.Add(objDatos);
                        if (p_objPlantillaNotificacion == NOTPlantilla.FormularioNotificacion || p_objPlantillaNotificacion == NOTPlantilla.FormularioNotificacionANLA || p_objPlantillaNotificacion == NOTPlantilla.FormularioNotificacionANLAFirmaIzquierda)
                            objInformacionPlantilla.Merge(p_objDatosActo.Tables["FIRMA"]);
                    }
                    else
                    {
                        objInformacionPlantilla.Merge(p_objDatosActo.Tables["FIRMA"]);
                        objInformacionPlantilla.Merge(p_objDatosActo.Tables["ACTO"]);
                        objInformacionPlantilla.Merge(p_objDatosActo.Tables["USUARIO"]);
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Notificacion :: CargarDataSetPlantilla -> Error Inesperado: " + exc.Message);

                    throw new NotificacionException("Notificacion :: CargarDataSetPlantilla -> Error cargando datos para generar plantilla: " + exc.Message, exc);
                }

                return objInformacionPlantilla;
            }


            /// <summary>
            /// Generar la plantilla de respuesta de notificación
            /// </summary>
            /// <param name="p_lngIdActo">long con el identifcador del acto administrativo</param>
            /// <param name="p_lngIdPersona">long con el idenftificador de la persona</param>
            /// <param name="p_objInformacionActo">DataSet con la información del acto administrativo</param>
            /// <param name="p_objEstado">EstadoFlujoNotificacionEntity con la información de configuración del estado</param>
            /// <param name="p_objFechaEstadoNuevo">DateTime con la fecha de notificación</param>
            /// <param name="p_strReferenciaEstado">string con la referencia del estado</param>
            /// <param name="p_objFechaReferenciaEstado">Datetime con la fecha de la referencia relacionada</param>
            /// <param name="p_strObservacionEstado">string con la observacion realizada al estado</param>
            /// <param name="p_intTipoIdentficacionPersonaNotificar">int con el tipo de identificación persona notificada</param>
            /// <param name="p_strNumeroIdentificacionPersonaNotificar">string con el número de identificacion persona notificada</param>
            /// <param name="p_strNombrePersonaNotificar">string con el nombre de la persona notificada</param>
            /// <param name="p_strCalidadPersonaNotificar">string con la calidad de la persona notificada</param>
            /// <param name="p_strRutaArchivo">string con la ruta en la cual se crerará el archivo</param>
            /// <param name="p_strNombreArchivo">string con el nombre del archivo</param>
            /// <param name="p_intIdUsuario">int con el identificador del usuario que se encuentra generarndo la plantilla</param>
            /// <returns>Arreglo de bytes con el archivo</returns>
            private byte[] GenerarPlantillaNotificacion(long p_lngIdActo, long p_lngIdPersona,
                                                        DataSet p_objInformacionActo, EstadoFlujoNotificacionEntity p_objEstado, DateTime p_objFechaEstadoNuevo, string p_strReferenciaEstado, DateTime p_objFechaReferenciaEstado, string p_strObservacionEstado,
                                                        int p_intTipoIdentficacionPersonaNotificar, string p_strNumeroIdentificacionPersonaNotificar, string p_strNombrePersonaNotificar, string p_strCalidadPersonaNotificar,
                                                        List<DireccionNotificacionEntity> p_lstDirecciones, List<CorreoNotificacionEntity> p_lstCorreos,
                                                        string p_strRutaArchivo, ref string p_strNombreArchivo)
            {
                CrearFormularios objFormulario = null;
                PlantillaNotificacion objPlantillaDalc = null;
                PlantillaNotificacionEntity objPlantilla = null;
                List<MarcaPlantillaNotificacionEntity> objMarcas = null;
                NOTPlantilla objPlantillaNotificacion = NOTPlantilla.FormularioNotificacion;
                DataSet objInformacionPlantilla = null;
                byte[] objArchivo = null;

                try
                {
                    //Verificar que se tenga el identificador de la plantilla
                    if (p_objEstado.PlantillaID > 0)
                    {
                        //Crear objeto manejo de plantillas
                        objPlantillaDalc = new PlantillaNotificacion();

                        //Consultar marcadores
                        objMarcas = objPlantillaDalc.ObtenerListadoMarcas();

                        //Consultar configuración de plantilla
                        objPlantilla = objPlantillaDalc.ObtenerPlantilla(p_objEstado.PlantillaID);

                        //JNS 20190911 - Se incorpora fecha de referencia
                        //Cargar datos en Dataset
                        objInformacionPlantilla = this.CargarDataSetPlantilla(p_lngIdActo, p_lngIdPersona, p_objInformacionActo, p_objFechaEstadoNuevo, p_strReferenciaEstado, p_objFechaReferenciaEstado, p_strObservacionEstado, p_intTipoIdentficacionPersonaNotificar, p_strNumeroIdentificacionPersonaNotificar, p_strNombrePersonaNotificar, p_strCalidadPersonaNotificar, p_lstDirecciones, p_lstCorreos, objPlantilla, objMarcas, ref objPlantillaNotificacion);

                        //Crear carpeta archivo
                        if (!Directory.Exists(p_strRutaArchivo))
                            Directory.CreateDirectory(p_strRutaArchivo);

                        //Complementar nombre archivo
                        p_strNombreArchivo = p_strNombreArchivo + ".pdf";

                        //Generar archivo
                        objFormulario = new CrearFormularios();
                        objFormulario.GenerarFormularioPdfNotificaciones(p_strRutaArchivo, p_strNombreArchivo, objInformacionPlantilla, objPlantillaNotificacion);

                        //Verificar que archivo se generara
                        if (File.Exists(p_strRutaArchivo + p_strNombreArchivo))
                        {
                            //Leer archivo
                            objArchivo = File.ReadAllBytes(p_strRutaArchivo + p_strNombreArchivo);

                            //Eliminar archivo
                            File.Delete(p_strRutaArchivo + p_strNombreArchivo);
                        }
                        else
                        {
                            throw new Exception("No se genero el archivo" + p_strNombreArchivo);
                        }
                        
                    }
                    else
                    {
                        throw new Exception("No se especifico el identificador de la plantilla");
                    }
                }
                catch (NotificacionException exc)
                {
                    throw exc;
                }
                catch (Exception exc)
                {

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Notificacion :: GenerarPlantillaNotificacion -> Error Inesperado: " + exc.Message);

                    throw new NotificacionException("Notificacion :: GenerarPlantillaNotificacion -> Error registrando avance: " + exc.Message, exc);
                }

                return objArchivo;
            }


            /// <summary>
            /// Crear el enlace a incluir en el acto administrativo
            /// </summary>
            /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo</param>
            /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
            /// <param name="p_lngEstadoPersonaID">long con el estado creado para la persona notificada/comunicada</param>
            /// <param name="p_AdjuntarActoAdministrativo">bool que indica si se adjunto acto administrativo</param>
            /// <param name="p_blnAdjuntarConceptosActoAdministrativo">bool que indica si se adjuntan conceptos relacionados al acto administrativo</param>
            /// <returns></returns>
            private string GenerarEnlaceCorreoNotificacion(long p_lngActoAdministrativoID, long p_lngPersonaID, long p_lngEstadoPersonaID, bool p_blnAdjuntarActoAdministrativo, bool p_blnAdjuntarConceptosActoAdministrativo)
            {
                string strEnlace = "";
                string strLlave = "";
                SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;
                EnlaceDocumentoDalc objEnlaceDocumentosDalc = null;
                EnlaceDocumentoEntity objEnlace = null;

                try
                {

                    //Generar llave
                    strLlave = EnDecript.Encriptar(p_lngPersonaID + "_" + p_lngEstadoPersonaID);

                    //Obtener URL
                    objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                    strEnlace = objParametrizacion.ObtenerValorParametroGeneral(-1, "URL_DOCUMENTOS_CORREOS_NOTIFICACION") + strLlave;

                    //Insertar datos de URL
                    objEnlace = new EnlaceDocumentoEntity
                    {
                        ActoNotificacionID = p_lngActoAdministrativoID,
                        PersonaID = p_lngPersonaID,
                        EstadoPersonaID = p_lngEstadoPersonaID,
                        LlaveEnviada = strLlave,
                        IncluirActoAdministrativo = p_blnAdjuntarActoAdministrativo,
                        IncluirConceptosActoAdministrativo = p_blnAdjuntarConceptosActoAdministrativo
                    };
                    objEnlaceDocumentosDalc = new EnlaceDocumentoDalc();
                    objEnlaceDocumentosDalc.CrearEnlace(objEnlace);

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Notificacion :: GenerarEnlaceCorreoNotificacion -> Error Inesperado: " + exc.Message);

                    throw new NotificacionException("Notificacion :: GenerarEnlaceCorreoNotificacion -> Error generando enlace: " + exc.Message, exc);
                }

                return strEnlace;
            }


            /// <summary>
            /// Crear el enlace para acceso desde el documento generado al acto administrativo
            /// </summary>
            /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo</param>
            /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
            /// <param name="p_lngEstadoPersonaID">long con el estado creado para la persona notificada/comunicada</param>
            /// <param name="p_AdjuntarActoAdministrativo">bool que indica si se adjunto acto administrativo</param>
            /// <param name="p_blnAdjuntarConceptosActoAdministrativo">bool que indica si se adjuntan conceptos relacionados al acto administrativo</param>
            /// <returns></returns>
            private string GenerarEnlacePlantillaNotificacion(long p_lngActoAdministrativoID, long p_lngPersonaID)
            {
                string strEnlace = "";
                string strLlave = "";
                SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;

                try
                {

                    //Generar llave
                    strLlave = EnDecript.Encriptar(p_lngActoAdministrativoID + "_" + p_lngPersonaID);

                    //Obtener URL
                    objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                    strEnlace = objParametrizacion.ObtenerValorParametroGeneral(-1, "URL_DOCUMENTOS_PLANTILLA_NOTIFICACION") + strLlave;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Notificacion :: GenerarEnlacePlantillaNotificacion -> Error Inesperado: " + exc.Message);

                    throw new NotificacionException("Notificacion :: GenerarEnlacePlantillaNotificacion -> Error generando enlace: " + exc.Message, exc);
                }

                return strEnlace;
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Determina si Este documento es un Acto o una Resolución o si es otro tipo de documento
            /// </summary>
            /// <param name="documentoXML">Estructura XML con el Documento en formato NotificacionType</param>
            /// <returns>Verdadero si es un Auto o Resolución </returns>
            /// <remarks>Los Autos y Resoluciones para este contexto siempre requieren ser notificados</remarks>
            public bool DeterminarNotificacion(string documentoXML)
            {
                NotificacionType _xmlNotificacion = new NotificacionType();
                XmlSerializador _ser = new XmlSerializador();
                _xmlNotificacion = (NotificacionType)_ser.Deserializar(new NotificacionType(), documentoXML);
                if (_xmlNotificacion.requiereNotificacion == true)
                    return true;
                else
                    return false;


            }


            /// <summary>
            /// Validar Notificaciones Pendiente
            /// </summary>
            /// <param name="documentoXML"></param>
            /// <returns></returns>
            public string ValidarNotifiacionesPendiente(string documentoXML)
            {
                string _strPendiente = "";
                NotificacionType _xmlNotificacion = new NotificacionType();
                XmlSerializador _ser = new XmlSerializador();
                _xmlNotificacion = (NotificacionType)_ser.Deserializar(new NotificacionType(), documentoXML);
                if (_xmlNotificacion.requiereNotificacion == true)
                {
                    NotificacionDalc not = new NotificacionDalc();
                    bool _pendiente = not.ValidaNumeroSilpaNotificacionesPendiente(_xmlNotificacion.numSILPA);

                    if(_pendiente)
                    {
                            WSRespuesta WSR = new WSRespuesta();
                            WSR.CodigoMensaje = "";
                            WSR.Mensaje = "No se puede avanzar la tarea porque el proceso tiene notificaciones pendientes.";
                            WSR.IdExterno = "-1"; // no esta relacionado a una publicación
                            WSR.IdSilpa = _xmlNotificacion.numSILPA;
                            WSR.Exito = false;
                            _strPendiente= WSR.GetXml();
                    }
                    //llamar la funcion de Harold
                }
           

                return _strPendiente;
            }


            /// <summary>
            /// Determinar si es sancionatorio y reasignar el usuario
            /// </summary>
            /// <param name="xmlNotificacion"></param>
            /// <returns></returns>
            public bool DeterminarSancionatorio(ref NotificacionType xmlNotificacion)
            {
                try
                {
	                if (xmlNotificacion.esSancionatorio == true)
	                {
	                    //Llamar el procedimiento que ejecuta el cambio de Propietario
	                    string nVital = xmlNotificacion.numSILPA;
	                    PersonaType[] objPersonaList = xmlNotificacion.listaPersonas;
	                    string UsuarioNuevoIdentificacion = objPersonaList[0].numeroIdentificacion;
	
	                    Persona objPersona = new Persona();
	                    DataTable tbPersona = objPersona.ConsultarPersonasNumeroSilpa(nVital);
	                    string usuarioNuevo = tbPersona.Rows[0]["PER_ID"].ToString();
	
	                    objPersona.ObternerPersonaByUsername(UsuarioNuevoIdentificacion);
	                    string cesionario = objPersona.Identity.PersonaId.ToString();
	                    SILPA.LogicaNegocio.CesionDeDerechos.Cesion ces = new SILPA.LogicaNegocio.CesionDeDerechos.Cesion();
	                    ces = new SILPA.LogicaNegocio.CesionDeDerechos.Cesion(nVital, usuarioNuevo, cesionario);
	                    ces.Ejecutar();
	                    return true;
	                    //llamar la funcion de Harold
	                }
	                return false;
                }
                catch (Exception ex)
                {
                    string strException = "Validar los pasos efectuados al emitir el Documento Manual y Determinar si es sancionatorio y reasignar el usuario.";
                    throw new Exception(strException, ex);
                }
            }

            /// <summary>
            /// Determinar Audiencia y Cambiar Propietario
            /// </summary>
            /// <param name="xmlNotificacion"></param>
            /// <returns></returns>
            public bool DeterminarAudiencia(ref NotificacionType xmlNotificacion)
            {
                try
                {
	                if (xmlNotificacion.esAudiencia == true)
	                {
	                    //Llamar el procedimiento que ejecuta el cambio de Propietario
	                    string nVital = xmlNotificacion.numSILPA;
	                    PersonaType[] objPersonaList = xmlNotificacion.listaPersonas;
	                    string UsuarioNuevoIdentificacion = objPersonaList[0].numeroIdentificacion;
	
	                    Persona objPersona = new Persona();
	                    DataTable tbPersona = objPersona.ConsultarPersonasNumeroSilpa(nVital);
	                    string usuarioNuevo = tbPersona.Rows[0]["PER_ID"].ToString();
	
	                    objPersona.ObternerPersonaByUsername(UsuarioNuevoIdentificacion);
	                    string cesionario = objPersona.Identity.PersonaId.ToString();
	                    SILPA.LogicaNegocio.CesionDeDerechos.Cesion ces = new SILPA.LogicaNegocio.CesionDeDerechos.Cesion();
	                    ces = new SILPA.LogicaNegocio.CesionDeDerechos.Cesion(nVital, usuarioNuevo, cesionario);
	                    ces.Ejecutar();
	                    return true;
	                    //llamar la funcion de Harold
	                }
	                return false;
                }
                catch (Exception ex)
                {
                    string strException = "Validar los pasos efectuados al Determinar Audiencia y Cambiar Propietario.";
                    throw new Exception(strException, ex);
                }
            }

            /// <summary>
            /// Determina si una actividad es de tipo Ejecutoria
            /// </summary>
            /// <param name="documentoXML">XML de la AA con los datos de la notificación - ejecutoria</param>
            /// <returns>verdadero si es una actividad de ejecutoria</returns>
            public bool DeterminarEjecutoria(string documentoXML)
            {
                NotificacionType _xmlNotificacion = new NotificacionType();
                XmlSerializador _ser = new XmlSerializador();
                _xmlNotificacion = (NotificacionType)_ser.Deserializar(new NotificacionType(), documentoXML);
                if (_xmlNotificacion.esEjecutoria)
                    return _xmlNotificacion.esEjecutoria;
                else
                    return false;

            }


            /// <summary>
            /// Verifica si se puede realizar una ejecutoria sobre el acto y envía a PDI los datos para ejecutoriarlo
            /// adicionalmente si se realiza la ejecutoria, se actualiza el estado en todas las personas involucradas
            /// </summary>
            /// <param name="documentoXML">Datos del Acto que se desea ejecutoriar</param>
            /// <returns>Mensaje de confirmación o de Error de la Ejecutoria</returns>
            public string Ejecutoria(string documentoXML, out bool respuesta)
            {
                SMLog.Escribir(Severidad.Informativo, "----Ingreso a Ejecutoria");
                string resultado = string.Empty;
                respuesta = false;


                try
                {
                    NotificacionType _xmlNotificacion = new NotificacionType();
                    XmlSerializador _ser = new XmlSerializador();
                    _xmlNotificacion = (NotificacionType)_ser.Deserializar(new NotificacionType(), documentoXML);
                    NotificacionEntity _objNotificacion = new NotificacionEntity();
                    NotificacionDalc _objNotificacionDalc = new NotificacionDalc();

                    ParametroEntity _parametroEjecutoriada = new ParametroEntity();
                    ParametroDalc parametro = new ParametroDalc();
                    _parametroEjecutoriada.IdParametro = -1;
                    _parametroEjecutoriada.NombreParametro = "EJECUTORIADA";
                    parametro.obtenerParametros(ref _parametroEjecutoriada);

                    bool permite_ejecutoria;
                    _objNotificacion = _objNotificacionDalc.ConsultarEjecutoriaActo(_xmlNotificacion.numActoAdministrativo, _xmlNotificacion.numProcesoAdministracion, _xmlNotificacion.numSILPA, int.Parse(_xmlNotificacion.tipoActoAdministrativo), out resultado, out permite_ejecutoria);
                    List<PersonaNotificarEntity> objPersonasNotificarEstadoActivoEject2 = _objNotificacion.ListaPersonas.FindAll(p => p.EstadoActualNotificado == true);
                    foreach (PersonaNotificarEntity per in objPersonasNotificarEstadoActivoEject2)
                    {
                        if (per.FechaNotificado > _xmlNotificacion.fechaActoAdministrativo)
                        {
                            respuesta = false;
                            return "La fecha de notificación no puede ser mayor que la fecha de ejecutoria.";
                        }
                    }

                    if (permite_ejecutoria == true)
                    {

                        string tempPersonaRecurso = "";
                        List<PersonaNotificarEntity> objPersonasNotificarEstadoActivoEject = _objNotificacion.ListaPersonas.FindAll(p => p.EstadoActualNotificado == true);
                        EstadoNotificacionDalc _estadoNotificacionDalc = new EstadoNotificacionDalc();
                        foreach (PersonaNotificarEntity per in objPersonasNotificarEstadoActivoEject)
                        {
                            Object[] parametrosIns = { per.IdActoNotificar, int.Parse(_parametroEjecutoriada.Parametro), per.Id, _xmlNotificacion.fechaActoAdministrativo, "", string.Empty, 0, 0 };
                            tempPersonaRecurso = per.NumeroIdentificacion;
                            resultado = _estadoNotificacionDalc.CrearEstadoPersonaActo(parametrosIns);
                            if (resultado != String.Empty)
                                respuesta = false;

                        }
                        resultado = "Ejecutoriado Satisfactorio";

                        if (!String.IsNullOrEmpty(_objNotificacion.NumeroActoAdministrativoAsociado))
                        {
                            NotificacionEntity _objNotificacion2 = _objNotificacionDalc.ConsultarEjecutoriaActo(_objNotificacion.NumeroActoAdministrativoAsociado, _xmlNotificacion.numProcesoAdministracion, _xmlNotificacion.numSILPA, null, out resultado, out respuesta);
                            List<PersonaNotificarEntity> objPersonasNotificarEstadoActivoRecurso = _objNotificacion2.ListaPersonas.FindAll(p => p.EstadoActualNotificado == true);
                            ParametroEntity _parametroFinDeProceso = new ParametroEntity();
                            _parametroFinDeProceso.IdParametro = -1;
                            _parametroFinDeProceso.NombreParametro = "FIN_DE_PROCESO";
                            parametro.obtenerParametros(ref _parametroFinDeProceso);

                            foreach (PersonaNotificarEntity per in objPersonasNotificarEstadoActivoRecurso)
                            {

                                if (per.NumeroIdentificacion == tempPersonaRecurso)
                                {
                                    Object[] parametrosIns = { per.IdActoNotificar, int.Parse(_parametroFinDeProceso.Parametro), per.Id, _xmlNotificacion.fechaActoAdministrativo, "", string.Empty, 0, 0 };
                                    resultado = _estadoNotificacionDalc.CrearEstadoPersonaActo(parametrosIns);
                                    if (resultado != String.Empty)
                                        respuesta = false;
                                }
                            }
                            NotificacionType NotificacionActoAsociado = new NotificacionType();
                            NotificacionActoAsociado = _xmlNotificacion;
                            NotificacionActoAsociado.numActoAdministrativo = _objNotificacion.NumeroActoAdministrativoAsociado;
                            NotificacionActoAsociado.fechaActoAdministrativo = NotificacionActoAsociado.fechaActoAdministrativo.AddSeconds(10);
                            string strResultado = Ejecutoria(NotificacionActoAsociado, out permite_ejecutoria);
                        }

                    }
                    respuesta = permite_ejecutoria;
                    return resultado;
                }
                catch (Exception ex)
                {
                    SMLog.Escribir(Severidad.Informativo, "----Error Ejecutoriado:" + ex.ToString());
                    string strException = "Validar los pasos efectuados al verificar si se puede realizar una ejecutoria sobre el acto. ";
                    resultado = strException + ex.Message;
                    respuesta = false;
                    return resultado;
                }
            }


            public string Ejecutoria(NotificacionType _xmlNotificacion, out bool respuesta)
            {
                SMLog.Escribir(Severidad.Informativo, "----Ingreso a Ejecutoria con Recurso");
                string resultado = string.Empty;
                respuesta = false;


                try
                {

                    NotificacionEntity _objNotificacion = new NotificacionEntity();
                    NotificacionDalc _objNotificacionDalc = new NotificacionDalc();

                    ParametroEntity _parametroEjecutoriada = new ParametroEntity();
                    ParametroDalc parametro = new ParametroDalc();
                    _parametroEjecutoriada.IdParametro = -1;
                    _parametroEjecutoriada.NombreParametro = "EJECUTORIADA";
                    parametro.obtenerParametros(ref _parametroEjecutoriada);

                    bool permite_ejecutoria;
                    _objNotificacion = _objNotificacionDalc.ConsultarEjecutoriaActo(_xmlNotificacion.numActoAdministrativo, _xmlNotificacion.numProcesoAdministracion, _xmlNotificacion.numSILPA, null, out resultado, out permite_ejecutoria);

                    List<PersonaNotificarEntity> objPersonasNotificarEstadoActivoEject2 = _objNotificacion.ListaPersonas.FindAll(p => p.EstadoActualNotificado == true);
                    foreach (PersonaNotificarEntity per in objPersonasNotificarEstadoActivoEject2)
                    {
                        if (per.FechaNotificado > _xmlNotificacion.fechaActoAdministrativo)
                        {
                            respuesta = false;
                            return "La fecha de notificación no puede ser mayor que la fecha de ejecutoria.";
                        }
                    }


                    if (permite_ejecutoria == true)
                    {
                        if (!String.IsNullOrEmpty(_objNotificacion.NumeroActoAdministrativoAsociado))
                        {
                            NotificacionType NotificacionActoAsociado = new NotificacionType();
                            NotificacionActoAsociado = _xmlNotificacion;
                            NotificacionActoAsociado.numActoAdministrativo = _objNotificacion.NumeroActoAdministrativoAsociado;
                            bool resp = false;
                            string strResultado = Ejecutoria(NotificacionActoAsociado, out resp);
                            if (!resp)
                            {
                                respuesta = resp;
                                return strResultado;
                            }

                        }

                        List<PersonaNotificarEntity> objPersonasNotificarEstadoActivoEject = _objNotificacion.ListaPersonas.FindAll(p => p.EstadoActualNotificado == true);
                        foreach (PersonaNotificarEntity per in objPersonasNotificarEstadoActivoEject)
                        {
                            EstadoNotificacionDalc _estadoNotificacionDalc = new EstadoNotificacionDalc();
                            Object[] parametrosIns = { per.IdActoNotificar, int.Parse(_parametroEjecutoriada.Parametro), per.Id, _xmlNotificacion.fechaActoAdministrativo, "", string.Empty, 0, 0 };
                            resultado = _estadoNotificacionDalc.CrearEstadoPersonaActo(parametrosIns);
                            if (resultado != String.Empty)
                                respuesta = false;
                        }
                        resultado = "Ejecutoriado Satisfactorio";
                    }
                    respuesta = true;
                    return resultado;
                }
                catch (Exception ex)
                {
                    SMLog.Escribir(Severidad.Informativo, "----Error Ejecutoriado:" + ex.ToString());
                    resultado = ex.Message;
                    respuesta = false;
                    return resultado;
                }
            }


            /// <summary>
            /// HAVA: 23-NOV-10
            /// Envia correo indicando que falló la comunicación con el sistema PDI
            /// </summary>
            public void EnviarCorreoFallaPDI(NotificacionEntity _objNot)
            {
                try
                {
	                SILPA.LogicaNegocio.Parametrizacion.Parametrizacion parametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();

	                int inactivoPDI = 0;
	                parametrizacion.obtenerTiempoNotificacion(out inactivoPDI);
	
	                PersonaDalc per = new PersonaDalc();
	                string emailFuncionario = per.ObtenerEmailFuncionario(_objNot.IdentificacionFuncionario);
	
	                if (Convert.ToBoolean(inactivoPDI) == true)
	                {
	                    ParametroEntity _eMailMasterAdmin = new ParametroEntity();
	                    _eMailMasterAdmin.IdParametro = -1;
	                    _eMailMasterAdmin.NombreParametro = "correo_administrador_Maestro";
	                    ParametroDalc parametro = new ParametroDalc();
	                    parametro.obtenerParametros(ref _eMailMasterAdmin);
	
	                    string fechaFalla = DateTime.Now.ToString("dd/MM/yyyy");
	                    string horaFalla = DateTime.Now.ToString("HH:mm");
	
	                    SILPA.LogicaNegocio.ICorreo.Correo correo = new SILPA.LogicaNegocio.ICorreo.Correo();
	                    correo.EnviarCorreoFallaComunicacionPDI(_eMailMasterAdmin.Parametro, emailFuncionario, fechaFalla, horaFalla);
	                }
                }
                catch (Exception ex)
                {
                    string strException = "Validar los pasos efectuados al enviar correo indicando que falló la comunicación con el sistema PDI.";
                    throw new Exception(strException, ex);
                }
            }

            /// <summary>
            /// Verifica si se puede realizar una ejecutoria sobre el acto y envía a PDI los datos para ejecutoriarlo
            /// adicionalmente si se realiza la ejecutoria, se actualiza el estado en todas las personas involucradas
            /// </summary>
            /// <param name="documentoXML">Datos del Acto que se desea ejecutoriar</param>
            /// <returns>Mensaje de confirmación o de Error de la Ejecutoria</returns>
            public string EjecutoriaManual(int idEstado, string strEstado, string esPDI, long idActoNot, out bool respuesta, long idPersona)
            {
                SMLog.Escribir(Severidad.Informativo, "----Ingreso a Ejecutoria Manual");
                string resultado = string.Empty;
                try
                {
                    NotificacionType _xmlNotificacion = new NotificacionType();
                    XmlSerializador _ser = new XmlSerializador();
                    // Pendiente recuperar el archivo desde el disco para enviar a PDI
                    //_xmlNotificacion = (NotificacionType)_ser.Deserializar(new NotificacionType(), documentoXML);

                    NotificacionEntity _objNotificacion = new NotificacionEntity();
                    NotificacionDalc _objNotificacionDalc = new NotificacionDalc();
                    TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
                    TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
                    TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
                    TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
                    DependenciaEntidadEntity _dependenciaEntidad = new DependenciaEntidadEntity();
                    SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
                    SolicitudDAAEIAIdentity solicitud = new SolicitudDAAEIAIdentity();
                    Comun.TraficoDocumento traficoArchivos = new TraficoDocumento();
                    PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
                    //PersonaNotificard
                    //PersonaNotificarEntity personaNotificar = new PersonaNotificarEntity();
                    TipoPersonaNotificacionDalc tipoPersonaDalc = new TipoPersonaNotificacionDalc();
                    //SILPA.AccesoDatos.Notificacion.TipoPersonaDalc tipoPersonaDalc = new SILPA.AccesoDatos.Notificacion.TipoPersonaDalc();

                    string _numeroActoAdministrativo = String.Empty;
                    string _procesoAdministracion = String.Empty;
                    string _parteResolutiva = String.Empty;
                    string _numeroSilpa = String.Empty;
                    string _rutaDocumento = String.Empty;
                    string _identificacionFuncionario = String.Empty;

                    _objNotificacionDalc.ObtenerDatosParaEjecutoria(idActoNot,
                            out _numeroActoAdministrativo,
                            out _procesoAdministracion,
                            out _numeroSilpa,
                            out _parteResolutiva,
                            out _rutaDocumento,
                            out _identificacionFuncionario
                    );

                    _objNotificacion.NumeroActoAdministrativo = _numeroActoAdministrativo;
                    _objNotificacion.ProcesoAdministracion = _procesoAdministracion;
                    _objNotificacion.ParteResolutiva = _parteResolutiva;
                    _objNotificacion.NumeroSILPA = _numeroSilpa;
                    _objNotificacion.RutaDocumento = _rutaDocumento;
                    _objNotificacion.IdentificacionFuncionario = _identificacionFuncionario;
                    //TODO - Validar Esto

                    _objNotificacion = _objNotificacionDalc.ObtenerActo(new object[] { null, null, null, _numeroActoAdministrativo, _procesoAdministracion, _numeroSilpa, null, null, null, null, null });

                    //Verificar Sistema Entidad publica

                    solicitud = _solicitudDalc.ObtenerSolicitud(null, null, _numeroSilpa);
                    //Se verifica que las personas tengan un estado válido para hacer ejecutoria
                    ParametroEntity _parametro1 = new ParametroEntity();
                    ParametroEntity _parametro2 = new ParametroEntity();
                    ParametroEntity _parametro3 = new ParametroEntity();
                    ParametroDalc parametro = new ParametroDalc();
                    _parametro1.IdParametro = _parametro2.IdParametro = -1;
                    bool permite_ejecutoria = true;
                    foreach (PersonaNotificarEntity personaNotificar in _objNotificacion.ListaPersonas)
                    {

                        _parametro1 = new ParametroEntity();
                        _parametro2 = new ParametroEntity();
                        _parametro3 = new ParametroEntity();
                        _parametro1.IdParametro = -1;
                        _parametro1.NombreParametro = "CON_RECURSO_INTERPUESTO";
                        _parametro2.IdParametro = -1;
                        _parametro2.NombreParametro = "CON_RENUNCIA_A_TÉRMINOS";
                        _parametro3.IdParametro = -1;
                        _parametro3.NombreParametro = "NOTIFICADA";
                        parametro.obtenerParametros(ref _parametro1);
                        parametro.obtenerParametros(ref _parametro2);
                        parametro.obtenerParametros(ref _parametro3);
                        //if (!personaNotificar.EstadoNotificado.ID.Equals(Convert.ToInt32(_parametro1.Parametro)) && !personaNotificar.EstadoNotificado.ID.Equals(Convert.ToInt32(_parametro2.Parametro)))
                        //Si es recurso entonces se verifica que exista un acto en la base de datos que tenga asociado el acto que se va a ejecutoriar y se verifica que ese acto esté notificado
                        if (personaNotificar.EstadoNotificado.ID.Equals(Convert.ToInt32(_parametro1.Parametro)))
                        {
                            NotificacionEntity asociado = _objNotificacionDalc.ObtenerActo(new object[] { null, null, null, null, null, null, null, null, null, _objNotificacion.NumeroActoAdministrativo, null });
                            if (asociado != null)
                            {
                                //Se busca que la lista todal de personas del acta sea igual a la lista total de personas con estado NOTIFICADA, sino no se puede ejecutoriar
                                if (asociado.ListaPersonas.FindAll(delegate(PersonaNotificarEntity per) { return per.EstadoNotificado.ID == Convert.ToInt32(_parametro3.Parametro); }).Count != asociado.ListaPersonas.Count)
                                    permite_ejecutoria = false;
                            }
                            else
                            {
                                permite_ejecutoria = false;
                            }
                        }
                        else if (!personaNotificar.EstadoNotificado.ID.Equals(Convert.ToInt32(_parametro2.Parametro)))
                        {
                            permite_ejecutoria = false;
                        }
                    }

                    /*
                        * hava:19-NOV-10
                        * Se crea el estado ejecutoriada por cada persona
                        * 
                        */
                    if (permite_ejecutoria == true)
                    {
                        ParametroEntity _parametroEjecutoriada = new ParametroEntity();
                        _parametroEjecutoriada.NombreParametro = "EJECUTORIADA";
                        _parametroEjecutoriada.IdParametro = -1;
                        ParametroDalc _parametroDalc = new ParametroDalc();
                        _parametroDalc.obtenerParametros(ref _parametroEjecutoriada);

                        foreach (PersonaNotificarEntity per in _objNotificacion.ListaPersonas)
                        {
                            if (per.Id == idPersona)
                            {
                                PersonaNotificarEntity personaInsertar = new PersonaNotificarEntity();
                                personaInsertar = per;

                                if (per.EstadoNotificado == null) per.EstadoNotificado = new EstadoNotificacionEntity();
                                per.EstadoNotificado.ID = int.Parse(_parametroEjecutoriada.Parametro);

                                _personaDalc.Insertar(ref personaInsertar);
                                personaInsertar = null;
                            }
                        }
                    }
                    /*Fin del cambio */

                    #region Creación del Proceso en PDI
                    if (permite_ejecutoria == true && esPDI == "SI")
                    {
                        List<byte[]> listaArchivos = new List<byte[]>();
                        // Pendiente recuperar el archivo desde el disco para enviar a PDI
                        //------ activar //listaArchivos.Add(_xmlNotificacion.datosArchivo);
                        List<string> nombres = new List<string>();
                        //------ activar //nombres.Add(_xmlNotificacion.nombreArchivo);
                        string ruta = "";
                        //------ activar //traficoArchivos.RecibirDocumento(_xmlNotificacion.numSILPA, solicitud.IdSolicitante.ToString(), listaArchivos, ref nombres, ref ruta);
                        //------ activar //_objNotificacion.RutaDocumento = nombres[0].ToString();

                        string xmlGEL = string.Empty;
                        //------ activar string xmlGEL = SetXMLGELEjecutoriarActoEntrada(_objNotificacion, _xmlNotificacion);

                        //.. Se consume el servicio de PDI - Agilizadores
                        ParametroEntity _parametroTramite = new ParametroEntity();
                        ParametroEntity _parametroVersion = new ParametroEntity();
                        _parametroTramite.IdParametro = -1;
                        _parametroTramite.NombreParametro = "tramite_ejecutoriar_notificacion";
                        _parametroVersion.IdParametro = -1;
                        _parametroVersion.NombreParametro = "version_ejecutoriar_notificacion";
                        parametro.obtenerParametros(ref _parametroTramite);
                        parametro.obtenerParametros(ref _parametroVersion);
                        NotificacionPDI.NotificacionPDI wsNotificacion = new SILPA.LogicaNegocio.NotificacionPDI.NotificacionPDI();
                        wsNotificacion.Url = SILPA.Comun.DireccionamientoWS.UrlWS("NotificacionPDI");
                        wsNotificacion.Credentials = SILPA.Comun.DireccionamientoWS.Credenciales();

                        //if (this.TestPDI(wsNotificacion.Url))
                        if (wsNotificacion.testPDI())
                        {

                            string resultadoPDI = string.Empty;
                            Escribir("----Mensaje Ejecutoriar: " + xmlGEL);
                            Escribir("----Parametros: " + _parametroTramite.Parametro + "/" + _parametroVersion.Parametro + "/" + nombres[0].ToString());
                            resultadoPDI = wsNotificacion.EjecutarNotificacion(xmlGEL, _parametroTramite.Parametro, _parametroVersion.Parametro, nombres[0].ToString());
                            //....

                            //Se obtienen los datos del XML resultado entregado por PDI
                            //bool respuesta=false;
                            resultado = GetXMLGELEjecutoriarActoSalida(resultadoPDI, ref _objNotificacion, out respuesta);
                        }
                        else
                        {
                            //Envía el correo indicando que la notificación con PDI falló
                            EnviarCorreoFallaPDI(_objNotificacion);

                            resultado = "No existe comunicación con PDI";
                            respuesta = false;
                        }
                    }
                    else
                    {
                        resultado = "Una o varias personas asociadas al Acto Tienen Estados de Notificación que no Permiten Ejecutoriar este Acto. Si alguna de las personas tiene estado con Recurso Interpuesto verifique que se repartió la actividad a un funcionario en el Sistema de Notificación, y el Acto Asociado tiene estado NOTIFICADA para todas las personas del Acto.";
                        respuesta = false;
                    }
                    #endregion
                    return resultado;
                }
                catch (Exception ex)
                {
                    SMLog.Escribir(Severidad.Informativo, "----Error Ejecutoriado:" + ex.ToString());
                    resultado = ex.Message;
                    respuesta = false;
                    return resultado;
                }

            }


            /// <summary>
            /// Envía un correo al solicitante con la información entregada por la AA del Oficio o Documento
            /// </summary>
            /// <param name="documentoXML">XML con los datos entregados por la AA</param>
            /// <returns>Número SILPA del solicitante al que se le envió el correo</returns>
            public string EnviarCorreo(NotificacionType documentoXML)
            {
                NotificacionType _xmlNotificacion = new NotificacionType();
                _xmlNotificacion = documentoXML;
                NotificacionEntity _objNotificacion = new NotificacionEntity();
                NotificacionDalc _objNotificacionDalc = new NotificacionDalc();
                TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
                TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
                TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
                TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
                DependenciaEntidadEntity _dependenciaEntidad = new DependenciaEntidadEntity();
                SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
                SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();

                _objNotificacion.NumeroActoAdministrativo = _xmlNotificacion.numActoAdministrativo;
                _objNotificacion.ProcesoAdministracion = _xmlNotificacion.numProcesoAdministracion;
                _objNotificacion.ParteResolutiva = _xmlNotificacion.parteResolutiva;
                _objNotificacion.NumeroSILPA = _xmlNotificacion.numSILPA;
                _objNotificacion.NumeroActoAdministrativoAsociado = _xmlNotificacion.numActoAdministrativoAsociado;
                _objNotificacion.IdentificacionFuncionario = _xmlNotificacion.numeroIdentificacionFuncionario;

                //Buscar Dalc de Tipo Identificación?
                _tipoID = _tipoIDDalc.ObtenerTipoIdentificacionPorCodigo(_xmlNotificacion.tipoIdentificacionFuncionario.ToString());
                _objNotificacion.TipoIdentificacionFuncionario = _tipoID;
                //buscar Dalc Tipo Acto
                _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(_xmlNotificacion.tipoActoAdministrativo), null);
                _objNotificacion.IdTipoActo = _tipoActo;
                //Verificar Código Entidad Pública
                //_objNotificacion.AnoNotificacion = DateTime.Now.Year.ToString();
                _objNotificacion.FechaActo = _xmlNotificacion.fechaActoAdministrativo;
                byte[] documento = _xmlNotificacion.datosArchivo;
                string nombreArchivo = _xmlNotificacion.nombreArchivo;
                #region Personas a Notificar - Enviar Oficio
                //esto es código emulado
                List<PersonaNotificarEntity> listaPersonasNotificacion = new List<PersonaNotificarEntity>();
                //listaPersonas.AddRange(_xmlNotificacion.listaPersonas);
                //1. Buscar en la lista de personas la cédula que sea igual a la de un solicitante en la tabla Persona de SILAMC_MAVDT
                //2. Llenar una lista de PersonaIdentity para enviar el correo al solicitante y sus apoderados, representantes legales
                PersonaIdentity persona1 = new PersonaIdentity();
                PersonaDalc personadalc = new PersonaDalc();

                _solicitud.NumeroSilpa = _objNotificacion.NumeroSILPA;
                //SMLog.Escribir((Severidad.Informativo, "Notificacion: _solicitud.NumeroSilpa = _objNotificacion.NumeroSILPA " + _objNotificacion.NumeroSILPA.ToString());
                _solicitudDalc.ObtenerSolicitud(ref _solicitud);
                persona1 = personadalc.BuscarPersonaByUserId(_solicitud.IdSolicitante.ToString());

                #endregion

                ICorreo.Correo.EnviarOficio(_objNotificacion, persona1, documento, nombreArchivo);
                //Se envia el correo a sus representantes
                //foreach (PersonaIdentity representante in persona1.ListaPersona)
                //{
                //    ICorreo.Correo.EnviarOficio(_objNotificacion, representante);
                //}
                return _objNotificacion.NumeroSILPA;

            }


            /// <summary>
            /// Obtiene los datos desde el xml para el objeto: NotificacionEntity
            /// </summary>
            /// <returns>NotificacionEntity:Objeto</returns>
            public NotificacionEntity ObtenerObjetoNotificacion(NotificacionType documentoXML)
            {
                try
                {
	                NotificacionType _xmlNotificacion = new NotificacionType();
	            
	                _xmlNotificacion = documentoXML;
	                NotificacionEntity _objNotificacion = new NotificacionEntity();
	                NotificacionDalc _objNotificacionDalc = new NotificacionDalc();
	                TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
	                TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
	                TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
	                TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
	                DependenciaEntidadEntity _dependenciaEntidad = new DependenciaEntidadEntity();
	                SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
	                SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();
	                _objNotificacion.CodigoAA = _xmlNotificacion.CodigoAutoridadAmbiental;
	                _objNotificacion.NumeroActoAdministrativo = _xmlNotificacion.numActoAdministrativo;
	                _objNotificacion.ProcesoAdministracion = _xmlNotificacion.numProcesoAdministracion;
	                _objNotificacion.ParteResolutiva = _xmlNotificacion.parteResolutiva;
	                _objNotificacion.NumeroSILPA = _xmlNotificacion.numSILPA;
	                _objNotificacion.NumeroActoAdministrativoAsociado = _xmlNotificacion.numActoAdministrativoAsociado;
	                _objNotificacion.IdentificacionFuncionario = _xmlNotificacion.numeroIdentificacionFuncionario;
	
	                //Buscar Dalc de Tipo Identificación?
	                _tipoID = _tipoIDDalc.ObtenerTipoIdentificacionPorCodigo(_xmlNotificacion.tipoIdentificacionFuncionario.ToString());
	                _objNotificacion.TipoIdentificacionFuncionario = _tipoID;
	                //buscar Dalc Tipo Acto
	                _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(_xmlNotificacion.tipoActoAdministrativo), null);
	                _objNotificacion.IdTipoActo = _tipoActo;
	                //Verificar Código Entidad Pública
	                //_objNotificacion.AnoNotificacion = DateTime.Now.Year.ToString();
	                _objNotificacion.FechaActo = _xmlNotificacion.fechaActoAdministrativo;
	                byte[] documento = _xmlNotificacion.datosArchivo;
	                string nombreArchivo = _xmlNotificacion.nombreArchivo;
	                #region Personas a Notificar - Enviar Oficio
	                //esto es código emulado
	                List<PersonaNotificarEntity> listaPersonasNotificacion = new List<PersonaNotificarEntity>();
	                //listaPersonas.AddRange(_xmlNotificacion.listaPersonas);
	                //1. Buscar en la lista de personas la cédula que sea igual a la de un solicitante en la tabla Persona de SILAMC_MAVDT
	                //2. Llenar una lista de PersonaIdentity para enviar el correo al solicitante y sus apoderados, representantes legales
	                PersonaIdentity persona1 = new PersonaIdentity();
	                PersonaDalc personadalc = new PersonaDalc();
	
	                _solicitud.NumeroSilpa = _objNotificacion.NumeroSILPA;
	                //SMLog.Escribir(Severidad.Informativo, "Notificacion: _solicitud.NumeroSilpa = _objNotificacion.NumeroSILPA " + _objNotificacion.NumeroSILPA.ToString());
	                _solicitudDalc.ObtenerSolicitud(ref _solicitud);
	                persona1 = personadalc.BuscarPersonaByUserId(_solicitud.IdSolicitante.ToString());
	                #endregion
	
	                //Se envia el correo a sus representantes
	                //foreach (PersonaIdentity representante in persona1.ListaPersona)
	                //{
	                //    ICorreo.Correo.EnviarOficio(_objNotificacion, representante);
	                //}
	                //return _objNotificacion.NumeroSILPA;
	
	                return _objNotificacion;
                }
                catch (Exception ex)
                {
                    string strException = "Validar los pasos efectuados al obtener los datos desde el xml para el objeto: NotificacionEntity.";
                    throw new Exception(strException, ex);
                }
            }


            /// <summary>
            /// Consulta la ruta física del documento asociado al acto administrativo que se notificó
            /// </summary>
            /// <param name="idActo">Identificador del Acto</param>
            /// <returns>Ruta del Documento en disco</returns>
            public string ConsultarRuta(decimal idActo)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                NotificacionEntity entity = dalc.ObtenerActo(new object[] { idActo, null, null, null, null, null, null, null, null, null, null });
                return entity.RutaDocumento;
            }

            /// <summary>
            /// Consulta información de acto administrativo
            /// </summary>
            /// <param name="idActo">Identificador del Acto</param>
            /// <returns>NotificacionEntity con la informacion del acto administrativo</returns>
            public NotificacionEntity ConsultarInformacionActo(decimal idActo)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ObtenerActo(new object[] { idActo, null, null, null, null, null, null, null, null, null, null });
            }

            /// <summary>
            /// Obtener información de acto administrativo por acto o por publicación
            /// </summary>
            /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo.</param>
            /// <param name="p_strOrigen">string con el origebn desde el cual se obtendar los datos</param>
            /// <param name="p_lngUsuarioID">long con el identificador del usuario que realiza la consulta</param>
            /// <returns>DataTable con la información de configuración del acto administrativo</returns>
            public DataTable ObtenerConfiguracionActoAdministrativo(long p_lngActoAdministrativoID, string p_strOrigen, long p_lngUsuarioID)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ObtenerConfiguracionActoAdministrativo(p_lngActoAdministrativoID, p_strOrigen, p_lngUsuarioID);
            }


            /// <summary>
            /// Modificar la configuración del acto administrativo
            /// </summary>
            /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo</param>
            /// <param name="p_strOrigen">string con el origen de la información del acto administrativo actual</param>
            /// <param name="p_blnEsNotificacion">bool que indica si presenta notificaciones</param>
            /// <param name="p_blnEsComunicacion">bool que indica si presenta comunicaciones</param>
            /// <param name="p_blnEsCumplase">bool que indica si el acto debe ser cumplido</param>
            /// <param name="p_blnpublica">bool que indica si el acto debe ser publicado</param>
            /// <param name="p_blnAplicaRecurso">boo que indica si el acto aplica recurso de reposición</param>
            /// <param name="p_strRutaActoAdministrativo">string con la ruta del acto administrativo</param>
            /// <param name="p_lngUsuarioID">long con el identificador del usuario que realiza el cambio de configuración</param>
            public void ModificarConfiguracionActoAdministrativo(long p_lngActoAdministrativoID, string p_strOrigen, bool p_blnEsNotificacion,
                                                                 bool p_blnEsComunicacion, bool p_blnEsCumplase, bool p_blnPublica,
                                                                 bool p_blnAplicaRecurso, string p_strRutaActoAdministrativo, long p_lngUsuarioID)
            {
                 NotificacionDalc dalc = new NotificacionDalc();
                 dalc.ModificarConfiguracionActoAdministrativo(p_lngActoAdministrativoID, p_strOrigen, p_blnEsNotificacion,
                                                               p_blnEsComunicacion, p_blnEsCumplase, p_blnPublica,
                                                               p_blnAplicaRecurso, p_strRutaActoAdministrativo, p_lngUsuarioID);
            }


            /// <summary>
            /// Modificar el estado del acto administrativo
            /// </summary>
            /// <param name="p_lngActoID">long con el identificador del acto</param>
            /// <param name="p_UsuarioID">int con el identificador del usuario que realiza la modificación</param>
            public void ModificarEstadoActoAdministrativo(long p_lngActoID, int p_intEstadoActoID, int p_UsuarioID)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                dalc.ModificarEstadoActoAdministrativo(p_lngActoID, p_intEstadoActoID, p_UsuarioID);
            }

            /// <summary>
            /// Crea un proceso de Notificación
            /// </summary>
            /// <param name="xmlDatos"></param>
            public string CrearProceso(NotificacionType xmlDatos)
            {
                try
                {

                    NotificacionType _xmlNotificacion = new NotificacionType();
                    _xmlNotificacion = xmlDatos;
                    NotificacionEntity _objNotificacion = new NotificacionEntity();
                    NotificacionDalc _objNotificacionDalc = new NotificacionDalc();
                    TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
                    TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
                    TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
                    TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
                    DependenciaEntidadEntity _dependenciaEntidad = new DependenciaEntidadEntity();
                    SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
                    SolicitudDAAEIAIdentity solicitud = new SolicitudDAAEIAIdentity();
                    Comun.TraficoDocumento traficoArchivos = new TraficoDocumento();
                    PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
                    ParametroDalc _parametroDalc = new ParametroDalc();
                    ParametroEntity _parametro = new ParametroEntity();
                    //PersonaNotificard
                    PersonaNotificarEntity personaNotificar = new PersonaNotificarEntity();
                    TipoPersonaNotificacionDalc tipoPersonaDalc = new TipoPersonaNotificacionDalc();
                    FlujoNotificacionElectronicaDalc objFlujoNotificacionDalc = null;
                    DataTable objInformacionFlujo = null;
                    DataRow[] objDatosFlujo = null;
                    EstadoInicialActoParametroDalc objEstadoInicialActoParametroDalc = null;
                    EstadoInicialActoParametroEntity objEstadosActosAdministrativo = null;

                    //SILPA.AccesoDatos.Notificacion.TipoPersonaDalc tipoPersonaDalc = new SILPA.AccesoDatos.Notificacion.TipoPersonaDalc();
                    _objNotificacion.NumeroActoAdministrativo = _xmlNotificacion.numActoAdministrativo;
                    _objNotificacion.ProcesoAdministracion = _xmlNotificacion.numProcesoAdministracion;
                    _objNotificacion.ParteResolutiva = _xmlNotificacion.parteResolutiva;
                    _objNotificacion.NumeroSILPA = _xmlNotificacion.numSILPA;
                    SMLog.Escribir(Severidad.Critico, "Este es el metodo que el usa no usa mas");
                    if (_xmlNotificacion.numActoAdministrativoAsociado != null && _xmlNotificacion.numActoAdministrativoAsociado != string.Empty)
                    {
                        NotificacionEntity actoAsocidado = new NotificacionEntity();


                        actoAsocidado = _objNotificacionDalc.ObtenerActo(new object[] { null, null, null, _xmlNotificacion.numActoAdministrativoAsociado, _xmlNotificacion.numProcesoAdministracion, null, null, null, null, null, null });

                        if (actoAsocidado != null)
                        {
                            // validamos si el actoasociado tiene numeros vitales asociados, si es asi es por que el acto esta asociado a un proceso de recurso de reposicion de lo contrario es un acto normal y no necesta validar los usuario
                            RecursoReposicionServicios servicio = new RecursoReposicionServicios();
                            if (servicio.ObtenerNumeroVitalRecursoReposicion(_xmlNotificacion.numSILPA, _xmlNotificacion.numProcesoAdministracion, Convert.ToInt32(_xmlNotificacion.CodigoAutoridadAmbiental)).Tables[0].Rows.Count > 0)
                            {
                                List<PersonaNotificarEntity> objPersonasNotificarEstadoActivo = actoAsocidado.ListaPersonas.FindAll(p => p.EstadoActualNotificado == true);
                                _parametro.IdParametro = -1;
                                _parametro.NombreParametro = "CON_RECURSO_INTERPUESTO";
                                _parametroDalc.obtenerParametros(ref _parametro);
                                int estadoRecurso = Convert.ToInt32(_parametro.Parametro);
                                _parametro.IdParametro = -1;
                                _parametro.NombreParametro = "FIN_DE_PROCESO";
                                _parametroDalc.obtenerParametros(ref _parametro);
                                int estadoFinProceso = Convert.ToInt32(_parametro.Parametro);
                                foreach (PersonaType pt in _xmlNotificacion.listaPersonas)
                                {
                                    if (actoAsocidado.ListaPersonas.FindAll(delegate(PersonaNotificarEntity per) { return per.EstadoNotificado.ID == estadoFinProceso && per.NumeroIdentificacion == pt.numeroIdentificacion; }).Count >= 1)
                                    {
                                        _objNotificacion.NumeroActoAdministrativoAsociado = _xmlNotificacion.numActoAdministrativoAsociado;
                                    }
                                    else
                                    {
                                        // JACOSTA 20120927. Consultamo en la lista de las personas el estado CON_RECURSO_INTERPUESTO
                                        if (actoAsocidado.ListaPersonas.FindAll(delegate(PersonaNotificarEntity per) { return per.EstadoNotificado.ID == estadoRecurso && per.NumeroIdentificacion == pt.numeroIdentificacion; }).Count == 1)
                                        {
                                            _objNotificacion.NumeroActoAdministrativoAsociado = _xmlNotificacion.numActoAdministrativoAsociado;
                                        }
                                        else
                                        {
                                            SMLog.Escribir(Severidad.Critico, "-No se encontraron personas a asociar.");
                                            return "Al menos uno de los usuarios a notificar no se encuentra en estado de recurso interpuesto, no se creó el proceso";
                                        }
                                    }
                                }
                            }

                        }
                    }
                    _objNotificacion.RequierePublicacion = _xmlNotificacion.requierePublicacion;
                    _objNotificacion.AplicaRecursoReposicion = _xmlNotificacion.aplicaRecurso;

                    if (_xmlNotificacion.esNotificacion != null)
                        _objNotificacion.EsNotificacion = _xmlNotificacion.esNotificacion;
                    else if (_xmlNotificacion.esComunicacion == null && _xmlNotificacion.esCumplase == null)
                        _objNotificacion.EsNotificacion = true;
                    else
                        _objNotificacion.EsNotificacion = false;
                    _objNotificacion.EsComunicacion = _xmlNotificacion.esComunicacion;
                    _objNotificacion.EsCumplase = _xmlNotificacion.esCumplase;
                    _objNotificacion.EsNotificacionEdicto = _xmlNotificacion.esNotificacionEdicto;
                    _objNotificacion.EsNotificacionEstrado = _xmlNotificacion.esNotificacionEstrado;

                    _objNotificacion.SistemaEntidadPublica = Convert.ToInt32(_xmlNotificacion.SistemaEntidadPublicaNot);

                    _parametro = new ParametroEntity();
                    _parametro.IdParametro = -1;
                    _parametro.NombreParametro = "id_plantilla_notificacion";
                    _parametroDalc.obtenerParametros(ref _parametro);

                    _objNotificacion.IdPlantilla = _parametro.Parametro;

                    _objNotificacion.IdentificacionFuncionario = _xmlNotificacion.numeroIdentificacionFuncionario;

                    _tipoID = _tipoIDDalc.ObtenerTipoIdentificacionPorCodigo(_xmlNotificacion.tipoIdentificacionFuncionario.ToString());
                    _objNotificacion.TipoIdentificacionFuncionario = _tipoID;

                    //if (_xmlNotificacion.numSILPA == ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
                    //{
                    //    _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(ConfigurationManager.AppSettings["ActoAdministrarivoNotificacion"].ToString()), null);
                    //}
                    //else
                    //{
                    _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(_xmlNotificacion.tipoActoAdministrativo), null);
                    //}
                    _objNotificacion.IdTipoActo = _tipoActo;

                    _dependenciaEntidad.ID = _xmlNotificacion.IdDependenciaEntidad;

                    _objNotificacion.DependenciaEntidad = _dependenciaEntidad;

                    _objNotificacion.CodigoEntidadPublica = Convert.ToInt32(_xmlNotificacion.EntidadPublicaNot);

                    _objNotificacion.FechaActo = _xmlNotificacion.fechaActoAdministrativo;

                    List<byte[]> listaArchivos = new List<byte[]>();
                    listaArchivos.Add(_xmlNotificacion.datosArchivo);
                    List<string> nombres = new List<string>();
                    nombres.Add(_xmlNotificacion.nombreArchivo);
                    string ruta = "";
                    if (_xmlNotificacion.numSILPA != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
                    {
                        solicitud = _solicitudDalc.ObtenerSolicitud(null, null, _xmlNotificacion.numSILPA);
                        traficoArchivos.RecibirDocumento(_xmlNotificacion.numSILPA, solicitud.IdSolicitante.ToString(), listaArchivos, ref nombres, ref ruta);
                    }
                    else
                    {
                        traficoArchivos.RecibirDocumento(_xmlNotificacion.numSILPA, _objNotificacion.IdentificacionFuncionario, listaArchivos, ref nombres, ref ruta);
                    }
                    if (nombres[0] != null)
                        _objNotificacion.RutaDocumento = nombres[0].ToString();

                    //Cargar autoridad ambiental
                    _objNotificacion.CodigoAA = _xmlNotificacion.CodigoAutoridadAmbiental;

                    //Consultar estado en que deben ingresar acto administrativo y personas
                    objEstadoInicialActoParametroDalc = new EstadoInicialActoParametroDalc();
                    objEstadosActosAdministrativo = objEstadoInicialActoParametroDalc.ObtenerEstadoInicialEntidad(Convert.ToInt32(_objNotificacion.CodigoAA));
                    if (objEstadosActosAdministrativo != null)
                        _objNotificacion.EstadoActo = objEstadosActosAdministrativo.EstadoInicialActoID.ToString();
                    else
                    {
                        SMLog.Escribir(Severidad.Critico, "No se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + ". No se encontro estados iniciales para la entidad " + _objNotificacion.CodigoAA);
                        return "No se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + ". No se encontro estados iniciales para la entidad " + _objNotificacion.CodigoAA;
                    }

                    //Crear acto administrativo
                    _objNotificacionDalc.Insertar(ref _objNotificacion);

                    //Si es un cobro se registra marca indicando la marca correspondiente
                    if (_xmlNotificacion.esCobro != null && _xmlNotificacion.esCobro.Value && _xmlNotificacion.CobroIdentificador != null && _xmlNotificacion.CobroIdentificador.Value > 0)
                    {
                        _objNotificacionDalc.AsociarCobroNotificacion(_objNotificacion.IdActoNotificacion, _xmlNotificacion.CobroIdentificador.Value);
                    }

                    AccesoDatos.Documento.MisTramitesDocumentosDALC _rutas = new AccesoDatos.Documento.MisTramitesDocumentosDALC();
                    _rutas.InsertarDocumentos(_xmlNotificacion.CodigoAutoridadAmbiental, _xmlNotificacion.numSILPA, _xmlNotificacion.numActoAdministrativo, ruta);

                    /*Se crea la respuesta para tomar datos relacionados  con la publicación */
                    WSRespuesta xmlRespuesta = new WSRespuesta();
                    xmlRespuesta.Exito = true;
                    xmlRespuesta.IdExterno = _objNotificacion.IdActoNotificacion.ToString();
                    xmlRespuesta.IdSilpa = _objNotificacion.NumeroSILPA;

                    _objNotificacion.ListaPersonas = new List<PersonaNotificarEntity>();
                    //Se agrega la lista de personas (pero aún no se instertan en la base de datos, hasta que se envien a PDI)
                    if (_objNotificacion.EsNotificacion != null && _objNotificacion.EsNotificacion.Value && _xmlNotificacion.listaPersonas == null)
                    {
                        SMLog.Escribir(Severidad.Critico, "No se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + ". No se especifico personas para notificar. Mensaje de error antes de la confirmación de notificación electrónica");
                        return "No se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + ". No se especifico personas para notificar. Mensaje de error antes de la confirmación de notificación electrónica";
                    }
                    else if (_objNotificacion.EsCumplase != null && _objNotificacion.EsCumplase.Value && _xmlNotificacion.listaPersonas == null)
                    {
                        SMLog.Escribir(Severidad.Critico, "No se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + ". No se especifico personas para comunicar. Mensaje de error antes de la confirmación de notificación electrónica");
                        return "No se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + ". No se especifico personas para cumplir. Mensaje de error antes de la confirmación de notificación electrónica";
                    }
                    else
                    {

                        if(_xmlNotificacion.listaPersonas != null && _xmlNotificacion.listaPersonas.Length > 0)
                        {

                            //Ciclo que carga las notificaciones
                            foreach (PersonaType pt in _xmlNotificacion.listaPersonas)
                            {
                                personaNotificar = new PersonaNotificarEntity();
                                personaNotificar.IdActoNotificar = _objNotificacion.IdActoNotificacion;
                                personaNotificar.NumeroIdentificacion = pt.numeroIdentificacion;
                                personaNotificar.TipoIdentificacion = _tipoIDDalc.ObtenerTipoIdentificacionPorCodigo(pt.ipoIdentificacion.ToString());
                                personaNotificar.TipoPersona = tipoPersonaDalc.ListarTipoPersona(new object[] { (int)pt.tipoPersona });

                                if (pt.numeroNIT != 0)
                                {
                                    personaNotificar.NumeroNIT = pt.numeroNIT;
                                    personaNotificar.DigitoVerificacionNIT = pt.digitoVerificacionNIT;
                                }
                                personaNotificar.PrimerApellido = pt.primerApellido;
                                personaNotificar.SegundoApellido = pt.segundoApellido;
                                personaNotificar.PrimerNombre = pt.primerNombre;
                                personaNotificar.SegundoNombre = pt.segundoNombre;
                                personaNotificar.FechaNotificado = _objNotificacion.FechaActo;
                                if(_xmlNotificacion.esCumplase != null && _xmlNotificacion.esCumplase.Value)
                                    personaNotificar.TipoNotificacionId = (int)TipoNotificacion.CUMPLASE;
                                else
                                    personaNotificar.TipoNotificacionId = (int)TipoNotificacion.NOTIFICACION;
                                if (pt.razonSocial != string.Empty)
                                    personaNotificar.RazonSocial = pt.razonSocial;

                                _objNotificacion.ListaPersonas.Add(personaNotificar);
                            }                            
                        }
                    }

                    //Si es comunicación y no se especifica mensaje generar error
                    if (_objNotificacion.EsComunicacion != null && _objNotificacion.EsComunicacion.Value && _xmlNotificacion.listaPersonaComunicar == null)
                    {
                        SMLog.Escribir(Severidad.Critico, "No se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + ". No se especifico personas para comunicar. Mensaje de error antes de la confirmación de notificación electrónica");
                        return "No se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + ". No se especifico personas para comunicar. Mensaje de error antes de la confirmación de notificación electrónica";
                    }
                    else
                    {
                        if (_xmlNotificacion.listaPersonaComunicar != null && _xmlNotificacion.listaPersonaComunicar.Length > 0)
                        {

                            //Ciclo que carga personas comunicaciones notificaciones
                            foreach (PersonaType pt in _xmlNotificacion.listaPersonaComunicar)
                            {
                                personaNotificar = new PersonaNotificarEntity();
                                personaNotificar.IdActoNotificar = _objNotificacion.IdActoNotificacion;
                                personaNotificar.NumeroIdentificacion = pt.numeroIdentificacion;
                                personaNotificar.TipoIdentificacion = _tipoIDDalc.ObtenerTipoIdentificacionPorCodigo(pt.ipoIdentificacion.ToString());
                                personaNotificar.TipoPersona = tipoPersonaDalc.ListarTipoPersona(new object[] { (int)pt.tipoPersona });

                                if (pt.numeroNIT != 0)
                                {
                                    personaNotificar.NumeroNIT = pt.numeroNIT;
                                    personaNotificar.DigitoVerificacionNIT = pt.digitoVerificacionNIT;
                                }
                                personaNotificar.PrimerApellido = pt.primerApellido;
                                personaNotificar.SegundoApellido = pt.segundoApellido;
                                personaNotificar.PrimerNombre = pt.primerNombre;
                                personaNotificar.SegundoNombre = pt.segundoNombre;
                                personaNotificar.FechaNotificado = _objNotificacion.FechaActo;
                                personaNotificar.TipoNotificacionId = (int)TipoNotificacion.COMUNICACION;
                                if (pt.razonSocial != string.Empty)
                                    personaNotificar.RazonSocial = pt.razonSocial;

                                _objNotificacion.ListaPersonas.Add(personaNotificar);
                            }
                        }
                    }

                    #region Creación del Proceso en PDI
                    NotificacionPDI.NotificacionPDI wsNotificacion = new SILPA.LogicaNegocio.NotificacionPDI.NotificacionPDI();
                    wsNotificacion.Url = SILPA.Comun.DireccionamientoWS.UrlWS("NotificacionPDI");
                    wsNotificacion.Credentials = SILPA.Comun.DireccionamientoWS.Credenciales();

                    SMLog.Escribir(Severidad.Informativo, "----wsNotificacion.Url: " + wsNotificacion.Url);
                    //     string xmlGEL = SetXMLGELProcesoNotificacionEntrada(_objNotificacion, _xmlNotificacion);
                    //     Escribir(xmlGEL);
                    //...
                    //.. Se consume el servicio de PDI - Agilizadores
                    ParametroEntity _parametroTramite = new ParametroEntity();
                    ParametroEntity _parametroVersion = new ParametroEntity();
                    _parametroTramite.IdParametro = -1;
                    _parametroTramite.NombreParametro = "tramite_crear_notificacion";
                    _parametroVersion.IdParametro = -1;
                    _parametroVersion.NombreParametro = "version_crear_notificacion";
                    _parametroDalc.obtenerParametros(ref _parametroTramite);
                    _parametroDalc.obtenerParametros(ref _parametroVersion);


                    string resultadoPDI = "";

                    SMLog.Escribir(Severidad.Informativo, "----_parametroTramite.Parametro: " + _parametroTramite.Parametro);
                    SMLog.Escribir(Severidad.Informativo, "----_parametroVersion.Parametro: " + _parametroVersion.Parametro);
                    if (nombres[0] != null)
                    {
                        SMLog.Escribir(Severidad.Informativo, "----nombres[0].ToString(): " + nombres[0].ToString());
                    }

                    ////////////////////////////////////////////////////////////
                    /*  10-nov-10- HAVA - CONTROL DE CAMBIOS NOTIFICACION 
                        * Insertamos las personas antes de ejecutar la notificación
                    */

               

                    foreach (PersonaNotificarEntity per in _objNotificacion.ListaPersonas)
                    {
                        SMLog.Escribir(Severidad.Critico, "-CrearProceso 6");
                        PersonaNotificarEntity personaInsertar = new PersonaNotificarEntity();                        
                        SolicitanteEntity solicitante = new SolicitanteEntity();
                        SolicitanteDalc dalc = new SolicitanteDalc();

                        solicitante = dalc.ConsultaSolicitante(null, per.NumeroIdentificacion);

                        if ((per.TipoNotificacionId == (int)TipoNotificacion.NOTIFICACION) && (solicitante.EsNotificacionElectronica || solicitante.EsNotificacionElectronica_AA || solicitante.EsNotificacionElectronica_EXP))
                        {
                            if (solicitante.EsNotificacionElectronica_EXP == true)
                            {
                                SolicitudNotificacion not = new SolicitudNotificacion();
                                bool esnot = not.ConsultarExpedientePersonaNotificar(solicitante.NumeroIdentificacion, _xmlNotificacion.CodigoExpediente, _xmlNotificacion.numSILPA);

                                if (esnot)
                                {
                                    per.EsNotificacionElectronica = true;
                                }
                                else
                                {
                                    per.EsNotificacionElectronica = false;
                                }
                            }
                            else
                            {
                                //Se verifica si la autoridad ambiental se encuentra inscrita a notificación electrónica
                                if (this.AutoridadAmbientalInscritaNotificacion(int.Parse(_xmlNotificacion.CodigoAutoridadAmbiental)))
                                {
                                    per.EsNotificacionElectronica = true;
                                }
                                else
                                {
                                    per.EsNotificacionElectronica = false;
                                }
                            }
                      
                        }
                        else
                        {
                            per.EsNotificacionElectronica = false;
                        }
                    
                        //Cargar información del flujo de acuerdo al tipo de publicidad
                        objFlujoNotificacionDalc = new FlujoNotificacionElectronicaDalc();
                        if (per.TipoNotificacionId == (int)TipoNotificacion.NOTIFICACION)
                        {
                            objInformacionFlujo = objFlujoNotificacionDalc.ConsultarFlujoPorParametros(Convert.ToInt32(_xmlNotificacion.CodigoAutoridadAmbiental),
                                                                                                        true,
                                                                                                        false,
                                                                                                        false,
                                                                                                        (_xmlNotificacion.esNotificacionEdicto == null ? false : _xmlNotificacion.esNotificacionEdicto.Value),
                                                                                                        (_xmlNotificacion.esNotificacionEstrado == null ? false : _xmlNotificacion.esNotificacionEstrado.Value),
                                                                                                        (_xmlNotificacion.aplicaRecurso != null ? _xmlNotificacion.aplicaRecurso.Value : true),
                                                                                                        per.EsNotificacionElectronica);
                        }
                        else if (per.TipoNotificacionId == (int)TipoNotificacion.CUMPLASE)
                        {
                            objInformacionFlujo = objFlujoNotificacionDalc.ConsultarFlujoPorParametros(Convert.ToInt32(_xmlNotificacion.CodigoAutoridadAmbiental),
                                                                                                        false,
                                                                                                        false,
                                                                                                        true,
                                                                                                        false,
                                                                                                        false,
                                                                                                        (_xmlNotificacion.aplicaRecurso != null ? _xmlNotificacion.aplicaRecurso.Value : true),
                                                                                                        per.EsNotificacionElectronica);
                        }
                        else if (per.TipoNotificacionId == (int)TipoNotificacion.COMUNICACION)
                        {
                            objInformacionFlujo = objFlujoNotificacionDalc.ConsultarFlujoPorParametros(Convert.ToInt32(_xmlNotificacion.CodigoAutoridadAmbiental),
                                                                                                        (_xmlNotificacion.esNotificacion != null ? _xmlNotificacion.esNotificacion.Value : true),
                                                                                                        true,
                                                                                                        (_xmlNotificacion.esCumplase != null ? _xmlNotificacion.esCumplase.Value : false),
                                                                                                        false,
                                                                                                        false,
                                                                                                        false,
                                                                                                        per.EsNotificacionElectronica);
                        }

                        if (objInformacionFlujo == null || objInformacionFlujo.Rows.Count == 0)
                        {
                            SMLog.Escribir(Severidad.Critico, "No se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + ". No se encontro flujo para asignar para usuario" + per.NumeroIdentificacion + ".");
                            return "No se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + ". No se encontro flujo para asignar para el usuario " + per.NumeroIdentificacion;
                        }
                        else
                        {
                            //Obtener el flujo que corresponda de acuerdo a configuracion
                            if (objInformacionFlujo.Rows.Count == 1)
                            {
                                //Cargar flujo
                                per.FlujoNotificacionId = Convert.ToInt32(objInformacionFlujo.Rows[0]["FLUJO_ID"]);

                                //Cargar estado inicial
                                if (per.EstadoNotificado == null) per.EstadoNotificado = new EstadoNotificacionEntity();
                                EstadoNotificacionDalc estadonotdalc = new EstadoNotificacionDalc();
                                per.EstadoNotificado = estadonotdalc.ListarEstadoNotificacion(new object[] { Convert.ToInt32(objInformacionFlujo.Rows[0]["ESTADO_ID"]) });
                            }
                            else if (objInformacionFlujo.Rows.Count > 1)
                            {
                                objDatosFlujo = objInformacionFlujo.Select("AUT_ID = " + _xmlNotificacion.CodigoAutoridadAmbiental);
                                if (objDatosFlujo == null || objDatosFlujo.Length == 0)
                                {
                                    //Cargar flujo
                                    per.FlujoNotificacionId = Convert.ToInt32(Convert.ToInt32(objInformacionFlujo.Rows[0]["FLUJO_ID"]));

                                    //Cargar estado inicial
                                    if (per.EstadoNotificado == null) per.EstadoNotificado = new EstadoNotificacionEntity();
                                    EstadoNotificacionDalc estadonotdalc = new EstadoNotificacionDalc();
                                    per.EstadoNotificado = estadonotdalc.ListarEstadoNotificacion(new object[] { Convert.ToInt32(objInformacionFlujo.Rows[0]["ESTADO_ID"]) });
                                }
                                else
                                {
                                    //Cargar flujo
                                    per.FlujoNotificacionId = Convert.ToInt32(Convert.ToInt32(objDatosFlujo[0]["FLUJO_ID"]));

                                    //Cargar estado inicial
                                    if (per.EstadoNotificado == null) per.EstadoNotificado = new EstadoNotificacionEntity();
                                    EstadoNotificacionDalc estadonotdalc = new EstadoNotificacionDalc();
                                    per.EstadoNotificado = estadonotdalc.ListarEstadoNotificacion(new object[] { Convert.ToInt32(objDatosFlujo[0]["ESTADO_ID"]) });
                                }
                            }

                            //Cargar estado persona
                            per.EstadoPersonaID = objEstadosActosAdministrativo.EstadoInicialPersonaID;

                            //Insertar persona
                            personaInsertar = per;
                            _personaDalc.Insertar(ref personaInsertar);                                                  
                        }

                        personaInsertar = null;
                    }

                    SMLog.Escribir(Severidad.Critico, "-CrearProceso 7");
                    /*
                    * Examinamos si el servicio de pdi esta respondiendo adecuadamente
                    * Si no es asi, terminamos aqui el proceso
                    * HAVA: 16-NOV-2010
                    */

                    //    xmlRespuesta.Exito = false;
                    xmlRespuesta.IdExterno = _objNotificacion.IdActoNotificacion.ToString();
                    xmlRespuesta.IdSilpa = _objNotificacion.NumeroSILPA;


                    // se verifica q exista respuesta desde el servicio local
                    //if (!TestPDI(wsNotificacion.Url))
                    //        return xmlRespuesta.GetXml();
                    //else 
                    //{
                    // el servicio local verificala respuesta desde el PDI real 


                    // no sirve el PDI y como lo estamos llamando de una mejor lo quito
                    return xmlRespuesta.GetXml();
                    if (!wsNotificacion.testPDI())
                    {
                        //Envía el correo indicando que la notificación con PDI falló
                        EnviarCorreoFallaPDI(_objNotificacion);

                    }

                    //}

                    /*Fin del getResponse()*/


                    // capturamos el error al consumir el servicio PDI LOCAL
                    //try
                    //{
                    //    resultadoPDI = wsNotificacion.EjecutarNotificacion(xmlGEL, _parametroTramite.Parametro, _parametroVersion.Parametro, nombres[0].ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    resultadoPDI = String.Empty;
                    //    SMLog.Escribir(Severidad.Informativo, "----error: al ejecutar: wsNotificacion.EjecutarNotificacion() " + ex.Message);
                    //    SMLog.Escribir(ex);
                    //}
                    /*FIN DE CODIGO  DE CONTROL DE CAMBIOS NOTIFICACION 10-NOV-10*/
                    ////////////////////////////////////////////////////////////

                    SMLog.Escribir(Severidad.Informativo, "----Resultado de Notificacion: " + resultadoPDI);
                    int resultado = 0;
                    string mensaje = string.Empty;
                    //EscribirArchivo("----: id de la plantilla " + _objNotificacion.IdPlantilla);
                    //SMLog.Escribir((Severidad.Informativo, "FileTraffic: id de la plantilla " + _objNotificacion.IdPlantilla);    
                    resultado = GetXMLGELProcesoNotificacionSalida(resultadoPDI, ref _objNotificacion, out mensaje);
                    Escribir("----resultado " + resultado.ToString());
                    //SMLog.Escribir((Severidad.Informativo, "FileTraffic: resultado " + resultado);    
                    if (resultado > 0)
                    {
                        foreach (PersonaNotificarEntity per in _objNotificacion.ListaPersonas)
                        {
                            //SMLog.Escribir((Severidad.Informativo, "Existen personas a Notificar");    
                            PersonaNotificarEntity personaActualizar = new PersonaNotificarEntity();
                            personaActualizar = per;

                            /*
                                * 10-nov-10: hava: control de cambios de Notificación.
                                * Se comenta el metodo insertar y se reemplaza po actualizar
                                */
                            // Se comenta para llamar al método de actualización y NO al de inserción
                            //_personaDalc.Insertar(ref personaInsertar);
                            //_personaDalc.Actualizar(ref personaActualizar);
                            // Se actualiza el estado desde PDI


                            SolicitanteEntity solicitante = new SolicitanteEntity();
                            SolicitanteDalc dalc = new SolicitanteDalc();

                            solicitante = dalc.ConsultaSolicitante(null, per.NumeroIdentificacion);

                            if (solicitante.EsNotificacionElectronica || solicitante.EsNotificacionElectronica_AA || solicitante.EsNotificacionElectronica_EXP)
                            {
                                    per.EsNotificacionElectronica = true;
                            }
                            else
                            {
                                per.EsNotificacionElectronica = false;
                            }
                            SMLog.Escribir(Severidad.Critico, "-CrearProceso 7A");
                            _personaDalc.Actualizar(ref personaActualizar, 1);
                            SMLog.Escribir(Severidad.Informativo, "Persona actualizada " + personaActualizar.NumeroIdentificacion.ToString());
                            personaActualizar = null;
                        }
                    }
                    else
                    {
                        Escribir("El resultado es error" + mensaje);
                        return mensaje;
                    }



                    //EstadoNotificacionEntity _estadoNotificacion = new EstadoNotificacionEntity();
                    //_estadoNotificacion.ID = 1;
                    //Random r = new Random(1000);
                    //_objNotificacion.Estado = _estadoNotificacion;
                    //_objNotificacion.ConsecutivoNotificacion = r.Next(9999).ToString();
                    #endregion


                    //escribir("Retorna Resultado");
                    //SMLog.Escribir((Severidad.Informativo, "Se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + " Mensaje de confirmación de notificación electrónica:" + mensaje);     		

                    xmlRespuesta.CodigoMensaje = "Creación proceso notificación";
                    xmlRespuesta.Mensaje = "Se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + " Mensaje de confirmación de notificación electrónica:" + mensaje;
                    //return "Se creó el proceso para el número SILPA: " + _objNotificacion.NumeroSILPA + " Mensaje de confirmación de notificación electrónica:" + mensaje;
                    return xmlRespuesta.GetXml();
                }
                catch (Exception ex)
                {
                    SMLog.Escribir(Severidad.Informativo, "----Hubo un error Creando proceso Notificación: " + ex.Message);

                    string strException = "Validar los pasos efectuados al Crear Proceso de Notificación.";
                    throw new Exception(strException, ex);
                }
            }


            /// <summary>
            /// Verifica que el servicio de PDI este respondiendo correctamente
            /// </summary>
            /// <param name="url">string: url del servicio</param>
            /// <returns>bool: true/false</returns>
            public bool TestPDI(string sUrl)
            {
                bool result = false;
                string respuesta = String.Empty;
                try
                {

                    WebRequest wReq = WebRequest.Create(sUrl);
                    //int prt = 0;
                    //WebProxy wPxy = new WebProxy(hst,prt);
                    //int_proxy_puerto
                    //str_proxy_server
                    wReq.Timeout = 900000;
                    WebResponse WebRes = null;

                    WebRes = wReq.GetResponse();
                    if (WebRes == null)
                    {
                        respuesta = String.Empty;
                    }
                    else
                    {
                        respuesta = WebRes.ContentLength.ToString();
                    }

                    if (!String.IsNullOrEmpty(respuesta))
                    {
                        //Si hubo respuesta del servicio de PDI
                        result = true;
                    }
                    else
                    {
                        //No hubo respuesta del servicio de PDI
                        result = false;
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        
            /// <summary>
            /// Consulta un estado de Notificación para la Autoridad Ambiental en SILPA
            /// </summary>
            /// <param name="xmlDatos">Datos del Acto Administrativo para la Prueba</param>
            /// <returns>XML con el acto, y la lista de personas con el estado Notificado para cada persona</returns>
            public string ConsultarEstado(string xmlDatos)
            {
                XmlSerializador _objSer = new XmlSerializador();
                NotificacionConsultaType _xmlConsulta = new NotificacionConsultaType();
                _xmlConsulta = (NotificacionConsultaType)_objSer.Deserializar(new NotificacionConsultaType(), xmlDatos);

                NotificacionDalc dalc = new NotificacionDalc();
                List<NotificacionEntity> not = new List<NotificacionEntity>();
                not = dalc.ObtenerActoPorAA_VerificarEstado(_xmlConsulta.numActoAdministrativoNotificacion, _xmlConsulta.numProcesoAdministracion, _xmlConsulta.numTipoDocumento, _xmlConsulta.numIdAA);
                RespuestaEstadoNotificacionType respuesta = new RespuestaEstadoNotificacionType();

                int index = 0;

                foreach (NotificacionEntity itemX in not)
                {
                    index += itemX.ListaPersonas.Count;
                }

                PersonaType[] listaPersonas = new PersonaType[index];

                index = 0;

                foreach (NotificacionEntity item1 in not)
                {

                    respuesta.numActoAdministrativoNotificacion = item1.NumeroActoAdministrativo;
                    respuesta.numProcesoAdministracion = item1.ProcesoAdministracion;
                    if (item1.FechaActo != null)
                        respuesta.fechaEmisionActoAdministrativo = (DateTime)item1.FechaActo;
                    EstadoNotificadoType estadoNotificadoType = new EstadoNotificadoType();
                    foreach (PersonaNotificarEntity per in item1.ListaPersonas)
                    {
                        bool notificacion = false;

                        NotExpedientesEntityDalc noti = new NotExpedientesEntityDalc();

                        //List<PersonaNotExpedienteEntity> listaExpPer = noti.listarNotificadosExpedientes(item1.ProcesoAdministracion, item1.NumeroSILPA.Trim().Substring(0,item1.NumeroSILPA.IndexOf("-")));
                        PersonaNotExpedienteEntity expPer = noti.listarNotificadosEstadoExpedientes(item1.ProcesoAdministracion, item1.NumeroSILPA.Trim().Substring(0, item1.NumeroSILPA.IndexOf("-")), per.NumeroIdentificacion);

                        if (expPer != null)
                        {
                            if (expPer.EsNotificacionElec == true)
                            {
                                notificacion = true;
                            }
                        }
                        listaPersonas[index] = new PersonaType();
                        listaPersonas[index].primerNombre = per.PrimerNombre;
                        listaPersonas[index].segundoNombre = per.SegundoNombre;
                        listaPersonas[index].primerApellido = per.PrimerApellido;
                        listaPersonas[index].segundoApellido = per.SegundoApellido;


                        listaPersonas[index].ipoIdentificacion = (enumTipoIdentificacion)Enum.Parse(typeof(enumTipoIdentificacion), per.TipoIdentificacion.Sigla);

                        estadoNotificadoType = new EstadoNotificadoType();

                        string item = "";
                        if (notificacion == true)
                        {
                                item = "Item" + per.EstadoNotificado.ID.ToString();
                                estadoNotificadoType.codigoEstado = (enumCodigoEstadoNotificacion)Enum.Parse(typeof(enumCodigoEstadoNotificacion), item);
                                estadoNotificadoType.nombreEstado = (enumNombreEstadoNotificacion)Enum.Parse(typeof(enumNombreEstadoNotificacion), per.EstadoNotificado.Estado);

                        }
                        else
                        {
                       
                                item = "Item" + per.EstadoNotificado.ID.ToString();
                                estadoNotificadoType.codigoEstado = (enumCodigoEstadoNotificacion)Enum.Parse(typeof(enumCodigoEstadoNotificacion), item);
                                estadoNotificadoType.nombreEstado = (enumNombreEstadoNotificacion)Enum.Parse(typeof(enumNombreEstadoNotificacion), per.EstadoNotificado.Estado);

                        

                        }
                
                        listaPersonas[index].estadoNotificado = estadoNotificadoType;

                        if (per.NumeroIdentificacion.Trim() != string.Empty)
                            listaPersonas[index].numeroIdentificacion = per.NumeroIdentificacion;
                        else
                            listaPersonas[index].numeroIdentificacion = per.NumeroNIT.ToString() + per.DigitoVerificacionNIT.ToString();
                        if (per.RazonSocial.Trim() != string.Empty)
                            listaPersonas[index].razonSocial = per.RazonSocial;
                        else
                            listaPersonas[index].razonSocial = per.PrimerNombre + " " + per.SegundoNombre + " " + per.PrimerApellido + " " + per.SegundoApellido;
                        if (per.FechaNotificado != null)
                            listaPersonas[index].fechaEstadoNotificado = per.FechaNotificado;
                        else if (per.FechaEstadoNotificado != null)
                            listaPersonas[index].fechaEstadoNotificado = per.FechaEstadoNotificado;
                        //JACOSTA 20150218. se agrega esta propiedad para almacenar la fecha en la que se genero el estado de la persona.
                        if (per.FechaEstadoNotificado != null)
                            listaPersonas[index].FechaEstado = per.FechaEstadoNotificado;
                        index++;
                    }

                    respuesta.listaPersonas = listaPersonas;


                }
                string salida = _objSer.serializar(respuesta);
                return salida;

            }


            /// <summary>
            /// Consulta el estado de notificación para una persona en PDI
            /// </summary>
            /// <param name="persona"></param>
            /// <param name="acto"></param>
            /// <returns></returns>
            public void ActualizarEstado(PersonaNotificarEntity persona, NotificacionEntity acto)
            {
                SMLog.Escribir(Severidad.Informativo, "----Inicio ActualizarEstado");
                PersonaNotificarDalc personaDalc = new PersonaNotificarDalc();
                ParametroDalc _parametroDalc = new ParametroDalc();
                ParametroEntity _parametro1 = new ParametroEntity();
                ParametroEntity _parametro2 = new ParametroEntity();
                _parametro1.IdParametro = -1;
                _parametro1.NombreParametro = "tramite_consultar_estado_notificacion";
                _parametro2.IdParametro = -1;
                _parametro2.NombreParametro = "version_consultar_estado_notificacion";
                _parametroDalc.obtenerParametros(ref _parametro1);
                _parametroDalc.obtenerParametros(ref _parametro2);
                string xmlEntrada = SetXMLGELEstadoNotificacionEntrada(persona, acto);
                SMLog.Escribir(Severidad.Informativo, "----persona" + persona.NumeroIdentificacion.ToString());
                NotificacionPDI.NotificacionPDI wsnotificacion = new SILPA.LogicaNegocio.NotificacionPDI.NotificacionPDI();
                wsnotificacion.Url = SILPA.Comun.DireccionamientoWS.UrlWS("NotificacionPDI");
                wsnotificacion.Credentials = DireccionamientoWS.Credenciales();

                string xmlRespuesta = wsnotificacion.EjecutarNotificacion(xmlEntrada, _parametro1.Parametro, _parametro2.Parametro, "");
                //            int estadoActual = persona.EstadoNotificado.ID;
                bool resultado = false;
                resultado = GetXMLGELEstadoNotificacionSalida(xmlRespuesta, ref persona, acto);
                //            int estadoNuevo = persona.EstadoNotificado.ID;
                //          JALCALA 2010-07-26 Debido a la condición, no actualizaba el registro.  Se modificó para que siempre actualice.
                //if (estadoActual != estadoNuevo && resultado)
                //{
                //    personaDalc.Actualizar(ref persona);
                //}
                //
                SMLog.Escribir(Severidad.Informativo, "----Va a actualizar el estado de la persona:" + persona.EstadoNotificado.Estado);
                personaDalc.Actualizar(ref persona);
            }


            /// <summary>
            /// Consulta el estado de notificación para una persona en PDI
            /// </summary>
            /// <param name="persona"></param>
            /// <param name="acto"></param>
            /// <returns></returns>
            public void ActualizarEstado(PersonaNotificarEntity persona, NotificacionEntity acto, int idEstado, string strEstado, string esPDI)
            {
                int provienePDI = 0;
                PersonaNotificarDalc personaDalc = new PersonaNotificarDalc();
                //if ((esPDI == "SI") || (esPDI == "NO" && strEstado == enumNombreEstadoNotificacion.SIN_INICIAR.ToString())) 
                if ((esPDI == "SI") || (strEstado == enumNombreEstadoNotificacion.SIN_INICIAR.ToString()))
                {

                    SMLog.Escribir(Severidad.Informativo, "ENT A SetXMLGELEstadoNotificacionEntrada");

                    ParametroDalc _parametroDalc = new ParametroDalc();
                    ParametroEntity _parametro1 = new ParametroEntity();
                    ParametroEntity _parametro2 = new ParametroEntity();
                    _parametro1.IdParametro = -1;
                    _parametro1.NombreParametro = "tramite_consultar_estado_notificacion";
                    _parametro2.IdParametro = -1;
                    _parametro2.NombreParametro = "version_consultar_estado_notificacion";
                    _parametroDalc.obtenerParametros(ref _parametro1);
                    _parametroDalc.obtenerParametros(ref _parametro2);

                    string xmlEntrada = SetXMLGELEstadoNotificacionEntrada(persona, acto);

                    SMLog.Escribir(Severidad.Informativo, "----persona" + persona.NumeroIdentificacion.ToString());
                    NotificacionPDI.NotificacionPDI wsnotificacion = new SILPA.LogicaNegocio.NotificacionPDI.NotificacionPDI();
                    wsnotificacion.Url = SILPA.Comun.DireccionamientoWS.UrlWS("NotificacionPDI");
                    wsnotificacion.Credentials = DireccionamientoWS.Credenciales();

                    persona.EstadoNotificado.ID = idEstado;
                    persona.EstadoNotificado.Estado = strEstado;


                    SMLog.Escribir(Severidad.Informativo, "----Entrar a PDI:" + persona.EstadoNotificado.Estado);
                    // Si Notificación Electronica responde entonces se consulta su estado
                    //if (this.TestPDI(wsnotificacion.Url))
                    if (wsnotificacion.testPDI())
                    {
                        string xmlRespuesta = wsnotificacion.EjecutarNotificacion(xmlEntrada, _parametro1.Parametro, _parametro2.Parametro, "");
                        //            int estadoActual = persona.EstadoNotificado.ID;
                        bool resultado = false;
                        resultado = GetXMLGELEstadoNotificacionSalida(xmlRespuesta, ref persona, acto);
                        provienePDI = 1;
                        //            int estadoNuevo = persona.EstadoNotificado.ID;
                        //          JALCALA 2010-07-26 Debido a la condición, no actualizaba el registro.  Se modificó para que siempre actualice.
                        //if (estadoActual != estadoNuevo && resultado)
                        //{
                        //    personaDalc.Actualizar(ref persona);
                        //}
                        //
                        SMLog.Escribir(Severidad.Informativo, "----Va a actualizar el estado de la persona:" + persona.EstadoNotificado.Estado);
                        //personaDalc.Actualizar(ref persona);
                    }
                    else
                    {

                        //Envía el correo indicando que la notificación con PDI falló
                        EnviarCorreoFallaPDI(acto);
                        //SMLog.Escribir(Severidad.Informativo, "----NO ENTRO a PDI:" + persona.EstadoNotificado.Estado);
                    }

                }

                SMLog.Escribir(Severidad.Informativo, "----SALIENDO DE PDI:" + persona.EstadoNotificado.Estado);
                personaDalc.Actualizar(ref persona, provienePDI);

            }


            #region Sección GEL-XML


                /// <summary>
                /// Verifica que se pueda publicar un acto dependiendo del esado de las personas
                /// </summary>
                /// <param name="not"></param>
                public void ConsultarTodosPublicables(NotificacionEntity not)
                {
                    // si requiere publicación
                    if (not.RequierePublicacion)
                    {
                        //Se determina si cada persona cumple con los estados para publicar
                        //not.IdTipoActo.ID
                        NotificacionDalc dalc = new NotificacionDalc();
                        int result = dalc.ConsultarTodosPublicacion(not.IdActoNotificacion, not.IdTipoActo.ID);
                        //Si Los estados de todas las personas a notificar se encuentran dentro de las posibles para publicar entonces 
                        //actualizamos la publicación
                        if (result==1)
                        {
                            PublicacionDalc pubDalc = new PublicacionDalc();
                            pubDalc.ActualizarPublicacionPorNotificacion(not.IdActoNotificacion);
                        }
                    }
                }

            #endregion

            /// <summary>
            /// Obtiene el  Listado de los estados de notificación
            /// </summary>
            public List<EstadosNotificacionSelect> ObtenerEstadosNotificacion
                (long idApplicationUser, string numeroVital, string numeroExpediente, DateTime FechaDesde, DateTime FechaHasta,
                    int idTipoActoAdministrativo, string numeroActoAdministrativo, string usuario, string identificacionUsuario,
                    int? diasVencimiento, int esDatoPDI, string idProcesoNotificacion, int estadoActual, int estadoNotificacion)
            {
                Object[] parametros =  {idApplicationUser, numeroVital,  numeroExpediente, FechaDesde, FechaHasta,
                    idTipoActoAdministrativo, numeroActoAdministrativo, usuario, identificacionUsuario,
                    diasVencimiento, esDatoPDI, idProcesoNotificacion, estadoActual,estadoNotificacion};

                EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
                return dalc.ObtenerEstadosNotificacion(parametros);
            }

            public List<EstadosNotificacionSelect> ObtenerNotificacionesPublico(string numeroVital, string numeroExpediente, DateTime? FechaDesde, DateTime? FechaHasta,
                    int idTipoActoAdministrativo, string numeroActoAdministrativo, string identificacionUsuario,
                    int? idautoambiental, int estadoActual, int estadoNotificacion)
            {
                Object[] parametros =  {numeroVital,  numeroExpediente, FechaDesde, FechaHasta,
                    idTipoActoAdministrativo, numeroActoAdministrativo, identificacionUsuario,
                    idautoambiental, estadoActual,estadoNotificacion};

                EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
                return dalc.ObtenerNotificacionPublico(parametros);
            }


            public List<EstadosNotificacionSelect> ObtenerNotificacionesParaAvanzarFlujoAutomatico()
            {
                EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
                return dalc.ObtenerNotificacionesParaAvanzarFlujoAutomatico();
            }


            /// <summary>
            /// Obtiene los datos del estado del persona por acto
            /// </summary>
            /// <param name="idActo">long:identificador del acto</param>
            /// <param name="idEstado">int: identificador del estado</param>
            /// <param name="idPersona">long: identificador de la persona</param>
            public EstadosNotificacionSelect CargarDatosEstadoPersona(long idActo, int idEstado, long idPersona)
            {
                EstadosNotificacionSelect estado = new EstadosNotificacionSelect();
                estado.ID = idActo;
                estado.IdEstadoNotificado = idEstado;
                estado.IdPersonaNotificar = idPersona;

                return estado;
            }


            /// <summary>
            /// 08-Nov-2010
            /// </summary>
            /// <param name="idActo"></param>
            /// <param name="idEstado"></param>
            /// <param name="idPersona"></param>
            /// <param name="textoCorreo"></param>
            /// <param name="nombreArchivo"></param>
            /// <param name="bytes"></param>
            public string CrearEstadoPersonaActo(long idActo, int idEstado, int idEstadoNuevo, long idPersona, DateTime fechaEstado,
                string numeroSilpa, string textoCorreo,
                string nombreArchivo, byte[] bytes, string accion, bool enviaCorreo, string funcionario, string expediente, string nroActoAdminsitrativo, int p_intAutoridadID, bool esModificable)
            {

                // valor por defecto
                string carpetaNotificacion = "CarpetaNotificacion";
                string rutaDocumento = string.Empty;
                string nombreAA = string.Empty;
                string nitAA = string.Empty;
                string telefonoAA = string.Empty;
                string strFechaEnvio = DateTime.Now.ToString();
                int intEnviaCorreo = Convert.ToInt32(enviaCorreo);
                NotificacionDalc objNotificacion = null;
                int intPlantillaCorreoID = 0;
                string strIdentificadorCorreo = "";

                List<string> lstNombres = new List<string>();
                lstNombres.Add(nombreArchivo);

                List<Byte[]> lstBytes = new List<byte[]>();
                lstBytes.Add(bytes);

                /// se verifica la existencia de la llave:
                if (ConfigurationManager.AppSettings["CarpetaNotificacion"] != null)
                {
                    carpetaNotificacion = ConfigurationManager.AppSettings["CarpetaNotificacion"].ToString();
                }

                carpetaNotificacion = carpetaNotificacion + @"\" + numeroSilpa + @"\";

                TraficoDocumento tf = new TraficoDocumento();
                tf.RecibirDocumento(carpetaNotificacion, idPersona.ToString(), lstBytes, ref lstNombres, ref rutaDocumento);


                EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();


                string mensajeExito = string.Empty;


                if (accion == "Crear")
                {
                    Object[] parametrosIns = { idActo, idEstadoNuevo, idPersona, fechaEstado, rutaDocumento, string.Empty, intEnviaCorreo, esModificable };
                    mensajeExito = dalc.CrearEstadoPersonaActo(parametrosIns);
                }
                else
                {
                    Object[] parametrosUpd = { idActo, idEstado, idEstadoNuevo, idPersona, fechaEstado, rutaDocumento, string.Empty, intEnviaCorreo };
                    mensajeExito = dalc.ActualizarEstadoNotificacionPersona(parametrosUpd);
                }


                if (String.IsNullOrEmpty(mensajeExito))
                {
                    if (enviaCorreo)
                    {
                        // Enviar correo:
                        nombreArchivo = lstNombres[0].ToString();
                        SILPA.LogicaNegocio.ICorreo.Correo correo = new SILPA.LogicaNegocio.ICorreo.Correo();
                        Persona per = new Persona();
                        if (numeroSilpa != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
                        {
                            per.ObternerPersonaByNumeroSilpa(numeroSilpa);
                        }
                        else
                        {
                            per.ObtenerPersonaNotificacion(idPersona);
                        }

                        //Cargar la plantilla de correo que se debe utilizar
                        objNotificacion = new NotificacionDalc();
                        intPlantillaCorreoID = objNotificacion.ConsultarPlantillaCorreoAutoridadPersona(p_intAutoridadID, idPersona);
                        if (intPlantillaCorreoID <= 0)
                        {
                            throw new Exception("No se eoncontro información de plantilla de correo. p_intAutoridadID: " + p_intAutoridadID.ToString());
                        }


                        string nombrePersona = per.Identity.PrimerNombre + " " + per.Identity.SegundoNombre + " " + per.Identity.PrimerApellido + " " + per.Identity.SegundoApellido;
                        List<string> lstArchivos = null;
                        if (!string.IsNullOrEmpty(nombreArchivo))
                        {
                            lstArchivos = new List<string>();
                            lstArchivos.Add(nombreArchivo);
                        }

                        //TODO Cargar identificador envío temporal de correos
                        if (p_intAutoridadID == (int)AutoridadesAmbientales.ANLA)
                        {
                            strIdentificadorCorreo = "(NRA100" + idActo.ToString() + idPersona.ToString() + idEstado.ToString() + idEstadoNuevo.ToString() + ")";
                            per.Identity.CorreoElectronico = per.Identity.CorreoElectronico + ".scmail.co";
                        }
                        else
                        {
                            strIdentificadorCorreo = "";
                        }

                        //TODO:JACOSTA 20121003
                        correo.EnviarCorreoNotificacionPersona(strIdentificadorCorreo, per.Identity.CorreoElectronico, nombrePersona, per.Identity.TipoDocumentoIdentificacion.Nombre, per.Identity.NumeroIdentificacion,
                                                                   textoCorreo, lstArchivos, expediente, strFechaEnvio, numeroSilpa, nroActoAdminsitrativo, "", intPlantillaCorreoID);


                    }
                    return string.Empty;
                }
                else
                {
                    return mensajeExito;
                }
            }


            /// <summary>
            /// hava:7-nov-2010
            /// </summary>
            /// <param name="idPersona">long: identificador de la persona </param>
            /// <param name="idActo">long: identificador del acto </param>
            /// <returns>string: nombre del estado actual</returns>
            public string ObtenerEstadoActual(long idPersona, long idActo, out int idEstado, out DateTime? fechaEstado)
            {
                EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
                return dalc.ObtenerEstadoActual(idPersona, idActo, out idEstado, out fechaEstado);
            }


            /// <summary>
            /// HAVA: determina si el tiempo de espera para el llamado 
            /// al sistema de notificación esta o no activo
            /// </summary>
            /// <returns>true/false</returns>
            public bool TiempoEsperaActivo()
            {
                int result = 0;
                TiempoNotificacionDalc tn = new TiempoNotificacionDalc();
                string strResult = tn.ObtenerTiempo(out result);
                return Convert.ToBoolean(result);
            }


            /// <summary>
            /// método que obtiene las fechas de citación y notificación 
            /// de un acto administrativo.
            /// </summary>
            /// <param name="numeroActo">string: numero del acto administrativo</param>
            /// <returns>string: dataset</returns>
            public string ConsultarFechaCitacionNotificacion(string numeroActo, int idAA, string codigoExpediente)
            {

                PersonaNotificarDalc dalc = new PersonaNotificarDalc();
                DataSet ds = dalc.ObtenerFechaCitacionNotificacion(numeroActo, idAA, codigoExpediente);
                // <?xml version="1.0" encoding="utf-8"?>
                //string result = "<?xml version="+ @"1.0" +  "encoding=" +@"utf-8"+@"" +"?>";
                return ds.GetXml();
            }

            /// <summary>
            /// Retorna el número de días que ha permanecido la notificación en el estado actual
            /// </summary>
            /// <param name="p_strIdPersona">string con el id de la persona que se notifica</param>
            /// <param name="p_strIdActo">string con identificador del acto de notificación</param>        
            /// <returns>int con el número de días</returns>
            public int ObtenerNumeroDiasNotificacion(string p_strIdPersona, string p_strIdActoNot)
            {
                PersonaNotificarDalc dalc = new PersonaNotificarDalc();
                return dalc.ObtenerNumeroDiasNotificacion(p_strIdPersona, p_strIdActoNot);
            }

            /// <summary>
            /// Lista las autoridades ambientales que se encuentran inscritas a notificación ele
            /// </summary>
            /// <param name="p_blnIntegradaNotificacionElectronica">bool indicando si extrae las autoridades integradas a notificación. Opcional</param>
            /// <returns>DataSet con los registros y las siguientes columnas: AUT_ID, AUT_NOMBRE, AUT_DIRECCION, AUT_TELEFONO, AUT_FAX, 
            /// AUT_CORREO, AUT_NIT, AUT_CARGUE, APLICA_RADICACION, AUT_ACTIVO</returns>
            public DataSet ListarAutoridadAmbientalNotificacion(bool? p_blnIntegradaNotificacionElectronica = null)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ListarAutoridadAmbientalNotificacion(p_blnIntegradaNotificacionElectronica);
            }


            /// <summary>
            /// Indica si una autoridad ambiental se encuentra inscrita a notificación electronica
            /// </summary>
            /// <param name="p_intIdAutoridad">Id de la autoridad ambiental</param>
            /// <returns>bool indicando si la autoridad ambiental esta inscrita a notificaión electronica</returns>
            public bool AutoridadAmbientalInscritaNotificacion(int p_intIdAutoridad)
            {
                try
                {
	                NotificacionDalc dalc = new NotificacionDalc();
	                return dalc.AutoridadAmbientalInscritaNotificacion(p_intIdAutoridad);
                }
                catch (Exception ex)
                {
                    string strException = "Validar los pasos efectuados al indicar si una autoridad ambiental se encuentra inscrita a notificación electrónica.";
                    throw new Exception(strException, ex);
                }
            }

            /// <summary>
            /// Verificar si existen pendientes de notificar
            /// </summary>
            /// <param name="p_lngIdActo">long con el identificador del acto que se comprobará</param>
            /// <returns>bool con true en caso de que existan pendientes, false en caso contrario.</returns>
            public bool ExistePendientesFinalizarNotificacionActo(long p_lngIdActo)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ExistePendientesFinalizarNotificacionActo(p_lngIdActo);
            }

            /// <summary>
            /// Verificar si existen pendientes de finalización por acto de notificación
            /// </summary>
            /// <param name="p_lngIdActo">long con el identificador del acto que se comprobará</param>
            /// <returns>bool con true en caso de que existan pendientes, false en caso contrario.</returns>
            public bool ExistePendientesFinalizarActo(long p_lngIdActo)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ExistePendientesFinalizarActo(p_lngIdActo);
            }

            /// <summary>
            /// Retorna el listado de usuarios que se encuentran relacionados a un acto de notificación
            /// </summary>
            /// <param name="p_lngIdActo">long con el identificador del acto que se comprobará</param>
            /// <returns>DataTable con la información de los usuarios.</returns>
            public DataTable ConsultarUsuariosActo(long p_lngIdActo)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ConsultarUsuariosActo(p_lngIdActo);
            }


            /// <summary>
            /// Realizar la consulta de información del reporte de notificaciones
            /// </summary>
            /// <param name="p_strNumeroVital">string con el número vital</param>
            /// <param name="p_strNumeroExpediente">string con el número de expediente</param>
            /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
            /// <param name="p_strNombreUsuario">string con el nombre del usuario</param>
            /// <param name="p_strNumeroActoAdministrativo">string con el número de acto administrativo</param>
            /// <param name="p_intTipoActoAdministrativo">int con el tipo de acto administrativo</param>
            /// <param name="p_objFechaInicio">DateTime con la fecha de inicio de busqueda</param>
            /// <param name="p_objFechaFinal">DateTime con la fecha final de busqueda</param>
            /// <param name="p_intUsuarioID">int con el id del usuario que realiza la consulta</param>
            /// <param name="p_blnConsultarNotificaciones">bool indica si se consulta notificaciones</param>
            /// <param name="p_blnConsultarComunicaciones">bool indica si se consulta comunicaciones</param>
            /// <param name="p_blnConsultarCumplase">bool que indica si se consultan cumplase</param>
            /// <param name="p_blnConsultarPublicacion">bool que indica si se consulta publicaciones</param>
            /// <returns>DataSet con la información del reporte</returns>
            public DataSet ConsultarReporteNotificaciones(string p_strNumeroVital, string p_strNumeroExpediente,
                                                        string p_strNumeroIdentificacion, string p_strNombreUsuario,
                                                        string p_strNumeroActoAdministrativo, int p_intTipoActoAdministrativo,
                                                        DateTime p_objFechaInicio, DateTime p_objFechaFinal,
                                                        int p_intUsuarioID, bool p_blnConsultarNotificaciones,
                                                        bool p_blnConsultarComunicaciones, bool p_blnConsultarCumplase,
                                                        bool p_blnConsultarPublicacion)
            {

                EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
                return dalc.ConsultarReporteNotificaciones(p_strNumeroVital,
                                                            p_strNumeroExpediente,
                                                            p_strNumeroIdentificacion,
                                                            p_strNombreUsuario,
                                                            p_strNumeroActoAdministrativo,
                                                            p_intTipoActoAdministrativo,
                                                            p_objFechaInicio,
                                                            p_objFechaFinal,
                                                            p_intUsuarioID,
                                                            p_blnConsultarNotificaciones,
                                                            p_blnConsultarComunicaciones,
                                                            p_blnConsultarCumplase,
                                                            p_blnConsultarPublicacion);

            }


            /// <summary>
            /// Consultar la información del acto administrativo a notificar
            /// </summary>
            /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo</param>
            /// <returns>DataTable con la información de la notificación</returns>
            public DataTable ConsultaActoNotificacion(long p_lngActoAdministrativoID)
            {
                EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
                return dalc.ConsultaActoNotificacion(p_lngActoAdministrativoID);
            }


            /// <summary>
            /// Realizar la consulta de información del reporte de notificaciones
            /// </summary>
            /// <param name="p_strNumeroVital">string con el número vital</param>
            /// <param name="p_strNumeroExpediente">string con el número de expediente</param>
            /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
            /// <param name="p_strNombreUsuario">string con el nombre del usuario</param>
            /// <param name="p_strNumeroActoAdministrativo">string con el número de acto administrativo</param>
            /// <param name="p_intTipoActoAdministrativo">int con el tipo de acto administrativo</param>
            /// <param name="p_objFechaInicio">DateTime con la fecha de inicio de busqueda</param>
            /// <param name="p_objFechaFinal">DateTime con la fecha final de busqueda</param>
            /// <param name="p_intAutoridadAmbiental">int con el id de la autoridad ambiental</param>
            /// <returns>DataSet con la información del reporte</returns>
            public DataSet ConsultaActoNostificacion(string p_strNumeroVital, string p_strNumeroExpediente,
                                                        string p_strNumeroIdentificacion, string p_strNombreUsuario,
                                                        string p_strNumeroActoAdministrativo, int p_intTipoActoAdministrativo,
                                                        DateTime p_objFechaInicio, DateTime p_objFechaFinal,
                                                        int p_intAutoridadAmbiental)
            {

                EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
                return dalc.ConsultaActoNostificacion(p_strNumeroVital,
                                                            p_strNumeroExpediente,
                                                            p_strNumeroIdentificacion,
                                                            p_strNombreUsuario,
                                                            p_strNumeroActoAdministrativo,
                                                            p_intTipoActoAdministrativo,
                                                            p_objFechaInicio,
                                                            p_objFechaFinal,
                                                            p_intAutoridadAmbiental);

            }


            /// <summary>
            /// Consultar la información registrada en bitacora sobre cambios de estados
            /// </summary>
            /// <param name="p_lngActoNotificacionId">long con el id de notificación</param>
            /// <returns>DataSet con la información de la bitacora</returns>
            public DataSet ConsultarBitacoraEstadosActoAdministrativo(long p_lngActoNotificacionId)
            {

                EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
                return dalc.ConsultarBitacoraEstadosActoAdministrativo(p_lngActoNotificacionId);
            }


            /// <summary>
            /// Consultar el reporte detallado de notificaciones
            /// </summary>
            /// <param name="p_lngActoNotificacionId">long con el id de notificación</param>
            /// <param name="p_intTipoNotificacion">int con el tipo de notificación</param>
            /// <returns>DataSet con la información detallada de notificaciones</returns>
            public DataSet ConsultarReporteDetalleNotificaciones(long p_lngActoNotificacionId, int p_intTipoNotificacion)
            {

                EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
                return dalc.ConsultarReporteDetalleNotificaciones(p_lngActoNotificacionId, p_intTipoNotificacion);
            }


            /// <summary>
            /// Retornar el listado de actos administrativos con notificación que cumpla con los parametros de buqueda especificados
            /// </summary>
            /// <param name="p_strNumeroVital">string con el numero VITAL. Opcional</param>
            /// <param name="p_strExpediente">string con el codigo del expediente. Opcional</param>
            /// <param name="p_strIdentificacionUsuario">string con el número de identificación del usuario. Opcional</param>
            /// <param name="p_strUsuario">string con el nombre del usuario. Opcional</param>
            /// <param name="p_strNumeroActo">string con el numero de acto. Opcional</param>
            /// <param name="p_intTipoActo">int con el tipo de acto administrativo. Opcional</param>
            /// <param name="p_intDiasVencimientoDesde">int con el valor del rango inicial de números de vencimiento. Opcional</param>
            /// <param name="p_intDiasVencimientoHasta">int con el valor del rango final de números de vencimiento. Opcional</param>
            /// <param name="p_intProvienePDI">int indicando si proviene de sistema de notificación. Opcional</param>
            /// <param name="p_strProcesoPDI">string con el identificador del proceso. Opcional</param>
            /// <param name="p_intFlujo">int con el identificador del flujo</param>
            /// <param name="p_intEstadoActual">int con el id del estado actual que se desea buscar. Opcional</param>
            /// <param name="p_blEsEstadoActual">bool que indica si solo se consulta estado actual o actos que hayan pasado por este estado</param>
            /// <param name="p_objFechaActoDesde">DateTime con la fecha inicial del rango</param>
            /// <param name="p_objFechaActoHasta">DateTime con la fecha final del rango</param>
            /// <param name="p_lngIDApplicationUser">long con el identificador del usuario que realiza la consulta</param>
            /// <returns>DataSet con la información de actos administrativos</returns>
            public DataSet ObtenerListadoActosAdministrativosNotificacion(string p_strNumeroVital, string p_strExpediente, string p_strIdentificacionUsuario, string p_strUsuario,
                                                                            string p_strNumeroActo, int p_intTipoActo, int? p_intDiasVencimientoDesde, int? p_intDiasVencimientoHasta,
                                                                            int p_intProvienePDI, string p_strProcesoPDI, int p_intFlujo, int p_intEstadoActual, bool p_blEsEstadoActual, DateTime p_objFechaActoDesde, DateTime p_objFechaActoHasta,
                                                                            long p_lngIDApplicationUser)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ObtenerListadoActosAdministrativosNotificacion(p_strNumeroVital, p_strExpediente, p_strIdentificacionUsuario, p_strUsuario,
                                                                            p_strNumeroActo, p_intTipoActo, p_intDiasVencimientoDesde, p_intDiasVencimientoHasta,
                                                                            p_intProvienePDI, p_strProcesoPDI, p_intFlujo, p_intEstadoActual, p_blEsEstadoActual, p_objFechaActoDesde, p_objFechaActoHasta,
                                                                            p_lngIDApplicationUser);
            }


            /// <summary>
            /// Obtener la información de la notificación en el estado actual para una persona
            /// </summary>
            /// <param name="p_lngActoID">long con el identificador del acto administrativo</param>
            /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
            /// <returns>DataSet con la información del estado</returns>
            public DataSet ObtenerEstadoActoPersona(long p_lngActoID, long p_lngPersonaID)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ObtenerEstadoActoPersona(p_lngActoID, p_lngPersonaID);
            }


            /// <summary>
            /// Obtener la informacion de los estados de notificación de un acto administrativo para una persona especifica
            /// </summary>
            /// <param name="p_lngActoID">long con el identificador del acto administrativo</param>
            /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
            /// <returns>DataSet con la información de los estados</returns>
            public DataSet ObtenerListadoEstadosActoPersona(long p_lngActoID, long p_lngPersonaID)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ObtenerListadoEstadosActoPersona(p_lngActoID, p_lngPersonaID);
            }


            /// <summary>
            /// Obtener el listado de tipos de adjuntos
            /// </summary>
            /// <returns>DataTable con la información del listado de adjuntos</returns>
            public DataTable ConsultarListadoTiposAdjuntosCorreo()
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ConsultarListadoTiposAdjuntosCorreo();
            }


            /// <summary>
            /// Obtener la información del estado indicado
            /// </summary>
            /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado</param>
            /// <returns>DataSet con la información del estado</returns>
            public DataSet ObtenerInformacionEstadoPersonaActo(long p_lngEstadoPersonaActoID)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ObtenerInformacionEstadoPersonaActo(p_lngEstadoPersonaActoID);
            }


            /// <summary>
            /// Crea un nuevo estado en el flujo de la notificación de una persona
            /// </summary>
            /// <param name="p_lngIdActo">long con el identificador del  acto administrativo</param>
            /// <param name="p_intFlujoID">int con el identificador del flujo al cual pertenece los estados</param>
            /// <param name="p_intIdEstado">int con el identificador del estado actual</param>
            /// <param name="p_intIdEstadoNuevo">int con el identificador del nuevo estado</param>
            /// <param name="p_intIdPersona">long con el identificador de la persona</param>
            /// <param name="p_objFechaEstadoNuevo">DateTime con la fecha del estado</param>
            /// <param name="p_strNumeroSilpa">string con el numero VITAL</param>
            /// <param name="p_strObservacion">string con la observación del cambio de estado</param>
            /// <param name="p_strRutaTemporalArchivos">string con la ruta temporal donde se colocan los archivos</param>
            /// <param name="p_strCodigoExpediente">string con el código del expediente</param>
            /// <param name="p_strNumeroActoAdministrativo">string con el número del acto administrativo</param>
            /// <param name="p_strNombreDocumentoAdicional">string con el nombre del documento adicional</param>
            /// <param name="p_objDocumentoAdicional">Arreglo de bytes con el documento adicional</param>
            /// <param name="p_blnEnviaDireccion">bool que indica si la información se envío a dirección fisica</param>
            /// <param name="p_strDepartamentoDireccion">string con el departamento al cual pertenece la dirección de envío</param>
            /// <param name="p_strMunicipioDireccion">string con el municipio al cual pertenece la dirección de envío</param>
            /// <param name="p_strDireccion">string con la dirección de envío</param>
            /// <param name="p_blnEnviaCorreo">bool que indica si se envía información por correo</param>
            /// <param name="p_strCorreo">string con la dirección a la cual se envío la información</param>
            /// <param name="p_strTextoCorreo">string con el texto de correo</param>
            /// <param name="p_blnAdjuntarActoAdministrativo">bool que indica si se anexa el acto administrativo</param>
            /// <param name="p_blnAdjuntarConceptosActoAdministrativo">bool que indica si se anexa los conceptos relacionados al acto administrativo</param>
            /// <param name="p_strNombreAdjunto">string con el nombre del archivo adjunto</param>
            /// <param name="p_objArchivoAdjunto">Arreglo de bytes con el adjunto</param>
            /// <param name="p_strReferenciaRecepcion">string con la referencia de recepción</param>
            /// <param name="p_objFechaRecepcion">DateTime con la fecha de recpción</param>
            /// <param name="p_intIdUsuario">int con el identificador del usuario que realiza el cambio de estado</param>
            /// <param name="p_blnEsModificable">bool que indica si el estado es modificable</param>
            /// <param name="p_intFirmaID">int con el identificador de la firma a aplicar</param>
            /// <param name="p_intTipoIdentficacionPersonaNotificar">int con el tipo de identificación persona notificada</param>
            /// <param name="p_strNumeroIdentificacionPersonaNotificar">string con el número de identificacion persona notificada</param>
            /// <param name="p_strNombrePersonaNotificar">string con el nombre de la persona notificada</param>
            /// <param name="p_strCalidadPersonaNotificar">string con la calidad de la persona notificada</param>
            /// <returns>string con error en caso de que se presente</returns>
            public void CrearEstadoPersonaActo(long p_lngIdActo, int p_intFlujoID, int p_intIdEstado, int p_intIdEstadoNuevo, long p_intIdPersona, int p_intAutoridadID, DateTime p_objFechaEstadoNuevo, string p_strNumeroSilpa, string p_strObservacion,
                                                string p_strRutaTemporalArchivos, string p_strCodigoExpediente, string p_strNumeroActoAdministrativo,
                                                string p_strNombreDocumentoAdicional, byte[] p_objDocumentoAdicional,
                                                bool p_blnEnviaDireccion, List<DireccionNotificacionEntity> p_lstDirecciones,
                                                bool p_blnEnviaCorreo, List<CorreoNotificacionEntity> p_lstCorreos, string p_strTextoCorreo, bool p_blnAdjuntarActoAdministrativo, bool p_blnAdjuntarConceptosActoAdministrativo, string p_strNombreAdjunto, byte[] p_objArchivoAdjunto,
                                                string p_strReferenciaRecepcion, DateTime p_objFechaRecepcion,
                                                int p_intIdUsuario, bool p_blnEsModificable, int p_intFirmaID,
                                                int p_intTipoIdentficacionPersonaNotificar, string p_strNumeroIdentificacionPersonaNotificar, string p_strNombrePersonaNotificar, string p_strCalidadPersonaNotificar)
            {

                string strCarpetaNotificacion = "CarpetaNotificacion";
                string strNombreArchivoRadicar = "";
                string strRutaArchivosNotificacion = "";
                byte[] objPlantilla = null;
                List<string> lstNombresArchivos = null;
                List<Byte[]> lstArchivos = null;
                EstadoFlujoNotificacion objEstadoFlujo = null;
                EstadoFlujoNotificacionEntity objEstado = null;
                TraficoDocumento objTraficoDocumentos = null;
                EstadoNotificacionDalc objEstadoNotificacion = null;
                NotificacionDalc objNotificacion = null;
                DataSet objInformacionActo = null;
                ICorreo.Correo objCorreo = null;
                long lngIdEstadoPersonaActo = 0;
                int intPlantillaCorreoID = 0;
                int intEstadoActualID = 0;
                DateTime? dteFechaEstadoActual = default(DateTime);
                string strEnlace = "";

                try
                {
                    //Verificar que los estados sean diferentes, se hace caso omiso de enviado desde presentación por posibles falla en demoras respuestas servidor
                    this.ObtenerEstadoActual(p_intIdPersona, p_lngIdActo, out intEstadoActualID, out dteFechaEstadoActual);
                    if (intEstadoActualID != p_intIdEstadoNuevo)
                    {
                        //Obtener configuración de estado nuevo
                        objEstadoFlujo = new EstadoFlujoNotificacion();
                        objEstado = objEstadoFlujo.ConsultarConfiguracionEstadoNotificacionElectronica(p_intFlujoID, p_intIdEstadoNuevo);

                        //Consultar información del acto administrativo
                        objInformacionActo = this.ConsultarInformacionActoNotificacion(p_lngIdActo, objEstado.PlantillaID, p_intIdPersona, p_intAutoridadID, p_intFlujoID, p_intIdEstadoNuevo, p_intIdUsuario, p_intFirmaID, p_blnAdjuntarConceptosActoAdministrativo);

                        //Cargar nombre de carpeta contenedora notificación
                        if (ConfigurationManager.AppSettings["CarpetaNotificacion"] != null)
                        {
                            strCarpetaNotificacion = ConfigurationManager.AppSettings["CarpetaNotificacion"].ToString();
                        }
                        strCarpetaNotificacion = strCarpetaNotificacion + @"\" + p_strNumeroSilpa + @"\";

                        //Cargar documento adicional
                        if (!string.IsNullOrEmpty(p_strNombreDocumentoAdicional) && p_objDocumentoAdicional != null)
                        {
                            //Cargar archivo adjunto correo notificacion
                            if (lstNombresArchivos == null)
                            {
                                lstNombresArchivos = new List<string>();
                                lstArchivos = new List<byte[]>();
                            }
                            lstNombresArchivos.Add(p_strNombreDocumentoAdicional);
                            lstArchivos.Add(p_objDocumentoAdicional);
                        }

                        //Cargar datos adjuntos correo
                        if (p_blnEnviaCorreo && objEstado.AnexaAdjunto && !string.IsNullOrEmpty(p_strNombreAdjunto))
                        {
                            //Cargar archivo adjunto correo notificacion
                            if (lstNombresArchivos == null)
                            {
                                lstNombresArchivos = new List<string>();
                                lstArchivos = new List<byte[]>();
                            }
                            lstNombresArchivos.Add(p_strNombreAdjunto);
                            lstArchivos.Add(p_objArchivoAdjunto);
                        }

                        //Generar pdf y adicionar a listado de archivos
                        if (objEstado.GeneraPlantilla)
                        {
                            //Adicionar plantilla
                            strNombreArchivoRadicar = "NOT_" + p_lngIdActo.ToString() + p_intIdEstadoNuevo.ToString() + p_intIdPersona.ToString() + "_" + p_strNumeroSilpa;
                            if (lstNombresArchivos == null)
                            {
                                lstNombresArchivos = new List<string>();
                                lstArchivos = new List<byte[]>();
                            }

                            //JNS 20190911 Se incorpora fecha de referencia
                            objPlantilla = this.GenerarPlantillaNotificacion(p_lngIdActo, p_intIdPersona, objInformacionActo, objEstado, p_objFechaEstadoNuevo, (objEstado.SolicitarReferenciaRecepcionNotificacion ? p_strReferenciaRecepcion : ""), (objEstado.SolicitarReferenciaRecepcionNotificacion ? p_objFechaRecepcion : default(DateTime)), p_strObservacion, p_intTipoIdentficacionPersonaNotificar, p_strNumeroIdentificacionPersonaNotificar, p_strNombrePersonaNotificar, p_strCalidadPersonaNotificar, p_lstDirecciones, p_lstCorreos, p_strRutaTemporalArchivos, ref strNombreArchivoRadicar);
                        }

                        //Almacenar documentos en carpeta de notificaciones
                        if ((lstArchivos != null && lstArchivos.Count > 0) || objEstado.GeneraPlantilla)
                        {
                            if (objEstado.GeneraPlantilla)
                            {
                                lstNombresArchivos.Add(strNombreArchivoRadicar + ".pdf");
                                lstArchivos.Add(objPlantilla);
                            }


                            objTraficoDocumentos = new TraficoDocumento();
                            objTraficoDocumentos.RecibirDocumento(strCarpetaNotificacion, p_intIdPersona.ToString(), lstArchivos, ref lstNombresArchivos, ref strRutaArchivosNotificacion);

                            //Cargar nombre adicional
                            if (!string.IsNullOrEmpty(p_strNombreDocumentoAdicional) && p_objDocumentoAdicional != null)
                            {
                                p_strNombreDocumentoAdicional = lstNombresArchivos[0];
                            }

                            //Cargar nombre adjunto
                            if (!string.IsNullOrEmpty(p_strNombreAdjunto))
                            {
                                if (!string.IsNullOrEmpty(p_strNombreDocumentoAdicional) && p_objDocumentoAdicional != null)
                                {
                                    p_strNombreAdjunto = lstNombresArchivos[1];
                                }
                                else
                                {
                                    p_strNombreAdjunto = lstNombresArchivos[0];
                                }
                            }

                            //Cargar plantilla
                            if (!string.IsNullOrEmpty(strNombreArchivoRadicar))
                            {
                                if ((string.IsNullOrEmpty(p_strNombreDocumentoAdicional) || p_objDocumentoAdicional == null) && string.IsNullOrEmpty(p_strNombreAdjunto))
                                {
                                    strNombreArchivoRadicar = lstNombresArchivos[0];
                                }
                                else if (((!string.IsNullOrEmpty(p_strNombreDocumentoAdicional) && p_objDocumentoAdicional != null) && string.IsNullOrEmpty(p_strNombreAdjunto)) || ((string.IsNullOrEmpty(p_strNombreDocumentoAdicional) || p_objDocumentoAdicional != null) && !string.IsNullOrEmpty(p_strNombreAdjunto)))
                                {
                                    strNombreArchivoRadicar = lstNombresArchivos[1];
                                }
                                if ((!string.IsNullOrEmpty(p_strNombreDocumentoAdicional) && p_objDocumentoAdicional != null) && !string.IsNullOrEmpty(p_strNombreAdjunto))
                                {
                                    strNombreArchivoRadicar = lstNombresArchivos[2];
                                }
                            }
                        }

                        //Registrar estado en la base de datos
                        objEstadoNotificacion = new EstadoNotificacionDalc();
                        lngIdEstadoPersonaActo = objEstadoNotificacion.CrearEstadoPersonaActo(p_lngIdActo, p_intFlujoID, p_intIdEstadoNuevo, p_intIdPersona, p_objFechaEstadoNuevo, p_strObservacion, p_strNombreDocumentoAdicional,
                                                                                                p_blnEnviaDireccion, p_blnEnviaCorreo, (p_blnEnviaCorreo ? p_strNombreAdjunto : ""), (p_blnEnviaCorreo ? p_blnAdjuntarActoAdministrativo : false), (p_blnEnviaCorreo ? p_blnAdjuntarConceptosActoAdministrativo : false),
                                                                                                "", (objEstado.GeneraPlantilla ? strNombreArchivoRadicar : ""),
                                                                                                (objEstado.SolicitarReferenciaRecepcionNotificacion ? p_strReferenciaRecepcion : ""), (objEstado.SolicitarReferenciaRecepcionNotificacion ? p_objFechaRecepcion : default(DateTime)),
                                                                                                p_intIdUsuario, p_blnEsModificable, p_intTipoIdentficacionPersonaNotificar, p_strNumeroIdentificacionPersonaNotificar, p_strNombrePersonaNotificar, p_strCalidadPersonaNotificar);

                        if (lngIdEstadoPersonaActo > 0)
                        {
                            //Envía direcciones
                            if (p_blnEnviaDireccion && p_lstDirecciones != null)
                            {
                                //Insertar las direcciones a las cuales se envío
                                foreach (DireccionNotificacionEntity objDireccion in p_lstDirecciones)
                                {
                                    objDireccion.EstadoPersonaActoID = lngIdEstadoPersonaActo;
                                    objEstadoNotificacion.CrearDireccionEstadoPersonaActo(objDireccion);
                                }
                            }

                            //Crear correos
                            if (p_blnEnviaCorreo && p_lstCorreos != null)
                            {
                                //Insertar los correos a las cuales se envío
                                foreach (CorreoNotificacionEntity objCorreoNotificar in p_lstCorreos)
                                {
                                    objCorreoNotificar.EstadoPersonaActoID = lngIdEstadoPersonaActo;
                                    objEstadoNotificacion.CrearCorreoEstadoPersonaActo(objCorreoNotificar, p_strTextoCorreo);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("No se obtuvo identificador del estado");
                        }

                        //Verifica si se debe enviar correo
                        if (p_blnEnviaCorreo && p_lstCorreos != null)
                        {
                            //Crear objeto envío de correo
                            objCorreo = new ICorreo.Correo();

							//No se envía el documento adicional por correo
                            if (lstNombresArchivos != null && lstNombresArchivos.Count > 0 && (!string.IsNullOrEmpty(p_strNombreDocumentoAdicional) && p_objDocumentoAdicional != null))
                            {
                                lstNombresArchivos.RemoveAt(0);
                            }

                            //Verificar si se anexa el acto administrativo
                            if (p_blnAdjuntarActoAdministrativo)
                            {
                                if (objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"] != System.DBNull.Value && !string.IsNullOrEmpty(objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString()) && !objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().EndsWith("/") && !objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().EndsWith("\\"))
                                {
                                    if (lstNombresArchivos == null)
                                        lstNombresArchivos = new List<string>();
                                    lstNombresArchivos.Add(objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString());
                                }
                            }

                            //Si adjuntos es enlace cargar enlace
                            if (objEstado.TipoAnexoCorreoID == (int)NOTTipoAnexo.Enlace || p_blnAdjuntarConceptosActoAdministrativo)
                            {
                                strEnlace = this.GenerarEnlaceCorreoNotificacion(p_lngIdActo, p_intIdPersona, lngIdEstadoPersonaActo, p_blnAdjuntarActoAdministrativo, p_blnAdjuntarConceptosActoAdministrativo);
                            }

                            //Cargar la plantilla de correo que se debe utilizar
                            objNotificacion = new NotificacionDalc();
                            intPlantillaCorreoID = objNotificacion.ConsultarPlantillaCorreoAutoridadPersona(p_intAutoridadID, p_intIdPersona);
                            if (intPlantillaCorreoID <= 0)
                            {
                                throw new Exception("No se eoncontro información de plantilla de correo. p_intAutoridadID: " + p_intAutoridadID.ToString() + " - p_intIdPersona: " + p_intIdPersona.ToString());
                            }

                            //Ciclo que envía los correos
                            int intCont = 1;
                            foreach (CorreoNotificacionEntity objCorreoNotificar in p_lstCorreos)
                            {
                                objCorreo.EnviarCorreoNotificacionPersona("", objCorreoNotificar.Correo, objInformacionActo.Tables["USUARIO"].Rows[0]["NOMBRE"].ToString(), objInformacionActo.Tables["USUARIO"].Rows[0]["TIPO_IDENTIFICACION"].ToString(), objInformacionActo.Tables["USUARIO"].Rows[0]["NUMERO_IDENTIFICACION"].ToString(),
                                                                          p_strTextoCorreo, lstNombresArchivos, p_strCodigoExpediente, DateTime.Now.ToString("dd/MM/yyyy hh:MM:ss"), p_strNumeroSilpa, p_strNumeroActoAdministrativo, strEnlace, intPlantillaCorreoID);

                                intCont++;
                            }

                        }

                    }
                    else
                    {
                        throw new Exception("El nuevo estado y el estado actual son iguales.");
                    }
                }
                catch (NotificacionException exc)
                {
                    throw exc;
                }
                catch(Exception exc){

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Notificacion :: CrearEstadoPersonaActo -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    throw new NotificacionException("Notificacion :: CrearEstadoPersonaActo -> Error registrando avance: " + exc.Message, exc);
                }
            }


            /// <summary>
            /// Editar un estado del flujo de la notificación de una persona
            /// </summary>
            /// <param name="p_EstadoPersonaActoId">long con el identificador del estado de la persona a modificar</param>
            /// <param name="p_lngIdActo">long con el identificador del  acto administrativo</param>
            /// <param name="p_intFlujoID">int con el identificador del flujo al cual pertenece los estados</param>
            /// <param name="p_intIdEstado">int con el identificador del estado</param>
            /// <param name="p_intIdPersona">long con el identificador de la persona</param>
            /// <param name="p_objFechaEstado">DateTime con la fecha del estado</param>
            /// <param name="p_strNumeroSilpa">string con el numero VITAL</param>
            /// <param name="p_strObservacion">string con la observación del cambio de estado</param>
            /// <param name="p_strRutaTemporalArchivos">string con la ruta temporal donde se colocan los archivos</param>
            /// <param name="p_strNombreFuncionario">string con el nombre del funcionario</param>
            /// <param name="p_strCodigoExpediente">string con el código del expediente</param>
            /// <param name="p_strNumeroActoAdministrativo">string con el número del acto administrativo</param>
            /// <param name="p_strNombreDocumentoAdicional">string con el nombre del documento adicional</param>
            /// <param name="p_objDocumentoAdicional">Arreglo de bytes con el documento adicional</param>
            /// <param name="p_blnEnviaDireccion">bool que indica si la información se envío a dirección fisica</param>
            /// <param name="p_strDepartamentoDireccion">string con el departamento al cual pertenece la dirección de envío</param>
            /// <param name="p_strMunicipioDireccion">string con el municipio al cual pertenece la dirección de envío</param>
            /// <param name="p_strDireccion">string con la dirección de envío</param>
            /// <param name="p_blnEnviaCorreo">bool que indica si se envía información por correo</param>
            /// <param name="p_strCorreo">string con la dirección a la cual se envío la información</param>
            /// <param name="p_strTextoCorreo">string con el texto de correo</param>
            /// <param name="p_strNombreAdjunto">string con el nombre del archivo adjunto</param>
            /// <param name="p_objArchivoAdjunto">Arreglo de bytes con el adjunto</param>
            /// <param name="p_strReferenciaRecepcion">string con la referencia de recepción</param>
            /// <param name="p_objFechaRecepcion">DateTime con la fecha de recpción</param>
            /// <param name="p_intIdUsuario">int con el identificador del usuario que realiza el cambio de estado</param>
            /// <param name="p_intFirmaID">int con el identificador de la firma que se debe aplicar</param>
            /// <param name="p_intTipoIdentficacionPersonaNotificar">int con el tipo de identificación persona notificada</param>
            /// <param name="p_strNumeroIdentificacionPersonaNotificar">string con el número de identificacion persona notificada</param>
            /// <param name="p_strNombrePersonaNotificar">string con el nombre de la persona notificada</param>
            /// <param name="p_strCalidadPersonaNotificar">string con la calidad de la persona notificada</param>
            /// <returns>string con error en caso de que se presente</returns>
            public void EditarEstadoPersonaActo(long p_EstadoPersonaActoId, long p_lngIdActo, int p_intFlujoID, int p_intIdEstado, long p_intIdPersona, int p_intAutoridadID, DateTime p_objFechaEstado, string p_strNumeroSilpa, string p_strObservacion,
                                                string p_strRutaTemporalArchivos, string p_strNombreFuncionario, string p_strCodigoExpediente, string p_strNumeroActoAdministrativo,
                                                string p_strNombreDocumentoAdicional, byte[] p_objDocumentoAdicional,
                                                bool p_blnEnviaDireccion, List<DireccionNotificacionEntity> p_lstDirecciones,
                                                bool p_blnEnviaCorreo, List<CorreoNotificacionEntity> p_lstCorreos, string p_strTextoCorreo, bool p_blnAdjuntarActoAdministrativo, bool p_blnAdjuntarConceptosActoAdministrativo, string p_strNombreAdjunto, byte[] p_objArchivoAdjunto,
                                                string p_strReferenciaRecepcion, DateTime p_objFechaRecepcion,
                                                int p_intIdUsuario, int p_intFirmaID,
                                                int p_intTipoIdentficacionPersonaNotificar, string p_strNumeroIdentificacionPersonaNotificar, string p_strNombrePersonaNotificar, string p_strCalidadPersonaNotificar)
            {

                string strCarpetaNotificacion = "CarpetaNotificacion";
                string strNombreArchivoRadicar = "";
                string strRutaArchivosNotificacion = "";
                byte[] objPlantilla = null;
                List<string> lstNombresArchivos = null;
                List<Byte[]> lstArchivos = null;
                EstadoFlujoNotificacion objEstadoFlujo = null;
                EstadoFlujoNotificacionEntity objEstado = null;
                TraficoDocumento objTraficoDocumentos = null;
                EstadoNotificacionDalc objEstadoNotificacion = null;
                NotificacionDalc objNotificacion = null;
                PersonaNotificarDalc objPersonaNotificar = null;
                DataSet objInformacionActo = null;
                ICorreo.Correo objCorreo = null;
                int intPlantillaCorreoID = 0;
                string strEnlace = "";
			    int intCobroID = 0;

                try
                {
                
                    //Obtener configuración de estado
                    objEstadoFlujo = new EstadoFlujoNotificacion();
                    objEstado = objEstadoFlujo.ConsultarConfiguracionEstadoNotificacionElectronica(p_intFlujoID, p_intIdEstado);

                    //Cargar nombre de carpeta contenedora notificación
                    if (ConfigurationManager.AppSettings["CarpetaNotificacion"] != null)
                    {
                        strCarpetaNotificacion = ConfigurationManager.AppSettings["CarpetaNotificacion"].ToString();
                    }
                    strCarpetaNotificacion = strCarpetaNotificacion + @"\" + p_strNumeroSilpa + @"\";

                    //Cargar documento adicional
                    if (!string.IsNullOrEmpty(p_strNombreDocumentoAdicional))
                    {
                        //Cargar archivo adjunto correo notificacion
                        if (lstNombresArchivos == null)
                        {
                            lstNombresArchivos = new List<string>();
                            lstArchivos = new List<byte[]>();
                        }
                        lstNombresArchivos.Add(p_strNombreDocumentoAdicional);
                        lstArchivos.Add(p_objDocumentoAdicional);
                    }

                    //Cargar datos adjuntos correo
                    if (p_blnEnviaCorreo && objEstado.AnexaAdjunto && !string.IsNullOrEmpty(p_strNombreAdjunto))
                    {
                        //Cargar archivo adjunto correo notificacion
                        if (lstNombresArchivos == null)
                        {
                            lstNombresArchivos = new List<string>();
                            lstArchivos = new List<byte[]>();
                        }
                        lstNombresArchivos.Add(p_strNombreAdjunto);
                        lstArchivos.Add(p_objArchivoAdjunto);
                    }

                    //Consultar información del acto administrativo
                    objInformacionActo = this.ConsultarInformacionActoNotificacion(p_lngIdActo, objEstado.PlantillaID, p_intIdPersona, p_intAutoridadID, p_intFlujoID, p_intIdEstado, p_intIdUsuario, p_intFirmaID, p_blnAdjuntarConceptosActoAdministrativo);

                    //Generar pdf y adicionar a listado de archivos
                    if (objEstado.GeneraPlantilla)
                    {
                        //Adicionar plantilla
                        strNombreArchivoRadicar = "NOT_" + p_lngIdActo.ToString() + p_intIdEstado.ToString() + p_intIdPersona.ToString() + "_" + p_strNumeroSilpa;
                        if (lstNombresArchivos == null)
                        {
                            lstNombresArchivos = new List<string>();
                            lstArchivos = new List<byte[]>();
                        }

                        //JNS 20190911 Se incorpora fecha de referencia
                        objPlantilla = this.GenerarPlantillaNotificacion(p_lngIdActo, p_intIdPersona, objInformacionActo, objEstado, p_objFechaEstado, (objEstado.SolicitarReferenciaRecepcionNotificacion ? p_strReferenciaRecepcion : ""), (objEstado.SolicitarReferenciaRecepcionNotificacion ? p_objFechaRecepcion : default(DateTime)), p_strObservacion, p_intTipoIdentficacionPersonaNotificar, p_strNumeroIdentificacionPersonaNotificar, p_strNombrePersonaNotificar, p_strCalidadPersonaNotificar, p_lstDirecciones, p_lstCorreos, p_strRutaTemporalArchivos, ref strNombreArchivoRadicar);
                    }

                    //Almacenar documentos en carpeta de notificaciones
                    if ((lstArchivos != null && lstArchivos.Count > 0) || objEstado.GeneraPlantilla)
                    {
                        
                        if (objEstado.GeneraPlantilla)
                        {
                            lstNombresArchivos.Add(strNombreArchivoRadicar + ".pdf");
                            lstArchivos.Add(objPlantilla);
                        }

                        objTraficoDocumentos = new TraficoDocumento();
                        objTraficoDocumentos.RecibirDocumento(strCarpetaNotificacion, p_intIdPersona.ToString(), lstArchivos, ref lstNombresArchivos, ref strRutaArchivosNotificacion);

                        //Cargar nombre adicional
                        if (!string.IsNullOrEmpty(p_strNombreDocumentoAdicional))
                        {
                            p_strNombreDocumentoAdicional = lstNombresArchivos[0];
                        }

                        //Cargar nombre adjunto
                        if (!string.IsNullOrEmpty(p_strNombreAdjunto))
                        {
                            if (!string.IsNullOrEmpty(p_strNombreDocumentoAdicional))
                            {
                                p_strNombreAdjunto = lstNombresArchivos[1];
                            }
                            else
                            {
                                p_strNombreAdjunto = lstNombresArchivos[0];
                            }
                        }

                        //Cargar plantilla
                        if (!string.IsNullOrEmpty(strNombreArchivoRadicar))
                        {
                            if (string.IsNullOrEmpty(p_strNombreDocumentoAdicional) && string.IsNullOrEmpty(p_strNombreAdjunto))
                            {
                                strNombreArchivoRadicar = lstNombresArchivos[0];
                            }
                            else if ((!string.IsNullOrEmpty(p_strNombreDocumentoAdicional) && string.IsNullOrEmpty(p_strNombreAdjunto)) || (string.IsNullOrEmpty(p_strNombreDocumentoAdicional) && !string.IsNullOrEmpty(p_strNombreAdjunto)))
                            {
                                strNombreArchivoRadicar = lstNombresArchivos[1];
                            }
                            if (!string.IsNullOrEmpty(p_strNombreDocumentoAdicional) && !string.IsNullOrEmpty(p_strNombreAdjunto))
                            {
                                strNombreArchivoRadicar = lstNombresArchivos[2];
                            }
                        }
                    }

                    //Registrar estado en la base de datos
                    objEstadoNotificacion = new EstadoNotificacionDalc();
                    objEstadoNotificacion.EditarEstadoPersonaActo(p_EstadoPersonaActoId, p_objFechaEstado, p_strObservacion, p_strNombreDocumentoAdicional,
                                                                    p_blnEnviaDireccion, p_blnEnviaCorreo, (p_blnEnviaCorreo ? p_strNombreAdjunto : ""), (p_blnEnviaCorreo ? p_blnAdjuntarActoAdministrativo : false), (p_blnEnviaCorreo ? p_blnAdjuntarConceptosActoAdministrativo : false),
                                                                    "", (objEstado.GeneraPlantilla ? strNombreArchivoRadicar : ""),
                                                                    (objEstado.SolicitarReferenciaRecepcionNotificacion ? p_strReferenciaRecepcion : ""), (objEstado.SolicitarReferenciaRecepcionNotificacion ? p_objFechaRecepcion : default(DateTime)),
                                                                    p_intIdUsuario, p_intTipoIdentficacionPersonaNotificar, p_strNumeroIdentificacionPersonaNotificar, p_strNombrePersonaNotificar, p_strCalidadPersonaNotificar);


                    //Eliminar direcciones
                    objEstadoNotificacion.EliminarDireccionesEstadoPersonaActo(p_EstadoPersonaActoId);

                    //Envía direcciones
                    if (p_blnEnviaDireccion && p_lstDirecciones != null)
                    {
                        //Insertar las direcciones a las cuales se envío
                        foreach (DireccionNotificacionEntity objDireccion in p_lstDirecciones)
                        {
                            objDireccion.EstadoPersonaActoID = p_EstadoPersonaActoId;
                            objEstadoNotificacion.CrearDireccionEstadoPersonaActo(objDireccion);
                        }
                    }

                    //Eliminar correos
                    objEstadoNotificacion.EliminarCorreosEstadoPersonaActo(p_EstadoPersonaActoId);

                    //Envía correos
                    if (p_blnEnviaCorreo && p_lstCorreos != null)
                    {
                        //Insertar los correos a las cuales se envío
                        foreach (CorreoNotificacionEntity objCorreoNotificar in p_lstCorreos)
                        {
                            objCorreoNotificar.EstadoPersonaActoID = p_EstadoPersonaActoId;
                            objEstadoNotificacion.CrearCorreoEstadoPersonaActo(objCorreoNotificar, p_strTextoCorreo);
                        }
                    }

                    //Enviar correos
                    if (p_blnEnviaCorreo && p_lstCorreos != null)
                    {
                        //Crear objeto envío de correo
                        objCorreo = new ICorreo.Correo();

						//No se envía el documento adicional por correo
                        if (lstNombresArchivos != null && lstNombresArchivos.Count > 0 && (!string.IsNullOrEmpty(p_strNombreDocumentoAdicional) && p_objDocumentoAdicional != null))
                        {
                            lstNombresArchivos.RemoveAt(0);
                        }

                        //Verificar si se anexa el acto administrativo
                        if (p_blnAdjuntarActoAdministrativo)
                        {
                            if (objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"] != System.DBNull.Value && !string.IsNullOrEmpty(objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString()) && !objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().EndsWith("/") && !objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().EndsWith("\\"))
                                lstNombresArchivos.Add(objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString());
                        }

                        if (objEstado.TipoAnexoCorreoID == (int)NOTTipoAnexo.Enlace || p_blnAdjuntarConceptosActoAdministrativo)
                        {
                            strEnlace = this.GenerarEnlaceCorreoNotificacion(p_lngIdActo, p_intIdPersona, p_EstadoPersonaActoId, p_blnAdjuntarActoAdministrativo, p_blnAdjuntarConceptosActoAdministrativo);
                        }

                        //Cargar la plantilla de correo que se debe utilizar
                        objNotificacion = new NotificacionDalc();
                        intPlantillaCorreoID = objNotificacion.ConsultarPlantillaCorreoAutoridadPersona(p_intAutoridadID, p_intIdPersona);
                        if (intPlantillaCorreoID <= 0)
                        {
                            throw new Exception("No se eoncontro información de plantilla de correo. p_intAutoridadID: " + p_intAutoridadID.ToString() + " - p_intIdPersona: " + p_intIdPersona.ToString());
                        }

                        //Ciclo que envía los correos
                        int intCont = 1;
                        foreach (CorreoNotificacionEntity objCorreoNotificar in p_lstCorreos)
                        {
                            objCorreo.EnviarCorreoNotificacionPersona("", objCorreoNotificar.Correo, objInformacionActo.Tables["USUARIO"].Rows[0]["NOMBRE"].ToString(), objInformacionActo.Tables["USUARIO"].Rows[0]["TIPO_IDENTIFICACION"].ToString(), objInformacionActo.Tables["USUARIO"].Rows[0]["NUMERO_IDENTIFICACION"].ToString(),
                                                                        p_strTextoCorreo, lstNombresArchivos, p_strCodigoExpediente, DateTime.Now.ToString("dd/MM/yyyy hh:MM:ss"), p_strNumeroSilpa, p_strNumeroActoAdministrativo, strEnlace, intPlantillaCorreoID);

                            intCont++;
                        }
                    }
                }
                catch (NotificacionException exc)
                {
                    throw exc;
                }
                catch (Exception exc)
                {

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Notificacion :: EditarEstadoPersonaActo -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    throw new NotificacionException("Notificacion :: EditarEstadoPersonaActo -> Error registrando avance: " + exc.Message, exc);
                }
            }


            /// <summary>
            /// Realizar avance de estado para notificación electrónica
            /// </summary>
            /// <param name="p_lngIdActo">long con el identificador del  acto administrativo</param>
            /// <param name="p_intFlujoID">int con el identificador del flujo al cual pertenece los estados</param>
            /// <param name="p_intIdEstadoNuevo">int con el identificador del nuevo estado</param>
            /// <param name="p_intIdPersona">long con el identificador de la persona</param>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_strNumeroSilpa">string con el numero VITAL</param>
            /// <param name="p_strObservacion">string con la observación del cambio de estado</param>
            /// <param name="p_strRutaTemporalArchivos">string con la ruta temporal donde se colocan los archivos</param>
            /// <param name="p_strCodigoExpediente">string con el código del expediente</param>
            /// <param name="p_strNumeroActoAdministrativo">string con el número del acto administrativo</param>
            /// <param name="p_blnEnviaCorreo">bool que indica si se envía información por correo</param>
            /// <param name="p_strCorreo">string con la dirección a la cual se envío la información</param>
            /// <param name="p_strTextoCorreo">string con el texto de correo</param>
            /// <param name="p_blnAdjuntarActoAdministrativo">bool que indica si se anexa el acto administrativo</param>
            /// <param name="p_blnAdjuntarConceptosActoAdministrativo">bool que indica si se anexa los conceptos relacionados al acto administrativo</param>
            /// <param name="p_intIdUsuario">int con el identificador del usuario que realiza el cambio de estado</param>
            /// <param name="p_blnEsModificable">bool que indica si el estado es modificable</param>
            /// <param name="p_intFirmaID">int con el identificador de la firma a aplicar</param>
            /// <param name="p_intTipoIdentficacionPersonaNotificar">int con el tipo de identificación persona notificada</param>
            /// <param name="p_strNumeroIdentificacionPersonaNotificar">string con el número de identificacion persona notificada</param>
            /// <param name="p_strNombrePersonaNotificar">string con el nombre de la persona notificada</param>
            /// <param name="p_strCalidadPersonaNotificar">string con la calidad de la persona notificada</param>
            /// <param name="p_strRutaArchivosAdjuntos">string con la ruta de archivos adjuntos</param>
            /// <param name="p_strRutaArchivosAdicionales">string con la ruta donde se ubican los archivos adicionales o el archivo adicional referenciado</param>
            /// <param name="p_strReferencia">string con la referecia a relacionr al estado</param>
            /// <param name="p_objFechaReferencia">DateTime con la fecha en la cual se genero la referencia</param>
            /// <returns>DateTime con fecha de avance de la notificación</returns>
            public DateTime AvanzarEstadoNotificacionElectronica(long p_lngIdActo, int p_intFlujoID, int p_intIdEstadoNuevo, long p_intIdPersona, int p_intAutoridadID, string p_strNumeroSilpa, string p_strObservacion,
                                                                 string p_strRutaTemporalArchivos, string p_strCodigoExpediente, string p_strNumeroActoAdministrativo,
                                                                 bool p_blnEnviaCorreo, List<CorreoNotificacionEntity> p_lstCorreos, string p_strTextoCorreo, bool p_blnAdjuntarActoAdministrativo, bool p_blnAdjuntarConceptosActoAdministrativo,
                                                                 int p_intIdUsuario, int p_intFirmaID,
                                                                 int p_intTipoIdentficacionPersonaNotificar, string p_strNumeroIdentificacionPersonaNotificar, string p_strNombrePersonaNotificar, string p_strCalidadPersonaNotificar, string p_strRutaArchivosAdicionales, string p_strReferencia, DateTime p_objFechaReferencia)
            {

                string strCarpetaNotificacion = "CarpetaNotificacion";
                string strNombreArchivoRadicar = "";
                string strRutaArchivosNotificacion = "";
                byte[] objPlantilla = null;
                List<string> lstNombresArchivos = null;
                List<Byte[]> lstArchivos = null;
                EstadoFlujoNotificacion objEstadoFlujo = null;
                EstadoFlujoNotificacionEntity objEstado = null;
                TraficoDocumento objTraficoDocumentos = null;
                NotificacionDalc objNotificacion = null;
                EstadoNotificacionDalc objEstadoNotificacion = null;
                DataSet objInformacionActo = null;
                long lngIdEstadoPersonaActo = 0;
                int intEstadoActualID = 0;
                DateTime? dteFechaEstadoActual = default(DateTime);
                DateTime objFechaEstadoNuevo = default(DateTime);
                EstampaTiempoServicio objEstampaTiempoServicio = null;
                ICorreo.Correo objCorreo = null;
                string strEnlace = "";
                int intPlantillaCorreoID = 0;
                int intCobroID = 0;

                try
                {
                    //Verificar que los estados sean diferentes, se hace caso omiso de enviado desde presentación por posibles falla en demoras respuestas servidor
                    this.ObtenerEstadoActual(p_intIdPersona, p_lngIdActo, out intEstadoActualID, out dteFechaEstadoActual);
                    if (intEstadoActualID != p_intIdEstadoNuevo)
                    {
                        //Obtener configuración de estado nuevo
                        objEstadoFlujo = new EstadoFlujoNotificacion();
                        objEstado = objEstadoFlujo.ConsultarConfiguracionEstadoNotificacionElectronica(p_intFlujoID, p_intIdEstadoNuevo);

                        //Consultar información del acto administrativo
                        objInformacionActo = this.ConsultarInformacionActoNotificacion(p_lngIdActo, objEstado.PlantillaID, p_intIdPersona, p_intAutoridadID, p_intFlujoID, p_intIdEstadoNuevo, p_intIdUsuario, p_intFirmaID, p_blnAdjuntarConceptosActoAdministrativo);

                        //Cargar nombre de carpeta contenedora notificación
                        if (ConfigurationManager.AppSettings["CarpetaNotificacion"] != null)
                        {
                            strCarpetaNotificacion = ConfigurationManager.AppSettings["CarpetaNotificacion"].ToString();
                        }
                        strCarpetaNotificacion = strCarpetaNotificacion + @"\" + p_strNumeroSilpa + @"\";

                        //Obtener fecha de nuevo estado
                        objEstampaTiempoServicio = new EstampaTiempoServicio();
                        objFechaEstadoNuevo = objEstampaTiempoServicio.ObtenerFecha();

                        //Generar pdf y adicionar a listado de archivos
                        if (objEstado.GeneraPlantilla)
                        {
                            //Adicionar plantilla
                            strNombreArchivoRadicar = "NOT_" + p_lngIdActo.ToString() + p_intIdEstadoNuevo.ToString() + p_intIdPersona.ToString() + "_" + p_strNumeroSilpa;
                            if (lstNombresArchivos == null)
                            {
                                lstNombresArchivos = new List<string>();
                                lstArchivos = new List<byte[]>();
                            }

                            //JNS 20190911 Se incorpora fecha de referencia
                            objPlantilla = this.GenerarPlantillaNotificacion(p_lngIdActo, p_intIdPersona, objInformacionActo, objEstado, objFechaEstadoNuevo, "", default(DateTime), p_strObservacion, p_intTipoIdentficacionPersonaNotificar, p_strNumeroIdentificacionPersonaNotificar, p_strNombrePersonaNotificar, p_strCalidadPersonaNotificar, null, p_lstCorreos, p_strRutaTemporalArchivos, ref strNombreArchivoRadicar);
                        }

                        //Almacenar documentos en carpeta de notificaciones
                        if ((lstArchivos != null && lstArchivos.Count > 0) || objEstado.GeneraPlantilla)
                        {                            
                            if (objEstado.GeneraPlantilla)
                            {
                                lstNombresArchivos.Add(strNombreArchivoRadicar + ".pdf");
                                lstArchivos.Add(objPlantilla);
                            }

                            //Enviar documento a carpeta vital
                            objTraficoDocumentos = new TraficoDocumento();
                            objTraficoDocumentos.RecibirDocumento(strCarpetaNotificacion, p_intIdPersona.ToString(), lstArchivos, ref lstNombresArchivos, ref strRutaArchivosNotificacion);

                            //Cargar plantilla
                            if (!string.IsNullOrEmpty(strNombreArchivoRadicar))
                            {
                                strNombreArchivoRadicar = lstNombresArchivos[0];
                            }
                        }

                        //Registrar estado en la base de datos
                        objEstadoNotificacion = new EstadoNotificacionDalc();
                        lngIdEstadoPersonaActo = objEstadoNotificacion.CrearEstadoPersonaActo(p_lngIdActo, p_intFlujoID, p_intIdEstadoNuevo, p_intIdPersona, objFechaEstadoNuevo, p_strObservacion, p_strRutaArchivosAdicionales,
                                                                                                false, p_blnEnviaCorreo, "", (p_blnEnviaCorreo ? p_blnAdjuntarActoAdministrativo : false), (p_blnEnviaCorreo ? p_blnAdjuntarConceptosActoAdministrativo : false),
                                                                                                "", (objEstado.GeneraPlantilla ? strNombreArchivoRadicar : ""),
                                                                                                p_strReferencia, p_objFechaReferencia,
                                                                                                p_intIdUsuario, false, p_intTipoIdentficacionPersonaNotificar, p_strNumeroIdentificacionPersonaNotificar, p_strNombrePersonaNotificar, p_strCalidadPersonaNotificar);

                        //Verificar que se haya realizado el avance
                        if (lngIdEstadoPersonaActo > 0)
                        {
                            //Verificar si estado envia correo
                            if (p_blnEnviaCorreo)
                            {
                                //Verificar que se tenga listado de correos
                                if (p_lstCorreos != null && p_lstCorreos.Count > 0)
                                {
                                    //Crear objeto envío de correo
                                    objCorreo = new ICorreo.Correo();

                                    //Verificar si se anexa el acto administrativo
                                    if (p_blnAdjuntarActoAdministrativo)
                                    {
                                        if (objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"] != System.DBNull.Value && !string.IsNullOrEmpty(objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString()) && !objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().EndsWith("/") && !objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().EndsWith("\\"))
                                        {
                                            if (lstNombresArchivos == null)
                                                lstNombresArchivos = new List<string>();
                                            lstNombresArchivos.Add(objInformacionActo.Tables["ACTO"].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString());
                                        }
                                    }

                                    //Cargar la plantilla de correo que se debe utilizar
                                    objNotificacion = new NotificacionDalc();
                                    intPlantillaCorreoID = objNotificacion.ConsultarPlantillaCorreoAutoridadPersona(p_intAutoridadID, p_intIdPersona);
                                    if (intPlantillaCorreoID <= 0)
                                    {
                                        throw new Exception("No se eoncontro información de plantilla de correo. p_intAutoridadID: " + p_intAutoridadID.ToString() + " - p_intIdPersona: " + p_intIdPersona.ToString());
                                    }

                                    //Si adjuntos es enlace cargar enlace
                                    if (objEstado.TipoAnexoCorreoID == (int)NOTTipoAnexo.Enlace || p_blnAdjuntarConceptosActoAdministrativo)
                                    {
                                        strEnlace = this.GenerarEnlaceCorreoNotificacion(p_lngIdActo, p_intIdPersona, lngIdEstadoPersonaActo, p_blnAdjuntarActoAdministrativo, p_blnAdjuntarConceptosActoAdministrativo);
                                    }

                                    //Ciclo que envía los correos
                                    int intCont = 1;
                                    foreach (CorreoNotificacionEntity objCorreoNotificar in p_lstCorreos)
                                    {
                                        objCorreo.EnviarCorreoNotificacionPersona("", objCorreoNotificar.Correo, objInformacionActo.Tables["USUARIO"].Rows[0]["NOMBRE"].ToString(), objInformacionActo.Tables["USUARIO"].Rows[0]["TIPO_IDENTIFICACION"].ToString(), objInformacionActo.Tables["USUARIO"].Rows[0]["NUMERO_IDENTIFICACION"].ToString(),
                                                                                  p_strTextoCorreo, lstNombresArchivos, p_strCodigoExpediente, DateTime.Now.ToString("dd/MM/yyyy hh:MM:ss"), p_strNumeroSilpa, p_strNumeroActoAdministrativo, strEnlace, intPlantillaCorreoID);

                                        intCont++;
                                    }
                                }
                                else
                                {
                                    //Se genera excepción envío de correos
                                    throw new Exception("No se especifico listado de correo para enviar");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("No se obtuvo identificador del estado");
                        }
                    }
                    else
                    {
                        throw new Exception("El nuevo estado y el estado actual son iguales.");
                    }
                }
                catch (NotificacionException exc)
                {
                    throw exc;
                }
                catch (Exception exc)
                {

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Notificacion :: AvanzarEstadoNotificacionElectronica -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    throw new NotificacionException("Notificacion :: AvanzarEstadoNotificacionElectronica -> Error registrando avance: " + exc.Message, exc);
                }

                return objFechaEstadoNuevo;
            }


            /// <summary>
            /// Avanzar estado de recurso de reposición 
            /// </summary>
            /// <param name="p_intIdUsuario">int con el identificador del usuario que realiza el avance</param>
            /// <param name="p_intNumeroActoNotificacion">int con el numero de acto de notificacion</param>
            /// <param name="p_strNumeroIdentificacion">string con el numero de identificacion</param>
            /// <param name="p_strArchivosAdjuntos">string con los documentos adjuntos</param>
            public void NotificarRecursosRegistraRecurso(int p_intIdUsuario, int p_intNumeroActoNotificacion, string p_strNumeroIdentificacion, string p_strArchivosAdjuntos, string p_strRutaArchivosTemporales)
            {
                DataTable objInformacionEstado = null;
                NotificacionDalc objNotificacionDalc = null;
                List<NotificacionEntity> objLstObjNotificacion = null;
                NotificacionEntity objNotificacion = null;

                try
                {
                    //Consultar Actos
                    objNotificacionDalc = new NotificacionDalc();
                    objLstObjNotificacion = new List<NotificacionEntity>();
                    objLstObjNotificacion = objNotificacionDalc.ObtenerActos(new object[] { p_intNumeroActoNotificacion, null, null, null, null, null, null, null, null, null, null });

                    foreach (NotificacionEntity noty in objLstObjNotificacion)
                    {
                        objNotificacion = new NotificacionEntity();
                        objNotificacion = noty;

                        if (noty.IdentificacionUsuario == p_strNumeroIdentificacion)
                        {
                            PersonaNotificarEntity objPersonasNotificarEstadoActivoEject = objNotificacion.ListaPersonas.Find(p => p.NumeroIdentificacion == noty.IdentificacionUsuario);

                            //Obtener el estado al cual debe avanzar
                            objInformacionEstado = objNotificacionDalc.ConsultarActividadRecursoReposicion(objPersonasNotificarEstadoActivoEject.Id);

                            //Validar que se obtenga datos del usuario
                            if (objInformacionEstado != null && objInformacionEstado.Rows.Count > 0)
                            {
                                this.CrearEstadoPersonaActo(Convert.ToInt64(objPersonasNotificarEstadoActivoEject.IdActoNotificar), Convert.ToInt32(objInformacionEstado.Rows[0]["ID_FLUJO_NOT_ELEC"]), Convert.ToInt32(objInformacionEstado.Rows[0]["ESTADO_PADRE_ID"]),
                                                            Convert.ToInt32(objInformacionEstado.Rows[0]["ID_ESTADO"]), Convert.ToInt64(objPersonasNotificarEstadoActivoEject.Id), Convert.ToInt32(objInformacionEstado.Rows[0]["ID_AUTORIDAD"]), DateTime.Now, objNotificacion.NumeroSILPA, "Recurso de Reposición",
                                                            p_strRutaArchivosTemporales, objNotificacion.CodigoExpediente, objNotificacion.NumeroActoAdministrativo, p_strArchivosAdjuntos, null, false, null, false, null, "", false, false, "", null, "", default(DateTime), p_intIdUsuario, false, -1, -1, "", "" , "");
                                                            
                            }
                        }

                    }
                }
                catch (Exception exc)
                {

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Notificacion :: NotificarRecursosRegistraRecurso -> Error Inesperado: " + exc.Message);

                    throw new NotificacionException("Notificacion :: NotificarRecursosRegistraRecurso -> Error registrando avance: " + exc.Message, exc);
                }
            }


            /// <summary>
            /// Verificar si el acto administrativo notificacdo tiene conceptos asociados
            /// </summary>
            /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo</param>
            /// <returns>bool con true en caso de que tenga conceptos asociados, false en caso contrario</returns>
            public bool ActoTieneConceptosAsociados(long p_lngActoAdministrativoID)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ActoTieneConceptosAsociados(p_lngActoAdministrativoID);
            }


            /// <summary>
            /// Consultar el listado de conceptos asociados al acto administrativo
            /// </summary>
            /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo</param>
            /// <returns>DataTable con la información de conceptos del acto administrativo</returns>
            public DataTable ConsultarConceptosAsociadosActoAdministrativo(long p_lngActoAdministrativoID)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                return dalc.ConsultarConceptosAsociadosActoAdministrativo(p_lngActoAdministrativoID);
            }

		    /// <summary>
            /// Lista el tipo de notificaciones que se encuentran en el repositorio de datos.
            /// </summary>
            public List<TipoNotificacionEntity> ListarTiposNotificacion()
            {
                TIpoNotificacionDalc objTIpoNotificacionDalc = new TIpoNotificacionDalc();
                return objTIpoNotificacionDalc.ListarTiposNotificacion();
            }

            /// <summary>
            /// Consulta la lista de los flujos de notificacion electronica
            /// </summary>
            /// <param name="intTipoNotificacionID">Tipo Notificacion ID</param>
            /// <param name="intAutoridadID">Autoridad Ambiental ID</param>
            public List<FlujoNotificacionElectronicaEntity> ConsulaFlujosNotificacionElectronica(long lngActoID, int intTipoNotificacionID, int intAutoridadID)
            {
                FlujoNotificacionElectronicaDalc objFlujoNotificacionElectronicaDalc = new FlujoNotificacionElectronicaDalc();
                return objFlujoNotificacionElectronicaDalc.ConsulaFlujosNotificacionElectronica(lngActoID, intTipoNotificacionID, intAutoridadID);
            }


            /// <summary>
            /// Obtener la información de los actos administrativos existentes para notificación y publicación que cumplan con los parámetros de búsqueda
            /// </summary>
            /// <param name="p_strNumeroVital">string con el número vital</param>
            /// <param name="p_strExpediente">string con el código del expediente</param>
            /// <param name="p_strIdentificacionPersona">string con la identificación de la persona a notificar</param>
            /// <param name="p_strNombrePersona">string con el nombre de la persona a notificar</param>
            /// <param name="p_strNumeroActoAdministrativo">string con el número de acto administrativo</param>
            /// <param name="p_intTipoActoAdministrativoID">int con el identificador del tipo de acto administrativo</param>
            /// <param name="p_intEstadoActoId">int con el estado del acto administrativo</param>
            /// <param name="p_fechaActoInicial">DateTime con la fecha del acto administrativo inicial del rango de busqueda</param>
            /// <param name="p_fechaActoFinal">DateTime con la fecha del acto administrativo final del rango de busqueda</param>
            /// <param name="p_lngUsuarioID">long con el identificador del usuario que realiza la consulta</param>
            /// <returns>DataTable con la información de los actos y publicaciones</returns>
            public DataTable ConsultarInformacionActosAdministrativosPublicaciones(string p_strNumeroVital, string p_strExpediente,
                                                                                   string p_strIdentificacionPersona, string p_strNombrePersona,
                                                                                   string p_strNumeroActoAdministrativo, int p_intTipoActoAdministrativoID,
                                                                                   int p_intEstadoActoId, DateTime p_fechaActoInicial, DateTime p_fechaActoFinal,
                                                                                   long p_lngUsuarioID)
            {
                NotificacionDalc objNotificacionDalc = new NotificacionDalc();
                return objNotificacionDalc.ConsultarInformacionActosAdministrativosPublicaciones(p_strNumeroVital, p_strExpediente,
                                                                                                 p_strIdentificacionPersona, p_strNombrePersona,
                                                                                                 p_strNumeroActoAdministrativo, p_intTipoActoAdministrativoID,
                                                                                                 p_intEstadoActoId, p_fechaActoInicial, p_fechaActoFinal,
                                                                                                 p_lngUsuarioID);
            }


            /// <summary>
            /// Retorna listado de procesos asociados al acto administrativo que no han sido notificados
            /// </summary>
            /// <param name="p_decActoNotificacionID">decimal con el identificador del acto administrativo </param>
            /// <returns>List con la información de procesos de notificación asociados</returns>
            public List<NotificacionEntity> ObtenerProcesosAsociadosNoNotificados(decimal p_decActoNotificacionID)
            {
                NotificacionDalc objNotificacionDalc = new NotificacionDalc();
                return objNotificacionDalc.ObtenerProcesosAsociadosNoNotificados(p_decActoNotificacionID);
            }


            /// <summary>
            /// Indica si tiene procesos asociados al acto administrativo que no han sido notificados
            /// </summary>
            /// <param name="p_decActoNotificacionID">decimal con el identificador del acto administrativo </param>
            /// <returns>bool en caso de que tenga procesos asociados, false en caso contrario</returns>
            public bool TieneProcesosAsociadosNoNotificados(decimal p_decActoNotificacionID)
            {
                NotificacionDalc objNotificacionDalc = new NotificacionDalc();
                return objNotificacionDalc.TieneProcesosAsociadosNoNotificados(p_decActoNotificacionID);
            }


            public void AgregarPersonaProcesoNotificacion(PersonaNotificarEntity pPersonaNotificarEntity)
            {
                PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
                _personaDalc.Insertar(ref pPersonaNotificarEntity);
            }


            public void ActualizarPersonaProcesoNotificacion(PersonaNotificarEntity pPersonaNotificarEntity)
            {
                PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
                _personaDalc.Actualizar(ref pPersonaNotificarEntity);
            }


            public void EliminarPersonaProcesoNoticiacion(long p_lngActoID, long p_lngPersonaID, string p_strMotivoEliminacion, string p_strUsuario)
            {
                PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
                _personaDalc.Eliminar(p_lngActoID, p_lngPersonaID, p_strMotivoEliminacion, p_strUsuario);
            }

        #endregion

    }
}