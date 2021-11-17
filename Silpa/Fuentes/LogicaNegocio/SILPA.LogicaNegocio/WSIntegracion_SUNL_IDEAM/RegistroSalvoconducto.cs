using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SILPA.AccesoDatos.WSIntegracion_SUNL_IDEAM_Dalc;
using SILPA.AccesoDatos.Salvoconducto;
using SILPA.LogicaNegocio.Generico;


namespace SILPA.LogicaNegocio.WSIntegracion_SUNL_IDEAM
{
    public class RegistroSalvoconducto
    {
        #region  Variables del Servicio del IDEAM
        private WSregistrarSalvoconductosIDEAM.Auditoria ObjSalvoconductoAuditoria = null;
        private WSregistrarSalvoconductosIDEAM.InfoGralAA ObjSalvoconductoInfoGralAA = null;
        private WSregistrarSalvoconductosIDEAM.InfoUsuarioRecurso ObjSalvoconductooInfoUsuarioRecurso = null;
        private WSregistrarSalvoconductosIDEAM.InfoGralMovilizacion ObjInfoGralMovilizacion = null;
        private WSregistrarSalvoconductosIDEAM.SUN ObjSUN = null;
        private WSregistrarSalvoconductosIDEAM.InfoGeografica ObjInfoGeografica = null;
        private WSregistrarSalvoconductosIDEAM.Especie ObjEspecie = null;
        private WSregistrarSalvoconductosIDEAM.guardarMovilizacionRequest ObjMovilizacionesRequest = null;
        private WSregistrarSalvoconductosIDEAM.guardarMovilizacionResponse ObjResponse = null;
        private SILPA.LogicaNegocio.Generico.Listas ObjListasSUNL = null;
        #endregion

        #region Variables locales para obtener la informacion del Salvoconducto
        private RegistroSalvoconductoDalc ObjRegistroSalvoconductoDalc = null;
        private SalvoconductoNewDalc ObjSalvoconductoDalc = null;
        private SalvoconductoNewIdentity ObjSalvoconductoIdentity = null;
        #endregion


        private int int_LogID = 0;

        public RegistroSalvoconducto()
        {
            this.ObjRegistroSalvoconductoDalc = new RegistroSalvoconductoDalc();
        }


        public void ListarSalvoconductosDiario()
        {
            ObjRegistroSalvoconductoDalc = new RegistroSalvoconductoDalc();
            DataSet dsLstIdSalvoconducto = new DataSet();
            dsLstIdSalvoconducto = ObjRegistroSalvoconductoDalc.ObtenerIdSalvoconductos(true);
            int int_SalvoconductoID = 0;
            if (dsLstIdSalvoconducto.Tables[0].Rows.Count > 0)
            {
                int_LogID = ObjRegistroSalvoconductoDalc.InsertarCabeceraLogConsumoWSIDEAM(null, null, "Diario");

                if (int_LogID > 0)
                {
                    foreach (DataRow dr in dsLstIdSalvoconducto.Tables[0].Rows)
                    {
                        int_SalvoconductoID = Convert.ToInt32(dr["SALVOCONDUCTO_ID"]);
                        ObtenerDatosSalvoconducto(int_SalvoconductoID);
                    }
                }
            }
        }
        public void RegistrarSalvoconductoIDEAM(SalvoconductoNewIdentity vSalvoconductoNewIdentity)
        {
            int_LogID = ObjRegistroSalvoconductoDalc.InsertarCabeceraLogConsumoWSIDEAM(null, null, "Diario");
            if (int_LogID > 0)
            {
                ObtenerDatosSalvoconducto(vSalvoconductoNewIdentity);
            }
        }

