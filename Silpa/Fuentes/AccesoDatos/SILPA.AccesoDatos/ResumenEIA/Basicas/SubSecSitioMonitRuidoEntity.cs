using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class SubSecSitioMonitRuidoEntity
    {
        private System.Int32 _ESS_ID;
        private System.String _ESS_SUBSECTOR_SITIO_MONIT;
        private System.Boolean _ESS_ACTIVO;
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
        public System.Int32 ESS_ID
        {
            get
            {
                return _ESS_ID;
            }
            set
            {
                _ESS_ID = value;
            }
        }
        public System.String ESS_SUBSECTOR_SITIO_MONIT
        {
            get
            {
                return ajustarAncho(_ESS_SUBSECTOR_SITIO_MONIT, 100);
            }
            set
            {
                _ESS_SUBSECTOR_SITIO_MONIT = value;
            }
        }
        public System.Boolean ESS_ACTIVO
        {
            get
            {
                return _ESS_ACTIVO;
            }
            set
            {
                _ESS_ACTIVO = value;
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
                    return this.ESS_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ESS_SUBSECTOR_SITIO_MONIT.ToString();
                }
                else if (index == 2)
                {
                    return this.ESS_ACTIVO.ToString();
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
                        this.ESS_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ESS_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ESS_SUBSECTOR_SITIO_MONIT = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ESS_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ESS_ACTIVO = false;
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
                if (index == "ESS_ID")
                {
                    return this.ESS_ID.ToString();
                }
                else if (index == "ESS_SUBSECTOR_SITIO_MONIT")
                {
                    return this.ESS_SUBSECTOR_SITIO_MONIT.ToString();
                }
                else if (index == "ESS_ACTIVO")
                {
                    return this.ESS_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ESS_ID")
                {
                    try
                    {
                        this.ESS_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ESS_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ESS_SUBSECTOR_SITIO_MONIT")
                {
                    this.ESS_SUBSECTOR_SITIO_MONIT = value;
                }
                else if (index == "ESS_ACTIVO")
                {
                    try
                    {
                        this.ESS_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ESS_ACTIVO = false;
                    }
                }
            }
        }
    }
}
