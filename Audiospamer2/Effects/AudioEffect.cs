using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AudioSpamer2.Effects
{
    public abstract class AudioEffect
    {
        public abstract void ApplyToSoundFile(SoundFile sf);
        public abstract void RemoveFromSoundFile();
        public abstract void Update();
        public abstract string Name
        {
            get;
        }

        public TabPage Page = null;
        public bool Enabled = false;
    }
}
