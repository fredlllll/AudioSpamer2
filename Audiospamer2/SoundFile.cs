using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;

namespace AudioSpamer2
{
    public class SoundFile
    {
        public int ID;
        public int nowDecoding;
        public SoundChannel sc;
        public String path;

        public SoundFile(String path)
        {
            this.path = path;
            nowDecoding = Bass.BASS_StreamCreateFile(path, 0L, 0L, BASSFlag.BASS_STREAM_DECODE);
            ID = Un4seen.Bass.AddOn.Fx.BassFx.BASS_FX_TempoCreate(nowDecoding, BASSFlag.BASS_DEFAULT);
            sc = new SoundChannel(ID);
        }

        public void ClearSoundChannelChanged()
        {
            SoundChannelChanged = null;
        }

        public delegate void SoundChannelChangedHandler(SoundChannel c);
        public event SoundChannelChangedHandler SoundChannelChanged;

        public void Free()
        {
            sc.Stop();
            Bass.BASS_StreamFree(ID);
        }

        public bool reversed;
        int revStream;
        public void Reverse()
        {
            if (!reversed)
            {
                reversed = true;
                long here = sc.StreamPosition;
                Free();
                revStream = Un4seen.Bass.AddOn.Fx.BassFx.BASS_FX_ReverseCreate(nowDecoding, 1f, BASSFlag.BASS_STREAM_DECODE);
                ID = Un4seen.Bass.AddOn.Fx.BassFx.BASS_FX_TempoCreate(revStream, BASSFlag.BASS_DEFAULT);
                sc = new SoundChannel(ID);
                sc.StreamPosition = here;
                sc.Play();
            }
            else
            {
                reversed = false;
                long here = sc.StreamPosition;
                Free();
                Bass.BASS_StreamFree(revStream);
                ID = Un4seen.Bass.AddOn.Fx.BassFx.BASS_FX_TempoCreate(nowDecoding, BASSFlag.BASS_DEFAULT);
                sc = new SoundChannel(ID);
                sc.StreamPosition = here;
                sc.Play();
            }
            if (SoundChannelChanged != null)
            {
                SoundChannelChanged(sc);
            }
        }


        public void CheckForEndAndReplay()
        {
            if (!reversed)
            {
                if (sc.StreamPosition >= sc.StreamLength && loop)
                {
                    Bass.BASS_ChannelPlay(sc.ID, false);
                }

                if (playMarks && (sc.StreamPosition >= B || sc.StreamPosition < A))
                {
                    sc.StreamPosition = A;
                }
            }
            else
            {
                if (sc.StreamPosition <= 0 && loop)
                {
                    sc.StreamPosition = sc.StreamLength;
                    Bass.BASS_ChannelPlay(sc.ID, false);
                }

                if (playMarks && (sc.StreamPosition <= A || sc.StreamPosition > B))
                {
                    sc.StreamPosition = B;
                }
            }
        }

        public Int64 A, B;
        public bool playMarks = false;
        public void PlayMarks()
        {
            playMarks = true;
            if (B<A)
            {
                Int64 temp = B;
                this.B = A;
                this.A = temp;
            }
            /*this.A = Math.Min(A,B);
            this.B = Math.Max(A,B);*/
        }

        public void StopPlayingMarks()
        {
            playMarks = false;
        }

        bool loop = true;
        public bool Loop
        {
            get { return loop; }
            set { loop = value; }
        }
    }
}
