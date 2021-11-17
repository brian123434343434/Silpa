using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoAcuiferoEntity
    {
        private System.Int32 _ETA_ID;
        private System.String _ETA_TIPO_ACUIFERO;
        private System.Boolean _ETA_ACTIVO;
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
        public System.Int32 ETA_ID
        {
            get
            {
                return _ETA_ID;
            }
            set
            {
                _ETA_ID = value;
            }
        }
        public System.String ETA_TIPO_ACUIFERO
        {
            get
            {
                return ajustarAncho(_ETA_TIPO_ACUIFERO, 100);
            }
            set
            {
                _ETA_TIPO_ACUIFERO = value;
            }
        }
        public System.Boolean ETA_ACTIVO
        {
            get
            {
                return _ETA_ACTIVO;
            }
            set
            {
                _ETA_ACTIVO = value;
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
                    return this.ETA_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETA_TIPO_ACUIFERO.ToString();
                }
                else if (index == 2)
                {
                    return this.ETA_ACTIVO.ToString();
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
                        this.ETA_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETA_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETA_TIPO_ACUIFERO = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETA_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETA_ACTIVO = false;
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
                if (index == "ETA_ID")
                {
                    return this.ETA_ID.ToString();
                }
                else if (index == "ETA_TIPO_ACUIFERO")
                {
                    return this.ETA_TIPO_ACUIFERO.ToString();
                }
                else if (index == "ETA_ACTIVO")
                {
                    return this.ETA_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETA_ID")
                {
                    try
                    {
                        this.ETA_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETA_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETA_TIPO_ACUIFERO")
                {
                    this.ETA_TIPO_ACUIFERO = value;
                }
                else if (index == "ETA_ACTIVO")
                {
                    try
                    {
                        this.ETA_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETA_ACTIVO = false;
                    }
                }
            }
        }
    }
}
