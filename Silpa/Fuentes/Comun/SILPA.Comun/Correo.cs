using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using SILPA.Comun;

namespace SILPA.Comun
{
    public class Correo
    {
        private Configuracion _objConfiguracion;
        private string _host;
        private int _puerto;
        private bool _useDefaultCredentials;

        private string _from;
        private string _to;
        private string _subject;
        private string _body;
        private string _attch;
        private List<string> _attachM;

        private byte[] _attachBytes;

        public byte[] AttachBytes
        {
            get { return _attachBytes; }
            set { _attachBytes = value; }
        }


        private string _user;
        private string _clave;
        private string _servidorCorreo;

        private SmtpClient objSmtp;

        public int Puerto { get { return this._puerto; } set { this._puerto = value; } }
        public string Host { get { return this._host; } set { this._host = value; } }
        public bool UseDefaultCredentials { get { return this._useDefaultCredentials; } set { this._useDefaultCredentials = value; } }

        public string From { get { return this._from; } set { this._from = value; } }
        public string To { get { return this._to; } set { this._to = value; } }
        public string Subject { get { return this._subject; } set { this._subject = value; } }
        public string Body { get { return this._body; } set { this._body = value; } }
        public string Attch { get { return this._attch; } set { this._attch = value; } }

        public List<string> AttchMultiple { get { return this._attachM; } set { this._attachM = value; } }

        public string User { get { return this._user; } set { this._user = value; } }
        public string Clave { get { return this._clave; } set { this._clave = value; } }
        public string ServidorCorreo { get { return this._servidorCorreo; } set { this._servidorCorreo = value; } }
        

        /// <summary>
        /// constructor sin parametros del correo
        /// </summary>
        public Correo()
        {
            this._useDefaultCredentials = true;
            this.objSmtp = new SmtpClient();
        }


        /// <summary>
        /// constructor con sus parametros de envio
        /// </summary>
        public Correo
        (
            string strHost, string strFrom, string strTo, string strSubject, string strBody, string Attch, string strUser, string strClave, bool blnDefaultCredentials, string strServidorCorreo, int intPuerto
        )
        {
            this.UseDefaultCredentials = blnDefaultCredentials;
            this.ServidorCorreo = strServidorCorreo;
            this.Puerto = intPuerto;

            this.Host = strHost;
            this.From = strFrom;
            this.To = strTo;
            this.Subject = strSubject;
            this.Body = strBody;
            this.Attch = Attch;
            this.User = strUser;
            this.Clave = strClave;

            this.objSmtp = new SmtpClient();

            objSmtp.Host = this.Host;
            objSmtp.UseDefaultCredentials = this.UseDefaultCredentials;

            if (objSmtp.UseDefaultCredentials == false)
            {
                string str_clave = Crypt.Decrypt(this.Clave, "");
                System.Net.NetworkCredential authInfo = new System.Net.NetworkCredential(this.User, str_clave);
                objSmtp.UseDefaultCredentials = false;
                objSmtp.Credentials = authInfo;
                
            }
            else
            {
                objSmtp.UseDefaultCredentials = false;
                System.Net.NetworkCredential authInfo = new System.Net.NetworkCredential(_objConfiguracion.UsuarioCorreo, _objConfiguracion.PassWordCorreo);
                objSmtp.Credentials = authInfo;

            } 

        }


        /// <summary>
        /// constructor con sus parametros de envío
        /// </summary>
        public Correo(string strFrom, string strTo, string strSubject, string strBody, string Attch)
        {
            _objConfiguracion = new Configuracion();

            this.ServidorCorreo = _objConfiguracion.ServidorCorreo;
            this.Puerto = int.Parse(_objConfiguracion.PuertoCorreo.ToString());

            this.Host = _objConfiguracion.ServidorCorreo;
            if (strFrom == null)
            {
                this.From = _objConfiguracion.SenderCorreo;
            }
            else
            {
                this.From = strFrom;
            }
            this.To = strTo;
            this.Subject = strSubject;
            this.Body = strBody;
            this.Attch = Attch;

            this.objSmtp = new SmtpClient();

            objSmtp.Host = this.Host;

            this.UseDefaultCredentials = Convert.ToBoolean(_objConfiguracion.DefaultCredentials);
            objSmtp.UseDefaultCredentials = this.UseDefaultCredentials;

            if (objSmtp.UseDefaultCredentials == false)
            {
                string str_clave = Crypt.Decrypt(_objConfiguracion.PassWordCorreo, "");
                System.Net.NetworkCredential authInfo = new System.Net.NetworkCredential(_objConfiguracion.UsuarioCorreo, str_clave);
                objSmtp.UseDefaultCredentials = false;
                objSmtp.Credentials = authInfo;
            }
            else
            {
                objSmtp.UseDefaultCredentials = false;
                System.Net.NetworkCredential authInfo = new System.Net.NetworkCredential(_objConfiguracion.UsuarioCorreo, _objConfiguracion.PassWordCorreo);
                objSmtp.Credentials = authInfo;
            } 

        }

