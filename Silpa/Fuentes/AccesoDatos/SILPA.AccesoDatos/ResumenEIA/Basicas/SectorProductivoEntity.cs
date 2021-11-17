using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class SectorProductivoEntity
    {
        private System.Int32 _ESP_ID;
        private System.String _ESP_SECTOR_PRODUCTIVO;
        private System.Boolean _ESP_ACTIVO;
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
        public System.Int32 ESP_ID
        {
            get
            {
                return _ESP_ID;
            }
            set
            {
                _ESP_ID = value;
            }
        }
        public System.String ESP_SECTOR_PRODUCTIVO
        {
            get
            {
                return ajustarAncho(_ESP_SECTOR_PRODUCTIVO, 100);
            }
            set
            {
                _ESP_SECTOR_PRODUCTIVO = value;
            }
        }
        public System.Boolean ESP_ACTIVO
        {
            get
            {
                return _ESP_ACTIVO;
            }
            set
            {
                _ESP_ACTIVO = value;
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
                    return this.ESP_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.ESP_SECTOR_PRODUCTIVO.ToString();
                }
                else if (index == 2)
                {
                    return this.ESP_ACTIVO.ToString();
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
                        this.ESP_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ESP_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.ESP_SECTOR_PRODUCTIVO = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.ESP_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ESP_ACTIVO = false;
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
                if (index == "ESP_ID")
                {
                    return this.ESP_ID.ToString();
                }
                else if (index == "ESP_SECTOR_PRODUCTIVO")
                {
                    return this.ESP_SECTOR_PRODUCTIVO.ToString();
                }
                else if (index == "ESP_ACTIVO")
                {
                    return this.ESP_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "ESP_ID")
                {
                    try
                    {
                        this.ESP_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.ESP_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "ESP_SECTOR_PRODUCTIVO")
                {
                    this.ESP_SECTOR_PRODUCTIVO = value;
                }
                else if (index == "ESP_ACTIVO")
                {
                    try
                    {
                        this.ESP_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.ESP_ACTIVO = false;
                    }
                }
            }
        }
    }
}
