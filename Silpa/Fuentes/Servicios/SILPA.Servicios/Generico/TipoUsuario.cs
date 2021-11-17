using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;

namespace SILPA.Servicios.Generico
{    
    public class TipoUsuario
    {        
        private TipoUsuarioIdentity Identity;
        private TipoUsuarioDalc objTipoUsuarioDalc;
        public TipoUsuarioIdentity TipousuarioIdentity
        {
            get { return this.Identity; }
        }
        public List<TipoUsuarioIdentity> CargarTipoUsuarios()
        {
            this.objTipoUsuarioDalc = new TipoUsuarioDalc();
            return objTipoUsuarioDalc.ObtenerUsuarios();
        }

        //public static void ActivarUsuario(object xmlDatos)
        //{
        //    string strXmlDatos = (string)xmlDatos;

        //    TipoUsuario _objUsuario = new TipoUsuario();
        //    _objUsuarioDalc.ActivarUsuario(strXmlDatos);
        //}
    }
}
