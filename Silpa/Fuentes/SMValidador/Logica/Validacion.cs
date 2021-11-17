using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SILPA.Validador
{
    public class Validacion
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private string _sentencia;

        public string Sentencia
        {
            get { return _sentencia; }
            set { _sentencia = value; }
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

                reg = db.EjecutarProcedimientoRs("SPVA_VALIDACION").Select("ID = " + id);

                if (reg.Length > 0)
                {
                    this.Id = Convert.ToInt32(reg[0]["ID"].ToString());
                    this.Descripcion = reg[0]["DESCRIPCION"].ToString();
                    this._sentencia = reg[0]["SENTENCIA"].ToString();  
                }
                return;
            }
            finally
            {
                db = null;
            }
        }

        public void InsertarRegistro(string descripcion, string sentencia)
        {
            DataBase db;
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
                db.AdicionarParametros("P_DESCRIPCION", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, descripcion);
                db.AdicionarParametros("P_SENTENCIA", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, sentencia);
                db.EjecutarProcedimiento("SPVA_CREAR_VALIDACION");
                return;
            }
            finally
            {
                db = null;
            }
        }

        public void ActualizarRegistro(Validacion validacion)
        {
            DataBase db;
            int id;
            string descripcion, sentencia;
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
                id = validacion.Id;
                descripcion = validacion.Descripcion;
                sentencia = validacion.Sentencia;
                db.AdicionarParametros("P_ID", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal , id);
                db.AdicionarParametros("P_DESCRIPCION", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, descripcion);
                db.AdicionarParametros("P_SENTENCIA", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, sentencia);
                db.EjecutarProcedimiento("SPVA_EDITAR_VALIDACION");
                return;
            }
            finally
            {
                db = null;
            }
        }

    }
}
