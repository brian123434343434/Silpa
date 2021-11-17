using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Salvoconducto;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;
using System.Data;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class ParametrizacionSunl
    {
        private ParametrizacionSunlDalc ObjParametrizacionSunlDalc;

        public ParametrizacionSunl()
        {
            ObjParametrizacionSunlDalc = new ParametrizacionSunlDalc();
        }

        public void InsertarEspecie(ParametrizacionSunlIdentity.EspecieTaxonomia ObjEspecieTaxonomia)
        {
            if (ObjEspecieTaxonomia != null)
            {
                ObjParametrizacionSunlDalc.InsertarEspecie(ObjEspecieTaxonomia);
            }
        }

        public int InsertarTipoProducto(ParametrizacionSunlIdentity.TipoProducto ObjTipoProducto)
        {
            int TipoProductoId = ObjParametrizacionSunlDalc.InsertarTipoProducto(ObjTipoProducto);
            return TipoProductoId;
        }

        public int InsertarClaseProducto(ParametrizacionSunlIdentity.ClaseProducto ObjClaseProducto)
        {
            int ClaseProductoId = ObjParametrizacionSunlDalc.InsertarClaseProducto(ObjClaseProducto);
            return ClaseProductoId;
        }

        public void InsertarClaseTipoProducto(List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto> ObjLstClaseProductoXTipoProducto)
        {
            int ClaseProductoID = 0;
            ClaseProductoID = ObjLstClaseProductoXTipoProducto.Select(x => x.ClaseProductoID).FirstOrDefault();
            ObjParametrizacionSunlDalc.EliminarClaseTipoProducto(ClaseProductoID);

            foreach (ParametrizacionSunlIdentity.ClaseProductoXTipoProducto ClaseXTipoprod in ObjLstClaseProductoXTipoProducto)
            {
                ObjParametrizacionSunlDalc.InsertarClaseTipoProducto(ClaseXTipoprod);
            }
        }

        public void InsertarTipoProductoUnidadMedida(List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida> ObjLstTipoProductoUnidadMedida)
        {
            int TipoProductoID = 0;
            TipoProductoID = ObjLstTipoProductoUnidadMedida.Select(x => x.TipoProductoID).First();
            ObjParametrizacionSunlDalc.EliminarTipoProductoUnidadMedida(TipoProductoID);

            foreach (ParametrizacionSunlIdentity.TipoProductoUnidadMedida TipoProdUnMed in ObjLstTipoProductoUnidadMedida)
            {
                ObjParametrizacionSunlDalc.InsertarTipoProductoXUnidadMedida(TipoProdUnMed);
            }
        }

        public List<ParametrizacionSunlIdentity.EspecieTaxonomia> ListarEspecies(string strNombreCientifico, int pClaseRecursoID)
        {
            ParametrizacionSunlDalc ObjParametrizacionSunlDalc = new ParametrizacionSunlDalc();
            List<ParametrizacionSunlIdentity.EspecieTaxonomia> LstEspecies = new List<ParametrizacionSunlIdentity.EspecieTaxonomia>();
            LstEspecies = ObjParametrizacionSunlDalc.ListarEspecies(strNombreCientifico, pClaseRecursoID);
            return LstEspecies;
        }

        public List<ParametrizacionSunlIdentity.ClaseProducto> ListarClaseProducto(int pClaseRecursoID, string pStrClaseProducto)
        {
            ParametrizacionSunlDalc ObjParametrizacionSunlDalc = new ParametrizacionSunlDalc();
            List<ParametrizacionSunlIdentity.ClaseProducto> LstClaseProducto = new List<ParametrizacionSunlIdentity.ClaseProducto>();
            LstClaseProducto = ObjParametrizacionSunlDalc.ListarClaseProducto(pClaseRecursoID, pStrClaseProducto);
            return LstClaseProducto;
        }


        public List<ParametrizacionSunlIdentity.TipoProducto> ListarTipoProducto(string strTipoPrdoucto)
        {
            ParametrizacionSunlDalc ObjParametrizacionSunlDalc = new ParametrizacionSunlDalc();
            List<ParametrizacionSunlIdentity.TipoProducto> LstTipoProducto = new List<ParametrizacionSunlIdentity.TipoProducto>();
            LstTipoProducto = ObjParametrizacionSunlDalc.ListarTipoProducto(strTipoPrdoucto);
            return LstTipoProducto;
        }

        public List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto> ListarClaseTipoProducto(int pClaseProductoID, int pTipoProductoID)
        {
            ParametrizacionSunlDalc ObjParametrizacionSunlDalc = new ParametrizacionSunlDalc();
            List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto> LstClaseTipoProducto = new List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto>();
            LstClaseTipoProducto = ObjParametrizacionSunlDalc.ListarClaseTipoProducto(pClaseProductoID, pTipoProductoID);
            return LstClaseTipoProducto;
        }

        public List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida> ListarTipoProductoUnidadMedida(int pTipoProductoID, int pUnidadMedidaID)
        {
            ParametrizacionSunlDalc ObjParametrizacionSunlDalc = new ParametrizacionSunlDalc();
            List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida> LstTipoProductoUnidadMedida = new List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida>();
            LstTipoProductoUnidadMedida = ObjParametrizacionSunlDalc.ListarTipoProductoUnidadMedida(pTipoProductoID, pUnidadMedidaID);
            return LstTipoProductoUnidadMedida;
        }


        public DataTable ObtenerTipoProductoDisponible(int pClaseProductoID)
        {
            return ObjParametrizacionSunlDalc.ObtenerTipoProductoDisponible(pClaseProductoID);
        }

        public DataTable ObtenerClaseProductoDisponible(int pTipoProductoID)
        {
            return ObjParametrizacionSunlDalc.ObtenerClaseProductoDisponible(pTipoProductoID);
        }

        public DataTable ObtenerUnidadMedidaDisponible(int pTipoProductoID)
        {
            return ObjParametrizacionSunlDalc.ObtenerUnidadMedidaDisponible(pTipoProductoID);
        }


    }
}
