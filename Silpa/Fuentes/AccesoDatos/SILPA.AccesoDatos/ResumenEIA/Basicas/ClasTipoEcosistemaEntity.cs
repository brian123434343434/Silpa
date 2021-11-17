using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class ClasTipoEcosistemaEntity
    {
        private System.Int32 _ECE_ID;
        private System.String _ECE_CLASIFICACION_TERRESTRE;
        private System.Boolean _ECE_ACTIVO;
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
        public System.Int32 ECE_ID
        {
            get
            {
                return _ECE_ID;
            }
            set
            {
                _ECE_ID = value;
            }
        }
        public System.String ECE_CLASIFICACION_TERRESTRE
        {
            get
            {
                return ajustarAncho(_ECE_CLASIFICACION_TERRESTRE, 100);
            }
            set
            {
                _ECE_CLASIFICACION_TERRESTRE = value;
            }
        }
        public System.Boolean ECE_ACTIVO
        {
            get
            {
                return _ECE_ACTIVO;
            }
            set
            {
                _ECE_ACTIVO = value;
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
                    return this.ECE_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ECE_CLASIFICACION_TERRESTRE.ToString();
                }
                else if (index == 2)
                {
                    return this.ECE_ACTIVO.ToString();
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
                        this.ECE_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ECE_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ECE_CLASIFICACION_TERRESTRE = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ECE_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ECE_ACTIVO = false;
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
                if (index == "ECE_ID")
                {
                    return this.ECE_ID.ToString();
                }
                else if (index == "ECE_CLASIFICACION_TERRESTRE")
                {
                    return this.ECE_CLASIFICACION_TERRESTRE.ToString();
                }
                else if (index == "ECE_ACTIVO")
                {
                    return this.ECE_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ECE_ID")
                {
                    try
                    {
                        this.ECE_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ECE_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ECE_CLASIFICACION_TERRESTRE")
                {
                    this.ECE_CLASIFICACION_TERRESTRE = value;
                }
                else if (index == "ECE_ACTIVO")
                {
                    try
                    {
                        this.ECE_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ECE_ACTIVO = false;
                    }
                }
            }
        }
    }
}
