using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SILPA.Comun;

using SILPA.LogicaNegocio;
using SILPA.LogicaNegocio.ICorreo;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Generico
{
    public class Sector
    {
        public Sector()
        { 
        }

        public DataTable ConsultarSectores()
        {
            SectorDalc  sector = new SectorDalc();
            return sector.ListarSector().Tables[0];
        }
    }
}
