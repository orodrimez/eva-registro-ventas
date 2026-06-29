namespace RegistroVentas.WinForms
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.btnGenerarToken = new System.Windows.Forms.Button();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.txtSecretoPlano = new System.Windows.Forms.TextBox();
            this.btnCifrarSecreto = new System.Windows.Forms.Button();
            this.txtSecretoCifrado = new System.Windows.Forms.TextBox();
            this.btnRegistrarVenta = new System.Windows.Forms.Button();
            this.btnConsultarPk = new System.Windows.Forms.Button();
            this.txtPk = new System.Windows.Forms.TextBox();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.txtPageSize = new System.Windows.Forms.TextBox();
            this.btnConsultarPaginado = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtReferenciaCancelacion = new System.Windows.Forms.TextBox();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(116, 19);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(121, 20);
            this.txtUsuario.TabIndex = 0;
            // 
            // cmbRol
            // 
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new System.Drawing.Point(116, 52);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(121, 21);
            this.cmbRol.TabIndex = 1;
            // 
            // btnGenerarToken
            // 
            this.btnGenerarToken.Location = new System.Drawing.Point(278, 50);
            this.btnGenerarToken.Name = "btnGenerarToken";
            this.btnGenerarToken.Size = new System.Drawing.Size(75, 23);
            this.btnGenerarToken.TabIndex = 2;
            this.btnGenerarToken.Text = "Generar";
            this.btnGenerarToken.UseVisualStyleBackColor = true;
            this.btnGenerarToken.Click += new System.EventHandler(this.btnGenerarToken_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(110, 15);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(100, 20);
            this.txtCliente.TabIndex = 3;
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(110, 46);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(100, 20);
            this.txtImporte.TabIndex = 4;
            // 
            // txtSecretoPlano
            // 
            this.txtSecretoPlano.Location = new System.Drawing.Point(110, 79);
            this.txtSecretoPlano.Name = "txtSecretoPlano";
            this.txtSecretoPlano.Size = new System.Drawing.Size(100, 20);
            this.txtSecretoPlano.TabIndex = 5;
            // 
            // btnCifrarSecreto
            // 
            this.btnCifrarSecreto.Location = new System.Drawing.Point(233, 76);
            this.btnCifrarSecreto.Name = "btnCifrarSecreto";
            this.btnCifrarSecreto.Size = new System.Drawing.Size(128, 23);
            this.btnCifrarSecreto.TabIndex = 6;
            this.btnCifrarSecreto.Text = "Cifrar Palabra";
            this.btnCifrarSecreto.UseVisualStyleBackColor = true;
            this.btnCifrarSecreto.Click += new System.EventHandler(this.btnCifrarSecreto_Click);
            // 
            // txtSecretoCifrado
            // 
            this.txtSecretoCifrado.Location = new System.Drawing.Point(19, 133);
            this.txtSecretoCifrado.Name = "txtSecretoCifrado";
            this.txtSecretoCifrado.Size = new System.Drawing.Size(277, 20);
            this.txtSecretoCifrado.TabIndex = 7;
            // 
            // btnRegistrarVenta
            // 
            this.btnRegistrarVenta.Location = new System.Drawing.Point(20, 169);
            this.btnRegistrarVenta.Name = "btnRegistrarVenta";
            this.btnRegistrarVenta.Size = new System.Drawing.Size(375, 23);
            this.btnRegistrarVenta.TabIndex = 8;
            this.btnRegistrarVenta.Text = "Registrar Venta";
            this.btnRegistrarVenta.UseVisualStyleBackColor = true;
            this.btnRegistrarVenta.Click += new System.EventHandler(this.btnRegistrarVenta_Click);
            // 
            // btnConsultarPk
            // 
            this.btnConsultarPk.Location = new System.Drawing.Point(19, 61);
            this.btnConsultarPk.Name = "btnConsultarPk";
            this.btnConsultarPk.Size = new System.Drawing.Size(136, 23);
            this.btnConsultarPk.TabIndex = 9;
            this.btnConsultarPk.Text = "Consultar PK";
            this.btnConsultarPk.UseVisualStyleBackColor = true;
            this.btnConsultarPk.Click += new System.EventHandler(this.btnConsultarPk_Click);
            // 
            // txtPk
            // 
            this.txtPk.Location = new System.Drawing.Point(55, 24);
            this.txtPk.Name = "txtPk";
            this.txtPk.Size = new System.Drawing.Size(100, 20);
            this.txtPk.TabIndex = 10;
            // 
            // txtPage
            // 
            this.txtPage.Location = new System.Drawing.Point(89, 19);
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(78, 20);
            this.txtPage.TabIndex = 11;
            // 
            // txtPageSize
            // 
            this.txtPageSize.Location = new System.Drawing.Point(89, 45);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(78, 20);
            this.txtPageSize.TabIndex = 12;
            // 
            // btnConsultarPaginado
            // 
            this.btnConsultarPaginado.Location = new System.Drawing.Point(14, 68);
            this.btnConsultarPaginado.Name = "btnConsultarPaginado";
            this.btnConsultarPaginado.Size = new System.Drawing.Size(153, 23);
            this.btnConsultarPaginado.TabIndex = 13;
            this.btnConsultarPaginado.Text = "Consultar";
            this.btnConsultarPaginado.UseVisualStyleBackColor = true;
            this.btnConsultarPaginado.Click += new System.EventHandler(this.btnConsultarPaginado_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(233, 27);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(189, 23);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Cancelar  Venta";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtReferenciaCancelacion
            // 
            this.txtReferenciaCancelacion.Location = new System.Drawing.Point(86, 27);
            this.txtReferenciaCancelacion.Name = "txtReferenciaCancelacion";
            this.txtReferenciaCancelacion.Size = new System.Drawing.Size(124, 20);
            this.txtReferenciaCancelacion.TabIndex = 15;
            // 
            // txtResultado
            // 
            this.txtResultado.Location = new System.Drawing.Point(13, 18);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultado.Size = new System.Drawing.Size(321, 464);
            this.txtResultado.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.cmbRol);
            this.groupBox1.Controls.Add(this.btnGenerarToken);
            this.groupBox1.Location = new System.Drawing.Point(25, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 88);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Autenticación";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtSecretoCifrado);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnCifrarSecreto);
            this.groupBox2.Controls.Add(this.btnRegistrarVenta);
            this.groupBox2.Controls.Add(this.txtCliente);
            this.groupBox2.Controls.Add(this.txtSecretoPlano);
            this.groupBox2.Controls.Add(this.txtImporte);
            this.groupBox2.Location = new System.Drawing.Point(25, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 212);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Registrar Venta";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.txtPk);
            this.groupBox3.Controls.Add(this.btnConsultarPk);
            this.groupBox3.Location = new System.Drawing.Point(25, 326);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(205, 100);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Consulta Registro";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtPage);
            this.groupBox4.Controls.Add(this.txtPageSize);
            this.groupBox4.Controls.Add(this.btnConsultarPaginado);
            this.groupBox4.Location = new System.Drawing.Point(236, 325);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(232, 100);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Paginación";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(16, 101);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 58);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "groupBox5";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.txtReferenciaCancelacion);
            this.groupBox6.Controls.Add(this.btnCancelar);
            this.groupBox6.Location = new System.Drawing.Point(25, 432);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(443, 62);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Cancelación";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Rol";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Cliente";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Importe";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Palabra";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Palabra Cifrada";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "PK";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Page";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "PageSize";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Referencia";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtResultado);
            this.groupBox7.Location = new System.Drawing.Point(484, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(334, 473);
            this.groupBox7.TabIndex = 22;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Resultado";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 518);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox7);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Button btnGenerarToken;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.TextBox txtSecretoPlano;
        private System.Windows.Forms.Button btnCifrarSecreto;
        private System.Windows.Forms.TextBox txtSecretoCifrado;
        private System.Windows.Forms.Button btnRegistrarVenta;
        private System.Windows.Forms.Button btnConsultarPk;
        private System.Windows.Forms.TextBox txtPk;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.TextBox txtPageSize;
        private System.Windows.Forms.Button btnConsultarPaginado;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtReferenciaCancelacion;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox7;
    }
}

