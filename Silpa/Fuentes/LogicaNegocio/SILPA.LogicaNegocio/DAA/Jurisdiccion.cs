using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.DAA;
using SILPA.Comun;
using System.Data;
using System.Collections;

namespace SILPA.LogicaNegocio.DAA
{
    public class Jurisdiccion
    {
        public JurisdiccionIdentity Identity;
        private JurisdiccionDalc Dalc;

        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public Jurisdiccion()
        {
            objConfiguracion = new Configuracion();
            Identity = new JurisdiccionIdentity();
            Dalc = new JurisdiccionDalc();
        }

        /// <summary>
        /// Método que inserta una jurisdicción
        /// </summary>
        /// <param name="_idMunicipio"></param>
        /// <param name="_idAutoridad"></param>
        public void InsertarJurisdiccion(int _idMunicipio, int _idAutoridad)
        {
            try
            {
                this.Identity.IdMunicipio = _idMunicipio;
                this.Identity.AutoridadAmbiental = _idAutoridad;
                Dalc.InsertarJurisdiccion(ref this.Identity);
            }
            catch (Exception ex)
            {
                throw new Exception("Mensaje de error al insertar la jurisdicción: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que retorna un listado de jurisdicciones
        /// </summary>
        /// <param name="v">Identificador del municipio</param>
        /// <returns>conjunto de datos: [JUR_ID] - [AUT_NOMBRE] - [MUN_NOMBRE] - [FECHA_INSERCION]</returns>
        public DataSet ListaJurisdiccion(Nullable<Int64> _idMunicipio)
        {
            return Dalc.ListaJurisdiccion(_idMunicipio);
        }

    }
}
