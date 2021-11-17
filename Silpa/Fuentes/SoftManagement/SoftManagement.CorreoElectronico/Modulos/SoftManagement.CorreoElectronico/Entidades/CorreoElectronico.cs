using System;
using System.Collections.Generic;
using System.Text;

namespace SoftManagement.CorreoElectronico.Entidades
{
    public class CorreoElectronico
    {
        private int correoElectronicoId;
        private DateTime fechaCreacion;
        private string asunto;
        private string de;
        private string para;
        private string cc;
        private string cco;
        private string mensaje;
        private string anexos;
        private DateTime fechaEnvio;
        private int servidorCorreoId;
        private int correoEstadoId;
        private DateTime fechaModificacion;
        private int plantillaCorreoId;
        private bool confirmarEnvio;

        public int CorreoElectronicoId
        {
            get { return correoElectronicoId; }
        }
        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
        }
        public string Asunto
        {
            get { return asunto; }
        }
        public string De
        {
            get { return de; }
        }
        public string Para
        {
            get { return para; }
        }
        public string Cc
        {
            get { return cc; }
        }
        public string Cco
        {
            get { return cco; }
        }
        public string Mensaje
        {
            get { return mensaje; }
        }
        public string Anexos
        {
            get { return anexos; }
        }
        public DateTime FechaEnvio
        {
            get { return fechaEnvio; }
        }
        public int ServidorCorreoId
        {
            get { return servidorCorreoId; }
        }
        public int CorreoEstadoId
        {
            get { return correoEstadoId; }
        }
        public DateTime FechaModificacion
        {
            get { return fechaModificacion; }
        }
        public int PlantillaCorreoId
        {
            get { return plantillaCorreoId; }
        }
        public bool ConfirmarEnvio
        {
            get { return confirmarEnvio; }
        }


        public CorreoElectronico(int correoElectronicoId, DateTime fechaCreacion, string asunto, string de, string para, string cc, string cco, string mensaje, string anexos, int servidorCorreoId, int plantillaCorreoId, bool confirmarEnvio)
        {
            this.correoElectronicoId = correoElectronicoId;
            this.fechaCreacion = fechaCreacion;
            this.asunto = asunto;
            this.de = de;
            this.para = para;
            this.cc = cc;
            this.cco = cco;
            this.mensaje = mensaje;
            this.anexos = anexos;
            this.servidorCorreoId = servidorCorreoId;
            this.plantillaCorreoId = plantillaCorreoId;
            this.confirmarEnvio = confirmarEnvio;
        }
    }


}
