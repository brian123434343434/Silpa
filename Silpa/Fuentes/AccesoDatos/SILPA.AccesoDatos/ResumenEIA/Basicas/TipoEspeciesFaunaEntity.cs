using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoEspeciesFaunaEntity
    {
        private System.Int32 _ETF_ID;
        private System.String _ETF_TIPO_ESPECIE;
        private System.Boolean _ETF_ACTIVO;
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
        public System.Int32 ETF_ID
        {
            get
            {
                return _ETF_ID;
            }
            set
            {
                _ETF_ID = value;
            }
        }
        public System.String ETF_TIPO_ESPECIE
        {
            get
            {
                return ajustarAncho(_ETF_TIPO_ESPECIE, 100);
            }
            set
            {
                _ETF_TIPO_ESPECIE = value;
            }
        }
        public System.Boolean ETF_ACTIVO
        {
            get
            {
                return _ETF_ACTIVO;
            }
            set
            {
                _ETF_ACTIVO = value;
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
                    return this.ETF_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETF_TIPO_ESPECIE.ToString();
                }
                else if (index == 2)
                {
                    return this.ETF_ACTIVO.ToString();
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
                        this.ETF_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETF_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETF_TIPO_ESPECIE = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETF_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETF_ACTIVO = false;
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
                if (index == "ETF_ID")
                {
                    return this.ETF_ID.ToString();
                }
                else if (index == "ETF_TIPO_ESPECIE")
                {
                    return this.ETF_TIPO_ESPECIE.ToString();
                }
                else if (index == "ETF_ACTIVO")
                {
                    return this.ETF_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETF_ID")
                {
                    try
                    {
                        this.ETF_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETF_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETF_TIPO_ESPECIE")
                {
                    this.ETF_TIPO_ESPECIE = value;
                }
                else if (index == "ETF_ACTIVO")
                {
                    try
                    {
                        this.ETF_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETF_ACTIVO = false;
                    }
                }
            }
        }
    }
}
