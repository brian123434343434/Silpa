namespace AppRadicacion
{
    partial class FrmRadicacion
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.GrpEnviarSolicitud = new System.Windows.Forms.GroupBox();
            this.CboAA = new System.Windows.Forms.ComboBox();
            this.LblAutoridad = new System.Windows.Forms.Label();
            this.GrpEnviarSolicitud.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpEnviarSolicitud
            // 
            this.GrpEnviarSolicitud.Controls.Add(this.LblAutoridad);
            this.GrpEnviarSolicitud.Controls.Add(this.CboAA);
            this.GrpEnviarSolicitud.Location = new System.Drawing.Point(12, 13);
            this.GrpEnviarSolicitud.Name = "GrpEnviarSolicitud";
            this.GrpEnviarSolicitud.Size = new System.Drawing.Size(472, 100);
            this.GrpEnviarSolicitud.TabIndex = 0;
            this.GrpEnviarSolicitud.TabStop = false;
            this.GrpEnviarSolicitud.Text = "EnviarDatos";
            // 
            // CboAA
            // 
            this.CboAA.FormattingEnabled = true;
            this.CboAA.Location = new System.Drawing.Point(80, 35);
            this.CboAA.Name = "CboAA";
            this.CboAA.Size = new System.Drawing.Size(167, 21);
            this.CboAA.TabIndex = 0;
            // 
            // LblAutoridad
            // 
            this.LblAutoridad.AutoSize = true;
            this.LblAutoridad.Location = new System.Drawing.Point(19, 38);
            this.LblAutoridad.Name = "LblAutoridad";
            this.LblAutoridad.Size = new System.Drawing.Size(55, 13);
            this.LblAutoridad.TabIndex = 1;
            this.LblAutoridad.Text = "Autoridad:";
            // 
            // FrmRadicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 271);
            this.Controls.Add(this.GrpEnviarSolicitud);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmRadicacion";
            this.Text = "Radicar";
            this.Load += new System.EventHandler(this.FrmRadicacion_Load);
            this.GrpEnviarSolicitud.ResumeLayout(false);
            this.GrpEnviarSolicitud.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpEnviarSolicitud;
        private System.Windows.Forms.Label LblAutoridad;
        private System.Windows.Forms.ComboBox CboAA;
    }
}

