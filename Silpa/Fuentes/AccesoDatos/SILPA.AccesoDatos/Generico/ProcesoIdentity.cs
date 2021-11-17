using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{



    public class ProcesoIdentity : EntidadSerializable
    {

        /// <summary>
        /// Consturctor sin parametros
        /// </summary>
        public ProcesoIdentity() { }


        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="int64IdProcessCase"></param>
        /// <param name="int64IdProcessInstance"></param>
        /// <param name="int64IdForm"></param>
        /// <param name="int64IdFormInstance"></param>
        public ProcesoIdentity(
                            Int64 int64IdProcessCase, Int64 int64IdProcessInstance,
                              Int64 int64IdForm, Int64 int64IdFormInstance)
        {
            this._idProcessCase = int64IdProcessCase;
            this._idProcessInstance = int64IdProcessInstance;
            this._idForm = int64IdForm;
            this._idFormInstance = int64IdFormInstance;
            this.Clave = string.Empty;
        }



        private bool _tipoEntidad;

        public bool TipoEntidad
        {
            get { return _tipoEntidad; }
            set { _tipoEntidad = value; }
        }
        private Int64 _idProcessCase;

        public Int64 IdProcessCase
        {
            get { return _idProcessCase; }
            set { _idProcessCase = value; }
        }
        private Int64 _idProcessInstance;

        public Int64 IdProcessInstance
        {
            get { return _idProcessInstance; }
            set { _idProcessInstance = value; }
        }
        private Int64 _idForm;

        public Int64 IdForm
        {
            get { return _idForm; }
            set { _idForm = value; }
        }
        private Int64 _idFormInstance;

        public Int64 IdFormInstance
        {
            get { return _idFormInstance; }
            set { _idFormInstance = value; }
        }

        private string _strClave;

        public string Clave
        {
            get { return _strClave; }
            set { _strClave = value; }
        }


        /// <summary>
        ///  Hava:21-jun-2010
        /// </summary>
        private string _strCondicionPago;
        private string _strCondicionImprimirReciboPago;

        public string CondicionPago
        {
            get { return _strCondicionPago; }
            set { _strCondicionPago = value; }
        }

        public string CondicionImprimirReciboPago
        {
            get { return _strCondicionImprimirReciboPago; }
            set { _strCondicionImprimirReciboPago = value; }
        }

        public void EjecutarProceso()
        {



        }


    }
}
