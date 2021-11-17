using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Aprovechamiento
{
    [Serializable]
    public class EspecieAprovechamientoIdentity
    {
        public int EspecieAprovechamientoID { get; set; }
        public int AprovechamientoID { get; set; }
        public int EspecieTaxonomiaID { get; set; }
        public string NombreEspecie { get; set; }
        public int UnidadMedidaID { get; set; }
        public string UnidadMedida { get; set; }
        public double Cantidad { get; set; }
        public string CantidadEspecieLetras { get; set; }
        public int ClaseProductoID { get; set; }
        public string ClaseProducto { get; set; }
        public int TipoProductoID { get; set; }
        public string TipoProducto { get; set; }
        public double? CantidadVolumenMovilizar { get; set; }
        public double? CantidadVolumenRemanente { get; set; }
        public double? CantidadDisponible { get; set; }
        public double? CantidadMovido { get; set; }

        //jmartinez variables para saldo por volumen y por cantidad para la consulta de Aprovechamientos
        public double CntVolumenMovido { get; set; }
        public double SaldoCntVolumen { get; set; }

        //Jmartinez Salvoconducto Fase 2
        public string NombreComunEspecie { get; set; }
        public string CodigoIDEAMClaseProducto { get; set; }
        public string CodigoIDEAMEspecie { get; set; }
        public string CodigoIDEAMUnidadMedida { get; set; }
        public string CodigoIDEAMTipoProducto { get; set; }
        public double DiametroAlturaPecho { get; set; }
        public double AlturaComercial { get; set; }
        public int TratamientoSilviculturaID { get; set; }

        public string CodigoIdeamTratamientoSilvID { get; set; }

    }
}
