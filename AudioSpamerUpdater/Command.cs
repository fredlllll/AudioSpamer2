using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace AudioSpamer2
{
    public class Command
    {
        String s;
        public Command(String s)
        {
            this.s = s;
        }

        public void Execute()
        {
            String[] sa = s.Split(' ');
            switch (sa[0].ToLower())
            {
                case "add":
                    WebClient adder = new WebClient();
                    adder.DownloadFile(sa[1], sa[2]);
                    break;
                case "replace":
                    System.IO.File.Delete(sa[2]);
                    WebClient replacer = new WebClient();
                    replacer.DownloadFile(sa[1], sa[2]);
                    break;
                case "delete":
                    System.IO.File.Delete(sa[1]);
                    break;
                case "setversion":
                    System.IO.File.Delete("version.txt");
                    System.IO.File.Create("version.txt").Close();
                    StreamWriter sw = new StreamWriter("version.txt");
                    sw.WriteLine(sa[1]);
                    sw.Close();
                    break;
                case "msg":
                    MessageBox.Show(sa[1]);
                    break;
            }
        }
    }
}
