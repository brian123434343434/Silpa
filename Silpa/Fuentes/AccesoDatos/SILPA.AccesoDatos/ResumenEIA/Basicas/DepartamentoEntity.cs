using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class DepartamentoEntity
    {
        private System.Int32 _DEP_ID;
        private System.String _DEP_NOMBRE;
        private System.Boolean _DEP_ACTIVO;
        private System.Int32 _REG_ID;
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
        public System.Int32 DEP_ID
        {
            get
            {
                return _DEP_ID;
            }
            set
            {
                _DEP_ID = value;
            }
        }
        public System.String DEP_NOMBRE
        {
            get
            {
                return ajustarAncho(_DEP_NOMBRE, 75);
            }
            set
            {
                _DEP_NOMBRE = value;
            }
        }
        public System.Boolean DEP_ACTIVO
        {
            get
            {
                return _DEP_ACTIVO;
            }
            set
            {
                _DEP_ACTIVO = value;
            }
        }
        public System.Int32 REG_ID
        {
            get
            {
                return _REG_ID;
            }
            set
            {
                _REG_ID = value;
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
                    return this.DEP_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.DEP_NOMBRE.ToString();
                }
                else if (index == 2)
                {
                    return this.DEP_ACTIVO.ToString();
                }
                else if (index == 3)
                {
                    return this.REG_ID.ToString();
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
                        this.DEP_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.DEP_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.DEP_NOMBRE = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.DEP_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.DEP_ACTIVO = false;
                    }
                }
                else if (index == 3)
                {
                    try
                    {
                        this.REG_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.REG_ID = System.Int32.Parse("0");
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
                if (index == "DEP_ID")
                {
                    return this.DEP_ID.ToString();
                }
                else if (index == "DEP_NOMBRE")
                {
                    return this.DEP_NOMBRE.ToString();
                }
                else if (index == "DEP_ACTIVO")
                {
                    return this.DEP_ACTIVO.ToString();
                }
                else if (index == "REG_ID")
                {
                    return this.REG_ID.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "DEP_ID")
                {
                    try
                    {
                        this.DEP_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.DEP_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "DEP_NOMBRE")
                {
                    this.DEP_NOMBRE = value;
                }
                else if (index == "DEP_ACTIVO")
                {
                    try
                    {
                        this.DEP_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.DEP_ACTIVO = false;
                    }
                }
                else if (index == "REG_ID")
                {
                    try
                    {
                        this.REG_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.REG_ID = System.Int32.Parse("0");
                    }
                }
            }
        }
    }
}
