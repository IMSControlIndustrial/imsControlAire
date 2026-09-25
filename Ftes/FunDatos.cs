using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace imsControlAire
{
  class FunDatos
  {
    #region Variables Globales
        public static BDPool PoolBD = new BDPool();
    #endregion

    #region [ Estructura BD ]
        public static bool Crear_BD(int Ejercicio)
        {
            // Declaracion de Variables
            SqlConnection Conn = new SqlConnection();
            SqlTransaction Trans = null;
            SqlCommand Consulta = new SqlCommand();

            string Nombre = "";

            // Carga Nombre BD
            Nombre = "imsLlenado_" + Ejercicio;

            // Comprueba Base de Datos
            if (!Existe_BD(Nombre))
            {
                // Crea Base de datos
                try
                {
                    Conn = PoolBD.Open("master");
                    if (Conn.State == ConnectionState.Closed)
                        return false;
                    // -----> Ejecuta Consulta
                    Consulta.Connection = Conn;
                    Consulta.CommandType = CommandType.Text;
                    Consulta.CommandText = "CREATE DATABASE \"" + Nombre + "\"";
                    Consulta.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Sin MessageBox: en una máquina desatendida nadie va a
                    // pulsar OK. Se registra en el log y se continúa.
                    Funcion.Grabar_Error("Crear_BD (CREATE DATABASE): ", ex.Message);
                    return false;
                }
                finally
                {
                    // Libera Conexión
                    PoolBD.Close(Conn);
                    // Libera Memoria
                    Funciones.Fugeneral.releaseObject(Conn);
                    Funciones.Fugeneral.releaseObject(Consulta);
                }

            }

            // CREA TABLAS
            try
            {
                // -----> Conecta Base de Datos
                Conn = PoolBD.Open(Nombre);
                if (Conn.State == ConnectionState.Closed)
                    return false;
                // -----> Inicia Transaccón
                Trans = Conn.BeginTransaction();
                // -----> Crea Tabla Datos Almazara
                if (!Crear_Tabla_Falmazara(Conn, Trans))
                    return false;
                // -----> Crea Tabla Cosecheros
                if (!Crear_Tabla_Fcosechero(Conn, Trans))
                    return false;
                // -----> Crea Tabla Tag RFID
                if (!Crear_Tabla_Ftarjeta(Conn, Trans))
                    return false;
                // -----> Crea Tabla Cupos Agua
                if (!Crear_Tabla_Fcupos(Conn, Trans))
                    return false;
                // -----> Crea Tabla Registros de Llenado
                if (!Crear_Tabla_Fllenado(Conn, Trans))
                    return false;
                // -----> Crea Tabla Registros de Memoria Llenado
                if (!Crear_Tabla_Memoria_Llenado(Conn, Trans))
                    return false;
                // -----> Crea Tabla Registros de Recarga Cupos
                if (!Crear_Tabla_Recargar_Cupos(Conn, Trans))
                    return false;
                // -----> Crea Tabla Registros de Recarga Cupos
                if (!Crear_Tabla_Memoria_Aire(Conn, Trans))
                    return false;
                // Finaliza Transaccion
                Trans.Commit();
            }
            catch (Exception ex)
            {
                // Cancela Transaccion (protegido: si la conexión ya se cayó,
                // Rollback() puede lanzar una segunda excepción; no dejar que
                // eso escape sin controlar).
                if (Trans != null)
                {
                    try { Trans.Rollback(); }
                    catch (Exception exRollback) { Funcion.Grabar_Error("Crear_BD (Rollback): ", exRollback.Message); }
                }
                Funcion.Grabar_Error("Crear_BD (Crear Tablas): ", ex.Message);
                return false;
            }
            finally
            {
                // Libera Conexión
                PoolBD.Close(Conn);
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Conn);
            }

            // Fin
            return true;
        }
        public static bool Crear_Tabla_Falmazara(SqlConnection Conn, SqlTransaction Trans)
        {
            // Declaración de Variables
            SqlCommand Consulta = new SqlCommand();
            int ivalor = 0;

            try
            {
                // Verifica Conexión
                if (Conn.State == ConnectionState.Closed)
                    return false;
                // Actualiza Tabla
                // ----------> Comando
                Consulta.Connection = Conn;
                Consulta.Transaction = Trans;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'falmazara'";
                ivalor = Convert.ToInt16(Consulta.ExecuteScalar().ToString());
                if (ivalor <= 0)
                {
                    Consulta.Connection = Conn;
                    Consulta.CommandType = CommandType.Text;
                    Consulta.CommandText = "CREATE TABLE falmazara (" +
                                                                "ALMAZARA varchar(50) DEFAULT ''," +
                                                                "DIRECCION varchar(50) DEFAULT ''," +
                                                                "CPOSTAL varchar(5) DEFAULT ''," +
                                                                "POBLACION varchar(40) DEFAULT ''," +
                                                                "PROVINCIA varchar(30) DEFAULT ''," +
                                                                "NUMCIF varchar(12) DEFAULT ''" +
                                                                ")";
                    Consulta.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Cancela Transacción
                if (Trans != null)
                {
                    try { Trans.Rollback(); }
                    catch (Exception exRollback) { Funcion.Grabar_Error("Crear_Tabla_Falmazara (Rollback): ", exRollback.Message); }
                }
                // Registra el error en el log (nunca MessageBox: máquina desatendida)
                Funcion.Grabar_Error("Crear_Tabla_Falmazara: ", ex.Message);
                return false;
            }
            finally
            {
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Consulta);
            }

            // Correcto
            return true;
        }
        public static bool Crear_Tabla_Fcosechero(SqlConnection Conn, SqlTransaction Trans)
        {
            // Declaración de Variables
            SqlCommand Consulta = new SqlCommand();
            int ivalor = 0;

            // Abre Ficheros
            try
            {
                // Verifica Conexión
                if (Conn.State == ConnectionState.Closed)
                    return false;
                // Actualiza Tabla Fincas
                // ----------> Comando
                Consulta.Connection = Conn;
                Consulta.Transaction = Trans;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'fcosechero'";
                ivalor = Convert.ToInt16(Consulta.ExecuteScalar().ToString());
                if (ivalor <= 0)
                {
                    Consulta.CommandText = "CREATE TABLE fcosechero (" +
                                                                    "CODIGO int NOT NULL DEFAULT 0," +
                                                                    "NOMBRE varchar(50) DEFAULT ''," +
                                                                    "DIRECCION varchar(50) DEFAULT ''," +
                                                                    "CPOSTAL varchar(5) DEFAULT ''," +
                                                                    "POBLACION varchar(35) DEFAULT ''," +
                                                                    "PROVINCIA varchar(25) DEFAULT ''," +
                                                                    "NUMDNI varchar(12) DEFAULT ''," +
                                                                    "IDENTIFICA varchar(10) DEFAULT ''," +
                                                                    "PRIMARY KEY (CODIGO)" +
                                                                    ")";
                    Consulta.ExecuteNonQuery();
                }
            }
            catch (Exception exp)
            {
                // Cancela Transacción
                if (Trans != null)
                {
                    try { Trans.Rollback(); }
                    catch (Exception exRollback) { Funcion.Grabar_Error("Crear_Tabla_Fcosechero (Rollback): ", exRollback.Message); }
                }
                Funcion.Grabar_Error("Crear_Tabla_Fcosechero: ", exp.Message);
                return false;
            }
            finally
            {
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Consulta);
            }

            // Correcto
            return true;
        }
        public static bool Crear_Tabla_Ftarjeta(SqlConnection Conn, SqlTransaction Trans)
        {
            // Declaración de Variables
            SqlCommand Consulta = new SqlCommand();
            int ivalor = 0;

            // Abre Ficheros
            try
            {
                // Verifica Conexión
                if (Conn.State == ConnectionState.Closed)
                    return false;
                // Actualiza Tabla Fincas
                // ----------> Comando
                Consulta.Connection = Conn;
                Consulta.Transaction = Trans;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'ftarjeta'";
                ivalor = Convert.ToInt16(Consulta.ExecuteScalar().ToString());
                if (ivalor <= 0)
                {
                    Consulta.CommandText = "CREATE TABLE ftarjeta (" +
                                                                    "COSECHERO int NOT NULL DEFAULT '0'," +
                                                                    "TAG_RFID varchar(20) DEFAULT ''," +
                                                                    "PRIMARY KEY (COSECHERO, TAG_RFID)" +
                                                                    ")";
                    Consulta.ExecuteNonQuery();
                }
            }
            catch (Exception exp)
            {
                // Cancela Transacción
                if (Trans != null)
                {
                    try { Trans.Rollback(); }
                    catch (Exception exRollback) { Funcion.Grabar_Error("Crear_Tabla_Ftarjeta (Rollback): ", exRollback.Message); }
                }
                Funcion.Grabar_Error("Crear_Tabla_Ftarjeta: ", exp.Message);
                return false;
            }
            finally
            {
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Consulta);
            }

            // Correcto
            return true;
        }
        public static bool Crear_Tabla_Fcupos(SqlConnection Conn, SqlTransaction Trans)
        {
            // Declaración de Variables
            SqlCommand Consulta = new SqlCommand();
            int ivalor = 0;

            // Abre Ficheros
            try
            {
                // Verifica Conexión
                if (Conn.State == ConnectionState.Closed)
                    return false;
                // Actualiza Tabla Fincas
                // ----------> Comando
                Consulta.Connection = Conn;
                Consulta.Transaction = Trans;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'fcupos'";
                ivalor = Convert.ToInt16(Consulta.ExecuteScalar().ToString());
                if (ivalor <= 0)
                {
                    Consulta.CommandText = "CREATE TABLE fcupos (" +
                                                                    "COSECHERO int NOT NULL DEFAULT 0," +
                                                                    "CUPO int DEFAULT 0," +
                                                                    "UTILIZADO int DEFAULT 0," +
                                                                    "MARGEN smallint DEFAULT 0" +
                                                                    "PRIMARY KEY (COSECHERO)" +
                                                                    ")";
                    Consulta.ExecuteNonQuery();
                }
            }
            catch (Exception exp)
            {
                // Cancela Transacción
                if (Trans != null)
                {
                    try { Trans.Rollback(); }
                    catch (Exception exRollback) { Funcion.Grabar_Error("Crear_Tabla_Fcupos (Rollback): ", exRollback.Message); }
                }
                Funcion.Grabar_Error("Crear_Tabla_Fcupos: ", exp.Message);
                return false;
            }
            finally
            {
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Consulta);
            }

            // Correcto
            return true;
        }
        public static bool Crear_Tabla_Fllenado(SqlConnection Conn, SqlTransaction Trans)
        {
            // Declaración de Variables
            SqlCommand Consulta = new SqlCommand();
            int ivalor = 0;

            // Abre Ficheros
            try
            {
                // Verifica Conexión
                if (Conn.State == ConnectionState.Closed)
                    return false;
                // Actualiza Tabla Fincas
                // ----------> Comando
                Consulta.Connection = Conn;
                Consulta.Transaction = Trans;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'fllenado'";
                ivalor = Convert.ToInt16(Consulta.ExecuteScalar().ToString());
                if (ivalor <= 0)
                {
                    Consulta.CommandText = "CREATE TABLE fllenado (" +
                                                                 "ID bigint IDENTITY," +
                                                                    "COSECHERO int NOT NULL DEFAULT 0," +
                                                                 "FECHA varchar(10) DEFAULT '000-00-00'," +
                                                                 "INICIO varchar(8) DEFAULT '00:00:00'," +
                                                                 "FINAL varchar(8) DEFAULT '00:00:00'," +
                                                                 "CUPO int DEFAULT 0," +
                                                                 "UTILIZADO int DEFAULT 0," +
                                                                 "SEGUNDOS int DEFAULT 0," +
                                                                 "LITROS int DEFAULT 0," +
                                                                 "PULSOS int DEFAULT 0," +
                                                                 "PRIMARY KEY (ID)" +
                                                                    ")";
                    Consulta.ExecuteNonQuery();
                }
            }
            catch (Exception exp)
            {
                // Cancela Transacción
                if (Trans != null)
                {
                    try { Trans.Rollback(); }
                    catch (Exception exRollback) { Funcion.Grabar_Error("Crear_Tabla_Fllenado (Rollback): ", exRollback.Message); }
                }
                Funcion.Grabar_Error("Crear_Tabla_Fllenado: ", exp.Message);
                return false;
            }
            finally
            {
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Consulta);

            }

            // Correcto
            return true;
        }
        public static bool Crear_Tabla_Memoria_Llenado(SqlConnection Conn, SqlTransaction Trans)
        {
            SqlCommand Consulta = new SqlCommand();
            int ivalor;
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    return false;
                Consulta.Connection = Conn;
                Consulta.Transaction = Trans;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'fmemoriallenado'";
                ivalor = Convert.ToInt16(Consulta.ExecuteScalar().ToString());
                if (ivalor <= 0)
                {
                    Consulta.CommandText = "CREATE TABLE fmemoriallenado (" +
                                                                 "CODIGO int NOT NULL DEFAULT 0," +
                                                                 "NOMBRE varchar(20) DEFAULT ''," +
                                                                 "FECHA varchar(10) DEFAULT '0000-00-00'," +
                                                                 "HINICIO varchar(8) DEFAULT '00:00:00'," +
                                                                 "HFINAL varchar(8) DEFAULT '00:00:00'," +
                                                                 "SELECCION int DEFAULT 0," +
                                                                 "LITROS int DEFAULT 0" +
                                                                    ")";
                    Consulta.ExecuteNonQuery();
                }
            }
            catch (Exception Exp)
            {
                // Cancela Transacción
                if (Trans != null)
                {
                    try { Trans.Rollback(); }
                    catch (Exception exRollback) { Funcion.Grabar_Error("Crear_Tabla_Memoria_Llenado (Rollback): ", exRollback.Message); }
                }
                Funcion.Grabar_Error("Crear_Tabla_Memoria_Llenado: ", Exp.Message);
                return false;
            }
            finally
            {
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Consulta);

            }


            return true;
        }
        public static bool Crear_Tabla_Recargar_Cupos(SqlConnection Conn, SqlTransaction Trans)
        {
            SqlCommand Consulta = new SqlCommand();
            int ivalor;
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    return false;
                Consulta.Connection = Conn;
                Consulta.Transaction = Trans;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'frecargacupos'";
                ivalor = Convert.ToInt16(Consulta.ExecuteScalar().ToString());
                if (ivalor <= 0)
                {
                    Consulta.CommandText = "CREATE TABLE frecargacupos (" +
                                                                 "ID int IDENTITY," +
                                                                 "CODIGO int NOT NULL DEFAULT 0," +
                                                                 "NOMBRE varchar(50) DEFAULT ''," +
                                                                 "FECHA date DEFAULT '0000-00-00'," +
                                                                 "IMPORTE decimal (6,4) DEFAULT 0.00," +
                                                                 "LITROS decimal (6,4) DEFAULT 0.00" +
                                                                    ")";
                    Consulta.ExecuteNonQuery();
                }
            }
            catch (Exception Exp)
            {
                // Cancela Transacción
                if (Trans != null)
                {
                    try { Trans.Rollback(); }
                    catch (Exception exRollback) { Funcion.Grabar_Error("Crear_Tabla_Recargar_Cupos (Rollback): ", exRollback.Message); }
                }
                Funcion.Grabar_Error("Crear_Tabla_Recargar_Cupos: ", Exp.Message);
                return false;
            }
            finally
            {
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Consulta);

            }


            return true;
        }
        public static bool Crear_Tabla_Memoria_Aire(SqlConnection Conn, SqlTransaction Trans)
        {
            SqlCommand Consulta = new SqlCommand();
            int ivalor;
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    return false;
                Consulta.Connection = Conn;
                Consulta.Transaction = Trans;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'fmemoriaAire'";
                ivalor = Convert.ToInt16(Consulta.ExecuteScalar().ToString());
                if (ivalor <= 0)
                {
                    Consulta.CommandText = "CREATE TABLE fmemoriaAire (" +
                                                                 "COD int NOT NULL DEFAULT 0," +
                                                                 "COSECHERO varchar(50) DEFAULT ''," +
                                                                 "FECHA varchar(10) DEFAULT '0000-00-00'," +
                                                                 "HORA varchar(8) DEFAULT '00:00:00'," +
                                                                 "TIEMPO int DEFAULT '0'," +
                                                                 "TAG varchar(20) DEFAULT ''," +
                                                                    ")";
                    Consulta.ExecuteNonQuery();
                }
            }
            catch (Exception Exp)
            {
                // Cancela Transacción
                if (Trans != null)
                {
                    try { Trans.Rollback(); }
                    catch (Exception exRollback) { Funcion.Grabar_Error("Crear_Tabla_Memoria_Aire (Rollback): ", exRollback.Message); }
                }
                Funcion.Grabar_Error("Crear_Tabla_Memoria_Aire: ", Exp.Message);
                return false;
            }
            finally
            {
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Consulta);

            }


            return true;

        }
        public static void Tabla_Memoria_Aire(ref SqlConnection Conn)
        {
            // Variables
            SqlTransaction Trans = null;
            SqlCommand Consulta = new SqlCommand();
            SqlDataAdapter Dconsulta = new SqlDataAdapter();
            DataTable Tconsulta = new DataTable();
            DataRow[] Filas = null;

            // Abre Ficheros
            try
            {
                // Verifica Conexión
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn = PoolBD.Open(Funcion.baseDatos);
                    if (Conn.State == ConnectionState.Closed)
                        return;
                }
                // Consulta si existe registro creado
                // -----> Comando
                Consulta.Connection = Conn;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "SELECT * FROM information_schema.columns WHERE table_name = 'fmemoriaAire'";
                // ----------> Adaptador
                Dconsulta.MissingSchemaAction = MissingSchemaAction.Add;
                Dconsulta.SelectCommand = Consulta;
                // ----------> Tabla
                Tconsulta.Rows.Clear();
                Dconsulta.Fill(Tconsulta);
                if (Tconsulta.Rows.Count == 0)
                {
                    Crear_Tabla_Memoria_Aire(Conn, Trans);
                }
                Consulta.CommandText = "SELECT * FROM information_schema.columns WHERE table_name = 'fmemoriaAire'";
                // ----------> Adaptador
                Dconsulta.MissingSchemaAction = MissingSchemaAction.Add;
                Dconsulta.SelectCommand = Consulta;
                // ----------> Tabla
                Tconsulta.Rows.Clear();
                Dconsulta.Fill(Tconsulta);
                // Verifica Campos
                // -----> PETICION DE COD
                Filas = Tconsulta.Select("column_name='COD'");
                if (Filas.Length <= 0)
                {
                    try
                    {
                        Trans = Conn.BeginTransaction();
                        Consulta.Transaction = Trans;
                        Consulta.CommandText = "ALTER TABLE fmemoriaAire ADD COD int NOT NULL DEFAULT 0";
                        Consulta.ExecuteNonQuery();
                        Trans.Commit();
                    }
                    catch (SqlException ex)
                    {
                        try { Trans.Rollback(); }
                        catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ADD): ", exRollback.Message); }
                        Funcion.Grabar_Error("Tabla_Memoria_Aire (ADD): ", ex.ErrorCode.ToString() + " - " + ex.Message);
                    }
                }
                else
                {

                    if (Convert.ToString(Filas[0]["data_type"]).IndexOf("int") < 0)
                    {
                        try
                        {
                            Trans = Conn.BeginTransaction();
                            Consulta.Transaction = Trans;
                            Consulta.CommandText = "IF OBJECT_ID('dbo.[fmemoriaAire_COD]', 'C') IS NOT NULL ALTER TABLE fmemoriaAire DROP CONSTRAINT fmemoriaAire_COD";
                            Consulta.ExecuteNonQuery();
                            Consulta.CommandText = "ALTER TABLE fmemoriaAire ALTER COLUMN COD int";
                            Consulta.ExecuteNonQuery();
                            Trans.Commit();
                        }
                        catch (SqlException exAlter)
                        {
                            try { Trans.Rollback(); }
                            catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ALTER): ", exRollback.Message); }
                            Funcion.Grabar_Error("Tabla_Memoria_Aire (ALTER COLUMN): ", exAlter.Message);
                        }
                    }
                }

                // Verifica Campos
                // -----> PETICION DE COSECHERO
                Filas = Tconsulta.Select("column_name='COSECHERO'");
                if (Filas.Length <= 0)
                {
                    try
                    {
                        Trans = Conn.BeginTransaction();
                        Consulta.Transaction = Trans;
                        Consulta.CommandText = "ALTER TABLE fmemoriaAire ADD COSECHERO VARCHAR(50) NULL DEFAULT '' ";
                        Consulta.ExecuteNonQuery();
                        Trans.Commit();
                    }
                    catch (SqlException ex)
                    {
                        try { Trans.Rollback(); }
                        catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ADD): ", exRollback.Message); }
                        Funcion.Grabar_Error("Tabla_Memoria_Aire (ADD): ", ex.ErrorCode.ToString() + " - " + ex.Message);
                    }
                }
                else
                {

                    if (Convert.ToString(Filas[0]["data_type"]).IndexOf("varchar(50)") < 0)
                    {
                        try
                        {
                            Trans = Conn.BeginTransaction();
                            Consulta.Transaction = Trans;
                            Consulta.CommandText = "IF OBJECT_ID('dbo.[fmemoriaAire_COSECHERO]', 'C') IS NOT NULL ALTER TABLE fmemoriaAire DROP CONSTRAINT fmemoriaAire_COSECHERO";
                            Consulta.ExecuteNonQuery();
                            Consulta.CommandText = "ALTER TABLE fmemoriaAire ALTER COLUMN COSECHERO varchar(50)";
                            Consulta.ExecuteNonQuery();
                            Trans.Commit();
                        }
                        catch (SqlException exAlter)
                        {
                            try { Trans.Rollback(); }
                            catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ALTER): ", exRollback.Message); }
                            Funcion.Grabar_Error("Tabla_Memoria_Aire (ALTER COLUMN): ", exAlter.Message);
                        }
                    }
                }

                // Verifica Campos
                // -----> PETICION DE FECHA
                Filas = Tconsulta.Select("column_name='FECHA'");
                if (Filas.Length <= 0)
                {
                    try
                    {
                        Trans = Conn.BeginTransaction();
                        Consulta.Transaction = Trans;
                        Consulta.CommandText = "ALTER TABLE fmemoriaAire ADD FECHA varchar(10) DEFAULT '0000-00-00'";
                        Consulta.ExecuteNonQuery();
                        Trans.Commit();
                    }
                    catch (SqlException ex)
                    {
                        try { Trans.Rollback(); }
                        catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ADD): ", exRollback.Message); }
                        Funcion.Grabar_Error("Tabla_Memoria_Aire (ADD): ", ex.ErrorCode.ToString() + " - " + ex.Message);
                    }
                }
                else
                {

                    if (Convert.ToString(Filas[0]["data_type"]).IndexOf("varchar (10)") < 0)
                    {
                        try
                        {
                            Trans = Conn.BeginTransaction();
                            Consulta.Transaction = Trans;
                            Consulta.CommandText = "IF OBJECT_ID('dbo.[fmemoriaAire_FECHA]', 'C') IS NOT NULL ALTER TABLE fmemoriaAire DROP CONSTRAINT fmemoriaAire_FECHA";
                            Consulta.ExecuteNonQuery();
                            Consulta.CommandText = "ALTER TABLE fmemoriaAire ALTER COLUMN FECHA varchar(10)";
                            Consulta.ExecuteNonQuery();
                            Trans.Commit();
                        }
                        catch (SqlException exAlter)
                        {
                            try { Trans.Rollback(); }
                            catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ALTER): ", exRollback.Message); }
                            Funcion.Grabar_Error("Tabla_Memoria_Aire (ALTER COLUMN): ", exAlter.Message);
                        }
                    }
                }

                // Verifica Campos
                // -----> PETICION DE HORA
                Filas = Tconsulta.Select("column_name='HORA'");
                if (Filas.Length <= 0)
                {
                    try
                    {
                        Trans = Conn.BeginTransaction();
                        Consulta.Transaction = Trans;
                        Consulta.CommandText = "ALTER TABLE fmemoriaAire ADD HORA varchar(10) NULL DEFAULT '00:00:00'";
                        Consulta.ExecuteNonQuery();
                        Trans.Commit();
                    }
                    catch (SqlException ex)
                    {
                        try { Trans.Rollback(); }
                        catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ADD): ", exRollback.Message); }
                        Funcion.Grabar_Error("Tabla_Memoria_Aire (ADD): ", ex.ErrorCode.ToString() + " - " + ex.Message);
                    }
                }
                else
                {

                    if (Convert.ToString(Filas[0]["data_type"]).IndexOf("varchar(10)") < 0)
                    {
                        try
                        {
                            Trans = Conn.BeginTransaction();
                            Consulta.Transaction = Trans;
                            Consulta.CommandText = "IF OBJECT_ID('dbo.[fmemoriaAire_HORA]', 'C') IS NOT NULL ALTER TABLE fmemoriaAire DROP CONSTRAINT fmemoriaAire_HORA";
                            Consulta.ExecuteNonQuery();
                            Consulta.CommandText = "ALTER TABLE fmemoriaAire ALTER COLUMN HORA varchar(10)";
                            Consulta.ExecuteNonQuery();
                            Trans.Commit();
                        }
                        catch (SqlException exAlter)
                        {
                            try { Trans.Rollback(); }
                            catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ALTER): ", exRollback.Message); }
                            Funcion.Grabar_Error("Tabla_Memoria_Aire (ALTER COLUMN): ", exAlter.Message);
                        }
                    }
                }
                // -----> PETICION DE TIEMPO
                Filas = Tconsulta.Select("column_name='TIEMPO'");
                if (Filas.Length <= 0)
                {
                    try
                    {
                        Trans = Conn.BeginTransaction();
                        Consulta.Transaction = Trans;
                        Consulta.CommandText = "ALTER TABLE fmemoriaAire ADD TIEMPO int NOT NULL DEFAULT 0";
                        Consulta.ExecuteNonQuery();
                        Trans.Commit();
                    }
                    catch (SqlException ex)
                    {
                        try { Trans.Rollback(); }
                        catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ADD): ", exRollback.Message); }
                        Funcion.Grabar_Error("Tabla_Memoria_Aire (ADD): ", ex.ErrorCode.ToString() + " - " + ex.Message);
                    }
                }
                else
                {

                    if (Convert.ToString(Filas[0]["data_type"]).IndexOf("int") < 0)
                    {
                        try
                        {
                            Trans = Conn.BeginTransaction();
                            Consulta.Transaction = Trans;
                            Consulta.CommandText = "IF OBJECT_ID('dbo.[fmemoriaAire_TIEMPO]', 'C') IS NOT NULL ALTER TABLE fmemoriaAire DROP CONSTRAINT fmemoriaAire_TIEMPO";
                            Consulta.ExecuteNonQuery();
                            Consulta.CommandText = "ALTER TABLE fmemoriaAire ALTER COLUMN TIEMPO int";
                            Consulta.ExecuteNonQuery();
                            Trans.Commit();
                        }
                        catch (SqlException exAlter)
                        {
                            try { Trans.Rollback(); }
                            catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ALTER): ", exRollback.Message); }
                            Funcion.Grabar_Error("Tabla_Memoria_Aire (ALTER COLUMN): ", exAlter.Message);
                        }
                    }
                }

                // -----> PETICION DE TAG
                Filas = Tconsulta.Select("column_name='TAG'");
                if (Filas.Length <= 0)
                {
                    try
                    {
                        Trans = Conn.BeginTransaction();
                        Consulta.Transaction = Trans;
                        Consulta.CommandText = "ALTER TABLE fmemoriaAire ADD TAG VARCHAR(20) NULL DEFAULT '' ";
                        Consulta.ExecuteNonQuery();
                        Trans.Commit();
                    }
                    catch (SqlException ex)
                    {
                        try { Trans.Rollback(); }
                        catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ADD): ", exRollback.Message); }
                        Funcion.Grabar_Error("Tabla_Memoria_Aire (ADD): ", ex.ErrorCode.ToString() + " - " + ex.Message);
                    }
                }
                else
                {

                    if (Convert.ToString(Filas[0]["data_type"]).IndexOf("varchar(20)") < 0)
                    {
                        try
                        {
                            Trans = Conn.BeginTransaction();
                            Consulta.Transaction = Trans;
                            Consulta.CommandText = "IF OBJECT_ID('dbo.[fmemoriaAire_TAG]', 'C') IS NOT NULL ALTER TABLE fmemoriaAire DROP CONSTRAINT fmemoriaAire_TAG";
                            Consulta.ExecuteNonQuery();
                            Consulta.CommandText = "ALTER TABLE fmemoriaAire ALTER COLUMN TAG varchar(20)";
                            Consulta.ExecuteNonQuery();
                            Trans.Commit();
                        }
                        catch (SqlException exAlter)
                        {
                            try { Trans.Rollback(); }
                            catch (Exception exRollback) { Funcion.Grabar_Error("Tabla_Memoria_Aire (Rollback ALTER): ", exRollback.Message); }
                            Funcion.Grabar_Error("Tabla_Memoria_Aire (ALTER COLUMN): ", exAlter.Message);
                        }
                    }
                }


            }
            catch (Exception exp)
            {
                Funcion.Grabar_Error("Tabla_Memoria_Aire: ", exp.Message);
                return;
            }
            finally
            {
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Consulta);
                Funciones.Fugeneral.releaseObject(Dconsulta);
                Funciones.Fugeneral.releaseObject(Tconsulta);
                Funciones.Fugeneral.releaseObject(Filas);
            }
            // Abrir Ficheros Correcto
            return;
        }

    #endregion

    #region [ Base de Datos ]
        public static bool Existe_BD(string Nombre)
        {
            // Declaracion de Variables
            SqlConnection Conn = new SqlConnection();
            SqlCommand Consulta = new SqlCommand();
            SqlDataAdapter Dconsulta = new SqlDataAdapter();
            DataTable Tconsulta = new DataTable();

            // Verifica Base de Datos
            try
            {
                Conn = PoolBD.Open("master");
                if (Conn.State == ConnectionState.Closed)
                    return false;
                // Ejecuta Consulta
                // --------> Comando
                Consulta.Connection = Conn;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "SELECT name from sysdatabases WHERE name LIKE " + Funciones.Fugeneral.QuotedStr(Nombre.ToLower());
                // --------> Adaptador
                Dconsulta.MissingSchemaAction = MissingSchemaAction.Add;
                Dconsulta.SelectCommand = Consulta;
                // --------> Tabla
                Tconsulta.Clear();
                Dconsulta.Fill(Tconsulta);
                // -----> Busca Base de Datos
                if (Tconsulta.Rows.Count <= 0)
                    return false;
            }
            catch (Exception ex)
            {
                Funcion.Grabar_Error("Existe_BD: ", ex.Message);
                return false;
            }
            finally
            {
                // Librera Conexion
                PoolBD.Close(Conn);
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Conn);
                Funciones.Fugeneral.releaseObject(Tconsulta);
                Funciones.Fugeneral.releaseObject(Dconsulta);
                Funciones.Fugeneral.releaseObject(Consulta);
            }

            // Correcto
            return true;
        }

        // -----> Tabla COSECHEROS 
        public static bool Consultar_Cosechero_RFID_Aire(string Id_Rfid, string BD)
        {
            // Declaración de Variables
            SqlConnection Conn = new SqlConnection();
            SqlCommand Consulta = new SqlCommand();
            SqlDataAdapter daConsulta = new SqlDataAdapter();
            DataTable Tconsulta = new DataTable();

            // Busca Cosechero
            try
            {
                // ----------> Abre Base de Datos
                try
                {
                    Conn = PoolBD.Open(BD);
                    if (Conn.State == ConnectionState.Closed)
                        return false;
                }
                catch (Exception ex)
                {
                    if (Funcion.Depuracion) Funcion.Grabar_Proceso_Depuracion("0.Consultar_Cosechero_RFID (Abrir BD)", ex.Message);
                    Funcion.Grabar_Error("Consultar_Cosechero_RFID (Abrir Base Datos): ", ex.Message);
                    // Antes seguía ejecutando con una conexión potencialmente
                    // inválida: ahora se corta aquí para evitar una segunda
                    // excepción en cascada.
                    return false;
                }
                // Inicializa Consulta
                Consulta.Connection = Conn;
                Consulta.CommandType = CommandType.Text;
                // Busca Código Cosechero
                Consulta.CommandText = "SELECT ftarjeta.COSECHERO,fcosechero.NOMBRE FROM ftarjeta INNER JOIN fcosechero ON fcosechero.CODIGO=ftarjeta.COSECHERO " +
                                                             "WHERE TAG_RFID=" + Funciones.Fugeneral.QuotedStr(Id_Rfid);
                // ----------> Adaptador
                daConsulta.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                daConsulta.SelectCommand = Consulta;
                // ----------> Tabla
                Tconsulta.Rows.Clear();
                daConsulta.Fill(Tconsulta);
                if (Tconsulta.Rows.Count <= 0)
                {
                    if (Funcion.Depuracion) Funcion.Grabar_Proceso_Depuracion("2..CONSULTAR COSECHERO CON RFID ", "NO SE HA ENCONTRADO COSECHERO");

                    Funcion.DDatos_LecAire.Cod_Cosechero = -1;
                    Funcion.DDatos_LecAire.Cosechero = "";
                    return false;
                }
                else
                {
                    Funcion.DDatos_LecAire.Cod_Cosechero = Tconsulta.Rows[0].IsNull("COSECHERO") ? 0 : Tconsulta.Rows[0].Field<int>("COSECHERO");
                    Funcion.DDatos_LecAire.Cosechero = Tconsulta.Rows[0].IsNull("NOMBRE") ? "" : Tconsulta.Rows[0].Field<string>("NOMBRE");
                    Funcion.DDatos_LecAire.Tag = Id_Rfid;
                    if (Funcion.Depuracion) Funcion.Grabar_Proceso_Depuracion("2.CONSULTAR COSECHERO CON RFID", "COSECHERO ENCONTRADO CORRECTO");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Funcion.Grabar_Error("Consultar Cosechero RFID de Lector de Aire: ", ex.Message);
                Funcion.DDatos_LecAire.Cod_Cosechero = -1;
                Funcion.DDatos_LecAire.Cosechero = "";
                return false;
            }
            finally
            {
                // Libera Conexión
                PoolBD.Close(Conn);
                // Libera Memoria
                Funciones.Fugeneral.releaseObject(Conn);
                Funciones.Fugeneral.releaseObject(Consulta);
                Funciones.Fugeneral.releaseObject(daConsulta);
                Funciones.Fugeneral.releaseObject(Tconsulta);
            }
        }

        //-------> Tabla Recarga Aire
        public static bool Agregar_Cupo_Llenado_Aire(string BD)
        {
            // Variables
            SqlConnection Conn = new SqlConnection();
            SqlCommand Consulta = new SqlCommand();

            try
            {
                // ----------> Abre Base de Datos
                Conn = PoolBD.Open(BD);
                if (Conn.State == ConnectionState.Closed)
                {
                    // La BD no está disponible ahora mismo (típico durante un
                    // corte de luz): en vez de perder la lectura, se guarda en
                    // local para reenviarla en cuanto la BD vuelva a estar.
                    Guardar_Registro_Pendiente_Aire(Funcion.DDatos_LecAire.Cod_Cosechero, Funcion.DDatos_LecAire.Cosechero,
                        Funcion.DDatos_LecAire.Fecha, Funcion.DDatos_LecAire.Hora, Funcion.DDatos_LecAire.Tiempo, Funcion.DDatos_LecAire.Tag);
                    return false;
                }
                // ----------> Comando
                Consulta.Connection = Conn;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "INSERT INTO fmemoriaAire (COD,COSECHERO,FECHA,HORA,TIEMPO,TAG) " +
                                                             "VALUES(@Cod,@Cosechero,@Fecha,@Hora,@Tiempo,@Tag)";

                Consulta.Parameters.Clear();

                Consulta.Parameters.AddWithValue("@Cod", Funcion.DDatos_LecAire.Cod_Cosechero);
                Consulta.Parameters.AddWithValue("@Cosechero", Funcion.DDatos_LecAire.Cosechero);
                Consulta.Parameters.AddWithValue("@Fecha", Funcion.DDatos_LecAire.Fecha.ToString("yyyy-MM-dd"));
                Consulta.Parameters.AddWithValue("@Hora", Funcion.DDatos_LecAire.Hora);
                Consulta.Parameters.AddWithValue("@Tiempo", Funcion.DDatos_LecAire.Tiempo);
                Consulta.Parameters.AddWithValue("@Tag", Funcion.DDatos_LecAire.Tag);
                Consulta.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (Funcion.Depuracion) Funcion.Grabar_Proceso_Depuracion("0.GRABAR REGISTRO AIRE", "ERROR AL GRABAR EL REGISTRO EN LA BD");

                Funcion.Grabar_Error("grabar_Cupo Llenado Aire: ", ex.Message);

                // Fallo al grabar (p.ej. la conexión se cayó a mitad de la
                // operación): tampoco perdemos el registro, se guarda en local.
                Guardar_Registro_Pendiente_Aire(Funcion.DDatos_LecAire.Cod_Cosechero, Funcion.DDatos_LecAire.Cosechero,
                    Funcion.DDatos_LecAire.Fecha, Funcion.DDatos_LecAire.Hora, Funcion.DDatos_LecAire.Tiempo, Funcion.DDatos_LecAire.Tag);
                return false;
            }
            finally
            {
                // Antes esta función NO cerraba la conexión: se ha corregido
                // para no ir agotando el pool de conexiones con el tiempo.
                PoolBD.Close(Conn);
                Funciones.Fugeneral.releaseObject(Conn);
                Funciones.Fugeneral.releaseObject(Consulta);
            }
            if (Funcion.Depuracion)
            {
                Funcion.Grabar_Proceso_Depuracion("4.GRABAR REGISTRO AIRE", "REGISTRO GUARDADO CORRECTO");
            }

            return true;
        }

        #region [ Cola Local de Pendientes (resiliencia ante cortes de luz) ]

        // Fichero donde se guardan, en texto plano separado por '|', las
        // lecturas RFID que no se pudieron grabar en la BD porque no estaba
        // disponible (por ejemplo, durante un corte de luz).
        private static string RutaFicheroPendientes()
        {
            string Directorio = Funcion.rutaAP + "\\Datos\\Pendientes";
            if (!Directory.Exists(Directorio))
                Directory.CreateDirectory(Directorio);
            return Directorio + "\\pendientes_aire.txt";
        }

        // Escapa el separador '|' y los saltos de línea para que cada
        // registro ocupe siempre una única línea del fichero.
        private static string EscaparCampo(string valor)
        {
            if (string.IsNullOrEmpty(valor)) return "";
            return valor.Replace("|", "/").Replace("\r", " ").Replace("\n", " ");
        }

        private static readonly object _lockPendientes = new object();

        public static void Guardar_Registro_Pendiente_Aire(int Cod, string Cosechero, DateTime Fecha, string Hora, int Tiempo, string Tag)
        {
            try
            {
                string linea = string.Join("|",
                    Cod.ToString(),
                    EscaparCampo(Cosechero),
                    Fecha.ToString("yyyy-MM-dd"),
                    EscaparCampo(Hora),
                    Tiempo.ToString(),
                    EscaparCampo(Tag));

                lock (_lockPendientes)
                {
                    File.AppendAllLines(RutaFicheroPendientes(), new[] { linea });
                }

                if (Funcion.Depuracion)
                    Funcion.Grabar_Proceso_Depuracion("GUARDAR PENDIENTE AIRE", "Registro guardado en local para reenvío posterior");
            }
            catch (Exception ex)
            {
                // Si ni siquiera se puede guardar en local, al menos queda
                // constancia en el log de errores.
                Funcion.Grabar_Error("Guardar_Registro_Pendiente_Aire: ", ex.Message);
            }
        }

        // Intenta reenviar a la BD todos los registros pendientes guardados en
        // local. Los que se graban correctamente se quitan del fichero; los
        // que sigan fallando (BD aún no disponible) se conservan para el
        // siguiente intento.
        public static void Reenviar_Pendientes_Aire(string BD)
        {
            string Fichero = RutaFicheroPendientes();

            List<string> lineas;
            lock (_lockPendientes)
            {
                if (!File.Exists(Fichero)) return;
                lineas = new List<string>(File.ReadAllLines(Fichero));
            }

            if (lineas.Count == 0) return;

            List<string> noEnviadas = new List<string>();

            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;

                string[] campos = linea.Split('|');
                if (campos.Length != 6)
                {
                    // Línea corrupta o de un formato antiguo: se descarta para
                    // no bloquear el resto de la cola indefinidamente.
                    Funcion.Grabar_Error("Reenviar_Pendientes_Aire", "Línea con formato inválido descartada: " + linea);
                    continue;
                }

                bool enviado = Reenviar_Un_Pendiente_Aire(BD, campos);
                if (!enviado)
                    noEnviadas.Add(linea);
            }

            lock (_lockPendientes)
            {
                if (noEnviadas.Count > 0)
                    File.WriteAllLines(Fichero, noEnviadas);
                else
                    File.Delete(Fichero);
            }

            if (Funcion.Depuracion && lineas.Count != noEnviadas.Count)
                Funcion.Grabar_Proceso_Depuracion("REENVIAR PENDIENTES AIRE",
                    $"Reenviados {lineas.Count - noEnviadas.Count} de {lineas.Count} registros pendientes");
        }

        private static bool Reenviar_Un_Pendiente_Aire(string BD, string[] campos)
        {
            SqlConnection Conn = new SqlConnection();
            SqlCommand Consulta = new SqlCommand();
            try
            {
                Conn = PoolBD.Open(BD);
                if (Conn.State == ConnectionState.Closed)
                    return false; // BD sigue sin estar disponible: se reintentará más tarde

                Consulta.Connection = Conn;
                Consulta.CommandType = CommandType.Text;
                Consulta.CommandText = "INSERT INTO fmemoriaAire (COD,COSECHERO,FECHA,HORA,TIEMPO,TAG) " +
                                                             "VALUES(@Cod,@Cosechero,@Fecha,@Hora,@Tiempo,@Tag)";
                Consulta.Parameters.Clear();
                Consulta.Parameters.AddWithValue("@Cod", Convert.ToInt32(campos[0]));
                Consulta.Parameters.AddWithValue("@Cosechero", campos[1]);
                Consulta.Parameters.AddWithValue("@Fecha", campos[2]);
                Consulta.Parameters.AddWithValue("@Hora", campos[3]);
                Consulta.Parameters.AddWithValue("@Tiempo", Convert.ToInt32(campos[4]));
                Consulta.Parameters.AddWithValue("@Tag", campos[5]);
                Consulta.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Funcion.Grabar_Error("Reenviar_Un_Pendiente_Aire: ", ex.Message);
                return false;
            }
            finally
            {
                PoolBD.Close(Conn);
                Funciones.Fugeneral.releaseObject(Conn);
                Funciones.Fugeneral.releaseObject(Consulta);
            }
        }

        #endregion

    #endregion

    }
}
