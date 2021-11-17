using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoFuenteRuidoEntity
    {
        private System.Int32 _ETR_ID;
        private System.String _ETR_TIPO_FUENT_RUIDO;
        private System.Boolean _ETR_ACTIVO;
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
        public System.Int32 ETR_ID
        {
            get
            {
                return _ETR_ID;
            }
            set
            {
                _ETR_ID = value;
            }
        }
        public System.String ETR_TIPO_FUENT_RUIDO
        {
            get
            {
                return ajustarAncho(_ETR_TIPO_FUENT_RUIDO, 100);
            }
            set
            {
                _ETR_TIPO_FUENT_RUIDO = value;
            }
        }
        public System.Boolean ETR_ACTIVO
        {
            get
            {
                return _ETR_ACTIVO;
            }
            set
            {
                _ETR_ACTIVO = value;
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
                    return this.ETR_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETR_TIPO_FUENT_RUIDO.ToString();
                }
                else if (index == 2)
                {
                    return this.ETR_ACTIVO.ToString();
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
                        this.ETR_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETR_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETR_TIPO_FUENT_RUIDO = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETR_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETR_ACTIVO = false;
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
                if (index == "ETR_ID")
                {
                    return this.ETR_ID.ToString();
                }
                else if (index == "ETR_TIPO_FUENT_RUIDO")
                {
                    return this.ETR_TIPO_FUENT_RUIDO.ToString();
                }
                else if (index == "ETR_ACTIVO")
                {
                    return this.ETR_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETR_ID")
                {
                    try
                    {
                        this.ETR_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETR_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETR_TIPO_FUENT_RUIDO")
                {
                    this.ETR_TIPO_FUENT_RUIDO = value;
                }
                else if (index == "ETR_ACTIVO")
                {
                    try
                    {
                        this.ETR_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETR_ACTIVO = false;
                    }
                }
            }
        }
    }
}
