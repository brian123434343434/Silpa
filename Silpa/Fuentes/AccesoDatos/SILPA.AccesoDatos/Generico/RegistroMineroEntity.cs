using System;
using SILPA.Comun;
using System.Text;
using System.Collections.Generic;


namespace SILPA.AccesoDatos.Generico
{
    
    public class RegistroMineroEntity : EntidadSerializable
    {        
        public int Estado { get; set; }             // (1) CREADO (2) MODIFICADO (3) ELIMINADO 
        public int RegistroMineroId { get; set; }
        public int DepartamentoId { get; set; }
        public string NombreDepartamento { get; set; }
        public int MunicipioId { get; set; }
        public string NombreMunicipio { get; set; }
        public string NumeroExpediente { get; set; }
        public string NombreTipoRegistroMinero { get; set; }
        public string NumeroActoAdministrativo { get; set; }
        public DateTime ? FechaActoAdministrativo { get; set; }
        public string NombreMina { get; set; }
        public DateTime ? FechaExpedicion { get; set; }
        public string NombreProyecto { get; set; }
        public int ? AutoridadAmbientalId { get; set; }
        public string Vigencia { get; set; }
        public DateTime ? FechaVigencia { get; set; }
        public string CodigoTituloMinero { get; set; }
        public float ? AreaHectareas { get; set; }
        public string Archivo { get; set; }
        public int ? SectorId { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaNovedadRegistro { get; set; } 
        public List<CoordenadasLocalizacion> ListaCoordenadasLocalizacion { get; set; }
        public List<TitularRegistroMinero> ListaTitularesRegistroMinero { get; set; }
    }

    public class CoordenadasLocalizacion : EntidadSerializable {

        public int RegistroMineroId { get; set; }
        public int CoordenadaId { get; set; }
        public decimal CoordenadaNorte { get; set; }
        public decimal CoordenadaEste { get; set; } 
    }


    public class TitularRegistroMinero : EntidadSerializable {
        
        public int RegistroMineroId { get; set; }
        public int TitularRegistroMineroId { get; set; }
        public string NombreTitular { get; set; }
        public string IdentificacionTitular { get; set; }
    }


    public class RespuestaWsConsultaRegistrosMinerosEntity : EntidadSerializable 
    {
        public bool Error { get; set; }
        public string TextoError { get; set; }
        public int CantidadRegistro { get; set; }
        public List<RegistroMineroEntity> ListaRegistrosMineros { get; set; }
    }
}