      /// <summary>
        /// Constructor para adjuntar documento por bytes
      /// </summary>
      /// <param name="strFrom"></param>
      /// <param name="strTo"></param>
      /// <param name="strSubject"></param>
      /// <param name="strBody"></param>
      /// <param name="Attch">Bytes</param>
      /// <param name="name">Nombre Archivo</param>
        
        public Correo(string strFrom, string strTo, string strSubject, string strBody, byte[] Attch, string name)
        {
            _objConfiguracion = new Configuracion();

            this.ServidorCorreo = _objConfiguracion.ServidorCorreo;
            this.Puerto = int.Parse(_objConfiguracion.PuertoCorreo.ToString());

            this.Host = _objConfiguracion.ServidorCorreo;
            if (strFrom == null)
            {
                this.From = _objConfiguracion.SenderCorreo;
            }
            else
            {
                this.From = strFrom;
            }
            this.To = strTo;
            this.Subject = strSubject;
            this.Body = strBody;
            this.AttachBytes = Attch;
            this.Attch = name;

            this.objSmtp = new SmtpClient();

            objSmtp.Host = this.Host;

            this.UseDefaultCredentials = Convert.ToBoolean(_objConfiguracion.DefaultCredentials);
            objSmtp.UseDefaultCredentials = this.UseDefaultCredentials;

            if (objSmtp.UseDefaultCredentials == false)
            {
                string str_clave = Crypt.Decrypt(_objConfiguracion.PassWordCorreo, "");
                System.Net.NetworkCredential authInfo = new System.Net.NetworkCredential(_objConfiguracion.UsuarioCorreo, str_clave);
                objSmtp.UseDefaultCredentials = false;
                objSmtp.Credentials = authInfo;
            }
            else
            {

                objSmtp.UseDefaultCredentials = false;
                System.Net.NetworkCredential authInfo = new System.Net.NetworkCredential(_objConfiguracion.UsuarioCorreo, _objConfiguracion.PassWordCorreo);
                objSmtp.Credentials = authInfo;
            } 

        }


        /// <summary>
        /// constructor con sus parametros de envío, para adjuntar archivos multiples
        /// </summary>
        public Correo(string strFrom, string strTo, string strSubject, string strBody, List<string> AttchMl)
        {
            _objConfiguracion = new Configuracion();

            this.ServidorCorreo = _objConfiguracion.ServidorCorreo;
            this.Puerto = int.Parse(_objConfiguracion.PuertoCorreo.ToString());

            this.Host = _objConfiguracion.ServidorCorreo;
            if (strFrom == null)
            {
                this.From = _objConfiguracion.SenderCorreo;
            }
            else
            {
                this.From = strFrom;
            }
            this.To = strTo;
            this.Subject = strSubject;
            this.Body = strBody;
            this._attachM = AttchMl;

            this.objSmtp = new SmtpClient();

            objSmtp.Host = this.Host;

            this.UseDefaultCredentials = Convert.ToBoolean(_objConfiguracion.DefaultCredentials);
            objSmtp.UseDefaultCredentials = this.UseDefaultCredentials;

            if (objSmtp.UseDefaultCredentials == false)
            {
                string str_clave = Crypt.Decrypt(_objConfiguracion.PassWordCorreo, "");
                System.Net.NetworkCredential authInfo = new System.Net.NetworkCredential(_objConfiguracion.UsuarioCorreo, str_clave);
                objSmtp.UseDefaultCredentials = false;
                objSmtp.Credentials = authInfo;
            }
            else {
                
                objSmtp.UseDefaultCredentials = false;
                System.Net.NetworkCredential authInfo = new System.Net.NetworkCredential(_objConfiguracion.UsuarioCorreo, _objConfiguracion.PassWordCorreo);
                objSmtp.Credentials = authInfo;
            } 
        }



