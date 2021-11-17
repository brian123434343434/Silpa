using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.RepositorioArchivos
{
    public class Archivo
    {
        public int? FileID { get; set; }
        public string NombreArchivo { get; set; }
        public string Ubicacion { get; set; }
        public int TipoArchivo { get; set; }
        public string DescTipoArchivo { get; set; }
        public bool Asociado { get; set; }
        public int UsuarioID { get; set; }
        public decimal Tamaño { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
