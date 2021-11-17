using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using System.Data;

namespace SILPA.LogicaNegocio.Generico
{
    public class PSE
    {
        #region atributos
        private PseDalc oDalc;
        private PseIdentity oIdentity;
        #endregion

        #region Propiedades
        public PseDalc ODalc
        {
            get { return oDalc; }
            set { oDalc = value; }
        }
        public PseIdentity OIdentity
        {
            get { return oIdentity; }
            set { oIdentity = value; }
        }
        #endregion

        #region Costructores
        public PSE()
        {
            ODalc = new PseDalc();
            oIdentity = new PseIdentity();
        }
        #endregion

        #region Metodos
        
        /// <summary>
        /// Permite insertar lo valores PSE para una autoridad ambiental especifica
        /// </summary>
        void InsertarPse()
        {
            this.ODalc.InsertarPse(ref this.oIdentity);
        }

        /// <summary>
        /// Permite consultar lo valores PSE para una autoridad ambiental especifica
        /// </summary>
        void ObtenerPseXAutAmbiental()
        {
            this.ODalc.ObtenerPseXAutAmbiental(ref this.oIdentity);
        }
        #endregion



    }
}
