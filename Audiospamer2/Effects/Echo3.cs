using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass;

namespace AudioSpamer2.Effects
{
    public class Echo3:AudioEffect
    {
        BASS_BFX_ECHO3 echo3;
        int ID;

        public Echo3()
        {
            echo3 = new BASS_BFX_ECHO3();
        }

        AudioClip sf=null;
        public override void ApplyToSoundFile(AudioClip sf)
        {
            this.sf = sf;
            ID = Bass.BASS_ChannelSetFX(sf.AudioStream.StreamHandle, BASSFXType.BASS_FX_BFX_ECHO3, 1);
            handler = new AudioClip.AudioChannelChangedHandler(sf_SoundChannelChanged);
            sf.AudioStreamChanged += handler;
            Update();
        }

        AudioClip.AudioChannelChangedHandler handler;

        void sf_SoundChannelChanged(AudioStream c)
        {
            ID = Bass.BASS_ChannelSetFX(c.StreamHandle, BASSFXType.BASS_FX_BFX_ECHO3, 1);
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
            Bass.BASS_FXSetParameters(ID, echo3);
        }

        [EffectPropertyDescription("Dry Mix", -2, 2, 0)]
        public float Dry
        {
            get { return echo3.fDryMix; }
            set { echo3.fDryMix = value; Update(); }
        }

        [EffectPropertyDescription("Wet Mix", -2, 2, 0)]
        public float Wet
        {
            get { return echo3.fWetMix; }
            set { echo3.fWetMix = value; Update(); }
        }

        [EffectPropertyDescription("Delay", 0, 6, 0)]
        public float Delay
        {
            get { return echo3.fDelay; }
            set { echo3.fDelay = value; Update(); }
        }

        public override string Name
        {
            get { return "Echo 3"; }
        }
    }
}
