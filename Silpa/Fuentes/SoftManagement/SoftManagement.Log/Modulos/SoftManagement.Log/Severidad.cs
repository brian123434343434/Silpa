using System;
using System.Collections.Generic;
using System.Text;

namespace SoftManagement.Log
{
    public enum Severidad
    {
        Critico = 0,
        Advertencia = 1,
        Informativo = 2,
        #region jmartinez agrego un tipo de severidad para identificar cuando el correo generar error en la excepcion
        Error = 3,
        #endregion

    }
}