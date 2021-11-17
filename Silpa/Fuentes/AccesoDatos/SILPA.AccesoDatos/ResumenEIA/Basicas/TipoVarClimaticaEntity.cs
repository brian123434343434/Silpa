using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoVarClimaticaEntity
    {
        private System.Int32 _ETV_ID;
        private System.String _ETV_TIPO_VAR_CLIMATICA;
        private System.Boolean _ETV_ACTIVO;
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
        public System.Int32 ETV_ID
        {
            get
            {
                return _ETV_ID;
            }
            set
            {
                _ETV_ID = value;
            }
        }
        public System.String ETV_TIPO_VAR_CLIMATICA
        {
            get
            {
                return ajustarAncho(_ETV_TIPO_VAR_CLIMATICA, 100);
            }
            set
            {
                _ETV_TIPO_VAR_CLIMATICA = value;
            }
        }
        public System.Boolean ETV_ACTIVO
        {
            get
            {
                return _ETV_ACTIVO;
            }
            set
            {
                _ETV_ACTIVO = value;
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
                    return this.ETV_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETV_TIPO_VAR_CLIMATICA.ToString();
                }
                else if (index == 2)
                {
                    return this.ETV_ACTIVO.ToString();
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
                        this.ETV_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETV_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETV_TIPO_VAR_CLIMATICA = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETV_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETV_ACTIVO = false;
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
                if (index == "ETV_ID")
                {
                    return this.ETV_ID.ToString();
                }
                else if (index == "ETV_TIPO_VAR_CLIMATICA")
                {
                    return this.ETV_TIPO_VAR_CLIMATICA.ToString();
                }
                else if (index == "ETV_ACTIVO")
                {
                    return this.ETV_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETV_ID")
                {
                    try
                    {
                        this.ETV_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETV_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETV_TIPO_VAR_CLIMATICA")
                {
                    this.ETV_TIPO_VAR_CLIMATICA = value;
                }
                else if (index == "ETV_ACTIVO")
                {
                    try
                    {
                        this.ETV_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETV_ACTIVO = false;
                    }
                }
            }
        }
    }
}
