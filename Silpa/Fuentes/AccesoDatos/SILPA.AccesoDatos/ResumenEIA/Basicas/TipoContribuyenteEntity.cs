using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoContribuyenteEntity
    {
        // Las variables privadas
        // TODO: Revisar los tipos de los campos
        private System.Int32 _ETC_ID;
        private System.String _ETC_TIPO_CONTRIBUYENTE;
        private System.Boolean _ETC_ACTIVO;
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
        public System.Int32 ETC_ID
        {
            get
            {
                return _ETC_ID;
            }
            set
            {
                _ETC_ID = value;
            }
        }
        public System.String ETC_TIPO_CONTRIBUYENTE
        {
            get
            {
                return ajustarAncho(_ETC_TIPO_CONTRIBUYENTE, 100);
            }
            set
            {
                _ETC_TIPO_CONTRIBUYENTE = value;
            }
        }
        public System.Boolean ETC_ACTIVO
        {
            get
            {
                return _ETC_ACTIVO;
            }
            set
            {
                _ETC_ACTIVO = value;
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
                    return this.ETC_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ETC_TIPO_CONTRIBUYENTE.ToString();
                }
                else if (index == 2)
                {
                    return this.ETC_ACTIVO.ToString();
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
                        this.ETC_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETC_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ETC_TIPO_CONTRIBUYENTE = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ETC_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETC_ACTIVO = false;
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
                if (index == "ETC_ID")
                {
                    return this.ETC_ID.ToString();
                }
                else if (index == "ETC_TIPO_CONTRIBUYENTE")
                {
                    return this.ETC_TIPO_CONTRIBUYENTE.ToString();
                }
                else if (index == "ETC_ACTIVO")
                {
                    return this.ETC_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ETC_ID")
                {
                    try
                    {
                        this.ETC_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ETC_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ETC_TIPO_CONTRIBUYENTE")
                {
                    this.ETC_TIPO_CONTRIBUYENTE = value;
                }
                else if (index == "ETC_ACTIVO")
                {
                    try
                    {
                        this.ETC_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ETC_ACTIVO = false;
                    }
                }
            }
        }
    }
}
