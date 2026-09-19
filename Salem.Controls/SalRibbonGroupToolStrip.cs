using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Salem.Controls {
    [ToolboxItem(true)]
    public class SalRibbonGroupToolStrip : ToolStrip {
        private enum LauncherStates : byte { Normal, Hover = 223, Pressed = 200 }

        public event EventHandler LauncherClicked;

        private LauncherStates _launcherState = LauncherStates.Normal;
        private bool _showLauncher = true, _displayLauncherToolTip = false;
        private Rectangle _launcherRect, _launcherHitRect;
        private const byte LAUNCHER_SIZE = 8, LAUNCHER_ARROW_GAP = 3, LAUNCHER_RIGHT_MARGIN = 6, LAUNCHER_BOTTOM_MARGIN = 6, LAUNCHER_HIGHLIGHT_INFLATION = 4;
        private readonly Timer _launcherDelayTimer;
        private ToolTip _launcherToolTip;
        private short _buttonPressNudgeAmount = 0;

        public override string Text { 
            get => base.Text; 
            set {
                base.Text = value;
                Invalidate();
            }
        }

        public bool LauncherEnabled {
            get => _showLauncher;
            set {
                _showLauncher = value;
                Invalidate();
            }
        }

        public string LauncherToolTipText { get; set; }

        public ToolTip LauncherToolTip {
            get => _launcherToolTip;
            set {
                _launcherToolTip = value;

                if (value != null)
                    _launcherDelayTimer.Interval = value.InitialDelay;
            }
        }

        public short ButtonPressNudgeAmount {
            get => _buttonPressNudgeAmount;
            set => ((OfficeFlatToolStripRenderer) Renderer).ButtonPressNudgeAmount = _buttonPressNudgeAmount = value;
        }

        public SalRibbonGroupToolStrip() {
            _launcherDelayTimer = new Timer();

            Padding = new Padding(0, 0, 0, 22);
            Renderer = new OfficeFlatToolStripRenderer(_buttonPressNudgeAmount);
            Dock = DockStyle.Left;
            LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            _launcherDelayTimer.Tick += _launcherDelayTimer_Tick;

            _RefreshLauncherRect();
        }

        private void _launcherDelayTimer_Tick(object sender, EventArgs e) {
            _launcherDelayTimer.Stop();
            _displayLauncherToolTip = false;
            Point pnt = PointToClient(Cursor.Position);

            if (_launcherToolTip.IsBalloon) { //These values are harcoded for now...
                pnt.X -= 15;
                pnt.Y -= 100;
            } else
                pnt.Y += 18;

            _launcherToolTip.Show(LauncherToolTipText, this, pnt, _launcherToolTip.AutoPopDelay);
        }

        private void _RefreshLauncherRect() {
            _launcherRect = new Rectangle(ClientSize.Width - LAUNCHER_SIZE - LAUNCHER_RIGHT_MARGIN - 1, ClientSize.Height - LAUNCHER_SIZE - LAUNCHER_BOTTOM_MARGIN - 1, LAUNCHER_SIZE, LAUNCHER_SIZE);
            _launcherHitRect = new Rectangle(ClientSize.Width - LAUNCHER_SIZE - LAUNCHER_RIGHT_MARGIN - LAUNCHER_HIGHLIGHT_INFLATION - 1, ClientSize.Height - LAUNCHER_SIZE - LAUNCHER_BOTTOM_MARGIN - LAUNCHER_HIGHLIGHT_INFLATION - 1, LAUNCHER_SIZE + 2 * LAUNCHER_HIGHLIGHT_INFLATION, LAUNCHER_SIZE + 2 * LAUNCHER_HIGHLIGHT_INFLATION);
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            e.Graphics.DrawLine(Pens.LightGray, ClientSize.Width - 1, LAUNCHER_BOTTOM_MARGIN, ClientSize.Width - 1, ClientSize.Height - LAUNCHER_BOTTOM_MARGIN);

            using (var _font = new Font(Font.FontFamily, Font.Size - 0.5F))
            using (var _stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far })
                e.Graphics.DrawString(Text, _font, Brushes.DimGray, ClientRectangle, _stringFormat);

            if (_showLauncher) {
                if (_launcherState != LauncherStates.Normal) {
                    byte _greyValue = (byte) _launcherState;
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(_greyValue, _greyValue, _greyValue)), _launcherHitRect);
                }

                _DrawLauncherShape(e.Graphics);
            }
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            _RefreshLauncherRect();
        }

        private void _DrawLauncherShape(Graphics graphics) {
            using (Pen _pen = new Pen(Color.DimGray)) {
                graphics.DrawLine(_pen, _launcherRect.X, _launcherRect.Y, _launcherRect.X + _launcherRect.Width, _launcherRect.Y);
                graphics.DrawLine(_pen, _launcherRect.X, _launcherRect.Y, _launcherRect.X, _launcherRect.Y + _launcherRect.Height);
                graphics.DrawLine(_pen, _launcherRect.X + LAUNCHER_ARROW_GAP, _launcherRect.Y + LAUNCHER_ARROW_GAP, _launcherRect.X + _launcherRect.Width, _launcherRect.Y + _launcherRect.Height);
                graphics.DrawLine(_pen, _launcherRect.X + LAUNCHER_ARROW_GAP, _launcherRect.Y + LAUNCHER_SIZE, _launcherRect.X + LAUNCHER_SIZE, _launcherRect.Y + LAUNCHER_SIZE);
                graphics.DrawLine(_pen, _launcherRect.X + LAUNCHER_SIZE, _launcherRect.Y + LAUNCHER_ARROW_GAP, _launcherRect.X + LAUNCHER_SIZE, _launcherRect.Y + LAUNCHER_SIZE);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            if (_showLauncher) {
                if (_launcherHitRect.Contains(e.Location)) {
                    if (_launcherState == LauncherStates.Normal) {
                        _launcherState = LauncherStates.Hover;
                        Invalidate();
                    }

                    if (_launcherToolTip != null && !_launcherDelayTimer.Enabled && _displayLauncherToolTip) {
                        _displayLauncherToolTip = false;
                        _launcherDelayTimer.Start();
                    }
                } else {
                    if (_launcherState == LauncherStates.Hover) {
                        _launcherState = LauncherStates.Normal;
                        Invalidate();
                    }
                    
                    _launcherToolTip?.Hide(this);
                    _launcherDelayTimer.Enabled = !(_displayLauncherToolTip = true);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);

            _launcherState = LauncherStates.Normal;
            Invalidate();

            _launcherToolTip?.Hide(this);
            _launcherDelayTimer.Enabled = !(_displayLauncherToolTip = true);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);

            if (_showLauncher && _launcherState != LauncherStates.Pressed && _launcherHitRect.Contains(e.Location)) {
                _launcherState = LauncherStates.Pressed;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            
            if (_showLauncher && _launcherState == LauncherStates.Pressed) {
                _launcherState = _launcherHitRect.Contains(e.Location) ? LauncherStates.Hover : LauncherStates.Normal;
                Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            
            if (_showLauncher && _launcherHitRect.Contains(e.Location))
                LauncherClicked?.Invoke(this, EventArgs.Empty);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _launcherDelayTimer.Stop();
                _launcherDelayTimer.Tick -= _launcherDelayTimer_Tick;
                _launcherDelayTimer.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
