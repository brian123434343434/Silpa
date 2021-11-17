using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun; 

namespace SILPA.AccesoDatos.AudienciaPublica
{
   public class AudienciaPublicaIdentity : EntidadSerializable
    {
            
            /// <summary>
            /// constructor sin
            /// </summary>
            public AudienciaPublicaIdentity() { }

            /// <summary>
            /// constructor con parametros
            /// </summary>
            public AudienciaPublicaIdentity(
                int intID_AudienciaPublica,
                string strNumeroSilpa, 
                string strAutoridadAmbiental, 
                string strNombreProyecto,
                string strNumeroExpediente,
                DateTime DateFechaReunionInformativa,
                DateTime DateFechaCelebracionAudiencia)
            {
                this._numeroSilpa = strNumeroSilpa;
                this._autoridadAmbiental = strAutoridadAmbiental;
                this._nombreProyecto = strNombreProyecto;
                this._numeroExpediente = strNumeroExpediente;
                this._fechaReunion=DateFechaReunionInformativa;
                this._fechaAudiencia = DateFechaCelebracionAudiencia; 
            }


      
            #region Declaracion de campos ...

             /// <summary>
            ///  Numero SILPA
            /// </summary>
            private int _ID_AudienciaPublica;

            /// <summary>
            ///  Numero SILPA
            /// </summary>
            private string _numeroSilpa;

            /// <summary>
            /// Nombre la del autoridad ambiental
            /// </summary>
            private string _autoridadAmbiental;

            /// <summary>
            /// Nombre del proyecto
            /// </summary>
            private string _nombreProyecto;


            /// <summary>
            /// Numero de expediente
            /// </summary>
            private string _numeroExpediente;

            /// <summary>
            /// Fecha reunion informativa
            /// </summary>
            private DateTime _fechaReunion;

            /// <summary>
            /// Fecha celebracion audiencia
            /// </summary>
            private DateTime _fechaAudiencia;


            #endregion

            #region Declaracion de propiedades ...
                  
            public int IDAudienciaPublica { get { return this._ID_AudienciaPublica; } set { this._ID_AudienciaPublica = value; } }
            public string  AutoridadAmbiental { get { return this._autoridadAmbiental; } set { this._autoridadAmbiental  = value; } }
            public string NombreProyecto { get { return this._nombreProyecto; } set { this._nombreProyecto = value; } }
            public string NumeroExpediente { get { return this._numeroExpediente; } set { this._numeroExpediente = value; } }
            public string NumeroSilpa { get { return this._numeroSilpa; } set { this._numeroSilpa = value; } }
            public DateTime FechaReunionInformativa { get { return this._fechaReunion; } set { this._fechaReunion = value; } }
            public DateTime FechaCelebracionAudiencia { get { return this._fechaAudiencia; } set { this._fechaAudiencia = value; } }

            #endregion


        }

    }

