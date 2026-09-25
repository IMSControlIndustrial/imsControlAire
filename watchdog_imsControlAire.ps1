# ============================================================================
# watchdog_imsControlAire.ps1
#
# Vigila que imsControlAire.exe siga "vivo" comprobando la fecha de
# modificación del fichero Datos\heartbeat.txt (el propio programa lo
# actualiza cada 30 segundos, ver Funcion.Actualizar_Heartbeat() y
# FControlAire.Tarea_Heartbeat()).
#
# Si el heartbeat lleva más de $MinutosLimite minutos sin actualizarse
# (o el proceso no está corriendo), mata el proceso (si existe) y lo
# vuelve a lanzar.
#
# CÓMO INSTALARLO:
#   1. Ajusta las variables $RutaExe y $RutaHeartbeat de abajo a tu instalación.
#   2. Guarda este fichero en, por ejemplo, C:\imsControlAire\watchdog_imsControlAire.ps1
#   3. Abre el "Programador de tareas" de Windows y crea una tarea nueva:
#        - Desencadenador: "Repetir cada 5 minutos, indefinidamente"
#          (y también "Al iniciar el sistema", con un retraso de 1-2 min)
#        - Acción: Iniciar un programa
#            Programa: powershell.exe
#            Argumentos: -NoProfile -ExecutionPolicy Bypass -File "C:\imsControlAire\watchdog_imsControlAire.ps1"
#        - Marca "Ejecutar tanto si el usuario ha iniciado sesión como si no"
#        - Marca "Ejecutar con los privilegios más altos"
#   4. Prueba la tarea con el botón "Ejecutar" y revisa Datos\Errores\ y
#      Datos\Depuracion\ para confirmar que todo funciona.
# ============================================================================

# ---------- CONFIGURACIÓN: AJUSTA ESTAS RUTAS A TU INSTALACIÓN ----------
$RutaExe        = "C:\imsControlAire\imsControlAire.exe"
$RutaHeartbeat  = "C:\imsControlAire\Datos\heartbeat.txt"
$NombreProceso  = "imsControlAire"
$MinutosLimite  = 5     # si el heartbeat lleva más de esto sin actualizarse, se considera colgado
# --------------------------------------------------------------------------

function Escribir-Log($mensaje) {
    $linea = "$(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')  $mensaje"
    $carpetaLog = Split-Path $RutaHeartbeat -Parent
    $ficheroLog = Join-Path $carpetaLog "watchdog.log"
    Add-Content -Path $ficheroLog -Value $linea
}

$procesoActivo = Get-Process -Name $NombreProceso -ErrorAction SilentlyContinue

$heartbeatOk = $false
if (Test-Path $RutaHeartbeat) {
    $ultimaEscritura = (Get-Item $RutaHeartbeat).LastWriteTime
    $minutosDesdeUltima = (New-TimeSpan -Start $ultimaEscritura -End (Get-Date)).TotalMinutes
    if ($minutosDesdeUltima -le $MinutosLimite) {
        $heartbeatOk = $true
    } else {
        Escribir-Log "Heartbeat desactualizado: hace $([math]::Round($minutosDesdeUltima,1)) minutos."
    }
} else {
    Escribir-Log "No se encuentra el fichero de heartbeat ($RutaHeartbeat)."
}

if ($procesoActivo -and $heartbeatOk) {
    # Todo correcto: el proceso está corriendo y respondiendo. No hacer nada.
    exit 0
}

# Si llegamos aquí, o el proceso no está corriendo, o lleva demasiado
# tiempo sin actualizar el heartbeat (probablemente colgado).
if ($procesoActivo) {
    Escribir-Log "Proceso encontrado pero heartbeat colgado: matando proceso (PID $($procesoActivo.Id))."
    try {
        Stop-Process -Id $procesoActivo.Id -Force -ErrorAction Stop
        Start-Sleep -Seconds 5
    } catch {
        Escribir-Log "No se pudo matar el proceso: $($_.Exception.Message)"
    }
} else {
    Escribir-Log "El proceso no está corriendo."
}

Escribir-Log "Relanzando $RutaExe ..."
try {
    Start-Process -FilePath $RutaExe -ErrorAction Stop
    Escribir-Log "Proceso relanzado correctamente."
} catch {
    Escribir-Log "ERROR al relanzar el proceso: $($_.Exception.Message)"
}
