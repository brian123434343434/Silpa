using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class PosFitoDominanteEntity
    {
        private System.Int32 _EFS_ID;
        private System.String _EFS_TIPO_POS_FITOSOC_DOM;
        private System.Boolean _EFS_ACTIVO;
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
        public System.Int32 EFS_ID
        {
            get
            {
                return _EFS_ID;
            }
            set
            {
                _EFS_ID = value;
            }
        }
        public System.String EFS_TIPO_POS_FITOSOC_DOM
        {
            get
            {
                return ajustarAncho(_EFS_TIPO_POS_FITOSOC_DOM, 100);
            }
            set
            {
                _EFS_TIPO_POS_FITOSOC_DOM = value;
            }
        }
        public System.Boolean EFS_ACTIVO
        {
            get
            {
                return _EFS_ACTIVO;
            }
            set
            {
                _EFS_ACTIVO = value;
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
                    return this.EFS_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.EFS_TIPO_POS_FITOSOC_DOM.ToString();
                }
                else if (index == 2)
                {
                    return this.EFS_ACTIVO.ToString();
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
                        this.EFS_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EFS_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.EFS_TIPO_POS_FITOSOC_DOM = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.EFS_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EFS_ACTIVO = false;
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
                if (index == "EFS_ID")
                {
                    return this.EFS_ID.ToString();
                }
                else if (index == "EFS_TIPO_POS_FITOSOC_DOM")
                {
                    return this.EFS_TIPO_POS_FITOSOC_DOM.ToString();
                }
                else if (index == "EFS_ACTIVO")
                {
                    return this.EFS_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "EFS_ID")
                {
                    try
                    {
                        this.EFS_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EFS_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "EFS_TIPO_POS_FITOSOC_DOM")
                {
                    this.EFS_TIPO_POS_FITOSOC_DOM = value;
                }
                else if (index == "EFS_ACTIVO")
                {
                    try
                    {
                        this.EFS_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EFS_ACTIVO = false;
                    }
                }
            }
        }
    }
}
