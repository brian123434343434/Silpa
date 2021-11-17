using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.AudienciaPublica;
using SILPA.LogicaNegocio.Generico;
using System.Data;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos;
using SILPA.LogicaNegocio.ICorreo;
using System.IO;
using System.Configuration; 

namespace SILPA.LogicaNegocio.Audiencia
{
    public class Audiencia
    {
        public AudienciaIdentity AudIdentity;
        private AudienciaDalc AudDalc;

        /// <summary>
        /// Objeto de Configuracion
        /// </summary>
        private Configuracion _objConfiguracion;

        public Audiencia()
        {
            _objConfiguracion = new Configuracion();
            AudIdentity = new AudienciaIdentity();
            AudDalc = new AudienciaDalc();

        }


        public void GuardarAudiencia(string xmlDatos)
        {
            try
            {
	            AudIdentity = (AudienciaIdentity)AudIdentity.Deserializar(xmlDatos);

	            string ruta = string.Empty;
	
	            List<string> lstNombres= new List<string>();
	            List<Byte[]> lstBytes = new List<byte[]>();
	
	            // Se complementa la ubicacion de los archivos con el filetraffic  
	            if(AudIdentity.ListaDocumentoAdjuntoType.ListaDocumento!=null)
	            {
	                for (int i = 0; i < AudIdentity.ListaDocumentoAdjuntoType.ListaDocumento.Length;i++ )
	                {
	                    lstNombres.Add(AudIdentity.ListaDocumentoAdjuntoType.ListaDocumento[i].nombreArchivo);
	                    lstBytes.Add(AudIdentity.ListaDocumentoAdjuntoType.ListaDocumento[i].bytes);
	                }
	            }
				
	            NumeroSilpa numsilpa = new NumeroSilpa();
	            int idProcessInsntace = numsilpa.ObtenerProcessInstance(AudIdentity.NumeroSilpa);
	            
	            // Se guardan los archivos:
	            TraficoDocumento tf = new TraficoDocumento();
	            tf.RecibirDocumento(idProcessInsntace.ToString(), "AudienciaPublica", lstBytes, ref lstNombres, ref ruta);
	
	            AudIdentity.Edicto = ruta;
	
	            this.AudDalc.InsertarDatosAudiencia(ref AudIdentity);
	
	            PersonaDalc _objPersonaDalc = new PersonaDalc();
	
	            List<PersonaIdentity> _listaPersona = new List<PersonaIdentity>();
	
	            _listaPersona = _objPersonaDalc.BuscarPersonaNumeroVITAL(AudIdentity.NumeroSilpa);
	            
	            //Se envia correo por cada solicitante
	            foreach (PersonaIdentity _objPersona in _listaPersona)
	                ICorreo.Correo.EnviarCorreoAudiencia(AudIdentity, _objPersona);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Guardar Audiencia Pública.";
                throw new Exception(strException, ex);
            }
        }


        public string ConsultarInscritosAudiencia(string idAudiencia)
        {
            DataSet ds_datos = new DataSet();
            XmlSerializador xmlSer = new XmlSerializador();

            ds_datos = this.AudDalc.ConsultarInscritosAudiencia(idAudiencia);

            string resultado = xmlSer.serializar(ds_datos.Tables[0]);

            return resultado;
            
        }


        /// <summary>
        /// hava:06-oct-10
        /// Envía correo electronico al ciudadano sobre la aprobación o no de la audiencia publica
        /// </summary>
        /// <param name="idExpediente">string: identificador del expediente de la audiencia pública</param>
        public void FinalizarAudiencia(string idExpediente)
        {
            try
            {
	            DataSet ds_datos = new DataSet();
	
	            ds_datos = this.AudDalc.ConsultarInscritosAudiencia(idExpediente);
					
	            SILPA.LogicaNegocio.ICorreo.Correo correoAUD = new SILPA.LogicaNegocio.ICorreo.Correo();
				
	            string justifica = string.Empty;
	            string aprobado = string.Empty;
	            string numeroVital = string.Empty;
	            string email = string.Empty;
	            string usuario = string.Empty;
	            string Autoridad_Ambiental = string.Empty;
	            
	            foreach (DataRow dr in ds_datos.Tables[0].Rows) 
	            {
	                if ( dr["NOMBRE"] != DBNull.Value)
	                {
	                    usuario = dr["NOMBRE"].ToString();
	                }
					
	                if ( dr["IAP_APROBACION"] != DBNull.Value)
	                {
	                    if ((bool)dr["IAP_APROBACION"]==true)
	                    {
	                        aprobado = "RECHAZADA";
	                    }
	                    else
	                    {
	                        aprobado = "ACEPTADA";
	                    }
	                }
					
	                if (dr["IAP_CORREO_ELECTRONICO"] != DBNull.Value) 
	                {
	                    email = dr["IAP_CORREO_ELECTRONICO"].ToString();
	                }
					
	                if (dr["AUD_NUMERO_SILPA"] != DBNull.Value)
	                {
	                    numeroVital = dr["AUD_NUMERO_SILPA"].ToString();
	                }
						
	                if (dr["IAP_JUSTIFICACION"] != DBNull.Value)
	                {
	                    justifica = dr["IAP_JUSTIFICACION"].ToString();
	                }
	                if (dr["AUT_DESCRIPCION"] != DBNull.Value)
	                {
	                    Autoridad_Ambiental = dr["AUT_DESCRIPCION"].ToString();
	                }
	                correoAUD.EnviarCorreoFinalizaAudiencia(email, numeroVital, justifica, aprobado, usuario, Autoridad_Ambiental);
	            }
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Finalizar Audiencia.";
                throw new Exception(strException, ex);
            }
        }


