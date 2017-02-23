using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass;

namespace AudioSpamer2.Effects
{
    public class Echo2:AudioEffect
    {
        BASS_BFX_ECHO2 echo2;
        int ID;

        public Echo2()
        {
            echo2 = new BASS_BFX_ECHO2();
        }

        AudioClip sf=null;
        public override void ApplyToSoundFile(AudioClip sf)
        {
            this.sf = sf;
            ID = Bass.BASS_ChannelSetFX(sf.AudioStream.StreamHandle, BASSFXType.BASS_FX_BFX_ECHO2, 1);
            handler = new AudioClip.AudioChannelChangedHandler(sf_SoundChannelChanged);
            sf.AudioStreamChanged += handler;
            Update();
        }

        AudioClip.AudioChannelChangedHandler handler;

        void sf_SoundChannelChanged(AudioStream c)
        {
            ID = Bass.BASS_ChannelSetFX(c.StreamHandle, BASSFXType.BASS_FX_BFX_ECHO2, 1);
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
            Bass.BASS_FXSetParameters(ID, echo2);
        }

        [EffectPropertyDescription("Dry Mix", -2, 2, 0)]
        public float Dry
        {
            get { return echo2.fDryMix; }
            set { echo2.fDryMix = value; Update(); }
        }

        [EffectPropertyDescription("Wet Mix", -2, 2, 0)]
        public float Wet
        {
            get { return echo2.fWetMix; }
            set { echo2.fWetMix = value; Update(); }
        }

        [EffectPropertyDescription("Feedback", -1, 1, 0)]
        public float Feedback
        {
            get { return echo2.fFeedback; }
            set { echo2.fFeedback = value; Update(); }
        }

        [EffectPropertyDescription("Delay", 0, 6, 0)]
        public float Delay
        {
            get { return echo2.fDelay; }
            set { echo2.fDelay = value; Update(); }
        }

        public override string Name
        {
            get { return "Echo 2"; }
        }
    }
}
