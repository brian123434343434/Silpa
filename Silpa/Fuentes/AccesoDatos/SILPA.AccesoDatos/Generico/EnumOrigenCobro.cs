using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SILPA.AccesoDatos.Generico
{
    public class OrigenCobroAttribute : System.Attribute
    {

        private string _strValor;

        /// <summary>
        /// Creadora
        /// </summary>
        /// <param name="p_strValor">string con el valor</param>
        public OrigenCobroAttribute(string p_strValor)
        {
           this._strValor = p_strValor;
        }

        /// <summary>
        /// Retorna el valor
        /// </summary>
        public string Valor
        {
            get { return this._strValor; }
        }

        /// <summary>
        /// Retorna el valor string de un enum
        /// </summary>
        /// <param name="p_objOrigenCobro">Enum con el origen del cobro</param>
        /// <returns></returns>
        public static string GetStringValue(Enum p_objOrigenCobro)
        {
            Type type = p_objOrigenCobro.GetType();
            string[] flags = p_objOrigenCobro.ToString().Split(',').Select(x => x.Trim()).ToArray();
            List<string> lstValores = new List<string>();

            for (int i = 0; i < flags.Length; i++)
            {
                FieldInfo fi = type.GetField(flags[i].ToString());

                OrigenCobroAttribute[] attrs =
                   fi.GetCustomAttributes(typeof(OrigenCobroAttribute),
                                           false) as OrigenCobroAttribute[];
                if (attrs.Length > 0)
                {
                    lstValores.Add(attrs[0].Valor);
                }
            }

            return string.Join(",", lstValores.ToArray());

        }
    }

    public enum EnumOrigenCobro
    {
        [OrigenCobro("CB")]
        COBRO,
        [OrigenCobro("AL")]
        AUTOLIQUIDACION
    }
}
