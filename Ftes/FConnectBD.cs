using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imsControlAire
{
  public partial class FConnectBD : Form
  {
    public FConnectBD()
    {
      InitializeComponent();
    }

    #region [ Variables ]
    int Spuerto;
    string Snombre, Susuario, Sclave;
    bool Anterior, Cerrar, FinalCarga;
    #endregion

    #region [ Carga Formulario ]
    private void Inicializa_Pantalla()
    {
      try
      {
        // Carga Datos Base de Datos
        Snombre = Funciones.LectorIni.Leer_String(Funcion.rutaAP, "Gestion.ini", "SERVIDOR", "NOMBRE");
        Spuerto = Funciones.LectorIni.Leer_Integer(Funcion.rutaAP, "Gestion.ini", "SERVIDOR", "PUERTO");
        Susuario = Funciones.LectorIni.Leer_String(Funcion.rutaAP, "Gestion.ini", "SERVIDOR", "USUARIO");
        Sclave = Funciones.LectorIni.Leer_String(Funcion.rutaAP, "Gestion.ini", "SERVIDOR", "CLAVE");

        this.Invoke((MethodInvoker)delegate
        {
          // Servidor Remoto
          if (Snombre.Length > 0)
            tbNombre.Text = Snombre;
          else
            tbNombre.Text = "localhost";
          // Puerto de Servidor
          if (Spuerto <= 0)
            Puerto.Text = "1433";
          else
            Puerto.Text = Spuerto.ToString();
          // Usuario de Servidor
          if (Susuario.Length > 0)
            Usuario.Text = Susuario;
          else
            Usuario.Text = "root";
          // Contraseña de Servidor
          if (Sclave.Length <= 0)
            Clave.Text = "imspesaje";
          else
            Clave.Text = Sclave;
        });
      }
      catch
      {
        Cerrar = true;
        return;
      }
      finally
      {
        FinalCarga = true;
      }
    }
    #endregion

    #region [ Pantalla ]
    private void FConnectBD_Load(object sender, EventArgs e)
    {

      // Ejecuta Inicialización de Pantalla
      Task.Factory.StartNew(() => { Inicializa_Pantalla(); });

      // Centra Pantalla
      Funciones.Fugeneral.Centrar_Formulario_Monitor(Funcion.Monitor, this);

      // Activa Animación
      gunaAnimateWindow1.Start();
    }
    private void FConnectBD_Shown(object sender, EventArgs e)
    {
      // Espera Final de Carga
      while (!FinalCarga)
        Application.DoEvents();

      // Verifica Cerrar Pantalla
      if (Cerrar)
      {
        this.Close();
        return;
      }

      // Solicita Datos
      tbNombre.Focus();
    }
    private void FConnectBD_FormClosed(object sender, FormClosedEventArgs e)
    {
      this.Dispose();
      GC.Collect();
    }
    private void FConnectBD_KeyDown(object sender, KeyEventArgs e)
    {
      // Tecla RETURN
      if (e.KeyCode == Keys.Return)
      {
        e.Handled = true;
        Anterior = false;
        SendKeys.SendWait("{TAB}");
      }

      // Tecla CURSOR ARRIBA
      if (e.KeyCode == Keys.Up)
      {
        e.Handled = true;
        Anterior = true;
        SendKeys.SendWait("+{TAB}");
      }

      // Tecla CURSOR ABAJO
      if (e.KeyCode == Keys.Down)
      {
        e.Handled = true;
        Anterior = false;
        SendKeys.SendWait("{TAB}");
      }

      // Tecla ESCAPE
      if (e.KeyCode == Keys.Escape)
      {
        e.Handled = true;
        Anterior = false;
        this.Close();
      }
    }
    #endregion

    #region [ Controles ]
    private void tbNombre_Enter(object sender, EventArgs e)
    {
      // Actualiza Banderas
      Anterior = false;

      // Carga Mensaje
      if (sender == tbNombre)
        tbNombre.SelectAll();
      if (sender == Puerto)
        Puerto.SelectAll();
      if (sender == Usuario)
        Usuario.SelectAll();
      if (sender == Clave)
        Clave.SelectAll();
    }
    private void tbNombre_Validating(object sender, CancelEventArgs e)
    {
      // Verifica Botones
      if (ActiveControl.Name == "Salir" || Anterior)
      {
        e.Cancel = false;
        return;
      }

      // Verifica Nombre Servidor
      if (sender == tbNombre)
      {
        if (tbNombre.Text.Length <= 0)
          e.Cancel = true;
        else
          e.Cancel = false;
      }

      // Verifica Usuario
      if (sender == Usuario)
      {
        if (Usuario.Text.Length <= 0)
          e.Cancel = true;
        else
          e.Cancel = false;
      }
    }
    private void Puerto_Validating(object sender, CancelEventArgs e)
    {
      // Verifica Botones
      if (ActiveControl.Name == "Salir" || Anterior)
      {
        e.Cancel = false;
        return;
      }

      // Verifica Puerto TCP/IP
      if (Puerto.AsInteger <= 0)
        e.Cancel = true;
      else
        e.Cancel = false;
    }
    private void btnBuscar_Click(object sender, EventArgs e)
    {
      // Declaración de Variables
      FolderBrowserDialog Carpeta = new FolderBrowserDialog();

      // Propiedades de la Caja de Dialogo
      Carpeta.Description = "Seleccionar carpeta ...";
      Carpeta.RootFolder = Environment.SpecialFolder.Desktop;
      Carpeta.ShowNewFolderButton = true;

      // Verifica si se pulsa el Botón OK.
      if (Carpeta.ShowDialog() == DialogResult.OK)
      {
        // Carga la Carpeta Seleccionada
        tbNombre.Text = Carpeta.SelectedPath;
      }

      // Libera Memoria
      Carpeta = null;
    }
    private void Salir_Click(object sender, EventArgs e)
    {
      this.Close();
    }
    private void Aceptar_Click(object sender, EventArgs e)
    {
      // Graba Selecciones
      Funciones.LectorIni.Escribe_String(Funcion.rutaAP, "Gestion.ini", "SERVIDOR", "NOMBRE", tbNombre.Text);
      Funciones.LectorIni.Escribe_String(Funcion.rutaAP, "Gestion.ini", "SERVIDOR", "PUERTO", Puerto.Text);
      Funciones.LectorIni.Escribe_String(Funcion.rutaAP, "Gestion.ini", "SERVIDOR", "USUARIO", Usuario.Text);
      Funciones.LectorIni.Escribe_String(Funcion.rutaAP, "Gestion.ini", "SERVIDOR", "CLAVE", Clave.Text);
      // Actualiza Parametros de Conexión
      FunDatos.PoolBD.Servidor = tbNombre.Text;
      FunDatos.PoolBD.Usuario = Usuario.Text;
      FunDatos.PoolBD.Password = Clave.Text;
      FunDatos.PoolBD.Puerto = Puerto.AsInteger;

      // Cierra Pantalla
      Close();
    }
    #endregion


  }
}
