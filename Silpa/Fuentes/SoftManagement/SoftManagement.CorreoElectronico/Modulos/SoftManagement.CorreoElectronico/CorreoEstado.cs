using System;
using System.Collections.Generic;
using System.Text;

namespace SoftManagement.CorreoElectronico
{
    public enum CorreoEstado
    {
        NoEnviado = 0,
        Enviado = 1,
        Inhabilitado = 2,
        #region jmartinez agrego un tipo de severidad para identificar cuando el correo generar error en la excepcion
        Error = 3,
        #endregion

    }
}
