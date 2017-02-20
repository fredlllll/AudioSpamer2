using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AudioSpamer2
{
    static class Program
    {
        public const String OptionsPath = "options.ini";

        static IniFile ini;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            ini = new IniFile(OptionsPath);
            bool initial = !System.IO.File.Exists(OptionsPath);
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(initial,ini));
        }
    }
}
