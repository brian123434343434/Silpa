using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoOlaEntity
    {
        private System.Int32 _EOO_ID;
        private System.String _EOO_TIPO_OLAS;
        private System.Boolean _EOO_ACTIVO;
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
        public System.Int32 EOO_ID
        {
            get
            {
                return _EOO_ID;
            }
            set
            {
                _EOO_ID = value;
            }
        }
        public System.String EOO_TIPO_OLAS
        {
            get
            {
                return ajustarAncho(_EOO_TIPO_OLAS, 100);
            }
            set
            {
                _EOO_TIPO_OLAS = value;
            }
        }
        public System.Boolean EOO_ACTIVO
        {
            get
            {
                return _EOO_ACTIVO;
            }
            set
            {
                _EOO_ACTIVO = value;
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
                    return this.EOO_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.EOO_TIPO_OLAS.ToString();
                }
                else if (index == 2)
                {
                    return this.EOO_ACTIVO.ToString();
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
                        this.EOO_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EOO_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.EOO_TIPO_OLAS = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.EOO_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EOO_ACTIVO = false;
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
                if (index == "EOO_ID")
                {
                    return this.EOO_ID.ToString();
                }
                else if (index == "EOO_TIPO_OLAS")
                {
                    return this.EOO_TIPO_OLAS.ToString();
                }
                else if (index == "EOO_ACTIVO")
                {
                    return this.EOO_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "EOO_ID")
                {
                    try
                    {
                        this.EOO_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EOO_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "EOO_TIPO_OLAS")
                {
                    this.EOO_TIPO_OLAS = value;
                }
                else if (index == "EOO_ACTIVO")
                {
                    try
                    {
                        this.EOO_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EOO_ACTIVO = false;
                    }
                }
            }
        }
    }
}
