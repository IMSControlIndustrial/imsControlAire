
namespace imsControlAire
{
  partial class FConfiguracion
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FConfiguracion));
      this.btnAceptar = new System.Windows.Forms.Button();
      this.btnSalir = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.label17 = new System.Windows.Forms.Label();
      this.nePuertoLector = new ControlesLib.NumericEdit();
      this.label16 = new System.Windows.Forms.Label();
      this.neMinutosApertura = new ControlesLib.NumericEdit();
      this.label14 = new System.Windows.Forms.Label();
      this.btnBuscar = new System.Windows.Forms.Button();
      this.tabControl2 = new System.Windows.Forms.TabControl();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.Puerto = new ControlesLib.ExtendedTextBox();
      this.Usuario = new System.Windows.Forms.TextBox();
      this.Clave = new System.Windows.Forms.TextBox();
      this.tbNombre = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.tbIPLector = new System.Windows.Forms.TextBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabControl2.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnAceptar
      // 
      this.btnAceptar.BackColor = System.Drawing.Color.MidnightBlue;
      this.btnAceptar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnAceptar.ForeColor = System.Drawing.Color.White;
      this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
      this.btnAceptar.Location = new System.Drawing.Point(8, 266);
      this.btnAceptar.Margin = new System.Windows.Forms.Padding(5);
      this.btnAceptar.Name = "btnAceptar";
      this.btnAceptar.Size = new System.Drawing.Size(110, 43);
      this.btnAceptar.TabIndex = 35;
      this.btnAceptar.Text = "Aceptar";
      this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAceptar.UseVisualStyleBackColor = false;
      this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
      // 
      // btnSalir
      // 
      this.btnSalir.BackColor = System.Drawing.Color.MidnightBlue;
      this.btnSalir.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnSalir.ForeColor = System.Drawing.Color.White;
      this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
      this.btnSalir.Location = new System.Drawing.Point(368, 266);
      this.btnSalir.Margin = new System.Windows.Forms.Padding(5);
      this.btnSalir.Name = "btnSalir";
      this.btnSalir.Size = new System.Drawing.Size(110, 43);
      this.btnSalir.TabIndex = 36;
      this.btnSalir.Text = "Salir";
      this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnSalir.UseVisualStyleBackColor = false;
      this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Location = new System.Drawing.Point(4, 7);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(478, 115);
      this.tabControl1.TabIndex = 39;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.tbIPLector);
      this.tabPage1.Controls.Add(this.label17);
      this.tabPage1.Controls.Add(this.nePuertoLector);
      this.tabPage1.Controls.Add(this.label16);
      this.tabPage1.Controls.Add(this.neMinutosApertura);
      this.tabPage1.Controls.Add(this.label14);
      this.tabPage1.Location = new System.Drawing.Point(4, 24);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(470, 87);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Configuración Lector RFID";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label17.Location = new System.Drawing.Point(6, 16);
      this.label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(95, 16);
      this.label17.TabIndex = 39;
      this.label17.Text = "IP Lector RFID";
      this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // nePuertoLector
      // 
      this.nePuertoLector.AsInteger = 0;
      this.nePuertoLector.Decimales = ((short)(0));
      this.nePuertoLector.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nePuertoLector.Location = new System.Drawing.Point(387, 49);
      this.nePuertoLector.Margin = new System.Windows.Forms.Padding(5);
      this.nePuertoLector.Mascara = "0";
      this.nePuertoLector.MaxLength = 4;
      this.nePuertoLector.MaxValor = 0D;
      this.nePuertoLector.MinValor = 0D;
      this.nePuertoLector.Name = "nePuertoLector";
      this.nePuertoLector.Size = new System.Drawing.Size(51, 21);
      this.nePuertoLector.TabIndex = 38;
      this.nePuertoLector.Text = "0";
      this.nePuertoLector.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nePuertoLector.Valor = 0D;
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label16.Location = new System.Drawing.Point(319, 55);
      this.label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(48, 16);
      this.label16.TabIndex = 37;
      this.label16.Text = "Puerto";
      this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // neMinutosApertura
      // 
      this.neMinutosApertura.AsInteger = 0;
      this.neMinutosApertura.Decimales = ((short)(0));
      this.neMinutosApertura.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.neMinutosApertura.Location = new System.Drawing.Point(387, 10);
      this.neMinutosApertura.Margin = new System.Windows.Forms.Padding(5);
      this.neMinutosApertura.Mascara = "0";
      this.neMinutosApertura.MaxLength = 4;
      this.neMinutosApertura.MaxValor = 0D;
      this.neMinutosApertura.MinValor = 0D;
      this.neMinutosApertura.Name = "neMinutosApertura";
      this.neMinutosApertura.Size = new System.Drawing.Size(51, 21);
      this.neMinutosApertura.TabIndex = 36;
      this.neMinutosApertura.Text = "0";
      this.neMinutosApertura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.neMinutosApertura.Valor = 0D;
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label14.Location = new System.Drawing.Point(240, 16);
      this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(135, 16);
      this.label14.TabIndex = 35;
      this.label14.Text = "Minutos de Apertura";
      this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // btnBuscar
      // 
      this.btnBuscar.BackColor = System.Drawing.Color.Silver;
      this.btnBuscar.CausesValidation = false;
      this.btnBuscar.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.btnBuscar.ForeColor = System.Drawing.Color.Blue;
      this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
      this.btnBuscar.Location = new System.Drawing.Point(401, 12);
      this.btnBuscar.Name = "btnBuscar";
      this.btnBuscar.Size = new System.Drawing.Size(37, 35);
      this.btnBuscar.TabIndex = 48;
      this.btnBuscar.TabStop = false;
      this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.btnBuscar.UseVisualStyleBackColor = true;
      this.btnBuscar.Visible = false;
      this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
      // 
      // tabControl2
      // 
      this.tabControl2.Controls.Add(this.tabPage2);
      this.tabControl2.Location = new System.Drawing.Point(4, 128);
      this.tabControl2.Name = "tabControl2";
      this.tabControl2.SelectedIndex = 0;
      this.tabControl2.Size = new System.Drawing.Size(478, 134);
      this.tabControl2.TabIndex = 49;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.Puerto);
      this.tabPage2.Controls.Add(this.Usuario);
      this.tabPage2.Controls.Add(this.Clave);
      this.tabPage2.Controls.Add(this.tbNombre);
      this.tabPage2.Controls.Add(this.label8);
      this.tabPage2.Controls.Add(this.label7);
      this.tabPage2.Controls.Add(this.label5);
      this.tabPage2.Controls.Add(this.btnBuscar);
      this.tabPage2.Controls.Add(this.label6);
      this.tabPage2.Location = new System.Drawing.Point(4, 24);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(470, 106);
      this.tabPage2.TabIndex = 0;
      this.tabPage2.Text = "Configuración Base de Datos";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // Puerto
      // 
      this.Puerto._DecimalPoint = true;
      this.Puerto._HighlightText = true;
      this.Puerto._KeyStrokes = ControlesLib.ExtendedTextBox.KeystrokeOption.NumbersOnly;
      this.Puerto._MinusSign = true;
      this.Puerto._ThousandsSeparator = true;
      this.Puerto._TrimSpaces = false;
      this.Puerto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
      this.Puerto.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Puerto.Location = new System.Drawing.Point(68, 65);
      this.Puerto.MaxLength = 5;
      this.Puerto.Name = "Puerto";
      this.Puerto.Size = new System.Drawing.Size(42, 21);
      this.Puerto.TabIndex = 54;
      this.Puerto.Tag = "7";
      this.Puerto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // Usuario
      // 
      this.Usuario.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Usuario.Location = new System.Drawing.Point(213, 65);
      this.Usuario.MaxLength = 40;
      this.Usuario.Name = "Usuario";
      this.Usuario.Size = new System.Drawing.Size(89, 21);
      this.Usuario.TabIndex = 55;
      // 
      // Clave
      // 
      this.Clave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Clave.Location = new System.Drawing.Point(374, 65);
      this.Clave.MaxLength = 40;
      this.Clave.Name = "Clave";
      this.Clave.Size = new System.Drawing.Size(89, 21);
      this.Clave.TabIndex = 56;
      // 
      // tbNombre
      // 
      this.tbNombre.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tbNombre.Location = new System.Drawing.Point(5, 26);
      this.tbNombre.MaxLength = 40;
      this.tbNombre.Name = "tbNombre";
      this.tbNombre.Size = new System.Drawing.Size(368, 21);
      this.tbNombre.TabIndex = 53;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(11, 70);
      this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(48, 16);
      this.label8.TabIndex = 51;
      this.label8.Text = "Puerto";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(143, 70);
      this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(55, 16);
      this.label7.TabIndex = 49;
      this.label7.Text = "Usuario";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(6, 3);
      this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(104, 16);
      this.label5.TabIndex = 39;
      this.label5.Text = "Nombre/IP/Ruta";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(329, 70);
      this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(42, 16);
      this.label6.TabIndex = 37;
      this.label6.Text = "Clave";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // tbIPLector
      // 
      this.tbIPLector.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tbIPLector.Location = new System.Drawing.Point(8, 44);
      this.tbIPLector.MaxLength = 40;
      this.tbIPLector.Name = "tbIPLector";
      this.tbIPLector.Size = new System.Drawing.Size(167, 21);
      this.tbIPLector.TabIndex = 54;
      // 
      // FConfiguracion
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(486, 310);
      this.Controls.Add(this.tabControl2);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.btnSalir);
      this.Controls.Add(this.btnAceptar);
      this.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Name = "FConfiguracion";
      this.Text = "Panel de Configuración";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FConfiguracion_FormClosed);
      this.Load += new System.EventHandler(this.FConfiguracion_Load);
      this.Shown += new System.EventHandler(this.FConfiguracion_Shown);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FConfiguracion_KeyDown);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabControl2.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    internal System.Windows.Forms.Button btnAceptar;
    internal System.Windows.Forms.Button btnSalir;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    internal System.Windows.Forms.Label label17;
    internal ControlesLib.NumericEdit nePuertoLector;
    internal System.Windows.Forms.Label label16;
    internal ControlesLib.NumericEdit neMinutosApertura;
    internal System.Windows.Forms.Label label14;
    internal System.Windows.Forms.Button btnBuscar;
    private System.Windows.Forms.TabControl tabControl2;
    private System.Windows.Forms.TabPage tabPage2;
    internal System.Windows.Forms.Label label8;
    internal System.Windows.Forms.Label label7;
    internal System.Windows.Forms.Label label5;
    internal System.Windows.Forms.Label label6;
    internal System.Windows.Forms.TextBox tbNombre;
    internal ControlesLib.ExtendedTextBox Puerto;
    internal System.Windows.Forms.TextBox Usuario;
    internal System.Windows.Forms.TextBox Clave;
    internal System.Windows.Forms.TextBox tbIPLector;
  }
}