using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;

namespace SILPA.AccesoDatos.RegistroMinero
{
    public class EstadoRegistroMineroIdenity
    {
        private Configuracion objConfiguracion;

        public int EstadoID { get; set; }
        public string NombreEstado { get; set; }

        public EstadoRegistroMineroIdenity()
        {
            objConfiguracion = new Configuracion();
        }

        public List<EstadoRegistroMineroIdenity> ListaEstadoregistroMinero()
        {
            List<EstadoRegistroMineroIdenity> LstEstado = new List<EstadoRegistroMineroIdenity>();

            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SP_LISTA_ESTADO_REGISTRO_MINERO");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    EstadoRegistroMineroIdenity estadoresgistro = new EstadoRegistroMineroIdenity();
                    estadoresgistro.EstadoID = Convert.ToInt32(reader["ESTADO_ID"]);
                    estadoresgistro.NombreEstado = reader["NOMBRE_ESTADO"].ToString();
                    LstEstado.Add(estadoresgistro);
                }
            }
            return LstEstado;
        }
    }
}
