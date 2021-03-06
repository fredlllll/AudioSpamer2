﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass;

namespace AudioSpamer2.Effects
{
    class Phaser : AudioEffect
    {
        BASS_BFX_PHASER phaser;
        int ID;

        public Phaser()
        {
            phaser = new BASS_BFX_PHASER();
        }

        AudioClip sf=null;
        public override void ApplyToSoundFile(AudioClip sf)
        {
            this.sf = sf;
            ID = Bass.BASS_ChannelSetFX(sf.AudioStream.StreamHandle, BASSFXType.BASS_FX_BFX_PHASER, 1);
            handler = new AudioClip.AudioChannelChangedHandler(sf_SoundChannelChanged);
            sf.AudioStreamChanged += handler;
            Update();
        }

        AudioClip.AudioChannelChangedHandler handler;

        void sf_SoundChannelChanged(AudioStream c)
        {
            ID = Bass.BASS_ChannelSetFX(c.StreamHandle, BASSFXType.BASS_FX_BFX_PHASER, 1);
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
            Bass.BASS_FXSetParameters(ID, phaser);
        }

        [EffectPropertyDescription("Dry Mix", -2, 2, 0)]
        public float Dry
        {
            get { return phaser.fDryMix; }
            set { phaser.fDryMix = value; Update(); }
        }

        [EffectPropertyDescription("Wet Mix", -2, 2, 0)]
        public float Wet
        {
            get { return phaser.fWetMix; }
            set { phaser.fWetMix = value; Update(); }
        }

        [EffectPropertyDescription("Feedback", -1, 1, 0)]
        public float Feedback
        {
            get { return phaser.fFeedback; }
            set { phaser.fFeedback = value; Update(); }
        }

        [EffectPropertyDescription("Frequency", 0, 1000, 0)]
        public float Frequency
        {
            get { return phaser.fFreq; }
            set { phaser.fFreq = value; Update(); }
        }

        [EffectPropertyDescription("Range", 0, 10, 0)]
        public float Range
        {
            get { return phaser.fRange; }
            set { phaser.fRange = value; Update(); }
        }

        [EffectPropertyDescription("Rate", 0, 10, 0)]
        public float Rate
        {
            get { return phaser.fRate; }
            set { phaser.fRate = value; Update(); }
        }

        public override string Name
        {
            get { return "Phaser"; }
        }
    }
}
