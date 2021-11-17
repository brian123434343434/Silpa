using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.AdmTablasBasicas
{
    public class SolicitudesEntity
    {

            public int      ID_SOLICITUD            {  get; set; }
			public int      TAR_SOL_ID              { get; set; } 
			public string   SOL_NUM_SILPA           { get; set; }
			public int      SOL_ID_SOLICITANTE      { get; set; }
			public string   AUT_NOMBRE              { get; set; } 
			public string   TRA_NOMBRE              { get; set; } 
			public string   SOL_FECHA_CREACION      { get; set; }
            public string   NOMBRE_COMPLETO         { get; set; } 
    }
}
