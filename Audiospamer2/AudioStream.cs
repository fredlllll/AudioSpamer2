using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;

namespace AudioSpamer2
{
    public class AudioStream : IDisposable
    {
        public int StreamHandle
        {
            get; protected set;
        }

        public AudioStream()
        {
            StreamHandle = Bass.BASS_StreamCreatePush(Global.DefaultSampleRate, 2, BASSFlag.BASS_DEFAULT, IntPtr.Zero);
        }

        public AudioStream(int streamHandle)
        {
            StreamHandle = streamHandle;
        }

        ~AudioStream()
        {
            Dispose();
        }

        public bool IsPlaying { get; protected set; } = false;
        public void Play(bool fromStart = false)
        {
            Bass.BASS_ChannelPlay(StreamHandle, fromStart);
            IsPlaying = true;
        }

        public void Stop()
        {
            Bass.BASS_ChannelStop(StreamHandle);
            IsPlaying = false;
        }
        public void Pause()
        {
            Bass.BASS_ChannelPause(StreamHandle);
            IsPlaying = false;
        }

        public void Dispose()
        {
            Bass.BASS_StreamFree(StreamHandle);
            GC.SuppressFinalize(this);
        }

        public float Volume
        {
            get { float re = 0; Bass.BASS_ChannelGetAttribute(StreamHandle, BASSAttribute.BASS_ATTRIB_VOL, ref re); return re; }
            set { Bass.BASS_ChannelSetAttribute(StreamHandle, BASSAttribute.BASS_ATTRIB_VOL, value); }
        }

        public float Speed
        {
            get { float re = 0; Bass.BASS_ChannelGetAttribute(StreamHandle, BASSAttribute.BASS_ATTRIB_FREQ, ref re); return re / Global.DefaultSampleRate; }
            set { Bass.BASS_ChannelSetAttribute(StreamHandle, BASSAttribute.BASS_ATTRIB_FREQ, (float)(value * Global.DefaultSampleRate)); }
        }

        public float Pitch
        {
            get { float re = 0; Bass.BASS_ChannelGetAttribute(StreamHandle, BASSAttribute.BASS_ATTRIB_TEMPO_PITCH, ref re); return re; }
            set { Bass.BASS_ChannelSetAttribute(StreamHandle, BASSAttribute.BASS_ATTRIB_TEMPO_PITCH, value); }
        }

        public float Tempo
        {
            get { float re = 0; Bass.BASS_ChannelGetAttribute(StreamHandle, BASSAttribute.BASS_ATTRIB_TEMPO, ref re); return re; }
            set { Bass.BASS_ChannelSetAttribute(StreamHandle, BASSAttribute.BASS_ATTRIB_TEMPO, value); }
        }

        public long Position
        {
            get { return Bass.BASS_ChannelGetPosition(StreamHandle); }
            set { Bass.BASS_ChannelSetPosition(StreamHandle, value); }
        }

        public long Length
        {
            get { return Bass.BASS_ChannelGetLength(StreamHandle); }
        }

        public float PercentagePlayed
        {
            get { return (Bass.BASS_ChannelGetPosition(StreamHandle) / (float)Bass.BASS_ChannelGetLength(StreamHandle)); }
        }
    }
}
