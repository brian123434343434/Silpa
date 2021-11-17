using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class MunicipioEntity
    {
        private System.Int32 _MUN_ID;
        private System.String _MUN_NOMBRE;
        private System.Int32 _DEP_ID;
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
        public System.Int32 MUN_ID
        {
            get
            {
                return _MUN_ID;
            }
            set
            {
                _MUN_ID = value;
            }
        }
        public System.String MUN_NOMBRE
        {
            get
            {
                return ajustarAncho(_MUN_NOMBRE, 75);
            }
            set
            {
                _MUN_NOMBRE = value;
            }
        }
        
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
        
        //
        public string this[int index]
        {
            // Devuelve el contenido del campo indicado en index
            // (el índice corresponde con la columna de la tabla)
            get
            {
                if (index == 0)
                {
                    return this.MUN_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.MUN_NOMBRE.ToString();
                }
                else if (index == 2)
                {
                    return this.DEP_ID.ToString();
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
                        this.MUN_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.MUN_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.MUN_NOMBRE = value;
                }
                else if (index == 2)
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
            }
        }
        public string this[string index]
        {
            // Devuelve el contenido del campo indicado en index
            // (el índice corresponde al nombre de la columna)
            get
            {
                if (index == "MUN_ID")
                {
                    return this.MUN_ID.ToString();
                }
                else if (index == "MUN_NOMBRE")
                {
                    return this.MUN_NOMBRE.ToString();
                }
                else if (index == "DEP_ID")
                {
                    return this.DEP_ID.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "MUN_ID")
                {
                    try
                    {
                        this.MUN_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.MUN_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "MUN_NOMBRE")
                {
                    this.MUN_NOMBRE = value;
                }
                else if (index == "DEP_ID")
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
            }
        }
    }
}
