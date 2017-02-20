using System.Windows.Forms;

namespace AudioSpamer2
{
    public partial class LabeledTrackBar : UserControl
    {
        public TrackBar TrackBar
        {
            get
            {
                return trackBar;
            }
        }

        public int LargeChange { get { return trackBar.LargeChange; } set { trackBar.LargeChange = value; } }
        public int Maximum { get { return trackBar.Maximum; } set { trackBar.Maximum = value; } }
        public int Minimum { get { return trackBar.Minimum; } set { trackBar.Minimum = value; } }
        public int SmallChange { get { return trackBar.SmallChange; } set { trackBar.SmallChange = value; } }
        public int TickFrequency { get { return trackBar.TickFrequency; } set { trackBar.TickFrequency = value; } }
        public TickStyle TickStyle { get { return trackBar.TickStyle; } set { trackBar.TickStyle = value; } }
        public int Value { get { return trackBar.Value; } set { trackBar.Value = value; } }

        public string LabelText { get { return label.Text; } set { label.Text = value; } }

        public LabeledTrackBar()
        {
            InitializeComponent();
        }
    }
}
