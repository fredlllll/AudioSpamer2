using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioSpamer2
{
    public class VersionContainer
    {
        public Version v;

        public int CommandCount
        {
            get { return commands.Count; }
        }
        LinkedList<Command> commands = new LinkedList<Command>();
        public VersionContainer(String s)
        {
            String[] sa = s.Split('|');
            v = new Version(sa[0]);
            for (int i = 1; i < sa.Length; i++)
            {
                commands.AddLast(new Command(sa[i]));
            }
        }

        public void Apply(MyProgressBar pb)
        {
            foreach (Command c in commands)
            {
                c.Execute();
                pb.Value++;
            }
        }
    }
}
