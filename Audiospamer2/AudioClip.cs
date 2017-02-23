using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;

namespace AudioSpamer2
{
    public class AudioClip
    {
        AudioStream decodingAudioStream;
        AudioStream audioStream;

        public delegate void AudioChannelChangedHandler(AudioStream c);
        public event AudioChannelChangedHandler AudioStreamChanged;

        public long MarkA { get; set; }
        public long MarkB { get; set; }

        public bool Loop { get; set; } = true;

        public AudioStream AudioStream
        {
            get
            {
                return audioStream;
            }
            protected set
            {
                audioStream = value;
                AudioStreamChanged?.Invoke(audioStream);
            }
        }

        public string Path { get; protected set; }

        public bool IsReversed { get; protected set; }
        public bool IsPlayingBetweenMarks { get; protected set; }

        public AudioClip(string path)
        {
            this.Path = path;
            int decodingStreamHandle = Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_STREAM_DECODE);
            decodingAudioStream = new AudioStream(decodingStreamHandle);
            int audioStreamHandle = Un4seen.Bass.AddOn.Fx.BassFx.BASS_FX_ReverseCreate(decodingStreamHandle, 0.5f, BASSFlag.BASS_DEFAULT);//other call had flag decode.. i wonder why
            AudioStream = new AudioStream(audioStreamHandle);
        }

        public void Reverse()
        {
            if(!IsReversed)
            {
                Bass.BASS_ChannelSetAttribute(AudioStream.StreamHandle, BASSAttribute.BASS_ATTRIB_REVERSE_DIR, -1f);
            }
            else
            {
                Bass.BASS_ChannelSetAttribute(AudioStream.StreamHandle, BASSAttribute.BASS_ATTRIB_REVERSE_DIR, 1f);
            }
            IsReversed = !IsReversed;


            /*if(!reversed)
            {
                reversed = true;
                long here = AudioStream.Position;
                Free();
                revStream = Un4seen.Bass.AddOn.Fx.BassFx.BASS_FX_ReverseCreate(decodingStreamHandle, 1f, BASSFlag.BASS_STREAM_DECODE);
                audioStreamHandle = Un4seen.Bass.AddOn.Fx.BassFx.BASS_FX_TempoCreate(revStream, BASSFlag.BASS_DEFAULT);
                AudioStream = new AudioStream(audioStreamHandle);
                AudioStream.Position = here;
                AudioStream.Play();
            }
            else
            {
                reversed = false;
                long here = AudioStream.Position;
                Free();
                Bass.BASS_StreamFree(revStream);
                audioStreamHandle = Un4seen.Bass.AddOn.Fx.BassFx.BASS_FX_TempoCreate(decodingStreamHandle, BASSFlag.BASS_DEFAULT);
                AudioStream = new AudioStream(audioStreamHandle);
                AudioStream.Position = here;
                AudioStream.Play();
            }
            */
        }

        public void CheckForEndAndReplay()
        {
            if(!IsReversed)
            {
                if(AudioStream.Position >= AudioStream.Length && Loop)
                {
                    Bass.BASS_ChannelPlay(AudioStream.StreamHandle, false);
                }

                if(IsPlayingBetweenMarks && (AudioStream.Position >= MarkB || AudioStream.Position < MarkA))
                {
                    AudioStream.Position = MarkA;
                }
            }
            else
            {
                if(AudioStream.Position <= 0 && Loop)
                {
                    AudioStream.Position = AudioStream.Length;
                    Bass.BASS_ChannelPlay(AudioStream.StreamHandle, false);
                }

                if(IsPlayingBetweenMarks && (AudioStream.Position <= MarkA || AudioStream.Position > MarkB))
                {
                    AudioStream.Position = MarkB;
                }
            }
        }

        public void PlayBetweenMarks()
        {
            IsPlayingBetweenMarks = true;
            if(MarkB < MarkA)
            {
                long temp = MarkB;
                this.MarkB = MarkA;
                this.MarkA = temp;
            }
        }

        public void StopPlayingBetweenMarks()
        {
            IsPlayingBetweenMarks = false;
        }
    }
}
