using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoEcosistemaEntity
    {
        private System.Int32 _ETE_ID;
        private System.Int32 _ECE_ID;
        private System.String _ETE_TIPO_ECOSISTEMA_TERRESTRE;
        private System.Boolean _ETE_ACTIVO;
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
        public System.Int32 ETE_ID
        {
            get
            {
                return _ETE_ID;
            }
            set
            {
                _ETE_ID = value;
            }
        }
        public System.Int32 ECE_ID
        {
            get
            {
                return _ECE_ID;
            }
            set
            {
                _ECE_ID = value;
            }
        }
        public System.String ETE_TIPO_ECOSISTEMA_TERRESTRE
        {
            get
            {
                return ajustarAncho(_ETE_TIPO_ECOSISTEMA_TERRESTRE, 100);
            }
            set
            {
                _ETE_TIPO_ECOSISTEMA_TERRESTRE = value;
            }
        }
        public System.Boolean ETE_ACTIVO
        {
            get
            {
                return _ETE_ACTIVO;
            }
            set
            {
                _ETE_ACTIVO = value;
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
                    return this.ETE_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ECE_ID.ToString();
                }
                else if (index == 2)
                {
                    return this.ETE_TIPO_ECOSISTEMA_TERRESTRE.ToString();
                }
                else if (index == 3)
                {
                    return this.ETE_ACTIVO.ToString();
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
                        this.ETE_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETE_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    try
                    {
                        this.ECE_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ECE_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 2)
                {
                    this.ETE_TIPO_ECOSISTEMA_TERRESTRE = value;
                }
                else if (index == 3)
                {
                    try
                    {
                        this.ETE_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETE_ACTIVO = false;
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
                if (index == "ETE_ID")
                {
                    return this.ETE_ID.ToString();
                }
                else if (index == "ECE_ID")
                {
                    return this.ECE_ID.ToString();
                }
                else if (index == "ETE_TIPO_ECOSISTEMA_TERRESTRE")
                {
                    return this.ETE_TIPO_ECOSISTEMA_TERRESTRE.ToString();
                }
                else if (index == "ETE_ACTIVO")
                {
                    return this.ETE_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETE_ID")
                {
                    try
                    {
                        this.ETE_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETE_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ECE_ID")
                {
                    try
                    {
                        this.ECE_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ECE_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETE_TIPO_ECOSISTEMA_TERRESTRE")
                {
                    this.ETE_TIPO_ECOSISTEMA_TERRESTRE = value;
                }
                else if (index == "ETE_ACTIVO")
                {
                    try
                    {
                        this.ETE_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETE_ACTIVO = false;
                    }
                }
            }
        }
    }
}