        public bool AprobarInscritosAudiencia(string strNumeroInscripcion, bool blnAprobado, string strMotivo)
        {
            bool aprobado;

            aprobado = this.AudDalc.AprobarInscritos(strNumeroInscripcion, blnAprobado, strMotivo);

            return aprobado;
        }

        public string ObtenerDocumentos(string numeroVitalAudiencia)
        {
            List<SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity> _lstDocumentos;            
            string pathDocumento = this.AudDalc.ObtenerPathDocumentos(numeroVitalAudiencia);
            string rutaFus;
            string[] ListaArchivos = null;

            if (pathDocumento != string.Empty)
            {
                if (System.IO.Directory.Exists(pathDocumento))
                {
                    ListaArchivos = System.IO.Directory.GetFiles(pathDocumento);
                }
            }

            _lstDocumentos = new List<SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity>();

         
            if (ListaArchivos != null)
            {
                if (ListaArchivos.Length > 0)
                {
                    foreach (string str in ListaArchivos)
                    {
                        SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity doc = new SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity();
                        doc.NombreArchivo = System.IO.Path.GetFileName(str);
                        doc.Ubicacion = str;
                        _lstDocumentos.Add(doc);
                    }
                }
            }

            string lstListaDocumentos = string.Empty;
            if (_lstDocumentos != null)
            {
                if (_lstDocumentos.Count > 0)
                {

                    foreach (SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity detDoc in _lstDocumentos)
                    {
                        lstListaDocumentos = lstListaDocumentos + ";" + detDoc.NombreArchivo;
                    }

                }
            }

            return lstListaDocumentos;
        }

        public Byte[] ObtenerDocumento(string numeroVitalAudiencia, string nombreArchivo)
        {
            
            string pathDocumento = this.AudDalc.ObtenerPathDocumentos(numeroVitalAudiencia);
            if (File.Exists(pathDocumento + nombreArchivo))
            {
                Byte[] archivo = File.ReadAllBytes(pathDocumento + nombreArchivo);
                return archivo;
            }
            else
            {
                string urlFileTraffic = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString();
                if (File.Exists(urlFileTraffic + nombreArchivo))
                {
                    Byte[] archivo = File.ReadAllBytes(urlFileTraffic + nombreArchivo);
                    return archivo;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Consultar el listado de audiencias
        /// </summary>
        /// <param name="idprocess">identificador del proceso</param>
        /// <param name="sol_id_aa">identificador de la aurtoridad ambiental</param>
        /// <param name="exp_cod">codigo del expediente</param>
        /// <returns></returns>
        public string ConsultarSolicitanteAudienciaPublicaGenEdi(int idprocess, int sol_id_aa, string exp_cod)
        {
            DataSet ds_datos = new DataSet();
            XmlSerializador xmlSer = new XmlSerializador();

            ds_datos = this.AudDalc.ConsultarSolicitanteAudienciaPublicaGenEdi(idprocess, sol_id_aa, exp_cod);

            string resultado = xmlSer.serializar(ds_datos);

            return resultado;

        }


        /// <summary>
        /// Consultar informacion de solicitante de audiencia
        /// </summary>
        /// <param name="idprocess">identificador del proceso</param>
        /// <param name="sol_id_aa">identificador de la aurtoridad ambiental</param>
        /// <param name="mov_id">identficador del movimiento</param>
        /// <returns></returns>
        public string ConsultarSolicitanteAudienciaPublica(Int32 idprocess, Int32 sol_id_aa, Int32 mov_id)
        {
            DataSet ds_datos = new DataSet();
            XmlSerializador xmlSer = new XmlSerializador();

            ds_datos = this.AudDalc.ConsultarSolicitanteAudienciaPublica(idprocess, sol_id_aa, mov_id);

            string resultado = xmlSer.serializar(ds_datos);

            return resultado;
        }
    }
}
