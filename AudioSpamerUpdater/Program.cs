using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace AudioSpamer2.Loader
{
    static class Program
    {
        const string audiospamerlib = "AudioSpamer2.dll";
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            if(!args.Any((x) => x.Equals("noupdate", StringComparison.InvariantCultureIgnoreCase)))
            {
                DoUpdate();
            }

            StartProgram(args);
        }

        static void DoUpdate()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void StartProgram(string[] args)
        {
            try
            {
                Assembly a = Assembly.LoadFrom(audiospamerlib);
                Type t = a.GetType("AudioSpamer2lib.Program");
                MethodInfo mi = t.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic/* new Type[] { typeof(String[]) }*/);
                mi.Invoke(null, args);
            }
            catch
            {
                MessageBox.Show("Well something failed with the update =/. please redownload the newest version by yourself from fredlllll.ohost.de");
            }
        }
    }
}
