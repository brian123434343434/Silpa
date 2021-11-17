using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class GenTipoUsuario
    {
        private GenTipoUsuarioDALC objGenTipoUsuarioDALC;
        public GenTipoUsuario()
        {
            objGenTipoUsuarioDALC = new GenTipoUsuarioDALC();
        }

        public DataTable ListarGenTipoUsuario(int id, int  strNombre)
        {
            return objGenTipoUsuarioDALC.ListarTipoUsuarioDALC(id, strNombre);
        }

        public DataTable ListarGenTipoUsuario(string strNombre)
        {
            return objGenTipoUsuarioDALC.ListarTipoUsuarioDALC(strNombre);
        }

        public DataTable ValidarGenTipoUsuario(int intIdPar, string strNombre)
        {
            return objGenTipoUsuarioDALC.ValidarTipoUsuarioDALC( intIdPar,  strNombre);
        }

        public void ActualizarGenTipoUsuario(int intId, int intIdPar, string strNombre)
        {
            objGenTipoUsuarioDALC.ActualizarTipoUsuarioDALC(intId, intIdPar, strNombre);
        }

        public void InsertarGenTipoUsuario(int intIdPar, string strNombre)
        {
            objGenTipoUsuarioDALC.InsertarTipoUsuarioDALC(intIdPar, strNombre);
        }

        public void EliminarGenTipoUsuario(int intId)
        {
            objGenTipoUsuarioDALC.EliminarTipoUsuarioDALC(intId);
        }

    }
}
