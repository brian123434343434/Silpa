using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.LogicaNegocio.Liquidacion.Entidades;
using System.Data;
using SILPA.Comun;
using SILPA.LogicaNegocio.Formularios;
using System.IO;
using SILPA.LogicaNegocio.DAA;
using SILPA.LogicaNegocio.Liquidacion;
using SILPA.LogicaNegocio.Liquidacion.Enum;
using System.Configuration;
using System.Globalization;
using SILPA.AccesoDatos.Correspondencia;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.DAA;
using SILPA.LogicaNegocio.Generico;
using Silpa.Workflow;
using SILPA.LogicaNegocio.Utilidad;
using SoftManagement.Log;

namespace SILPA.Servicios.Liquidacion
{
    public class AutoliquidacionFachada
    {

        #region Metodos Privados

            /// <summary>
            /// Leer la imagen de firma de ANLA y retornarla en un arreglo de bits
            /// </summary>
            /// <returns>Arrglo de bytes con la imagen de la firma</returns>
            private byte[] LeerFirmaANLA()
            {
                byte[] objFirma = null;
                string strRutaArchivo = null;
                
                //Cargar ruta archivo
                strRutaArchivo = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + ConfigurationManager.AppSettings["LiquidacionImagenFirmaANLA"].ToString();

                //Verificar que el archivo exista y cargarlo
                if (File.Exists(strRutaArchivo))
                {
                    objFirma = File.ReadAllBytes(strRutaArchivo);
                }
                
                return objFirma;
            }

