using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass;

namespace AudioSpamer2.Effects
{
    class LPF : AudioEffect
    {
        BASS_BFX_LPF lpf;
        int ID;

        public LPF()
        {
            lpf = new BASS_BFX_LPF();
        }

        SoundFile sf=null;
        public override void ApplyToSoundFile(SoundFile sf)
        {
            this.sf = sf;
            ID = Bass.BASS_ChannelSetFX(sf.sc.ID, BASSFXType.BASS_FX_BFX_LPF, 1);
            handler = new SoundFile.SoundChannelChangedHandler(sf_SoundChannelChanged);
            sf.SoundChannelChanged += handler;
            Update();
        }

        SoundFile.SoundChannelChangedHandler handler;

        void sf_SoundChannelChanged(SoundChannel c)
        {
            ID = Bass.BASS_ChannelSetFX(c.ID, BASSFXType.BASS_FX_BFX_LPF, 1);
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
            Bass.BASS_FXSetParameters(ID, lpf);
        }

        [stuff.EffectProp("Cut Off Frequency",1,10000,200,1)]
        public float CutOff
        {
            get { return lpf.fCutOffFreq; }
            set { lpf.fCutOffFreq = value; Update(); }
        }

        [stuff.EffectProp("Resonance", 0.01f, 10, 2,100)]
        public float Resonance
        {
            get { return lpf.fResonance; }
            set { lpf.fResonance = value; Update(); }
        }

        public override string Name
        {
            get { return "LPF"; }
        }
    }
}
