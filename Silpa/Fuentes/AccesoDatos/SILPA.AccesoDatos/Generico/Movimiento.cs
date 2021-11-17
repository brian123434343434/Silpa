using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
/// <summary>
/// Summary description for Movimiento
/// </summary>
/// 

namespace SILPA.AccesoDatos.Generico
{

    public class Movimiento
    {
        private int int_mov_id;
        private int int_doc_id;
        private DateTime dt_doc_fecha_creacion;
        private DateTime dt_mov_fecha_creacion;
        private DateTime dt_fecha_vencimiento;
        private string str_nur;
        private string str_remitente;
        private string str_asunto;
        private string str_resumen;
        private int int_anno;

        /// <summary>
        /// Numero Silpa generado
        /// </summary>
        private Int64 int_numero_silpa;


        /// <summary>
        /// Identifdicador del representante legal del solicitante
        /// </summary>
        private int int_representante_legal;

        //09-jun-2010 - aegb: incidencia 1827
        /// <summary>
        /// Identifdicador del representante legal del solicitante
        /// </summary>
        private int int_apoderado;

        /// <summary>
        /// Identifdicador del solicitante
        /// </summary>
        private int int_solicitante;

        /// <summary>
        /// DataTable con la ubicación del proceso
        /// </summary>
        private List<UbicacionProyecto> dt_ubicacion;

        private string str_nombre_representante_legal;

        private int int_tipo_tramite;

        private int int_sector;

        private int int_sector_hijo;

        /// <summary>
        /// identificador del remitente
        /// </summary>
        private int int_id_remitente;

        /// <summary>
        /// LACH - 03/03/2010 Numero vital compuesto
        /// </summary>
        private string str_numero_vital;

        /// <summary>
        /// LACH - 03/03/2010 Numero vital compuesto
        /// </summary>
        public string Str_numero_vital
        {
            get { return str_numero_vital; }
            set { str_numero_vital = value; }
        }



        public int Id_Remitente
        {
            get { return this.int_id_remitente; }
            set { this.int_id_remitente = value; }
        }


        public int Tipo_Tramite 
        {
            get { return this.int_tipo_tramite; }
            set { this.int_tipo_tramite = value; }
        }

        public int Representante_Legal
        {
            get { return this.int_representante_legal; }
            set { this.int_representante_legal = value; }
        }
        //09-jun-2010 - aegb: incidencia 1827
        public int Apoderado
        {
            get { return this.int_apoderado; }
            set { this.int_apoderado = value; }
        }
        
        public string Nombre_Representante_Legal
        {
            get { return this.str_nombre_representante_legal; }
            set { this.str_nombre_representante_legal = value; }
        }

        public int Solicitante
        {
            get { return this.int_solicitante; }
            set { this.int_solicitante = value; }
        }

        public Int64 Numero_Silpa
        {
            get { return this.int_numero_silpa; }
            set { this.int_numero_silpa = value; }
        }


        public int MovimientoID
        {
            get { return this.int_mov_id; }
            set { this.int_mov_id = value; }
        }

        public int DocumentoID
        {
            get { return this.int_doc_id; }
            set { this.int_doc_id = value; }
        }

        public DateTime FechaDocumento
        {
            get { return this.dt_doc_fecha_creacion; }
            set { this.dt_doc_fecha_creacion = value; }
        }

        public int Anno
        {
            get { return this.int_anno; }
            set { this.int_anno = value; }
        }

        public DateTime FechaMovimiento
        {
            get { return this.dt_mov_fecha_creacion; }
            set { this.dt_mov_fecha_creacion = value; }
        }

        public DateTime FechaVencimiento
        {
            get { return this.dt_fecha_vencimiento; }
            set { this.dt_fecha_vencimiento = value; }
        }

        public string NUR
        {
            get { return this.str_nur; }
            set { this.str_nur = value; }
        }

        public string Remitente
        {
            get { return this.str_remitente; }
            set { this.str_remitente = value; }
        }

        public string Asunto
        {
            get { return this.str_asunto; }
            set { this.str_asunto = value; }
        }

        public string Resumen
        {
            get { return this.str_resumen; }
            set { this.str_resumen = value; }
        }

        public List<UbicacionProyecto> Ubicaciones
        {
            get { return this.dt_ubicacion; }
            set { this.dt_ubicacion = value; }
        }

        
        public int Sector
        {
            get { return this.int_sector; }
            set { this.int_sector = value; }
        }

        
        public int Sector_Hijo
        {
            get { return this.int_sector_hijo; }
            set { this.int_sector_hijo = value; }
        }
        
        private string str_observaciones;
        public string Observaciones
        {
            get { return this.str_observaciones; }
            set { this.str_observaciones = value; }
        }

        private string str_nur_asociado;
        public string Nur_Asociado
        {
            get { return this.str_nur_asociado; }
            set { this.str_nur_asociado = value; }
        }

        private string str_dir_Remitente;
        public string Dir_Remitente
        {
            get { return this.str_dir_Remitente; }
            set { this.str_dir_Remitente = value; }
        }

     
    }

}
