using System;
using System.Collections.Generic;
using System.Text;

namespace Silpa.Workflow.Entidades
{
    public class Condicion
    {
        private int idCondicion;
        private string codigoCondicion;

        public int IdCondicion
        {
            get { return idCondicion; }
            set { idCondicion = value; }
        }

        public string CodigoCondicion
        {
            get { return codigoCondicion; }
            set { codigoCondicion = value; }
        }

        public Condicion(int idCondicion, string codigoCondicion)
        {
            this.idCondicion = idCondicion;
            this.codigoCondicion = codigoCondicion;
        }
    }
}
