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
  public partial class FConfiguracion : Form
  {
    public FConfiguracion()
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

          tbIPLector.Text = Funcion.datLectorAire.IP;
          nePuertoLector.Valor = Funcion.datLectorAire.Puerto;
          neMinutosApertura.Valor = Funcion.datLectorAire.Tiempo;

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
    private void FConfiguracion_Load(object sender, EventArgs e)
    {
      // Posiciona Pantalla
      Funciones.Fugeneral.Centrar_Formulario_Monitor(Funcion.Monitor, this);

      // Ejecuta Inicialización de Pantalla
      Task.Factory.StartNew(() => { Inicializa_Pantalla(); });
    }
    private void FConfiguracion_Shown(object sender, EventArgs e)
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
      tbIPLector.Focus();
    }

    public void FConfiguracion_FormClosed(object sender, FormClosedEventArgs e)
    {
      // Libera Memoria
      this.Dispose();
      GC.Collect();
    }
    public void FConfiguracion_KeyDown(object sender, KeyEventArgs e)
    {
      // Tecla RETURN
      if (e.KeyCode == Keys.Return)
      {
        e.Handled = true;
        Anterior = false;
        ProcessTabKey(true);
        //SendKeys.SendWait("{TAB}")
      }

      // Tecla CURSOR ARRIBA
      if (e.KeyCode == Keys.Up)
      {
        e.Handled = true;
        Anterior = true;
        ProcessTabKey(false);
        //SendKeys.SendWait("+{TAB}")
      }

      // Tecla CURSOR ABAJO
      if (e.KeyCode == Keys.Down)
      {
        e.Handled = true;
        Anterior = false;
        ProcessTabKey(true);
        //SendKeys.SendWait("{TAB}")
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
      if (ActiveControl.Name == "btnSalir" || Anterior)
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
      if (ActiveControl.Name == "btnSalir" || Anterior)
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
    private void btnSalir_Click(object sender, EventArgs e)
    {
      // Cierra Pantalla
      this.Close();
    }
    private void btnAceptar_Click(object sender, EventArgs e)
    {    
        Funcion.datLectorAire.IP = tbIPLector.Text;
        Funciones.LectorIni.Escribe_String(Funcion.rutaAP, "Gestion.ini", "Lector_Aire", "IP", Funcion.datLectorAire.IP);
        Funcion.datLectorAire.Puerto = Convert.ToInt32(nePuertoLector.Valor);
        Funciones.LectorIni.Escribe_Integer(Funcion.rutaAP, "Gestion.ini", "Lector_Aire", "PUERTO", Funcion.datLectorAire.Puerto);
        Funcion.datLectorAire.Tiempo = Convert.ToInt32(neMinutosApertura.Valor);
        Funciones.LectorIni.Escribe_Integer(Funcion.rutaAP, "Gestion.ini", "Lector_Aire", "TIEMPO", Funcion.datLectorAire.Tiempo);
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
