using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class UserGroup
    {
        private UserGroupDALC objUserGroupDALC;
        public UserGroup()
        {
            objUserGroupDALC = new UserGroupDALC();
        }

        public DataTable ListarGenTipoUsuario(int id)
        {
            return objUserGroupDALC.ListarUserGroupDALC(id);
        }
    }
}
