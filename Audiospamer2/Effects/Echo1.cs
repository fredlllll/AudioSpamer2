using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass;

namespace AudioSpamer2.Effects
{
    public class Echo1:AudioEffect
    {
        BASS_BFX_ECHO echo;
        int ID;

        public Echo1()
        {
            echo = new BASS_BFX_ECHO();
        }

        AudioClip sf=null;
        public override void ApplyToSoundFile(AudioClip sf)
        {
            this.sf = sf;
            ID = Bass.BASS_ChannelSetFX(sf.AudioStream.StreamHandle, BASSFXType.BASS_FX_BFX_ECHO, 1);
            handler = new AudioClip.AudioChannelChangedHandler(sf_SoundChannelChanged);
            sf.AudioStreamChanged += handler;
            Update();
        }

        AudioClip.AudioChannelChangedHandler handler;

        void sf_SoundChannelChanged(AudioStream c)
        {
            ID = Bass.BASS_ChannelSetFX(c.StreamHandle, BASSFXType.BASS_FX_BFX_ECHO, 1);
        }

        public override void RemoveFromSoundFile()
        {
            if (sf != null)
            {
                Bass.BASS_ChannelRemoveFX(sf.AudioStream.StreamHandle, ID);
                sf.AudioStreamChanged -= handler;
                sf = null;
            }
        }

        public override void Update()
        {
            Bass.BASS_FXSetParameters(ID, echo);
        }

        [EffectPropertyDescription("Level", 0, 100, 0)]
        public float Level
        {
            get { return echo.fLevel; }
            set { echo.fLevel = value; Update(); }
        }

        [EffectPropertyDescription("Delay", 1200, 30000, 1200,1,true)]
        public int Delay
        {
            get { return echo.lDelay; }
            set { echo.lDelay = value; Update(); }
        }

        public override string Name
        {
            get { return "Echo 1"; }
        }
    }
}
