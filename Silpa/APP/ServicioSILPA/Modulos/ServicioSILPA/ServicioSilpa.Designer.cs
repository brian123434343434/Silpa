namespace ServicioSILPA
{
    partial class ServicioSilpa
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
            this.tmrTimer = new System.Timers.Timer();
            this.timerActualizarEstado = new System.Timers.Timer();
            this.TmrActualizarPago = new System.Timers.Timer();
            this.TmrActualizaEE = new System.Timers.Timer();
            this.TmrAvanzarEstados = new System.Timers.Timer();
            this.TmrAlarmasActividadesPINES = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.tmrTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerActualizarEstado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TmrActualizarPago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TmrActualizaEE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TmrAvanzarEstados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TmrAlarmasActividadesPINES)).BeginInit();
            // 
            // tmrTimer
            // 
            this.tmrTimer.Enabled = true;
            this.tmrTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.tmrTimer_Elapsed);
            // 
            // timerActualizarEstado
            // 
            this.timerActualizarEstado.Interval = 5000D;
            this.timerActualizarEstado.Elapsed += new System.Timers.ElapsedEventHandler(this.timerActualizarEstado_Elapsed);
            // 
            // TmrActualizarPago
            // 
            this.TmrActualizarPago.Enabled = true;
            this.TmrActualizarPago.Interval = 3000D;
            this.TmrActualizarPago.Elapsed += new System.Timers.ElapsedEventHandler(this.TmrActualizarPago_Elapsed);
            // 
            // TmrActualizaEE
            // 
            this.TmrActualizaEE.Interval = 5000D;
            this.TmrActualizaEE.Elapsed += new System.Timers.ElapsedEventHandler(this.TmrActualizaEE_Elapsed);
            // 
            // TmrAvanzarEstados
            // 
            this.TmrAvanzarEstados.Interval = 5000D;
            this.TmrAvanzarEstados.Elapsed += new System.Timers.ElapsedEventHandler(this.TmrAvanzarEstados_Elapsed);
            // 
            // TmrAlarmasActividadesPINES
            // 
            this.TmrAlarmasActividadesPINES.Enabled = true;
            this.TmrAlarmasActividadesPINES.Interval = 10000D;
            this.TmrAlarmasActividadesPINES.Elapsed += new System.Timers.ElapsedEventHandler(this.TmrAlarmasActividadesPINES_Elapsed);
            // 
            // ServicioSilpa
            // 
            this.ServiceName = "ServicioSupra";
            ((System.ComponentModel.ISupportInitialize)(this.tmrTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerActualizarEstado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TmrActualizarPago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TmrActualizaEE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TmrAvanzarEstados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TmrAlarmasActividadesPINES)).EndInit();

        }

        #endregion

        private System.Timers.Timer tmrTimer;
        private System.Timers.Timer timerActualizarEstado;
        private System.Timers.Timer TmrActualizarPago;
        private System.Timers.Timer TmrActualizaEE;
        private System.Timers.Timer TmrAvanzarEstados;
        private System.Timers.Timer TmrAlarmasActividadesPINES;
    }
}
