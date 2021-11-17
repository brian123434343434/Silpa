namespace ServicioNotificacion
{
    partial class ServicioNotificacion
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.timerActualizarEstado = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.timerActualizarEstado)).BeginInit();
            // 
            // timerActualizarEstado
            // 
            this.timerActualizarEstado.Interval = 5000;
            this.timerActualizarEstado.Elapsed += new System.Timers.ElapsedEventHandler(this.timerActualizarEstado_Elapsed);
            // 
            // ServicioNotificacion
            // 
            this.ServiceName = "Service1";
            ((System.ComponentModel.ISupportInitialize)(this.timerActualizarEstado)).EndInit();

        }

        #endregion

        private System.Timers.Timer timerActualizarEstado;
    }
}
