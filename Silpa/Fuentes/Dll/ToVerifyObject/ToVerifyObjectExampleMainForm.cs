using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using co.com.certicamara.toVerifyObject.core;
using co.com.certicamara.certicamaraUtils.core;
using co.com.certicamara.certicamaraException.core;
using System.Collections;

namespace ToVerifyObjectExample
{
    public partial class ToVerifyObjectExampleMainForm : Form
    {
        public ToVerifyObjectExampleMainForm()
        {
            InitializeComponent();
        }

        private void buttonSeleccionarAttachedSign_Click(object sender, EventArgs e)
        {
            openFileDialogCMSAttached.ShowDialog();
            textBoxFileSignedAttachedPath.Text = openFileDialogCMSAttached.FileName;
        }

        /// <summary>
        /// Lógica de verificación de firma digital CMS Attached
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFirmarCMSAttached_Click(object sender, EventArgs e)
        {
            try
            {
                //
                //Verificamos la firma digital
                CMSAttachedVerifier verifier = new CMSAttachedVerifier(FileUtils.loadFile(textBoxFileSignedAttachedPath.Text));
                verifier.verify(false);
                MessageBox.Show("Firma digital verificada exitosamente.", "Resultado de verificación de firma digital");
                //
                //Recuperamos firmantes de la transacción
                ArrayList subjects = verifier.getSignersSubjects();
                String subjectText = "";
                for (int i = 0; i < subjects.Count; i++)
                {
                    subjectText += (i + 1) + ". " + subjects[i] + "\n\n";
                }
                MessageBox.Show(subjectText, "Firmantes de la transacción");
                
                string originalFileName = openFileDialogCMSAttached.SafeFileName;
                originalFileName = originalFileName.Remove(originalFileName.IndexOf(".p7z"));
                
                //
                //Recuperamos las fechas de firmado del documento para el primer firmante. Si no existe atributo de fecha y hora se retornará null
                ArrayList dateTimeSigners = verifier.getSignedTimeXSigner();
                DateTime dateTimeFirstSigner = (DateTime)dateTimeSigners[0];

                if (dateTimeFirstSigner != null)
                {
                    MessageBox.Show(dateTimeFirstSigner.ToString(), "Fecha y hora de la primera firma.");
                }

                //
                //Guardamos el texto claro
                byte[] clearText = verifier.ContentInfoByteArray;
                FileUtils.saveFile(System.IO.Path.GetDirectoryName(textBoxFileSignedAttachedPath.Text) + "\\" + originalFileName, clearText);
                MessageBox.Show(System.IO.Path.GetDirectoryName(textBoxFileSignedAttachedPath.Text) + "\\" + originalFileName, "Path de almacenamiento del texto claro.");
                textBoxFileClearTextAttachedPath.Text = System.IO.Path.GetDirectoryName(textBoxFileSignedAttachedPath.Text) + "\\" + originalFileName;
            }
            catch (CerticamaraException exception)
            {
                displayToSignObjectException(exception);
            }
        }

        /// <summary>
        /// Lógica de despliegue de las excepciones generadas por el API de firma digital de Certicámara.
        /// </summary>
        /// <param name="e"></param>
        private void displayToSignObjectException(CerticamaraException e)
        {
            String errorDescription = e.ErrorDescription;
            int errorCode = e.ErrorCode;

            String messageToDisplay = "Error presentando al momento de verificar la firma digital.\n";
            messageToDisplay += "Código de error: " + errorCode + "\n";
            messageToDisplay += "Descripción del error: " + errorDescription + "\n";
            messageToDisplay += "Descripción Excepción interna: " + e.InnerException.Message;

            MessageBox.Show(messageToDisplay, "Error de verificación de firma digital.");
        }
    }
}