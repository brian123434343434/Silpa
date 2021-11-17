using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// Clase pais
    /// </summary>
    public class ParametroEntity
    {
        private int _idParametro;

        public int IdParametro
        {
            get{return _idParametro;}
            set{_idParametro = value;}

        }
        private int _idTipoDato;
        public int IdTipoDato
        {
            get{return _idTipoDato;}
            set{_idTipoDato = value;}
        }
        private string _pararmetro;
        public string Parametro
        {
            get{return _pararmetro;}
            set{_pararmetro = value;}
        }
        private string _nombreParametro;

        public string NombreParametro
        {
            get{return _nombreParametro;}
            set{_nombreParametro = value;}
        }

            
    }

}