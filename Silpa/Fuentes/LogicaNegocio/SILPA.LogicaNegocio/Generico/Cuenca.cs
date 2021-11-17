using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using System.Data;
using SILPA.LogicaNegocio.Enumeracion;   

namespace SILPA.LogicaNegocio.Generico
{
    public class Cuenca
    {
        public Cuenca()
        { 

        }

        public CuencaIdentity ConsultarCuenca(int i)
        {
            CuencaDalc dalc = new CuencaDalc();
            DataTable dt = dalc.ListarCuencas().Tables[0];
            CuencaIdentity cuenca = null;

            foreach(DataRow r  in dt.Rows) 
            {
                if (Convert.ToInt32(r["CUE_ID"]) == i)
                {
                    cuenca = new CuencaIdentity();
                    cuenca.Id = Convert.ToInt32(r["CUE_ID"]);
                    cuenca.Nombre = r["CUE_NOMBRE"].ToString();
                    if (Convert.ToInt32(r["CUE_ACTIVO"]) == Convert.ToInt32(EnumeracionActivo.ESTADO.ACTIVO))
                        cuenca.Activo = true;
                    return cuenca;
                }
            }
            return cuenca;
        }

        public DataTable ConsultarCuencas()
        {
            CuencaDalc cuenca = new CuencaDalc();
            return cuenca.ListarCuencas().Tables[0];   
        }
    }
}
