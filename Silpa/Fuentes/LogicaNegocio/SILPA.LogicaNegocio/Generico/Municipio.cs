using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using System.Data;

namespace SILPA.LogicaNegocio.Generico
{
    public class Municipio
    {
        public static string obtenerNomDepMunByMunId(int MunId)
        {
            MunicipioDalc objMunDal = new MunicipioDalc();
            MunicipioIdentity objMunicipioIndentity = new MunicipioIdentity();
            objMunicipioIndentity.Id= MunId;
            objMunDal.ObtenerMunicipios(ref objMunicipioIndentity);
            
            DepartamentoDalc objDepDal = new DepartamentoDalc();
            DepartamentoIdentity objDepartamentoIndentity = new DepartamentoIdentity();
            objDepartamentoIndentity.Id=objMunicipioIndentity.DeptoId;
            objDepDal.ObtenerDepartamentos(ref objDepartamentoIndentity);

            return objDepartamentoIndentity.Nombre + "-" + objMunicipioIndentity.NombreMunicipio;
        }


        /// <summary>
        /// Obtener el listado de municipios asociados a un medio de transporte (Autoliquidación)
        /// </summary>
        /// <param name="p_intDepartamentoID">int con el identificador del deparrtamento al cual pertenecen los municipios</param>
        /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
        /// <returns>List con la información de los municipios</returns>
        public List<MunicipioIdentity> ListarMuncicipiosDepartamentoMedioTransporteLiquidacion(int p_intDepartamentoID, int p_intMedioTransporteID)
        {
            MunicipioDalc objMunDal = new MunicipioDalc();
            return objMunDal.ListarMuncicipiosDepartamentoMedioTransporteLiquidacion(p_intDepartamentoID, p_intMedioTransporteID);
        }

        /// <summary>
        /// Obtener el listado de municipios destino asociados a un medio de transporte (Autoliquidación)
        /// </summary>
        /// <param name="p_intMunipioOrigenID">int con el municipio de origen</param>
        /// <param name="p_intDepartamentoID">int con el identificador del deparrtamento al cual pertenecen los municipios</param>
        /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
        /// <returns>List con la información de los municipios</returns>
        public List<MunicipioIdentity> ListarMuncicipiosDestinoDepartamentoMedioTransporteLiquidacion(int p_intMunipioOrigenID, int p_intDepartamentoID, int p_intMedioTransporteID)
        {
            MunicipioDalc objMunDal = new MunicipioDalc();
            return objMunDal.ListarMuncicipiosDestinoDepartamentoMedioTransporteLiquidacion(p_intMunipioOrigenID, p_intDepartamentoID, p_intMedioTransporteID);
        }


        /// <summary>
        /// Lista los municipios. Exposible listar los municipios de un departamento o una regional.
        /// </summary>
        /// <param name="intId" >Con solo este valor se lista el municipio con el ID</param>
        /// <param name="intDeptoId" >Con solo este valor se listan los municipios del departamento en particular</param>
        /// <param name="intRegionalId" >Con solo este valor se listan los municipios del la regional en particular</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  MUN_ID, MUN_NOMBRE, MUN_VALOR_TIQUETE, DEP_ID, DEP_NOMBRE, RGN_ID, RGN_NOMBRE, UBI_ID, UBI_NOMBRE</returns>
        public DataSet ListarMunicipios(Nullable<int> intId, Nullable<int> intDeptoId, Nullable<int> intRegionalId)
        {
            MunicipioDalc objMunDal = new MunicipioDalc();
            return objMunDal.ListarMunicipios(intId, intDeptoId, intRegionalId);
        }
    }
}
