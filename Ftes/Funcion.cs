using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ComunicaDll;
using System.Globalization;

namespace imsControlAire
{
  class Funcion
  {

    // Declaraciones Externas
    [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

    #region [ Clases ]
    public class Datos_Lectura_Aire
    {
      public string Hora, Cosechero, Tag;
      public DateTime Fecha;
      public int Cod_Cosechero, Tiempo;
      public Datos_Lectura_Aire()
      {
        Cosechero = "";
        Hora = "00:00:00";
        Cod_Cosechero = 0;
        Fecha = DateTime.Now.Date;
        Tiempo = 0;
        Tag = "";
      }
    }
    public class datos_Lector_Aire
    {
      public string IP;
      public int Tiempo, Puerto;
      public bool Activa;
      public Stopwatch Crono;
      public datos_Lector_Aire()
      {
        Tiempo = Puerto = 0;
        IP = string.Empty;
        Activa = false;
        Crono = Stopwatch.StartNew();
      }
    }
    #endregion

    #region [ Variables ]
    // Mutex de Proceso
    public static System.Threading.Mutex mutexProc;

    public static Datos_Lectura_Aire DDatos_LecAire = new Datos_Lectura_Aire();   // Datos del Aire a compresión
    public static int frecLectura;


    public static string rutaAP;                                      // Ruta de la Aplicación
    public static int Monitor;                                        // Monitor de Visualización
    public static string Icampa;                                      // Campaña Actual
    public static bool Tactil;                                        // Bandera de Pantalla Táctil
    public static int Ejercicio;																			// Ejercicio Actual
    public static string baseDatos;                                   // Nombre de la Base de Datos
    public static string ClaveA;                                      // Clave de Acceso
    public static string cValor;                                      // Variable para intercambio de datos.
    public static bool Depuracion;                                    // Bit para la depuracion con el ejecutable

    public static CancellationToken tokenEsperaRegistros = new CancellationToken();
    public static CancellationTokenSource tokenCancela = new CancellationTokenSource();

    public static datos_Lector_Aire datLectorAire = new datos_Lector_Aire();//Datos del Lector de Aire a Compresion

    private static KNet.TCPClient lector = new KNet.TCPClient();
    public static KNet.BioNet lecFlexy = new KNet.BioNet();
    public static bool Conectado;

    #endregion

    #region [ Funciones ]
    public static void Grabar_Error(string Proceso, string Mensaje)
    {
        // Declaración de Variables
        Funciones.StringList Datos = null;
        string Fichero = "";
        string Cadena = "";

        // Verifica Directorio DATOS
        Fichero = Funcion.rutaAP + "\\Datos\\Errores";
        if (!Directory.Exists(Fichero))
            Directory.CreateDirectory(Fichero);

        // Graba Mensaje de Error
        try
        {
            Cadena = DateTime.Now.ToString() + " " + Proceso + ": " + Mensaje;
            // Obtiene Nombre de Fichero
            Fichero = Funcion.rutaAP + "\\Datos\\Errores\\";
            Fichero = Fichero + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            // Graba Registro
            Datos = new Funciones.StringList();
            //if (File.Exists(Fichero))
            //  Datos.LoadFromFile(Fichero);
            Datos.Add(Cadena);
            Datos.SaveToFile(Fichero);
        }
        finally
        {
            Funciones.Fugeneral.releaseObject(Datos);
        }
    }
    public static void Grabar_Proceso_Depuracion(string Proceso, string Mensaje)
    {
        // Declaración de Variables
        Funciones.StringList Datos = null;
        string Fichero = "";
        string Cadena = "";

        // Verifica Directorio DATOS
        Fichero = Funcion.rutaAP + "\\Datos\\Depuracion";
        if (!Directory.Exists(Fichero))
            Directory.CreateDirectory(Fichero);

        // Graba Mensaje de Error
        try
        {
            Cadena = DateTime.Now.ToString() + " " + Proceso + ": " + Mensaje;
            // Obtiene Nombre de Fichero
            Fichero = Funcion.rutaAP + "\\Datos\\Depuracion\\";
            Fichero = Fichero + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            // Graba Registro
            Datos = new Funciones.StringList();
            Datos.Add(Cadena);
            Datos.SaveToFile(Fichero);
        }
        finally
        {
            Funciones.Fugeneral.releaseObject(Datos);
        }
    }

    // ANTES: esta función forzaba GC.Collect() + SetProcessWorkingSetSize(-1,-1)
    // y se llamaba varias veces por segundo desde AperturaValvulaAire(). Eso
    // fuerza a Windows a "paginar" toda la memoria del proceso una y otra vez,
    // lo que con el tiempo (días) degrada gravemente el rendimiento y acaba
    // dejando el proceso prácticamente congelado. SetProcessWorkingSetSize se
    // ha eliminado por completo: el propio Garbage Collector de .NET gestiona
    // la memoria mucho mejor que este truco manual.
    //
    // Si en algún punto muy concreto necesitas forzar una recolección (por
    // ejemplo, una vez cada 10-15 minutos, nunca en un bucle rápido), usa
    // GC.Collect() a secas, sin SetProcessWorkingSetSize.
    // Escribe la hora actual en un fichero "heartbeat.txt". Una Tarea
    // Programada de Windows (u otro vigilante externo) puede comprobar la
    // fecha de modificación de este fichero: si lleva demasiado tiempo sin
    // actualizarse, es señal de que el programa se ha quedado colgado y toca
    // matarlo y relanzarlo. Ver instrucciones del watchdog.
    public static void Actualizar_Heartbeat()
    {
        try
        {
            string Fichero = Funcion.rutaAP + "\\Datos\\heartbeat.txt";
            string Directorio = Funcion.rutaAP + "\\Datos";
            if (!Directory.Exists(Directorio))
                Directory.CreateDirectory(Directorio);
            File.WriteAllText(Fichero, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        catch (Exception)
        {
            // No crítico: si falla, simplemente no se actualiza el heartbeat.
        }
    }

    public static void FlushMemory()
    {
        try
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        catch (Exception)
        { }
    }
    #endregion

    #region [ Funciones LECTOR AIRE ]
    private static readonly object _lockConexionTCP = new object();
    public static int OpenTCP()
    {
        lock (_lockConexionTCP)
        {
            // Conecta lector
            Funcion.lecFlexy = new KNet.BioNet();
            int error = Funcion.lecFlexy.OpenPortTCP(Funcion.datLectorAire.IP, 1001, 1001);
            //int error = Funcion.lecFlexy.OpenPortUDP(Funcion.datLectorAire.IP,5500,5501);

            if (error != 0)
            {
                //MessageBox.Show("Error " + error + " al Conectar Lector");
                if (Funcion.Depuracion) Funcion.Grabar_Proceso_Depuracion("-> OPEN TCP", " NO SE HA PODIDO ESTABLECER LA COMUNICACION");
                Funcion.Grabar_Error("OpenTCP: ", error.ToString());
                Funcion.Conectado = false;
                return error;
            }
            Funcion.lecFlexy.OnTrack += recibirDatos;
            Funcion.lecFlexy.SetUpFlexy();
            Funcion.lecFlexy.HotReset();
            Funcion.Conectado = true;
            if (Funcion.Depuracion) Funcion.Grabar_Proceso_Depuracion("-> OPEN TCP", " SE HA ABIERTO LA COMUNICACION CORRECTAMENTE");
            return error;
        }
    }
    public static int CloseTCP()
    {
        lock (_lockConexionTCP)
        {
            // Cada paso protegido por separado: un fallo en uno NO debe impedir
            // que se ejecuten los siguientes ni que se limpie el estado al final.
            try { Funcion.lecFlexy?.SwitchRelay(0, false); }
            catch (Exception ex) { Funcion.Grabar_Error("CloseTCP.SwitchRelay: ", ex.Message); }

            try { Funcion.lecFlexy?.HotReset(); }
            catch (Exception ex) { Funcion.Grabar_Error("CloseTCP.HotReset: ", ex.Message); }

            try { FControlAire.Inicializa_Aire(); }
            catch (Exception ex) { Funcion.Grabar_Error("CloseTCP.Inicializa_Aire: ", ex.Message); }

            int error = 0;
            try
            {
                error = Funcion.lecFlexy?.ClosePort() ?? -1;
            }
            catch (Exception ex)
            {
                Funcion.Grabar_Error("CloseTCP.ClosePort: ", ex.Message);
                error = -1;
            }

            // Pase lo que pase arriba, el estado SIEMPRE queda limpio y coherente.
            try { if (Funcion.lecFlexy != null) Funcion.lecFlexy.OnTrack -= recibirDatos; }
            catch { /* no crítico */ }

            Funcion.lecFlexy = null;
            Funcion.Conectado = false;  // <- ahora esto se ejecuta SIEMPRE, sin excepción posible que lo salte

            if (Funcion.Depuracion)
                Funcion.Grabar_Proceso_Depuracion("-> CLOSE TCP", error == 0
                    ? "SE HA CERRADO LA COMUNICACION CORRECTAMENTE"
                    : $"CIERRE CON INCIDENCIAS (error {error}), pero el estado ha quedado limpio para reconectar");

            return error;
        }
    }
    public static void recibirDatos(string sDatos)
    {
        // Todo el cuerpo se protege con try/catch: esto corre en el hilo del
        // lector RFID, sin interfaz de usuario delante. Un dato corrupto (por
        // ejemplo, ruido eléctrico durante un corte de luz) no debe poder
        // tirar abajo el proceso entero por una excepción sin capturar.
        try
        {
            //Este método va a estar activo mientras se encuentre el puerto TCP abierto, pero no va a entrar hasta que no se recoja algun dato
            //(es decir que no se acerque ningun llavero)
            if (Funcion.DDatos_LecAire.Cod_Cosechero == -1)
            {
                Funcion.DDatos_LecAire.Cod_Cosechero = 0;
            }



            if (Funcion.DDatos_LecAire.Cod_Cosechero == 0 && Funcion.DDatos_LecAire.Cosechero.Equals("") && Funcion.datLectorAire.Crono.ElapsedMilliseconds == 0)
            {
                string svalor = sDatos.Replace("*", "");
                long lvalor = Convert.ToInt64(svalor);
                string cadena = lvalor.ToString("X");
                cadena += hexCRC(cadena);
                while (cadena.Length < 11)
                    cadena = "0" + cadena;

                if (Funcion.Depuracion)
                {
                    Funcion.Grabar_Proceso_Depuracion("1. LECTURA LLAVERO", "CODIGO:" + cadena);
                }

                //Aqui se leería los datos del cosechero
                if (!FunDatos.Consultar_Cosechero_RFID_Aire(cadena, Funcion.baseDatos))
                {
                    //SE REALIZARÁ UN CAMBIO EN LA LUZ DEL LECTOR Y DP DE 10 SEGUNDOS Y ALGO DE bUZZER SE SALDRÁ SIN HACER NADA MAS
                    Funcion.lecFlexy.SwitchRelay(0, false);
                    if (Funcion.Depuracion)
                    {
                        Funcion.Grabar_Proceso_Depuracion("3.COSECHERO NO ENCONTRADO", "DESACTIVO RELÉ");
                    }
                    Funcion.lecFlexy.ActivateDigitalOutput(3, 10);
                    Funcion.lecFlexy.ActivateDigitalOutput(1, 25);
                    if (Funcion.Depuracion)
                    {
                        Funcion.Grabar_Proceso_Depuracion("4.COSECHERO NO ENCONTRADO", "SALIDA DIGITAL, LED EN ROJO Y BUZZ");
                    }

                }
                else
                {
                    Funcion.lecFlexy.SwitchRelay(0, true);
                    if (Funcion.Depuracion)
                    {
                        Funcion.Grabar_Proceso_Depuracion("3.COSECHERO ENCONTRADO", "ACTIVACION RELÉ");
                        //MessageBox.Show(" Cosechero dentro de la BD, mandada orden para encender relé");
                    }
                    Funcion.datLectorAire.Crono.Restart();
                    Funcion.DDatos_LecAire.Tiempo = Funcion.datLectorAire.Tiempo;
                    Funcion.DDatos_LecAire.Fecha = DateTime.Now.Date;
                    Funcion.DDatos_LecAire.Hora = DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss");
                    if (!FunDatos.Agregar_Cupo_Llenado_Aire(Funcion.baseDatos)) return;
                }
            }
        }
        catch (Exception ex)
        {
            Funcion.Grabar_Error("recibirDatos: ", ex.Message);
        }
    }
    public static string hexCRC(string svalor)
    {
        // Variables
        string CRC8 = string.Empty;
        int calculoCRC = 0;

        // Cálculo CRC8
        try
        {
            calculoCRC = 0;
            for (int Cy = 0; Cy < svalor.Length; Cy++)
            {
                calculoCRC = calculoCRC ^ byte.Parse(svalor[Cy].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
        }
        catch (Exception ex)
        {
            // Esto se ejecuta en el hilo del lector RFID, sin nadie delante de
            // la pantalla: un MessageBox aquí dejaría el programa "colgado"
            // esperando un clic que nunca llega. Se registra en el log y ya está.
            Funcion.Grabar_Error("hexCRC: ", ex.Message);
            return "";
        }

        // Correcto
        return calculoCRC.ToString("X");
    }
    #endregion


    }
}
