using System.Threading.Tasks;
using System.Windows.Forms;

namespace Salem.Controls {
    public class SalSpinner : Label {
        private bool _spinningEnabled = true;
        private char _currentGlyph = '\ue052'; //Initial value
        private Timer _timer = new Timer();
        private byte _endDelayCounter = 0;

        public int Interval { get => _timer.Interval; set => _timer.Interval = value; }
        public bool SpinningEnabled {
            get => _spinningEnabled;
            set { _timer.Enabled = _spinningEnabled = value; }
        }

        public SalSpinner() {
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            _timer.Tick += _timer_Tick;
            Interval = 30;
            Font = new System.Drawing.Font("Segoe Boot Semilight", 14);
            Text = _currentGlyph.ToString();
            _timer.Start();
        }
        private void _timer_Tick(object sender, System.EventArgs e) {
            if (_currentGlyph < '\ue0cb') {
                _currentGlyph++;
                Text = _currentGlyph.ToString();
                return;
            }

            _currentGlyph = '\ue0c7'; //First empty after end
            _endDelayCounter++;

            if (_endDelayCounter == 4) {
                _endDelayCounter = 0;
                _currentGlyph = '\ue052'; //Initial value
                Text = _currentGlyph.ToString();
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _timer.Stop();
                _timer.Tick -= _timer_Tick;
                _timer.Dispose();
                _timer = null;
            }
            
            base.Dispose(disposing);
        }

        public void Start() => SpinningEnabled = true;
        public void Stop() => SpinningEnabled = false;
        public void Reset() => _currentGlyph = '\ue052'; //Initial value
    }
}
