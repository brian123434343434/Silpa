using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class ContactoPersonaIdentity
    {
        public Int64 PerId { get; set; }
        public int ContactoId { get; set; }
        public int TipoContactoId { get; set; }
        public string TipoContacto { get; set; }
        public string NombreCompleto { get; set; }
        public int TipoIdentificacionId { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string NumeroTarjetaProfesional { get; set; }
        public string Area { get; set; }
        public string Cargo { get; set; }
        public bool Activo { get; set; }
        //public List<InfoContactoPersonaIdentity> LstDatosContacto { get; set; }
    }
    

}
