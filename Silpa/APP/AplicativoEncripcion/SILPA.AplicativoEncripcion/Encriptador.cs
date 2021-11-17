using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SILPA.Comun;

namespace SILPA.AplicativoEncripcion
{
    public partial class Encriptador : Form
    {
        #region Eventos

            // Evento que se ejecuta al cargar la forma
            public Encriptador()
            {
                InitializeComponent();
            }

            /// <summary>
            /// Evento que ejecuta la accion elegida
            /// </summary>
            private void cmdAceptar_Click(object sender, EventArgs e)
            {
                //Verificar que se especifique cadena
                if (!string.IsNullOrWhiteSpace(this.txtCadena.Text))
                {
                    if(this.lstAcciones.SelectedIndex == 0)
                    {
                        this.txtResultado.Text = EnDecript.Encriptar(this.txtCadena.Text.Trim());
                    }
                    else if(this.lstAcciones.SelectedIndex == 1)
                    {
                        this.txtResultado.Text = EnDecript.DesencriptarDesplazamiento(this.txtCadena.Text.Trim());
                    }
                    else if (this.lstAcciones.SelectedIndex == 2)
                    {
                        this.txtResultado.Text = Crypt.Encrypt(this.txtCadena.Text.Trim(), "");
                    }
                    else if (this.lstAcciones.SelectedIndex == 3)
                    {
                        this.txtResultado.Text = Crypt.Decrypt(this.txtCadena.Text.Trim(), "");
                    }
                }
                else
                {
                    this.txtResultado.Text = "No se especifico cadena de entrada";
                }

            }

        #endregion
    }
}
