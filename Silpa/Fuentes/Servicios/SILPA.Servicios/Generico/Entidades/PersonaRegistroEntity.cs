using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.Servicios.Generico.Entidades
{
    public class PersonaRegistroEntity : EntidadSerializable
    {

        #region Propiedades

            /// <summary>
            /// Identificador de la persona en VITAL
            /// </summary>
            public long PersonaVITALID { get; set; }

            /// <summary>
            /// Identificador de la autoridad ambiental
            /// </summary>
            public int AutoridadAmbientalID { get; set; }

            /// <summary>
            /// Identificador del tipo de persona
            /// </summary>
            public int TipoPersonaID { get; set; }

            /// <summary>
            /// Primer nombre de la persona
            /// </summary>
            public string PrimerNombre { get; set; }

            /// <summary>
            /// Segundo nombre de la persona
            /// </summary>
            public string SegundoNombre { get; set; }

            /// <summary>
            /// Primer apellido de la persona
            /// </summary>
            public string PrimerApellido { get; set; }

            /// <summary>
            /// Segundo apellido de la persona
            /// </summary>
            public string SegundoApellido { get; set; }

            /// <summary>
            /// Razón social de la persona
            /// </summary>
            public string RazonSocial { get; set; }

            /// <summary>
            /// Razón social de la persona
            /// </summary>
            public int TipoDocumentoID { get; set; }

            /// <summary>
            /// Número de Documento
            /// </summary>
            public string NumeroDocumento { get; set; }

            /// <summary>
            /// Identificador del departamento donde se expidio el doumento de identificación
            /// </summary>
            public int DepartamentoOrigenDocumentoID { get; set; }

            /// <summary>
            /// Identificador del municipio donde se expidio el doumento de identificación
            /// </summary>
            public int MunicipioOrigenDocumentoID { get; set; } 

            /// <summary>
            /// Dirección de contacto de la persona
            /// </summary>
            public string DireccionContacto { get; set; }

            /// <summary>
            /// Identificador del país al cual pertnece la dirección de contacto
            /// </summary>
            public int PaisContactoID { get; set; }

            /// <summary>
            /// Identificador del departamento al cual pertenece la dirección de contacto
            /// </summary>
            public int DepartamentoContactoID { get; set; }

            /// <summary>
            /// Identificador del municipio al cual pertenece la dirección de contacto
            /// </summary>
            public int MunicipioContactoID { get; set; }            

            /// <summary>
            /// Dirección de correspondencia de la persona
            /// </summary>
            public string DireccionCorrespondencia { get; set; }

            /// <summary>
            /// Identificador del país al cual pertenece la dirección de correspondencia
            /// </summary>
            public int PaisCorrespondenciaID { get; set; }

            /// <summary>
            /// Identificador del departamento al cual pertenece la dirección de correspondencia
            /// </summary>
            public int DepartamentoCorrespondenciaID { get; set; }

            /// <summary>
            /// Identificador del municipio al cual pertenece la dirección de correspondencia
            /// </summary>
            public int MunicipioCorrespondenciaID { get; set; }

            /// <summary>
            /// Número telefonico de la persona
            /// </summary>
            public string Telefono { get; set; }

            /// <summary>
            /// Número celular de la persona
            /// </summary>
            public string Celular { get; set; }

            /// <summary>
            /// Número fax de la persona
            /// </summary>
            public string Fax { get; set; }

            /// <summary>
            /// Correo electrónico de la persona
            /// </summary>
            public string CorreoElectronico { get; set; }

            /// <summary>
            /// Indica si el usuario autoriza el envío de notificaciones
            /// </summary>
            public bool AutorizaEnvioNotificaciones { get; set; }

        #endregion

    }
}
