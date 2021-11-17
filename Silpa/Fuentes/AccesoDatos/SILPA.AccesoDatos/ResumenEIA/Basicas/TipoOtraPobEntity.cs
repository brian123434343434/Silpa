using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoOtraPobEntity
    {
        private System.Int32 _ETO_ID;
        private System.String _ETO_TIPO_OTRA_POBLACION;
        private System.Boolean _ETO_ACTIVO;
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
        public System.Int32 ETO_ID
        {
            get
            {
                return _ETO_ID;
            }
            set
            {
                _ETO_ID = value;
            }
        }
        public System.String ETO_TIPO_OTRA_POBLACION
        {
            get
            {
                return ajustarAncho(_ETO_TIPO_OTRA_POBLACION, 100);
            }
            set
            {
                _ETO_TIPO_OTRA_POBLACION = value;
            }
        }
        public System.Boolean ETO_ACTIVO
        {
            get
            {
                return _ETO_ACTIVO;
            }
            set
            {
                _ETO_ACTIVO = value;
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
                    return this.ETO_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETO_TIPO_OTRA_POBLACION.ToString();
                }
                else if (index == 2)
                {
                    return this.ETO_ACTIVO.ToString();
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
                        this.ETO_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETO_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETO_TIPO_OTRA_POBLACION = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETO_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETO_ACTIVO = false;
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
                if (index == "ETO_ID")
                {
                    return this.ETO_ID.ToString();
                }
                else if (index == "ETO_TIPO_OTRA_POBLACION")
                {
                    return this.ETO_TIPO_OTRA_POBLACION.ToString();
                }
                else if (index == "ETO_ACTIVO")
                {
                    return this.ETO_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETO_ID")
                {
                    try
                    {
                        this.ETO_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETO_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETO_TIPO_OTRA_POBLACION")
                {
                    this.ETO_TIPO_OTRA_POBLACION = value;
                }
                else if (index == "ETO_ACTIVO")
                {
                    try
                    {
                        this.ETO_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETO_ACTIVO = false;
                    }
                }
            }
        }
    }
}
