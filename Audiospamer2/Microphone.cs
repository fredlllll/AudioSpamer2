using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;

namespace AudioSpamer2
{
    public class Microphone
    {
        int device;
        public int ID;
        private RECORDPROC rpro;
        public Microphone(int device)
        {
            this.device = device;
            rpro = new RECORDPROC(MyRecording);
            Bass.BASS_RecordInit(device);
            ID = Bass.BASS_RecordStart(Global.defaultSampleRate*2, 1, BASSFlag.BASS_DEFAULT, rpro, IntPtr.Zero);
        }

        void Global_PlayFreqChanged(int i)
        {
            Bass.BASS_ChannelSetAttribute(ID, BASSAttribute.BASS_ATTRIB_FREQ, i * 2);
        }

        public delegate void Samples(int handle, IntPtr buffer, int length, IntPtr user);
        public event Samples newSamples;

        private bool MyRecording(int handle, IntPtr buffer, int length, IntPtr user)
        {
            if (newSamples != null)
            {
                newSamples(handle, buffer, length, user);
            }
            return true;
        }

        public float Volume
        {
            get { float re = 0; Bass.BASS_RecordGetInput(-1, ref re); return re; }
            set { Bass.BASS_RecordSetInput(-1, BASSInput.BASS_INPUT_ON, value); }
        }
    }
}
