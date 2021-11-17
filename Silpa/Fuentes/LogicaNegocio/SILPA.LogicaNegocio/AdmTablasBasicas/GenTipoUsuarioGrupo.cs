using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class GenTipoUsuarioGrupo
    {
        private GenTipoUsuarioGrupoDALC objTipoUsuarioGrupoDALC;

        public GenTipoUsuarioGrupo()
        {
            objTipoUsuarioGrupoDALC = new GenTipoUsuarioGrupoDALC();
        }

        public DataTable ListarTipoUsuarioGrupo(int tipoUsuario ,int userGroup)
        {
            return objTipoUsuarioGrupoDALC.ListarTipoUsuarioGrupo(tipoUsuario, userGroup);
        }

        public void EliminarTipoUsuarioGrupo(int intIdTipoUsuario, int intIdUserGroup)
        {
            objTipoUsuarioGrupoDALC.EliminarTipoUsuarioGrupo(intIdTipoUsuario, intIdUserGroup);
        }

        public void ActualizarTipoUsuarioGrupo(int intIdTipoUsuario, int intIdUserGroup, int intIdTipoUsuarioAnt, int intIdUserGroupAnt)
        {
            objTipoUsuarioGrupoDALC.ActualizarTipoUsuarioGrupo(intIdTipoUsuario, intIdUserGroup, intIdTipoUsuarioAnt,  intIdUserGroupAnt);
        }

        public void InsertarTipoTramite(int intIdTipoUsuario, int intIdUserGroup)
        {
            objTipoUsuarioGrupoDALC.InsertarTipoUsuarioGrupo(intIdTipoUsuario, intIdUserGroup);
        }
    }
}
