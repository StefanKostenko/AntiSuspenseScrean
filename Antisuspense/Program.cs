using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace PreventScreenOff
{
    static class Program
    {
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetSystemPowerStatus(out SYSTEM_POWER_STATUS sps);

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SYSTEM_POWER_STATUS
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte SystemStatusFlag;
        }

        const byte VK_SHIFT = 0x10;
        const uint KEYEVENTF_KEYUP = 0x0002;

        static uint GetIdleTime()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            GetLastInputInfo(ref lastInputInfo);
            return ((uint)Environment.TickCount - lastInputInfo.dwTime);
        }

        static void SimulateKeyPress()
        {
            keybd_event(VK_SHIFT, 0, 0, UIntPtr.Zero); // Simula la pulsación de la tecla Shift
            System.Threading.Thread.Sleep(100); // Espera 100 ms
            keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, UIntPtr.Zero); // Simula la liberación de la tecla Shift
        }

        static bool IsScreenSaverRunning()
        {
            SYSTEM_POWER_STATUS sps;
            if (GetSystemPowerStatus(out sps))
            {
                return sps.SystemStatusFlag == 1;
            }
            return false;
        }

        static MenuItem startMenuItem;
        static MenuItem stopMenuItem;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon("icon.ico"); // Asegúrate de tener un archivo de icono
            notifyIcon.Visible = true;

            startMenuItem = new MenuItem("Encender", (s, e) => StartService());
            stopMenuItem = new MenuItem("Apagar", (s, e) => StopService());

            UpdateMenuItems();

            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] {
                startMenuItem,
                stopMenuItem,
                new MenuItem("Salir", (s, e) => Application.Exit())
            });

            Application.Run();
        }

        static Timer timer;

        static void StartService()
        {
            timer = new Timer();
            timer.Interval = 60000; // Verifica cada 10 segundos
            timer.Tick += (s, e) => {
                if (GetIdleTime() > 60000 && !IsScreenSaverRunning())
                {
                    SimulateKeyPress();
                }
            };
            timer.Start();
            UpdateMenuItems();
        }

        static void StopService()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
            UpdateMenuItems();
        }

        static void UpdateMenuItems()
        {
            if (timer != null && timer.Enabled)
            {
                startMenuItem.Checked = true;
                stopMenuItem.Checked = false;
            }
            else
            {
                startMenuItem.Checked = false;
                stopMenuItem.Checked = true;
            }
        }
    }
}
