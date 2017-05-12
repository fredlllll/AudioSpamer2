using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Codolith.Config;

namespace AudioSpamer2
{
    static class Program
    {
        const string configFile = "audiospamer.xml";
        public static Config Config
        {
            get;
            private set;
        }

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Config = new Config(configFile);

            bool initial = !System.IO.File.Exists(configFile);
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
