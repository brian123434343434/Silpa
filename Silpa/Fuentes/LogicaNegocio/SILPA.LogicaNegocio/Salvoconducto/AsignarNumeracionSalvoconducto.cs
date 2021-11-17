using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class AsignarNumeracionSalvoconducto
    {


        private AsignarNumeracionSalvoconductoDalc vNumeracionSalvoconductoDalc;

        public AsignarNumeracionSalvoconducto()
        {
            vNumeracionSalvoconductoDalc = new AsignarNumeracionSalvoconductoDalc();
        }

        #region atributos
        private int ID_SERIE;
        private int ID_AUT_AMBIENTAL;
        private int SERIE_DESDE;
        private int SERIE_HASTA;
        private int ESTADO_SERIE_ID;
        private string MOTIVO_BLOQUEO;
        private int CNT_SERIES_ALERTA;
        private string NOMBRE_ARCHIVO_CREACION_SERIE;
        private string RUTA_ARCHIVO_CREACION_SERIE;
        private string NOMBRE_ARCHIVO_BLOQUEO_SERIE;
        private string RUTA_ARCHIVO_BLOQUEO_SERIE;
        private string CODIGO_USUARIO;
        private DataTable SERIES_NUMERACION_SALVOCONDUCTO;
        #region variables para la obtencion del numero del salvoconducto que se va a amitir
        private string NUMERO_SALVOCONDUCTO;
        private string MENSAJE;


        //VARIABLES PARA ASIGNAR LA NUMERIACION DEL SALVOCONDUCTO
        public string V_NUMERO_SALVOCONDUCTO
        {
            get
            {
                return NUMERO_SALVOCONDUCTO;
            }

            set
            {
                NUMERO_SALVOCONDUCTO = value;
            }
        }
        public string V_MENSAJE
        {
            get
            {
                return MENSAJE;
            }

            set
            {
                MENSAJE = value;
            }
        }

        

        #endregion

        public int V_ID_SERIE
        {
            get
            {
                return ID_SERIE;
            }

            set
            {
                ID_SERIE = value;
            }
        }

        public int V_ID_AUT_AMBIENTAL
        {
            get
            {
                return ID_AUT_AMBIENTAL;
            }

            set
            {
                ID_AUT_AMBIENTAL = value;
            }
        }

        public int V_SERIE_DESDE
        {
            get
            {
                return SERIE_DESDE;
            }

            set
            {
                SERIE_DESDE = value;
            }
        }

        public int V_SERIE_HASTA
        {
            get
            {
                return SERIE_HASTA;
            }

            set
            {
                SERIE_HASTA = value;
            }
        }

        public int V_ESTADO_SERIE_ID
        {
            get
            {
                return ESTADO_SERIE_ID;
            }

            set
            {
                ESTADO_SERIE_ID = value;
            }
        }

        public string V_MOTIVO_BLOQUEO
        {
            get
            {
                return MOTIVO_BLOQUEO;
            }

            set
            {
                MOTIVO_BLOQUEO = value;
            }
        }

        public int V_CNT_SERIES_ALERTA
        {
            get
            {
                return CNT_SERIES_ALERTA;
            }

            set
            {
                CNT_SERIES_ALERTA = value;
            }
        }

        public string V_NOMBRE_ARCHIVO_CREACION_SERIE
        {
            get
            {
                return NOMBRE_ARCHIVO_CREACION_SERIE;
            }

            set
            {
                NOMBRE_ARCHIVO_CREACION_SERIE = value;
            }
        }

        public string V_RUTA_ARCHIVO_CREACION_SERIE
        {
            get
            {
                return RUTA_ARCHIVO_CREACION_SERIE;
            }

            set
            {
                RUTA_ARCHIVO_CREACION_SERIE = value;
            }
        }

        public string V_NOMBRE_ARCHIVO_BLOQUEO_SERIE
        {
            get
            {
                return NOMBRE_ARCHIVO_BLOQUEO_SERIE;
            }

            set
            {
                NOMBRE_ARCHIVO_BLOQUEO_SERIE = value;
            }
        }

        public string V_RUTA_ARCHIVO_BLOQUEO_SERIE
        {
            get
            {
                return RUTA_ARCHIVO_BLOQUEO_SERIE;
            }

            set
            {
                RUTA_ARCHIVO_BLOQUEO_SERIE = value;
            }
        }

        public string V_CODIGO_USUARIO
        {
            get
            {
                return CODIGO_USUARIO;
            }

            set
            {
                CODIGO_USUARIO = value;
            }
        }

        public DataTable V_SERIES_NUMERACION_SALVOCONDUCTO
        {
            get
            {
                return SERIES_NUMERACION_SALVOCONDUCTO;
            }

            set
            {
                SERIES_NUMERACION_SALVOCONDUCTO = value;
            }
        }


        #endregion

        #region Metodos
        public DataTable ObtenerEstadosSerie()
        {
            DataTable dt = new DataTable();
            dt = vNumeracionSalvoconductoDalc.ListarParametrizacionSalvoconducto("ESTADO_SERIE_ID, DESCRIPCION", "ESTADOS_SERIES_SALVOCONDUCTO", "", "").Tables[0];
            return dt;
        }

        public DataTable ObtenerMotivoBloqueo()
        {
            DataTable dt = new DataTable();
            dt = vNumeracionSalvoconductoDalc.ListarParametrizacionSalvoconducto("ID_TIPO_BLOQUEO, DESCRIPCION", "SAB_TIPO_BLOQUEO_SALVOCONDUCTO", "", "").Tables[0];
            return dt;
        }


        public DataTable ObtenerEstadosSerieActivo()
        {
            DataTable dt = new DataTable();
            dt = vNumeracionSalvoconductoDalc.ListarParametrizacionSalvoconducto("ESTADO_SERIE_ID, DESCRIPCION", "ESTADOS_SERIES_SALVOCONDUCTO", "ESTADO_SERIE_ID = 1", "").Tables[0];
            return dt;
        }

        public DataTable ValidarNumeracionSalvoconducto()
        {
            DataTable dt = new DataTable();
            dt = vNumeracionSalvoconductoDalc.ValidarNumeracionSalvoconducto(ID_SERIE,SERIE_DESDE, SERIE_HASTA).Tables[0];
            return dt;
        }

        public bool GrabarSerie()
        {
            bool resultado = false;
            resultado = vNumeracionSalvoconductoDalc.GrabarSerieDalc(ID_AUT_AMBIENTAL, SERIE_DESDE, SERIE_HASTA, CNT_SERIES_ALERTA, NOMBRE_ARCHIVO_CREACION_SERIE, RUTA_ARCHIVO_CREACION_SERIE,CODIGO_USUARIO);
            return resultado;
        }
        
        public bool EditarSerie()
        {
            bool resultado = false;
            resultado = vNumeracionSalvoconductoDalc.EditarSerieDalc(ID_SERIE, SERIE_DESDE, SERIE_HASTA, CNT_SERIES_ALERTA, NOMBRE_ARCHIVO_CREACION_SERIE, RUTA_ARCHIVO_CREACION_SERIE,CODIGO_USUARIO);
            return resultado;
        }

        public bool BloquearSerie()
        {

            bool resultado = false;
            resultado = vNumeracionSalvoconductoDalc.BloquearSerieDalc(ID_SERIE, ESTADO_SERIE_ID, NOMBRE_ARCHIVO_BLOQUEO_SERIE, RUTA_ARCHIVO_BLOQUEO_SERIE, MOTIVO_BLOQUEO);
            return resultado;
        }

        public string ValidarNumeracion(int ID_SERIE, int SERIE_DESDE, int SERIE_HASTA, int TOTAL)
        {
            string mensaje = "";
            string str_serie_desde = "";
            string str_serie_hasta = "";
            DataTable dt = new DataTable();
            SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _ValidarSerie = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
            _ValidarSerie.ID_SERIE = ID_SERIE;
            _ValidarSerie.V_SERIE_DESDE = SERIE_DESDE;
            _ValidarSerie.V_SERIE_HASTA = SERIE_HASTA;
            dt = _ValidarSerie.ValidarNumeracionSalvoconducto();

            DataTable dt_AutoridadAmbiental = new DataTable();
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            dt_AutoridadAmbiental = _listaAutoridades.ListarAutoridades(null).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {

                var query = from numeracion in dt.AsEnumerable()
                            join listAutAmbiental in dt_AutoridadAmbiental.AsEnumerable()
                            on numeracion.Field<int>("ID_AUT_AMBIENTAL") equals listAutAmbiental.Field<int>("AUT_ID")
                            select new
                            {
                                AUTORIDAD_AMBIENTAL = listAutAmbiental.Field<string>("AUT_NOMBRE"),
                                SERIE_DESDE = numeracion.Field<int>("SERIE_DESDE"),
                                SERIE_HASTA = numeracion.Field<int>("SERIE_HASTA")
                            };

                foreach (var Validacion in query)
                {
                    str_serie_desde = Convert.ToString(Validacion.SERIE_DESDE);
                    str_serie_hasta = Convert.ToString(Validacion.SERIE_HASTA);
                }
       
            }


            if (str_serie_desde.Length > 0 && str_serie_hasta.Length > 0 )
            {
                mensaje = "La Numeracion Hasta ya esta asigada por esta serie: " + str_serie_desde + " - " + str_serie_hasta + " Por favor digite otra numeracion";
            }

            if (mensaje == string.Empty)
            {
                if ((SERIE_HASTA -  SERIE_DESDE) < TOTAL)
                {
                    mensaje = "No se puede reducir la cantidad se salvoconductos solo se permite incrementarlos";
                }
            }


            return mensaje;
        }

        public DataTable ConsultarSerieSalvoConducto(DataTable dt_AutoridadAmbiental, int ID_AUT_AMBIENTAL, int? SERIE_DESDE, int? SERIE_HASTA, DateTime? FECHA_INGRESO_INI, DateTime? FECHA_INGRESO_FIN, int ESTADO_SERIE_ID)
        {
            DataTable dt = new DataTable(); //cargo las informacion del sp
            DataTable dtResultado = new DataTable(); //retorno el resultado
            dt = vNumeracionSalvoconductoDalc.ConsultarDatosSerieDalc(ID_AUT_AMBIENTAL, SERIE_DESDE, SERIE_HASTA, FECHA_INGRESO_INI, FECHA_INGRESO_FIN, ESTADO_SERIE_ID).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {

                var query = from listAutAmbiental in dt_AutoridadAmbiental.AsEnumerable()
                            join series in dt.AsEnumerable()
                            on listAutAmbiental.Field<int>("AUT_ID") equals series.Field<int>("ID_AUT_AMBIENTAL")
                            select new
                            {
                                ID_SERIE = series.Field<int>("ID_SERIE"),
                                AUTORIDAD_AMBIENTAL = listAutAmbiental.Field<string>("AUT_NOMBRE"),
                                SERIE_DESDE = series.Field<int>("SERIE_DESDE"),
                                SERIE_HASTA = series.Field<int>("SERIE_HASTA"),
                                CNT_SERIES_ALERTA = series.Field<int>("PJE_SERIES_ALERTA"),
                                ESTADO = series.Field<string>("ESTADO"),
                                NOMBRE_ARCHIVO_CREACION_SERIE = series.Field<string>("NOMBRE_ARCHIVO_CREACION_SERIE"),
                                RUTA_ARCHIVO_CREACION_SERIE = series.Field<string>("RUTA_ARCHIVO_CREACION_SERIE"),
                                NOMBRE_ARCHIVO_BLOQUEO_SERIE = series.Field<string>("NOMBRE_ARCHIVO_BLOQUEO_SERIE"),
                                RUTA_ARCHIVO_BLOQUEO_SERIE = series.Field<string>("RUTA_ARCHIVO_BLOQUEO_SERIE"),
                                MOTIVO_BLOQUEO = series.Field<string>("MOTIVO_BLOQUEO"),
                                NRO_SERIE_ACTUAL = series.Field<int>("NRO_SERIE_ACTUAL"),
                                FECHA_CREACION = series.Field<string>("FECHA_CREACION")

                            };


                dtResultado.Columns.Add(new DataColumn("COMANDO", typeof(string)));
                dtResultado.Columns.Add(new DataColumn("ID_SERIE", typeof(int)));
                dtResultado.Columns.Add(new DataColumn("AUT_AMBIENTAL", typeof(string)));
                dtResultado.Columns.Add(new DataColumn("DESDE", typeof(string)));
                dtResultado.Columns.Add(new DataColumn("HASTA", typeof(string)));
                dtResultado.Columns.Add(new DataColumn("PJE_SERIES_ALERTA", typeof(int)));
                dtResultado.Columns.Add(new DataColumn("ESTADO_SERIE", typeof(string)));
                dtResultado.Columns.Add(new DataColumn("NOMBRE_ARCHIVO_CREACION_SERIE", typeof(string)));
                dtResultado.Columns.Add(new DataColumn("RUTA_ARCHIVO_CREACION_SERIE", typeof(string)));
                dtResultado.Columns.Add(new DataColumn("NOMBRE_ARCHIVO_BLOQUEO_SERIE", typeof(string)));
                dtResultado.Columns.Add(new DataColumn("RUTA_ARCHIVO_BLOQUEO_SERIE", typeof(string)));
                dtResultado.Columns.Add(new DataColumn("MOTIVO_BLOQUEO", typeof(string)));
                dtResultado.Columns.Add(new DataColumn("NRO_SERIE_ACTUAL", typeof(int)));
                dtResultado.Columns.Add(new DataColumn("FECHA_CREACION", typeof(string)));


                foreach (var series in query)
                {
                    DataRow dr = dtResultado.NewRow();

                    dr["COMANDO"] = "";
                    dr["ID_SERIE"] = series.ID_SERIE;
                    dr["AUT_AMBIENTAL"] = series.AUTORIDAD_AMBIENTAL;
                    dr["DESDE"] = Convert.ToString(series.SERIE_DESDE);
                    dr["HASTA"] = Convert.ToString(series.SERIE_HASTA);
                    dr["PJE_SERIES_ALERTA"] = series.CNT_SERIES_ALERTA;
                    dr["ESTADO_SERIE"] = series.ESTADO;
                    dr["NOMBRE_ARCHIVO_CREACION_SERIE"] = series.NOMBRE_ARCHIVO_CREACION_SERIE;
                    dr["RUTA_ARCHIVO_CREACION_SERIE"] = series.RUTA_ARCHIVO_CREACION_SERIE;
                    dr["NOMBRE_ARCHIVO_BLOQUEO_SERIE"] = series.NOMBRE_ARCHIVO_BLOQUEO_SERIE;
                    dr["RUTA_ARCHIVO_BLOQUEO_SERIE"] = series.RUTA_ARCHIVO_BLOQUEO_SERIE;
                    dr["MOTIVO_BLOQUEO"] = series.MOTIVO_BLOQUEO;
                    dr["NRO_SERIE_ACTUAL"] = series.NRO_SERIE_ACTUAL;
                    dr["FECHA_CREACION"] = series.FECHA_CREACION;
                    dtResultado.Rows.Add(dr);
                }
            }
            return dtResultado;
        }

    #endregion


}
}

