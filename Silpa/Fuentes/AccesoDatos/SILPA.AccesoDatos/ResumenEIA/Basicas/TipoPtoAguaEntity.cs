using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoPtoAguaEntity
    {
        private System.Int32 _ETP_ID;
        private System.String _ETP_TIPO_PTO_AGUA;
        private System.Boolean _ETP_ACTIVO;
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
        public System.Int32 ETP_ID
        {
            get
            {
                return _ETP_ID;
            }
            set
            {
                _ETP_ID = value;
            }
        }
        public System.String ETP_TIPO_PTO_AGUA
        {
            get
            {
                return ajustarAncho(_ETP_TIPO_PTO_AGUA, 100);
            }
            set
            {
                _ETP_TIPO_PTO_AGUA = value;
            }
        }
        public System.Boolean ETP_ACTIVO
        {
            get
            {
                return _ETP_ACTIVO;
            }
            set
            {
                _ETP_ACTIVO = value;
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
                    return this.ETP_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETP_TIPO_PTO_AGUA.ToString();
                }
                else if (index == 2)
                {
                    return this.ETP_ACTIVO.ToString();
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
                        this.ETP_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETP_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETP_TIPO_PTO_AGUA = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETP_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETP_ACTIVO = false;
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
                if (index == "ETP_ID")
                {
                    return this.ETP_ID.ToString();
                }
                else if (index == "ETP_TIPO_PTO_AGUA")
                {
                    return this.ETP_TIPO_PTO_AGUA.ToString();
                }
                else if (index == "ETP_ACTIVO")
                {
                    return this.ETP_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETP_ID")
                {
                    try
                    {
                        this.ETP_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETP_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETP_TIPO_PTO_AGUA")
                {
                    this.ETP_TIPO_PTO_AGUA = value;
                }
                else if (index == "ETP_ACTIVO")
                {
                    try
                    {
                        this.ETP_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETP_ACTIVO = false;
                    }
                }
            }
        }
    }
}
