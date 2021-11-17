using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;  
using System.Data; 

namespace SILPA.Validador
{
    public class Validador
    {
        public enum TipoMotor { SQLSERVER, ORACLE, MYSQL }

        private TipoMotor _motor;

        private string _cadena;

        public Validador(string cadenaCx, TipoMotor tipoMotor)
        {
            _cadena = cadenaCx;
            _motor = tipoMotor;
            switch (_motor)
            {
                case TipoMotor.SQLSERVER: Utilidad.CargaValores(cadenaCx, SILPA.Validador.Utilidad.TipoMotor.SQLSERVER);
                    break;
                case TipoMotor.ORACLE: Utilidad.CargaValores(cadenaCx, SILPA.Validador.Utilidad.TipoMotor.ORACLE);
                    break;
                default: Utilidad.CargaValores(cadenaCx, SILPA.Validador.Utilidad.TipoMotor.SQLSERVER);
                    break;
            }
        }

        public TipoDato BuscarTipoDato(int id)
        {
            TipoDato dato = new SILPA.Validador.TipoDato();
            dato.BuscarRegistro(id);
            return dato;
        }

        public void AdicionarTipoDatos(string tipoDato)
        {
            TipoDato dato = new SILPA.Validador.TipoDato();
            dato.InsertarRegistro(tipoDato); 
            return;
        }

        public void EditarTipoDatos(TipoDato tipoDato)
        {
            TipoDato dato = new SILPA.Validador.TipoDato();
            dato.EditarRegistro(tipoDato);
            return;
        }

        public Campo BuscarCampo(int i)
        {
            Campo campo = new SILPA.Validador.Campo();
            campo.BuscarRegistro("1");//Busca el primer registro
            return campo;
        }

        public void CrearCampo(string id, string descripcion, TipoDato dato)
        {
            Campo  campo = new SILPA.Validador.Campo();
            campo.AdicionarRegistro(id,descripcion, dato);
            return;
        }

        public void EditarCampo(Campo campo)
        {
            Campo campos = new SILPA.Validador.Campo();
            campos.EditarRegistro(campo);
            return;
        }

        public Validacion BuscarValidacion(int id)
        {
            Validacion validacion = new SILPA.Validador.Validacion();
            validacion.BuscarRegistro(id);
            return validacion; 
        }

        public void InsertarValidacion(string descripcion, string sentencia)
        {
            Validacion validacion = new SILPA.Validador.Validacion();
            validacion.InsertarRegistro(descripcion, sentencia);   
        }

        public void EditarValidacion(Validacion validacion)
        {
            Validacion val = new SILPA.Validador.Validacion();
            val.ActualizarRegistro(validacion); 
        }

        public void InsertarValidacionCampo(string idCampo, int idValidacion, string activoSN)
        {
            Campo campo = new Campo();
            Validacion val = new Validacion();

            campo.BuscarRegistro(idCampo);
            val.BuscarRegistro(idValidacion);  

            ValidarCampo validacionCampo = new SILPA.Validador.ValidarCampo();
            validacionCampo.InsertarRegistro(campo, val, activoSN);
            
            return; 
        }

        public ValidarCampo BuscarValidacionCampo(int id)
        {
            ValidarCampo valCam = new SILPA.Validador.ValidarCampo();
            valCam.BuscarRegistro(id);
            return valCam; 
        }

        public void EditarValidacionCampo(ValidarCampo validacionCampo)
        { 
            ValidarCampo valCam = new SILPA.Validador.ValidarCampo();
            valCam.EditarRegistro(validacionCampo);
            return;
        }

        public Negocio ValidadorVariable(string idCampo, object valor)
        {
            Negocio negocio = new SILPA.Validador.Negocio(idCampo, valor);
            negocio.Validador();
            return negocio; 
        }


    }
}
