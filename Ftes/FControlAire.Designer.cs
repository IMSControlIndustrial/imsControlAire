
namespace imsControlAire
{
  partial class FControlAire
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FControlAire));
      this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
      this.gbFechaHora = new System.Windows.Forms.GroupBox();
      this.lbUsoCPU = new Guna.UI.WinForms.GunaLabel();
      this.btnConfiguracion = new System.Windows.Forms.Button();
      this.neMilisegundos = new ControlesLib.NumericEdit();
      this.label16 = new System.Windows.Forms.Label();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.configurarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
      this.gbFechaHora.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // gbFechaHora
      // 
      this.gbFechaHora.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(43)))), ((int)(((byte)(92)))));
      this.gbFechaHora.Controls.Add(this.lbUsoCPU);
      this.gbFechaHora.Dock = System.Windows.Forms.DockStyle.Right;
      this.gbFechaHora.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.gbFechaHora.Location = new System.Drawing.Point(141, 0);
      this.gbFechaHora.Name = "gbFechaHora";
      this.gbFechaHora.Size = new System.Drawing.Size(73, 57);
      this.gbFechaHora.TabIndex = 14;
      this.gbFechaHora.TabStop = false;
      // 
      // lbUsoCPU
      // 
      this.lbUsoCPU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(110)))));
      this.lbUsoCPU.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lbUsoCPU.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbUsoCPU.ForeColor = System.Drawing.Color.White;
      this.lbUsoCPU.Location = new System.Drawing.Point(3, 26);
      this.lbUsoCPU.Name = "lbUsoCPU";
      this.lbUsoCPU.Size = new System.Drawing.Size(66, 17);
      this.lbUsoCPU.TabIndex = 10;
      this.lbUsoCPU.Text = "CPU: 0,0";
      this.lbUsoCPU.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // btnConfiguracion
      // 
      this.btnConfiguracion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(43)))), ((int)(((byte)(92)))));
      this.btnConfiguracion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnConfiguracion.ForeColor = System.Drawing.Color.White;
      this.btnConfiguracion.Image = ((System.Drawing.Image)(resources.GetObject("btnConfiguracion.Image")));
      this.btnConfiguracion.Location = new System.Drawing.Point(-2, 13);
      this.btnConfiguracion.Name = "btnConfiguracion";
      this.btnConfiguracion.Size = new System.Drawing.Size(50, 41);
      this.btnConfiguracion.TabIndex = 164;
      this.btnConfiguracion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnConfiguracion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnConfiguracion.UseVisualStyleBackColor = false;
      this.btnConfiguracion.Click += new System.EventHandler(this.btnConfiguracion_Click);
      // 
      // neMilisegundos
      // 
      this.neMilisegundos.AsInteger = 0;
      this.neMilisegundos.Decimales = ((short)(0));
      this.neMilisegundos.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.neMilisegundos.Location = new System.Drawing.Point(56, 29);
      this.neMilisegundos.Margin = new System.Windows.Forms.Padding(5);
      this.neMilisegundos.Mascara = "0";
      this.neMilisegundos.MaxLength = 4;
      this.neMilisegundos.MaxValor = 0D;
      this.neMilisegundos.MinValor = 0D;
      this.neMilisegundos.Name = "neMilisegundos";
      this.neMilisegundos.Size = new System.Drawing.Size(56, 22);
      this.neMilisegundos.TabIndex = 165;
      this.neMilisegundos.Text = "0";
      this.neMilisegundos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.neMilisegundos.Valor = 0D;
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label16.Location = new System.Drawing.Point(110, 35);
      this.label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(26, 16);
      this.label16.TabIndex = 166;
      this.label16.Text = "ms";
      this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.toolStripSeparator1,
            this.configurarToolStripMenuItem,
            this.toolStripSeparator2,
            this.salirToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(132, 82);
      // 
      // abrirToolStripMenuItem
      // 
      this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
      this.abrirToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
      this.abrirToolStripMenuItem.Text = "Abrir";
      this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
      // 
      // configurarToolStripMenuItem
      // 
      this.configurarToolStripMenuItem.Name = "configurarToolStripMenuItem";
      this.configurarToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
      this.configurarToolStripMenuItem.Text = "Configurar";
      this.configurarToolStripMenuItem.Click += new System.EventHandler(this.btnConfiguracion_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(128, 6);
      // 
      // salirToolStripMenuItem
      // 
      this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
      this.salirToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
      this.salirToolStripMenuItem.Text = "Salir";
      this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
      // 
      // notifyIcon1
      // 
      this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
      this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
      this.notifyIcon1.Text = "imsControlAire";
      this.notifyIcon1.Visible = true;
      this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
      // 
      // FControlAire
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(214, 57);
      this.Controls.Add(this.label16);
      this.Controls.Add(this.neMilisegundos);
      this.Controls.Add(this.btnConfiguracion);
      this.Controls.Add(this.gbFechaHora);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Name = "FControlAire";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Control de Aire";
      this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FControlAire_FormClosing);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FControlAire_FormClosed);
      this.Load += new System.EventHandler(this.FControlAire_Load);
      this.Shown += new System.EventHandler(this.FControlAire_Shown);
      this.Click += new System.EventHandler(this.btnSalir_Click);
      this.gbFechaHora.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private System.Windows.Forms.GroupBox gbFechaHora;
    private Guna.UI.WinForms.GunaLabel lbUsoCPU;
    private System.Windows.Forms.Button btnConfiguracion;
    internal ControlesLib.NumericEdit neMilisegundos;
    internal System.Windows.Forms.Label label16;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem configurarToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
    private System.Windows.Forms.NotifyIcon notifyIcon1;
  }
}

