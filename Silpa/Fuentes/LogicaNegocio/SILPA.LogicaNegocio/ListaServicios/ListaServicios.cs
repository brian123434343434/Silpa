using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos; 
using System.Collections; 

namespace SILPA.LogicaNegocio.ListaServicios
{
    public class ListaServicios
    {
        public ListaServicios()
        { 
        }

        public List<SILPA.AccesoDatos.ListaServicios.Metodo> Metodo(int idMetodo)
        {
            string[] param = { idMetodo.ToString() };
            return PrivadoMetodoServicio(param);
        }

        public List<SILPA.AccesoDatos.ListaServicios.Metodo> MetodoServicio(int idMetodo, int idServicio)
        {
            string[] param = {idMetodo.ToString() , idServicio.ToString()};
            return PrivadoMetodoServicio(param);
        }

        public List<SILPA.AccesoDatos.ListaServicios.Metodo> MetodoServicio(int idServicio)
        {
            string[] param = { "-1", idServicio.ToString() };
            return PrivadoMetodoServicio(param);
        }

        private List<SILPA.AccesoDatos.ListaServicios.Metodo> PrivadoMetodoServicio(string[] param)
        {
            SILPA.AccesoDatos.ListaServicios.MetodoDalc met = new SILPA.AccesoDatos.ListaServicios.MetodoDalc();
            try
            {
                return met.ListarMetodo(param);
            }
            finally
            {
                met = null;
            }
        }

        public List<SILPA.AccesoDatos.ListaServicios.Servicio> Servicios()
        {
            SILPA.AccesoDatos.ListaServicios.ServicioDalc  ser = new SILPA.AccesoDatos.ListaServicios.ServicioDalc();
            try
            {
                return ser.Listar();
            }
            finally
            {
                ser = null;
            }
        }

        public List<SILPA.AccesoDatos.ListaServicios.Metodo> Metodos()
        {
            SILPA.AccesoDatos.ListaServicios.MetodoDalc met = new SILPA.AccesoDatos.ListaServicios.MetodoDalc();
            try
            {
                return met.Listar();
            }
            finally
            {
                met = null;
            }
        }
    }
}
