using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace imsControlAire
{
  public partial class FControlAire : Form
  {
    public FControlAire()
    {
      InitializeComponent();
    }

    #region [ Variables ]
    bool Finalizado = false;
    private int intento = 0;
    #endregion

    #region [ Inicio ]
    public static void Inicializa_Aire()
    {
      Funcion.DDatos_LecAire.Cod_Cosechero = 0;
      Funcion.DDatos_LecAire.Cosechero = "";
      Funcion.DDatos_LecAire.Fecha = DateTime.Now.Date;
      Funcion.DDatos_LecAire.Hora = DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss");
      Funcion.DDatos_LecAire.Tiempo = 0;
    }
    #endregion

    #region [ Pantalla ]
    private void FControlAire_Load(object sender, EventArgs e)
    {
      // Posiciona Pantalla
      Funciones.Fugeneral.Centrar_Formulario_Monitor(Funcion.Monitor, this);
      Funcion.datLectorAire.Crono.Reset();
    }
    private void FControlAire_Shown(object sender, EventArgs e)
    {
      // Actualiza Titulo Pantalla
      Funcion.tokenEsperaRegistros = Funcion.tokenCancela.Token;
        // Tarea Uso CPU
        Task.Factory.StartNew(() => Tarea_ConexionTCPAire(), TaskCreationOptions.LongRunning);
        // Tarea Uso CPU
        Task.Factory.StartNew(() => usoCPU("imsControlAire"), TaskCreationOptions.LongRunning);

        //Tarea Lector RFID IP
        Task.Factory.StartNew(() => AperturaValvulaAire(), TaskCreationOptions.LongRunning);

        // Tarea de Heartbeat para el watchdog externo (ver instrucciones)
        Task.Factory.StartNew(() => Tarea_Heartbeat(), TaskCreationOptions.LongRunning);

        // Tarea para reenviar a la BD las lecturas RFID que no se pudieron
        // grabar por caída de conexión (por ejemplo, durante un corte de luz)
        Task.Factory.StartNew(() => Tarea_ReenvioPendientes(), TaskCreationOptions.LongRunning);

    }
        private void FControlAire_FormClosing(object sender, FormClosingEventArgs e)
    {
            // Si el cierre lo provoca Windows (apagado, reinicio, cierre de sesión)
            // o el propio Administrador de Tareas / un watchdog externo, NO se debe
            // pedir confirmación: nadie va a estar ahí para responder, y el sistema
            // podría quedarse esperando indefinidamente a que el programa cierre.
            if (e.CloseReason == CloseReason.WindowsShutDown ||
                e.CloseReason == CloseReason.TaskManagerClosing ||
                e.CloseReason == CloseReason.ApplicationExitCall)
            {
                return;
            }

            if (MessageBox.Show("¿SEGURO QUE DESEA SALIR?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
        e.Cancel = true;
    }
    private void FControlAire_FormClosed(object sender, FormClosedEventArgs e)
    {
      // Cierra Válvula Aire
      //Funcion.lecFlexy.SwitchRelay(0, false);
      Funcion.CloseTCP();

      // Cierra Tareas
      if (!Funcion.tokenCancela.IsCancellationRequested)
        Funcion.tokenCancela.Cancel();

      // Libera Mutex
      Funcion.mutexProc.ReleaseMutex();

      // Finaliza
      Environment.Exit(0);
    }
    #endregion

    #region[ Controles ]
    private void btnConfiguracion_Click(object sender, EventArgs e)
    {
            using (FConfiguracion frm = new FConfiguracion())
            {
                frm.ShowDialog();
                if (!Funcion.datLectorAire.IP.Equals("") && !Funcion.Conectado)
                    while (intento < 3)
                    {
                        if (Funcion.OpenTCP() != 0) intento++;
                        else
                        {
                            intento = 0;
                            break;
                        }
                    }
                if (intento == 3)
                {
                    MessageBox.Show("No es posible conectar con el lector. Mire su IP.");
                    intento = 0;
                    Application.Exit();
                }
            }
        }
    private void btnSalir_Click(object sender, EventArgs e)
    {
        //Finalizado = true;
        // Cierra Pantalla
        this.Close();

    }

    #endregion

    #region [ Tareas ]
    private void usoCPU(string appName)
    {
        PerformanceCounter total_cpu = new PerformanceCounter("Process", "% Processor Time", "_Total");
        PerformanceCounter process_cpu = new PerformanceCounter("Process", "% Processor Time", appName);
        // Contador para no llamar a FlushMemory en cada vuelta: cada iteración
        // tarda ~5s, así que 60 iteraciones ≈ 5 minutos.
        int ciclosParaFlush = 0;
        while (!Funcion.tokenEsperaRegistros.IsCancellationRequested)
        {
            try
            {
                float t = total_cpu.NextValue();
                float p = process_cpu.NextValue();
                // Actualiza Uso CPU (solo si la pantalla sigue viva)
                if (!this.IsDisposed && this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        try
                        {
                            float uso = (p / t) * 100;
                            if (float.IsNaN(uso) || float.IsInfinity(uso)) uso = 0;
                            lbUsoCPU.Text = "CPU: " + uso.ToString("0.00");
                        }
                        catch { }
                    });
                }
            }
            catch (Exception ex)
            {
                Funcion.Grabar_Error("usoCPU", ex.Message);
            }
            finally
            {
                // Ya NO se llama a FlushMemory en cada vuelta: solo cada ~5 minutos,
                // y sin el vaciado forzado del working set (ver Funcion.FlushMemory).
                ciclosParaFlush++;
                if (ciclosParaFlush >= 60)
                {
                    ciclosParaFlush = 0;
                    Funcion.FlushMemory();
                }
            }
            // Espera
            Task.Delay(5000).Wait();
        }
    }
    private void AperturaValvulaAire()
    {

        // Variables
        int TempRefresco;
        // Este bucle corre cada ~300ms: NO se debe llamar a FlushMemory() aquí
        // (era la causa principal del cuelgue progresivo). Si en algún momento
        // se quisiera forzar una recolección, hacerlo como mucho cada varios
        // minutos con un contador, nunca en cada vuelta.

        while (!Funcion.tokenEsperaRegistros.IsCancellationRequested)
        {
            if (Funcion.frecLectura <= 0)
            {
                Funcion.frecLectura = 200;
            }
            TempRefresco = Funcion.frecLectura;

            try
            {
                // Tiempo de Espera
                Task.Delay(TempRefresco).Wait(Funcion.tokenEsperaRegistros);

                if (Funcion.tokenEsperaRegistros.IsCancellationRequested) return;

                //Para saber si la comunicación con el lector está bien o no.
                bool enlaceOk;
                try
                {
                    enlaceOk = Funcion.lecFlexy.TestNodeLink() == 0;
                }
                catch (Exception exTest)
                {
                    Funcion.Grabar_Error("AperturaValvulaAire.TestNodeLink: ", exTest.Message);
                    enlaceOk = false;
                }

                if (!enlaceOk)
                {
                    Funcion.datLectorAire.Crono.Reset();
                    Funcion.CloseTCP();                       // ahora SIEMPRE deja Conectado=false
                    Task.Delay(1000).Wait(Funcion.tokenEsperaRegistros);  // espera REAL antes de reintentar
                    int resultado = Funcion.OpenTCP();
                    if (resultado != 0)
                    {
                        Funcion.Grabar_Error("AperturaValvulaAire.Reconexion", $"Fallo al reconectar, codigo {resultado}. Se reintentará en el siguiente ciclo.");
                    }
                }

                if (!this.IsDisposed && this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        try
                        {
                            neMilisegundos.Valor = Funcion.datLectorAire.Crono.ElapsedMilliseconds;
                        }
                        catch (Exception Exc)
                        {
                            Funcion.Grabar_Error("AperturaValvulaAire.ContajeMiliSegundos:", Exc.Message);
                        }
                    });
                }

                //SE COMPARARÁ LOS MINUTOS QUE LLEVA EL CRONO
                if ((Funcion.datLectorAire.Crono.ElapsedMilliseconds / 60000) >= Funcion.datLectorAire.Tiempo && Funcion.datLectorAire.Tiempo > 0)
                {
                    //Funcion.datLectorAire.Crono.Stop();
                    Funcion.datLectorAire.Crono.Reset();
                    if (!Funcion.DDatos_LecAire.Cosechero.Equals("") && Funcion.DDatos_LecAire.Cod_Cosechero != 0)
                    {

                        Funcion.lecFlexy.SwitchRelay(0, false);
                        if (Funcion.Depuracion) Funcion.Grabar_Proceso_Depuracion("5.CUENTA ATRAS AIRE ACTIVADO", "CUENTA ATRAS FINALIZADA, AIRE STOP");
                        // Reset Lector
                        Funcion.lecFlexy.HotReset();
                        Inicializa_Aire();
                    }
                }
            }
            catch (Exception Exc)
            {
                Funcion.Grabar_Error("Error en la tarea de Apertura Valvula Aire:", Exc.Message);
            }
            // Espera
            Task.Delay(100).Wait();
        }

    }
    private void Tarea_ConexionTCPAire()
    {

        // Variables
        int TempRefresco = 600000;

        // Tarea para Conexion con Lector RFID.
        while (!Funcion.tokenEsperaRegistros.IsCancellationRequested)
        {
            try
            {
                // Tiempo de Espera
                Task.Delay(TempRefresco).Wait(Funcion.tokenEsperaRegistros);

                if (Funcion.tokenEsperaRegistros.IsCancellationRequested) return;
                if (!Funcion.Conectado)
                    Funcion.OpenTCP();
            }
            catch (Exception Exc)
            {
                Funcion.Grabar_Error("Error en la tarea de Conexion con lector RFID:", Exc.Message);
            }
            // Espera
            Task.Delay(800).Wait();
        }
    }

    // Refresca el fichero heartbeat.txt cada 30s para que el watchdog
    // externo (Tarea Programada de Windows) sepa que el programa sigue
    // vivo y respondiendo. Ver instrucciones del watchdog.
    private void Tarea_Heartbeat()
    {
        while (!Funcion.tokenEsperaRegistros.IsCancellationRequested)
        {
            try
            {
                Funcion.Actualizar_Heartbeat();
            }
            catch (Exception Exc)
            {
                Funcion.Grabar_Error("Tarea_Heartbeat:", Exc.Message);
            }
            Task.Delay(30000).Wait(Funcion.tokenEsperaRegistros);
        }
    }

    // Cada 2 minutos intenta reenviar a la BD las lecturas RFID que se
    // guardaron en local porque la BD no estaba disponible (por ejemplo,
    // durante un corte de luz). Así no se pierden cupos de aire.
    private void Tarea_ReenvioPendientes()
    {
        while (!Funcion.tokenEsperaRegistros.IsCancellationRequested)
        {
            try
            {
                Task.Delay(120000).Wait(Funcion.tokenEsperaRegistros);
                if (Funcion.tokenEsperaRegistros.IsCancellationRequested) return;
                FunDatos.Reenviar_Pendientes_Aire(Funcion.baseDatos);
            }
            catch (Exception Exc)
            {
                Funcion.Grabar_Error("Tarea_ReenvioPendientes:", Exc.Message);
            }
        }
    }
    #endregion

    #region [ Menu Emergente ]
    private void notifyIcon1_DoubleClick(object sender, EventArgs e)
    {
        if (this.WindowState == FormWindowState.Minimized)
            this.WindowState = FormWindowState.Normal;
    }
    private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (this.WindowState == FormWindowState.Minimized)
            this.WindowState = FormWindowState.Normal;
    }
    private void salirToolStripMenuItem_Click(object sender, EventArgs e)
    {
        this.Close();
    }
    #endregion
    }
}
