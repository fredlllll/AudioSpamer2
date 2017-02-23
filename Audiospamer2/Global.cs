using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioSpamer2
{
    public static class Global
    {
        public static int DefaultSampleRate { get; set; } = 44100;
        public static int OutputDeviceIndex { get; set; } = -1;
        public static int InputDeviceIndex { get; set; } = -1;
    }
}
