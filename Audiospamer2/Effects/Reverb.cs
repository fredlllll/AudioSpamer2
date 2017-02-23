using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass;

namespace AudioSpamer2.Effects
{
    public class Reverb:AudioEffect
    {
        BASS_BFX_FREEVERB reverb;
        int ID;

        public Reverb()
        {
            reverb = new BASS_BFX_FREEVERB();
        }

        AudioClip sf=null;
        public override void ApplyToSoundFile(AudioClip sf)
        {
            this.sf = sf;
            ID = Bass.BASS_ChannelSetFX(sf.AudioStream.StreamHandle, BASSFXType.BASS_FX_BFX_FREEVERB, 1);
            handler = new AudioClip.AudioChannelChangedHandler(sf_SoundChannelChanged);
            sf.AudioStreamChanged += handler;
            Update();
        }

        AudioClip.AudioChannelChangedHandler handler;

        void sf_SoundChannelChanged(AudioStream c)
        {
            ID = Bass.BASS_ChannelSetFX(c.StreamHandle, BASSFXType.BASS_FX_BFX_FREEVERB, 1);
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
            Bass.BASS_FXSetParameters(ID, reverb);
        }

        [EffectPropertyDescription("Level", 0, 100, 0)]
        public float Level
        {
            get { return reverb.fLevel; }
            set { reverb.fLevel = value; Update(); }
        }

        [EffectPropertyDescription("Delay", 1200, 30000, 1200, 1, true)]
        public int Delay
        {
            get { return reverb.lDelay; }
            set { reverb.lDelay = value; Update(); }
        }

        public override string Name
        {
            get { return "Reverb"; }
        }
    }
}
