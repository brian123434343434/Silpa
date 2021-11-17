using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    [Serializable]
    public class EspecimenNewIdentity
    {
        public int Orden { get; set; } 
        public int Identity { get; set; }
        public int EspecieSalvoconductoID { get; set; }
        public int SalvocoductoID { get; set; }
        public int EspecieTaxonomiaID { get; set; }
        public string NombreEspecie { get; set; }
        public int ClaseProductoID { get; set; }
        public string ClaseProducto { get; set; }
        public int TipoProductoId { get; set; }
        public string TipoProducto { get; set; }
        public int UnidadMedidaId { get; set; }
        public string UnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad { get; set; }
        public string CantidadLetras { get; set; }
        public double Volumen { get; set; }
        public double VolumenBruto { get; set; }
        public double CantidadDisponible { get; set; }
        public int? SalvoconductoOrigenId { get; set; }
        public int? AprovechamientoOrigenId { get; set; }
        public string Dimensiones { get; set; }
        public string Identificacion { get; set; }
        public double CantidadMovido { get; set; }
        //jmartinez salvoconducto fase 2
        public string NumeroSUNL { get; set; }
        public string NumeroSUNLAnterior { get; set; }
        public string NombreComunEspecie { get; set; }
        public string CodigoIdeamClaseRecurso { get; set; }
        public string CodigoIdeamEspecie { get; set; }
        public string CodigoIdeamUnidadMedida { get; set; }
        public string CodigoIdeamClaseProducto { get; set; }
        public string CodigoIdeamTipoProducto { get; set; }
        public string StrEspecieSunlAnterior { get; set; }
    }
}
