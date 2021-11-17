using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SILPA.AccesoDatos.WSIntegracion_SUNL_IDEAM_Dalc;
using SILPA.AccesoDatos.Aprovechamiento;
using SILPA.LogicaNegocio.Generico;

namespace SILPA.LogicaNegocio.WSIntegracion_SUNL_IDEAM
{
    public class RegistroAprovechamiento
    {
        #region Variables del Servicio del IDEAM


        private WSRegistrarAprovechamientoIDEAM.Auditoria ObjAprovechamientoAuditoria = null;
        private WSRegistrarAprovechamientoIDEAM.InfoGralAA ObjAprovechamientoInfoGralAA = null;
        private WSRegistrarAprovechamientoIDEAM.InfoUsuarioRecurso ObjAprovechamientoInfoUsuarioRecurso = null;
        private WSRegistrarAprovechamientoIDEAM.InfoLugarAprovechamiento ObjAprovechamientoInfoLugarAprovechamiento = null;
        private WSRegistrarAprovechamientoIDEAM.InfoGralTramite ObjAprovechamientoInfoGralTramite = null;
        private WSRegistrarAprovechamientoIDEAM.Vertice ObjAprovechamientoVertice = null;
        private WSRegistrarAprovechamientoIDEAM.Especie ObjAprovechamientoEspecie = null;
        private WSRegistrarAprovechamientoIDEAM.guardarAprovechamientoRequest ObjAprovechamientoRequest = null;
        private SILPA.LogicaNegocio.Generico.Listas ObjListasSUNL = null;
        #endregion

        #region Variables locales para obtener la informacion del aprovechamiento
        private RegistroAprovechamientoDalc ObjRegistroAprovechamientoDalc = null;
        private AprovechamientoDalc ObjAprovechamientoDalc = null;
        private AprovechamientoIdentity ObjAprovechamientoIdentity = null;
        #endregion

        private int int_LogID = 0;

