using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class CargueSaldo
    {
         private CargueSaldoDalc vCargueSaldoDalc;

         public CargueSaldo()
        {
            vCargueSaldoDalc = new CargueSaldoDalc();
        }

        public CargueSaldoIdentity ConsultaCargueSaldoTarea(int TareaID)
        {
            return vCargueSaldoDalc.ConsultaCargueSaldoTarea(TareaID);
        }
        public void InsertaCargueSaldoTarea(CargueSaldoIdentity vCargueSaldoIdentity)
        {
            vCargueSaldoDalc.InsertaCargueSaldoTarea(vCargueSaldoIdentity);
        }
    }
}
