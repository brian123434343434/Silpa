using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class CobroDalc
    {
        private Configuracion objConfiguracion;

         public CobroDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public CobroDalc(ref CobroIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();
          
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IdCobro, objIdentity.NumSILPA, objIdentity.NumExpediente, objIdentity.FechaExpedicion, objIdentity.ConceptoCobro.IDConcepto, objIdentity.NumSolicitud };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBRO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.IdCobro = Convert.ToDecimal(dsResultado.Tables[0].Rows[0]["COB_ID"]);
                objIdentity.NumSILPA = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_SILPA"]);
                objIdentity.NumExpediente = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_EXPEDIENTE"]);
                objIdentity.NumReferencia = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_REFERENCIA"]);
                objIdentity.FechaExpedicion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_EXPEDICION"]);
                objIdentity.FechaVencimiento = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_VENCIMIENTO"]);
                objIdentity.CodigoBarras = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_CODIGO_BARRAS"]);
                objIdentity.RutaArchivo = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_RUTA_ARCHIVOS"]);
                objIdentity.NumSolicitud = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["SOL_ID"]);
                objIdentity.EstadoCobro = dsResultado.Tables[0].Rows[0]["ECO_ID"] == null ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ECO_ID"]);
                objIdentity.Transaccion = dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"] == null ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"]);
                objIdentity.FechaTransaccion = dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"] == null ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"].ToString();
                objIdentity.Banco = dsResultado.Tables[0].Rows[0]["COB_BANCO"].ToString();
                objIdentity.OrigenCobro = dsResultado.Tables[0].Rows[0]["COB_ORIGEN"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_ORIGEN"].ToString();

                //Se agrega el Concepto
                ConceptoIdentity _objConcepto = new ConceptoIdentity();
                _objConcepto.IDConcepto= Convert.ToInt32(dsResultado.Tables[0].Rows[0]["CON_ID"]);
                ConceptoCobroDalc _concepto = new ConceptoCobroDalc(ref _objConcepto);
                objIdentity.ConceptoCobro = _objConcepto;
                //Se agrega la lista de detalles
                DetalleCobroDalc _detalle = new DetalleCobroDalc();
                List<DetalleCobroIdentity> _listaDetalles = new List<DetalleCobroIdentity>();
                _listaDetalles = _detalle.ListarDetalles(objIdentity.IdCobro);
                PermisosCobroDalc _permiso = new PermisosCobroDalc();
                objIdentity.Permisos = _permiso.ObtenerPermisosCobro(Convert.ToInt64(objIdentity.IdCobro));
         }

        
        public void ObtenerCobro(ref CobroIdentity objIdentity)
        {          
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IdCobro, objIdentity.NumSILPA, objIdentity.NumExpediente, objIdentity.NumSolicitud };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBRO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado.Tables.Count > 0)
                {
                    
                    if(dsResultado.Tables[0].Rows.Count>0)
                    {
                    objIdentity.IdCobro = Convert.ToDecimal(dsResultado.Tables[0].Rows[0]["COB_ID"]);
                    objIdentity.NumSILPA = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_SILPA"]);
                    objIdentity.NumExpediente = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_EXPEDIENTE"]);
                    objIdentity.NumReferencia = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_REFERENCIA"]);
                    objIdentity.FechaExpedicion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_EXPEDICION"]);
                    objIdentity.FechaVencimiento = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_VENCIMIENTO"]);
                    objIdentity.CodigoBarras = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_CODIGO_BARRAS"]);
                    objIdentity.RutaArchivo = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_RUTA_ARCHIVOS"]);
                    objIdentity.NumSolicitud = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["SOL_ID"]);
                    objIdentity.EstadoCobro = dsResultado.Tables[0].Rows[0]["ECO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ECO_ID"]);
                    objIdentity.Transaccion = dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"] == DBNull.Value ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"]);
                    objIdentity.FechaTransaccion = dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"].ToString();
                    objIdentity.Banco = dsResultado.Tables[0].Rows[0]["COB_BANCO"].ToString();
                    objIdentity.FechaRecordacion = dsResultado.Tables[0].Rows[0]["COB_FECHA_RECORDACION"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_FECHA_RECORDACION"].ToString();
                    objIdentity.IndicadorProcesoField = dsResultado.Tables[0].Rows[0]["NOMBRE_PROYECTO"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["NOMBRE_PROYECTO"].ToString();
                    objIdentity.OrigenCobro = dsResultado.Tables[0].Rows[0]["COB_ORIGEN"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_ORIGEN"].ToString();
                    objIdentity.FechaPagoOportuno = (dsResultado.Tables[0].Rows[0]["COB_FECHA_PAGO_OPORTUNO"] != DBNull.Value ? Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_PAGO_OPORTUNO"]) : default(DateTime));
                    //Se agrega el Concepto
                    ConceptoIdentity _objConcepto = new ConceptoIdentity();
                    _objConcepto.IDConcepto = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["CON_ID"]);

                    //Se agrega la lista de detalles
                    DetalleCobroDalc _detalle = new DetalleCobroDalc();
                    List<DetalleCobroIdentity> _listaDetalles = new List<DetalleCobroIdentity>();
                    _listaDetalles = _detalle.ListarDetalles(objIdentity.IdCobro);
                    objIdentity.ListaConceptoCobro = _listaDetalles;
                    PermisosCobroDalc _permiso = new PermisosCobroDalc();
                    objIdentity.Permisos = _permiso.ObtenerPermisosCobro(Convert.ToInt64(objIdentity.IdCobro));
                }
            }
          
        }


        /// <summary>
        /// Obtener la información de un cobro relacionado a una autoliquidacion
        /// </summary>
        /// <param name="p_intCobroAutoliquidacionID">int con el identificador del cobro de autoliquidacion</param>
        /// <param name="objIdentity">CobroIdentity donde se cargara la información del cobro </param>
        public void ObtenerCobroAutoliquidacion(int p_intCobroAutoliquidacionID, ref CobroIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_intCobroAutoliquidacionID };
            DbCommand cmd = db.GetStoredProcCommand("GEN_CONSULTA_COBRO_AUTOLIQUIDACION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado.Tables.Count > 0)
            {

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    objIdentity.IdCobro = Convert.ToDecimal(dsResultado.Tables[0].Rows[0]["COB_ID"]);
                    objIdentity.NumSILPA = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_SILPA"]);
                    objIdentity.NumExpediente = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_EXPEDIENTE"]);
                    objIdentity.NumReferencia = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_REFERENCIA"]);
                    objIdentity.FechaExpedicion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_EXPEDICION"]);
                    objIdentity.FechaVencimiento = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_VENCIMIENTO"]);
                    objIdentity.CodigoBarras = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_CODIGO_BARRAS"]);
                    objIdentity.RutaArchivo = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_RUTA_ARCHIVOS"]);
                    objIdentity.NumSolicitud = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["SOL_ID"]);
                    objIdentity.EstadoCobro = dsResultado.Tables[0].Rows[0]["ECO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ECO_ID"]);
                    objIdentity.Transaccion = dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"] == DBNull.Value ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"]);
                    objIdentity.FechaTransaccion = dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"].ToString();
                    objIdentity.Banco = dsResultado.Tables[0].Rows[0]["COB_BANCO"].ToString();
                    objIdentity.FechaRecordacion = dsResultado.Tables[0].Rows[0]["COB_FECHA_RECORDACION"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_FECHA_RECORDACION"].ToString();
                    objIdentity.IndicadorProcesoField = dsResultado.Tables[0].Rows[0]["NOMBRE_PROYECTO"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["NOMBRE_PROYECTO"].ToString();
                    objIdentity.OrigenCobro = dsResultado.Tables[0].Rows[0]["COB_ORIGEN"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_ORIGEN"].ToString();
                    objIdentity.FechaPagoOportuno = (dsResultado.Tables[0].Rows[0]["COB_FECHA_PAGO_OPORTUNO"] != DBNull.Value ? Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_PAGO_OPORTUNO"]) : default(DateTime));
                    objIdentity.AutoridaAmbientalID = (dsResultado.Tables[0].Rows[0]["AUT_ID"] != DBNull.Value ? Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]) : 0);
                    objIdentity.SolicitanteID = (dsResultado.Tables[0].Rows[0]["SOLICITANTE_ID"] != DBNull.Value ? Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOLICITANTE_ID"]) : 0);

                    //Se agrega el Concepto
                    ConceptoIdentity _objConcepto = new ConceptoIdentity();
                    _objConcepto.IDConcepto = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["CON_ID"]);

                    //Se agrega la lista de detalles
                    DetalleCobroDalc _detalle = new DetalleCobroDalc();
                    List<DetalleCobroIdentity> _listaDetalles = new List<DetalleCobroIdentity>();
                    _listaDetalles = _detalle.ListarDetalles(objIdentity.IdCobro);
                    objIdentity.ListaConceptoCobro = _listaDetalles;
                    PermisosCobroDalc _permiso = new PermisosCobroDalc();
                    objIdentity.Permisos = _permiso.ObtenerPermisosCobro(Convert.ToInt64(objIdentity.IdCobro));
                }
            }

        }


        /// <summary>
        /// Obtener el listado de cobros vencidos pertenecientes a un origen especifico
        /// </summary>
        /// <param name="p_strOrigenCobro">string con el origen del cobro</param>
        /// <returns>List con la información de los cobros vencidos</returns>
        public List<CobroIdentity> ObtenerListadoCobrosVencidos(string p_strOrigenCobro)
        {
            CobroIdentity objIdentity = null;
            List<CobroIdentity> lstCobrosVencidos = null;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_strOrigenCobro };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBROS_VENCIDOS", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);           

            //Verificar que se obtengan datos
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                //Crear listado
                lstCobrosVencidos = new List<CobroIdentity>();

                //Ciclo que carga los datos de los cobros
                foreach (DataRow objCobro in dsResultado.Tables[0].Rows)
                {
                    //Cargar datos
                    objIdentity = new CobroIdentity();
                    objIdentity.IdCobro = Convert.ToDecimal(dsResultado.Tables[0].Rows[0]["COB_ID"]);
                    objIdentity.NumSILPA = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_SILPA"]);
                    objIdentity.NumExpediente = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_EXPEDIENTE"]);
                    objIdentity.NumReferencia = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_REFERENCIA"]);
                    objIdentity.FechaExpedicion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_EXPEDICION"]);
                    objIdentity.FechaVencimiento = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_VENCIMIENTO"]);
                    objIdentity.CodigoBarras = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_CODIGO_BARRAS"]);
                    objIdentity.RutaArchivo = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_RUTA_ARCHIVOS"]);
                    objIdentity.NumSolicitud = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["SOL_ID"]);
                    objIdentity.EstadoCobro = dsResultado.Tables[0].Rows[0]["ECO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ECO_ID"]);
                    objIdentity.Transaccion = dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"] == DBNull.Value ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"]);
                    objIdentity.FechaTransaccion = dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"].ToString();
                    objIdentity.Banco = dsResultado.Tables[0].Rows[0]["COB_BANCO"].ToString();
                    objIdentity.FechaRecordacion = dsResultado.Tables[0].Rows[0]["COB_FECHA_RECORDACION"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_FECHA_RECORDACION"].ToString();                    
                    objIdentity.OrigenCobro = dsResultado.Tables[0].Rows[0]["COB_ORIGEN"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_ORIGEN"].ToString();
                    objIdentity.FechaPagoOportuno = (dsResultado.Tables[0].Rows[0]["COB_FECHA_PAGO_OPORTUNO"] != DBNull.Value ? Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_PAGO_OPORTUNO"]) : default(DateTime));

                    //Agregar al listado
                    lstCobrosVencidos.Add(objIdentity);
                }
            }

            return lstCobrosVencidos;
        }



        /// <summary>
        /// Obtener la información de cobro perteneciente a la referencia especificada
        /// </summary>
        /// <param name="p_strNumeroReferencia">string con el número de referencia</param>
        /// <returns>CobroIdentity con la información del cobro</returns>
        public CobroIdentity ObtenerCobroTransaccion(string p_strNumeroReferencia)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_strNumeroReferencia };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBRO_POR_TRANSACCION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            CobroIdentity objIdentity = null;

            if (dsResultado.Tables.Count > 0)
            {

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    objIdentity = new CobroIdentity();
                    objIdentity.IdCobro = Convert.ToDecimal(dsResultado.Tables[0].Rows[0]["COB_ID"]);
                    objIdentity.NumSILPA = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_SILPA"]);
                    objIdentity.NumExpediente = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_EXPEDIENTE"]);
                    objIdentity.NumReferencia = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_REFERENCIA"]);
                    objIdentity.FechaExpedicion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_EXPEDICION"]);
                    objIdentity.FechaVencimiento = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_VENCIMIENTO"]);
                    objIdentity.CodigoBarras = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_CODIGO_BARRAS"]);
                    objIdentity.RutaArchivo = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_RUTA_ARCHIVOS"]);
                    objIdentity.NumSolicitud = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["SOL_ID"]);
                    objIdentity.EstadoCobro = dsResultado.Tables[0].Rows[0]["ECO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ECO_ID"]);
                    objIdentity.Transaccion = dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"] == DBNull.Value ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"]);
                    objIdentity.FechaTransaccion = dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"]).ToString("dd/MM/yyyy");
                    objIdentity.Banco = dsResultado.Tables[0].Rows[0]["COB_BANCO"].ToString();
                    objIdentity.FechaRecordacion = dsResultado.Tables[0].Rows[0]["COB_FECHA_RECORDACION"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_FECHA_RECORDACION"].ToString();
                    objIdentity.IndicadorProcesoField = dsResultado.Tables[0].Rows[0]["NOMBRE_PROYECTO"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["NOMBRE_PROYECTO"].ToString();
                    objIdentity.OrigenCobro = dsResultado.Tables[0].Rows[0]["COB_ORIGEN"] == DBNull.Value ? string.Empty : dsResultado.Tables[0].Rows[0]["COB_ORIGEN"].ToString();
                    objIdentity.FechaTransaccionPSE = dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION_PSE"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION_PSE"]);

                    //Se agrega el Concepto
                    ConceptoIdentity _objConcepto = new ConceptoIdentity();
                    _objConcepto.IDConcepto = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["CON_ID"]);

                    //Se agrega la lista de detalles
                    DetalleCobroDalc _detalle = new DetalleCobroDalc();
                    List<DetalleCobroIdentity> _listaDetalles = new List<DetalleCobroIdentity>();
                    _listaDetalles = _detalle.ListarDetalles(objIdentity.IdCobro);
                    objIdentity.ListaConceptoCobro = _listaDetalles;
                    PermisosCobroDalc _permiso = new PermisosCobroDalc();
                    objIdentity.Permisos = _permiso.ObtenerPermisosCobro(Convert.ToInt64(objIdentity.IdCobro));
                }
            }

            return objIdentity;
        }

        public DataSet ListarCobros(CobroIdentity objIdentity)
        {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IdCobro, objIdentity.NumSILPA, objIdentity.NumExpediente, objIdentity.FechaExpedicion, objIdentity.ConceptoCobro.IDConcepto, objIdentity.NumSolicitud };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBRO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

        }

        public CobroType ConsultarDatosPago(string numReferencia)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numReferencia };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBRO_DATOS_PAGO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            CobroType objType = new CobroType();          
            objType.numSILPA = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_SILPA"]);
            objType.numExpediente = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_EXPEDIENTE"]);
            objType.numFormulario = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_REFERENCIA"]);
            objType.estadoCobro = dsResultado.Tables[0].Rows[0]["ECO_ID"].ToString() == string.Empty ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ECO_ID"]);
            objType.transaccion = dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"].ToString() == string.Empty ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"]);
            objType.fechaTransaccion = dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"].ToString();
            objType.banco = dsResultado.Tables[0].Rows[0]["COB_BANCO"].ToString();
           
            return objType;
        }

        //17-jun-2010 - aegb: se agrega parametro para consulta de todos los registros pagados
        public CobroType ConsultarDatosPago(string numReferencia, string codigoExpediente, string estado)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numReferencia, codigoExpediente, estado };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBRO_DATOS_PAGO_ESTADO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            CobroType objType = new CobroType();
            objType.numSILPA = string.Empty;
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                objType.numSILPA = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_SILPA"]);
                objType.numExpediente = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_NUMERO_EXPEDIENTE"]);
                objType.numFormulario = Convert.ToString(dsResultado.Tables[0].Rows[0]["COB_REFERENCIA"]);
                objType.estadoCobro = dsResultado.Tables[0].Rows[0]["ECO_ID"].ToString() == string.Empty ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ECO_ID"]);
                objType.transaccion = dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"].ToString() == string.Empty ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COB_TRANSACCION"]);
                objType.fechaTransaccion = dsResultado.Tables[0].Rows[0]["COB_FECHA_TRANSACCION"].ToString();
                objType.banco = dsResultado.Tables[0].Rows[0]["COB_BANCO"].ToString();
            }

            return objType;
        }      


        /// <summary>
        /// Lista el cobro basado en un ID
        /// </summary>
        /// <param name="idCobro">ID del Cobro</param>
        /// <returns></returns>
        public DataSet ListarCobros(Nullable<decimal> idCobro)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { idCobro, null, null, null, null, null };

                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBRO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
           
        }

       
        /// <summary>
        /// Inserta un nuevo cobro
        /// </summary>
        /// <param name="objIdentity">CobroIdentity con la información de cobro a insertar. Una vez insertado se actualiza el id del cobro.</param>
        public void InsertarCobro(ref CobroIdentity objIdentity)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("GEN_INSERT_COBRO");
                objDataBase.AddInParameter(objCommand, "@P_COB_NUMERO_SILPA", DbType.String, objIdentity.NumSILPA);
                objDataBase.AddInParameter(objCommand, "@P_COB_NUMERO_EXPEDIENTE", DbType.String, objIdentity.NumExpediente);
                objDataBase.AddInParameter(objCommand, "@P_COB_REFERENCIA", DbType.String, objIdentity.NumReferencia);
                objDataBase.AddInParameter(objCommand, "@P_COB_FECHA_EXPEDICION", DbType.DateTime, objIdentity.FechaExpedicion);
                objDataBase.AddInParameter(objCommand, "@P_COB_FECHA_VENCIMIENTO", DbType.DateTime, objIdentity.FechaVencimiento);
                objDataBase.AddInParameter(objCommand, "@P_CON_ID", DbType.Int32, objIdentity.ConceptoCobro.IDConcepto);
                objDataBase.AddInParameter(objCommand, "@P_COB_CODIGO_BARRAS", DbType.String, objIdentity.CodigoBarras);
                objDataBase.AddInParameter(objCommand, "@P_COB_RUTA_ARCHIVOS", DbType.String, objIdentity.RutaArchivo);
                objDataBase.AddInParameter(objCommand, "@P_SOL_ID", DbType.Int64, objIdentity.NumSolicitud);
                objDataBase.AddInParameter(objCommand, "@P_COB_ORIGEN", DbType.String, (!string.IsNullOrEmpty(objIdentity.OrigenCobro) ? objIdentity.OrigenCobro : OrigenCobroAttribute.GetStringValue(EnumOrigenCobro.COBRO)));
                if(objIdentity.FechaPagoOportuno != default(DateTime))
                    objDataBase.AddInParameter(objCommand, "@P_COB_FECHA_PAGO_OPORTUNO", DbType.DateTime, objIdentity.FechaPagoOportuno);

                //Ejecutar comando
                objIdentity.IdCobro = Convert.ToDecimal(objDataBase.ExecuteScalar(objCommand));

            }
            catch (Exception exc)
            {
                //Escalar error
                throw exc;
            }            
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }
        }


        /// <summary>
        /// Actualiza la informacion de cobro cuando se efectua el pago
        /// </summary>
        /// <param name="objIdentity"></param>
        public void ActualizarCobroEstado(CobroIdentity objIdentity)
        {
            SqlDatabase objDatabase = null;
            DbCommand objCmd = null;

            //Cargar conexión
            objDatabase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            //Realizar consulta
            objCmd = objDatabase.GetStoredProcCommand("GEN_UPDATE_COBRO_ESTADO");
            objDatabase.AddInParameter(objCmd, "@P_REFERENCIA", DbType.String, objIdentity.NumReferencia);            
            objDatabase.AddInParameter(objCmd, "@P_TRANSACCION", DbType.Int64, objIdentity.Transaccion);
            objDatabase.AddInParameter(objCmd, "@P_ORIGEN", DbType.String, objIdentity.OrigenLlamadoPSE);
            objDatabase.AddInParameter(objCmd, "@P_SERVICIO", DbType.String, objIdentity.ServicioLlamadoPSE);
            objDatabase.AddInParameter(objCmd, "@P_RESULTADO", DbType.String, objIdentity.ResultadoServicioLlamadoPSE);
            if(!string.IsNullOrEmpty(objIdentity.EstadoPSE))
                objDatabase.AddInParameter(objCmd, "@P_ESTADO_PSE", DbType.String, objIdentity.EstadoPSE);
            objDatabase.AddInParameter(objCmd, "@P_ESTADO", DbType.Int32, objIdentity.EstadoCobro);
            if(objIdentity.FechaTransaccionBancaria != default(DateTime))
                objDatabase.AddInParameter(objCmd, "@P_FECHA_TRANSACCION_BANCARIA", DbType.DateTime, objIdentity.FechaTransaccionBancaria);            
            objDatabase.ExecuteNonQuery(objCmd);     
        }


        /// <summary>
        /// Actualiza la informacion del cobro cuando se envia correo de recordacion
        /// </summary>
        /// <param name="objIdentity"></param>
        public void ActualizarCobroRecordacion(CobroIdentity objIdentity)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            //Se agrega el cobro
            object[] parametros = new object[] { objIdentity.NumReferencia };

            DbCommand cmd = db.GetStoredProcCommand("GEN_UPDATE_COBRO_RECORDACION", parametros);
            objIdentity.IdCobro = Convert.ToDecimal(db.ExecuteScalar(cmd));
            //objIdentity.IdCobro = Int64.Parse(cmd.Parameters["@PER_ID"].Value.ToString());  

        }


        public DataSet ListarCobrosPago(CobroIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.IdCobro, objIdentity.NumSILPA, objIdentity.NumExpediente, objIdentity.NumSolicitud, objIdentity.NumReferencia };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBROS", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);

        }

        public DataSet ListarCobrosPago(CobroIdentity objIdentity, int idUsuario)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.IdCobro, objIdentity.NumSILPA, objIdentity.NumExpediente, objIdentity.NumSolicitud, objIdentity.NumReferencia, idUsuario };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBROS", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);

        }

        public List<CobroPSE> ListarCobrosPagoPSE(CobroIdentity cobroIdentity)
        {

            List<CobroPSE> objListCobroPse = new List<CobroPSE>();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { cobroIdentity.NumExpediente };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBROS_PSE", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            foreach (DataRow dr in dsResultado.Tables[0].Rows)
            {
                CobroPSE objCobroPse = new CobroPSE();
                objCobroPse.CobReferencia = dr["COB_REFERENCIA"].ToString();
                objCobroPse.CobFechaTransaccion = DateTime.Parse(dr["COB_FECHA_TRANSACCION"].ToString());

                objListCobroPse.Add(objCobroPse);
            }

            return objListCobroPse;
        }

        /// <summary>
        /// Retorna el listado de transacciones pendientes de pago
        /// </summary>
        /// <returns>List con la informacion de las transacciones pendientes</returns>
        public List<TransaccionPSEIdentity> ListaPagosPendientes()
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_COBRO_PENDIENTES");
            DataSet objInformacionTransaccion = db.ExecuteDataSet(cmd);
            List<TransaccionPSEIdentity> listTransacciones = null;
            TransaccionPSEIdentity objTransaccion = null;

            //Validar que se obtengan datos
            if (objInformacionTransaccion != null && objInformacionTransaccion.Tables.Count > 0 && objInformacionTransaccion.Tables[0].Rows.Count > 0)
            {
                listTransacciones = new List<TransaccionPSEIdentity>();

                //Ciclo que carga datos
                foreach (DataRow dr in objInformacionTransaccion.Tables[0].Rows)
                {
                    objTransaccion = new TransaccionPSEIdentity
                    {
                        TransaccionPSEID = Convert.ToInt32(objInformacionTransaccion.Tables[0].Rows[0]["TRANSACCION_PSE_ID"]),
                        CobroID = Convert.ToInt64(objInformacionTransaccion.Tables[0].Rows[0]["COB_ID"]),
                        NumeroReferencia = objInformacionTransaccion.Tables[0].Rows[0]["NUMERO_REFERENCIA"].ToString(),
                        NumeroSilpa = objInformacionTransaccion.Tables[0].Rows[0]["NUMERO_SILPA"].ToString(),
                        CodigoPSEEntidad = objInformacionTransaccion.Tables[0].Rows[0]["COB_CODIGO_ENTIDAD"].ToString(),
                        FechaSolicitud = Convert.ToDateTime(objInformacionTransaccion.Tables[0].Rows[0]["COB_FECHA_SOLICITUD"]),
                        TipoPersona = objInformacionTransaccion.Tables[0].Rows[0]["COB_TIPO_PERSONA"].ToString(),
                        Banco = objInformacionTransaccion.Tables[0].Rows[0]["COB_BANCO"].ToString(),
                        Valor = Convert.ToDecimal(objInformacionTransaccion.Tables[0].Rows[0]["COB_VALOR"]),
                        Referencia1 = objInformacionTransaccion.Tables[0].Rows[0]["COB_REFERENCIA1"].ToString(),
                        Referencia2 = objInformacionTransaccion.Tables[0].Rows[0]["COB_REFERENCIA2"].ToString(),
                        Referencia3 = objInformacionTransaccion.Tables[0].Rows[0]["COB_REFERENCIA3"].ToString(),
                        IPTransaccion = objInformacionTransaccion.Tables[0].Rows[0]["COB_IP_TRANSACCION_PSE"].ToString(),
                        RazonSocialComercio = objInformacionTransaccion.Tables[0].Rows[0]["COB_RAZON_SOCIAL"].ToString(),
                        DescripcionTransaccion = objInformacionTransaccion.Tables[0].Rows[0]["COB_DESCRIPCION_TRANSACCION"].ToString(),
                        UrlRetorno = objInformacionTransaccion.Tables[0].Rows[0]["COB_URL_RETORNO"].ToString(),
                        NumeroTransaccion = (objInformacionTransaccion.Tables[0].Rows[0]["COB_TRANSACCION"] != System.DBNull.Value ? Convert.ToInt64(objInformacionTransaccion.Tables[0].Rows[0]["COB_TRANSACCION"]) : 0),
                        UrlPSE = (objInformacionTransaccion.Tables[0].Rows[0]["COB_URL_PSE"] != System.DBNull.Value ? objInformacionTransaccion.Tables[0].Rows[0]["COB_URL_PSE"].ToString() : ""),
                        Estado = (objInformacionTransaccion.Tables[0].Rows[0]["ECO_ID"] != System.DBNull.Value ? Convert.ToInt32(objInformacionTransaccion.Tables[0].Rows[0]["ECO_ID"]) : 0),
                        FechaRegistroTransacion = Convert.ToDateTime(objInformacionTransaccion.Tables[0].Rows[0]["COB_FECHA_REGISTRO_TRANSACCION"]),
                        FechaUltimaActualizacion = Convert.ToDateTime(objInformacionTransaccion.Tables[0].Rows[0]["COB_FECHA_ULTIMA_MODIFICACION"])
                    };

                    listTransacciones.Add(objTransaccion);
                }
            }

            return listTransacciones;
        }


        /// <summary>
        /// Retornar el listado de transacciones PSE realizados por el usuario que cumplan con las condiciones de busqueda
        /// </summary>
        /// <param name="p_intUsuarioId">int con el identificador del usuario</param>
        /// <param name="p_strNumeroVital">string con el numero vital. Opcional</param>
        /// <param name="p_lngNumeroTransaccion">long con el numero de transaccion. Opcional (-1)</param>
        /// <param name="p_objFechaInicial">DateTime con la fecha inicial. Opcional</param>
        /// <param name="p_objFechaFinal">DateTime con la fecha final. Opcional</param>
        /// <returns>DataTable con la información de las transacciones</returns>
        public DataTable ListarTransaccionesPSEUsuario(int p_intUsuarioId, string p_strNumeroVital, long p_lngNumeroTransaccion, DateTime p_objFechaInicial, DateTime p_objFechaFinal)
        {
            SqlDatabase objDatabase = null;
            DbCommand objCmd = null;
            DataSet objInfoTransacciones = null;
            DataTable objTransacciones = null;

            //Cargar conexión
            objDatabase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            //Realizar consulta
            objCmd = objDatabase.GetStoredProcCommand("GEN_LISTAR_TRANSACCIONES_PSE_USUARIO");
            objDatabase.AddInParameter(objCmd, "@P_SOL_ID", DbType.Int32, p_intUsuarioId);
            if (!string.IsNullOrEmpty(p_strNumeroVital))
                objDatabase.AddInParameter(objCmd, "@P_NUMERO_SILPA", DbType.String, p_strNumeroVital);
            if (p_lngNumeroTransaccion > 0)
                objDatabase.AddInParameter(objCmd, "@P_TRANSACCION", DbType.Int64, p_lngNumeroTransaccion);
            if (p_objFechaInicial != default(DateTime))
                objDatabase.AddInParameter(objCmd, "@P_FECHA_DESDE", DbType.DateTime, p_objFechaInicial);
            if (p_objFechaFinal != default(DateTime))
                objDatabase.AddInParameter(objCmd, "@P_FECHA_HASTA", DbType.DateTime, p_objFechaFinal);
            objInfoTransacciones = objDatabase.ExecuteDataSet(objCmd);

            if (objInfoTransacciones != null && objInfoTransacciones.Tables != null && objInfoTransacciones.Tables.Count > 0 && objInfoTransacciones.Tables[0].Rows.Count > 0)
            {
                objTransacciones = objInfoTransacciones.Tables[0];
            }

            return objTransacciones;

        }


        /// <summary>
        /// Crea un nuevo registro de transacción PSE con los datos especificados
        /// </summary>
        /// <param name="p_objTransaccioPSE">TransaccionPSEIdentity con la informacion de la transacción</param>
        /// <returns>long con el identificador local de la transacción</returns>
        public long CrearTransaccionPSE(TransaccionPSEIdentity p_objTransaccioPSE)
        {
            SqlDatabase objDatabase = null;
            DbCommand objCmd = null;
            long lngTransaccionPSEID = 0;

            //Cargar conexión
            objDatabase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            //Realizar consulta
            objCmd = objDatabase.GetStoredProcCommand("MOH_INSERTAR_TRANSACCION_PSE");
            
            objDatabase.AddInParameter(objCmd, "@P_COB_ID", DbType.Int64, p_objTransaccioPSE.CobroID);
            objDatabase.AddInParameter(objCmd, "@P_COB_CODIGO_ENTIDAD", DbType.String, p_objTransaccioPSE.CodigoPSEEntidad);
            objDatabase.AddInParameter(objCmd, "@P_COB_FECHA_SOLICITUD", DbType.DateTime, p_objTransaccioPSE.FechaSolicitud);
            objDatabase.AddInParameter(objCmd, "@P_COB_TIPO_PERSONA", DbType.String, p_objTransaccioPSE.TipoPersona);
            objDatabase.AddInParameter(objCmd, "@P_COB_BANCO", DbType.String, p_objTransaccioPSE.Banco);
            objDatabase.AddInParameter(objCmd, "@P_COB_VALOR", DbType.Decimal, p_objTransaccioPSE.Valor);
            objDatabase.AddInParameter(objCmd, "@P_COB_REFERENCIA1", DbType.String, p_objTransaccioPSE.Referencia1);
            objDatabase.AddInParameter(objCmd, "@P_COB_REFERENCIA2", DbType.String, p_objTransaccioPSE.Referencia2);
            objDatabase.AddInParameter(objCmd, "@P_COB_REFERENCIA3", DbType.String, p_objTransaccioPSE.Referencia3);
            objDatabase.AddInParameter(objCmd, "@P_COB_IP_TRANSACCION_PSE", DbType.String, p_objTransaccioPSE.IPTransaccion);
            objDatabase.AddInParameter(objCmd, "@P_COB_RAZON_SOCIAL", DbType.String, p_objTransaccioPSE.RazonSocialComercio);
            objDatabase.AddInParameter(objCmd, "@P_COB_DESCRIPCION_TRANSACCION", DbType.String, p_objTransaccioPSE.DescripcionTransaccion);
            objDatabase.AddInParameter(objCmd, "@P_COB_URL_RETORNO", DbType.String, p_objTransaccioPSE.UrlRetorno);
            if (p_objTransaccioPSE.NumeroTransaccion > 0)
                objDatabase.AddInParameter(objCmd, "@P_COB_TRANSACCION", DbType.Int64, p_objTransaccioPSE.NumeroTransaccion);
            if (!string.IsNullOrEmpty(p_objTransaccioPSE.UrlPSE))
                objDatabase.AddInParameter(objCmd, "@P_COB_URL_PSE", DbType.String, p_objTransaccioPSE.UrlPSE);
            if (p_objTransaccioPSE.Estado > 0)
                objDatabase.AddInParameter(objCmd, "@P_ECO_ID", DbType.Int32, p_objTransaccioPSE.Estado);

            //Ejecuta sentencia
            using (IDataReader reader = objDatabase.ExecuteReader(objCmd))
            {
                //Cargar id del certificado
                if (reader.Read())
                {
                    lngTransaccionPSEID = Convert.ToInt64(reader["TRANSACCION_PSE_ID"]);
                }
            }

            return lngTransaccionPSEID;
        }


        /// <summary>
        /// Actualizar la información de un atrnsacción PSE
        /// </summary>
        /// <param name="p_objTransaccioPSE">TransaccionPSEIdentity con la informacion de la transacción</param>        
        public void ActualizarTransaccionPSE(TransaccionPSEIdentity p_objTransaccioPSE)
        {
            SqlDatabase objDatabase = null;
            DbCommand objCmd = null;

            //Cargar conexión
            objDatabase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            //Realizar consulta
            objCmd = objDatabase.GetStoredProcCommand("MOH_ACTUALIZAR_TRANSACCION_PSE");

            objDatabase.AddInParameter(objCmd, "@P_TRANSACCION_PSE_ID", DbType.Int64, p_objTransaccioPSE.TransaccionPSEID);
            if (p_objTransaccioPSE.NumeroTransaccion > 0)
                objDatabase.AddInParameter(objCmd, "@P_COB_TRANSACCION", DbType.Int64, p_objTransaccioPSE.NumeroTransaccion);
            if (!string.IsNullOrEmpty(p_objTransaccioPSE.UrlPSE))
                objDatabase.AddInParameter(objCmd, "@P_COB_URL_PSE", DbType.String, p_objTransaccioPSE.UrlPSE);
            if (p_objTransaccioPSE.Estado > 0)
                objDatabase.AddInParameter(objCmd, "@P_ECO_ID", DbType.Int32, p_objTransaccioPSE.Estado);
            objDatabase.ExecuteNonQuery(objCmd);            
        }


        /// <summary>
        /// Crea un nuevo registro de detalle de transacción PSE con los datos especificados
        /// </summary>
        /// <param name="p_objDetalleTransaccioPSE">DetalleTransaccionPSEIdentity con la informacion de la transacción</param>
        public void CrearDetalleTransaccionPSE(DetalleTransaccionPSEIdentity p_objDetalleTransaccioPSE)
        {
            SqlDatabase objDatabase = null;
            DbCommand objCmd = null;
            //Cargar conexión
            objDatabase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            //Realizar consulta
            objCmd = objDatabase.GetStoredProcCommand("MOH_INSERTAR_DETALLE_TRANSACCION_PSE");

            objDatabase.AddInParameter(objCmd, "@P_TRANSACCION_PSE_ID", DbType.Int64, p_objDetalleTransaccioPSE.TransaccionPSEID);
            objDatabase.AddInParameter(objCmd, "@P_COB_ORIGEN", DbType.String, p_objDetalleTransaccioPSE.Origen);
            objDatabase.AddInParameter(objCmd, "@P_COB_SERVICIO", DbType.String, p_objDetalleTransaccioPSE.Servicio);
            objDatabase.AddInParameter(objCmd, "@P_COB_RESULTADO", DbType.String, p_objDetalleTransaccioPSE.ResultadoPSE);
            if (!string.IsNullOrEmpty(p_objDetalleTransaccioPSE.EstadoPSE))
                objDatabase.AddInParameter(objCmd, "@P_COB_ESTADO_PSE", DbType.String, p_objDetalleTransaccioPSE.EstadoPSE);
            objDatabase.AddInParameter(objCmd, "@P_ECO_ID", DbType.Int32, p_objDetalleTransaccioPSE.EstadoID);
            if (p_objDetalleTransaccioPSE.FechaTransaccionBancaria != default(DateTime))
                objDatabase.AddInParameter(objCmd, "@P_FECHA_TRANSACCION_BANCARIA", DbType.DateTime, p_objDetalleTransaccioPSE.FechaTransaccionBancaria);
            objDatabase.ExecuteNonQuery(objCmd);

        }


        /// <summary>
        /// Consultar la información de la transacción por CUS
        /// </summary>
        /// <param name="p_lngCUS">long con el identificador unico CUS</param>
        /// <returns>TransaccionPSEIdentity con la información de la transacción</returns>
        public TransaccionPSEIdentity ConsultarInformacionTransaccion(long p_lngCUS)
        {
            SqlDatabase objDatabase = null;
            DbCommand objCmd = null;
            TransaccionPSEIdentity objTransaccion = null;
            DataSet objInformacionTransaccion = null;

            //Cargar conexión
            objDatabase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            //Realizar consulta
            objCmd = objDatabase.GetStoredProcCommand("MOH_CONSULTAR_TRANSACCION_PSE_CUS");
            objDatabase.AddInParameter(objCmd, "@P_COB_TRANSACCION", DbType.Int64, p_lngCUS);
            objInformacionTransaccion = objDatabase.ExecuteDataSet(objCmd);

            //Cargar datos
            if (objInformacionTransaccion != null && objInformacionTransaccion.Tables.Count > 0 && objInformacionTransaccion.Tables[0].Rows.Count > 0)
            {
                objTransaccion = new TransaccionPSEIdentity
                {
                    TransaccionPSEID = Convert.ToInt32(objInformacionTransaccion.Tables[0].Rows[0]["TRANSACCION_PSE_ID"]),
                    CobroID = Convert.ToInt64(objInformacionTransaccion.Tables[0].Rows[0]["COB_ID"]),
                    NumeroReferencia = objInformacionTransaccion.Tables[0].Rows[0]["NUMERO_REFERENCIA"].ToString(),
                    NumeroSilpa = objInformacionTransaccion.Tables[0].Rows[0]["NUMERO_SILPA"].ToString(),
                    CodigoPSEEntidad = objInformacionTransaccion.Tables[0].Rows[0]["COB_CODIGO_ENTIDAD"].ToString(),
                    FechaSolicitud = Convert.ToDateTime(objInformacionTransaccion.Tables[0].Rows[0]["COB_FECHA_SOLICITUD"]),
                    TipoPersona = objInformacionTransaccion.Tables[0].Rows[0]["COB_TIPO_PERSONA"].ToString(),
                    Banco = objInformacionTransaccion.Tables[0].Rows[0]["COB_BANCO"].ToString(),
                    Valor = Convert.ToDecimal(objInformacionTransaccion.Tables[0].Rows[0]["COB_VALOR"]),
                    Referencia1 = objInformacionTransaccion.Tables[0].Rows[0]["COB_REFERENCIA1"].ToString(),
                    Referencia2 = objInformacionTransaccion.Tables[0].Rows[0]["COB_REFERENCIA2"].ToString(),
                    Referencia3 = objInformacionTransaccion.Tables[0].Rows[0]["COB_REFERENCIA3"].ToString(),
                    IPTransaccion = objInformacionTransaccion.Tables[0].Rows[0]["COB_IP_TRANSACCION_PSE"].ToString(),
                    RazonSocialComercio = objInformacionTransaccion.Tables[0].Rows[0]["COB_RAZON_SOCIAL"].ToString(),
                    DescripcionTransaccion = objInformacionTransaccion.Tables[0].Rows[0]["COB_DESCRIPCION_TRANSACCION"].ToString(),
                    UrlRetorno = objInformacionTransaccion.Tables[0].Rows[0]["COB_URL_RETORNO"].ToString(),
                    NumeroTransaccion = (objInformacionTransaccion.Tables[0].Rows[0]["COB_TRANSACCION"] != System.DBNull.Value ? Convert.ToInt64(objInformacionTransaccion.Tables[0].Rows[0]["COB_TRANSACCION"]) : 0),
                    UrlPSE = (objInformacionTransaccion.Tables[0].Rows[0]["COB_URL_PSE"] !=  System.DBNull.Value ? objInformacionTransaccion.Tables[0].Rows[0]["COB_URL_PSE"].ToString() : ""),
                    Estado = (objInformacionTransaccion.Tables[0].Rows[0]["ECO_ID"] != System.DBNull.Value ? Convert.ToInt32(objInformacionTransaccion.Tables[0].Rows[0]["ECO_ID"]) : 0),
                    FechaRegistroTransacion = Convert.ToDateTime(objInformacionTransaccion.Tables[0].Rows[0]["COB_FECHA_REGISTRO_TRANSACCION"]),
                    FechaUltimaActualizacion = Convert.ToDateTime(objInformacionTransaccion.Tables[0].Rows[0]["COB_FECHA_ULTIMA_MODIFICACION"]),
                    OrigenCobro = (objInformacionTransaccion.Tables[0].Rows[0]["COB_ORIGEN"] != System.DBNull.Value ? objInformacionTransaccion.Tables[0].Rows[0]["COB_ORIGEN"].ToString() : "")
                };
            }

            return objTransaccion;
        }

    }
}
