using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class CaractEstCalidadEntity
    {
        private System.Int32 _EPC_ID;
        private System.Int32 _ETP_ID;
        private System.String _EPC_PARAMETROS_EST_CALIDAD;
        private System.Boolean _EPC_ACTIVO;
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
        public System.Int32 EPC_ID
        {
            get
            {
                return _EPC_ID;
            }
            set
            {
                _EPC_ID = value;
            }
        }
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
        public System.String EPC_PARAMETROS_EST_CALIDAD
        {
            get
            {
                return ajustarAncho(_EPC_PARAMETROS_EST_CALIDAD, 100);
            }
            set
            {
                _EPC_PARAMETROS_EST_CALIDAD = value;
            }
        }
        public System.Boolean EPC_ACTIVO
        {
            get
            {
                return _EPC_ACTIVO;
            }
            set
            {
                _EPC_ACTIVO = value;
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
                    return this.EPC_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETP_ID.ToString();
                }
                else if (index == 2)
                {
                    return this.EPC_PARAMETROS_EST_CALIDAD.ToString();
                }
                else if (index == 3)
                {
                    return this.EPC_ACTIVO.ToString();
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
                        this.EPC_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EPC_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
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
                else if (index == 2)
                {
                    this.EPC_PARAMETROS_EST_CALIDAD = value;
                }
                else if (index == 3)
                {
                    try
                    {
                        this.EPC_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EPC_ACTIVO = false;
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
                if (index == "EPC_ID")
                {
                    return this.EPC_ID.ToString();
                }
                else if (index == "ETP_ID")
                {
                    return this.ETP_ID.ToString();
                }
                else if (index == "EPC_PARAMETROS_EST_CALIDAD")
                {
                    return this.EPC_PARAMETROS_EST_CALIDAD.ToString();
                }
                else if (index == "EPC_ACTIVO")
                {
                    return this.EPC_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "EPC_ID")
                {
                    try
                    {
                        this.EPC_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EPC_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETP_ID")
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
                else if (index == "EPC_PARAMETROS_EST_CALIDAD")
                {
                    this.EPC_PARAMETROS_EST_CALIDAD = value;
                }
                else if (index == "EPC_ACTIVO")
                {
                    try
                    {
                        this.EPC_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EPC_ACTIVO = false;
                    }
                }
            }
        }
    }
}
