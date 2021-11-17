using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class UnidadAreaEntity
    {
        private System.Int32 _EUA_ID;
        private System.String _EUA_UNIDAD_AREA;
        private System.Boolean _EUA_ACTIVO;
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
        public System.Int32 EUA_ID
        {
            get
            {
                return _EUA_ID;
            }
            set
            {
                _EUA_ID = value;
            }
        }
        public System.String EUA_UNIDAD_AREA
        {
            get
            {
                return ajustarAncho(_EUA_UNIDAD_AREA, 100);
            }
            set
            {
                _EUA_UNIDAD_AREA = value;
            }
        }
        public System.Boolean EUA_ACTIVO
        {
            get
            {
                return _EUA_ACTIVO;
            }
            set
            {
                _EUA_ACTIVO = value;
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
                    return this.EUA_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.EUA_UNIDAD_AREA.ToString();
                }
                else if (index == 2)
                {
                    return this.EUA_ACTIVO.ToString();
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
                        this.EUA_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EUA_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.EUA_UNIDAD_AREA = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.EUA_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EUA_ACTIVO = false;
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
                if (index == "EUA_ID")
                {
                    return this.EUA_ID.ToString();
                }
                else if (index == "EUA_UNIDAD_AREA")
                {
                    return this.EUA_UNIDAD_AREA.ToString();
                }
                else if (index == "EUA_ACTIVO")
                {
                    return this.EUA_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "EUA_ID")
                {
                    try
                    {
                        this.EUA_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EUA_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "EUA_UNIDAD_AREA")
                {
                    this.EUA_UNIDAD_AREA = value;
                }
                else if (index == "EUA_ACTIVO")
                {
                    try
                    {
                        this.EUA_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EUA_ACTIVO = false;
                    }
                }
            }
        }
    }
}
