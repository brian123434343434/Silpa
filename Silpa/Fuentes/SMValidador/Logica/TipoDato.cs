using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SILPA.Validador
{
    public class TipoDato
    {
        private int _id;
        private string _descripcion;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private string _separador;

        public string Separador
        {
            get { return _separador; }
            set { _separador = value; }
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

                reg = db.EjecutarProcedimientoRs("dbo.SPVA_TIPO_DATO").Select("ID = " + id);

                if (reg.Length > 0)
                {
                    this.Id = Convert.ToInt32(reg[0]["ID"].ToString());
                    this.Descripcion = reg[0]["DESCRIPCION"].ToString();
                    this.Separador = reg[0]["SEPARADOR"].ToString();
                }
                return;
            }
            finally
            {
                db = null;
            }
        }
        public void InsertarRegistro(string tipoDato)
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
                db.AdicionarParametros("P_DESCRIPCION", ParameterDirection.Input, SqlDbType.VarChar, (object)tipoDato);
                db.EjecutarProcedimiento("SPVA_CREAR_TIPO_DATO");
                return;
            }
            finally
            {
                db = null;
            }
        }

        public void EditarRegistro(TipoDato dato)
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
                db.AdicionarParametros("P_ID", ParameterDirection.Input, SqlDbType.Decimal, (object)dato.Id);
                db.AdicionarParametros("P_DESCRIPCION", ParameterDirection.Input, SqlDbType.VarChar, (object)dato.Descripcion);
                db.EjecutarProcedimiento("SPVA_EDITAR_TIPO_DATO");
                return;
            }
            finally
            {
                db = null;
            }
        }
    }
}
