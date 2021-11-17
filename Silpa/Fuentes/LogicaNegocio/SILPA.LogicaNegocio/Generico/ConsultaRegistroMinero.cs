using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Generico
{
    public class ConsultaRegistroMinero
    {
        // Constructor
        public ConsultaRegistroMinero() {
       
        }

        /// <summary>
        /// Obtiene el listado de los registros mineros creados, modificados y eliminados a partir de la fecha de busqueda enviada 
        /// </summary>
        /// <param name="fechaConsulta">DateTime con la fecha de busqueda</param>
        /// <returns>List<RegistroMineroEntity> con la informacion de los registros mineros encontrados</returns>
        public List<RegistroMineroEntity> ConsultarRegistroMinero(DateTime fechaConsulta) {
            try
            {
                var listaRegistros = new List<RegistroMineroEntity>();
                DataSet objDatos = null;

                try
                {
                    RegistroMineroDalc dalc = new RegistroMineroDalc();
                    objDatos = dalc.ObtenerInformacionBaseRegistrosMineros(fechaConsulta);


                    // Verificar si se obtuvo datos
                    if (objDatos != null && objDatos.Tables.Count > 0 && objDatos.Tables[0].Rows.Count > 0)
                    {
                        // Recorrer dataset y almacenar en entity
                        foreach (DataRow registro in objDatos.Tables[0].Rows)
                        {
                            var registroMineroInfo = new RegistroMineroEntity();

                            registroMineroInfo.RegistroMineroId = int.Parse(registro["REGISTROMINERO_ID"].ToString());
                            registroMineroInfo.NombreDepartamento = registro["NOMBRE_DEPARTAMENTO"] != DBNull.Value ? registro["NOMBRE_DEPARTAMENTO"].ToString() : "";
                            registroMineroInfo.NombreMunicipio = registro["NOMBRE_MUNICIPIO"] != DBNull.Value ? registro["NOMBRE_MUNICIPIO"].ToString() : "";
                            registroMineroInfo.NumeroExpediente = registro["NRO_EXPEDIENTE"] != DBNull.Value ? registro["NRO_EXPEDIENTE"].ToString() : "";
                            registroMineroInfo.NombreTipoRegistroMinero = registro["NOMBRE_TIPO_REGISTRO_MINERO"] != DBNull.Value ? registro["NOMBRE_TIPO_REGISTRO_MINERO"].ToString() : "";
                            registroMineroInfo.NumeroActoAdministrativo = registro["NRO_ACTOADMINISTRATIVO"] != DBNull.Value ? registro["NRO_ACTOADMINISTRATIVO"].ToString() : "";
                            registroMineroInfo.NombreMina = registro["NOMBRE_MINA"] != DBNull.Value ? registro["NOMBRE_MINA"].ToString() : "";
                            registroMineroInfo.NombreProyecto = registro["NOMBRE_PROYECTO"] != DBNull.Value ? registro["NOMBRE_PROYECTO"].ToString() : "";
                            registroMineroInfo.Vigencia = registro["VIGENCIA"] != DBNull.Value ? registro["VIGENCIA"].ToString() : "";
                            registroMineroInfo.CodigoTituloMinero = registro["CODIGO_TITULOMINERO"] != DBNull.Value ? registro["CODIGO_TITULOMINERO"].ToString() : "";
                            registroMineroInfo.Archivo = registro["ARCHIVO"] != DBNull.Value ? registro["ARCHIVO"].ToString() : "";
                            registroMineroInfo.Observaciones = registro["OBSERVACIONES"] != DBNull.Value ? registro["OBSERVACIONES"].ToString() : "";
                            registroMineroInfo.DepartamentoId = int.Parse(registro["DEPARTAMENTO_ID"].ToString());
                            registroMineroInfo.MunicipioId = int.Parse(registro["MUNICIPIO_ID"].ToString());
                            registroMineroInfo.Estado = int.Parse(registro["ESTADO_NOVEDAD"].ToString());
                            registroMineroInfo.FechaNovedadRegistro = DateTime.Parse(registro["FECHA_NOVEDAD_REGISTRO"].ToString());

                            if (registro["FECHA_ACTOADMINISTRATIVO"] != DBNull.Value)
                                registroMineroInfo.FechaActoAdministrativo = DateTime.Parse(registro["FECHA_ACTOADMINISTRATIVO"].ToString());

                            if (registro["FECHA_EXPEDICION"] != DBNull.Value)
                                registroMineroInfo.FechaExpedicion = DateTime.Parse(registro["FECHA_EXPEDICION"].ToString());

                            if (registro["AUTORIDAD_AMB_ID"] != DBNull.Value)
                                registroMineroInfo.AutoridadAmbientalId = int.Parse(registro["AUTORIDAD_AMB_ID"].ToString());

                            if (registro["FECHA_VIGENCIA"] != DBNull.Value)
                                registroMineroInfo.FechaVigencia = DateTime.Parse(registro["FECHA_VIGENCIA"].ToString());

                            if (registro["AREA_HECTAREAS"] != DBNull.Value)
                                registroMineroInfo.AreaHectareas = float.Parse(registro["AREA_HECTAREAS"].ToString());

                            if (registro["SECTOR_ID"] != DBNull.Value)
                                registroMineroInfo.SectorId = int.Parse(registro["SECTOR_ID"].ToString());

                            if (registroMineroInfo.Estado < 3)
                            {
                                registroMineroInfo.ListaCoordenadasLocalizacion = RetornarCoordenadasRegistroMinero(registroMineroInfo.RegistroMineroId);
                                registroMineroInfo.ListaTitularesRegistroMinero = RetornarTitularesRegistroMinero(registroMineroInfo.RegistroMineroId);    
                            }                           

                            listaRegistros.Add(registroMineroInfo);
                        }

                    }
                    return listaRegistros;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en la consulta de Registros Mineros. " + ex.Message + " - " + ex.StackTrace.ToString(), ex);
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }


        /// <summary>
        /// Obtiene la información de las coordenadas de localizacion de un registro minero especifico 
        /// </summary>
        /// <param name="idRegistroMinero">int identificador del registro minero</param>
        /// <returns>List CoordenadasLocalizacion con las coordenadas del registro minero</returns>
        private List<CoordenadasLocalizacion> RetornarCoordenadasRegistroMinero(int idRegistroMinero) {
            try
            {
                var listaCoordenadas = new List<CoordenadasLocalizacion>();
                DataSet objDatos = null;

                try
                {
                    RegistroMineroDalc dalc = new RegistroMineroDalc();
                    objDatos = dalc.ObtenerCoordenadasLocalizacionRegistroMinero(idRegistroMinero);


                    // Verificar si se obtuvo datos
                    if (objDatos != null && objDatos.Tables.Count > 0 && objDatos.Tables[0].Rows.Count > 0)
                    {
                        // Recorrer dataset y almacenar en entity
                        foreach (DataRow registro in objDatos.Tables[0].Rows)
                        {
                            var coordenadasRegistro = new CoordenadasLocalizacion();
                            coordenadasRegistro.RegistroMineroId = int.Parse(registro["REGISTRO_MINERO_ID"].ToString());
                            coordenadasRegistro.CoordenadaNorte = decimal.Parse(registro["COORDENADA_NORTE"].ToString());
                            coordenadasRegistro.CoordenadaEste = decimal.Parse(registro["COORDENADA_ESTE"].ToString());
                            coordenadasRegistro.CoordenadaId = int.Parse(registro["COOR_ID"].ToString());

                            listaCoordenadas.Add(coordenadasRegistro);
                        }

                    }
                    return listaCoordenadas;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en la consulta de las coordenadas del Registro Mineros ID: " +  idRegistroMinero + ". " + ex.Message + " - " + ex.StackTrace.ToString(), ex);
                }
            }
            catch (Exception)
            {
                throw;
            }
        
        }

        /// <summary>
        /// Obtiene la información de los titulares de un registro minero especifico 
        /// </summary>
        /// <param name="idRegistroMinero">int identificador del registro minero</param>
        /// <returns>List TitularRegistroMinero con la informacion de los titulares del registro minero</returns>
        private List<TitularRegistroMinero> RetornarTitularesRegistroMinero(int idRegistroMinero) 
        {
            try
            {
                var listaTitulares = new List<TitularRegistroMinero>();
                DataSet objDatos = null;

                try
                {
                    RegistroMineroDalc dalc = new RegistroMineroDalc();
                    objDatos = dalc.ObtenerTitularesRegistroMinero(idRegistroMinero);


                    // Verificar si se obtuvo datos
                    if (objDatos != null && objDatos.Tables.Count > 0 && objDatos.Tables[0].Rows.Count > 0)
                    {
                        // Recorrer dataset y almacenar en entity
                        foreach (DataRow registro in objDatos.Tables[0].Rows)
                        {
                            var titularRegistro = new TitularRegistroMinero();
                            titularRegistro.RegistroMineroId = int.Parse(registro["REGISTRO_MINERO_ID"].ToString());
                            titularRegistro.TitularRegistroMineroId = int.Parse(registro["TITULAR_REGISTRO_MINERO_ID"].ToString());
                            titularRegistro.NombreTitular = registro["NOMBRE_TITULAR"] != DBNull.Value ? registro["NOMBRE_TITULAR"].ToString() : "";
                            titularRegistro.IdentificacionTitular = registro["IDENTIFICACION_TITULAR"] != DBNull.Value ? registro["IDENTIFICACION_TITULAR"].ToString() : "";

                            listaTitulares.Add(titularRegistro);
                        }
                    }
                    return listaTitulares;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en la consulta de los titulares del Registro Mineros ID: " + idRegistroMinero + ". " + ex.Message + " - " + ex.StackTrace.ToString(), ex);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
