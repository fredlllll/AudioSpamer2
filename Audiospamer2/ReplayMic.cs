using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;

namespace AudioSpamer2
{
    public class ReplayMic
    {

        int nowDecoding;
        public int MicID,PlayID;
        private RECORDPROC rpro;
        public ReplayMic(int device)
        {
            rpro = new RECORDPROC(MyRecording);
            Bass.BASS_RecordInit(device);
            MicID = Bass.BASS_RecordStart(Global.DefaultSampleRate, 1, BASSFlag.BASS_DEFAULT, rpro, IntPtr.Zero);

            nowDecoding = Bass.BASS_StreamCreatePush(Global.DefaultSampleRate, 1, BASSFlag.BASS_STREAM_DECODE, IntPtr.Zero);
            PlayID = Un4seen.Bass.AddOn.Fx.BassFx.BASS_FX_TempoCreate(nowDecoding, BASSFlag.BASS_DEFAULT);

            Bass.BASS_ChannelPlay(nowDecoding, false);
            Bass.BASS_ChannelPlay(PlayID, true);
        }

        private bool MyRecording(int handle, IntPtr buffer, int length, IntPtr user)
        {
            Bass.BASS_StreamPutData(nowDecoding, buffer, length);
            return true;
        }

        public void STOP()
        {
            Bass.BASS_ChannelStop(MicID);
            Bass.BASS_StreamFree(MicID);
            Bass.BASS_ChannelStop(nowDecoding);
            Bass.BASS_StreamFree(nowDecoding);
            Bass.BASS_RecordFree();
            Bass.BASS_ChannelStop(PlayID);
            Bass.BASS_StreamFree(PlayID);
        }

        public float Volume
        {
            get { float re = 0; Bass.BASS_ChannelGetAttribute(PlayID, BASSAttribute.BASS_ATTRIB_VOL, ref re); return re; }
            set { Bass.BASS_ChannelSetAttribute(PlayID, BASSAttribute.BASS_ATTRIB_VOL, value); }
        }

        public AudioStream AudioChannel
        {
            get { return new AudioStream(PlayID); }
        }
    }
}
