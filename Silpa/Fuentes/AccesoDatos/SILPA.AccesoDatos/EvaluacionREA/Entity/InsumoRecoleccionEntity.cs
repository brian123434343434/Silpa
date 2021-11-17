using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class InsumoRecoleccionEntity 
    {
        public GrupoBiologicoEntity objGrupoBiologico { get; set; }
        public TecnicaMuestreoEntity objTecnicaMuestreoEntity { get; set; }
        public List<CaracteristicaEntity> ObjLstCaracteristicaEntity { get; set; }
        public UnidadMuestreoEntity objUnidadMuestreoEntity { get; set; }
        public List<EsfuerzoMuestreoEntity> objLstEsfuerzoMuestreoEntity { get; set; }
        public RecoleccionEntity objRecoleccionEntity { get; set; }
        public string Key { get { return UniqueKey(); } }

        public string UniqueKey()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}", this.objGrupoBiologico.GrupoBiologicoID, objTecnicaMuestreoEntity.TecnicaMuestreoID, cadenaCaracteristicas(), objUnidadMuestreoEntity.UnidadMuestreoID, cadenaEsfuerzos(), objRecoleccionEntity.RecoleccionID);
        }

        public string cadenaCaracteristicas()
        {
            string str_caracateristica = string.Empty;
            foreach (var item in ObjLstCaracteristicaEntity)
            {
                str_caracateristica += item.CaracteristicaID;
            }
            return str_caracateristica;
        }
        public string cadenaEsfuerzos()
        {
            string str_esfuerzos = string.Empty;
            foreach (var item in objLstEsfuerzoMuestreoEntity)
            {
                str_esfuerzos += item.EsfuerzoMuestreoID;
            }
            return str_esfuerzos;
        }
        
    }
}
