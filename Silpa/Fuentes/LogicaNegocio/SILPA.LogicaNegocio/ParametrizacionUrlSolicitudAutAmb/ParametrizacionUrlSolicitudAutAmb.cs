using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.ParametrizacionUrlSolicitudAutAmbDalc;
using System.Data;

namespace SILPA.LogicaNegocio.ParametrizacionUrlSolicitudAutAmb
{
    public class ParametrizacionUrlSolicitudAutAmb
    {
        private ParametrizacionUrlSolicitudAutAmbDalc ParametrizacionUrlSolicitudAutAmbDalc;

        public ParametrizacionUrlSolicitudAutAmb()
        {
            ParametrizacionUrlSolicitudAutAmbDalc = new ParametrizacionUrlSolicitudAutAmbDalc();
        }

        public void ActualizarUrlSolicitudXAutAmb(int ID_AA, int ID_TRAMITE, string URL, Int32 ID_PARTICIPANTE)
        {
            ParametrizacionUrlSolicitudAutAmbDalc.ActualizarUrlSolicitudXAutAmb(ID_AA, ID_TRAMITE, URL, ID_PARTICIPANTE);
        }

        public void EliminarUrlSolicitudXAutAmb(int ID_AA, int ID_TRAMITE)
        {
            ParametrizacionUrlSolicitudAutAmbDalc.EliminarUrlSolicitudXAutAmb(ID_AA, ID_TRAMITE);
        }

        public DataSet ConsultarUrlSolicitudXAutAmb(int ID_AA, int ID_TRAMITE)
        {
            DataSet ds = new DataSet();
            ds = ParametrizacionUrlSolicitudAutAmbDalc.ConsultarUrlSolicitudXAutAmb(ID_AA, ID_TRAMITE);
            return ds;
        }

        public DataSet ObtenerListaTramites()
        {
            DataSet ds = new DataSet();
            ds = ParametrizacionUrlSolicitudAutAmbDalc.ListarParametrizacion("ID,NOMBRE_TIPO_TRAMITE", "GEN_TIPO_TRAMITE", "VISIBLE = 1", "");
            return ds;
        }


    }
}
