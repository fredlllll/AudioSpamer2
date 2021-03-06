﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass;

namespace AudioSpamer2.Effects
{
    class Distortion : AudioEffect
    {
        BASS_BFX_DISTORTION distortion;
        int ID;

        public Distortion()
        {
            distortion = new BASS_BFX_DISTORTION();
        }

        AudioClip sf=null;
        public override void ApplyToSoundFile(AudioClip sf)
        {
            this.sf = sf;
            ID = Bass.BASS_ChannelSetFX(sf.AudioStream.StreamHandle, BASSFXType.BASS_FX_BFX_DISTORTION, 1);
            handler = new AudioClip.AudioChannelChangedHandler(sf_SoundChannelChanged);
            sf.AudioStreamChanged += handler;
            Update();
        }

        AudioClip.AudioChannelChangedHandler handler;

        void sf_SoundChannelChanged(AudioStream c)
        {
            ID = Bass.BASS_ChannelSetFX(c.StreamHandle, BASSFXType.BASS_FX_BFX_DISTORTION, 1);
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
            Bass.BASS_FXSetParameters(ID, distortion);
        }

        [EffectPropertyDescription("Drive",0,5,0,100)]
        public float Drive
        {
            get { return distortion.fDrive; }
            set { distortion.fDrive = value; Update(); }
        }

        [EffectPropertyDescription("Dry Mix", -5, 5, 0, 100)]
        public float Dry
        {
            get { return distortion.fDryMix; }
            set { distortion.fDryMix = value; Update(); }
        }

        [EffectPropertyDescription("Wet Mix", -5, 5, 0, 100)]
        public float Wet
        {
            get { return distortion.fWetMix; }
            set { distortion.fWetMix = value; Update(); }
        }

        [EffectPropertyDescription("Feedback", -1, 1, 0, 100)]
        public float Feedback
        {
            get { return distortion.fFeedback; }
            set { distortion.fFeedback = value; Update(); }
        }

        [EffectPropertyDescription("Volume", 0, 2, 0, 100)]
        public float Volume
        {
            get { return distortion.fVolume; }
            set { distortion.fVolume = value; Update(); }
        }

        public override string Name
        {
            get { return "Distortion"; }
        }
    }
}
