namespace ToVerifyObjectExample
{
    partial class ToVerifyObjectExampleMainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialogCMSAttached = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogFileCMSDetached = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogSignCMSDetached = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogXML = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.cmsPkcs7Attached = new System.Windows.Forms.TabPage();
            this.buttonFirmarCMSAttached = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSeleccionarAttachedSign = new System.Windows.Forms.Button();
            this.textBoxFileClearTextAttachedPath = new System.Windows.Forms.TextBox();
            this.textBoxFileSignedAttachedPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.cmsPkcs7Attached.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialogCMSAttached
            // 
            this.openFileDialogCMSAttached.Filter = "Archivos firmados en formato CMS Attached|*.p7z|All Files|*.*";
            // 
            // openFileDialogFileCMSDetached
            // 
            this.openFileDialogFileCMSDetached.Filter = "All Files|*.*";
            // 
            // openFileDialogSignCMSDetached
            // 
            this.openFileDialogSignCMSDetached.Filter = "Archivos firmados en formato CMS Detached|*.p7m|All Files|*.*";
            // 
            // openFileDialogXML
            // 
            this.openFileDialogXML.Filter = "Archivos XML firmados digitalmente|*.xml|All files|*.*";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.cmsPkcs7Attached);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(576, 191);
            this.tabControl1.TabIndex = 1;
            // 
            // cmsPkcs7Attached
            // 
            this.cmsPkcs7Attached.Controls.Add(this.buttonFirmarCMSAttached);
            this.cmsPkcs7Attached.Controls.Add(this.groupBox2);
            this.cmsPkcs7Attached.Location = new System.Drawing.Point(4, 22);
            this.cmsPkcs7Attached.Name = "cmsPkcs7Attached";
            this.cmsPkcs7Attached.Padding = new System.Windows.Forms.Padding(3);
            this.cmsPkcs7Attached.Size = new System.Drawing.Size(568, 165);
            this.cmsPkcs7Attached.TabIndex = 0;
            this.cmsPkcs7Attached.Text = "Firma CMS PKCS7 attached";
            this.cmsPkcs7Attached.UseVisualStyleBackColor = true;
            // 
            // buttonFirmarCMSAttached
            // 
            this.buttonFirmarCMSAttached.Location = new System.Drawing.Point(453, 118);
            this.buttonFirmarCMSAttached.Name = "buttonFirmarCMSAttached";
            this.buttonFirmarCMSAttached.Size = new System.Drawing.Size(88, 23);
            this.buttonFirmarCMSAttached.TabIndex = 2;
            this.buttonFirmarCMSAttached.Text = "Verificar Firma";
            this.buttonFirmarCMSAttached.UseVisualStyleBackColor = true;
            this.buttonFirmarCMSAttached.Click += new System.EventHandler(this.buttonFirmarCMSAttached_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSeleccionarAttachedSign);
            this.groupBox2.Controls.Add(this.textBoxFileClearTextAttachedPath);
            this.groupBox2.Controls.Add(this.textBoxFileSignedAttachedPath);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(13, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(528, 89);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Verificación de firma en formato CMS attached";
            // 
            // buttonSeleccionarAttachedSign
            // 
            this.buttonSeleccionarAttachedSign.Location = new System.Drawing.Point(422, 20);
            this.buttonSeleccionarAttachedSign.Name = "buttonSeleccionarAttachedSign";
            this.buttonSeleccionarAttachedSign.Size = new System.Drawing.Size(95, 23);
            this.buttonSeleccionarAttachedSign.TabIndex = 4;
            this.buttonSeleccionarAttachedSign.Text = "Seleccionar...";
            this.buttonSeleccionarAttachedSign.UseVisualStyleBackColor = true;
            this.buttonSeleccionarAttachedSign.Click += new System.EventHandler(this.buttonSeleccionarAttachedSign_Click);
            // 
            // textBoxFileClearTextAttachedPath
            // 
            this.textBoxFileClearTextAttachedPath.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBoxFileClearTextAttachedPath.Enabled = false;
            this.textBoxFileClearTextAttachedPath.Location = new System.Drawing.Point(145, 52);
            this.textBoxFileClearTextAttachedPath.Name = "textBoxFileClearTextAttachedPath";
            this.textBoxFileClearTextAttachedPath.Size = new System.Drawing.Size(372, 20);
            this.textBoxFileClearTextAttachedPath.TabIndex = 3;
            // 
            // textBoxFileSignedAttachedPath
            // 
            this.textBoxFileSignedAttachedPath.Location = new System.Drawing.Point(145, 22);
            this.textBoxFileSignedAttachedPath.Name = "textBoxFileSignedAttachedPath";
            this.textBoxFileSignedAttachedPath.Size = new System.Drawing.Size(271, 20);
            this.textBoxFileSignedAttachedPath.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ruta texto claro";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ruta archivo firmado";
            // 
            // ToVerifyObjectExampleMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 216);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ToVerifyObjectExampleMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::Certicámara S.A. :: Ejemplo de uso API verificación de firma digital";
            this.tabControl1.ResumeLayout(false);
            this.cmsPkcs7Attached.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogCMSAttached;
        private System.Windows.Forms.OpenFileDialog openFileDialogFileCMSDetached;
        private System.Windows.Forms.OpenFileDialog openFileDialogSignCMSDetached;
        private System.Windows.Forms.OpenFileDialog openFileDialogXML;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage cmsPkcs7Attached;
        private System.Windows.Forms.Button buttonFirmarCMSAttached;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSeleccionarAttachedSign;
        private System.Windows.Forms.TextBox textBoxFileClearTextAttachedPath;
        private System.Windows.Forms.TextBox textBoxFileSignedAttachedPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

