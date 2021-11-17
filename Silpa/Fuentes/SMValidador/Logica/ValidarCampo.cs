using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SILPA.Validador
{
    public class ValidarCampo
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private Campo _campo;

        public Campo Campo
        {
            get { return _campo; }
            set { _campo = value; }
        }
        private Validacion _validacion;

        public Validacion Validacion
        {
            get { return _validacion; }
            set { _validacion = value; }
        }
        private DateTime _fechaCreacion;

        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }
        private string _activoSN;

        public string ActivoSN
        {
            get { return _activoSN; }
            set { _activoSN = value; }
        }

        public DataTable BuscarRegistroCampo(string IdCampo)
        {
            DataBase db;
            string cadena = Utilidad._utilidad._cadena;            try
            {
                switch (Utilidad._utilidad._tipoMotor)
                {
                    case Utilidad.TipoMotor.ORACLE: db = new DataBase(cadena, DataBase.TipoMotor.ORACLE);
                        break;
                    case Utilidad.TipoMotor.SQLSERVER: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                    default: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                }
                db.AdicionarParametros("P_ID_CAMPO", ParameterDirection.Input, SqlDbType.VarChar, IdCampo);
                return(db.EjecutarProcedimientoRs("SPVA_VALIDACION_CAMPO_ID_CAMPO"));
            }
            finally
            {
                db = null;
            }
        }

        public void BuscarRegistro(int id)
        {
            DataBase db;
            DataRow[] reg;
            string cadena = Utilidad._utilidad._cadena;
            try
            {
                switch (Utilidad._utilidad._tipoMotor)
                {
                    case Utilidad.TipoMotor.ORACLE: db = new DataBase(cadena, DataBase.TipoMotor.ORACLE);
                        break;
                    case Utilidad.TipoMotor.SQLSERVER: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                    default: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                }

                reg = db.EjecutarProcedimientoRs("SPVA_VALIDACION_CAMPO").Select("ID = " + id);

                if (reg.Length > 0)
                {
                    Campo campo = new Campo();
                    Validacion validacion = new Validacion();
  
                    this._id = Convert.ToInt32(reg[0]["ID"].ToString());
                    campo.BuscarRegistro(reg[0]["ID_CAMPO"].ToString());
                    this._campo = campo;
                    validacion.BuscarRegistro(Convert.ToInt32(reg[0]["ID_VALIDACION"].ToString()));
                    this._validacion = validacion; 

                    this._fechaCreacion = Convert.ToDateTime(reg[0]["FECHA_INSERCION"].ToString());
                    this._activoSN = reg[0]["ACTIVO_SN"].ToString();
                }
                return;
            }
            finally
            {
                db = null;
            }
        }

        public void InsertarRegistro(Campo campo, Validacion validacion, string activo)
        {
            DataBase db;
            string cadena = Utilidad._utilidad._cadena;
            string idCampo, idValidacion;
            string activoSN;
            try
            {
                idCampo = campo.Id;
                idValidacion = campo.Id;
                activoSN = activo;
                switch (Utilidad._utilidad._tipoMotor)
                {
                    case Utilidad.TipoMotor.ORACLE: db = new DataBase(cadena, DataBase.TipoMotor.ORACLE);
                        break;
                    case Utilidad.TipoMotor.SQLSERVER: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                    default: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                }
                db.AdicionarParametros("P_ID_CAMPO", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, idCampo);
                db.AdicionarParametros("P_ID_VALIDACION", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal, idValidacion);
                db.AdicionarParametros("P_ACTIVO_SN", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, activoSN);

                db.EjecutarProcedimiento("SPVA_CREAR_VALIDACION_CAMPO");
                
                return;
            }
            finally
            {
                db = null;
            }
        }

        public void EditarRegistro(ValidarCampo validacionCampo)
        {
            DataBase db;
            string cadena = Utilidad._utilidad._cadena;
            int id ;
            string idCampo, idValidacion;
            DateTime fechaInsercion; 
            string activoSN;
            try
            {
                id = validacionCampo.Id;
                idCampo = validacionCampo.Campo.Id;
                idValidacion = validacionCampo.Validacion.Id.ToString() ;
                fechaInsercion = validacionCampo.FechaCreacion;
                activoSN = validacionCampo.ActivoSN; 
                switch (Utilidad._utilidad._tipoMotor)
                {
                    case Utilidad.TipoMotor.ORACLE: db = new DataBase(cadena, DataBase.TipoMotor.ORACLE);
                        break;
                    case Utilidad.TipoMotor.SQLSERVER: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                    default: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                }
                db.AdicionarParametros("P_ID", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal, id);
                db.AdicionarParametros("P_ID_CAMPO", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, idCampo);
                db.AdicionarParametros("P_ID_VALIDACION", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal, idValidacion);
                db.AdicionarParametros("P_FECHA_CREACION", System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime, fechaInsercion);
                db.AdicionarParametros("P_ACTIVO_SN", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, activoSN);

                db.EjecutarProcedimiento("SPVA_EDITAR_VALIDACION_CAMPO");

                return;
            }
            finally
            {
                db = null;
            }
        }
    }

}
