using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class FuenteInfoEcoterrEntity
    {
        private System.Int32 _EFI_ID;
        private System.String _EFI_FUENT_INFO_ECOTERR;
        private System.Boolean _EFI_ACTIVO;
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
        public System.Int32 EFI_ID
        {
            get
            {
                return _EFI_ID;
            }
            set
            {
                _EFI_ID = value;
            }
        }
        public System.String EFI_FUENT_INFO_ECOTERR
        {
            get
            {
                return ajustarAncho(_EFI_FUENT_INFO_ECOTERR, 100);
            }
            set
            {
                _EFI_FUENT_INFO_ECOTERR = value;
            }
        }
        public System.Boolean EFI_ACTIVO
        {
            get
            {
                return _EFI_ACTIVO;
            }
            set
            {
                _EFI_ACTIVO = value;
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
                    return this.EFI_ID.ToString();
                }
                else if (index == 1)
                {
                    return this.EFI_FUENT_INFO_ECOTERR.ToString();
                }
                else if (index == 2)
                {
                    return this.EFI_ACTIVO.ToString();
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
                        this.EFI_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EFI_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == 1)
                {
                    this.EFI_FUENT_INFO_ECOTERR = value;
                }
                else if (index == 2)
                {
                    try
                    {
                        this.EFI_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EFI_ACTIVO = false;
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
                if (index == "EFI_ID")
                {
                    return this.EFI_ID.ToString();
                }
                else if (index == "EFI_FUENT_INFO_ECOTERR")
                {
                    return this.EFI_FUENT_INFO_ECOTERR.ToString();
                }
                else if (index == "EFI_ACTIVO")
                {
                    return this.EFI_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set
            {
                if (index == "EFI_ID")
                {
                    try
                    {
                        this.EFI_ID = System.Int32.Parse("0" + value);
                    }
                    catch
                    {
                        this.EFI_ID = System.Int32.Parse("0");
                    }
                }
                else if (index == "EFI_FUENT_INFO_ECOTERR")
                {
                    this.EFI_FUENT_INFO_ECOTERR = value;
                }
                else if (index == "EFI_ACTIVO")
                {
                    try
                    {
                        this.EFI_ACTIVO = System.Boolean.Parse(value);
                    }
                    catch
                    {
                        this.EFI_ACTIVO = false;
                    }
                }
            }
        }
    }
}
