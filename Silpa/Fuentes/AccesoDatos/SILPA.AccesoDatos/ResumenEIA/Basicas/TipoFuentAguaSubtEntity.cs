using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoFuentAguaSubtEntity
    {
        private System.Int32 _ETS_ID;
        private System.String _ETS_TIPO_AGUA_SUBT;
        private System.Boolean _ETS_ACTIVO;
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
        public System.Int32 ETS_ID
        {
            get
            {
                return _ETS_ID;
            }
            set
            {
                _ETS_ID = value;
            }
        }
        public System.String ETS_TIPO_AGUA_SUBT
        {
            get
            {
                return ajustarAncho(_ETS_TIPO_AGUA_SUBT, 100);
            }
            set
            {
                _ETS_TIPO_AGUA_SUBT = value;
            }
        }
        public System.Boolean ETS_ACTIVO
        {
            get
            {
                return _ETS_ACTIVO;
            }
            set
            {
                _ETS_ACTIVO = value;
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
                    return this.ETS_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETS_TIPO_AGUA_SUBT.ToString();
                }
                else if (index == 2)
                {
                    return this.ETS_ACTIVO.ToString();
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
                        this.ETS_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETS_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETS_TIPO_AGUA_SUBT = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETS_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETS_ACTIVO = false;
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
                if (index == "ETS_ID")
                {
                    return this.ETS_ID.ToString();
                }
                else if (index == "ETS_TIPO_AGUA_SUBT")
                {
                    return this.ETS_TIPO_AGUA_SUBT.ToString();
                }
                else if (index == "ETS_ACTIVO")
                {
                    return this.ETS_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETS_ID")
                {
                    try
                    {
                        this.ETS_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETS_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETS_TIPO_AGUA_SUBT")
                {
                    this.ETS_TIPO_AGUA_SUBT = value;
                }
                else if (index == "ETS_ACTIVO")
                {
                    try
                    {
                        this.ETS_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETS_ACTIVO = false;
                    }
                }
            }
        }
    }
}
