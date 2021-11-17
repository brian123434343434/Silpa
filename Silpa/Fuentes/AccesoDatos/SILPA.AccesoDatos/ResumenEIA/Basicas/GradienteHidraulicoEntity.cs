using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class GradienteHidraulicoEntity
    {
        private System.Int32 _EGH_ID;
        private System.String _EGH_TIPO_GRADIENTE_HIDRA;
        private System.Boolean _EGH_ACTIVO;
        //
        // Este método se usará para ajustar los anchos de las propiedades
        private string ajustarAncho(string cadena, int ancho){
            System.Text.StringBuilder sb = new System.Text.StringBuilder(new String(' ', ancho));
            // devolver la cadena quitando los espacios en blanco
            // esto asegura que no se devolverá un tamaño mayor ni espacios "extras"
            return (cadena + sb.ToString()).Substring(0, ancho).Trim();
        }
        //
        // Las propiedades públicas
        // TODO: Revisar los tipos de las propiedades
        public System.Int32 EGH_ID{
            get{
                return  _EGH_ID;
            }
            set{
                _EGH_ID = value;
            }
        }
        public System.String EGH_TIPO_GRADIENTE_HIDRA{
            get{
                return ajustarAncho(_EGH_TIPO_GRADIENTE_HIDRA,100);
            }
            set{
                _EGH_TIPO_GRADIENTE_HIDRA = value;
            }
        }
        public System.Boolean EGH_ACTIVO{
            get{
                return  _EGH_ACTIVO;
            }
            set{
                _EGH_ACTIVO = value;
            }
        }
        //
        public string this[int index]{
            // Devuelve el contenido del campo indicado en index
            // (el índice corresponde con la columna de la tabla)
            get{
                if(index == 0){
                    return this.EGH_ID.ToString();
                }else if(index == 1){
                    return this.EGH_TIPO_GRADIENTE_HIDRA.ToString();
                }else if(index == 2){
                    return this.EGH_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set{
                if(index == 0){
                    try{
                        this.EGH_ID = System.Int32.Parse("0" + value);
                    }catch{
                        this.EGH_ID = System.Int32.Parse("0");
                    }
                }else if(index == 1){
                    this.EGH_TIPO_GRADIENTE_HIDRA = value;
                }else if(index == 2){
                    try{
                        this.EGH_ACTIVO = System.Boolean.Parse(value);
                    }catch{
                        this.EGH_ACTIVO = false;
                    }
                }
            }
        }
        public string this[string index]{
            // Devuelve el contenido del campo indicado en index
            // (el índice corresponde al nombre de la columna)
            get{
                if(index == "EGH_ID"){
                    return this.EGH_ID.ToString();
                }else if(index == "EGH_TIPO_GRADIENTE_HIDRA"){
                    return this.EGH_TIPO_GRADIENTE_HIDRA.ToString();
                }else if(index == "EGH_ACTIVO"){
                    return this.EGH_ACTIVO.ToString();
                }
                // Para que no de error el compilador de C#
                return "";
            }
            set{
                if(index == "EGH_ID"){
                    try{
                        this.EGH_ID = System.Int32.Parse("0" + value);
                    }catch{
                        this.EGH_ID = System.Int32.Parse("0");
                    }
                }else if(index == "EGH_TIPO_GRADIENTE_HIDRA"){
                    this.EGH_TIPO_GRADIENTE_HIDRA = value;
                }else if(index == "EGH_ACTIVO"){
                    try{
                        this.EGH_ACTIVO = System.Boolean.Parse(value);
                    }catch{
                        this.EGH_ACTIVO = false;
                    }
                }
            }
        }
    }
}
