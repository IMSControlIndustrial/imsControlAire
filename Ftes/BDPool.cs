using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace imsControlAire
{
  class BDPool
  {
    #region [ Variables ]
    private SqlConnectionStringBuilder CadenaBD = new SqlConnectionStringBuilder();

    private string _servidor;
    private string _usuario;
    private string _clave;
    private int _port;
    #endregion

    #region  [ Constructor ]
    public BDPool()
    {
      try
      {
        string RutaAP = Application.StartupPath;
        // Carga Datos de Conexión
        _servidor = Funciones.LectorIni.Leer_String(RutaAP, "Gestion.ini", "SERVIDOR", "NOMBRE");
        _usuario = Funciones.LectorIni.Leer_String(RutaAP, "Gestion.ini", "SERVIDOR", "USUARIO");
        _clave = Funciones.LectorIni.Leer_String(RutaAP, "Gestion.ini", "SERVIDOR", "CLAVE");
        _port = Funciones.LectorIni.Leer_Integer(RutaAP, "Gestion.ini", "SERVIDOR", "PUERTO");
        // Actualiza Parametros de Conexión
        CadenaBD.Pooling = true;
        CadenaBD.MinPoolSize = 1;
        CadenaBD.MaxPoolSize = 100;
        CadenaBD.ConnectTimeout = 6;
        CadenaBD.DataSource = _servidor + "," + _port.ToString();
        CadenaBD.UserID = _usuario;
        CadenaBD.Password = _clave;
        CadenaBD.InitialCatalog = "Master";
        CadenaBD.MultipleActiveResultSets = true;
      }
      catch (Exception ex)
      {
      }
    }
    #endregion

    #region [ Propiedades ]
    public string Servidor
    {
      get { return _servidor; }
      set
      {
        _servidor = value;
        CadenaBD.DataSource = _servidor + "," + _port.ToString();
      }
    }
    public string Usuario
    {
      get { return CadenaBD.UserID; }
      set { CadenaBD.UserID = value; }
    }
    public string Password
    {
      get { return CadenaBD.Password; }
      set { CadenaBD.Password = value; }
    }
    public int Puerto
    {
      get { return _port; }
      set
      {
        _port = value;
        CadenaBD.DataSource = _servidor + "," + _port.ToString();
      }
    }
    public string Database
    {
      get { return CadenaBD.InitialCatalog; }
      set { CadenaBD.InitialCatalog = value; }
    }
        #endregion

    #region [ Funciones ]
        public SqlConnection Open(string BaseDatos)
        {
            // IMPORTANTE: se crea una conexión LOCAL en cada llamada (no se guarda en
            // ningún campo estático compartido) para evitar que dos hilos que llamen
            // a Open() al mismo tiempo se "pisen" la conexión entre sí.
            SqlConnection Conn = null;
            try
            {
                // ConnectionStringBuilder no es thread-safe: lo protegemos con lock
                // para que dos hilos no lean/escriban InitialCatalog a la vez.
                string cadenaConexion;
                lock (CadenaBD)
                {
                    CadenaBD.InitialCatalog = BaseDatos.ToLower();
                    cadenaConexion = CadenaBD.ConnectionString;
                }
                Conn = new SqlConnection(cadenaConexion);
                Conn.Open();
            }
            catch (Exception ex)
            {
                // Fallo de conexión (típico durante un corte de luz): se registra en
                // el log y se devuelve la conexión tal cual (cerrada), NUNCA se
                // muestra un MessageBox aquí, para no colgar una máquina desatendida.
                Funcion.Grabar_Error("Abrir Conexion (Pool): ", ex.Message);
                if (Conn != null)
                {
                    try { Conn.Dispose(); } catch { /* no crítico */ }
                }
                Conn = new SqlConnection(); // conexión "vacía" en estado Closed
            }
            return Conn;
        }
        public void Close(SqlConnection Con)
        {
            if (Con == null) return;
            try
            {
                if (Con.State != ConnectionState.Closed)
                    Con.Close();
            }
            catch (Exception ex)
            {
                Funcion.Grabar_Error("Cerrar Conexion (Pool): ", ex.ToString());
            }
            finally
            {
                // Libera siempre los recursos de la conexión, se haya podido
                // cerrar "limpiamente" o no.
                try { Con.Dispose(); } catch { /* no crítico */ }
            }
        }
    #endregion


    }
}
