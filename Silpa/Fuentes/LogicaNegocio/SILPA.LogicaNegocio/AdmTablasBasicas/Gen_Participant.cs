using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class Gen_Participant
    {
        private GenParticipantDALC objCasoGenParticipantDALC;

        public Gen_Participant() {
            objCasoGenParticipantDALC = new GenParticipantDALC();
        }

        public DataTable ListarParticipantes()
        {
            return objCasoGenParticipantDALC.ListarParticipantes();
        }
    }
}
