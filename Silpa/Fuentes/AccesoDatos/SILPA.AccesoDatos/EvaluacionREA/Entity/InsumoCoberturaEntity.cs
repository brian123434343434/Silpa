using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class InsumoCoberturaEntity
    {
        public int DepartamentoID { get; set; }
        public string Departamento { get; set; }
        public int MunicipioID { get; set; }
        public string Municipio { get; set; }
        public string Key { get { return UniqueKey(); } }
        public string UniqueKey()
        {
            return string.Format("{0}{1}", this.DepartamentoID, this.MunicipioID);
        }
    }
}
