using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;

namespace AudioSpamer2
{
    public class SoundChannel
    {
        public int ID;

        public SoundChannel()
        {
            ID = Bass.BASS_StreamCreatePush(Global.defaultSampleRate, 2, BASSFlag.BASS_DEFAULT, IntPtr.Zero); 
        }

        public SoundChannel(int id)
        {
            ID = id;
        }

        public bool playing = false;
        public void Play(bool restart=false)
        {
            Bass.BASS_ChannelPlay(ID, restart);
            playing = true;
        }

        public void Stop()
        {
            Bass.BASS_ChannelStop(ID);
            playing = false;
        }
        public void Pause()
        {
            Bass.BASS_ChannelPause(ID);
            playing = false;
        }

        public float Volume
        {
            get { float re = 0; Bass.BASS_ChannelGetAttribute(ID, BASSAttribute.BASS_ATTRIB_VOL, ref re); return re; }
            set { Bass.BASS_ChannelSetAttribute(ID, BASSAttribute.BASS_ATTRIB_VOL, value); }
        }

        public float Speed
        {
            get { float re = 0; Bass.BASS_ChannelGetAttribute(ID, BASSAttribute.BASS_ATTRIB_FREQ, ref re); return re / Global.defaultSampleRate; }
            set { Bass.BASS_ChannelSetAttribute(ID, BASSAttribute.BASS_ATTRIB_FREQ, (float)(value * Global.defaultSampleRate)); }
        }

        public float Pitch
        {
            get { float re = 0; Bass.BASS_ChannelGetAttribute(ID, BASSAttribute.BASS_ATTRIB_TEMPO_PITCH, ref re); return re; }
            set { Bass.BASS_ChannelSetAttribute(ID, BASSAttribute.BASS_ATTRIB_TEMPO_PITCH, value); }
        }

        public float Tempo
        {
            get { float re = 0; Bass.BASS_ChannelGetAttribute(ID, BASSAttribute.BASS_ATTRIB_TEMPO, ref re); return re; }
            set { Bass.BASS_ChannelSetAttribute(ID, BASSAttribute.BASS_ATTRIB_TEMPO, value); }
        }

        public long StreamPosition
        {
            get
            {
                return Bass.BASS_ChannelGetPosition(ID);
            }
            set
            {
                Bass.BASS_ChannelSetPosition(ID, value);
            }
        }

        public long StreamLength
        {
            get { return Bass.BASS_ChannelGetLength(ID); }
        }

        public float PercentagePlayed
        {
            get
            {
                return (Bass.BASS_ChannelGetPosition(ID) / (float)Bass.BASS_ChannelGetLength(ID));
            }
        }
    }
}
