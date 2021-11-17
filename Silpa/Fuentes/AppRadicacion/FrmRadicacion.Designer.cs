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
            this.BtnSolicitudRadicacion = new System.Windows.Forms.Button();
            this.LblAutoridad = new System.Windows.Forms.Label();
            this.CboAA = new System.Windows.Forms.ComboBox();
            this.LblCadena = new System.Windows.Forms.Label();
            this.TxtCadena = new System.Windows.Forms.TextBox();
            this.BtnCarga = new System.Windows.Forms.Button();
            this.GrpEnviarSolicitud.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpEnviarSolicitud
            // 
            this.GrpEnviarSolicitud.Controls.Add(this.BtnSolicitudRadicacion);
            this.GrpEnviarSolicitud.Controls.Add(this.LblAutoridad);
            this.GrpEnviarSolicitud.Controls.Add(this.CboAA);
            this.GrpEnviarSolicitud.Location = new System.Drawing.Point(12, 60);
            this.GrpEnviarSolicitud.Name = "GrpEnviarSolicitud";
            this.GrpEnviarSolicitud.Size = new System.Drawing.Size(472, 100);
            this.GrpEnviarSolicitud.TabIndex = 0;
            this.GrpEnviarSolicitud.TabStop = false;
            this.GrpEnviarSolicitud.Text = "EnviarDatos";
            // 
            // BtnSolicitudRadicacion
            // 
            this.BtnSolicitudRadicacion.Location = new System.Drawing.Point(253, 33);
            this.BtnSolicitudRadicacion.Name = "BtnSolicitudRadicacion";
            this.BtnSolicitudRadicacion.Size = new System.Drawing.Size(75, 23);
            this.BtnSolicitudRadicacion.TabIndex = 2;
            this.BtnSolicitudRadicacion.Text = "Solicitud";
            this.BtnSolicitudRadicacion.UseVisualStyleBackColor = true;
            this.BtnSolicitudRadicacion.Click += new System.EventHandler(this.BtnSolicitudRadicacion_Click);
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
            // CboAA
            // 
            this.CboAA.FormattingEnabled = true;
            this.CboAA.Location = new System.Drawing.Point(80, 35);
            this.CboAA.Name = "CboAA";
            this.CboAA.Size = new System.Drawing.Size(167, 21);
            this.CboAA.TabIndex = 0;
            // 
            // LblCadena
            // 
            this.LblCadena.AutoSize = true;
            this.LblCadena.Location = new System.Drawing.Point(9, 22);
            this.LblCadena.Name = "LblCadena";
            this.LblCadena.Size = new System.Drawing.Size(47, 13);
            this.LblCadena.TabIndex = 1;
            this.LblCadena.Text = "Cadena:";
            // 
            // TxtCadena
            // 
            this.TxtCadena.Location = new System.Drawing.Point(63, 22);
            this.TxtCadena.Name = "TxtCadena";
            this.TxtCadena.Size = new System.Drawing.Size(376, 20);
            this.TxtCadena.TabIndex = 2;
            // 
            // BtnCarga
            // 
            this.BtnCarga.Location = new System.Drawing.Point(445, 20);
            this.BtnCarga.Name = "BtnCarga";
            this.BtnCarga.Size = new System.Drawing.Size(50, 23);
            this.BtnCarga.TabIndex = 3;
            this.BtnCarga.Text = "Carga";
            this.BtnCarga.UseVisualStyleBackColor = true;
            this.BtnCarga.Click += new System.EventHandler(this.BtnCarga_Click);
            // 
            // FrmRadicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 271);
            this.Controls.Add(this.BtnCarga);
            this.Controls.Add(this.TxtCadena);
            this.Controls.Add(this.LblCadena);
            this.Controls.Add(this.GrpEnviarSolicitud);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmRadicacion";
            this.Text = "Radicar";
            this.Load += new System.EventHandler(this.FrmRadicacion_Load);
            this.GrpEnviarSolicitud.ResumeLayout(false);
            this.GrpEnviarSolicitud.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpEnviarSolicitud;
        private System.Windows.Forms.Label LblAutoridad;
        private System.Windows.Forms.ComboBox CboAA;
        private System.Windows.Forms.Label LblCadena;
        private System.Windows.Forms.TextBox TxtCadena;
        private System.Windows.Forms.Button BtnCarga;
        private System.Windows.Forms.Button BtnSolicitudRadicacion;
    }
}

