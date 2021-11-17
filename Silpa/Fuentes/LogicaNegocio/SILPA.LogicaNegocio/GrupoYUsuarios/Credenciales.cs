using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.GrupoYUsuarios;
using SILPA.AccesoDatos.Generico;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Xml;

namespace SILPA.LogicaNegocio.GrupoYUsuarios
{
    public class Credenciales
    {
        public List<CredencialesEntity> ConsultarCredenciales()
        {
            CredencialesDalc  cred = new CredencialesDalc ();
            return  cred.ConsultaCredenciales(); 
        }

        public CredencialesEntity ConsultarCredencial(string login)
        {
            CredencialesDalc cred = new CredencialesDalc();
            return cred.ConsultaCredencial(login);
        }
        /// <summary>
        /// Consultar la informacion de credenciales solo para los usuarios que se creen para entidades Territoriales
        /// </summary>
        /// <param name="login"></param>
        /// <param name="ser"></param>
        /// <returns></returns>
        public CredencialesEntity ConsultarCredencial(string login, string ser)
        {
            CredencialesDalc cred = new CredencialesDalc();
            return cred.ConsultaCredencial(login, "S");
        }
    }
}
