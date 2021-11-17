using System;
using System.Collections.Generic;
using System.Text;

namespace SoftManagement.CorreoElectronico.Entidades
{
    public class CorreoServidor
    {
        private int correoServidorId;
        private string nombreServidor;
        private string host;
        private string usuario;
        private string contrasena;
        private string separador;

        private string puerto;
        private bool aplicaSeguridad;

        public string Puerto
        {
            get { return puerto; }
            set { puerto = value; }
        }


        public int CorreoServidorId 
        {
            get { return correoServidorId; }
        }
        public string NombreServidor
        {
            get { return nombreServidor; }
        }
        public string Host
        {
            get { return host; }
        }
        public string Usuario
        {
            get { return usuario; }
        }
        public string Contrasena
        {
            get { return contrasena; }
        }
        public string Separador
        {
            get { return separador; }
        }

        public bool AplicaSeguridad
        {
            get { return aplicaSeguridad; }
        }

        public CorreoServidor(int correoServidorId, string nombreServidor, string host, string usuario, string contrasena, string separador)
        {
            this.correoServidorId = correoServidorId;
            this.nombreServidor = nombreServidor;
            this.host = host;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.separador = separador;
        }
        public CorreoServidor(int correoServidorId, string nombreServidor, string host, string usuario, string contrasena, string separador,string puerto)
        {
            this.correoServidorId = correoServidorId;
            this.nombreServidor = nombreServidor;
            this.host = host;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.separador = separador;
            this.puerto = puerto;
        }
        public CorreoServidor(int correoServidorId, string nombreServidor, string host, string usuario, string contrasena, string separador, string puerto,bool aplicaSeguridad)
        {
            this.correoServidorId = correoServidorId;
            this.nombreServidor = nombreServidor;
            this.host = host;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.separador = separador;
            this.puerto = puerto;
            this.aplicaSeguridad = aplicaSeguridad;
        }
    }
}
