using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Net.Mail;
using SoftManagement.CorreoElectronico.Entidades;
using SoftManagement.CorreoElectronico;
using SoftManagement.CorreoElectronico.AccesoDatos;
using System.Data;
using SoftManagement.Log;

namespace SoftManagement.ServicioCorreoElectronico
{
    public class ServicioCorreoElectronico
    {
        private StringCollection para;
        private NameValueCollection tokens;
        private StringCollection adjuntos;
        private StringCollection cc;
        private StringCollection cco;


        public StringCollection Para
        {
            get{ return para;}
            protected set {para = value;}
        }

        public NameValueCollection Tokens
        {
            get {return tokens;}
            protected set { tokens = value; }
        }

        public StringCollection Adjuntos
        {
            get { return adjuntos; }
            protected set { adjuntos = value; }
        }

        public StringCollection Cc
        {
            get { return cc; }
            protected set { cc = value; }
        }

        public StringCollection Cco
        {
            get { return cco; }
            protected set { cco = value; }
        }

        public ServicioCorreoElectronico()
        {
            this.para = new StringCollection();
            this.tokens = new NameValueCollection();
            this.adjuntos = new StringCollection();
            this.cc = new StringCollection();
            this.cco = new StringCollection();
        }

        /// <summary>
        /// Enviar la información a la base de datos.
        /// </summary>
        /// <param name="plantillaCorreoId"></param>
        public void Enviar(int plantillaCorreoId)
        {
            try
            {
	            CorreoPlantilla correoPlantilla = CorreoPlantillaDao.ConsultarPlantillaCorreo(plantillaCorreoId);
	            if (correoPlantilla != null)
	            {
	                if (correoPlantilla.Cc != string.Empty)
	                    cc.Add(correoPlantilla.Cc);
	                if (correoPlantilla.Cco != string.Empty)
	                    cco.Add(correoPlantilla.Cco);
	
	                CorreoElectronicoDao.InsertarCorreoElectronico(ReemplazarPlantilla(correoPlantilla.Asunto), correoPlantilla.De, Join(para), Join(cc), Join(cco), ReemplazarPlantilla(correoPlantilla.Plantilla), correoPlantilla.CorreoServidorId, Join(adjuntos), plantillaCorreoId,0);
	            }
	            else
	            {
	                //SMLog.Escribir(Severidad.Critico, String.Format( "No existe una plantilla asociada para el id {0}",plantillaCorreoId));
	                SMLog.Escribir(Severidad.Advertencia, String.Format("No existe una plantilla asociada para el id {0}", plantillaCorreoId));
	                throw new ArgumentException(String.Format( "No existe una plantilla asociada para el id {0}",plantillaCorreoId));
	            }
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al enviar la información a la base de datos.";
                throw new Exception(strException, ex);
            }
        }

        #region jmartinez creo un proceso exclusivo que envie los acuses de envio
        public void EnviarCorreoAcuseEnvio(int plantillaCorreoId, int IdCorreoPadre)
        {
            CorreoPlantilla correoPlantilla = CorreoPlantillaDao.ConsultarPlantillaCorreo(plantillaCorreoId);
            if (correoPlantilla != null)
            {
                if (correoPlantilla.Cc != string.Empty)
                    cc.Add(correoPlantilla.Cc);
                if (correoPlantilla.Cco != string.Empty)
                    cco.Add(correoPlantilla.Cco);

                CorreoElectronicoDao.InsertarCorreoElectronico(ReemplazarPlantilla(correoPlantilla.Asunto), correoPlantilla.De, Join(para), Join(cc), Join(cco), ReemplazarPlantilla(correoPlantilla.Plantilla), correoPlantilla.CorreoServidorId, Join(adjuntos), plantillaCorreoId, IdCorreoPadre);
            }
            else
            {
                //SMLog.Escribir(Severidad.Critico, String.Format( "No existe una plantilla asociada para el id {0}",plantillaCorreoId));
                SMLog.Escribir(Severidad.Advertencia, String.Format("No existe una plantilla asociada para el id {0}", plantillaCorreoId));
                throw new ArgumentException(String.Format("No existe una plantilla asociada para el id {0}", plantillaCorreoId));
            }
        }
        #endregion





        /// <summary>
        /// Enviar la información a la base de datos para correos de estado inhabilitado.
        /// </summary>
        /// <param name="plantillaCorreoId"></param>
        public void EnviarInhabilitado(int plantillaCorreoId, int idRadicacion)
        {
            CorreoPlantilla correoPlantilla = CorreoPlantillaDao.ConsultarPlantillaCorreo(plantillaCorreoId);
            if (correoPlantilla != null)
            {
                //SMLog.Escribir(Severidad.Informativo, "InsertarCorreoElectronico");
                //CorreoElectronicoDao.InsertarCorreoElectronico(ReemplazarPlantilla(correoPlantilla.Asunto), correoPlantilla.De, Join(para), Join(cc), Join(cco), ReemplazarPlantilla(correoPlantilla.Plantilla), correoPlantilla.CorreoServidorId, Join(adjuntos), plantillaCorreoId);
                CorreoElectronicoDao.InsertarCorreoElectronicoInHabilitado(ReemplazarPlantilla(correoPlantilla.Asunto), correoPlantilla.De, Join(para), Join(cc), Join(cco), ReemplazarPlantilla(correoPlantilla.Plantilla), correoPlantilla.CorreoServidorId, Join(adjuntos), plantillaCorreoId, idRadicacion);
            }
            else
            {
                //SMLog.Escribir(Severidad.Critico, String.Format("No existe una plantilla asociada para el id {0}", plantillaCorreoId));
                SMLog.Escribir(Severidad.Advertencia, String.Format("No existe una plantilla asociada para el id {0}", plantillaCorreoId));
                throw new ArgumentException(String.Format("No existe una plantilla asociada para el id {0}", plantillaCorreoId));
            }
        }


        private string ReemplazarPlantilla(string plantilla)
        {
            string valorRemplazar;
            StringBuilder buffer = new StringBuilder(plantilla);
            foreach (String key in this.Tokens.AllKeys)
            {
                valorRemplazar = "{" + key + "}";
                plantilla =  plantilla.Replace(valorRemplazar, Tokens.Get(key));
            }

            return plantilla;
        }


        private string Join(StringCollection cadena)
        {
            StringBuilder cadenaFinal = new StringBuilder();
            StringEnumerator enumerator = cadena.GetEnumerator();
            string resultado;
            while (enumerator.MoveNext())
            {
                cadenaFinal.Append(enumerator.Current);
                cadenaFinal.Append(";");
            }
            if (cadenaFinal.Length > 0)
                resultado = cadenaFinal.ToString().Substring(0, cadenaFinal.Length - 1).ToString();
            else
                resultado = cadenaFinal.ToString();

            return resultado;
        }


    }
}
