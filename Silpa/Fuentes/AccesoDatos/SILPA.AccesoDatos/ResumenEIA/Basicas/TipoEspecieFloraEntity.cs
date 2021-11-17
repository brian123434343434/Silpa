using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoEspecieFloraEntity
    {
        private System.Int32 _EEF_ID;
        private System.String _EEF_TIPO_ESPECIE;
        private System.Boolean _EEF_ACTIVO;
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
        public System.Int32 EEF_ID
        {
            get
            {
                return _EEF_ID;
            }
            set
            {
                _EEF_ID = value;
            }
        }
        public System.String EEF_TIPO_ESPECIE
        {
            get
            {
                return ajustarAncho(_EEF_TIPO_ESPECIE, 100);
            }
            set
            {
                _EEF_TIPO_ESPECIE = value;
            }
        }
        public System.Boolean EEF_ACTIVO
        {
            get
            {
                return _EEF_ACTIVO;
            }
            set
            {
                _EEF_ACTIVO = value;
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
                    return this.EEF_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.EEF_TIPO_ESPECIE.ToString();
                }
                else if (index == 2)
                {
                    return this.EEF_ACTIVO.ToString();
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
                        this.EEF_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EEF_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.EEF_TIPO_ESPECIE = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.EEF_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EEF_ACTIVO = false;
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
                if (index == "EEF_ID")
                {
                    return this.EEF_ID.ToString();
                }
                else if (index == "EEF_TIPO_ESPECIE")
                {
                    return this.EEF_TIPO_ESPECIE.ToString();
                }
                else if (index == "EEF_ACTIVO")
                {
                    return this.EEF_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "EEF_ID")
                {
                    try
                    {
                        this.EEF_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EEF_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "EEF_TIPO_ESPECIE")
                {
                    this.EEF_TIPO_ESPECIE = value;
                }
                else if (index == "EEF_ACTIVO")
                {
                    try
                    {
                        this.EEF_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EEF_ACTIVO = false;
                    }
                }
            }
        }
    }
}
