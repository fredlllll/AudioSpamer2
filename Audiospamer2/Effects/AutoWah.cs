using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass;

namespace AudioSpamer2.Effects
{
    public class AutoWah : AudioEffect
    {
        BASS_BFX_AUTOWAH wah;
        int ID;

        public AutoWah()
        {
            wah = new BASS_BFX_AUTOWAH();
        }

        AudioClip sf=null;
        public override void ApplyToSoundFile(AudioClip sf)
        {
            this.sf = sf;
            ID = Bass.BASS_ChannelSetFX(sf.AudioStream.StreamHandle, BASSFXType.BASS_FX_BFX_AUTOWAH, 1);
            handler = new AudioClip.AudioChannelChangedHandler(sf_SoundChannelChanged);
            sf.AudioStreamChanged += handler;
            Update();
        }

        AudioClip.AudioChannelChangedHandler handler;

        void sf_SoundChannelChanged(AudioStream c)
        {
            ID = Bass.BASS_ChannelSetFX(c.StreamHandle, BASSFXType.BASS_FX_BFX_AUTOWAH, 1);
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
            Bass.BASS_FXSetParameters(ID, wah);
        }

        [EffectPropertyDescription("Dry Mix", -2, 2, 0)]
        public float Dry
        {
            get { return wah.fDryMix; }
            set { wah.fDryMix = value; Update(); }
        }

        [EffectPropertyDescription("Wet Mix", -2, 2, 0)]
        public float Wet
        {
            get { return wah.fWetMix; }
            set { wah.fWetMix = value; Update(); }
        }

        [EffectPropertyDescription("Feedback", -1, 1, 0)]
        public float Feedback
        {
            get { return wah.fFeedback; }
            set { wah.fFeedback = value; Update(); }
        }

        [EffectPropertyDescription("Frequency", 0, 1000, 0)]
        public float Frequency
        {
            get { return wah.fFreq; }
            set { wah.fFreq = value; Update(); }
        }

        [EffectPropertyDescription("Range", 0, 10, 0)]
        public float Range
        {
            get { return wah.fRange; }
            set { wah.fRange = value; Update(); }
        }

        [EffectPropertyDescription("Rate", 0, 10, 0)]
        public float Rate
        {
            get { return wah.fRate; }
            set { wah.fRate = value; Update(); }
        }

        public override string Name
        {
            get { return "Wah"; }
        }
    }
}
