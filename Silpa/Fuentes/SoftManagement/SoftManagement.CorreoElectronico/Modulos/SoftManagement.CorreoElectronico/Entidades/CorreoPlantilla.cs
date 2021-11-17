using System;
using System.Collections.Generic;
using System.Text;

namespace SoftManagement.CorreoElectronico.Entidades
{
    public class CorreoPlantilla
    {
        private int plantillaCorreoId;
        private string de;
        private string cc;
        private string cco;
        private string plantilla;
        private string asunto;
        private int correoServidorId;

        public int PlantillaCorreoId
        {
            get {return plantillaCorreoId;}
        }
        public string De
        {
            get { return de; }
        }
        public string Cc
        {
            get { return cc; }
        }
        public string Cco
        {
            get { return cco; }
        }
        public string Plantilla
        {
            get { return plantilla; }
        }
        public string Asunto
        {
            get { return asunto; }
        }
        public int CorreoServidorId
        {
            get { return correoServidorId; }
        }

        public CorreoPlantilla(int plantillaCorreoId, string de, string cc, string cco, string plantilla, string asunto, int correoServidorId)
        {
            this.plantillaCorreoId = plantillaCorreoId;
            this.de = de;
            this.cc = cc;
            this.cco = cco;
            this.plantilla = plantilla;
            this.asunto = asunto;
            this.correoServidorId = correoServidorId;
        }
    }
}
