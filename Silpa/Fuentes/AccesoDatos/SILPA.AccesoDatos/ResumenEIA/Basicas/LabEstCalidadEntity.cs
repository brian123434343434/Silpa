using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class LabEstCalidadEntity
    {
        private System.Int32 _ELC_ID;
        private System.Int32 _ETL_ID;
        private System.String _ELC_LABORATORIO_EST_CALIDAD;
        private System.Boolean _ELC_ACTIVO;
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
        public System.Int32 ELC_ID
        {
            get
            {
                return _ELC_ID;
            }
            set
            {
                _ELC_ID = value;
            }
        }
        public System.Int32 ETL_ID
        {
            get
            {
                return _ETL_ID;
            }
            set
            {
                _ETL_ID = value;
            }
        }
        public System.String ELC_LABORATORIO_EST_CALIDAD
        {
            get
            {
                return ajustarAncho(_ELC_LABORATORIO_EST_CALIDAD, 200);
            }
            set
            {
                _ELC_LABORATORIO_EST_CALIDAD = value;
            }
        }
        public System.Boolean ELC_ACTIVO
        {
            get
            {
                return _ELC_ACTIVO;
            }
            set
            {
                _ELC_ACTIVO = value;
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
                    return this.ELC_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETL_ID.ToString();
                }
                else if (index == 2)
                {
                    return this.ELC_LABORATORIO_EST_CALIDAD.ToString();
                }
                else if (index == 3)
                {
                    return this.ELC_ACTIVO.ToString();
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
                        this.ELC_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ELC_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    try
                    {
                        this.ETL_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETL_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 2)
                {
                    this.ELC_LABORATORIO_EST_CALIDAD = value;
                }
                else if (index == 3)
                {
                    try
                    {
                        this.ELC_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ELC_ACTIVO = false;
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
                if (index == "ELC_ID")
                {
                    return this.ELC_ID.ToString();
                }
                else if (index == "ETL_ID")
                {
                    return this.ETL_ID.ToString();
                }
                else if (index == "ELC_LABORATORIO_EST_CALIDAD")
                {
                    return this.ELC_LABORATORIO_EST_CALIDAD.ToString();
                }
                else if (index == "ELC_ACTIVO")
                {
                    return this.ELC_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ELC_ID")
                {
                    try
                    {
                        this.ELC_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ELC_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETL_ID")
                {
                    try
                    {
                        this.ETL_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETL_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ELC_LABORATORIO_EST_CALIDAD")
                {
                    this.ELC_LABORATORIO_EST_CALIDAD = value;
                }
                else if (index == "ELC_ACTIVO")
                {
                    try
                    {
                        this.ELC_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ELC_ACTIVO = false;
                    }
                }
            }
        }
    }
}
