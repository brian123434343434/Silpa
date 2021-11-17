
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.AccesoDatos.Salvoconducto;
using SILPA.AccesoDatos.Aprovechamiento;
using SILPA.Servicios.Salvoconducto;
using SILPA.LogicaNegocio.Comunicacion;
using SILPA.LogicaNegocio.Salvoconducto;
using SILPA.LogicaNegocio.Aprovechamiento;
using SILPA.LogicaNegocio.Generico;
using System.Xml;
using SoftManagement.Log;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ServiceModel.Web;
using System;
/// <summary>
/// Summary description for WSSUN
/// 22-FEB-2010
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSSUN : System.Web.Services.WebService
{

    public WSSUN()
    {
    }

    [WebMethod(Description= "[Recibe los datos del salvonconducto]")]
    public string RecibirDatosSalvoconducto(string datosSalvoconductoXML)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";
        try
        {
            iIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), "RecibirDatosSalvoconducto", datosSalvoconductoXML, "", 0);
            SalvoconductoFachada _objSalvoFachada = new SalvoconductoFachada();
            string mensaje=SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividadSalvoconducto(_objSalvoFachada.GuardarSalvoconducto(datosSalvoconductoXML),DatosSesion.Usuario);
           return "Exito";
        }
        catch (Exception ex)
        {
            SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), "RecibirDatosSalvoconducto", ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), "RecibirDatosSalvoconducto", datosSalvoconductoXML, strNoVital, iAA, iIdPadre);
        }
    }

    [WebMethod(Description = "[Recibe los datos del recurso]")]
    public string RecibirDatosRecursos(string datosRecursoXML)
    {
        Int64 iIdPadre = 0;
        Int32 iAA = 0;
        String strNoVital = "";

        try
        {
            iIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), "RecibirDatosRecursos", datosRecursoXML, "", 0);
            SalvoconductoFachada _objSalvoFachada = new SalvoconductoFachada();
            string mensaje = _objSalvoFachada.GuardarRecursos(datosRecursoXML);
            return "Exito";
        }
        catch (Exception ex)
        {
            SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), "RecibirDatosRecursos", ex.ToString(), iIdPadre);
            SMLog.Escribir(ex);
            throw;
        }

        finally
        {
            SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), "RecibirDatosSalvoconducto", datosRecursoXML, strNoVital, iAA, iIdPadre);
        }

    }
    # region Aprovechamiento
    [WebMethod(Description = "RecibeInformacion Arpovechamiento")]
    public string RegistrarAprovechamiento(AprovechamientoIdentity vAprovechamientoIdentity)
    {
        try
        {
            Aprovechamiento vAprovechamiento = new Aprovechamiento();
            return vAprovechamiento.CrearAprovechamiento(ref vAprovechamientoIdentity);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    [WebMethod(Description = "Consulta lista autoridad Ambiental")]
    
    public List<AprovechamientoIdentity> ListaAprovechamiento(int pAutoridadId, int pUsuarioId, int pTipoAprovechamientoId)
    {
        Aprovechamiento vAprovechamiento = new Aprovechamiento();
        return vAprovechamiento.ConsultaAprovechamientoAutoridadSolicitante(pAutoridadId, pUsuarioId, pTipoAprovechamientoId);
    }

    #endregion

    #region Salvoconducto
    [WebMethod(Description = "RecibeInformacion Salvoconducto")]
    public int RegistrarSalvoconducto(SalvoconductoNewIdentity vSalvoconductoNewIdentity)
    {
        SalvoconductoNew vSalvoconductoNew = new SalvoconductoNew();
        return 0;// vSalvoconductoNew.CrearSolicitudSalvoconducto(vSalvoconductoNewIdentity);
    }
    [WebMethod(Description = "Consulta la informacion del Salvoconducto asociado al Salvoconducto ID")]
    public SalvoconductoNewIdentity ConsultaSalvoconductoXSalvoconductoID(int pSalvoconductoId)
    {
        SalvoconductoNew vSalvoconductoNew = new SalvoconductoNew();
        return vSalvoconductoNew.ConsultaSalvoconductoXSalvoconductoId(pSalvoconductoId);
    }
    #endregion
    #region Listas Basicas
    [WebMethod(Description = "Consulta los tipos de salvoconducto")]
    public List<TipoSalvoconductoIdentity> ListaTipoSalvoConducto()
    {
        TipoSalvoconducto vTipoSalvoconducto = new TipoSalvoconducto();
        return vTipoSalvoconducto.ListaTipoSalvoconducto();
    }
    [WebMethod(Description = "Consulta las clases de recurso")]
    public List<ClaseRecursoIdentity> ListaClaseRecurso()
    {
        ClaseRecurso vClaseRecurso = new ClaseRecurso();
        return vClaseRecurso.ListaClaseRecurso();
    }
    [WebMethod(Description = "Consulta los modos de adquisicion del recurso ")]
    public List<ModoAdquisicionRecursoIdentity> ListaModoAdquisicionRecurso(int? claseRecursoId)
    {
        ModoAdquisicionRecurso vModoAdquisicionRecurso = new ModoAdquisicionRecurso();
        return new List<ModoAdquisicionRecursoIdentity>();// vModoAdquisicionRecurso.ListaModoAdquisicionRecurso(claseRecursoId);
    }
    [WebMethod(Description = "Consulta las finalidades del aprovechamiento")]
    public List<FinalidadRecursoIdentity> ListaFinalidadRecurso(int? claseRecursoId)
    {
        FinalidadAprovechamiento vFinalidadAprovechamiento = new FinalidadAprovechamiento();
        return vFinalidadAprovechamiento.ListaFinalidadAprovechamiento(claseRecursoId);
    }
    [WebMethod(Description = "Consulta las clase del aprovechamiento")]
    public List<ClaseAprovechamientoIdentity> ListaClaseAprovechamiento(int? claseRecursoId)
    {
        ClaseAprovechamiento vClaseAprovechamiento = new ClaseAprovechamiento();
        return vClaseAprovechamiento.ListaClaseAprovechamiento(claseRecursoId);
    }
    [WebMethod(Description = "Consulta las procedencias legales")]
    public List<ProcedenciaLegalIdentity> ListaProcedenciaLegal()
    {
        ProcedenciaLegal vProcedenciaLegal = new ProcedenciaLegal();
        return vProcedenciaLegal.ListaProcedenciaLegal();
    }
    [WebMethod(Description = "Consulta la lista de unidades de medida")]
    public List<UnidadMedidaIdentity> ListaUnidaMedidaTipoProducto(int tipoProductoID)
    {
        UnidadMedida vUnidadMedida = new UnidadMedida();
        return vUnidadMedida.ListaUnidadMedidaTipoProducto(tipoProductoID);
    }
    [WebMethod(Description = "Consulta la lista de clase de producto del recurso")]
    public List<ClaseProductoIdentity> ListaClaseProductoParaAprovechamiento(int claseRecursoId)
    {
        ClaseProducto vClaseProducto = new ClaseProducto();
        return vClaseProducto.ListaClaseProducto(claseRecursoId, false);
    }
    [WebMethod(Description = "Consulta la lista de clase de producto del recurso")]
    public List<TipoProductoIdentity> ListaTipoProducto(int claseProductoId)
    {
        TipoProducto vTipoProducto = new TipoProducto();
        return new List<TipoProductoIdentity>(); //vTipoProducto.ListaTipoProducto(claseProductoId);
    }
    [WebMethod(Description = "Consulta la lista de especies por recurso")]
    public List<EspecieIdentity> ListaEspecie(string nombreCientifico, int claseRecursoId)
    {
        Especie vEspecie = new Especie();
        return vEspecie.ListaEspecie(nombreCientifico, claseRecursoId);
    }
    [WebMethod(Description = "Consulta lista autoridad Ambiental")]
    public List<AutoridadAmbiental> ListaAutoridadAmbiental()
    {
        List<AutoridadAmbiental> LstAutoridad = new List<AutoridadAmbiental>();
        Listas lstListas = new Listas();
        DataTable dttAutoridades = lstListas.ListarAutoridades(null).Tables[0];
        foreach (DataRow dtrAutoridad in dttAutoridades.Rows)
        {
            LstAutoridad.Add(new AutoridadAmbiental(Convert.ToInt32(dtrAutoridad["AUT_ID"]), dtrAutoridad["AUT_NOMBRE"].ToString()));
        }
        return LstAutoridad;
    }
    [WebMethod(Description = "Consulta lista Forma Otorgamiento")]

    public List<FormaOtorgamientoIdentity> ListaFormaOtorgamiento(int claseRecursoID)
    {
        FormaOtorgamiento vFormaOtorgamiento = new FormaOtorgamiento();
        return new List<FormaOtorgamientoIdentity>();// vFormaOtorgamiento.ListaFormaOtorgamiento(claseRecursoID);
    }
    [WebMethod(Description = "Consulta lista Modo de Transporte")]

    public List<ModoTransporteIdentity> ListaModoTransporte()
    {
        ModoTransporte vModoTransporte = new ModoTransporte();
        return vModoTransporte.ListaModoTransporte();
    }
    [WebMethod(Description = "Consulta lista Tipo de Transporte")]

    public List<TipoTransporteIdentity> ListaTipoTransportePorModoTransporte(int modoTransporteID)
    {
        TipoTransporte vTipoTransporte = new TipoTransporte();
        return vTipoTransporte.ListaTipoTransportePorModoTransporte(modoTransporteID);
    }
    #endregion

    #region CargueSaldos
    //[WebMethod(Description = "Consulta lista autoridad Ambiental")]
    //public CargueSaldoIdentity ConsultaCargueSaldoTarea(int TareaID)
    //{
    //    CargueSaldo vCargueSaldo = new CargueSaldo();
    //    return vCargueSaldo.ConsultaCargueSaldoTarea(TareaID);
    //}
    [WebMethod(Description = "RecibeInformacion para crear el saldo del Arpovechamiento")]
    public int CargueSaldoAprovechamiento(AprovechamientoIdentity vAprovechamientoIdentity, int tipoSaldoID)
    {
        //int CargueID = 0;
        //Aprovechamiento vAprovechamiento = new Aprovechamiento();
        //if (vAprovechamientoIdentity.AprovechamientoID != 0)
        //   CargueID = vAprovechamiento.ActualizarAprovechamiento(vAprovechamientoIdentity);
        //else
        //    CargueID = vAprovechamiento.CrearAprovechamiento(vAprovechamientoIdentity);
        //CargueSaldo vCargueSaldo = new CargueSaldo();
        ////vCargueSaldo.InsertaCargueSaldoTarea(new CargueSaldoIdentity { CargueID = CargueID, TareaID = vAprovechamientoIdentity.TareaID, TipoSaldoID = tipoSaldoID });
        //return CargueID;
        return 0;
    }
    //[WebMethod(Description = "RecibeInformacion para crear el saldo del Arpovechamiento")]
    //public int CargueSaldoSAlvoconducto(SalvoconductoNewIdentity vSalvoconductoNewIdentity, int tipoSaldoID)
    //{
    //    SalvoconductoNew vSalvoconductoNew = new SalvoconductoNew();
    //    int CargueID = vSalvoconductoNew.CrearSolicitudSalvoconducto(vSalvoconductoNewIdentity);
    //    CargueSaldo vCargueSaldo = new CargueSaldo();
    //    //vCargueSaldo.InsertaCargueSaldoTarea(new CargueSaldoIdentity { CargueID = CargueID, TareaID = vSalvoconductoNewIdentity.TareaID, TipoSaldoID = tipoSaldoID });
    //    return CargueID;
    //}
    #endregion

    #region MetodosJSON
    [WebMethod(Description = "Listas de Autoridades")]
    public string ListaAutoridadAmbientalJSON()
    {
        List<AutoridadAmbiental> LstAutoridad = new List<AutoridadAmbiental>();
        Listas lstListas = new Listas();
        DataTable dttAutoridades = lstListas.ListarAutoridades(null).Tables[0];
        foreach (DataRow dtrAutoridad in dttAutoridades.Rows)
        {
            LstAutoridad.Add(new AutoridadAmbiental(Convert.ToInt32(dtrAutoridad["AUT_ID"]), dtrAutoridad["AUT_NOMBRE"].ToString()));
        }
        return JsonConvert.SerializeObject(LstAutoridad);
    }
    //[WebMethod(Description = "Consulta lista autoridad Ambiental")]

    //public string ListaAprovechamientoJSON(int pAutoridadId, int pUsuarioId)
    //{
    //    Aprovechamiento vAprovechamiento = new Aprovechamiento();
    //    return JsonConvert.SerializeObject(vAprovechamiento.ConsultaAprovechamientoAutoridadSolicitante(pAutoridadId, pUsuarioId));
    //}
    [WebMethod(Description = "Consulta la informacion del Arpovechamiento asociado a la tarea")]

    public string ListaRecursosAprovechamientoJSON(int pAprovechamientoId)
    {
        Aprovechamiento vAprovechamiento = new Aprovechamiento();
        return JsonConvert.SerializeObject(vAprovechamiento.ListaRecursosAprovechamiento(pAprovechamientoId));
    }
    [WebMethod(Description = "Consulta las listas de los departamentos (DEP_NOMBRE,DEP_ID)")]

    public string ListaDepartamentosJSON()
    {
        Listas listaDepto = new Listas();
        return JsonConvert.SerializeObject(listaDepto.ListarDepartamentos(49).Tables[0]);
    }
    [WebMethod(Description = "Consulta las listas de los municipios del departamento (MUN_NOMBRE,MUN_ID)")]

    public string ListaMunicipiosJSON(string DeptoID)
    {
        Listas listaMunpio = new Listas();
        return JsonConvert.SerializeObject(listaMunpio.ListaMunicipios(null, int.Parse(DeptoID), null).Tables[0]);
    }

    [WebMethod(Description = "Consulta las listas de los Tipo de productos")]

    public string ListaUnidadMedidaPorClaseRecursoJSON(string claseRecursoId)
    {
        UnidadMedida vUnidadMedida = new UnidadMedida();
        return JsonConvert.SerializeObject(vUnidadMedida.ListaUnidadMedidaTipoProducto(Convert.ToInt32(claseRecursoId)));
    }

    [WebMethod(Description = "Consulta las listas de los Tipo de productos")]

    public string ListaModoTransporteJSON()
    {
        ModoTransporte vModoTransporte = new ModoTransporte();
        return JsonConvert.SerializeObject(vModoTransporte.ListaModoTransporte());
    }
    [WebMethod(Description = "Consulta las listas de los Tipo de productos")]

    public string ListaTipoTransportePorModoTransporteJSON(string modoTransporteID)
    {
        TipoTransporte vTipoTransporte = new TipoTransporte();
        return JsonConvert.SerializeObject(vTipoTransporte.ListaTipoTransportePorModoTransporte(Convert.ToInt32(modoTransporteID)));
    }

    #endregion

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {        
        return;
    }



    #region Consultas Salvoconductos

        [WebMethod(Description = "[Registra un nuevo cobro de autoliquidacion en VITAL]")]
        public string VerificarResolucionSUN(string strInformacionResolucion)
        {
            Int64 intIdPadre = 0;
            string strMensaje = "";
            string strDatosRecibidos = "";
            SalvoconductoFachada objSalvoFachada = null;

            try
            {
                //Insertar log
                strDatosRecibidos = "strInformacionResolucion: " + (!string.IsNullOrEmpty(strInformacionResolucion) ? strInformacionResolucion : "null");
                intIdPadre = SoftManagement.LogWS.SMLogWS.EscribirInicio(this.ToString(), "VerificarResolucionSUN", strDatosRecibidos, "", 0);

                //Generar certificado
                objSalvoFachada = new SalvoconductoFachada();
                strMensaje = objSalvoFachada.VerificarResolucionSUN(strInformacionResolucion);

            }
            catch (Exception ex)
            {
                SoftManagement.LogWS.SMLogWS.EscribirExcepcion(this.ToString(), "VerificarResolucionSUN", ex.ToString(), intIdPadre);
                SMLog.Escribir(ex);
                strMensaje = "Se presento error durante la validación de la resolución en el sistema de salvoconducto. Error: " + ex.ToString();
            }
            finally
            {
                SoftManagement.LogWS.SMLogWS.EscribirFinalizar(this.ToString(), "VerificarResolucionSUN", strMensaje, "", 0, intIdPadre);
            }

            return strMensaje;
        }


    #endregion

}

