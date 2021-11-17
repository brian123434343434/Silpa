using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoDocumentoEntity
    {
        private System.Int32 _ETD_ID;
        private System.String _ETD_TIPO_DOCUMENTO;
        private System.Boolean _ETD_ACTIVO;
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
        public System.Int32 ETD_ID
        {
            get
            {
                return _ETD_ID;
            }
            set
            {
                _ETD_ID = value;
            }
        }
        public System.String ETD_TIPO_DOCUMENTO
        {
            get
            {
                return ajustarAncho(_ETD_TIPO_DOCUMENTO, 100);
            }
            set
            {
                _ETD_TIPO_DOCUMENTO = value;
            }
        }
        public System.Boolean ETD_ACTIVO
        {
            get
            {
                return _ETD_ACTIVO;
            }
            set
            {
                _ETD_ACTIVO = value;
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
                    return this.ETD_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETD_TIPO_DOCUMENTO.ToString();
                }
                else if (index == 2)
                {
                    return this.ETD_ACTIVO.ToString();
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
                        this.ETD_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETD_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETD_TIPO_DOCUMENTO = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETD_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETD_ACTIVO = false;
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
                if (index == "ETD_ID")
                {
                    return this.ETD_ID.ToString();
                }
                else if (index == "ETD_TIPO_DOCUMENTO")
                {
                    return this.ETD_TIPO_DOCUMENTO.ToString();
                }
                else if (index == "ETD_ACTIVO")
                {
                    return this.ETD_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETD_ID")
                {
                    try
                    {
                        this.ETD_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETD_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETD_TIPO_DOCUMENTO")
                {
                    this.ETD_TIPO_DOCUMENTO = value;
                }
                else if (index == "ETD_ACTIVO")
                {
                    try
                    {
                        this.ETD_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETD_ACTIVO = false;
                    }
                }
            }
        }
    }
}
