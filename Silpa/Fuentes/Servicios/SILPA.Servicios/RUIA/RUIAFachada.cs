using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio;
using System.Data;
using SILPA.LogicaNegocio.Generico;
using SILPA.Comun;

using System.Threading;
namespace SILPA.Servicios.RUIA
{
    public class RUIAFachada
    {
        //SILPA.LogicaNegocio.RUIA.Sancion _objRuia;
        //private static Queue<string> _mensaje;
        //private string _descripcionObjeto;
        //private bool _parado;
        
                      
        public string consultaTotal()
        {
            ListasSancionType total = new ListasSancionType();

            SILPA.LogicaNegocio.RUIA.Listas listas = new SILPA.LogicaNegocio.RUIA.Listas();

            // creamos las listas
            DataSet ds = listas.ListaTipoFalta();

            TipoFaltaType[] listasFaltas = new TipoFaltaType[ds.Tables[0].Rows.Count];
            int posicion = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TipoFaltaType tipo = new TipoFaltaType();
                tipo.idTipoFalta = (int)dr["ID_TIPO_FALTA"];
                tipo.nombreTipoFalta = (string)dr["NOMBRE_TIPO_FALTA"];
                listasFaltas[posicion] = tipo;
                posicion++;
            }

            total.listaFaltas = listasFaltas;
            
            ds = listas.ListaOpcionSancion();
            OpcionTipoSancionType[] listaOpcion = new OpcionTipoSancionType[ds.Tables[0].Rows.Count];

            posicion = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                OpcionTipoSancionType opcion = new OpcionTipoSancionType();
                opcion.idOpcionSancion = (int)dr["ID_OPCION_SANCION"];
                opcion.nombreOpcionSancion = (string)dr["NOMBRE_OPCION_SANCION"];
                listaOpcion[posicion] = opcion;
                
                posicion++;
            }

            total.listaOpciones = listaOpcion;

            ds = listas.ListaTipoSancion();
            TipoSancionType[] listaTipoSancion = new TipoSancionType[ds.Tables[0].Rows.Count];

            posicion = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TipoSancionType tipo = new TipoSancionType();
                tipo.idTipoSancion = (int)dr["ID_TIPO_SANCION"];
                tipo.nombreTipoSancion = (string)dr["NOMBRE_TIPO_SANCION"];                
                listaTipoSancion[posicion] = tipo;

                posicion++;
            }
            total.listaTipoSancion = listaTipoSancion;
            
            XmlSerializador serializador = new XmlSerializador();

            string retorno = serializador.serializar(total);

            return retorno;
        }

        public string consultaTiposFalta()
        {
            XmlSerializador _oSer = new XmlSerializador();
            DataTable dt_tipo_falta = new DataTable();

            SILPA.LogicaNegocio.RUIA.Listas _oLista = new SILPA.LogicaNegocio.RUIA.Listas();
            DataSet ds_tipo_falta = _oLista.ListaTipoFalta();
            dt_tipo_falta = ds_tipo_falta.Tables[0];

            string str_tipo_falta = _oSer.serializar(dt_tipo_falta);

            return str_tipo_falta;
        }

        public string consultaTiposSancion()
        {
            XmlSerializador _oSer = new XmlSerializador();
            DataTable dt_tipo_sancion = new DataTable();

            SILPA.LogicaNegocio.RUIA.Listas _oLista = new SILPA.LogicaNegocio.RUIA.Listas();
            DataSet ds_tipo_sancion = _oLista.ListaTipoSancion();
            dt_tipo_sancion = ds_tipo_sancion.Tables[0];

            string str_tipo_sancion = _oSer.serializar(dt_tipo_sancion);

            return str_tipo_sancion;

        }

        public string consultaOpcionSancion()
        {
            XmlSerializador _oSer = new XmlSerializador();
            DataTable dt_opcion_sancion = new DataTable();

            SILPA.LogicaNegocio.RUIA.Listas _oLista = new SILPA.LogicaNegocio.RUIA.Listas();
            DataSet ds_opcion_sancion = _oLista.ListaOpcionSancion();
            dt_opcion_sancion = ds_opcion_sancion.Tables[0];

            string str_opcion_sancion = _oSer.serializar(dt_opcion_sancion);

            return str_opcion_sancion;
        }

        /// <summary>
        /// Metodo delegado para utilizar enviar proceso de  actualizar fecha ejecutoria RUIA
        /// </summary>
        /// <param name="xmlDatos">string que simboliza los datos</param>   
        public static void GuardarRuia(object xmlDatos)
        {
            string strXmlDatos = (string)xmlDatos;

            SILPA.LogicaNegocio.RUIA.Sancion _objRuia = new SILPA.LogicaNegocio.RUIA.Sancion();
           _objRuia.GuardarSancion(strXmlDatos);
        }
    }


}
