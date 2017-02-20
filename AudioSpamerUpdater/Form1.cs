using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace AudioSpamer2.Loader
{
    public partial class Form1 : Form
    {
        const String VersionURI = "http://fredlllll.ohost.de/getfile.php?f=audiospamer2/audiospamer2version.txt";
        const String VersionCommandsURI = "http://fredlllll.ohost.de/getfile.php?f=audiospamer2/audiospamer2versioncommands.txt";

        public Form1()
        {
            InitializeComponent();
            this.Shown += new EventHandler(Form1_Shown);
            updateThread = new System.Threading.Thread(RunUpdate);
        }

        System.Threading.Thread updateThread;
        void Form1_Shown(object sender, EventArgs e)
        {
            updateThread.Start();
        }

        LinkedList<VersionContainer> versions;
        Version thisversion;
        void RunUpdate()
        {
            StreamReader reader=null;
            try
            {
                reader = new StreamReader("version.txt");
                thisversion = new Version(reader.ReadLine());
                reader.Close();
            }
            catch
            {
                try { reader.Close(); }
                catch { }
                thisversion = new Version(1, 0, 0, 0);
            }
            try
            {
                HttpWebRequest req = null;
                HttpWebResponse response = null;
                Stream resStream = null;
                Version newestVersion = null;
                try
                {
                    req = (HttpWebRequest)WebRequest.Create(VersionURI);
                    response = (HttpWebResponse)req.GetResponse();
                    resStream = response.GetResponseStream();
                    reader = new StreamReader(resStream);
                    newestVersion = new Version(reader.ReadLine());
                    reader.Close();
                }
                catch(WebException)
                {
                    this.BackgroundImage = global::AudioSpamer2.Loader.Properties.Resources.nonewversion;
                    //timer.Start();
                    return;
                }
                if (!newestVersion.IsHigherThan(thisversion))
                {
                    this.BackgroundImage = global::AudioSpamer2.Loader.Properties.Resources.nonewversion;
                    //timer.Start();
                    return;
                }
                else
                {
                    this.BackgroundImage = global::AudioSpamer2.Loader.Properties.Resources.newversion;
                }
                progressBar2.Show();

                req = (HttpWebRequest)WebRequest.Create(VersionCommandsURI);
                response = (HttpWebResponse)req.GetResponse();
                resStream = response.GetResponseStream();
                reader = new StreamReader(resStream);

                LinkedList<String> lines = new LinkedList<string>();
                while (!reader.EndOfStream)
                {
                    lines.AddLast(reader.ReadLine());
                }
                reader.Close();
                String[] sa = lines.ToArray();
                versions = new LinkedList<VersionContainer>();
                int instructions = 0;
                for (int i = 0; i < sa.Length; i++)
                {
                    VersionContainer vc = new VersionContainer(sa[i]);
                    instructions += vc.CommandCount;
                    versions.AddLast(vc);
                }
                progressBar2.Maximum = instructions;
                progressBar2.Value = 0;
                foreach (VersionContainer vc in versions)
                {
                    if (vc.v.IsHigherThan(thisversion) && !vc.v.IsHigherThan(newestVersion))
                    {
                        vc.Apply(progressBar2);
                    }
                }
            }
            catch
            {
                this.Hide();
                MessageBox.Show("The Updateserver screwed something up =/. well you have to keep the old version then, or manually update by going to fredlllll.ohost.de");
                this.Close();
                return;
            }
            this.Close();
        }
    }
}
