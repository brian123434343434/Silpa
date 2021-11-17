using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity

{
    [Serializable]
    public class InsumoPreservacionMovilizacionEntity
    {
        public GrupoBiologicoEntity objGrupoBiologico { get; set; }
        public TipoSacrificioEntity objTipoSacrificioEntity { get; set; }
        public TipoFijacionPreservacionEntity objTipoFijacionPreservacionEntity { get; set; }
        public TipoMovilizacionEntity objTipoMovilizacionEntity { get; set; }
        public string Key { get { return UniqueKey(); } }
        public string UniqueKey()
        {
            return string.Format("{0}{1}{2}{3}", this.objGrupoBiologico.GrupoBiologicoID, objTipoSacrificioEntity.TipoSacrificioID, objTipoFijacionPreservacionEntity.TipoFijacionPreservacionID, objTipoMovilizacionEntity.TipoMovilizacionID);
        }
    }
}
