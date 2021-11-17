using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.DAA;
using SILPA.AccesoDatos.Utilidades;
using System.Data;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.DAA
{
    public class Ubicacion
    {
        public UbicacionDalc objDalc;
        public UbicacionIdentity objIdentity;
        public DataSet Resultados;

        public Ubicacion()
        {
            objDalc = new UbicacionDalc();
            objIdentity = new UbicacionIdentity();
        }

        public void IniciarIdentity(int IdUbicacion)
        {
            Resultados = new DataSet();

            Resultados = objDalc.ObtenerUbicacion(IdUbicacion);
            if (Resultados.Tables[0].Rows.Count > 0)
            {
                objIdentity.IdUbicacion =  Convert.ToInt32(Resultados.Tables[0].Rows[0]["ID_UBICACION"]);
                objIdentity.IdDepartamento = Convert.ToInt32(Resultados.Tables[0].Rows[0]["DEP_ID"]);
                objIdentity.Idmunicipio = Convert.ToInt32(Resultados.Tables[0].Rows[0]["MUN_ID"]);
                objIdentity.IdCorregimiento = Convert.ToInt32(Resultados.Tables[0].Rows[0]["COR_ID"]);
                objIdentity.IdVereda = Convert.ToInt32(Resultados.Tables[0].Rows[0]["VER_ID"]);
                objIdentity.IdCuenca = Convert.ToInt32(Resultados.Tables[0].Rows[0]["CUE_ID"]);
                objIdentity.UbicacionNombre = Convert.ToString(Resultados.Tables[0].Rows[0]["UBI_ID"]);
                objIdentity.UbicacionID = Convert.ToString(Resultados.Tables[0].Rows[0]["UBI_NOMBRE"]);
            }
        }

        /// <summary>
        /// Insertar en la tabla Expediente_Ubicacion
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los valores de los campos que se van a insertar</param>
        public void InsertarUbicacion(ref UbicacionIdentity objIdentityUbi)
        {
            try
            {
                objDalc.InsertarUbicacion(ref objIdentityUbi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Retorna un DataSet con los datos de una ubicacion especifica
        /// </summary>
        /// <param name="intIdAA">int: indentificador de la ubicacion</param>
        /// <returns>DataSet: Conjunto de resultados de ubicacion</returns>
        public DataSet ObtenerUbicacion(int intIdUbicacion)
        {
            try
            {
                return objDalc.ObtenerUbicacion(intIdUbicacion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista en un DataSet los departamentos y los municipios asociados
        /// </summary>
        /// <param name="idDepto">Identificador del Departamento se puede usar el comodin -1 para imprimir todos</param>
        /// <param name="idMun">Identificador del municipio se puede usar el comodin -1 para imprimir todos</param>
        /// <returns>Retorna un DataSet con todos los departamentos y sus municipios definidos en los cireterios de busqueda</returns>
        public DataSet ListarDeptosMunicipios(int idDepto, int idMun)
        {
            try
            {
                return objDalc.ListarDeptosMunicipios(idDepto, idMun);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista en un DataSet los departamentos
        /// </summary>
        /// <param name="idDepto">Identificador del Departamento se puede usar el comodin -1 para imprimir todos</param>
        /// <returns>Retorna un DataSet con todos los departamentos</returns>
        public DataSet ListarDeptos(int idDepto)
        {
            try
            {
                return objDalc.ListarDeptos(idDepto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista en un DataSet los corregimientos del municipio
        /// </summary>
        /// <param name="idMunicipio">Identificador del municipio</param>
        /// <returns>Retorna un DataSet con todos los corregimientos del municipio</returns>
        public DataSet ListarCorregimientos(int idMunicipio)
        {
            try
            {
                return objDalc.ListarCorregimientos(idMunicipio);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista en un DataSet con las veredas pertenecientes a un municipio
        /// </summary>
        /// <param name="idMunicipio">Identificador del municipio </param>
        /// <returns>Retorna un DataSet con todos las veredas del municipio</returns>
        public DataSet ListarVeredas(int idMunicipio)
        {
            try
            {
                return objDalc.ListarVeredas(idMunicipio);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista en un DataSet con las cuencas
        /// </summary>
        /// <returns>Retorna un DataSet con todos las cuencas</returns>
        public DataSet ListarCuencas()
        {
            try
            {
                return objDalc.ListarCuencas();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



 

     }
}