        private void ObtenerDatosSalvoconducto(int int_SalvoconductoID)
        {
            ObjRegistroSalvoconductoDalc = new RegistroSalvoconductoDalc();
            ObjSalvoconductoIdentity = new SalvoconductoNewIdentity();
            ObjSalvoconductoDalc = new SalvoconductoNewDalc();
            bool Error = false;

            ObjSalvoconductoIdentity = ObjSalvoconductoDalc.ConsultaSalvoconductoXSalvoconductoID(int_SalvoconductoID);

            if (ObjSalvoconductoIdentity != null)
            {
                ObjMovilizacionesRequest = new WSregistrarSalvoconductosIDEAM.guardarMovilizacionRequest();
                ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "se obtiene Datos Salvoconducto", false);

                #region Tag <reg:Auditoria>
                ObjSalvoconductoAuditoria = new WSregistrarSalvoconductosIDEAM.Auditoria();
                ObjSalvoconductoAuditoria.SistemaSolicitud = "SUNL";
                //ObjSalvoconductoAuditoria.FechaHoraSolicitud = DateTime.Now.ToString();
                //ObjSalvoconductoAuditoria.IpSolicitud = "192.1.1.1";
                ObjSalvoconductoAuditoria.AutoridadAmbiental = "5000029864";
                ObjSalvoconductoAuditoria.Dependencia = "WebService";
                #endregion

                #region tag <reg:InfoGralAA>
                DataSet dsInfoAutAmb = new DataSet();
                ObjSalvoconductoInfoGralAA = new WSregistrarSalvoconductosIDEAM.InfoGralAA();
                ObjListasSUNL = new Listas();
                dsInfoAutAmb = ObjListasSUNL.ListarAutoridadesSUNL(ObjSalvoconductoIdentity.AutoridadEmisoraID);

                if (dsInfoAutAmb.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsInfoAutAmb.Tables[0].Rows)
                    {
                        ObjSalvoconductoInfoGralAA.CodigoAutoridad = dr["CODIGO_IDEAM"].ToString();
                        ObjSalvoconductoInfoGralAA.Dependencia = "WebService";
                        ObjSalvoconductoInfoGralAA.Funcionario = dr["AUT_NOMBRE"].ToString();
                        ObjSalvoconductoInfoGralAA.Correo = dr["AUT_CORREO"].ToString();
                        ObjSalvoconductoInfoGralAA.Telefono = dr["AUT_TELEFONO"].ToString().Substring(0, 7);
                        ObjMovilizacionesRequest.InfoGralAA = ObjSalvoconductoInfoGralAA;
                    }
                }
                else
                {
                    ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "no existe informacion de la autoridad ambiental emisiora del aprovechamiento", false);
                    Error = true;
                }
                #endregion

                #region tag <reg:InfoUsuarioRecurso>
                ObjSalvoconductooInfoUsuarioRecurso = new WSregistrarSalvoconductosIDEAM.InfoUsuarioRecurso();
                ObjSalvoconductooInfoUsuarioRecurso.CodigoTipoPersona = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.TipoPersonaIDEAM;
                ObjSalvoconductooInfoUsuarioRecurso.CodigoTipoIdentificacion = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.TipoIdentificacionIDEAM;
                ObjSalvoconductooInfoUsuarioRecurso.Identificacion = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.NumeroIdentificacion;
                ObjSalvoconductooInfoUsuarioRecurso.RazonSocial = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.RazonSocial;
                ObjSalvoconductooInfoUsuarioRecurso.PrimerNombre = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.PrimerNombre;
                ObjSalvoconductooInfoUsuarioRecurso.SegundoNombre = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.SegundoNombre;
                ObjSalvoconductooInfoUsuarioRecurso.PrimerApellido = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.PrimerApellido;
                ObjSalvoconductooInfoUsuarioRecurso.SegundoApellido = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.SegundoApellido;
                ObjSalvoconductooInfoUsuarioRecurso.CorreoElectronico = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.CorreoElectronico;
                ObjSalvoconductooInfoUsuarioRecurso.TelefonoFijo = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.Telefono != string.Empty ? ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.Telefono.Substring(0, 7) : "No registra";
                ObjSalvoconductooInfoUsuarioRecurso.Celular = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.Celular != string.Empty ? ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.Celular.Substring(0, 10): "No registra";
                ObjSalvoconductooInfoUsuarioRecurso.Direccion = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.DireccionPersona.DireccionPersona;
                ObjSalvoconductooInfoUsuarioRecurso.CodigoDepartamento = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.DireccionPersona.DepartamentoId.ToString().PadLeft(2, '0');
                ObjSalvoconductooInfoUsuarioRecurso.CodigoMunicipio = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.DireccionPersona.MunicipioId.ToString().PadLeft(5, '0');
                ObjMovilizacionesRequest.InfoUsuarioRecurso = ObjSalvoconductooInfoUsuarioRecurso;
                #endregion

                #region tag <reg:InfoGralMovilizacion>
                ObjInfoGralMovilizacion = new WSregistrarSalvoconductosIDEAM.InfoGralMovilizacion();
                ObjInfoGralMovilizacion.NumeroActoAdministrativoMovilizacion = ObjSalvoconductoIdentity.CodigoSeguridad; //PENDIENTE REVISION 05/09/2019
                ObjInfoGralMovilizacion.FinalidadRecurso = ObjSalvoconductoIdentity.CodigoIdeamFinalidadRecurso;
                ObjInfoGralMovilizacion.NumeroSUN = ObjSalvoconductoIdentity.Numero;
                ObjInfoGralMovilizacion.FechaExpedicionSUN = ObjSalvoconductoIdentity.FechaExpedicion.ToString("dd/MM/yyyy");
                ObjInfoGralMovilizacion.FechaVencimientoSUN = ObjSalvoconductoIdentity.FechaFinalVigencia.HasValue ? ObjSalvoconductoIdentity.FechaFinalVigencia.Value.ToString("dd/MM/yyyy") : string.Empty;
                ObjInfoGralMovilizacion.CodigoTipoSUN = ObjSalvoconductoIdentity.CodigoIdeamTipoSalvoconducto;

