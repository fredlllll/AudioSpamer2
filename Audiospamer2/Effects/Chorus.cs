using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass;

namespace AudioSpamer2.Effects
{
    public class Chorus :AudioEffect
    {
        BASS_BFX_CHORUS chorus;
        int ID;

        public Chorus()
        {
            chorus = new BASS_BFX_CHORUS();
        }

        SoundFile sf=null;
        public override void ApplyToSoundFile(SoundFile sf)
        {
            this.sf = sf;
            ID = Bass.BASS_ChannelSetFX(sf.sc.ID, BASSFXType.BASS_FX_BFX_CHORUS, 1);
            handler = new SoundFile.SoundChannelChangedHandler(sf_SoundChannelChanged);
            sf.SoundChannelChanged += handler;
            Update();
        }

        SoundFile.SoundChannelChangedHandler handler;

        void sf_SoundChannelChanged(SoundChannel c)
        {
            ID = Bass.BASS_ChannelSetFX(c.ID, BASSFXType.BASS_FX_BFX_CHORUS, 1);
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
            Bass.BASS_FXSetParameters(ID, chorus);
        }

        [stuff.EffectProp("Dry Mix", -2, 2, 0)]
        public float Dry
        {
            get { return chorus.fDryMix; }
            set { chorus.fDryMix = value; Update(); }
        }

        [stuff.EffectProp("Wet Mix", -2, 2, 0)]
        public float Wet
        {
            get { return chorus.fWetMix; }
            set { chorus.fWetMix = value; Update(); }
        }

        [stuff.EffectProp("Feedback", -1, 1, 0)]
        public float Feedback
        {
            get { return chorus.fFeedback; }
            set { chorus.fFeedback = value; Update(); }
        }

        [stuff.EffectProp("Rate", 0, 10, 0)]
        public float Rate
        {
            get { return chorus.fRate; }
            set { chorus.fRate = value; Update(); }
        }

        [stuff.EffectProp("Min Sweep", 0, 6000, 0)]
        public float MinSweep
        {
            get { return chorus.fMinSweep; }
            set { chorus.fMinSweep = value; Update(); }
        }

        [stuff.EffectProp("Max Sweep", 0, 6000, 0)]
        public float MaxSweep
        {
            get { return chorus.fMaxSweep; }
            set { chorus.fMaxSweep = value; Update(); }
        }

        public override string Name
        {
            get { return "Chorus"; }
        }
    }
}
