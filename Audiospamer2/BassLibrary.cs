using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace AudioSpamer2
{
    public static class BassLibrary
    {
        const string bassRegMail = "frederik.gelder@freenet.de";
        const string bassRegCode = "2X16373726163723";

        public static void Initialize()
        {
            BassNet.Registration(bassRegMail, bassRegCode);
        }
    }
}
