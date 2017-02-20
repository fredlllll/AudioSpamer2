using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioSpamer2.Effects.stuff
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EffectProp : Attribute
    {
        public String Label;
        public float minValue;
        public float maxValue;
        public float defaultValue;
        public bool isInt;
        public int resolution;
        public EffectProp(String label, float min, float max, float defaultValue,int resolution = 10, bool isInt=false)
        {
            this.Label = label;
            this.minValue = min;
            this.maxValue = max;
            this.defaultValue = defaultValue;
            this.isInt = isInt;
            this.resolution = resolution;
        }
    }
}
