using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioSpamer2
{
    public class Version
    {
        public int major, minor, rel, rev;
        public Version(int major, int minor, int rel, int rev)
        {
            this.major = major;
            this.minor = minor;
            this.rel = rel;
            this.rev = rev;
        }

        public Version(String s)
        {
            String[] sa = s.Split('.');
            major = int.Parse(sa[0]);
            minor = int.Parse(sa[1]);
            rel = int.Parse(sa[2]);
            rev = int.Parse(sa[3]);
        }

        public override string ToString()
        {
            return major + "." + minor + "." + rel + "." + rev;
        }

        public bool IsHigherThan(Version other)
        {
            if (major > other.major)
            {
                return true;
            }
            if (minor > other.minor)
            {
                return true;
            }
            if (rel > other.rel)
            {
                return true;
            }
            if (rev > other.rev)
            {
                return true;
            }
            return false;
        }
    }
}
