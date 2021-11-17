using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.AudienciaPublica;
using System.Data;
using SILPA.LogicaNegocio.Generico;

namespace SILPA.LogicaNegocio.AudienciaPublica
{
    public class FuncionarioPublico
    {
        public FuncionarioPublicoIdentity FunIdentity;
        private FuncionarioPublicoDalc FunDalc;
        /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public FuncionarioPublico()
        {
            _objConfiguracion = new Configuracion();
            FunDalc = new FuncionarioPublicoDalc();
            FunIdentity = new FuncionarioPublicoIdentity();
        }

        /// <summary>
        /// Lista los funcionarios publicos existentes en la base de datos segun los registros que apliquen.
        /// </summary>
        /// <param name="intIdFuncionario">Identificador del funcionario</param>
        /// <returns>DataSet con los registros y las siguientes columnas: 
        /// FPU_ID - FPU_NOMBRE - FPU_EXTENSION
        /// </returns>
        public DataSet ListarFuncionario(Nullable<int> intIdFuncionario)
        {
            return FunDalc.ListarFuncionario(intIdFuncionario);
        }

    }
}