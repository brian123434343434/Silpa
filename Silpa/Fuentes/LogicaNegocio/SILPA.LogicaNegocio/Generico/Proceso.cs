using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using System.Data;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.Generico
{
    /*
     * Clase útil para determinar el proceso que se lanza desde el bpm
     */
    public class Proceso
    {
        private ProcesoDalc Dalc;
        private ProcesoIdentity Identity;

        public ProcesoIdentity PIdentity
        {
            get { return Identity; }
            set { Identity = value; }
        }
        private BpmParametros bpmParaDacl;

        private string _IdCondicion;

        private int _idCondicionTransicion;
        
        private int _idActividadSigTransicion;

        private string _idActividadSig;

        private int _idActiviteInstance;

        public int IdActiviteInstance
        {
            get { return _idActiviteInstance; }
            set { _idActiviteInstance = value; }
        }

        public string IdActividadSig
        {
            get { return _idActividadSig; }
            set { _idActividadSig = value; }
        }

        public string IdCondicion
        {
            get { return _IdCondicion; }
            set { _IdCondicion = value; }
        }

        public int IdCondicionTransicion
        {
            get { return _idCondicionTransicion; }
            set { _idCondicionTransicion = value; }
        }

        public int ActividadSigTransicion
        {
            get { return _idActividadSigTransicion; }
            set { _idActividadSigTransicion = value; }
        }
        
        public Proceso()
        {
            Dalc = new ProcesoDalc();
            Identity = new ProcesoIdentity();
            bpmParaDacl = new BpmParametros();
        }


        /// <summary>
        /// indica el proceso lanzado desde el bpm
        /// </summary>
        private int _procesoIniciado;
        public int ProcesoIniciado
        {
            get { return _procesoIniciado; }
            set { _procesoIniciado = value; }
        }

        /// <summary>
        /// Indica Avtividad en el que se encuentra actualmente el proceso
        /// </summary>
        private int _actividadActual;
        public int ActividadActual
        {
            get { return _actividadActual; }
            set { _actividadActual = value; }
        }
        /// <summary>
        /// Constructor con el numero de la instancia del proceso
        /// </summary>
        /// <param name="IDProcessInstance"></param>
        public Proceso(Int64 IDProcessInstance) 
        {
           this.Identity= this.DeterminarProceso(IDProcessInstance);
            //this.DeterminarActividad(IDProcessInstance);
        }



        /// <summary>
        /// Lógica que determina el proceso lanzado desde el bpm
        /// </summary>
        /// <param name="IDProcessInstance">Int64: identificador del proceso</param>
        private ProcesoIdentity DeterminarProceso(Int64 IDProcessInstance)
        {
            //SMLog.Escribir(Severidad.Informativo, "Proceso Incoder DeterminarProceso -  Processinstance: " + IDProcessInstance.ToString());
            ProcesoIdentity proceso = new ProcesoIdentity();
            if (this.Dalc == null)
                this.Dalc = new ProcesoDalc();
            proceso = this.Dalc.ObtenerObjProceso(IDProcessInstance);

            //SMLog.Escribir(Severidad.Informativo, "Proceso Incoder DeterminarProceso 2 -  Clave: " + proceso.Clave);
            return proceso;
        }


        /// <summary>
        /// Retorna la informacion del estado en que se encuentra el proceso si este esta parametrizado 
        /// </summary>
        /// <param name="IDProcessInstance">Int64: identificador del proceso</param>
        /// <returns>ID, IDActivity, IDProcessInstance, IDParticipant, EntryDataType, EntryData, IDEntryData, TES_ID_ESTADO_PROCESO, GEN_DESCRIPCION</returns>
        public DataTable ActividadesXProceso(Int64 IDProcessInstance)
        {
            return this.Dalc.ObtenerEstadoProceso(IDProcessInstance);
        }

        /// <summary>
        /// Retorna la informacion del estado en que se encuentra el proceso si este esta parametrizado 
        /// </summary>
        /// <param name="IDProcessInstance">Int64: identificador del proceso</param>
        public void DeterminarActividad(Int64 IDProcessInstance)
        {
            this._idActiviteInstance = this.Dalc.UltimoIdActivityInstance(Convert.ToInt32(IDProcessInstance));
        }

        /// <summary>
        /// Busca el parametro y retorna su codigo
        /// </summary>
        /// <param name="intTipo">Define el tipo de parametro 1.=Actividad, 2.=Condiciones, 3.=Formularios </param>
        /// <param name="strNombre">Realiza un filtro en la busqueda por el nombre de parametro</param>
        /// <returns>Codigo asociado al nombre del parametro insertado</returns>
        public int ObtenerCodigoParametroBPM(int intIdParametro)
        {
            DataSet dsTemp = new DataSet();

            dsTemp = this.bpmParaDacl.ListarBmpParametros(intIdParametro);

            if (dsTemp.Tables[0] != null)
            {
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(dsTemp.Tables[0].Rows[0]["CODIGO"]);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
       }

        /// <summary>
        /// Obtiene el código del parámetro
        /// </summary>
        /// <param name="tipo">tipo</param>
        /// <param name="codigo">código</param>
        /// <returns></returns>
        public int ObtenerCodigoParametroBPM(int? tipo, int? codigo)
        {
            DataSet dsTemp = new DataSet();

            dsTemp = this.bpmParaDacl.ListarBmpParametros(tipo, codigo);

            if (dsTemp.Tables[0] != null)
            {
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                   return Convert.ToInt32(dsTemp.Tables[0].Rows[0]["CODIGO"]);
                } 
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public void EstablecerCondicion(int intActivityInstance, string strNombreParametroBpm)
        {
            DataSet dsRes = new DataSet();
            this.CondicionActual(intActivityInstance, strNombreParametroBpm);
            if (String.IsNullOrEmpty(this.IdCondicion))
            {
                this.CondicionSiguientePorActividad(intActivityInstance, strNombreParametroBpm);
            }
        }


        public string RetornarCondicion(int intActivityInstance, string strNombreParametroBpm)
        {
            DataSet dsRes = new DataSet();
            this.CondicionActual(intActivityInstance, strNombreParametroBpm);
            if (String.IsNullOrEmpty(this.IdCondicion))
            {
                this.CondicionSiguientePorActividad(intActivityInstance, strNombreParametroBpm);
            }
            return this.IdCondicion;
        }

       /// <summary>
       /// Busca en la BD la condicion que finaliza la actividad actual siempre y cuando el formulario asociado sea el
       /// formulario con el nombre strParametroBpm
       /// </summary>
       /// <param name="intActivityInstance">Instancia de la actividad</param>
       /// <param name="strNombreParametroBpm">Nombre del Formulario BPM previamente registrado en la BD de silpa</param>
       /// <returns>Identificador de la condicion</returns>
       public void CondicionActual(int intActivityInstance, string strNombreParametroBpm)
       {
           try
           {
               DataSet dsRes = new DataSet();
               dsRes = this.Dalc.CondicionActual(intActivityInstance, strNombreParametroBpm);
               if (dsRes != null)
               {
                   this.IdCondicion = dsRes.Tables[0].Rows[0]["IDCondition"].ToString();
               }
               else
               {
                   this.IdCondicion = string.Empty;
               }
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       /// <summary>
       /// Busca en la BD la condicion que finaliza la actividad actual siempre y cuando el formulario asociado a la 
       /// actividad siguiente sea el formulario con el nombre strParametroBpm
       /// </summary>
       /// <param name="intActivityInstance">Instancia de la actividad</param>
       /// <param name="strNombreParametroBpm">Nombre del Formulario BPM previamente registrado en la BD de silpa</param>
       /// <returns>DataSet con las siguietes columnas: [IDActivity - IDCondition]</returns>
       public void CondicionSiguientePorActividad(int intActivityInstance, string strNombreParametroBpm)
       {
           try
           {
               DataSet dsRes = new DataSet();
               dsRes = this.Dalc.CondicionSiguiente(intActivityInstance, strNombreParametroBpm);
               if (dsRes != null)
               {
                   this.IdCondicion = dsRes.Tables[0].Rows[0]["IDCondition"].ToString();
               }
               else 
               {
                   this.IdCondicion = string.Empty;
               }

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       /// <summary>
       /// Busca en la BD la condicion que finaliza la actividad actual de la instancia del proceso siempre y cuando el formulario asociado a la 
       /// actividad siguiente sea el formulario con el nombre strParametroBpm
       /// </summary>
       /// <param name="intActivityInstance">Identificador de la Instancia del proceso</param>
       /// <param name="strNombreParametroBpm">Nombre del Formulario BPM previamente registrado en la BD de silpa</param>
       public void CondicionSiguientePorProceso(int intProcessInstance, string strNombreParametroBpm)
       {
           try
           {
               DataSet dsRes = new DataSet();
               dsRes = this.Dalc.CondicionSiguiente(this.Dalc.UltimoIdActivityInstance(intProcessInstance), strNombreParametroBpm);
               if (dsRes != null)
               {
                   this.IdCondicion = dsRes.Tables[0].Rows[0]["IDCondition"].ToString();
               }
               else
               {
                   this.IdCondicion = string.Empty;
               }

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       /// <summary>
       /// Hava: 28-abr-10
       /// Avanza el proceso a una actividad específica
       /// </summary>
       /// <param name="idActividadDestino">Identificador de la actividad final</param>
       public void ObtenerActividadActual(long idProcessInstance)
       {
           try
           {
               this._actividadActual = this.Dalc.ActividadActual(idProcessInstance);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       /// <summary>
       /// hava: 28-bar-10
       /// Determina la consdición necesaria para adelantar el proceso desde la actividad actual 
       /// hasta la activida final
       /// </summary>
       /// <param name="idActividadFinal">int: Identificador de la actividad final</param>
       public void ObtenerCondicionTransicion() 
       {
           this._idCondicionTransicion = this.Dalc.ObtenerCondicionTransicion(this.ActividadActual, this.ActividadSigTransicion);
       }


       /// <summary>
       /// Obtiene las condiciones asicadas a los botones de pago electronico
       /// Pago Electronico  e Imprimir de acuerdo al proces case
       /// </summary>
        public void ObtenerCondicionPagoElectronico() 
       {
            this.Dalc.ObtenerCondicionPagoElectronico(ref this.Identity);
       }

        /// <summary>
        /// Obtiene el tipo de tramite al que pertenece el Processinstance Asociado.
        /// </summary>
        /// <param name="IntProcessInstance"></param>
        /// <returns>String: Nombre del tipo de trámite</returns>
       public string ObtenerTipoTramiteByProcessInstance(Int64 IntProcessInstance) 
       { 
           return this.Dalc.ObtenerTipoTramiteByProcessInstance(IntProcessInstance);
       }
    }
}