        /// <summary>
        /// Método que envia correo electrónico cuando el parametro bytes fue enviado
        /// </summary>
        public string EnviarCorreoBytes()
        {
            MailMessage msg = new MailMessage(this.From, this.To);

            msg.Body = this.Body;
            msg.Subject = this.Subject;
            msg.IsBodyHtml = true;

            if (this.AttachBytes != null)
            {
                System.IO.MemoryStream str = new System.IO.MemoryStream(this.AttachBytes);

                Attachment atch = new Attachment(str, this.Attch);
                msg.Attachments.Add(atch);
            }


            //objSmtp.UseDefaultCredentials = this.UseDefaultCredentials;
            objSmtp.Port = this.Puerto;
            try
            {
                
                //objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //objSmtp.EnableSsl = true;
                //objSmtp.Send(msg);
                
                objSmtp.Send(msg);
                return "";
            }
            catch (System.Net.Mail.SmtpFailedRecipientsException e)
            {
                return e.Message;
            }
        }


        public void EnviarCorreo()
        {

            MailMessage msg = new MailMessage(this.From, this.To);

            msg.Body = this.Body;
            msg.Subject = this.Subject;

            if (this.Attch != string.Empty && this.Attch != null)
            {
                Attachment atch = new Attachment(this.Attch);
                msg.Attachments.Add(atch);
            }


            if (this.AttchMultiple != null)
            {
                if (this.AttchMultiple.Count != 0)
                {

                    foreach (string str in AttchMultiple)
                    {
                        if (System.IO.File.Exists(str) == true) 
                        {
                            Attachment atch = new Attachment(str);
                            msg.Attachments.Add(atch);
                        }
                    }
                }
            }

            //objSmtp.UseDefaultCredentials = this.UseDefaultCredentials;

            objSmtp.Port = this.Puerto;

            //objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //objSmtp.EnableSsl = true;
            //objSmtp.Send(msg);

            objSmtp.Send(msg);
        }

        /// <summary>
        /// Método que envia correo electrónico con formato en el Cuerpo (HTML)
        /// </summary>
        /// <returns>Retorna el Resultado del Envío</returns>
        public string EnviarCorreoFormato()
        {
            MailMessage msg = new MailMessage(this.From, this.To);

            msg.Body = this.Body;
            msg.Subject = this.Subject;
            msg.IsBodyHtml = true;
            //msg.bodyFormat = MailFormat.Html;
            if (this.Attch != string.Empty && this.Attch != null)
            {
                Attachment atch = new Attachment(this.Attch);
                msg.Attachments.Add(atch);
            }


            if (this.AttchMultiple != null)
            {
                if (this.AttchMultiple.Count != 0)
                {

                    foreach (string str in AttchMultiple)
                    {
                        if (System.IO.File.Exists(str) == true) 
                        {
                            Attachment atch = new Attachment(str);
                            msg.Attachments.Add(atch);
                        }
                    }
                }
            }

            //objSmtp.UseDefaultCredentials = this.UseDefaultCredentials;
            objSmtp.Port = this.Puerto;
            try
            {
                //    objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    objSmtp.EnableSsl = true;
                //    objSmtp.Send(msg);
                objSmtp.Send(msg);

                return "";
            }
            catch (System.Net.Mail.SmtpFailedRecipientsException e)
            {
                return e.Message;
            }
            catch (System.Net.Mail.SmtpException e)
            {
                return e.Message;
            }
            //MailMessage mail = new MailMessage();
            //mail.To = "me@mycompany.com";
            //mail.From = "you@yourcompany.com";
            //mail.Subject = "this is a test email.";
            //mail.Body = "Some text goes here";
            //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");	//basic authentication
            //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "my_username_here"); //set your username here
            //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "super_secret");	//set your password here

            //SmtpMail.SmtpServer = "mail.mycompany.com";  //your real server goes here
            //SmtpMail.Send(mail);
        }

    }
}
