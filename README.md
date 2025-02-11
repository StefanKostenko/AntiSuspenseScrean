# AntiSuspenseScrean

_AntiSuspenseScreen es una aplicación de Windows Forms desarrollada en C# que evita que la pantalla se apague debido a la inactividad del sistema. La aplicación simula la pulsación de la tecla Shift cuando detecta que el sistema ha estado inactivo durante un período de tiempo específico._

## Características 📌

_Estas instrucciones te permitirán obtener una copia del proyecto en funcionamiento en tu máquina local para propósitos de desarrollo y pruebas._

* **Prevención de apagado de pantalla:** Simula la pulsación de la tecla Shift para evitar que la pantalla se apague. ⌨️
* **Icono en la barra de tareas:** La aplicación agrega un icono en la barra de tareas de Windows para facilitar el control del servicio. 🖥️
* **Control del servicio:** Permite encender y apagar el servicio desde el menú contextual del icono en la barra de tareas. 🔛
* **Detección de inactividad:** Utiliza la API de Windows para detectar la inactividad del sistema. ⚙️

Mira **Deployment** para conocer como desplegar el proyecto.

## Instalación 🔧

1. Para instalar la aplicación dirigete a [RELEASES](https://github.com/StefanKostenko/AntiSuspenseScrean/releases) y descarge el .zip

2. Una vez bajado el .zip descomprimelo y ejecute el .exe

## Uso ⚙️

1. **Ejecutar la aplicación:** Haz doble clic en PreventScreenOff.exe para iniciar la aplicación.
2. **Icono en la barra de tareas:** Verás un icono en la barra de tareas de Windows.

### Control del servicio:
**Haz clic derecho en el icono de la barra de tareas.**
  * Selecciona "Encender" para iniciar el servicio.
  * Selecciona "Apagar" para detener el servicio.
  * Selecciona "Salir" para cerrar la aplicación.

## Notas 🗒️

* La aplicación verifica la inactividad del sistema cada 60 segundos.
* Si el sistema ha estado inactivo durante más de 60 segundos y no se está ejecutando el protector de pantalla, la aplicación simula la pulsación de la tecla Shift.

## Autores ✒️

_Stefan Kostenko_
