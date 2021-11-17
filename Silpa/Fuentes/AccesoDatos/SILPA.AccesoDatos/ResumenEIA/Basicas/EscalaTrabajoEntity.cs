using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class EscalaTrabajoEntity
    {
        private System.Int32 _EET_ID;
        private System.String _EET_ESCALA_TRABAJO;
        private System.Boolean _EET_ACTIVO;
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
        public System.Int32 EET_ID
        {
            get
            {
                return _EET_ID;
            }
            set
            {
                _EET_ID = value;
            }
        }
        public System.String EET_ESCALA_TRABAJO
        {
            get
            {
                return ajustarAncho(_EET_ESCALA_TRABAJO, 100);
            }
            set
            {
                _EET_ESCALA_TRABAJO = value;
            }
        }
        public System.Boolean EET_ACTIVO
        {
            get
            {
                return _EET_ACTIVO;
            }
            set
            {
                _EET_ACTIVO = value;
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
                    return this.EET_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.EET_ESCALA_TRABAJO.ToString();
                }
                else if (index == 2)
                {
                    return this.EET_ACTIVO.ToString();
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
                        this.EET_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EET_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.EET_ESCALA_TRABAJO = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.EET_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EET_ACTIVO = false;
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
                if (index == "EET_ID")
                {
                    return this.EET_ID.ToString();
                }
                else if (index == "EET_ESCALA_TRABAJO")
                {
                    return this.EET_ESCALA_TRABAJO.ToString();
                }
                else if (index == "EET_ACTIVO")
                {
                    return this.EET_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "EET_ID")
                {
                    try
                    {
                        this.EET_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EET_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "EET_ESCALA_TRABAJO")
                {
                    this.EET_ESCALA_TRABAJO = value;
                }
                else if (index == "EET_ACTIVO")
                {
                    try
                    {
                        this.EET_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EET_ACTIVO = false;
                    }
                }
            }
        }
    }
}
