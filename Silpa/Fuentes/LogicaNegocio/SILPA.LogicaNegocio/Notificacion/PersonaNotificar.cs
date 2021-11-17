using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Notificacion;
using System.Data;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class PersonaNotificar
    {
        #region Metodos Publicos

        /// <summary>
        /// Retorna la información de las personas que se encuentran relacionadas al acto administrativo
        /// </summary>
        /// <param name="p_lngActoID">long con el identificador del acto administrativo</param>
        /// <returns>List con la información de las personas</returns>
        public List<PersonaNotificarEntity> ObtenerPersonasActoAdministrativo(long p_lngActoID)
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            return objPersona.ObtenerPersonas(new object[] { null, p_lngActoID, null, null, null, null });
        }


        /// <summary>
        /// Obtener el listado de direcciones de una persona
        /// </summary>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <returns>DataSet con la información de las direcciones</returns>
        public DataSet ObtenerListadoDireccionesNotificar(long p_lngPersonaID)
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            return objPersona.ObtenerListadoDireccionesNotificar(p_lngPersonaID);
        }

        /// <summary>
        /// Obtener el listado de direcciones de una persona
        /// </summary>
        /// <param name="p_strNumeroIdentificacion">string con el numero de identificacion</param>
        /// <returns>List con la información de las direcciones</returns>
        public List<DireccionNotificacionEntity> ObtenerListadoDireccionesNotificarNumeroIdentificacion(string p_strNumeroIdentificacion)
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            return objPersona.ObtenerListadoDireccionesNotificarNumeroIdentificacion(p_strNumeroIdentificacion);
        }

        /// <summary>
        /// Obtener la información de la dirección de la persona
        /// </summary>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_lngDIreccionID">long con el identificador de la dirección.</param>
        /// <returns>DataRow con la información de la dirección</returns>
        public DataRow ObtenerInformacionDireccionPersona(long p_lngPersonaID, long p_lngDIreccionID)
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            return objPersona.ObtenerInformacionDireccionPersona(p_lngPersonaID, p_lngDIreccionID);
        }

        /// <summary>
        /// Obtener la información de la dirección de la persona
        /// </summary>
        /// <param name="p_lngDIreccionID">long con el identificador de la direccion</param>
        /// <returns>DireccionNotificacionEntity con la información de la dirección</returns>
        public DireccionNotificacionEntity ObtenerInformacionDireccionPersona(long p_lngDIreccionID)
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            return objPersona.ObtenerInformacionDireccionPersona(p_lngDIreccionID);
        }

        /// <summary>
        /// Obtener el listado de correos de una persona
        /// </summary>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <returns>DataSet con la información de las direcciones</returns>
        public DataSet ObtenerListadoCorreosNotificar(long p_lngPersonaID)
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            return objPersona.ObtenerListadoCorreosNotificar(p_lngPersonaID);
        }

        /// <summary>
        /// Obtener el listado de correos de una persona
        /// </summary>
        /// <param name="p_strNumeroIdentificacion">string con el numero de identificacion</param>
        /// <returns>DataSet con la información de las direcciones</returns>
        public List<string> ObtenerListadoCorreosNotificarNumeroIdentificacion(string p_strNumeroIdentificacion)
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            return objPersona.ObtenerListadoCorreosNotificarNumeroIdentificacion(p_strNumeroIdentificacion);
        }

        /// <summary>
        /// Obtener el listado de correos de una persona
        /// </summary>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_strNumeroVital">string con el numero Vital. Opcional</param>
        /// <returns>DataSet con la información de las direcciones</returns>
        public DataSet ObtenerListadoCorreosNotificar(long p_lngPersonaID, string p_strNumeroVital = "")
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            return objPersona.ObtenerListadoCorreosNotificar(p_lngPersonaID, p_strNumeroVital);
        }

        /// <summary>
        /// Realizar el avance automatico de los estados que se encuentren pendientes de cambio
        /// </summary>
        /// <returns>DataTable con la información de los estados que se deben avanzar</returns>
        public DataTable ObtenerListadoEstadosPendientesAvanceAutomatico()
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            return objPersona.ObtenerListadoEstadosPendientesAvanceAutomatico();
        }

        public PersonaNotificarEntity ObtenerPersona(int intPersonaID)
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            return objPersona.ObtenerPersona(new object[] { intPersonaID,-1,-1,-1,-1,"" });
        }


        /// <summary>
        /// Obtener la información de personas asociadas a un acto por tipo de notificación
        /// </summary>
        /// <param name="p_lngActoId">long con el identificador del acto administrativo</param>
        /// <param name="p_intTipoNotificacionID">long con el identificador del tipo de notificación</param>
        /// <param name="p_lngPersonaID">long con el identificador de la persona. Opcional</param>
        /// <returns>DataTable con la información de las personas</returns>
        public DataTable ObtenerListadoPersonasNotificarActoAdmin(long p_lngActoId, int p_intTipoNotificacionID, long p_lngPersonaID = 0)
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            return objPersona.ObtenerListadoPersonasNotificarActoAdmin(p_lngActoId, p_intTipoNotificacionID, p_lngPersonaID);
        }


        /// <summary>
        /// Modificar el estado de una persona
        /// </summary>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_intEstadoPersonaID">int con el estado de la persona</param>
        public void ModificarEstadoPersona(long p_lngPersonaID, int p_intEstadoPersonaID)
        {
            PersonaNotificarDalc objPersona = new PersonaNotificarDalc();
            objPersona.ModificarEstadoPersona(p_lngPersonaID, p_intEstadoPersonaID);
        }

        #endregion
    }
}

