using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.Expedientes;
using SoftManagement.Log;
using System.Data;
using System.Data.SqlClient;

namespace SILPA.LogicaNegocio.Expedientes
{
    public class ExpedienteAutExt
    {
        

        public ExpedienteAutExtEntity _expedienteAutExtEntity;
        public ExpedienteAutExtDaLC _expedienteAutExtDaLC;
          /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExpedienteAutExt()
        {
            _objConfiguracion = new Configuracion();
            _expedienteAutExtEntity = new ExpedienteAutExtEntity();
            _expedienteAutExtDaLC = new ExpedienteAutExtDaLC();
          
        }


        /// <summary>
        /// Lista los expedientes existententes en la base de datos segun los registros que apliquen.
        /// </summary>
        /// <param name="IdAA">Identificador de la Autoridad Ambiental</param>
        /// <param name="ID_Sol">Identificador del Solicitante</param>
        public string ListarExpedientesAutExternas(Int32 IdAA, string IdentificacionSol)
        {
            try
            {
                    SMLog.Escribir(Severidad.Informativo, "ListarExpedientesAutExternas");
                    ExpedienteAutExtDaLC obj = new ExpedienteAutExtDaLC();
                    XmlSerializador _objSer = new XmlSerializador();
                    List<ExpedientesExternosEntity> not = new List<ExpedientesExternosEntity>();
                    not = obj.ListarExpedientesAutExternas(IdAA, IdentificacionSol);

                    string salida = _objSer.serializar(not);
                    return salida;
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ListarExpedientesAutExternas: " + sqle.ToString());
                
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Inserta una publicacion en la base de datos.
        /// </summary>
        /// <param name="strXmlDatos">string en formato XML para la insercion de una publicacion</param>
        /// <returns>El identity de Publicacion serializado</returns>
        public void InsertarExpedientesAutExternas(string strXmlDatos)
        {
            try
            {
                   SMLog.Escribir(Severidad.Informativo, "InsertarExpedientesAutExternas");
                  
                    ExpedienteAutExtEntity _exp = new ExpedienteAutExtEntity();
                    ExpedienteAutExtDaLC _expDalc = new ExpedienteAutExtDaLC();    
                    _exp= (ExpedienteAutExtEntity)_exp.Deserializar(strXmlDatos);

                    if (_exp != null)
                    {
                        _expDalc.InsertarExpedientesAutExternas(ref _exp);

                        if (_exp.LISTA_EXT_EXPEDIENTE_AA != null)
                        {
                            if (_exp.EXT_ID != null)
                            {
                                foreach (ExpedientesExternosEntity e in _exp.LISTA_EXT_EXPEDIENTE_AA)
                                {
                                    e.EXP_ID = _exp.EXT_ID;
                                    _expDalc.InsertarExpedientesExternos(e);
                                }
                            }

                        }

                    }

             }
             catch (Exception sqle)
             {
                 SMLog.Escribir(Severidad.Critico, "InsertarExpedientesAutExternas: " + sqle.ToString());
              
                 throw new Exception(sqle.Message);
             }
          }


        /// <summary>
        /// guarda la relación entre dos procesos padre hijo (los numero silpa)
        /// ejemplo el caso de información adicional encualquier momento
        /// </summary>
        /// <param name="padre">string: número silpa del trámite desde donde se origina el segundo proceso</param>
        /// <param name="hijo">string: número silpa del trámite hijo originado</param>
        public void RelacionarExpedienteSilpaPadreHijo(string codigo_Expediente, string padre, string hijo, int tipoTramite)
        {
            try
            {
                ExpedienteAutExtDaLC _expDalc = new ExpedienteAutExtDaLC();
                _expDalc.RelacionarExpedienteSilpaPadreHijo(codigo_Expediente, padre, hijo, tipoTramite);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "RelacionarExpedienteSilpaPadreHijo: " + sqle.ToString());

                throw new Exception(sqle.Message);
            }
        }


    }


}
