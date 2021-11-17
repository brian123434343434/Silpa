using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SILPA.Validador
{
    public class Campo
    {
        private string _id;

        public string Id
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
        private TipoDato _tipoDato = new TipoDato();

        public TipoDato TipoDato
        {
            get { return _tipoDato; }
            set { _tipoDato = value; }
        }

        public void AdicionarRegistro(string id, string descripcion, TipoDato dato)
        {
            int idTipoDato;
            DataBase db;
            
            string cadena = Utilidad._utilidad._cadena;
            try
            {
                idTipoDato = dato.Id;  
                switch (Utilidad._utilidad._tipoMotor)
                {
                    case Utilidad.TipoMotor.ORACLE: db = new DataBase(cadena, DataBase.TipoMotor.ORACLE);
                        break;
                    case Utilidad.TipoMotor.SQLSERVER: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                    default: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                }
                db.AdicionarParametros("P_ID", ParameterDirection.Input, SqlDbType.VarChar, id);
                db.AdicionarParametros("P_DESCRIPCION", ParameterDirection.Input, SqlDbType.VarChar, descripcion);
                db.AdicionarParametros("P_ID_TIPO_DATO", ParameterDirection.Input, SqlDbType.Decimal, idTipoDato);
     
                db.EjecutarProcedimiento("SPVA_CREAR_CAMPO");
                return;
            }
            finally
            {
                db = null;
            }

        }

        public void EditarRegistro(Campo campo)
        {
            int idTipoDato;
            string descripcion;
            string idCampo;
            DataBase db;

            string cadena = Utilidad._utilidad._cadena;
            try
            {
                idTipoDato = campo.TipoDato.Id;
                descripcion = campo.Descripcion;
                idCampo = campo.Id;   

                switch (Utilidad._utilidad._tipoMotor)
                {
                    case Utilidad.TipoMotor.ORACLE: db = new DataBase(cadena, DataBase.TipoMotor.ORACLE);
                        break;
                    case Utilidad.TipoMotor.SQLSERVER: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                    default: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                }
                db.AdicionarParametros("P_ID", ParameterDirection.Input, SqlDbType.VarChar, idCampo);
                db.AdicionarParametros("P_DESCRIPCION", ParameterDirection.Input, SqlDbType.VarChar, descripcion);
                db.AdicionarParametros("P_ID_TIPO_DATO", ParameterDirection.Input, SqlDbType.Decimal, idTipoDato);

                db.EjecutarProcedimiento("SPVA_EDITAR_CAMPO");
                return;
            }
            finally
            {
                db = null;
            }
        }

        public void BuscarRegistro(string id)
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

                reg = db.EjecutarProcedimientoRs("SPVA_CAMPO").Select("ID = '" + id + "'");

                if (reg.Length > 0)
                {
                    TipoDato dato = new TipoDato() ; 
                    this.Id = reg[0]["ID"].ToString();
                    this.Descripcion = reg[0]["DESCRIPCION"].ToString();
                    dato.BuscarRegistro(Convert.ToInt32(reg[0]["ID_TIPO_DATO"]));
                    this._tipoDato = dato;
                }
                return;
            }
            finally
            {
                db = null;
            }
        }
    }
}
