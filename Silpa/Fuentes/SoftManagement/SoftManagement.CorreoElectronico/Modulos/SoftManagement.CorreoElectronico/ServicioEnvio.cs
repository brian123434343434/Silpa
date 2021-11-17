using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using SoftManagement.CorreoElectronico.Entidades;
using SoftManagement.CorreoElectronico.AccesoDatos;
using SoftManagement.Log;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace SoftManagement.CorreoElectronico
{
    public class ServicioEnvio
    {
        public void Enviar()
        {            
            List<CorreoElectronico.Entidades.CorreoElectronico> correosPendientes = CorreoElectronicoDao.ConsultarCorreosPendientesEnvio();
            
            foreach (CorreoElectronico.Entidades.CorreoElectronico correo in correosPendientes)
            {
                    EnviarCorreo(correo);
            }
        }

        private void EnviarCorreo(CorreoElectronico.Entidades.CorreoElectronico correo)
        {
            MailMessage msg;
            string[] adjuntos;
            char[] splitter = { ';' };
            //TimeSpan diasDiferencia;
            try
            {
                //CorreoServidor correoServidor = CorreoServidorDao.ConsultarCorreoServidorP(correo.ServidorCorreoId);             
                CorreoServidor correoServidor = CorreoServidorDao.ConsultarCorreoServidorP(correo.ServidorCorreoId);   
                if (correoServidor != null)
                {
                    SmtpClient cliente;


                    
                    if (correoServidor.Puerto != string.Empty && correoServidor.Puerto != null)
                        cliente = new SmtpClient(correoServidor.Host, Convert.ToInt32(correoServidor.Puerto));
                    else
                        cliente = new SmtpClient(correoServidor.Host, 25);                    
                    string password = SILPA.Comun.Crypt.Decrypt(correoServidor.Contrasena, "");
                    cliente.Credentials = new System.Net.NetworkCredential(correoServidor.Usuario, password);

                    if (correoServidor.AplicaSeguridad)
                    {
                        cliente.EnableSsl = true;
                    }

                    msg = new MailMessage();
                    msg.From = new MailAddress(correo.De);
                    msg.Subject = correo.Asunto;
                    if (!string.IsNullOrEmpty(correo.Cc))
                        foreach (string destinatarioCC in correo.Cc.Split(';'))
                        {
                            if (destinatarioCC != string.Empty)
                                msg.CC.Add(destinatarioCC);
                        }
                    if (!string.IsNullOrEmpty(correo.Cco))
                        foreach (string destinatarioCCo in correo.Cco.Split(';'))
                        {
                            if (destinatarioCCo != string.Empty)
                            msg.Bcc.Add(destinatarioCCo);
                        }
                    adjuntos = correo.Anexos.Split(splitter);
                    for (int x = 0; x < adjuntos.Length; x++)
                    {
                        if (! String.IsNullOrEmpty(adjuntos[x]))
                        {                         
                            if (System.IO.File.Exists(adjuntos[x]))
                            {
                                //SoftManagement.Log.SMLog.Escribir(Severidad.Informativo,"Archivo Adjunto para Correo: "+ adjuntos[x]);
                                //AGREGAR CONDICION BANDERA
                                if (LogConfig.IngresarDetallesLogCorreo.ToString() == "1")
                                {
                                    SoftManagement.Log.SMLog.EscribirCorreo(correo.CorreoElectronicoId, Severidad.Informativo, "Archivo Adjunto para Correo: " + adjuntos[x], false);
                                }
                                msg.Attachments.Add(new Attachment(adjuntos[x]));
                            }
                        }
                    }

                    //if (correoServidor.NombreServidor.Contains("smtp.gmail.com"))
                    //    cliente.EnableSsl = true;
                    //else
                    //    cliente.EnableSsl = false;
                    msg.IsBodyHtml = true;
                    msg.Body = correo.Mensaje;
                    msg.To.Add(correo.Para);

                    #region jmartinez se realiza cambio de la clase para el log de correos
                    if (LogConfig.IngresarDetallesLogCorreo.ToString() == "1")
                    {
                        //SMLog.Escribir(Severidad.Advertencia, "Envio Correo ---> Servidor: " + cliente.Host + "- Puerto : " + cliente.Port + "- AplicaSeguridad : " + cliente.EnableSsl + "- USuario : " + correoServidor.Usuario + "- USuario : " + password);
                        SMLog.EscribirCorreo(correo.CorreoElectronicoId, Severidad.Advertencia, "Envio Correo ---> Servidor: " + cliente.Host + "- Puerto : " + cliente.Port + "- AplicaSeguridad : " + cliente.EnableSsl + "- USuario : " + correoServidor.Usuario + "- USuario : " + password, false);
                    }

                    #endregion
                    cliente.Send(msg);
                    CorreoElectronicoDao.ActualizarEstadoCorreoElectronico(correo.CorreoElectronicoId, CorreoEstado.Enviado);
                    SMLog.EscribirCorreo(correo.CorreoElectronicoId, Severidad.Advertencia, "Se envio Correo Cumpliendo las validaciones basicas", false);
                    if (correo.ConfirmarEnvio)
                    {
                        this.EnviarAcuseEnvio(correo.Asunto, correo.Mensaje, correo.Para, correo.CorreoElectronicoId);

                    }
                }
                else
                    #region jmartinez se realiza cambio de la clase para el log de correos
                    //SMLog.Escribir(Severidad.Advertencia, String.Format("No existe un servidor de correo con el id {0}", correo.ServidorCorreoId.ToString()));
                    SMLog.EscribirCorreo(correo.CorreoElectronicoId, Severidad.Advertencia, String.Format("No existe un servidor de correo con el id {0}", correo.ServidorCorreoId.ToString()), false);
                    #endregion
            }
            //catch (Exception ex)
            catch (SmtpFailedRecipientException ex)
            {
                SMLog.EscribirCorreo(correo.CorreoElectronicoId, Severidad.Error, string.Format("{0}:{1}",ex.Message,ex.StackTrace), true);
                CorreoElectronicoDao.ActualizarEstadoCorreoElectronico(correo.CorreoElectronicoId, CorreoEstado.Error);
                //CorreoServidor correoServidor = CorreoServidorDao.ConsultarCorreoServidorP(correo.ServidorCorreoId);
                //SMLog.Escribir(Severidad.Critico, "Error : " + ex.Message);
                //SMLog.Escribir(Severidad.Advertencia, "Envio Correo ---> Servidor: " + correoServidor.Host + "- Puerto : " + correoServidor.Puerto + "- AplicaSeguridad : " + correoServidor.AplicaSeguridad + "- USuario : " + correoServidor.Usuario + "- USuario : " + SILPA.Comun.Crypt.Decrypt(correoServidor.Contrasena, ""));
                //SMLog.Escribir(Severidad.Advertencia, "Envío de Correo Electrónico. --> correoElectronicoId: " + correo.CorreoElectronicoId + " Para: " + correo.Para + " Asunto: " + correo.Asunto);

                //diasDiferencia = DateTime.Now.Date.Subtract(correo.FechaCreacion);             
                //if (diasDiferencia.Days >= Convert.ToInt32(CorreoElectronicoConfig.DiasInhabilitar))
                //{
                //    CorreoElectronicoDao.ActualizarEstadoCorreoElectronico(correo.CorreoElectronicoId, CorreoEstado.Inhabilitado);
                //    this.EnviarErrorEnvio(correo.Asunto, correo.Mensaje, correo.Para);
                //}
                //else
                //    CorreoElectronicoDao.ActualizarEstadoCorreoElectronico(correo.CorreoElectronicoId, CorreoEstado.NoEnviado);
            }
            catch (SmtpException e)
            {
                SMLog.EscribirCorreo(correo.CorreoElectronicoId, Severidad.Error, string.Format("{0}:{1}",e.Message,e.StackTrace), true);
                CorreoElectronicoDao.ActualizarEstadoCorreoElectronico(correo.CorreoElectronicoId, CorreoEstado.Error);
                
            }
            catch (Exception f)
            {
                SMLog.EscribirCorreo(correo.CorreoElectronicoId, Severidad.Error, f.ToString(), true);
                CorreoElectronicoDao.ActualizarEstadoCorreoElectronico(correo.CorreoElectronicoId, CorreoEstado.Error);
            }

        }

        private string RemplazarSeparador(string cadena, string separador)
        {
            char[] splitter = { ';' };
            string[] correos;
            string cadenaRemplazar;
            correos = cadena.Split(splitter);
            if (correos.Length == 2)
                separador = "";

            if (separador == ";")
                cadenaRemplazar = ",";
            else
                cadenaRemplazar = ";";


            return cadena.Replace(cadenaRemplazar, separador);
        }

        private void EnviarAcuseEnvio(string asunto, string mensaje, string destinatario, int IdCorreoPadre)
        {
            ServicioCorreoElectronico.ServicioCorreoElectronico correo = new ServicioCorreoElectronico.ServicioCorreoElectronico();
            correo.Para.Add(CorreoElectronicoConfig.CuentaControl);
            correo.Tokens.Add("MENSAJE", mensaje);
            correo.Tokens.Add("CORREO", destinatario);
            correo.Tokens.Add("ASUNTO", asunto);
            correo.EnviarCorreoAcuseEnvio(CorreoElectronicoConfig.PlantillaAcuseEnvio, IdCorreoPadre);

           
        }

        private void EnviarErrorEnvio(string asunto, string mensaje, string destinatario, int IdCorreoPadre)
        {
            ServicioCorreoElectronico.ServicioCorreoElectronico correo = new ServicioCorreoElectronico.ServicioCorreoElectronico();
            correo.Para.Add(CorreoElectronicoConfig.CuentaControl);
            correo.Tokens.Add("MENSAJE", mensaje);
            correo.Tokens.Add("CORREO", destinatario);
            correo.Tokens.Add("ASUNTO", asunto);
            correo.Enviar(CorreoElectronicoConfig.PlantillaErrorEnvio);
        }
    }
}