            /// <summary>
            /// Cargar la informacion base de la liquidacion en una tabla
            /// </summary>
            /// <param name="p_objLiquidacion">AutoliquidacionLiquidacionEntity con la informacion de la liquidacion</param>
            /// <returns>DataTable con la informacion de la liquidacion</returns>
            private DataTable ObtenerDataSetLiquidacion(AutoliquidacionLiquidacionEntity p_objLiquidacion)
            {
                Autoliquidacion objAutoliquidacion = null;
                DataSet objInformacionRadicacion = null;
                DataTable objInformacion = null;
                DataRow objFila = null;
                SILPA.LogicaNegocio.DAA.DAA objDaa = null;
                string strXMLFormulario = "";
                StringReader objReader = null;
                DataSet objFormulario = null;

                //Verificar que el objeto no sea nulo
                if (p_objLiquidacion != null)
                {
                    //Crear tabla
                    objInformacion = new DataTable();
                    objInformacion.TableName = "LIQUIDACION";
                    objInformacion.Columns.Add("LIQUIDACIONID", Type.GetType("System.Int32"));
                    objInformacion.Columns.Add("RESOLUCION", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_PERMISOS", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_LIQUIDACION", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_TOTAL_LIQUIDACION", Type.GetType("System.String"));
                    objInformacion.Columns.Add("EXPEDIENTE", Type.GetType("System.String"));
                    objInformacion.Columns.Add("FECHA", Type.GetType("System.String"));
                    objInformacion.Columns.Add("NUMERO_VITAL", Type.GetType("System.String"));
                    objInformacion.Columns.Add("REFERENCIA_PAGO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("SOLICITANTE", Type.GetType("System.String"));
                    objInformacion.Columns.Add("APODERADOS", Type.GetType("System.String"));
                    objInformacion.Columns.Add("CORREOS_APODERADOS", Type.GetType("System.String"));
                    objInformacion.Columns.Add("NUMERO_RADICADO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("FECHA_RADICADO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("SOLICITUD", Type.GetType("System.String"));
                    objInformacion.Columns.Add("TRAMITE", Type.GetType("System.String"));
                    objInformacion.Columns.Add("PROYECTO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("CORREO_SOLICITANTE", Type.GetType("System.String"));
                    objInformacion.Columns.Add("RESOLUCIONID", Type.GetType("System.Int32"));
                    objInformacion.Columns.Add("FIRMA", Type.GetType("System.Byte[]"));
                    objInformacion.Columns.Add("NOMBRE_FIRMA", Type.GetType("System.String"));
                    objInformacion.Columns.Add("CARGO_FIRMA", Type.GetType("System.String"));
                    objInformacion.Columns.Add("CONTIENE_PERMISOS", Type.GetType("System.Int16"));
                    objInformacion.Columns.Add("CONTIENE_PERMISOS_ANLA", Type.GetType("System.Int16"));
                    objInformacion.Columns.Add("VALOR_LIQUIDACION_LETRAS", Type.GetType("System.String"));
                    
                    //Consultar información radicación
                    objAutoliquidacion = new Autoliquidacion();
                    objInformacionRadicacion = objAutoliquidacion.ConsultarInformacionRadicacionNumeroVital(p_objLiquidacion.NumeroVITAL);

                    //Cargar datos
                    objFila = objInformacion.NewRow();
                    objFila["LIQUIDACIONID"] = p_objLiquidacion.LiquidacionID;
                    objFila["RESOLUCION"] = p_objLiquidacion.Resolucion;
                    objFila["VALOR_PERMISOS"] = p_objLiquidacion.ValorPermisos;
                    objFila["VALOR_LIQUIDACION"] = p_objLiquidacion.ValorLiquidacion;
                    objFila["VALOR_TOTAL_LIQUIDACION"] = p_objLiquidacion.ValorTotal;
                    objFila["EXPEDIENTE"] = p_objLiquidacion.Expediente;
                    objFila["FECHA"] = Convert.ToDateTime(p_objLiquidacion.Fecha).ToString("MMMM, dd", CultureInfo.CreateSpecificCulture("es-CO")) + " de " + Convert.ToDateTime(p_objLiquidacion.Fecha).ToString("yyyy");
                    objFila["NUMERO_VITAL"] = p_objLiquidacion.NumeroVITAL;
                    objFila["REFERENCIA_PAGO"] = p_objLiquidacion.ReferenciaPago;
                    objFila["SOLICITANTE"] = p_objLiquidacion.Solicitante;
                    objFila["RESOLUCIONID"] = p_objLiquidacion.ResolucionID;
                    objFila["FIRMA"] = this.LeerFirmaANLA();
                    objFila["NOMBRE_FIRMA"] = ConfigurationManager.AppSettings["LiquidacionNombreFirmaDocumento"].ToString();
                    objFila["CARGO_FIRMA"] = ConfigurationManager.AppSettings["LiquidacionCargoFirmaANLA"].ToString();
                    objFila["APODERADOS"] = "";
                    objFila["CORREOS_APODERADOS"] = "";
                    objFila["CORREO_SOLICITANTE"] = (objInformacionRadicacion.Tables["Solicitante"].Rows[0]["CORREO_ELECTRONICO"] != System.DBNull.Value && !string.IsNullOrEmpty(objInformacionRadicacion.Tables["Solicitante"].Rows[0]["CORREO_ELECTRONICO"].ToString()) ? objInformacionRadicacion.Tables["Solicitante"].Rows[0]["CORREO_ELECTRONICO"].ToString() : "-");
                    objFila["NUMERO_RADICADO"] = objInformacionRadicacion.Tables["Radicacion"].Rows[0]["NUMERO_RADICACION"].ToString();
                    objFila["FECHA_RADICADO"] = Convert.ToDateTime(objInformacionRadicacion.Tables["Radicacion"].Rows[0]["FECHA_RADICACION_AA"]).ToString("dd-MMMM-yyyy", new CultureInfo("es-CO"));
                    if (!p_objLiquidacion.PermisosANLA)
                    {
                        objFila["CONTIENE_PERMISOS"] = (p_objLiquidacion.Detalle0324.Permisos != null && p_objLiquidacion.Detalle0324.Permisos.Count > 0 ? 1 : 0);
                        objFila["CONTIENE_PERMISOS_ANLA"] = 0;
                    }
                    else
                    {
                        objFila["CONTIENE_PERMISOS"] = 0;
                        objFila["CONTIENE_PERMISOS_ANLA"] = 1;
                    }
                    objFila["VALOR_LIQUIDACION_LETRAS"] = Utilidades.NumeroALetras(decimal.Parse(p_objLiquidacion.ValorLiquidacion, NumberStyles.Currency).ToString()).ToUpper() + " MCTE";

                    //Consultar información solicitud
                    objDaa = new SILPA.LogicaNegocio.DAA.DAA();
                    strXMLFormulario = objDaa.ConsultarDatosFormulario(p_objLiquidacion.NumeroVITAL, "D");

                    //Cargar XML en DataSet
                    objReader = new StringReader(strXMLFormulario);
                    objFormulario = new DataSet();
                    objFormulario.ReadXml(objReader);

                    //Cargar datos de la solicitud
                    if (p_objLiquidacion.NumeroVITAL.StartsWith(ConfigurationManager.AppSettings["LiquidacionProcesoLicencia"]))
                    {
                        objFila["SOLICITUD"] = objFormulario.Tables[0].Rows[0]["Solicitud_de_liquidación_para_8498"].ToString(); 
                        objFila["TRAMITE"] = objFormulario.Tables[0].Rows[0]["Trámite_8499"].ToString();
                        objFila["PROYECTO"] = objFormulario.Tables[0].Rows[0]["Nombre_del_Proyecto_Obra_o_Actividad_8502"].ToString();    
                    }
                    else if (p_objLiquidacion.NumeroVITAL.StartsWith(ConfigurationManager.AppSettings["LiquidacionProcesoPermiso"]))
                    {
                        objFila["SOLICITUD"] = objFormulario.Tables[0].Rows[0][1].ToString();
                        objFila["TRAMITE"] = "Permiso";
                        objFila["PROYECTO"] = objFormulario.Tables[0].Rows[0]["Nombre_del_Permiso_Autorización_yo_Concesión_Ambiental_8736"].ToString();
                    }
                    else if (p_objLiquidacion.NumeroVITAL.StartsWith(ConfigurationManager.AppSettings["LiquidacionProcesoInstrumentos"]))
                    {
                        objFila["SOLICITUD"] = objFormulario.Tables[0].Rows[0]["Solicitud_de_liquidación_para_8798"].ToString();
                        objFila["TRAMITE"] = objFormulario.Tables[0].Rows[0]["Trámite_8799"].ToString();
                        objFila["PROYECTO"] = objFormulario.Tables[0].Rows[0]["Nombre_del_Proyecto_Obra_o_Actividad_8800"].ToString();
                    }

                    //Agrgar fila
                    objInformacion.Rows.Add(objFila);
                }

                return objInformacion;
            }


            /// <summary>
            /// Cargar la informacion de la liquidacion por ley 633
            /// </summary>
            /// <param name="p_objLiquidacion">AutoliquidacionLiquidacionEntity con la informacion de la liquidacion</param>
            /// <returns>DataTable con la informacion de la liquidacion</returns>
            private DataTable ObtenerDataSetLey633(AutoliquidacionLiquidacionEntity p_objLiquidacion)
            {
                DataTable objInformacion = null;
                DataRow objFila = null;

                //Verificar que el objeto no sea nulo
                if (p_objLiquidacion != null && p_objLiquidacion.Detalle633 != null)
                {
                    //Crear tabla
                    objInformacion = new DataTable();
                    objInformacion.TableName = "DETALLE_LIQUIDACION_633";
                    objInformacion.Columns.Add("LIQUIDACIONID", Type.GetType("System.Int32"));
                    objInformacion.Columns.Add("VALOR_PROYECTO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_SALARIO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("RELACION", Type.GetType("System.String"));
                    objInformacion.Columns.Add("TARIFA_APLICAR", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_TOTAL", Type.GetType("System.String"));

                    //Cargar datos
                    objFila = objInformacion.NewRow();
                    objFila["LIQUIDACIONID"] = p_objLiquidacion.LiquidacionID;
                    objFila["VALOR_PROYECTO"] = p_objLiquidacion.Detalle633.Valorproyecto;
                    objFila["VALOR_SALARIO"] = p_objLiquidacion.Detalle633.ValorSalario;
                    objFila["RELACION"] = p_objLiquidacion.Detalle633.Relacion;
                    objFila["TARIFA_APLICAR"] = p_objLiquidacion.Detalle633.TarifaAplicar;
                    objFila["VALOR_TOTAL"] = p_objLiquidacion.Detalle633.ValorTotal;

                    //Agrgar fila
                    objInformacion.Rows.Add(objFila);
                }

                return objInformacion;
            }


            /// <summary>
            /// Cargar la informacion de la liquidacion por resolucion 0324
            /// </summary>
            /// <param name="p_objLiquidacion">AutoliquidacionLiquidacionEntity con la informacion de la liquidacion</param>
            /// <returns>DataTable con la informacion de la liquidacion</returns>
            private DataTable ObtenerDataSetResolucion0324(AutoliquidacionLiquidacionEntity p_objLiquidacion)
            {
                DataTable objInformacion = null;
                DataRow objFila = null;

                //Verificar que el objeto no sea nulo
                if (p_objLiquidacion != null && p_objLiquidacion.Detalle0324 != null)
                {
                    //Crear tabla
                    objInformacion = new DataTable();
                    objInformacion.TableName = "DETALLE_LIQUIDACION_0324";
                    objInformacion.Columns.Add("LIQUIDACIONID", Type.GetType("System.Int32"));
                    objInformacion.Columns.Add("VALOR_SERVICIO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_ADMINISTRACION", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_TIQUETES", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_TOTAL", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_PERMISOS", Type.GetType("System.String"));
                    objInformacion.Columns.Add("NUMERO_TABLA", Type.GetType("System.String"));
                    objInformacion.Columns.Add("NOMBRE_MICROTABLA", Type.GetType("System.String"));

                    //Cargar datos
                    objFila = objInformacion.NewRow();
                    objFila["LIQUIDACIONID"] = p_objLiquidacion.LiquidacionID;
                    objFila["VALOR_SERVICIO"] = p_objLiquidacion.Detalle0324.ValorServicio;
                    objFila["VALOR_ADMINISTRACION"] = p_objLiquidacion.Detalle0324.ValorAdministracion;
                    objFila["VALOR_TIQUETES"] = p_objLiquidacion.Detalle0324.ValorTiquetes;
                    objFila["VALOR_TOTAL"] = p_objLiquidacion.Detalle0324.ValorTotal;
                    objFila["VALOR_PERMISOS"] = p_objLiquidacion.Detalle0324.ValorPermisos;
                    objFila["NUMERO_TABLA"] = p_objLiquidacion.Detalle0324.NumeroTabla;
                    objFila["NOMBRE_MICROTABLA"] = p_objLiquidacion.Detalle0324.NombreMicroTabla;

                    //Agrgar fila
                    objInformacion.Rows.Add(objFila);
                }

                return objInformacion;
            }


            /// <summary>
            /// Cargar la informacion de los permisos
            /// </summary>
            /// <param name="p_objLiquidacion">AutoliquidacionLiquidacionEntity con la informacion de la liquidacion</param>
            /// <returns>DataTable con la informacion de la liquidacion</returns>
            private DataTable ObtenerDataSetPermisos(AutoliquidacionLiquidacionEntity p_objLiquidacion)
            {
                DataTable objInformacion = null;
                DataRow objFila = null;

                //Verificar que el objeto no sea nulo
                if (p_objLiquidacion != null && p_objLiquidacion.Detalle0324 != null && p_objLiquidacion.Detalle0324.Permisos != null)
                {
                    //Crear tabla
                    objInformacion = new DataTable();
                    objInformacion.TableName = "PERMISOS";
                    objInformacion.Columns.Add("LIQUIDACIONID", Type.GetType("System.Int32"));
                    objInformacion.Columns.Add("PERMISO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("NUMERO_PERMISOS", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_ADMINISTRACION", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_SERVICIO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_TOTAL", Type.GetType("System.String"));
                    objInformacion.Columns.Add("CORPORACION", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_TOTAL_LETRAS", Type.GetType("System.String"));

                    //Cargar datos
                    foreach (AutoliquidacionDatosPermisosEntity objPermiso in p_objLiquidacion.Detalle0324.Permisos)
                    {
                        objFila = objInformacion.NewRow();
                        objFila["LIQUIDACIONID"] = p_objLiquidacion.LiquidacionID;
                        objFila["PERMISO"] = objPermiso.Permiso;
                        objFila["NUMERO_PERMISOS"] = objPermiso.NumeroPermisos;
                        objFila["VALOR_ADMINISTRACION"] = objPermiso.ValorAdministracion;
                        objFila["VALOR_SERVICIO"] = objPermiso.ValorServicio;
                        objFila["VALOR_TOTAL"] = objPermiso.ValorTotal;
                        objFila["CORPORACION"] = objPermiso.EntidadPermiso;
                        objFila["VALOR_TOTAL_LETRAS"] = Utilidades.NumeroALetras(decimal.Parse(objPermiso.ValorTotal, NumberStyles.Currency).ToString()).ToUpper() + " MCTE";

                        //Agrgar fila
                        objInformacion.Rows.Add(objFila);
                    }
                }

                return objInformacion;
            }


            /// <summary>
            /// Cargar la informacion de las microtablas
            /// </summary>
            /// <param name="p_objLiquidacion">AutoliquidacionLiquidacionEntity con la informacion de la liquidacion</param>
            /// <returns>DataTable con la informacion de la liquidacion</returns>
            private DataTable ObtenerDataSetMicroTablas(AutoliquidacionLiquidacionEntity p_objLiquidacion)
            {
                DataTable objInformacion = null;
                DataRow objFila = null;

                //Verificar que el objeto no sea nulo
                if (p_objLiquidacion != null && p_objLiquidacion.Detalle0324 != null && p_objLiquidacion.Detalle0324.DatosMicrotabla != null)
                {
                    //Crear tabla
                    objInformacion = new DataTable();
                    objInformacion.TableName = "DATOS_MICROTABLA";
                    objInformacion.Columns.Add("LIQUIDACIONID", Type.GetType("System.Int32"));
                    objInformacion.Columns.Add("CATEGORIA", Type.GetType("System.String"));
                    objInformacion.Columns.Add("HONORARIO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("DEDICACION", Type.GetType("System.String"));
                    objInformacion.Columns.Add("NUMERO_VISITAS", Type.GetType("System.String"));
                    objInformacion.Columns.Add("DURACION", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VIATICOS_DIARIOS", Type.GetType("System.String"));
                    objInformacion.Columns.Add("TOTAL_VIATICOS", Type.GetType("System.String"));
                    objInformacion.Columns.Add("COSTO_TOTAL", Type.GetType("System.String"));
                    objInformacion.Columns.Add("TOTAL_DIAS", Type.GetType("System.String"));

                    //Cargar datos
                    foreach (AutoliquidacionDatosMicroTablaEntity objDato in p_objLiquidacion.Detalle0324.DatosMicrotabla)
                    {
                        objFila = objInformacion.NewRow();
                        objFila["LIQUIDACIONID"] = p_objLiquidacion.LiquidacionID;
                        objFila["CATEGORIA"] = objDato.Categoria;
                        objFila["HONORARIO"] = objDato.Honorario;
                        objFila["DEDICACION"] = objDato.Dedicacion;
                        objFila["NUMERO_VISITAS"] = objDato.NumeroVisitas;
                        objFila["DURACION"] = objDato.Duracion;
                        objFila["VIATICOS_DIARIOS"] = objDato.ViaticosDiarios;
                        objFila["TOTAL_VIATICOS"] = objDato.TotalViaticos;
                        objFila["COSTO_TOTAL"] = objDato.CostoTotal;
                        objFila["TOTAL_DIAS"] = string.Format("{0:#0.##}", Convert.ToDecimal(objDato.NumeroVisitas) * Convert.ToDecimal(objDato.Duracion));

                        //Agrgar fila
                        objInformacion.Rows.Add(objFila);
                    }
                }

                return objInformacion;
            }


            /// <summary>
            /// Cargar la informacion de los tiquetes
            /// </summary>
            /// <param name="p_objLiquidacion">AutoliquidacionLiquidacionEntity con la informacion de la liquidacion</param>
            /// <returns>DataTable con la informacion de la liquidacion</returns>
            private DataTable ObtenerDataSetTiquetes(AutoliquidacionLiquidacionEntity p_objLiquidacion)
            {
                DataTable objInformacion = null;
                DataRow objFila = null;

                //Verificar que el objeto no sea nulo
                if (p_objLiquidacion != null && p_objLiquidacion.Detalle0324 != null && p_objLiquidacion.Detalle0324.Tiquetes != null)
                {
                    //Crear tabla
                    objInformacion = new DataTable();
                    objInformacion.TableName = "TIQUETES";
                    objInformacion.Columns.Add("LIQUIDACIONID", Type.GetType("System.Int32"));
                    objInformacion.Columns.Add("TIPO_TIQUETE", Type.GetType("System.String"));
                    objInformacion.Columns.Add("DEPARTAMENTO_ORIGEN", Type.GetType("System.String"));
                    objInformacion.Columns.Add("MUNICIPIO_ORIGEN", Type.GetType("System.String"));
                    objInformacion.Columns.Add("DEPARTAMENTO_DESTINO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("MUNICIPIO_DESTINO", Type.GetType("System.String"));
                    objInformacion.Columns.Add("VALOR_TIQUETE", Type.GetType("System.String"));
                    objInformacion.Columns.Add("NUMERO_TIQUETES", Type.GetType("System.String"));
                    objInformacion.Columns.Add("TOTAL_VALOR_TIQUETES", Type.GetType("System.String"));

                    //Cargar datos
                    foreach (AutoliquidacionTiquetesEntity objTiquete in p_objLiquidacion.Detalle0324.Tiquetes)
                    {
                        objFila = objInformacion.NewRow();
                        objFila["LIQUIDACIONID"] = p_objLiquidacion.LiquidacionID;
                        objFila["TIPO_TIQUETE"] = objTiquete.TipoTiquete;
                        objFila["DEPARTAMENTO_ORIGEN"] = objTiquete.DepartamentoOrigen;
                        objFila["MUNICIPIO_ORIGEN"] = objTiquete.MunicipioOrigen;
                        objFila["DEPARTAMENTO_DESTINO"] = objTiquete.DepartamentoDestino;
                        objFila["MUNICIPIO_DESTINO"] = objTiquete.MunicipioDestino;
                        objFila["VALOR_TIQUETE"] = objTiquete.ValorTiquete;
                        objFila["NUMERO_TIQUETES"] = objTiquete.NumeroTiquetes;
                        objFila["TOTAL_VALOR_TIQUETES"] = objTiquete.ValorTotalTiquetes;

                        //Agrgar fila
                        objInformacion.Rows.Add(objFila);
                    }
                }

                return objInformacion;
            }


            /// <summary>
            /// Cargar la informacion de la liquidacion en un DataSet para la generación del formulario
            /// </summary>
            /// <param name="p_objLiquidacion">AutoliquidacionLiquidacionEntity con la informacion de la liquidacion</param>
            /// <returns>DataSet con la informacion de la liquidacion</returns>
            private DataSet ObtenerDataSetFormulario(AutoliquidacionLiquidacionEntity p_objLiquidacion)
            {
                DataSet objDsLiquidacion = null;
                DataTable objInformacion = null;

                //Verificar que no sea nulo el objeto
                if (p_objLiquidacion != null)
                {
                    //Crear DataSet
                    objDsLiquidacion = new DataSet();

                    //Cargar datos basicos
                    objInformacion = this.ObtenerDataSetLiquidacion(p_objLiquidacion);
                    if (objInformacion != null)
                        objDsLiquidacion.Tables.Add(objInformacion);

                    //Cargar datos ley 633
                    objInformacion = this.ObtenerDataSetLey633(p_objLiquidacion);
                    if (objInformacion != null)
                        objDsLiquidacion.Tables.Add(objInformacion);

                    //Cargar datos resolucion 0324
                    objInformacion = this.ObtenerDataSetResolucion0324(p_objLiquidacion);
                    if (objInformacion != null)
                        objDsLiquidacion.Tables.Add(objInformacion);

                    //Cargar datos permisos
                    objInformacion = this.ObtenerDataSetPermisos(p_objLiquidacion);
                    if (objInformacion != null)
                        objDsLiquidacion.Tables.Add(objInformacion);

                    //Cargar datos microtablas
                    objInformacion = this.ObtenerDataSetMicroTablas(p_objLiquidacion);
                    if (objInformacion != null)
                        objDsLiquidacion.Tables.Add(objInformacion);

                    //Cargar datos tiquetes
                    objInformacion = this.ObtenerDataSetTiquetes(p_objLiquidacion);
                    if (objInformacion != null)
                        objDsLiquidacion.Tables.Add(objInformacion);
                }

                return objDsLiquidacion;
            }


            /// <summary>
            /// Cargar la información del formulario de liquidación para licencia ambiental
            /// </summary>
            /// <param name="p_strNumeroVITAL">string con el número VITAL</param>
            /// <param name="p_intAutoridadAmbiental">int con el id de la autoridad ambiental</param>
            /// <returns></returns>
            private AutoliquidacionSolicitudEntity CargarDatosFormularioLicencias(string p_strNumeroVITAL, int p_intAutoridadAmbiental)
            {
                CorrespondenciaSilpaDalc objCorrespondencia = null;
                AutoliquidacionSolicitudEntity objSolicitud = null;
                DAA objDaa = null;
                string strXMLFormulario = "";
                StringReader objReader = null;
                DataSet objFormulario = null;
                Autoliquidacion objAutoliquidacion = null;
                DataSet objDatosMovimientos = null;
                AutoliquidacionPermisoEntity objPermiso = null;

                //Consultar el formulario
                objDaa = new SILPA.LogicaNegocio.DAA.DAA();
                strXMLFormulario = objDaa.ConsultarDatosFormulario(p_strNumeroVITAL, "D");

                if (!string.IsNullOrEmpty(strXMLFormulario))
                {
                    //Cargar XML en DataSet
                    objReader = new StringReader(strXMLFormulario);
                    objFormulario = new DataSet();
                    objFormulario.ReadXml(objReader);

                    //Crear objeto
                    objSolicitud = new AutoliquidacionSolicitudEntity();
                    objSolicitud.NumeroVITAL = p_strNumeroVITAL;

                    //Cargar el identificador del formulario
                    objSolicitud.FormularioID = Convert.ToInt32(ConfigurationManager.AppSettings["LiquidacionFormularioLicencia"]);


                    //Cargar datos basicos
                    if (objFormulario.Tables.Contains("FORMULARIO"))
                    {

                        //Cargar descripción del proyecto
                        objSolicitud.NombreProyecto = objFormulario.Tables[0].Rows[0]["Nombre_del_Proyecto_Obra_o_Actividad_8502"].ToString();

                        //Cargar valor del proyecto
                        if (!string.IsNullOrEmpty(objFormulario.Tables[0].Rows[0]["Valor_de_Proyecto_en_pesos_colombianos_8505"].ToString()))
                        {
                            try
                            {
                                objSolicitud.ValorProyecto = Convert.ToDecimal(objFormulario.Tables[0].Rows[0]["Valor_de_Proyecto_en_pesos_colombianos_8505"]);
                            }
                            catch (Exception)
                            {
                                objSolicitud.ValorProyecto = 0;
                            }
                        }

                        //Cargar datos desplegables
                        objAutoliquidacion = new Autoliquidacion();
                        objSolicitud.SolicitudID = objAutoliquidacion.ConsultarIdentificadoDesplegable((int)DesplegableEnum.Solicitud, objFormulario.Tables[0].Rows[0]["Solicitud_de_liquidación_para_8498"].ToString());
                        objSolicitud.TramiteID = objAutoliquidacion.ConsultarIdentificadoDesplegable((int)DesplegableEnum.Tramite_Licencia, objFormulario.Tables[0].Rows[0]["Trámite_8499"].ToString(), objFormulario.Tables[0].Rows[0]["Solicitud_de_liquidación_para_8498"].ToString());
                        objSolicitud.SectorID = objAutoliquidacion.ConsultarIdentificadoDesplegable((int)DesplegableEnum.Sector, objFormulario.Tables[0].Rows[0]["Sector._8722"].ToString());
                        objSolicitud.Proyecto = objFormulario.Tables[0].Rows[0]["Proyecto_8500"].ToString();
                        objSolicitud.ProyectoID = objAutoliquidacion.ConsultarIdentificadoDesplegable((int)DesplegableEnum.Proyecto, objFormulario.Tables[0].Rows[0]["Proyecto_8500"].ToString());
                        objSolicitud.Actividad = objFormulario.Tables[0].Rows[0]["Tipo_de_Proyecto_Obra_o_Actividad_Identifique_el_numeral_yo_literal_del_Art._8_y_9_Decreto_20412014_8501"].ToString();
                        objSolicitud.ActividadID = objAutoliquidacion.ConsultarIdentificadoDesplegable((int)DesplegableEnum.Actividad, objFormulario.Tables[0].Rows[0]["Tipo_de_Proyecto_Obra_o_Actividad_Identifique_el_numeral_yo_literal_del_Art._8_y_9_Decreto_20412014_8501"].ToString());
                        objSolicitud.PermisosANLA = false;
                    }

                    //Cargar permisos
                    if (objFormulario.Tables.Contains("VITAL_INFORMACION_SECTOR_LIQUIDACION_300_300"))
                    {
                        foreach (DataRow objPermisoFormulario in objFormulario.Tables["VITAL_INFORMACION_SECTOR_LIQUIDACION_300_300"].Rows)
                        {
                            //Cargar información de permisos
                            objPermiso = new AutoliquidacionPermisoEntity
                                        {
                                            Descripcion = (!string.IsNullOrEmpty(objPermisoFormulario["Otro_Cuál_8725"].ToString()) ? objPermisoFormulario["Otro_Cuál_8725"].ToString().Trim() : objPermisoFormulario["RELACIÓN_DE_PERMISOS_AUORIZACIONES_YO_CONCESIONES_8552"].ToString()),
                                            NumeroPermisos = Convert.ToInt32(objPermisoFormulario["Numero_de_Permisos_a_Solicitar_8555"]),
                                            Entidad = objPermisoFormulario["autoridad_ambiental_competente_8553"].ToString()
                                        };

                            //Cargar id del permiso
                            if (objPermisoFormulario["RELACIÓN_DE_PERMISOS_AUORIZACIONES_YO_CONCESIONES_8552"].ToString().Trim().Contains("Concesión de Aguas Subterráneas"))
                            {
                                objPermiso.PermisoID = 1;
                            }
                            else if (objPermisoFormulario["RELACIÓN_DE_PERMISOS_AUORIZACIONES_YO_CONCESIONES_8552"].ToString().Trim().Contains("Concesión de Aguas Superficiales"))
                            {
                                objPermiso.PermisoID = 8;
                            }
                            else if (objPermisoFormulario["RELACIÓN_DE_PERMISOS_AUORIZACIONES_YO_CONCESIONES_8552"].ToString().Trim().Contains("Emisiones Atmosféricas"))
                            {
                                objPermiso.PermisoID = 3;
                            }
                            else if (objPermisoFormulario["RELACIÓN_DE_PERMISOS_AUORIZACIONES_YO_CONCESIONES_8552"].ToString().Trim().Contains("Explotación de Canteras"))
                            {
                                objPermiso.PermisoID = 7;
                            }
                            else if (objPermisoFormulario["RELACIÓN_DE_PERMISOS_AUORIZACIONES_YO_CONCESIONES_8552"].ToString().Trim().Contains("Material de Arrastre"))
                            {
                                objPermiso.PermisoID = 6;
                            }
                            else if (objPermisoFormulario["RELACIÓN_DE_PERMISOS_AUORIZACIONES_YO_CONCESIONES_8552"].ToString().Trim().Contains("Ocupación de Cauce"))
                            {
                                objPermiso.PermisoID = 5;
                            }
                            else if (objPermisoFormulario["RELACIÓN_DE_PERMISOS_AUORIZACIONES_YO_CONCESIONES_8552"].ToString().Trim().Contains("Vertimiento de Aguas Residuales"))
                            {
                                objPermiso.PermisoID = 2;
                            }
                            else if (objPermisoFormulario["RELACIÓN_DE_PERMISOS_AUORIZACIONES_YO_CONCESIONES_8552"].ToString().Trim().Contains("Aprovechamiento Forestal"))
                            {
                                objPermiso.PermisoID = 4;
                            }
                            else if (objPermisoFormulario["RELACIÓN_DE_PERMISOS_AUORIZACIONES_YO_CONCESIONES_8552"].ToString().Trim().Contains("Otro"))
                            {
                                objPermiso.PermisoID = 9;
                            }
                            else if (objPermisoFormulario["RELACIÓN_DE_PERMISOS_AUORIZACIONES_YO_CONCESIONES_8552"].ToString().Trim().Contains("Exploración de Aguas Subterráneas"))
                            {
                                objPermiso.PermisoID = 10;
                            }
                            
                            //Adicionar permiso
                            objSolicitud.AdicionarPermiso(objPermiso);                            
                        }
                    }

                    //Cargar ubicación del proyecto
                    if (objFormulario.Tables.Contains("Información_para_la_ubicación_del_proyecto_con_seleccion_de_AA_91"))
                    {
                        foreach (DataRow objUbicacion in objFormulario.Tables["Información_para_la_ubicación_del_proyecto_con_seleccion_de_AA_91"].Rows)
                        {
                            objSolicitud.AdicionarUbicacion(new AutoliquidacionUbicacionEntity { Departamento = objUbicacion["Departamento_1753"].ToString(), Municipio = objUbicacion["Municipio_1754"].ToString() });
                        }
                    }

                    //Consultar información de radicación
                    objCorrespondencia = new CorrespondenciaSilpaDalc();
                    objDatosMovimientos = objCorrespondencia.consultarMovimientos(null, null, null, DateTime.Now.AddYears(-5), DateTime.Now, null, null, null, p_strNumeroVITAL, p_intAutoridadAmbiental.ToString());
                    if (objDatosMovimientos != null && objDatosMovimientos.Tables.Count > 0 && objDatosMovimientos.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(objDatosMovimientos.Tables[0].Rows[0]["Mov_NUR"].ToString()))
                        {
                            objSolicitud.Radicado = objDatosMovimientos.Tables[0].Rows[0]["Mov_NUR"].ToString();
                            objSolicitud.FechaRadicado = Convert.ToDateTime(objDatosMovimientos.Tables[0].Rows[0]["Doc_FechaCreacion"]);
                        }
                    }
                }

                return objSolicitud;
            }


            /// <summary>
            /// Cargar la información del formulario de liquidación para permisos
            /// </summary>
            /// <param name="p_strNumeroVITAL">string con el número VITAL</param>
            /// <param name="p_intAutoridadAmbiental">int con el id de la autoridad ambiental</param>
            /// <returns></returns>
            private AutoliquidacionSolicitudEntity CargarDatosFormularioPermisos(string p_strNumeroVITAL, int p_intAutoridadAmbiental)
            {
                CorrespondenciaSilpaDalc objCorrespondencia = null;
                AutoliquidacionSolicitudEntity objSolicitud = null;
                DAA objDaa = null;
                string strXMLFormulario = "";
                StringReader objReader = null;
                DataSet objFormulario = null;
                Autoliquidacion objAutoliquidacion = null;
                DataSet objDatosMovimientos = null;
                AutoliquidacionPermisoEntity objPermiso = null;

                //Consultar el formulario
                objDaa = new SILPA.LogicaNegocio.DAA.DAA();
                strXMLFormulario = objDaa.ConsultarDatosFormulario(p_strNumeroVITAL, "D");

                if (!string.IsNullOrEmpty(strXMLFormulario))
                {
                    //Cargar XML en DataSet
                    objReader = new StringReader(strXMLFormulario);
                    objFormulario = new DataSet();
                    objFormulario.ReadXml(objReader);

                    //Crear objeto
                    objSolicitud = new AutoliquidacionSolicitudEntity();
                    objSolicitud.NumeroVITAL = p_strNumeroVITAL;

                    //Cargar el identificador del formulario
                    objSolicitud.FormularioID = Convert.ToInt32(ConfigurationManager.AppSettings["LiquidacionFormularioPermiso"]);

                    //Cargar descripción del proyecto
                    objSolicitud.NombreProyecto = objFormulario.Tables[0].Rows[0]["Nombre_del_Permiso_Autorización_yo_Concesión_Ambiental_8736"].ToString();

                    //Cargar valor del proyecto
                    if (!string.IsNullOrEmpty(objFormulario.Tables[0].Rows[0]["Costo_del_Proyecto_en_pesos_colombianos_8743"].ToString()))
                    {
                        try
                        {
                            objSolicitud.ValorProyecto = Convert.ToDecimal(objFormulario.Tables[0].Rows[0]["Costo_del_Proyecto_en_pesos_colombianos_8743"]);
                        }
                        catch (Exception)
                        {
                            objSolicitud.ValorProyecto = 0;
                        }
                    }

                    //Cargar datos desplegables
                    objAutoliquidacion = new Autoliquidacion();
                    objSolicitud.SolicitudID = objAutoliquidacion.ConsultarIdentificadoDesplegable((int)DesplegableEnum.Solicitud, objFormulario.Tables[0].Rows[0][1].ToString());
                    objSolicitud.TramiteID = 0;
                    objSolicitud.SectorID = 0;
                    objSolicitud.Proyecto = "";
                    objSolicitud.ProyectoID = 0;
                    objSolicitud.Actividad = "";
                    objSolicitud.ActividadID = 0;
                    objSolicitud.PermisosANLA = true;

                    //Cargar permisos
                    if (objFormulario.Tables.Count > 3 && objFormulario.Tables["PARA_LIQUIDAR_PERMISOS_316"].Rows.Count > 0)
                    {
                        foreach (DataRow objPermisoFormulario in objFormulario.Tables["PARA_LIQUIDAR_PERMISOS_316"].Rows)
                        {
                            //Cargar información de permisos
                            objPermiso = new AutoliquidacionPermisoEntity
                            {
                                Descripcion = (!string.IsNullOrEmpty(objPermisoFormulario["Otro_Cuál_9120"].ToString()) ? objPermisoFormulario["Otro_Cuál_9120"].ToString().Trim() : objPermisoFormulario["Trámite_8733"].ToString()),
                                NumeroPermisos = Convert.ToInt32(objPermisoFormulario["Número_de_Permisos_9119"]),
                                Entidad = "ANLA"
                            };

                            //Cargar id del permiso
                            if (objPermisoFormulario["Trámite_8733"].ToString().Trim().Contains("Concesión de Aguas Subterráneas"))
                            {
                                objPermiso.PermisoID = 1;
                            }
                            else if (objPermisoFormulario["Trámite_8733"].ToString().Trim().Contains("Concesión de Aguas Superficiales"))
                            {
                                objPermiso.PermisoID = 8;
                            }
                            else if (objPermisoFormulario["Trámite_8733"].ToString().Trim().Contains("Emisiones Atmosféricas"))
                            {
                                objPermiso.PermisoID = 3;
                            }
                            else if (objPermisoFormulario["Trámite_8733"].ToString().Trim().Contains("Explotación de Canteras"))
                            {
                                objPermiso.PermisoID = 7;
                            }
                            else if (objPermisoFormulario["Trámite_8733"].ToString().Trim().Contains("Material de Arrastre"))
                            {
                                objPermiso.PermisoID = 6;
                            }
                            else if (objPermisoFormulario["Trámite_8733"].ToString().Trim().Contains("Ocupación de Cauce"))
                            {
                                objPermiso.PermisoID = 5;
                            }
                            else if (objPermisoFormulario["Trámite_8733"].ToString().Trim().Contains("Vertimiento"))
                            {
                                objPermiso.PermisoID = 2;
                            }
                            else if (objPermisoFormulario["Trámite_8733"].ToString().Trim().Contains("Aprovechamiento Forestal"))
                            {
                                objPermiso.PermisoID = 4;
                            }
                            else if (objPermisoFormulario["Trámite_8733"].ToString().Trim().Contains("Otro"))
                            {
                                objPermiso.PermisoID = 9;
                            }
                            else if (objPermisoFormulario["Trámite_8733"].ToString().Trim().Contains("Exploración de Aguas Subterráneas"))
                            {
                                objPermiso.PermisoID = 10;
                            }

                            //Adicionar permiso
                            objSolicitud.AdicionarPermiso(objPermiso);
                        }
                    }

                    //Cargar ubicación del proyecto
                    foreach (DataRow objUbicacion in objFormulario.Tables["Información_para_la_ubicación_del_proyecto_con_seleccion_de_AA_91"].Rows)
                    {
                        objSolicitud.AdicionarUbicacion(new AutoliquidacionUbicacionEntity { Departamento = objUbicacion["Departamento_1753"].ToString(), Municipio = objUbicacion["Municipio_1754"].ToString() });
                    }

                    //Consultar información de radicación
                    objCorrespondencia = new CorrespondenciaSilpaDalc();
                    objDatosMovimientos = objCorrespondencia.consultarMovimientos(null, null, null, DateTime.Now.AddYears(-5), DateTime.Now, null, null, null, p_strNumeroVITAL, p_intAutoridadAmbiental.ToString());
                    if (objDatosMovimientos != null && objDatosMovimientos.Tables.Count > 0 && objDatosMovimientos.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(objDatosMovimientos.Tables[0].Rows[0]["Mov_NUR"].ToString()))
                        {
                            objSolicitud.Radicado = objDatosMovimientos.Tables[0].Rows[0]["Mov_NUR"].ToString();
                            objSolicitud.FechaRadicado = Convert.ToDateTime(objDatosMovimientos.Tables[0].Rows[0]["Doc_FechaCreacion"]);
                        }
                    }
                }

                return objSolicitud;
            }


            /// <summary>
            /// Cargar la información del formulario de liquidación para instrumentos
            /// </summary>
            /// <param name="p_strNumeroVITAL">string con el número VITAL</param>
            /// <param name="p_intAutoridadAmbiental">int con el id de la autoridad ambiental</param>
            /// <returns></returns>
            private AutoliquidacionSolicitudEntity CargarDatosFormularioInstrumentos(string p_strNumeroVITAL, int p_intAutoridadAmbiental)
            {
                CorrespondenciaSilpaDalc objCorrespondencia = null;
                AutoliquidacionSolicitudEntity objSolicitud = null;
                DAA objDaa = null;
                string strXMLFormulario = "";
                StringReader objReader = null;
                DataSet objFormulario = null;
                Autoliquidacion objAutoliquidacion = null;
                DataSet objDatosMovimientos = null;

                //Consultar el formulario
                objDaa = new SILPA.LogicaNegocio.DAA.DAA();
                strXMLFormulario = objDaa.ConsultarDatosFormulario(p_strNumeroVITAL, "D");

                if (!string.IsNullOrEmpty(strXMLFormulario))
                {
                    //Cargar XML en DataSet
                    objReader = new StringReader(strXMLFormulario);
                    objFormulario = new DataSet();
                    objFormulario.ReadXml(objReader);

                    //Crear objeto
                    objSolicitud = new AutoliquidacionSolicitudEntity();
                    objSolicitud.NumeroVITAL = p_strNumeroVITAL;

                    //Cargar el identificador del formulario
                    objSolicitud.FormularioID = Convert.ToInt32(ConfigurationManager.AppSettings["LiquidacionFormularioInstrumentos"]);

                    //Cargar descripción del proyecto
                    objSolicitud.NombreProyecto = objFormulario.Tables[0].Rows[0]["Nombre_del_Proyecto_Obra_o_Actividad_8800"].ToString();

                    //Cargar valor del proyecto
                    if (!string.IsNullOrEmpty(objFormulario.Tables[0].Rows[0]["Costo_del_Proyecto_en_pesos_colombianos_8802"].ToString()))
                    {
                        try
                        {
                            objSolicitud.ValorProyecto = Convert.ToDecimal(objFormulario.Tables[0].Rows[0]["Costo_del_Proyecto_en_pesos_colombianos_8802"]);
                        }
                        catch (Exception)
                        {
                            objSolicitud.ValorProyecto = 0;
                        }
                    }

                    //Cargar datos desplegables
                    objAutoliquidacion = new Autoliquidacion();
                    objSolicitud.SolicitudID = objAutoliquidacion.ConsultarIdentificadoDesplegable((int)DesplegableEnum.Solicitud, objFormulario.Tables[0].Rows[0]["Solicitud_de_liquidación_para_8798"].ToString());
                    objSolicitud.TramiteID = objAutoliquidacion.ConsultarIdentificadoDesplegable((int)DesplegableEnum.Tramite_Instrumento, objFormulario.Tables[0].Rows[0]["Trámite_8799"].ToString(), objFormulario.Tables[0].Rows[0]["Solicitud_de_liquidación_para_8798"].ToString());
                    objSolicitud.SectorID = 0;
                    objSolicitud.Proyecto = "";
                    objSolicitud.ProyectoID = 0;
                    objSolicitud.Actividad = "";
                    objSolicitud.ActividadID = 0;
                    objSolicitud.PermisosANLA = false;

                    //Cargar ubicación del proyecto
                    foreach (DataRow objUbicacion in objFormulario.Tables[1].Rows)
                    {
                        objSolicitud.AdicionarUbicacion(new AutoliquidacionUbicacionEntity { Departamento = objUbicacion["Departamento_1753"].ToString(), Municipio = objUbicacion["Municipio_1754"].ToString() });
                    }

                    //Consultar información de radicación
                    objCorrespondencia = new CorrespondenciaSilpaDalc();
                    objDatosMovimientos = objCorrespondencia.consultarMovimientos(null, null, null, DateTime.Now.AddYears(-5), DateTime.Now, null, null, null, p_strNumeroVITAL, p_intAutoridadAmbiental.ToString());
                    if (objDatosMovimientos != null && objDatosMovimientos.Tables.Count > 0 && objDatosMovimientos.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(objDatosMovimientos.Tables[0].Rows[0]["Mov_NUR"].ToString()))
                        {
                            objSolicitud.Radicado = objDatosMovimientos.Tables[0].Rows[0]["Mov_NUR"].ToString();
                            objSolicitud.FechaRadicado = Convert.ToDateTime(objDatosMovimientos.Tables[0].Rows[0]["Doc_FechaCreacion"]);
                        }
                    }
                }

                return objSolicitud;
            }
            
            /// <summary>
            /// Avanza tarea en el BPM
            /// </summary>
            /// <param name="p_objCobro">AutoliquidacionCobroEntity con la información del cobro</param>
            /// <returns>string con mensaje de error en caso de que se presente</returns>
            private string AvanzarTarea(AutoliquidacionCobroEntity p_objCobro, string p_objUsuario)
            {
                SolicitudDAAEIAIdentity objSolicitud = null;
                SolicitudDAAEIADalc objSolicitudDalc = null;
                BpmParametros objBpmParametrosDalc = null;
                SILPA.Servicios.BPMServices.GattacaBPMServices9000 objServicioBPM = null;
                Proceso objProceso = null;
                DataSet dsResultadoBPMParametro = null;
                string strMensajeError = "";
                int intTipo = 0;
                string strNombre = "";

                //Verificar numero vital
                if (!string.IsNullOrEmpty(p_objCobro.NumeroSILPA))
                {
                    //Obtener parametro
                    objBpmParametrosDalc = new BpmParametros();
                    dsResultadoBPMParametro = objBpmParametrosDalc.ListarBmpParametros(18);

                    //Cargar la solicitud
                    objSolicitudDalc = new SolicitudDAAEIADalc();
                    objSolicitud = objSolicitudDalc.ObtenerSolicitud(null, null, p_objCobro.NumeroSILPA);
                    objProceso = new Proceso();
                    objProceso.DeterminarActividad(objSolicitud.IdProcessInstance);

                    //Obtener condicion de cierre
                    if (dsResultadoBPMParametro.Tables[0].Rows.Count > 0)
                    {
                        intTipo = Convert.ToInt32(dsResultadoBPMParametro.Tables[0].Rows[0]["TIPO"]);
                        strNombre = Convert.ToString(dsResultadoBPMParametro.Tables[0].Rows[0]["NOMBRE"]);

                        switch (intTipo)
                        {
                            case 3:
                                objProceso.CondicionSiguientePorActividad(objProceso.IdActiviteInstance, strNombre);
                                break;
                        }

                        //Si se encontro la condicion finalizar la actividad
                        if (objProceso.IdCondicion != string.Empty)
                        {
                            //Finalizar
                            objServicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();
                            objServicioBPM.Url = SILPA.Comun.DireccionamientoWS.UrlWS("GattacaBpm");
                            objServicioBPM.EndActivityInstance("SoftManagement", 1, objProceso.IdActiviteInstance,
                                                                objSolicitud.IdProcessInstance, objProceso.IdCondicion, "Actividad Finalizada desde AutoLiquidación", "", "0", "0", "0");
                        }
                    }
                }
                else
                {
                    strMensajeError = "Número Vital no especificado";
                }

                return strMensajeError;
            }


            /// <summary>
            /// Registrar la informacion del cobro en el sistema
            /// </summary>
            /// <param name="p_objCobro">AutoliquidacionCobroEntity con la información del cobro</param>
            /// <returns>string con mensaje de error en caso de que se presente</returns>
            private string RegistrarCobro(AutoliquidacionCobroEntity p_objCobro)
            {
                SolicitudDAAEIADalc objSolicitudDalc = null;
                SolicitudDAAEIAIdentity objSolicitudIdentity = null;
                ConceptoIdentity objConceptoIdentity = null;
                CobroDalc objCobroDalc = null;
                DetalleCobroDalc objDetalleDalc = null;
                PermisosCobroDalc objPermisoDalc = null;
                DetalleCobroIdentity objDetalleIdentity = null;
                PermisoCobroIdentity objPermisoIdentity = null;
                CobroIdentity objCobroVITAL = null;
                string strMensajeError = "";
                
                //Cargar información de la solicitud
                objSolicitudDalc = new SolicitudDAAEIADalc();
                objSolicitudIdentity = objSolicitudDalc.ObtenerSolicitud(null, null, p_objCobro.NumeroSILPA);

                //Verificar que exista la solicitud
                if (objSolicitudIdentity != null)
                {
                    objConceptoIdentity = new ConceptoIdentity();
                    objConceptoIdentity.IDConcepto = p_objCobro.ConceptoCobro;

                    //Cargar informacion de cobro para creación
                    objCobroVITAL = new CobroIdentity
                    {
                        NumSILPA = p_objCobro.NumeroSILPA,
                        NumExpediente = p_objCobro.CodigoExpediente,
                        NumReferencia = p_objCobro.NumeroReferencia,
                        IndicadorProcesoField = p_objCobro.IndicadorProceso,
                        NumDocumentoField = p_objCobro.NumeroDocumento,
                        ConceptoCobro = objConceptoIdentity,
                        NumSolicitud = objSolicitudIdentity.IdSolicitud,
                        FechaExpedicion = p_objCobro.FechaCreacion,
                        FechaPagoOportuno = p_objCobro.FechaPagoOportuno,
                        FechaVencimiento = p_objCobro.FechaVencimiento,
                        CodigoBarras = p_objCobro.CodigoBarras,
                        OrigenCobro = (!string.IsNullOrEmpty(p_objCobro.OrigenCobro) ? p_objCobro.OrigenCobro : OrigenCobroAttribute.GetStringValue(EnumOrigenCobro.AUTOLIQUIDACION))
                    };

                    //Se crea registro
                    objCobroDalc = new CobroDalc();
                    objCobroDalc.InsertarCobro(ref objCobroVITAL);

                    //Verificar que se insertará el cobro
                    if(objCobroVITAL.IdCobro > 0){

                        //Cargar datos del detalle
                        objDetalleIdentity = new DetalleCobroIdentity
                        {
                            Descripcion = p_objCobro.DescripcionCobro,
                            Valor = p_objCobro.ValorCobroServicio
                        };

                        //Insertar detalle del cobro
                        objDetalleDalc = new DetalleCobroDalc();
                        objDetalleDalc.InsertarDetalle(ref objDetalleIdentity, objCobroVITAL.IdCobro);

                        //Cargar los permisos
                        if (p_objCobro.Permisos != null && p_objCobro.Permisos.Count > 0)
                        {
                            objPermisoDalc = new PermisosCobroDalc();
                            foreach (AutoliquidacionPermisoCobroEntity objPermiso in p_objCobro.Permisos)
                            {
                                objPermisoIdentity = new PermisoCobroIdentity
                                {
                                    CobroID = Convert.ToInt64(objCobroVITAL.IdCobro),
                                    Permiso = objPermiso.Permiso,
                                    Autoridad = objPermiso.Autoridad,
                                    NumeroPermisos = objPermiso.NumeroPermisos,
                                    ValorAdministracion = objPermiso.ValorAdministracion,
                                    ValorServicio = objPermiso.ValorServicio,
                                    ValorTotal = objPermiso.ValorTotal
                                };


                                //Crear permiso
                                objPermisoDalc.InsertarPermiso(objPermisoIdentity);
                            }
                        }

                    }
                    else{
                        strMensajeError = "No se creo el cobro en VITAL";
                    }
                }
                else
                {
                    strMensajeError = "Solicitud para numero VITAL " + p_objCobro.NumeroSILPA + " NO existe";
                }
                

                return strMensajeError;
            }


            /// <summary>
            /// Registrar la informacion del cobro en el sistema
            /// </summary>
            /// <param name="p_objCobro">AutoliquidacionCobroEntity con la información del cobro</param>
            /// <returns>string con mensaje de error en caso de que se presente</returns>
            private decimal RegistrarCobroAutoliquidacion(AutoliquidacionCobroEntity p_objCobro)
            {
                SolicitudDAAEIADalc objSolicitudDalc = null;
                SolicitudDAAEIAIdentity objSolicitudIdentity = null;
                ConceptoIdentity objConceptoIdentity = null;
                CobroDalc objCobroDalc = null;
                DetalleCobroDalc objDetalleDalc = null;
                PermisosCobroDalc objPermisoDalc = null;
                DetalleCobroIdentity objDetalleIdentity = null;
                PermisoCobroIdentity objPermisoIdentity = null;
                CobroIdentity objCobroVITAL = null;

                //Cargar información de la solicitud
                objSolicitudDalc = new SolicitudDAAEIADalc();
                objSolicitudIdentity = objSolicitudDalc.ObtenerSolicitud(null, null, p_objCobro.NumeroSILPA);

                //Verificar que exista la solicitud
                if (objSolicitudIdentity != null)
                {
                    objConceptoIdentity = new ConceptoIdentity();
                    objConceptoIdentity.IDConcepto = p_objCobro.ConceptoCobro;

                    //Cargar informacion de cobro para creación
                    objCobroVITAL = new CobroIdentity
                    {
                        NumSILPA = p_objCobro.NumeroSILPA,
                        NumExpediente = p_objCobro.CodigoExpediente,
                        NumReferencia = p_objCobro.NumeroReferencia,
                        IndicadorProcesoField = p_objCobro.IndicadorProceso,
                        NumDocumentoField = p_objCobro.NumeroDocumento,
                        ConceptoCobro = objConceptoIdentity,
                        NumSolicitud = objSolicitudIdentity.IdSolicitud,
                        FechaExpedicion = p_objCobro.FechaCreacion,
                        FechaPagoOportuno = p_objCobro.FechaPagoOportuno,
                        FechaVencimiento = p_objCobro.FechaVencimiento,
                        CodigoBarras = p_objCobro.CodigoBarras,
                        OrigenCobro = (!string.IsNullOrEmpty(p_objCobro.OrigenCobro) ? p_objCobro.OrigenCobro : OrigenCobroAttribute.GetStringValue(EnumOrigenCobro.AUTOLIQUIDACION))
                    };

                    //Se crea registro
                    objCobroDalc = new CobroDalc();
                    objCobroDalc.InsertarCobro(ref objCobroVITAL);

                    //Verificar que se insertará el cobro
                    if (objCobroVITAL.IdCobro > 0)
                    {

                        //Cargar datos del detalle
                        objDetalleIdentity = new DetalleCobroIdentity
                        {
                            Descripcion = p_objCobro.DescripcionCobro,
                            Valor = p_objCobro.ValorCobroServicio
                        };

                        //Recortar la cadena para almacenarla en la base de datos
                        while (objDetalleIdentity.Descripcion.Length > 500 && objDetalleIdentity.Descripcion.Contains(" "))
                        {
                            objDetalleIdentity.Descripcion = objDetalleIdentity.Descripcion.Substring(0, objDetalleIdentity.Descripcion.LastIndexOf(" "));
                            if (objDetalleIdentity.Descripcion.Length <= 497)
                                objDetalleIdentity.Descripcion = objDetalleIdentity.Descripcion + "...";                            
                            else if (objDetalleIdentity.Descripcion.Length > 497 && objDetalleIdentity.Descripcion.Length <= 500)
                                objDetalleIdentity.Descripcion = objDetalleIdentity.Descripcion.Substring(0, objDetalleIdentity.Descripcion.Length - 3) + "...";
                        }
                        objDetalleIdentity.Descripcion = (objDetalleIdentity.Descripcion.Length <= 500 ? objDetalleIdentity.Descripcion : objDetalleIdentity.Descripcion.Substring(0, 497) + "...");

                        //Insertar detalle del cobro
                        objDetalleDalc = new DetalleCobroDalc();
                        objDetalleDalc.InsertarDetalle(ref objDetalleIdentity, objCobroVITAL.IdCobro);

                        //Cargar los permisos
                        if (p_objCobro.Permisos != null && p_objCobro.Permisos.Count > 0)
                        {
                            objPermisoDalc = new PermisosCobroDalc();
                            foreach (AutoliquidacionPermisoCobroEntity objPermiso in p_objCobro.Permisos)
                            {
                                objPermisoIdentity = new PermisoCobroIdentity
                                {
                                    CobroID = Convert.ToInt64(objCobroVITAL.IdCobro),
                                    Permiso = objPermiso.Permiso,
                                    Autoridad = objPermiso.Autoridad,
                                    NumeroPermisos = objPermiso.NumeroPermisos,
                                    ValorAdministracion = objPermiso.ValorAdministracion,
                                    ValorServicio = objPermiso.ValorServicio,
                                    ValorTotal = objPermiso.ValorTotal
                                };


                                //Crear permiso
                                objPermisoDalc.InsertarPermiso(objPermisoIdentity);
                            }
                        }

                    }
                    else
                    {
                        throw new Exception("No se creo el cobro en VITAL");
                    }
                }
                else
                {
                    throw new Exception("Solicitud para numero VITAL " + p_objCobro.NumeroSILPA + " NO existe");
                }

                return objCobroVITAL.IdCobro;
            }


            /// <summary>
            /// Finaliza la transacción de cobro en BPM
            /// </summary>
            /// <param name="p_strNumeroSilpa">string con el numero vital</param>
            /// <param name="p_strUsuario">string con la identificación del usuario</param>
            /// <returns>string con mensaje de error en caso de que se presente</returns>
            public string FinalizarTransaccionCobroBPM(string p_strNumeroSilpa, string p_strUsuario)
            {
                SolicitudDAAEIAIdentity objSolicitud = null;
                SolicitudDAAEIADalc objSolicitudDalc = null;
                Proceso objProceso = null;
                string strCasoProceso = string.Empty;
                string strCondicion = string.Empty;
                string strMensaje = "";

                try
                {
                    //Obtener información de la solicitud
                    objSolicitud = new SolicitudDAAEIAIdentity();
                    objSolicitudDalc = new SolicitudDAAEIADalc();
                    objSolicitud = objSolicitudDalc.ObtenerSolicitud(null, null, p_strNumeroSilpa);

                    //Crear objeto manejo del proceso
                    objProceso = new Proceso();

                    //Cargar el identificador del proceso
                    if (objSolicitud.IdProcessInstance != null)
                    {
                        ProcesoDalc pDalc = new ProcesoDalc();
                        objProceso.PIdentity = pDalc.ObtenerObjProceso(objSolicitud.IdProcessInstance);

                        //Obtener condiciones de pago para finalización del proceso
                        objProceso.ObtenerCondicionPagoElectronico();
                        strCondicion = objProceso.PIdentity.CondicionPago;

                        //Finalizar en BPM
                        ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                        int intIDProcessInstance = Convert.ToInt32(objSolicitud.IdProcessInstance);

                        //Validar condiciones
                        strMensaje = servicioWorkflow.ValidarActividadActual(intIDProcessInstance, p_strUsuario, (long)ActividadSilpa.ConsultarPago);
                        if (string.IsNullOrEmpty(strMensaje))
                            servicioWorkflow.FinalizarTarea(intIDProcessInstance, ActividadSilpa.ConsultarPago, p_strUsuario, strCondicion);
                        else
                            //JNS 20190822 se escribe mensaje de error
                            SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "BPM - Validacion :: AutoliquidacionFachada::FinalizarTransaccionCobroBPM - p_strNumeroSilpa: " + (!string.IsNullOrEmpty(p_strNumeroSilpa) ? p_strNumeroSilpa : "null") + " - p_strUsuario: " + (!string.IsNullOrEmpty(p_strUsuario) ? p_strUsuario : "null") + "\n\n Error: " + strMensaje, "BPM_VAL_CON");
                    }
                    else
                    {
                        strMensaje = "No se encontro identificador del proceso para el numero vital " + p_strNumeroSilpa;
                    }
                }
                catch (Exception ex)
                {
                    strMensaje = "Se presento error finalizando cobro en BPM. " + ex.ToString() + " " + ex.StackTrace;
                }

                return strMensaje;
            }

        #endregion

        
        #region Metodos Publicos

            
            /// <summary>
            /// Consultar la información de la solicitud de liquidación correspondiente al número VITAL especificado
            /// </summary>
            /// <param name="p_strNumeroVITAL">string con el numero VITAL</param>
            /// <param name="p_intAutoridadAmbiental">int con el id de la autoridad ambiental</param>
            /// <returns>string con el XML que contiene la información de la solicitud</returns>
            public string ConsultarInformacionSolicitud(string p_strNumeroVITAL, int p_intAutoridadAmbiental)
            {
                AutoliquidacionSolicitudEntity objSolicitud = null;

                try
                {
                    //Verificar que se especifique la  información
                    if (!string.IsNullOrEmpty(p_strNumeroVITAL))
                    {
                        //Verificar el proceso
                        if (p_strNumeroVITAL.StartsWith(ConfigurationManager.AppSettings["LiquidacionProcesoLicencia"]))
                        {
                            //Cargar datos de formulario
                            objSolicitud = this.CargarDatosFormularioLicencias(p_strNumeroVITAL, p_intAutoridadAmbiental);
                        }
                        else if (p_strNumeroVITAL.StartsWith(ConfigurationManager.AppSettings["LiquidacionProcesoPermiso"]))
                        {
                            //Cargar datos de formulario
                            objSolicitud = this.CargarDatosFormularioPermisos(p_strNumeroVITAL, p_intAutoridadAmbiental);
                        }
                        else if (p_strNumeroVITAL.StartsWith(ConfigurationManager.AppSettings["LiquidacionProcesoInstrumentos"]))
                        {
                            //Cargar datos de formulario
                            objSolicitud = this.CargarDatosFormularioInstrumentos(p_strNumeroVITAL, p_intAutoridadAmbiental);
                        }

                        if (objSolicitud != null)
                        {
                            objSolicitud.Respuesta = new AutoliquidacionRespuestaEntity { Codigo = "0", Mensaje = "OK" };
                        }
                        else
                        {
                            objSolicitud.Respuesta = new AutoliquidacionRespuestaEntity { Codigo = "3", Mensaje = "No se pudo obtener la información del formulario" };
                        }
                    }
                    else
                    {
                        //Cargar error
                        objSolicitud = new AutoliquidacionSolicitudEntity();
                        objSolicitud.Respuesta = new AutoliquidacionRespuestaEntity { Codigo = "1", Mensaje = "No se especifico información para la obtención de la información" };
                    }
                }
                catch (Exception exc)
                {
                    objSolicitud = new AutoliquidacionSolicitudEntity();
                    objSolicitud.Respuesta = new AutoliquidacionRespuestaEntity { Codigo = "2", Mensaje = "Se presento error en la obtención de la información de la solicitud. " + exc.Message };
                }

                return objSolicitud.GetXml();
            }


            /// <summary>
            /// Registra la información de un nuevo cobro
            /// </summary>
            /// <param name="p_strXMLInformacionCobro">string con el XML que contiene la información del cobro</param>
            /// <param name="p_strUsuario">string con el usuario que ejecuta</param>
            /// <param name="p_blnAvanzaTarea">bool que indica si la tarea deb ser avanzada</param>
            /// <returns>string con mensaje de error en caso de que se genere</returns>
            public string CrearCobro(string p_strXMLInformacionCobro, string p_strUsuario, bool p_blnAvanzaTarea)
            {
                XmlSerializador objSerializador = null;
                AutoliquidacionCobroEntity objCobro = null;
                string strMensaje = "";
                
                //Verificar que el contenido no sea nulo
                if (!string.IsNullOrEmpty(p_strXMLInformacionCobro))
                {
                    //Cargar la informacion
                    objSerializador = new XmlSerializador();
                    objCobro = (AutoliquidacionCobroEntity)objSerializador.Deserializar(new AutoliquidacionCobroEntity(), p_strXMLInformacionCobro);

                    //Avanzar tarea
                    if (p_blnAvanzaTarea)
                        strMensaje = this.AvanzarTarea(objCobro, p_strUsuario);

                    //Registrar la información del cobro
                    if (string.IsNullOrEmpty(strMensaje))
                        strMensaje = this.RegistrarCobro(objCobro);

                }
                else
                {
                    //Cargar error
                    strMensaje = "No se especifico información de cobro para registrar";
                }                

                return strMensaje;
            }


            /// <summary>
            /// Registra la información de un nuevo cobro de autoliquidacion
            /// </summary>
            /// <param name="p_strXMLInformacionCobro">string con el XML que contiene la información del cobro</param>
            /// <param name="p_strUsuario">string con el usuario que ejecuta</param>
            /// <returns>string con XML de respuesta resultado del proceso de registro del cobro</returns>
            public string CrearCobroAutoliquidacion(string p_strXMLInformacionCobro, string p_strUsuario)
            {
                XmlSerializador objSerializador = null;
                AutoliquidacionCobroEntity objCobro = null;
                AutoliquidacionRespuestaCobroEntity objRespuestaCobro = null;
                decimal decCobroID = 0;
                
                //Verificar que el contenido no sea nulo
                if (!string.IsNullOrEmpty(p_strXMLInformacionCobro))
                {
                    //Cargar la informacion
                    objSerializador = new XmlSerializador();
                    objCobro = (AutoliquidacionCobroEntity)objSerializador.Deserializar(new AutoliquidacionCobroEntity(), p_strXMLInformacionCobro);

                    //Registrar el cobro en el modulo de cobro
                    decCobroID = this.RegistrarCobroAutoliquidacion(objCobro);

                    //Generar respuesta ok en caso de que se reciba identificador de cobro
                    if(decCobroID > 0)
                    {
                        //Cargar error
                        objRespuestaCobro = new AutoliquidacionRespuestaCobroEntity();
                        objRespuestaCobro.CobroID = Convert.ToInt32(decCobroID);
                        objRespuestaCobro.Respuesta = new AutoliquidacionRespuestaEntity { Codigo = "0", Mensaje = "OK" };
                    }
                    else
                    {
                        //Cargar error
                        objRespuestaCobro = new AutoliquidacionRespuestaCobroEntity();                        
                        objRespuestaCobro.Respuesta = new AutoliquidacionRespuestaEntity { Codigo = "1", Mensaje = "No se pudo generar el cobro de autoliquidación en VITAL" };
                    }
                }
                else
                {
                    //Cargar error
                    throw new Exception("No se especifico información de cobro para registrar");
                }

                return objRespuestaCobro.GetXml();
            }


            /// <summary>
            /// Finalizar transacción de cobro
            /// </summary>
            /// <param name="p_strNumeroReferencia">string con el numero de referencia</param>
            /// <returns>string con mensaje de error en caso de que se presente</returns>
            public string FinalizarTransaccionCobro(string p_strNumeroReferencia)
            {
                Cobro objCobro = null;
                CobroIdentity objCobroIdentity = null;
                string strMensaje = "";

                //Consultar información del cobro
                objCobro = new Cobro();
                objCobroIdentity = objCobro.ObtenerCobroTransaccion(p_strNumeroReferencia);

                //Verificar que se obtenga información del cobro
                if (objCobroIdentity != null && objCobroIdentity.IdCobro > 0)
                {
                    //Obtener información de persona
                    objCobro.ObtenerPersona(objCobroIdentity.NumSILPA);

                    //Finalizar transacción en BPM
                    strMensaje = this.FinalizarTransaccionCobroBPM(objCobroIdentity.NumSILPA, objCobro.objPersona.Identity.NumeroIdentificacion.ToString());

                    //Si no se presento error actualizar el estado del cobro
                    if (string.IsNullOrEmpty(strMensaje))
                    {
                        objCobroIdentity.EstadoCobro = (int)EnumEstadoCobro.TRANSACCION_FINALIZADA;
                        objCobro.ActualizarCobroEstado(objCobroIdentity);
                    }
                }
                else
                {
                    strMensaje = "No se encontro cobro relacionado al numero de referencia especificado";
                }

                return strMensaje;
            }
    

            /// <summary>
            /// Obtener el listado de autoridades para solicitud de permisos ambientales
            /// </summary>
            /// <returns>string con mensaje de error en caso de que se presente</returns>
            public string ListarAutoridadAmbientalPermisos()
            {
                AutoridadAmbiental objAutoridad = null;
                XmlSerializador objXmlSerializador = null;
                DataSet objAutoridades = null;
                string strMensaje = "";

                //Consultar información de las autoridades
                objAutoridad = new AutoridadAmbiental();
                objAutoridades = objAutoridad.ListarAutoridadAmbientalPermisos();

                //Verificar que se obtenga información del cobro
                if (objAutoridades != null)
                {
                    objXmlSerializador = new XmlSerializador();
                    strMensaje = objXmlSerializador.serializar(objAutoridades);
                }
                else
                {
                    strMensaje = "No se encontro autoridades para permisos";
                }

                return strMensaje;
            }

        #endregion
    
    }
}
