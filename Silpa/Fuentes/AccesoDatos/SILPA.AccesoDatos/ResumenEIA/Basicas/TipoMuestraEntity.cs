using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoMuestraEntity
    {
        private System.Int32 _ETM_ID;
        private System.String _ETM_TIPO_MUESTRA;
        private System.Boolean _ETM_ACTIVO;
        //
        // Este método se usará para ajustar los anchos de las propiedades
        private string ajustarAncho(string cadena, int ancho)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(new String(' ', ancho));
            // devolver la cadena quitando los espacios en blanco
            // esto asegura que no se devolverá un tamaño mayor ni espacios "extras"
            return (cadena + sb.ToString()).Substring(0, ancho).Trim();
        }
        //
        // Las propiedades públicas
        // TODO: Revisar los tipos de las propiedades
        public System.Int32 ETM_ID
        {
            get
            {
                return _ETM_ID;
            }
            set
            {
                _ETM_ID = value;
            }
        }
        public System.String ETM_TIPO_MUESTRA
        {
            get
            {
                return ajustarAncho(_ETM_TIPO_MUESTRA, 100);
            }
            set
            {
                _ETM_TIPO_MUESTRA = value;
            }
        }
        public System.Boolean ETM_ACTIVO
        {
            get
            {
                return _ETM_ACTIVO;
            }
            set
            {
                _ETM_ACTIVO = value;
            }
        }
        //
        public string this[int index]
        {
            // Devuelve el contenido del campo indicado en index
            // (el índice corresponde con la columna de la tabla)
            get
            {
                if (index == 0)
                {
                    return this.ETM_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETM_TIPO_MUESTRA.ToString();
                }
                else if (index == 2)
                {
                    return this.ETM_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == 0)
                {
                    try
                    {
                        this.ETM_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETM_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETM_TIPO_MUESTRA = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETM_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETM_ACTIVO = false;
                    }
                }
            }
        }
        public string this[string index]
        {
            // Devuelve el contenido del campo indicado en index
            // (el índice corresponde al nombre de la columna)
            get
            {
                if (index == "ETM_ID")
                {
                    return this.ETM_ID.ToString();
                }
                else if (index == "ETM_TIPO_MUESTRA")
                {
                    return this.ETM_TIPO_MUESTRA.ToString();
                }
                else if (index == "ETM_ACTIVO")
                {
                    return this.ETM_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETM_ID")
                {
                    try
                    {
                        this.ETM_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETM_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETM_TIPO_MUESTRA")
                {
                    this.ETM_TIPO_MUESTRA = value;
                }
                else if (index == "ETM_ACTIVO")
                {
                    try
                    {
                        this.ETM_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETM_ACTIVO = false;
                    }
                }
            }
        }
    }
}
