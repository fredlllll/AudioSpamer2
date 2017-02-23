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

        AudioClip sf=null;
        public override void ApplyToSoundFile(AudioClip sf)
        {
            this.sf = sf;
            ID = Bass.BASS_ChannelSetFX(sf.AudioStream.StreamHandle, BASSFXType.BASS_FX_BFX_LPF, 1);
            handler = new AudioClip.AudioChannelChangedHandler(sf_SoundChannelChanged);
            sf.AudioStreamChanged += handler;
            Update();
        }

        AudioClip.AudioChannelChangedHandler handler;

        void sf_SoundChannelChanged(AudioStream c)
        {
            ID = Bass.BASS_ChannelSetFX(c.StreamHandle, BASSFXType.BASS_FX_BFX_LPF, 1);
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
            Bass.BASS_FXSetParameters(ID, lpf);
        }

        [EffectPropertyDescription("Cut Off Frequency",1,10000,200,1)]
        public float CutOff
        {
            get { return lpf.fCutOffFreq; }
            set { lpf.fCutOffFreq = value; Update(); }
        }

        [EffectPropertyDescription("Resonance", 0.01f, 10, 2,100)]
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
