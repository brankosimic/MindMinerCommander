using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MindMinerCommander
{
    static class Program
    {
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool turnon);

        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run();

            var processes = Process.GetProcesses();

            var mindMiner = processes.First(x => x.ProcessName == "cmd" && x.MainWindowTitle.StartsWith("MindMiner"));

            SwitchToThisWindow(mindMiner.MainWindowHandle, false);
            SetForegroundWindow(mindMiner.MainWindowHandle);

            SendKeys.SendWait("^(r)");
        }
    }
}
