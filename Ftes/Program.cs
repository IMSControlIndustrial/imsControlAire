using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;

namespace imsControlAire
{
  static class Program
  {
    /// <summary>
    /// Punto de entrada principal para la aplicación.
    /// </summary>
    [STAThread]
        static void Main()
        {
            // Obtiene Ruta del Aplicativo LO PRIMERO DE TODO, para que
            // Funcion.Grabar_Error() funcione ya desde aquí (sustituye a los
            // MessageBox.Show bloqueantes de antes: en una máquina desatendida
            // tras un corte de luz, nadie va a estar para pulsar OK).
            Funcion.rutaAP = Application.StartupPath;

            // Captura cualquier excepción no controlada (tanto en el hilo de UI
            // como en cualquier otro hilo) para que, en vez de dejar que Windows
            // muestre su propio diálogo de "la aplicación ha dejado de
            // funcionar" (que también se queda esperando un clic), se registre
            // en el log y el proceso termine limpiamente. Combinado con el
            // watchdog externo (ver instrucciones), el programa se relanzará solo.
            Application.ThreadException += (sender, e) =>
            {
                Funcion.Grabar_Error("Application.ThreadException: ", e.Exception.ToString());
            };
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                try
                {
                    Exception ex = e.ExceptionObject as Exception;
                    Funcion.Grabar_Error("AppDomain.UnhandledException: ", ex != null ? ex.ToString() : "Excepción desconocida");
                }
                catch { /* no crítico */ }
            };

            // Verifica instancia única
            try
            {
                bool activo;
                string name = Assembly.GetEntryAssembly().FullName;
                Funcion.mutexProc = new System.Threading.Mutex(true, name, out activo);
                if (!activo)
                {
                    // Ya hay una instancia corriendo: se registra en el log y se sale
                    // sin diálogo (puede pasar sin que haya nadie delante, por
                    // ejemplo si el watchdog relanza el proceso mientras el anterior
                    // todavía está cerrando).
                    Funcion.Grabar_Error("Main", "PROGRAMA EN EJECUCION. PROCESO CANCELADO.");
                    Application.Exit();
                    return;
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                }
            }
            catch (Exception ex)
            {
                Funcion.Grabar_Error("Main (instancia única): ", ex.Message);
                Application.Exit();
                return;
            }

            // Lectura Monitor
            Funcion.Monitor = Funciones.LectorIni.Leer_Integer(Funcion.rutaAP, "Gestion.ini", "SISTEMA", "MONITOR");
            if (Funcion.Monitor <= 0)
                Funcion.Monitor = 1;

            Funcion.datLectorAire.IP = Funciones.LectorIni.Leer_String(Funcion.rutaAP, "Gestion.ini", "Lector_Aire", "IP");
            Funcion.datLectorAire.Puerto = Funciones.LectorIni.Leer_Integer(Funcion.rutaAP, "Gestion.ini", "Lector_Aire", "PUERTO");
            Funcion.datLectorAire.Tiempo = Funciones.LectorIni.Leer_Integer(Funcion.rutaAP, "Gestion.ini", "Lector_Aire", "TIEMPO");

            //BIT DE DEPURACION 
            Funcion.Depuracion = Funciones.LectorIni.Leer_Boolean(Funcion.rutaAP, "Gestion.ini", "SISTEMA", "DEPURACION");


            // Verifica Servidor Base de Datos
            Funcion.baseDatos = Funciones.LectorIni.Leer_String(Funcion.rutaAP, "Gestion.ini", "SERVIDOR", "NOMBRE");
            // Obtiene Ejercicio Actual
            Funcion.Ejercicio = Funciones.LectorIni.Leer_Integer(Funcion.rutaAP, "Gestion.ini", "SISTEMA", "EJERCICIO");
            if (Funcion.Ejercicio <= 0)
            {
                try
                {
                    Funcion.Ejercicio = DateTime.Now.Year;
                    // Antes: si esto fallaba (típico si arrancamos justo tras un
                    // corte de luz y la BD aún no ha levantado), se mostraba un
                    // MessageBox y se cerraba el programa para siempre. Ahora se
                    // reintenta con espera, dando tiempo a que la BD se recupere.
                    if (!Esperar_Y_Crear_BD(Funcion.Ejercicio))
                    {
                        Funcion.Grabar_Error("Main", "No se ha podido crear la Base de Datos tras varios reintentos. Se continúa igualmente: el programa seguirá reintentando en segundo plano.");
                    }
                    else
                    {
                        Funciones.LectorIni.Escribe_Integer(Funcion.rutaAP, "Gestion.ini", "SISTEMA", "EJERCICIO", Funcion.Ejercicio);
                    }
                }
                catch (Exception ex)
                {
                    Funcion.Grabar_Error("Main (Crear_BD inicial): ", ex.Message);
                }
            }

            // Carga Base de Datos
            Funcion.baseDatos = "imsLlenado_" + Funcion.Ejercicio.ToString("0000");
            if (!FunDatos.Existe_BD(Funcion.baseDatos))
            {
                // Igual que arriba: se reintenta con espera en vez de cerrar el
                // programa. Si tras los reintentos sigue sin poder crearse, se deja
                // que el programa arranque igualmente: btnConfiguracion y las
                // tareas en segundo plano seguirán reintentando la conexión sin
                // necesidad de reiniciar nada a mano.
                if (!Esperar_Y_Crear_BD(Funcion.Ejercicio))
                {
                    Funcion.Grabar_Error("Main", "La Base de Datos no está disponible tras varios reintentos. El programa arrancará igualmente y seguirá reintentando.");
                }
            }
            else
            {
                // Verifica Estructura BD
                SqlConnection Conn = new SqlConnection();
                FunDatos.Tabla_Memoria_Aire(ref Conn);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FControlAire());
        }

        // Reintenta Crear_BD varias veces con espera entre intentos, en vez de
        // rendirse a la primera. Pensado para el arranque justo después de un
        // corte de luz, cuando el servidor SQL puede tardar uno o dos minutos
        // en estar disponible tras reiniciarse.
        private static bool Esperar_Y_Crear_BD(int Ejercicio)
        {
            const int intentosMaximos = 6;      // 6 intentos...
            const int esperaEntreIntentosMs = 20000; // ...cada 20s => hasta 2 minutos de espera total

            for (int intento = 1; intento <= intentosMaximos; intento++)
            {
                try
                {
                    if (FunDatos.Crear_BD(Ejercicio))
                        return true;
                }
                catch (Exception ex)
                {
                    Funcion.Grabar_Error("Esperar_Y_Crear_BD", $"Intento {intento}/{intentosMaximos}: {ex.Message}");
                }

                if (intento < intentosMaximos)
                    Thread.Sleep(esperaEntreIntentosMs);
            }

            return false;
        }
    }
}
