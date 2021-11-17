using SILPA.AccesoDatos.EvaluacionREA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class InsumosGrupoBiologicoEntity
    {
        public GrupoBiologicoEntity objGrupoBiologicoEntity { get; set; }
        public List<InsumoRecoleccionEntity> objLstInsumoRecoleccion { get; set; }
        public List<InsumoPreservacionMovilizacionEntity> objLstInsumoPreservacionMovilizacion { get; set; }
        public List<InsumoProfesionalEntity> ObjLstInsumoProfesional { get; set; }
    }
}
