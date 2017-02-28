using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace AudioSpamer2
{
    public class AudioSpamerCore
    {
        public AudioClip CurrentSoundFile
        {
            get;set;
        }

        public ReplayMic ReplayMic
        {
            get; protected set;
        }

        public void InitMicrophone(int micDevice)
        {
            ReplayMic = new ReplayMic(micDevice);
        }

        public void InitializeOutputDevice(int device, int sampleRate)
        {
            Bass.BASS_Init(device, sampleRate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
        }
    }
}
