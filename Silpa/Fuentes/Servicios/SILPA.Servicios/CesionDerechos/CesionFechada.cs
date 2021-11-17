using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.LogicaNegocio.Generico;

namespace SILPA.Servicios.CesionDerechos
{
    public class CesionFechada
    {
        public AccesoDatos.CesionDeDerechos.CesionEntity LeerCesion(string datosCesion)
        {
            AccesoDatos.CesionDeDerechos.CesionEntity ces = new SILPA.AccesoDatos.CesionDeDerechos.CesionEntity();
            ces = (AccesoDatos.CesionDeDerechos.CesionEntity)ces.Deserializar(datosCesion);
            return ces;
        }

        /// <summary>
        /// HAVA: 04-oct-10
        /// </summary>
        /// <param name="IdProcessInstance">Long: identificador del proceso </param>
        public void ActualizarParticipanteForma(long IdProcessInstance) 
        { 
            Formulario form = new Formulario();
            form.ActualizarParticipanteFormulario(IdProcessInstance);
        }

    }
}