                if (string.IsNullOrEmpty(ObjSalvoconductoIdentity.CodigoIdeamTipoSalvoconducto))
                {
                    ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "no existe datos del IDEAM para el tipo Salvoconducto", false);
                    Error = true;
                }
                else
                {
                    if (ObjSalvoconductoIdentity.TipoSalvoconductoID == 2) //si es removilizacion se asigna el salvoconducto anterior
                    {
                        if (ObjSalvoconductoIdentity.LstSalvoconductoAnterior != null)
                        {
                            ObjInfoGralMovilizacion.NumeroSUNAnterior = ObjSalvoconductoIdentity.LstSalvoconductoAnterior.FirstOrDefault().Numero;
                        }
                        else
                        {
                            ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "no existe el salvoconducto anterior para esta removilizacion ", false);
                            Error = true;
                        }
                    }
                }



                if (ObjSalvoconductoIdentity.LstTransporte != null && ObjSalvoconductoIdentity.LstTransporte.Count > 0)
                {
                    ObjInfoGralMovilizacion.CodigoMedioTransporte = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().CodigoIdeamModoTransporte;
                    //ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().CodigoIdeamTipoTransporte;
                    //ObjInfoGralMovilizacion.Estado = ObjSalvoconductoIdentity.Estado;
                    ObjMovilizacionesRequest.InfoGralMovilizacion = ObjInfoGralMovilizacion;
                #endregion

                    #region tag <reg:SUN>
                    ObjSUN = new WSregistrarSalvoconductosIDEAM.SUN();
                    ObjSUN.NombreConductor = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().NombreTransportador;
                    ObjSUN.CodigoTipoDocumento = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().TipoIdentificacionTransportadorIDEAM;
                    ObjSUN.NumeroDocumento = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().NumeroIdentificacionTransportador;

                    if (string.IsNullOrEmpty(ObjSUN.CodigoTipoVehiculo = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().CodigoIdeamTipoTransporte))
                    {
                        ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "no existe datos del IDEAM para el tipo transporte ", false);
                        Error = true;
                    }
                    else
                    {
                        ObjSUN.CodigoTipoVehiculo = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().CodigoIdeamTipoTransporte;
                    }
                    ObjSUN.PlacaVehiculo = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().NumeroIdentificacionMedioTransporte;
                    ObjMovilizacionesRequest.SUN = ObjSUN;
                }
                else
                {
                    ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "no existe datos del transporte para este ID Salvoconducto", false);
                    Error = true;
                }
                    #endregion

                #region tag <reg:InfoGeografica>
                if (ObjSalvoconductoIdentity.LstRuta != null && ObjSalvoconductoIdentity.LstRuta.Count > 0)
                {
                    ObjInfoGeografica = new WSregistrarSalvoconductosIDEAM.InfoGeografica();
                    ObjInfoGeografica.CodigoDepartamentoOrigenProducto = ObjSalvoconductoIdentity.LstRuta.Where(x => x.IdOrigenDestino == 1).FirstOrDefault().DepartamentoID.ToString().PadLeft(2, '0');
                    ObjInfoGeografica.CodigoMunicipioOrigenProducto = ObjSalvoconductoIdentity.LstRuta.Where(x => x.IdOrigenDestino == 1).FirstOrDefault().MunicipioID.ToString().PadLeft(5, '0');
                    ObjInfoGeografica.CodigoDepartamentoDestinoProducto = ObjSalvoconductoIdentity.LstRuta.Where(x => x.IdOrigenDestino == 2).FirstOrDefault().DepartamentoID.ToString().PadLeft(2, '0');
                    ObjInfoGeografica.CodigoMunicipioDestinoProducto = ObjSalvoconductoIdentity.LstRuta.Where(x => x.IdOrigenDestino == 2).FirstOrDefault().MunicipioID.ToString().PadLeft(5, '0');
                    ObjMovilizacionesRequest.InfoGeografica = ObjInfoGeografica;
                }
                else
                {
                    ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "no existe datos de la ruta de desplazamiento para este ID Salvoconducto", false);
                    Error = true;
                }
                #endregion

                #region tag <reg:EspeciesAmparadasSalvoconducto> - <reg:Especie>
                if (ObjSalvoconductoIdentity.LstEspecimen.Count > 0)
                {
                    int x = 0;
                    ObjMovilizacionesRequest.EspeciesAmparadasSalvoconducto = new WSregistrarSalvoconductosIDEAM.Especie[ObjSalvoconductoIdentity.LstEspecimen.Count];
                    foreach (var Especies in ObjSalvoconductoIdentity.LstEspecimen)
                    {
                        ObjEspecie = new WSregistrarSalvoconductosIDEAM.Especie();

                        if (string.IsNullOrEmpty(Especies.CodigoIdeamEspecie))
                        {
                            ObjEspecie.CodigoNombreCientifico = "1000029171";
                        }
                        else
                        {
                            ObjEspecie.CodigoNombreCientifico = Especies.CodigoIdeamEspecie;
                        }
                        if (string.IsNullOrEmpty(Especies.NombreComunEspecie))
                        {
                            ObjEspecie.NombreComun = Especies.NombreEspecie;
                        }
                        else
                        {
                            ObjEspecie.NombreComun = Especies.NombreComunEspecie;
                        }


                        ObjEspecie.CodigoClaseRecurso = Especies.CodigoIdeamClaseRecurso;
                        ObjEspecie.CodigoUnidadMedida = Especies.CodigoIdeamUnidadMedida;


                        if (Especies.CodigoIdeamClaseRecurso == "MA") //flora maderable
                        {
                            ObjEspecie.CodigoTipoProductoMaderable = Especies.CodigoIdeamClaseProducto;
                            ObjEspecie.CodigoTipoProductoNoMaderable = string.Empty;
                            ObjEspecie.VolumenBrutoMovidoMaderable = Especies.VolumenBruto.ToString(); // AJUSTE REUNION 05/09/2019
                            ObjEspecie.VolumenBrutoMovidoMaderable = Especies.Volumen.ToString();

                            if (Especies.CodigoIdeamUnidadMedida != "M3")
                            {
                                ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "La unidad de medida para la flora maderable debe ser Metros Cubicos M3 para este ID Salvoconducto ID especie:" + Especies.EspecieSalvoconductoID, false);
                                Error = true;
                            }

                            if (Especies.CodigoIdeamClaseProducto == "RLLZ")
                            {
                                if (string.IsNullOrEmpty(Especies.CodigoIdeamTipoProducto))
                                {
                                    ObjEspecie.CodigoClaseProductoMaderableRollizo = "NS";
                                }
                                else
                                {
                                    ObjEspecie.CodigoClaseProductoMaderableRollizo = Especies.CodigoIdeamTipoProducto;
                                }
                            }
                            else if (Especies.CodigoIdeamClaseProducto == "ASRR")
                            {
                                if (string.IsNullOrEmpty(Especies.CodigoIdeamTipoProducto))
                                {
                                    ObjEspecie.CodigoClaseProductoMaderableAserrado = "NO_IDN";
                                }
                                else
                                {
                                    ObjEspecie.CodigoClaseProductoMaderableAserrado = Especies.CodigoIdeamTipoProducto;
                                }
                            }
                        }

                        if (Especies.CodigoIdeamClaseRecurso == "NM") //flora no maderable
                        {
                            ObjEspecie.CodigoTipoProductoNoMaderable = Especies.CodigoIdeamTipoProducto;
                            ObjEspecie.VolumenBrutoMovidoNoMaderable = Especies.VolumenBruto.ToString();
                        }

                        ObjEspecie.PorcentajeDesperdicio = "0.1";//CODIGO MACHETE REUNION 5 SEPTIEMBRE 2019 POR IDEAM
                        ObjEspecie.VolumenDesperdicio = "0.1";//CODIGO MACHETE
                        ObjEspecie.VolumenDisponibleMover = "0.1";// CODIGO MACHETE

                        ObjMovilizacionesRequest.EspeciesAmparadasSalvoconducto[x] = ObjEspecie;
                        x += 1;
                    }
                }
                else
                {
                    ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "no existe datos de la especies para este ID Salvoconducto", false);
                    Error = true;
                }
                #endregion

            }
            else
            {
                ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "no existe datos para este ID Salvocondcuto", true);
                Error = true;
            }

            if (!Error)
            {
                GuardarSalvocondcuto(ObjSalvoconductoAuditoria, ObjMovilizacionesRequest, int_SalvoconductoID);
            }

            SetearClases();

        }
        private void ObtenerDatosSalvoconducto(SalvoconductoNewIdentity vSalvoconductoNewIdentity)
        {
            ObjRegistroSalvoconductoDalc = new RegistroSalvoconductoDalc();
            ObjSalvoconductoDalc = new SalvoconductoNewDalc();
            ObjSalvoconductoIdentity = vSalvoconductoNewIdentity;
            bool Error = false;
            if (vSalvoconductoNewIdentity != null)
            {
                ObjMovilizacionesRequest = new WSregistrarSalvoconductosIDEAM.guardarMovilizacionRequest();
                ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, vSalvoconductoNewIdentity.SalvoconductoID, "se obtiene Datos Salvoconducto", false);

                #region Tag <reg:Auditoria>
                ObjSalvoconductoAuditoria = new WSregistrarSalvoconductosIDEAM.Auditoria();
                ObjSalvoconductoAuditoria.SistemaSolicitud = "SUNL";
                //ObjSalvoconductoAuditoria.FechaHoraSolicitud = DateTime.Now.ToString();
                //ObjSalvoconductoAuditoria.IpSolicitud = "192.1.1.1";
                ObjSalvoconductoAuditoria.AutoridadAmbiental = "5000029864";
                ObjSalvoconductoAuditoria.Dependencia = "WebService";
                #endregion

                #region tag <reg:InfoGralAA>
                DataSet dsInfoAutAmb = new DataSet();
                ObjSalvoconductoInfoGralAA = new WSregistrarSalvoconductosIDEAM.InfoGralAA();
                ObjListasSUNL = new Listas();
                dsInfoAutAmb = ObjListasSUNL.ListarAutoridadesSUNL(ObjSalvoconductoIdentity.AutoridadEmisoraID);

                if (dsInfoAutAmb.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsInfoAutAmb.Tables[0].Rows)
                    {
                        ObjSalvoconductoInfoGralAA.CodigoAutoridad = dr["CODIGO_IDEAM"].ToString();
                        ObjSalvoconductoInfoGralAA.Dependencia = "WebService";
                        ObjSalvoconductoInfoGralAA.Funcionario = dr["AUT_NOMBRE"].ToString();
                        ObjSalvoconductoInfoGralAA.Correo = dr["AUT_CORREO"].ToString();
                        ObjSalvoconductoInfoGralAA.Telefono = dr["AUT_TELEFONO"].ToString() != string.Empty ? dr["AUT_TELEFONO"].ToString().PadLeft(7,'0').Substring(0, 7) : "No registra";
                        ObjMovilizacionesRequest.InfoGralAA = ObjSalvoconductoInfoGralAA;
                    }
                }
                else
                {
                    ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, vSalvoconductoNewIdentity.SalvoconductoID, "no existe informacion de la autoridad ambiental emisiora del aprovechamiento", false);
                    Error = true;
                }
                #endregion

                #region tag <reg:InfoUsuarioRecurso>
                ObjSalvoconductooInfoUsuarioRecurso = new WSregistrarSalvoconductosIDEAM.InfoUsuarioRecurso();
                ObjSalvoconductooInfoUsuarioRecurso.CodigoTipoPersona = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.TipoPersonaIDEAM;
                ObjSalvoconductooInfoUsuarioRecurso.CodigoTipoIdentificacion = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.TipoIdentificacionIDEAM;
                ObjSalvoconductooInfoUsuarioRecurso.Identificacion = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.NumeroIdentificacion;
                ObjSalvoconductooInfoUsuarioRecurso.RazonSocial = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.RazonSocial;
                ObjSalvoconductooInfoUsuarioRecurso.PrimerNombre = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.PrimerNombre;
                ObjSalvoconductooInfoUsuarioRecurso.SegundoNombre = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.SegundoNombre;
                ObjSalvoconductooInfoUsuarioRecurso.PrimerApellido = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.PrimerApellido;
                ObjSalvoconductooInfoUsuarioRecurso.SegundoApellido = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.SegundoApellido;
                ObjSalvoconductooInfoUsuarioRecurso.CorreoElectronico = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.CorreoElectronico;
                ObjSalvoconductooInfoUsuarioRecurso.TelefonoFijo = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.Telefono != string.Empty ? ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.Telefono.PadLeft(7,'0').Substring(0, 7) : "No registra";
                ObjSalvoconductooInfoUsuarioRecurso.Celular = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.Celular != string.Empty ? ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.Celular.PadLeft(10, '0').Substring(0, 10) : "No registra";
                ObjSalvoconductooInfoUsuarioRecurso.Direccion = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.DireccionPersona.DireccionPersona;
                ObjSalvoconductooInfoUsuarioRecurso.CodigoDepartamento = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.DireccionPersona.DepartamentoId.ToString().PadLeft(2, '0');
                ObjSalvoconductooInfoUsuarioRecurso.CodigoMunicipio = ObjSalvoconductoIdentity.SolicitanteTitularPersonaIdentity.DireccionPersona.MunicipioId.ToString().PadLeft(5, '0');
                ObjMovilizacionesRequest.InfoUsuarioRecurso = ObjSalvoconductooInfoUsuarioRecurso;
                #endregion

                #region tag <reg:InfoGralMovilizacion>
                ObjInfoGralMovilizacion = new WSregistrarSalvoconductosIDEAM.InfoGralMovilizacion();
                if (ObjSalvoconductoIdentity.LstAprovechamientoOrigen.Count > 1)
                {
                    string aprovechamientosAsociados = string.Empty;
                    foreach (AprovechamientoOrigen itaproOrigen in ObjSalvoconductoIdentity.LstAprovechamientoOrigen)
                    {
                        aprovechamientosAsociados += "," + itaproOrigen.numeroAprovechamiento;
                    }
                    ObjInfoGralMovilizacion.NumeroActoAdministrativoMovilizacion = aprovechamientosAsociados.Remove(0, 1);
                }
                else
                {
                    if (ObjSalvoconductoIdentity.LstAprovechamientoOrigen.Count > 0)
                    {
                        ObjInfoGralMovilizacion.NumeroActoAdministrativoMovilizacion = ObjSalvoconductoIdentity.LstAprovechamientoOrigen.First().numeroAprovechamiento;
                    }
                    else
                    {
                        ObjInfoGralMovilizacion.NumeroActoAdministrativoMovilizacion = "No registra";
                    }
                }
                
                ObjInfoGralMovilizacion.FinalidadRecurso = ObjSalvoconductoIdentity.CodigoIdeamFinalidadRecurso;
                ObjInfoGralMovilizacion.NumeroSUN = ObjSalvoconductoIdentity.Numero;
                ObjInfoGralMovilizacion.FechaExpedicionSUN = ObjSalvoconductoIdentity.FechaExpedicion.ToString("dd/MM/yyyy");
                ObjInfoGralMovilizacion.FechaVencimientoSUN = ObjSalvoconductoIdentity.FechaFinalVigencia.HasValue ? ObjSalvoconductoIdentity.FechaFinalVigencia.Value.ToString("dd/MM/yyyy") : string.Empty;
                ObjInfoGralMovilizacion.CodigoTipoSUN = ObjSalvoconductoIdentity.CodigoIdeamTipoSalvoconducto;

                if (string.IsNullOrEmpty(ObjSalvoconductoIdentity.CodigoIdeamTipoSalvoconducto))
                {
                    ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, vSalvoconductoNewIdentity.SalvoconductoID, "no existe datos del IDEAM para el tipo Salvoconducto", false);
                    Error = true;
                }
                else
                {
                    if (ObjSalvoconductoIdentity.TipoSalvoconductoID == 2) //si es removilizacion se asigna el salvoconducto anterior
                    {
                        if (ObjSalvoconductoIdentity.LstSalvoconductoAnterior != null)
                        {
                            ObjInfoGralMovilizacion.NumeroSUNAnterior = ObjSalvoconductoIdentity.LstSalvoconductoAnterior.FirstOrDefault().Numero;
                        }
                        else
                        {
                            ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, vSalvoconductoNewIdentity.SalvoconductoID, "no existe el salvoconducto anterior para esta removilizacion ", false);
                            Error = true;
                        }
                    }
                }



                if (ObjSalvoconductoIdentity.LstTransporte != null && ObjSalvoconductoIdentity.LstTransporte.Count > 0)
                {
                    ObjInfoGralMovilizacion.CodigoMedioTransporte = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().CodigoIdeamModoTransporte;
                    //ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().CodigoIdeamTipoTransporte;
                    //ObjInfoGralMovilizacion.Estado = ObjSalvoconductoIdentity.Estado;
                    ObjMovilizacionesRequest.InfoGralMovilizacion = ObjInfoGralMovilizacion;
                #endregion

                #region tag <reg:SUN>
                    ObjSUN = new WSregistrarSalvoconductosIDEAM.SUN();
                    ObjSUN.NombreConductor = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().NombreTransportador;
                    ObjSUN.CodigoTipoDocumento = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().TipoIdentificacionTransportadorIDEAM;
                    ObjSUN.NumeroDocumento = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().NumeroIdentificacionTransportador;

                    if (string.IsNullOrEmpty(ObjSUN.CodigoTipoVehiculo = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().CodigoIdeamTipoTransporte))
                    {
                        ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, vSalvoconductoNewIdentity.SalvoconductoID, "no existe datos del IDEAM para el tipo transporte ", false);
                        Error = true;
                    }
                    else
                    {
                        ObjSUN.CodigoTipoVehiculo = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().CodigoIdeamTipoTransporte;
                    }
                    ObjSUN.PlacaVehiculo = ObjSalvoconductoIdentity.LstTransporte.FirstOrDefault().NumeroIdentificacionMedioTransporte;
                    ObjMovilizacionesRequest.SUN = ObjSUN;
                }
                else
                {
                    ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, vSalvoconductoNewIdentity.SalvoconductoID, "no existe datos del transporte para este ID Salvoconducto", false);
                    Error = true;
                }
                    #endregion

                #region tag <reg:InfoGeografica>
                if (ObjSalvoconductoIdentity.LstRuta != null && ObjSalvoconductoIdentity.LstRuta.Count > 0)
                {
                    ObjInfoGeografica = new WSregistrarSalvoconductosIDEAM.InfoGeografica();
                    ObjInfoGeografica.CodigoDepartamentoOrigenProducto = ObjSalvoconductoIdentity.LstRuta.Where(x => x.IdOrigenDestino == 1).FirstOrDefault().DepartamentoID.ToString().PadLeft(2, '0');
                    ObjInfoGeografica.CodigoMunicipioOrigenProducto = ObjSalvoconductoIdentity.LstRuta.Where(x => x.IdOrigenDestino == 1).FirstOrDefault().MunicipioID.ToString().PadLeft(5, '0');
                    ObjInfoGeografica.CodigoDepartamentoDestinoProducto = ObjSalvoconductoIdentity.LstRuta.Where(x => x.IdOrigenDestino == 2).FirstOrDefault().DepartamentoID.ToString().PadLeft(2, '0');
                    ObjInfoGeografica.CodigoMunicipioDestinoProducto = ObjSalvoconductoIdentity.LstRuta.Where(x => x.IdOrigenDestino == 2).FirstOrDefault().MunicipioID.ToString().PadLeft(5, '0');
                    ObjMovilizacionesRequest.InfoGeografica = ObjInfoGeografica;
                }
                else
                {
                    ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, vSalvoconductoNewIdentity.SalvoconductoID, "no existe datos de la ruta de desplazamiento para este ID Salvoconducto", false);
                    Error = true;
                }
                #endregion

                #region tag <reg:EspeciesAmparadasSalvoconducto> - <reg:Especie>
                if (ObjSalvoconductoIdentity.LstEspecimen.Count > 0)
                {
                    int x = 0;
                    ObjMovilizacionesRequest.EspeciesAmparadasSalvoconducto = new WSregistrarSalvoconductosIDEAM.Especie[ObjSalvoconductoIdentity.LstEspecimen.Count];
                    foreach (var Especies in ObjSalvoconductoIdentity.LstEspecimen)
                    {
                        ObjEspecie = new WSregistrarSalvoconductosIDEAM.Especie();

                        if (string.IsNullOrEmpty(Especies.CodigoIdeamEspecie))
                        {
                            ObjEspecie.CodigoNombreCientifico = "1000029171";
                        }
                        else
                        {
                            ObjEspecie.CodigoNombreCientifico = Especies.CodigoIdeamEspecie;
                        }
                        if (string.IsNullOrEmpty(Especies.NombreComunEspecie))
                        {
                            ObjEspecie.NombreComun = Especies.NombreEspecie;
                        }
                        else
                        {
                            ObjEspecie.NombreComun = Especies.NombreComunEspecie;
                        }


                        ObjEspecie.CodigoClaseRecurso = Especies.CodigoIdeamClaseRecurso;
                        ObjEspecie.CodigoUnidadMedida = Especies.CodigoIdeamUnidadMedida;


                        if (Especies.CodigoIdeamClaseRecurso == "MA") //flora maderable
                        {
                            ObjEspecie.CodigoTipoProductoMaderable = Especies.CodigoIdeamClaseProducto;
                            ObjEspecie.CodigoTipoProductoNoMaderable = string.Empty;
                            //ObjEspecie.VolumenBrutoMovidoMaderable = Especies.VolumenBruto.ToString(); // AJUSTE REUNION 05/09/2019
                            ObjEspecie.VolumenBrutoMovidoMaderable = Especies.Volumen.ToString();

                            if (Especies.CodigoIdeamUnidadMedida != "M3")
                            {
                                ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, vSalvoconductoNewIdentity.SalvoconductoID, "La unidad de medida para la flora maderable debe ser Metros Cubicos M3 para este ID Salvoconducto ID especie:" + Especies.EspecieSalvoconductoID, false);
                                Error = true;
                            }

                            if (Especies.CodigoIdeamClaseProducto == "RLLZ")
                            {
                                if (string.IsNullOrEmpty(Especies.CodigoIdeamTipoProducto))
                                {
                                    ObjEspecie.CodigoClaseProductoMaderableRollizo = "NS";
                                }
                                else
                                {
                                    ObjEspecie.CodigoClaseProductoMaderableRollizo = Especies.CodigoIdeamTipoProducto;
                                }
                            }
                            else if (Especies.CodigoIdeamClaseProducto == "ASRR")
                            {
                                if (string.IsNullOrEmpty(Especies.CodigoIdeamTipoProducto))
                                {
                                    ObjEspecie.CodigoClaseProductoMaderableAserrado = "NO_IDN";
                                }
                                else
                                {
                                    ObjEspecie.CodigoClaseProductoMaderableAserrado = Especies.CodigoIdeamTipoProducto;
                                }
                            }
                        }

                        if (Especies.CodigoIdeamClaseRecurso == "NM") //flora no maderable
                        {
                            ObjEspecie.CodigoTipoProductoNoMaderable = Especies.CodigoIdeamTipoProducto;
                            ObjEspecie.VolumenBrutoMovidoNoMaderable = Especies.Volumen.ToString();
                        }

                        ObjEspecie.PorcentajeDesperdicio = "0.01";//CODIGO MACHETE REUNION 5 SEPTIEMBRE 2019 POR IDEAM
                        ObjEspecie.VolumenDesperdicio = "0.01";//CODIGO MACHETE
                        ObjEspecie.VolumenDisponibleMover = "0.01";// CODIGO MACHETE

                        ObjMovilizacionesRequest.EspeciesAmparadasSalvoconducto[x] = ObjEspecie;
                        x += 1;
                    }
                }
                else
                {
                    ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, vSalvoconductoNewIdentity.SalvoconductoID, "no existe datos de la especies para este ID Salvoconducto", false);
                    Error = true;
                }
                #endregion

            }
            else
            {
                ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, vSalvoconductoNewIdentity.SalvoconductoID, "no existe datos para este ID Salvocondcuto", true);
                Error = true;
            }

            if (!Error)
            {
                GuardarSalvocondcuto(ObjSalvoconductoAuditoria, ObjMovilizacionesRequest, vSalvoconductoNewIdentity.SalvoconductoID);
            }

            SetearClases();
        }

        private void GuardarSalvocondcuto(WSregistrarSalvoconductosIDEAM.Auditoria ObjSalvoconductoAuditoria, WSregistrarSalvoconductosIDEAM.guardarMovilizacionRequest ObjMovilizacionesRequest, int int_SalvoconductoID)
        {
            string CodigoError = string.Empty;
            string IdentificadorCargue = string.Empty;
            string[] MensajeError;


            try
            {
                ObjResponse = new WSregistrarSalvoconductosIDEAM.guardarMovilizacionResponse();
                WSregistrarSalvoconductosIDEAM.RegistrarMovilizacionPortTypeClient ObjClient = new WSregistrarSalvoconductosIDEAM.RegistrarMovilizacionPortTypeClient();
                ObjClient.guardarMovilizacion(ObjSalvoconductoAuditoria, ObjMovilizacionesRequest.InfoGralAA, ObjMovilizacionesRequest.InfoUsuarioRecurso, ObjMovilizacionesRequest.InfoGralMovilizacion, ObjMovilizacionesRequest.SUN, ObjMovilizacionesRequest.InfoGeografica, ObjMovilizacionesRequest.EspeciesAmparadasSalvoconducto, out CodigoError, out MensajeError, out IdentificadorCargue);
                ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "Consumo Servicio Salvoconducto Existoso : " + IdentificadorCargue, true);

                if ((string.IsNullOrEmpty(CodigoError) && CodigoError == "0") && Convert.ToInt32(MensajeError.Length) == 0)
                {
                    ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "Consumo Servicio Salvoconducto Existoso : " + IdentificadorCargue, true);
                }
                else if (MensajeError.Length > 0)
                {
                    foreach (string StrError in MensajeError)
                    {
                        ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "Consumo Servicio Detalle Error: " + StrError, false);
                    }
                }
            }
            catch (Exception ex)
            {
                ObjRegistroSalvoconductoDalc.InsertarDetalleLogConsumoWSIDEAM(int_LogID, int_SalvoconductoID, "Consumo Servicio Detalle Error: " + ex.Message, false);

            }
            finally
            {
                SetearClases();
            }

        }

        private void SetearClases()
        {

            if (ObjSalvoconductoAuditoria != null)
            {
                ObjSalvoconductoAuditoria = null;
            }



            if (ObjSalvoconductoInfoGralAA != null)
            {
                ObjSalvoconductoInfoGralAA = null;
            }


            if (ObjSalvoconductooInfoUsuarioRecurso != null)
            {
                ObjSalvoconductooInfoUsuarioRecurso = null;
            }


            if (ObjInfoGralMovilizacion != null)
            {
                ObjInfoGralMovilizacion = null;
            }


            if (ObjSUN != null)
            {
                ObjSUN = null;
            }


            if (ObjInfoGeografica != null)
            {
                ObjInfoGeografica = null;
            }


            if (ObjEspecie != null)
            {
                ObjEspecie = null;
            }


            if (ObjMovilizacionesRequest != null)
            {
                ObjMovilizacionesRequest = null;
            }


            if (ObjListasSUNL != null)
            {
                ObjListasSUNL = null;
            }



            if (ObjRegistroSalvoconductoDalc != null)
            {
                ObjRegistroSalvoconductoDalc = null;
            }


            if (ObjSalvoconductoDalc != null)
            {
                ObjSalvoconductoDalc = null;
            }


            if (ObjSalvoconductoIdentity != null)
            {
                ObjSalvoconductoIdentity = null;
            }

        }


    }
}
