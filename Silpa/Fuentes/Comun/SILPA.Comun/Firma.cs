using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using co.com.certicamara.certicamaraException.core;
using co.com.certicamara.toVerifyObject.core;
using co.com.certicamara.certicamaraUtils.core;

namespace SILPA.Comun
{
    public class Firma
    {


        private void buttonSeleccionarAttachedSign_Click(object sender, EventArgs e)
        {
            //openFileDialogCMSAttached.ShowDialog();
            //textBoxFileSignedAttachedPath.Text = openFileDialogCMSAttached.FileName;
        }

        /// <summary>
        /// L�gica de verificaci�n de firma digital CMS Attached
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerificarFirma(string rutaArchivoFirmado)
        {
            string _strResultado = "";
            string textBoxFileClearTextAttachedPath = "";
            try
            {
                //
                //Verificamos la firma digital
                CMSAttachedVerifier verifier = new CMSAttachedVerifier(FileUtils.loadFile(rutaArchivoFirmado));
                //Parametro en False para verificaci�n completa (verificaci�n criptogr�fica + verificaci�n certificado y confianza )
                // Parametro en True solo verificaci�n criptogr�fica (ej. para cuando se tenga un certificado revocado o inv�lido y se desea solo extraer el archivo)
                verifier.verify(false);
                _strResultado = "Firma digital verificada exitosamente. ";
                //
                //Recuperamos firmantes de la transacci�n
                ArrayList subjects = verifier.getSignersSubjects();
                string subjectText = "";
                for (int i = 0; i < subjects.Count; i++)
                {
                    subjectText += (i + 1) + ". " + subjects[i] + "\n\n";
                }
                _strResultado += "Firmantes de la transacci�n: " + subjectText;

                //JG: originalFileName obtiene el archivo guardado y luego le quita la extension p7z?
                //Pendiente verificaci�n de como hacerlo en Web
                string originalFileName = "";// openFileDialogCMSAttached.SafeFileName;
                originalFileName = originalFileName.Remove(originalFileName.IndexOf(".p7z"));

                //
                //Recuperamos las fechas de firmado del documento para el primer firmante. Si no existe atributo de fecha y hora se retornar� null
                ArrayList dateTimeSigners = verifier.getSignedTimeXSigner();
                DateTime dateTimeFirstSigner = (DateTime)dateTimeSigners[0];

                if (dateTimeFirstSigner != null)
                {
                    _strResultado += "Fecha y hora de la primera firma. " + dateTimeFirstSigner.ToString();
                }

                //
                //Guardamos el texto claro - Pendiente a Verificaci�n Web
                byte[] clearText = verifier.ContentInfoByteArray;
                FileUtils.saveFile(System.IO.Path.GetDirectoryName(rutaArchivoFirmado) + "\\" + originalFileName, clearText);
                _strResultado+="Path de almacenamiento del texto claro: "+ System.IO.Path.GetDirectoryName(rutaArchivoFirmado) + "\\" + originalFileName;
                textBoxFileClearTextAttachedPath = System.IO.Path.GetDirectoryName(rutaArchivoFirmado) + "\\" + originalFileName;
            }
            catch (CerticamaraException exception)
            {
                displayToSignObjectException(exception);
            }
        }

        /// <summary>
        /// L�gica de despliegue de las excepciones generadas por el API de firma digital de Certic�mara.
        /// </summary>
        /// <param name="e"></param>
        private string displayToSignObjectException(CerticamaraException e)
        {
            string errorDescription = e.ErrorDescription;
            int errorCode = e.ErrorCode;

            string messageToDisplay = "Error presentando al momento de verificar la firma digital.\n";
            messageToDisplay += "C�digo de error: " + errorCode + "\n";
            messageToDisplay += "Descripci�n del error: " + errorDescription + "\n";
            messageToDisplay += "Descripci�n Excepci�n interna: " + e.InnerException.Message;

            return "Error de verificaci�n de firma digital. " + messageToDisplay;
        }
    }
}
