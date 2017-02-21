using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioSpamer2.Effects
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EffectPropertyDescription : Attribute
    {
        public string Label { get; set; } = "Effect Property";
        public float MinValue { get; set; } = 0;
        public float MaxValue { get; set; } = 1;
        public float DefaultValue { get; set; } = 0.5f;
        public float TickInterval { get; set; } = 0.1f;
        public bool IsInteger { get; set; } = false;
        public int Resolution { get; set; } = 10;

        public EffectPropertyDescription(string label, float min, float max, float defaultValue,int resolution = 10, bool isInt=false)
        {
            this.Label = label;
            this.MinValue = min;
            this.MaxValue = max;
            this.DefaultValue = defaultValue;
            this.IsInteger = isInt;
            this.Resolution = resolution;
        }
    }
}
