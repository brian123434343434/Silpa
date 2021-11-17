using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class MetDetEstCalidadEntity
    {
        private System.Int32 _EMC_ID;
        private System.String _EMC_METODOS_DET_CALIDAD;
        private System.Boolean _EMC_ACTIVO;
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
        public System.Int32 EMC_ID
        {
            get
            {
                return _EMC_ID;
            }
            set
            {
                _EMC_ID = value;
            }
        }
        public System.String EMC_METODOS_DET_CALIDAD
        {
            get
            {
                return ajustarAncho(_EMC_METODOS_DET_CALIDAD, 100);
            }
            set
            {
                _EMC_METODOS_DET_CALIDAD = value;
            }
        }
        public System.Boolean EMC_ACTIVO
        {
            get
            {
                return _EMC_ACTIVO;
            }
            set
            {
                _EMC_ACTIVO = value;
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
                    return this.EMC_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.EMC_METODOS_DET_CALIDAD.ToString();
                }
                else if (index == 2)
                {
                    return this.EMC_ACTIVO.ToString();
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
                        this.EMC_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EMC_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.EMC_METODOS_DET_CALIDAD = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.EMC_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EMC_ACTIVO = false;
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
                if (index == "EMC_ID")
                {
                    return this.EMC_ID.ToString();
                }
                else if (index == "EMC_METODOS_DET_CALIDAD")
                {
                    return this.EMC_METODOS_DET_CALIDAD.ToString();
                }
                else if (index == "EMC_ACTIVO")
                {
                    return this.EMC_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "EMC_ID")
                {
                    try
                    {
                        this.EMC_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EMC_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "EMC_METODOS_DET_CALIDAD")
                {
                    this.EMC_METODOS_DET_CALIDAD = value;
                }
                else if (index == "EMC_ACTIVO")
                {
                    try
                    {
                        this.EMC_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EMC_ACTIVO = false;
                    }
                }
            }
        }
    }
}
