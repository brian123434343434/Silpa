using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoBiotaEntity
    {
        private System.Int32 _ETB_ID;
        private System.String _ETB_TIPO_BIOTA;
        private System.Boolean _ETB_ACTIVO;
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
        public System.Int32 ETB_ID
        {
            get
            {
                return _ETB_ID;
            }
            set
            {
                _ETB_ID = value;
            }
        }
        public System.String ETB_TIPO_BIOTA
        {
            get
            {
                return ajustarAncho(_ETB_TIPO_BIOTA, 100);
            }
            set
            {
                _ETB_TIPO_BIOTA = value;
            }
        }
        public System.Boolean ETB_ACTIVO
        {
            get
            {
                return _ETB_ACTIVO;
            }
            set
            {
                _ETB_ACTIVO = value;
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
                    return this.ETB_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETB_TIPO_BIOTA.ToString();
                }
                else if (index == 2)
                {
                    return this.ETB_ACTIVO.ToString();
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
                        this.ETB_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETB_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETB_TIPO_BIOTA = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETB_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETB_ACTIVO = false;
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
                if (index == "ETB_ID")
                {
                    return this.ETB_ID.ToString();
                }
                else if (index == "ETB_TIPO_BIOTA")
                {
                    return this.ETB_TIPO_BIOTA.ToString();
                }
                else if (index == "ETB_ACTIVO")
                {
                    return this.ETB_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETB_ID")
                {
                    try
                    {
                        this.ETB_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETB_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETB_TIPO_BIOTA")
                {
                    this.ETB_TIPO_BIOTA = value;
                }
                else if (index == "ETB_ACTIVO")
                {
                    try
                    {
                        this.ETB_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETB_ACTIVO = false;
                    }
                }
            }
        }
    }
}
