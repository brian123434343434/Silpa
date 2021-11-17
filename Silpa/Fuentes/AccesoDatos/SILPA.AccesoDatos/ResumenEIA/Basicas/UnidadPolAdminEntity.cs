using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class UnidadPolAdminEntity
    {
        private System.Int32 _EPA_ID;
        private System.String _EPA_UNIDAD_POL_ADMIN;
        private System.Boolean _EPA_ACTIVO;
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
        public System.Int32 EPA_ID
        {
            get
            {
                return _EPA_ID;
            }
            set
            {
                _EPA_ID = value;
            }
        }
        public System.String EPA_UNIDAD_POL_ADMIN
        {
            get
            {
                return ajustarAncho(_EPA_UNIDAD_POL_ADMIN, 100);
            }
            set
            {
                _EPA_UNIDAD_POL_ADMIN = value;
            }
        }
        public System.Boolean EPA_ACTIVO
        {
            get
            {
                return _EPA_ACTIVO;
            }
            set
            {
                _EPA_ACTIVO = value;
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
                    return this.EPA_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.EPA_UNIDAD_POL_ADMIN.ToString();
                }
                else if (index == 2)
                {
                    return this.EPA_ACTIVO.ToString();
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
                        this.EPA_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EPA_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.EPA_UNIDAD_POL_ADMIN = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.EPA_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EPA_ACTIVO = false;
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
                if (index == "EPA_ID")
                {
                    return this.EPA_ID.ToString();
                }
                else if (index == "EPA_UNIDAD_POL_ADMIN")
                {
                    return this.EPA_UNIDAD_POL_ADMIN.ToString();
                }
                else if (index == "EPA_ACTIVO")
                {
                    return this.EPA_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "EPA_ID")
                {
                    try
                    {
                        this.EPA_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EPA_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "EPA_UNIDAD_POL_ADMIN")
                {
                    this.EPA_UNIDAD_POL_ADMIN = value;
                }
                else if (index == "EPA_ACTIVO")
                {
                    try
                    {
                        this.EPA_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EPA_ACTIVO = false;
                    }
                }
            }
        }
    }
}