        private void GuardarAprovechamiento(WSRegistrarAprovechamientoIDEAM.Auditoria ObjAprovechamientoAuditoria, WSRegistrarAprovechamientoIDEAM.guardarAprovechamientoRequest ObjAprovechamientoRequest, int int_aprovechamientoID)
        {

            string CodigoError = string.Empty;
            string IdentificadorCargue = string.Empty;
            string[] MensajeError;
            try
            {
                WSRegistrarAprovechamientoIDEAM.RegistrarAprovechamientoPortTypeClient objClient = new WSRegistrarAprovechamientoIDEAM.RegistrarAprovechamientoPortTypeClient();
                objClient.guardarAprovechamiento(ObjAprovechamientoAuditoria, ObjAprovechamientoRequest.InfoGralAA, ObjAprovechamientoRequest.InfoUsuarioRecurso, ObjAprovechamientoRequest.InfoGralTramite, ObjAprovechamientoRequest.InfoLugarAprovechamiento, ObjAprovechamientoRequest.Poligono, ObjAprovechamientoRequest.InfoEspecieAprovechada, out CodigoError, out MensajeError, out IdentificadorCargue);

                if ((string.IsNullOrEmpty(CodigoError) && CodigoError == "0") && Convert.ToInt32(MensajeError.Length) == 0 && IdentificadorCargue.Length > 0)
                {
                    ObjRegistroAprovechamientoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_aprovechamientoID, "Consumo Servicio Aprovechamiento Exitoso Identificador Cargue: " + IdentificadorCargue, true);
                }
                else if (MensajeError.Length > 0)
                {
                    foreach (string StrError in MensajeError)
                    {
                        ObjRegistroAprovechamientoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_aprovechamientoID, "Consumo Servicio Detalle Error: " + StrError, false);
                    }
                }
            }
            catch (Exception ex)
            {
                ObjRegistroAprovechamientoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_aprovechamientoID, "Consumo Servicio Detalle Error: " + ex.Message.ToString(), false);
            }
        }
        public void ListarAprovechamientosDiario()
        {
            ObjRegistroAprovechamientoDalc = new RegistroAprovechamientoDalc();
            DataSet dsLstIdAprovechamiento = new DataSet();
            dsLstIdAprovechamiento = ObjRegistroAprovechamientoDalc.ObtenerIdAprovechamiento(true);
            int int_AprovechamientoID = 0;

            if (dsLstIdAprovechamiento.Tables[0].Rows.Count > 0)
            {
                int_LogID = ObjRegistroAprovechamientoDalc.InsertarCabeceraLogConsumoWSIDEAM(null, null, "Diario");

                if (int_LogID > 0)
                {
                    foreach (DataRow dr in dsLstIdAprovechamiento.Tables[0].Rows)
                    {
                        int_AprovechamientoID = Convert.ToInt32(dr["APROVECHAMIENTO_ID"]);
                        ObtenerDatosAprovechamiento(int_AprovechamientoID);
                    }
                }

            }
        }
        private void ObtenerDatosAprovechamiento(int int_aprovechamientoID)
        {
            ObjAprovechamientoDalc = new AprovechamientoDalc();
            ObjAprovechamientoIdentity = new AprovechamientoIdentity();
            bool Error = false;
            ObjAprovechamientoIdentity = ObjAprovechamientoDalc.ConsultaAprovechamientoXAprovechamientoId(int_aprovechamientoID);

            if (ObjAprovechamientoIdentity != null)
            {
                ObjAprovechamientoRequest = new WSRegistrarAprovechamientoIDEAM.guardarAprovechamientoRequest();
                ObjRegistroAprovechamientoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_aprovechamientoID, "se obtiene Datos Aprovechamiento", false);

                #region Tag <reg:Auditoria>
                ObjAprovechamientoAuditoria = new WSRegistrarAprovechamientoIDEAM.Auditoria();
                ObjAprovechamientoAuditoria.SistemaSolicitud = "SUNL";
                //ObjAprovechamientoAuditoria.FechaHoraSolicitud = DateTime.Now.ToString();
                //ObjAprovechamientoAuditoria.IpSolicitud = "192.1.1.1";
                ObjAprovechamientoAuditoria.AutoridadAmbiental = "5000029864";
                ObjAprovechamientoAuditoria.Dependencia = "WebService";
                //ObjAprovechamientoAuditoria.FechaHoraSolicitud = DateTime.Now.ToString("dd/MM/yyyy");
                #endregion

                #region tag <reg:InfoGralAA>


                DataSet dsInfoAutAmb = new DataSet();
                ObjAprovechamientoInfoGralAA = new WSRegistrarAprovechamientoIDEAM.InfoGralAA();
                ObjListasSUNL = new Listas();
                dsInfoAutAmb = ObjListasSUNL.ListarAutoridadesSUNL(ObjAprovechamientoIdentity.AutoridadEmisoraID);

                if (dsInfoAutAmb.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsInfoAutAmb.Tables[0].Rows)
                    {
                        ObjAprovechamientoInfoGralAA.CodigoAutoridad = dr["CODIGO_IDEAM"].ToString();
                        ObjAprovechamientoInfoGralAA.Dependencia = "WebService";
                        ObjAprovechamientoInfoGralAA.Funcionario = dr["AUT_NOMBRE"].ToString();
                        ObjAprovechamientoInfoGralAA.Correo = dr["AUT_CORREO"].ToString();
                        ObjAprovechamientoInfoGralAA.Telefono = dr["AUT_TELEFONO"].ToString().Substring(0, 7);
                        ObjAprovechamientoRequest.InfoGralAA = ObjAprovechamientoInfoGralAA;
                    }
                }
                else
                {
                    ObjRegistroAprovechamientoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_aprovechamientoID, "no existe informacion de la autoridad ambiental emisiora del aprovechamiento", false);
                    Error = true;
                }
                #endregion

                #region tag <reg:InfoUsuarioRecurso>
                ObjAprovechamientoInfoUsuarioRecurso = new WSRegistrarAprovechamientoIDEAM.InfoUsuarioRecurso();
                ObjAprovechamientoInfoUsuarioRecurso.CodigoTipoPersona = ObjAprovechamientoIdentity.Solicitante.TipoPersonaIDEAM;
                ObjAprovechamientoInfoUsuarioRecurso.CodigoTipoIdentificacion = ObjAprovechamientoIdentity.Solicitante.TipoIdentificacionIDEAM;
                ObjAprovechamientoInfoUsuarioRecurso.Identificacion = ObjAprovechamientoIdentity.Solicitante.NumeroIdentificacion;
                ObjAprovechamientoInfoUsuarioRecurso.RazonSocial = ObjAprovechamientoIdentity.Solicitante.RazonSocial;
                ObjAprovechamientoInfoUsuarioRecurso.PrimerNombre = ObjAprovechamientoIdentity.Solicitante.PrimerNombre;
                ObjAprovechamientoInfoUsuarioRecurso.SegundoNombre = ObjAprovechamientoIdentity.Solicitante.SegundoNombre;
                ObjAprovechamientoInfoUsuarioRecurso.PrimerApellido = ObjAprovechamientoIdentity.Solicitante.PrimerApellido;
                ObjAprovechamientoInfoUsuarioRecurso.SegundoApellido = ObjAprovechamientoIdentity.Solicitante.SegundoApellido;
                ObjAprovechamientoInfoUsuarioRecurso.CorreoElectronico = ObjAprovechamientoIdentity.Solicitante.CorreoElectronico;
                ObjAprovechamientoInfoUsuarioRecurso.TelefonoFijo = ObjAprovechamientoIdentity.Solicitante.Telefono.Substring(0, 7);
                ObjAprovechamientoInfoUsuarioRecurso.Celular = ObjAprovechamientoIdentity.Solicitante.Celular.Substring(0, 10);
                ObjAprovechamientoInfoUsuarioRecurso.Direccion = ObjAprovechamientoIdentity.Solicitante.DireccionPersona.DireccionPersona;
                ObjAprovechamientoInfoUsuarioRecurso.CodigoDepartamento = ObjAprovechamientoIdentity.Solicitante.DireccionPersona.DepartamentoId.ToString().PadLeft(2, '0');
                ObjAprovechamientoInfoUsuarioRecurso.CodigoMunicipio = ObjAprovechamientoIdentity.Solicitante.DireccionPersona.MunicipioId.ToString().PadLeft(5, '0');
                ObjAprovechamientoRequest.InfoUsuarioRecurso = ObjAprovechamientoInfoUsuarioRecurso;
                #endregion

                #region <reg:InfoLugarAprovechamiento>
                ObjAprovechamientoInfoLugarAprovechamiento = new WSRegistrarAprovechamientoIDEAM.InfoLugarAprovechamiento();
                ObjAprovechamientoInfoLugarAprovechamiento.NombrePredio = ObjAprovechamientoIdentity.Predio;
                ObjAprovechamientoInfoLugarAprovechamiento.DireccionPredio = ObjAprovechamientoIdentity.DepartamentoProcedencia + "-" + ObjAprovechamientoIdentity.MunicipioProcedencia + "-" + ObjAprovechamientoIdentity.CorregimientoProcedencia + "-" + ObjAprovechamientoIdentity.VeredaProcedencia;
                ObjAprovechamientoInfoLugarAprovechamiento.Vereda = ObjAprovechamientoIdentity.VeredaProcedencia;
                ObjAprovechamientoInfoLugarAprovechamiento.Corregimiento = ObjAprovechamientoIdentity.CorregimientoProcedencia;
                ObjAprovechamientoInfoLugarAprovechamiento.CodigoDepartamento = ObjAprovechamientoIdentity.DepartamentoProcedenciaID.ToString().PadLeft(2, '0');
                ObjAprovechamientoInfoLugarAprovechamiento.CodigoMunicipio = ObjAprovechamientoIdentity.MunicipioProcedenciaID.ToString().PadLeft(5, '0');
                ObjAprovechamientoRequest.InfoLugarAprovechamiento = ObjAprovechamientoInfoLugarAprovechamiento;
                #endregion

                #region <reg:InfoGralTramite>
                ObjAprovechamientoInfoGralTramite = new WSRegistrarAprovechamientoIDEAM.InfoGralTramite();
                ObjAprovechamientoInfoGralTramite.NumeroActoAdministrativoAprovechamiento = ObjAprovechamientoIdentity.Numero;
                ObjAprovechamientoInfoGralTramite.FechaExpedicionActoAdministrativo = ObjAprovechamientoIdentity.FechaExpedicion.HasValue ? ObjAprovechamientoIdentity.FechaExpedicion.Value.ToString("dd/MM/yyyy") : string.Empty;
                ObjAprovechamientoInfoGralTramite.FechaFinalizacionActoAdministrativo = ObjAprovechamientoIdentity.FechaFinalizacion != null? ObjAprovechamientoIdentity.FechaFinalizacion.Value.ToString("dd/MM/yyyy"): string.Empty;

                ObjAprovechamientoInfoGralTramite.CodigoClaseAprovechamiento = ObjAprovechamientoIdentity.CodigoIDEAMModoAdquisicion;

                ObjAprovechamientoInfoGralTramite.CodigoFormaOtorgaAprovechamiento = ObjAprovechamientoIdentity.CodigoIDEAMFormaOtorgamiento;

                if (ObjAprovechamientoIdentity.AreaTotalAutorizada > 0)
                {
                    ObjAprovechamientoInfoGralTramite.AreaTotalOtorgada = ObjAprovechamientoIdentity.AreaTotalAutorizada.ToString();

                }
                else
                {
                    ObjRegistroAprovechamientoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_aprovechamientoID, "El area otorgada no debe ser cero(0) para este este ID aprovechamiento", false);
                    Error = true;
                }

                switch (ObjAprovechamientoIdentity.ModoAdquisicionRecursoID)
                {

                    case 8: //Aprovechamiento de Arboles Aislados
                        if (ObjAprovechamientoIdentity.CodigoUbicacionArbolAislado > 0)
                        {
                            ObjAprovechamientoInfoGralTramite.CodigoUbicacionArbolAislado = ObjAprovechamientoIdentity.CodigoIDEAMUbicacionArbolAislado;
                        }
                        else
                        {
                            ObjAprovechamientoInfoGralTramite.CodigoUbicacionArbolAislado = "RRL";
                        }
                        break;

                    default:
                        break;
                }

                ObjAprovechamientoRequest.InfoGralTramite = ObjAprovechamientoInfoGralTramite;
                #endregion

                #region <reg:Poligono< - <reg:Vertices>
                if (ObjAprovechamientoIdentity.LstCoordenadas != null && ObjAprovechamientoIdentity.LstCoordenadas.Count > 0)
                {
                    int i = 0;
                    ObjAprovechamientoRequest.Poligono = new WSRegistrarAprovechamientoIDEAM.Vertice[ObjAprovechamientoIdentity.LstCoordenadas.Count];
                    foreach (var AprovechamientoCoordenadas in ObjAprovechamientoIdentity.LstCoordenadas)
                    {

                        ObjAprovechamientoVertice = new WSRegistrarAprovechamientoIDEAM.Vertice();
                        ObjAprovechamientoVertice.Latitud = AprovechamientoCoordenadas.Norte.ToString().Replace(',', '.');
                        ObjAprovechamientoVertice.Longitud = AprovechamientoCoordenadas.Este.ToString().Replace(',', '.');
                        ObjAprovechamientoRequest.Poligono[i] = ObjAprovechamientoVertice;
                        i += 1;
                    }
                }
                else
                {
                    ObjRegistroAprovechamientoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_aprovechamientoID, "no existe datos de ubicacion de latitud y longitud para este ID aprovechamiento", false);
                    Error = true;
                }

                #endregion

                #region <reg:InfoEspecieAprovechada> - <reg:EspeciesAprovechadas>
                if (ObjAprovechamientoIdentity.LstEspecies != null && ObjAprovechamientoIdentity.LstEspecies.Count > 0)
                {
                    int y = 0;
                    ObjAprovechamientoRequest.InfoEspecieAprovechada = new WSRegistrarAprovechamientoIDEAM.Especie[ObjAprovechamientoIdentity.LstEspecies.Count];
                    foreach (var AprovechamientoEspecies in ObjAprovechamientoIdentity.LstEspecies)
                    {
                        ObjAprovechamientoEspecie = new WSRegistrarAprovechamientoIDEAM.Especie();
                        if (String.IsNullOrEmpty(AprovechamientoEspecies.CodigoIDEAMEspecie))
                        {
                            AprovechamientoEspecies.CodigoIDEAMEspecie = "1000029171";
                        }
                        ObjAprovechamientoEspecie.CodigoNombreCientifico = AprovechamientoEspecies.CodigoIDEAMEspecie;
                        ObjAprovechamientoEspecie.NombreComun = AprovechamientoEspecies.NombreComunEspecie;
                        ObjAprovechamientoEspecie.CodigoClaseRecurso = ObjAprovechamientoIdentity.CodigoIDEAMClaseRecurso;

                        if (string.IsNullOrEmpty(AprovechamientoEspecies.CodigoIDEAMUnidadMedida))
                        {
                            ObjRegistroAprovechamientoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_aprovechamientoID, "no existe datos Unidad de Medida para las Especies para este ID aprovechamiento", true);
                            Error = true;
                        }
                        else
                        {
                            ObjAprovechamientoEspecie.CodigoUnidadMedida = AprovechamientoEspecies.CodigoIDEAMUnidadMedida;
                        }


                        switch (ObjAprovechamientoIdentity.CodigoIDEAMClaseRecurso)
                        {

                            case "MA":
                                ObjAprovechamientoEspecie.VolumenBrutoOtorgadoMaderable = AprovechamientoEspecies.CantidadVolumenMovilizar.ToString();
                                break;

                            case "NM":
                                if (string.IsNullOrEmpty(AprovechamientoEspecies.CodigoIDEAMTipoProducto))
                                {
                                    ObjRegistroAprovechamientoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_aprovechamientoID, "no existe datos Clase de producto para las Especies para este ID aprovechamiento", false);
                                    Error = true;
                                }
                                else
                                {
                                    ObjAprovechamientoEspecie.VolumenBrutoOtorgadoNoMaderable = AprovechamientoEspecies.CantidadVolumenMovilizar.ToString();
                                    ObjAprovechamientoEspecie.CodigoTipoProducto = AprovechamientoEspecies.CodigoIDEAMTipoProducto;
                                }

                                break;
                            default:
                                break;
                        }


                        switch (ObjAprovechamientoInfoGralTramite.CodigoUbicacionArbolAislado)
                        {
                            case "URB":
                                ObjAprovechamientoEspecie.DiametroAlturaPecho = AprovechamientoEspecies.DiametroAlturaPecho.ToString();
                                ObjAprovechamientoEspecie.AlturaComercial = AprovechamientoEspecies.AlturaComercial.ToString();
                                ObjAprovechamientoEspecie.TratamientoSilviculturaAsignado = AprovechamientoEspecies.CodigoIdeamTratamientoSilvID;
                                break;
                            default:
                                break;
                        }


                        if (string.IsNullOrEmpty(AprovechamientoEspecies.NombreComunEspecie))
                        {
                            ObjAprovechamientoEspecie.Observaciones = AprovechamientoEspecies.NombreEspecie;
                        }
                        else
                        {
                            ObjAprovechamientoEspecie.Observaciones = AprovechamientoEspecies.NombreEspecie;
                        }

                        if (ObjAprovechamientoIdentity.CodigoUbicacionArbolAislado > 0 && !string.IsNullOrEmpty(ObjAprovechamientoInfoGralTramite.CodigoUbicacionArbolAislado))
                        {

                            ObjAprovechamientoEspecie.TratamientoSilviculturaAsignado = string.Empty;
                        }

                        ObjAprovechamientoRequest.InfoEspecieAprovechada[y] = ObjAprovechamientoEspecie;
                        y += 1;
                    }

                }
                else
                {
                    ObjRegistroAprovechamientoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_aprovechamientoID, "no existe datos de Especies para este ID aprovechamiento", false);
                    Error = true;
                }
                #endregion


                if (!Error)
                {
                    GuardarAprovechamiento(ObjAprovechamientoAuditoria, ObjAprovechamientoRequest, int_aprovechamientoID);
                }
            }
            else
            {
                ObjRegistroAprovechamientoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_aprovechamientoID, "no existe datos para este ID aprovechamiento", true);
                Error = true;
            }
            SetearClases();

        }

        private void SetearClases()
        {
            if (ObjAprovechamientoAuditoria != null)
            {
                ObjAprovechamientoAuditoria = null;
            }

            if (ObjAprovechamientoInfoGralAA != null)
            {
                ObjAprovechamientoInfoGralAA = null;
            }

            if (ObjAprovechamientoInfoUsuarioRecurso != null)
            {
                ObjAprovechamientoInfoUsuarioRecurso = null;
            }

            if (ObjAprovechamientoInfoLugarAprovechamiento != null)
            {
                ObjAprovechamientoInfoLugarAprovechamiento = null;
            }

            if (ObjAprovechamientoInfoGralTramite != null)
            {
                ObjAprovechamientoInfoGralTramite = null;
            }

            if (ObjAprovechamientoVertice != null)
            {
                ObjAprovechamientoVertice = null;
            }

            if (ObjAprovechamientoEspecie != null)
            {
                ObjAprovechamientoEspecie = null;
            }

            if (ObjAprovechamientoRequest != null)
            {
                ObjAprovechamientoRequest = null;
            }

            if (ObjAprovechamientoIdentity != null)
            {
                ObjAprovechamientoIdentity = null;
            }

            if (ObjRegistroAprovechamientoDalc != null)
            {
                ObjRegistroAprovechamientoDalc = null;
            }

            if (ObjAprovechamientoDalc != null)
            {
                ObjAprovechamientoDalc = null;
            }

            if (ObjListasSUNL != null)
            {
                ObjListasSUNL = null;
            }

        }

    }
}
