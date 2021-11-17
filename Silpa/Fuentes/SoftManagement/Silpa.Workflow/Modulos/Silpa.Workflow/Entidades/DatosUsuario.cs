using System;
using System.Collections.Generic;
using System.Text;

namespace Silpa.Workflow.Entidades
{   
    public class DatosUsuario
    {
        private string nombreUsuario;
        private string ultimoLogin;
        private string menuAsociado;

        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value;     }
        }

        public string UltimoLogin
        {
            get { return ultimoLogin; }
            set { ultimoLogin = value; }
        }
        public string MenuAsociado
        {
            get { return menuAsociado; }
            set { menuAsociado = value; }
        }

        public DatosUsuario(string nombreUsuario, string ultimoLogin, string menuAsociado)
        {
            this.nombreUsuario = nombreUsuario;
            this.ultimoLogin = ultimoLogin;
            this.menuAsociado = menuAsociado;
        }
    }
}
