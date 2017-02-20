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

        SoundFile sf=null;
        public override void ApplyToSoundFile(SoundFile sf)
        {
            this.sf = sf;
            ID = Bass.BASS_ChannelSetFX(sf.sc.ID, BASSFXType.BASS_FX_BFX_ECHO2, 1);
            handler = new SoundFile.SoundChannelChangedHandler(sf_SoundChannelChanged);
            sf.SoundChannelChanged += handler;
            Update();
        }

        SoundFile.SoundChannelChangedHandler handler;

        void sf_SoundChannelChanged(SoundChannel c)
        {
            ID = Bass.BASS_ChannelSetFX(c.ID, BASSFXType.BASS_FX_BFX_ECHO2, 1);
        }

        public override void RemoveFromSoundFile()
        {
            if (sf != null)
            {
                Bass.BASS_ChannelRemoveFX(sf.sc.ID, ID);
                sf.SoundChannelChanged -= handler;
                sf = null;
            }
        }

        public override void Update()
        {
            Bass.BASS_FXSetParameters(ID, echo2);
        }

        [stuff.EffectProp("Dry Mix", -2, 2, 0)]
        public float Dry
        {
            get { return echo2.fDryMix; }
            set { echo2.fDryMix = value; Update(); }
        }

        [stuff.EffectProp("Wet Mix", -2, 2, 0)]
        public float Wet
        {
            get { return echo2.fWetMix; }
            set { echo2.fWetMix = value; Update(); }
        }

        [stuff.EffectProp("Feedback", -1, 1, 0)]
        public float Feedback
        {
            get { return echo2.fFeedback; }
            set { echo2.fFeedback = value; Update(); }
        }

        [stuff.EffectProp("Delay", 0, 6, 0)]
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
