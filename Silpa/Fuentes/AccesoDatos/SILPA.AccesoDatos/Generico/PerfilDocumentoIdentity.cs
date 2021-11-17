using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Generico {
    public class PerfilDocumentoIdentity {
        private int _id_tipo;
        public int IdTipo {
            get { return _id_tipo; }
            set { _id_tipo = value; }
        }

        private string _tipo_perfil;
        public string TipoPerfil {
            get { return _tipo_perfil; }
            set { _tipo_perfil = value; }
        }

        private Nullable<bool> _require_login;
        public Nullable<bool> RequireLogin {
            get { return _require_login; }
            set { _require_login = value; }
        }

        private Nullable<bool> _estado;
        public Nullable<bool> Estado {
            get { return _estado; }
            set { _estado = value; }
        }

        private string _Descripcion;
        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
    }
}
