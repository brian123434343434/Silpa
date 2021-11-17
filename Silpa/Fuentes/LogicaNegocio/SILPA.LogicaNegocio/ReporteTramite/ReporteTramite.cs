using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.ReporteTramite;
using System.Data;
using SILPA.LogicaNegocio.Generico;

namespace SILPA.LogicaNegocio.ReporteTramite
{
    public class ReporteTramite
    {

        public ReporteTramiteEntity ReporteEntity;
        private ReporteTramiteDalc ReporteDalc;
               
        
        /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public ReporteTramite()
        {
            _objConfiguracion = new Configuracion();
            ReporteDalc = new ReporteTramiteDalc();
            ReporteEntity = new ReporteTramiteEntity();

        }


        public DataView ListarReporteTramite(Nullable<DateTime> dateFechaInicial, 
            Nullable<DateTime> dateFechaFinal, Nullable<int> intTipoTramite, 
            Nullable<int> intAutoridadAmbiental, string strNombre_Proyecto)
        {
            ReporteTramiteDalc obj = new ReporteTramiteDalc();
            FormularioDalc objFormulario = new FormularioDalc();
            DataSet dsDatosTramite = new DataSet();
            DataSet dsNombreProyecto = new DataSet();
            DataView dv = new DataView(); 

            int? intProcessInstance = null;
           
            dsDatosTramite = obj.ListarReporteTramite(dateFechaInicial, dateFechaFinal, intTipoTramite, intAutoridadAmbiental, intProcessInstance);
            dsDatosTramite.Tables[0].Columns.Add("NOMBRE_PROYECTO");

            for(int i=0;i<dsDatosTramite.Tables[0].Rows.Count;i++)
            {
                dsNombreProyecto = objFormulario.ObtenerDatosFormulariosProceso(long.Parse(dsDatosTramite.Tables[0].Rows[i]["SOL_IDPROCESSINSTANCE"].ToString()), "Nombre del Proyecto");

                if (dsNombreProyecto != null)
                {
                    if (dsNombreProyecto.Tables[0].Rows.Count > 0)
                        dsDatosTramite.Tables[0].Rows[i]["NOMBRE_PROYECTO"] = dsNombreProyecto.Tables[0].Rows[0]["VALOR"].ToString().Trim();
                    else
                        dsDatosTramite.Tables[0].Rows[i]["NOMBRE_PROYECTO"] = null;
                }
                else
                {
                    dsDatosTramite.Tables[0].Rows[i]["NOMBRE_PROYECTO"] = null;
                }
            
            }


            if (strNombre_Proyecto != "")
            {
                dv = dsDatosTramite.Tables[0].DefaultView;
                dv.RowFilter = "NOMBRE_PROYECTO LIKE '%" + strNombre_Proyecto + "%'";

            }
            else

            {
                dv = dsDatosTramite.Tables[0].DefaultView;
                dv.RowFilter = "NOMBRE_PROYECTO IS NOT NULL AND NOMBRE_PROYECTO <>' ' ";
            }

            return dv; 

        }

        public bool CrearRelacionExpedienteExpediente(string expedienteId, string expedienteIdRef,string numeroVital)
        {
            return ReporteDalc.CrearRelacionExpedienteExpediente(expedienteId, expedienteIdRef,numeroVital);
        }


        public bool EliminarRelacionExpedienteExpediente(string expedienteId, string expedienteIdRef)
        {
            return ReporteDalc.EliminarRelacionExpedienteExpediente(expedienteId, expedienteIdRef);
        }

        public bool CrearMisTramites(string numeroVital, DateTime fechaCreacion, string descripcion, string pathDocumento, string idExpediente, string etaNombre, int actReposicion, string addNombre,int? actoEjec,int? actoNot,string tipoDocumento,string descripcionActo)
        {
            try
            {
                return ReporteDalc.CrearMisTramites(numeroVital, fechaCreacion, descripcion, pathDocumento, idExpediente, etaNombre, actReposicion, addNombre, actoEjec, actoNot, tipoDocumento, descripcionActo);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Crear Trámites.";
                throw new Exception(strException, ex);
            }
        }


        /// <summary>
        /// Modificar la informacion de un expediente relacionado a un tramite
        /// </summary>
        /// <param name="p_strExpedienteAnteriorID">string con el identificador del expediente anterior</param>
        /// <param name="p_strExpedienteID">string con el identificador del expediente</param>
        /// <param name="p_intNumeroDocumento">int con el numero de documento</param>
        /// <param name="p_strTipoActo">string con el identificador del tipo de acto administrativo</param>
        /// <param name="strDescripcion">string con la descripcion</param>
        /// <param name="p_strEtapa">string con la etapa</param>
        /// <param name="p_strNombre">string con el nombre</param>
        /// <param name="p_strDescripcionActo">string con la descricion del acto administrativo</param>
        /// <returns>bool indicando si se realizo el tramite</returns>
        public bool ModificarDatosExpedienteTramite(string p_strExpedienteAnteriorID, string p_strExpedienteID, int p_intNumeroDocumento, string p_strTipoActo, string strDescripcion, string p_strEtapa, string p_strNombre, string p_strDescripcionActo)
        {
            try
            {
                return ReporteDalc.ModificarDatosExpedienteTramite(p_strExpedienteAnteriorID, p_strExpedienteID, p_intNumeroDocumento, p_strTipoActo, strDescripcion, p_strEtapa, p_strNombre, p_strDescripcionActo);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Crear Trámites.";
                throw new Exception(strException, ex);
            }
        }

        public DataTable Tramites()
        {
            return ReporteDalc.Tramites();
        }

        public DataTable Actividades(int traId)
        {
            return ReporteDalc.Actividades(traId);
        }

        public void InsertarRelacionAlerta(int traId, string nombre, int diaLimite, string caracter, int actId, int docId, string colorLetra, bool negrilla, string colorFondo, string tipoCorreo, string idSolcitante, string correoElectronico,string diasInicioAlerta)
        {
            int _diasInicioAlerta=0;
            if (diasInicioAlerta != "")
                _diasInicioAlerta = int.Parse(diasInicioAlerta);
            ReporteDalc.InsertarRelacion(traId, nombre, diaLimite, caracter, actId, docId, colorLetra, negrilla, colorFondo, tipoCorreo, idSolcitante, correoElectronico, _diasInicioAlerta);            
        }

        public DataTable DocumentosSila(int actId)
        {
            return ReporteDalc.Documentos(actId);
        }


        public String CodigoCondicion(int codId)
        {
            return ReporteDalc.CodigoCondicion(codId);
        }

        public DataTable ReporteXTramite(int traId)
        {
            return ReporteDalc.ReporteXTramite(traId);
        }
        public void EliminarRelacionAlerta(int traId,int actId,int docId)
        {
            ReporteDalc.EliminarRelacionAlerta(traId,actId,docId);
        }

        public DataSet ConsultarFechasCalendatios(string numeroVital)
        {
            return ReporteDalc.ConsultarFechasCalendatios(numeroVital);
        }

        public DataTable CondicionesSinActividades()
        {
            return ReporteDalc.ConsultarCondicionesSinActividades();
        }
    }
}
