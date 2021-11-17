using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoInstitucionEntity
    {
        private System.Int32 _ETI_ID;
        private System.String _ETI_TIPO_INSTITUCION;
        private System.Boolean _ETI_ACTIVO;
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
        public System.Int32 ETI_ID
        {
            get
            {
                return _ETI_ID;
            }
            set
            {
                _ETI_ID = value;
            }
        }
        public System.String ETI_TIPO_INSTITUCION
        {
            get
            {
                return ajustarAncho(_ETI_TIPO_INSTITUCION, 100);
            }
            set
            {
                _ETI_TIPO_INSTITUCION = value;
            }
        }
        public System.Boolean ETI_ACTIVO
        {
            get
            {
                return _ETI_ACTIVO;
            }
            set
            {
                _ETI_ACTIVO = value;
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
                    return this.ETI_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETI_TIPO_INSTITUCION.ToString();
                }
                else if (index == 2)
                {
                    return this.ETI_ACTIVO.ToString();
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
                        this.ETI_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETI_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETI_TIPO_INSTITUCION = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETI_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETI_ACTIVO = false;
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
                if (index == "ETI_ID")
                {
                    return this.ETI_ID.ToString();
                }
                else if (index == "ETI_TIPO_INSTITUCION")
                {
                    return this.ETI_TIPO_INSTITUCION.ToString();
                }
                else if (index == "ETI_ACTIVO")
                {
                    return this.ETI_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETI_ID")
                {
                    try
                    {
                        this.ETI_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETI_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETI_TIPO_INSTITUCION")
                {
                    this.ETI_TIPO_INSTITUCION = value;
                }
                else if (index == "ETI_ACTIVO")
                {
                    try
                    {
                        this.ETI_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETI_ACTIVO = false;
                    }
                }
            }
        }
    }
}
