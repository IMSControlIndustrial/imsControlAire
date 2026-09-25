
namespace imsControlAire
{
  partial class FConnectBD
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FConnectBD));
      this.gunaGroupBox2 = new Guna.UI.WinForms.GunaGroupBox();
      this.Label1 = new System.Windows.Forms.Label();
      this.btnBuscar = new System.Windows.Forms.Button();
      this.Label2 = new System.Windows.Forms.Label();
      this.tbNombre = new System.Windows.Forms.TextBox();
      this.Puerto = new ControlesLib.ExtendedTextBox();
      this.Usuario = new System.Windows.Forms.TextBox();
      this.Label4 = new System.Windows.Forms.Label();
      this.Label3 = new System.Windows.Forms.Label();
      this.Clave = new System.Windows.Forms.TextBox();
      this.lbTitulo = new Guna.UI.WinForms.GunaLabel();
      this.Aceptar = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.Salir = new System.Windows.Forms.Button();
      this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
      this.gunaAnimateWindow1 = new Guna.UI.WinForms.GunaAnimateWindow(this.components);
      this.gunaGroupBox2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // gunaGroupBox2
      // 
      this.gunaGroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(100)))), ((int)(((byte)(130)))));
      this.gunaGroupBox2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(100)))), ((int)(((byte)(130)))));
      this.gunaGroupBox2.BorderColor = System.Drawing.Color.Gainsboro;
      this.gunaGroupBox2.Controls.Add(this.Label1);
      this.gunaGroupBox2.Controls.Add(this.btnBuscar);
      this.gunaGroupBox2.Controls.Add(this.Label2);
      this.gunaGroupBox2.Controls.Add(this.tbNombre);
      this.gunaGroupBox2.Controls.Add(this.Puerto);
      this.gunaGroupBox2.Controls.Add(this.Usuario);
      this.gunaGroupBox2.Controls.Add(this.Label4);
      this.gunaGroupBox2.Controls.Add(this.Label3);
      this.gunaGroupBox2.Controls.Add(this.Clave);
      this.gunaGroupBox2.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gunaGroupBox2.LineColor = System.Drawing.Color.Gainsboro;
      this.gunaGroupBox2.Location = new System.Drawing.Point(5, 34);
      this.gunaGroupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.gunaGroupBox2.Name = "gunaGroupBox2";
      this.gunaGroupBox2.Size = new System.Drawing.Size(536, 155);
      this.gunaGroupBox2.TabIndex = 5;
      this.gunaGroupBox2.Text = "PARÁMETROS DE CONEXIÓN";
      this.gunaGroupBox2.TextLocation = new System.Drawing.Point(10, 8);
      // 
      // Label1
      // 
      this.Label1.AutoSize = true;
      this.Label1.ForeColor = System.Drawing.Color.White;
      this.Label1.Location = new System.Drawing.Point(49, 44);
      this.Label1.Name = "Label1";
      this.Label1.Size = new System.Drawing.Size(104, 16);
      this.Label1.TabIndex = 0;
      this.Label1.Text = "Nombre/IP/Ruta";
      // 
      // btnBuscar
      // 
      this.btnBuscar.BackColor = System.Drawing.Color.Silver;
      this.btnBuscar.CausesValidation = false;
      this.btnBuscar.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.btnBuscar.ForeColor = System.Drawing.Color.Blue;
      this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
      this.btnBuscar.Location = new System.Drawing.Point(426, 58);
      this.btnBuscar.Name = "btnBuscar";
      this.btnBuscar.Size = new System.Drawing.Size(37, 35);
      this.btnBuscar.TabIndex = 8;
      this.btnBuscar.TabStop = false;
      this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.btnBuscar.UseVisualStyleBackColor = true;
      this.btnBuscar.Visible = false;
      this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
      // 
      // Label2
      // 
      this.Label2.AutoSize = true;
      this.Label2.ForeColor = System.Drawing.Color.White;
      this.Label2.Location = new System.Drawing.Point(131, 94);
      this.Label2.Name = "Label2";
      this.Label2.Size = new System.Drawing.Size(55, 16);
      this.Label2.TabIndex = 4;
      this.Label2.Text = "Usuario";
      // 
      // tbNombre
      // 
      this.tbNombre.Location = new System.Drawing.Point(52, 64);
      this.tbNombre.MaxLength = 40;
      this.tbNombre.Name = "tbNombre";
      this.tbNombre.Size = new System.Drawing.Size(368, 22);
      this.tbNombre.TabIndex = 1;
      this.tbNombre.Enter += new System.EventHandler(this.tbNombre_Enter);
      this.tbNombre.Validating += new System.ComponentModel.CancelEventHandler(this.tbNombre_Validating);
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
      this.Puerto.Location = new System.Drawing.Point(52, 115);
      this.Puerto.MaxLength = 5;
      this.Puerto.Name = "Puerto";
      this.Puerto.Size = new System.Drawing.Size(42, 22);
      this.Puerto.TabIndex = 3;
      this.Puerto.Tag = "7";
      this.Puerto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.Puerto.Enter += new System.EventHandler(this.tbNombre_Enter);
      this.Puerto.Validating += new System.ComponentModel.CancelEventHandler(this.Puerto_Validating);
      // 
      // Usuario
      // 
      this.Usuario.Location = new System.Drawing.Point(134, 115);
      this.Usuario.MaxLength = 40;
      this.Usuario.Name = "Usuario";
      this.Usuario.Size = new System.Drawing.Size(89, 22);
      this.Usuario.TabIndex = 5;
      this.Usuario.Enter += new System.EventHandler(this.tbNombre_Enter);
      this.Usuario.Validating += new System.ComponentModel.CancelEventHandler(this.tbNombre_Validating);
      // 
      // Label4
      // 
      this.Label4.AutoSize = true;
      this.Label4.ForeColor = System.Drawing.Color.White;
      this.Label4.Location = new System.Drawing.Point(49, 94);
      this.Label4.Name = "Label4";
      this.Label4.Size = new System.Drawing.Size(33, 16);
      this.Label4.TabIndex = 2;
      this.Label4.Text = "Port";
      // 
      // Label3
      // 
      this.Label3.AutoSize = true;
      this.Label3.ForeColor = System.Drawing.Color.White;
      this.Label3.Location = new System.Drawing.Point(286, 94);
      this.Label3.Name = "Label3";
      this.Label3.Size = new System.Drawing.Size(42, 16);
      this.Label3.TabIndex = 6;
      this.Label3.Text = "Clave";
      // 
      // Clave
      // 
      this.Clave.Location = new System.Drawing.Point(289, 115);
      this.Clave.MaxLength = 40;
      this.Clave.Name = "Clave";
      this.Clave.Size = new System.Drawing.Size(89, 22);
      this.Clave.TabIndex = 7;
      this.Clave.Enter += new System.EventHandler(this.tbNombre_Enter);
      // 
      // lbTitulo
      // 
      this.lbTitulo.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.lbTitulo.Dock = System.Windows.Forms.DockStyle.Top;
      this.lbTitulo.Font = new System.Drawing.Font("Segoe UI", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbTitulo.ForeColor = System.Drawing.Color.White;
      this.lbTitulo.Location = new System.Drawing.Point(0, 0);
      this.lbTitulo.Name = "lbTitulo";
      this.lbTitulo.Size = new System.Drawing.Size(550, 30);
      this.lbTitulo.TabIndex = 6;
      this.lbTitulo.Text = "Conexión con Base de Datos";
      this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // Aceptar
      // 
      this.Aceptar.BackColor = System.Drawing.SystemColors.HotTrack;
      this.Aceptar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Aceptar.ForeColor = System.Drawing.Color.White;
      this.Aceptar.Image = ((System.Drawing.Image)(resources.GetObject("Aceptar.Image")));
      this.Aceptar.Location = new System.Drawing.Point(2, 1);
      this.Aceptar.Name = "Aceptar";
      this.Aceptar.Size = new System.Drawing.Size(120, 50);
      this.Aceptar.TabIndex = 0;
      this.Aceptar.Text = "Aceptar";
      this.Aceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.Aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.Aceptar.UseVisualStyleBackColor = false;
      this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.Salir);
      this.panel1.Controls.Add(this.Aceptar);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 194);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(550, 57);
      this.panel1.TabIndex = 7;
      // 
      // Salir
      // 
      this.Salir.BackColor = System.Drawing.SystemColors.HotTrack;
      this.Salir.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Salir.ForeColor = System.Drawing.Color.White;
      this.Salir.Image = ((System.Drawing.Image)(resources.GetObject("Salir.Image")));
      this.Salir.Location = new System.Drawing.Point(420, 2);
      this.Salir.Name = "Salir";
      this.Salir.Size = new System.Drawing.Size(120, 50);
      this.Salir.TabIndex = 1;
      this.Salir.Text = "Salir";
      this.Salir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.Salir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.Salir.UseVisualStyleBackColor = false;
      this.Salir.Click += new System.EventHandler(this.Salir_Click);
      // 
      // gunaControlBox1
      // 
      this.gunaControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.gunaControlBox1.AnimationHoverSpeed = 0.07F;
      this.gunaControlBox1.AnimationSpeed = 0.03F;
      this.gunaControlBox1.BackColor = System.Drawing.Color.Red;
      this.gunaControlBox1.IconColor = System.Drawing.Color.White;
      this.gunaControlBox1.IconSize = 15F;
      this.gunaControlBox1.Location = new System.Drawing.Point(505, 1);
      this.gunaControlBox1.Name = "gunaControlBox1";
      this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
      this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
      this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
      this.gunaControlBox1.Size = new System.Drawing.Size(45, 29);
      this.gunaControlBox1.TabIndex = 8;
      // 
      // gunaAnimateWindow1
      // 
      this.gunaAnimateWindow1.AnimationType = Guna.UI.WinForms.GunaAnimateWindow.AnimateWindowType.AW_CENTER;
      this.gunaAnimateWindow1.Interval = 1000;
      this.gunaAnimateWindow1.TargetControl = null;
      // 
      // FConnectBD
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(550, 251);
      this.Controls.Add(this.gunaControlBox1);
      this.Controls.Add(this.gunaGroupBox2);
      this.Controls.Add(this.lbTitulo);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.KeyPreview = true;
      this.Name = "FConnectBD";
      this.Text = "FConnectBD";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FConnectBD_FormClosed);
      this.Shown += new System.EventHandler(this.FConnectBD_Shown);
      this.Load += new System.EventHandler(this.FConnectBD_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FConnectBD_KeyDown);
      this.gunaGroupBox2.ResumeLayout(false);
      this.gunaGroupBox2.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Guna.UI.WinForms.GunaGroupBox gunaGroupBox2;
    internal System.Windows.Forms.Label Label1;
    internal System.Windows.Forms.Button btnBuscar;
    internal System.Windows.Forms.Label Label2;
    internal System.Windows.Forms.TextBox tbNombre;
    internal ControlesLib.ExtendedTextBox Puerto;
    internal System.Windows.Forms.TextBox Usuario;
    internal System.Windows.Forms.Label Label4;
    internal System.Windows.Forms.Label Label3;
    internal System.Windows.Forms.TextBox Clave;
    private Guna.UI.WinForms.GunaLabel lbTitulo;
    internal System.Windows.Forms.Button Aceptar;
    private System.Windows.Forms.Panel panel1;
    internal System.Windows.Forms.Button Salir;
    private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
    private Guna.UI.WinForms.GunaAnimateWindow gunaAnimateWindow1;
  }
}