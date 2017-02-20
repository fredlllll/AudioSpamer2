using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AudioSpamer2
{
    public class IniFile
    {
        Dictionary<String, String> properties = new Dictionary<string, string>();
        String path;
        public IniFile(String path)
        {
            this.path = path;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                while (true)
                {
                    try
                    {
                        String[] sa = sr.ReadLine().Split('=');
                        properties.Add(sa[0], sa[1]);
                    }
                    catch
                    {
                        break;
                    }
                }
                sr.Close();
            }catch(FileNotFoundException){
                //no file yet. make empty ini file
            }
        }

        ~IniFile()
        {
            Flush();
        }

        public void SetProperty(String name, String value)
        {
            if (properties.ContainsKey(name))
            {
                properties[name] = value;
            }
            else
            {
                properties.Add(name, value);
            }
        }

        public String GetProperty(String name)
        {
            if (properties.ContainsKey(name))
            {
                return properties[name];
            }
            return "";
        }

        public void Flush()
        {
            File.Delete(path);
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (KeyValuePair<String, String> kv in properties)
            {
                sw.WriteLine(kv.Key+"="+kv.Value);
            }
            sw.Close();
        }
    }
}
