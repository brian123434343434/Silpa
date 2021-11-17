using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class GenProcesoPagoCondicion
    {
        GenProcesoPagoCondicionDALC objGenProcesoPagoCondicionDALC;

        public GenProcesoPagoCondicion() {
            objGenProcesoPagoCondicionDALC = new GenProcesoPagoCondicionDALC();
        }

        public DataTable ListarGenProcesoPagoCondicion(int intIdProcCase, string strConPago, string strConImprimir)
        {
            return objGenProcesoPagoCondicionDALC.ListarProcesoPagoCondicion(intIdProcCase, strConPago, strConImprimir);
        }

        public void InsertarGenProcesoPagoCondicion(int intIdProcCase, string strConPago, string strConImprimir)
        {
            objGenProcesoPagoCondicionDALC.InsertarProcesoPagoCondicion( intIdProcCase,  strConPago,  strConImprimir);
        }

        public void ActualizarGenProcesoPagoCondicion(int intId, int intIdProcCase, string strConPago, string strConImprimir)
        {
            objGenProcesoPagoCondicionDALC.ActualizarProcesoPagoCondicion(intId, intIdProcCase, strConPago, strConImprimir);
            }

        public void EliminarGenProcesoPagoCondicion(int intId)
        {
            objGenProcesoPagoCondicionDALC.EliminarProcesoPagoCondicion(intId);
        }
    }
}
