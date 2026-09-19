using System;
using System.Threading.Tasks;

namespace Salem.Utils {
    /// <summary>
    /// Provides a light-weight asynchronous timer that can be started and stopped, with a configurable interval and events for
    /// tick, enable state changes, and interval updates.
    /// </summary>
    /// <remarks>Raises events when the timer ticks, when the enabled state changes, and when the interval is
    /// modified.</remarks>
    public class SimpleAsyncTimer {
        #region IntervalChangedEventArgs
        /// <summary>
        /// Provides data for the event that occurs when an interval value changes.
        /// </summary>
        /// <remarks>Contains the previous and current values of the interval.</remarks>
        public class IntervalChangedEventArgs : EventArgs {
            /// <summary>
            /// Gets the old value of the interval.
            /// </summary>
            public int OldValue { get; }
            /// <summary>
            /// Gets the current value of the interval
            /// </summary>
            public int CurrentValue { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="IntervalChangedEventArgs"/> class with the specified old and current
            /// interval values.
            /// </summary>
            /// <param name="oldValue">The previous interval value.</param>
            /// <param name="currentValue">The new interval value.</param>
            public IntervalChangedEventArgs(int oldValue, int currentValue) {
                OldValue = oldValue;
                CurrentValue = currentValue;
            }
        }
        #endregion

        /// <summary>
        /// Provides data for a <see cref="Tick"/> event, including the time the event was raised.
        /// </summary>
        public class TickEventArgs : EventArgs {
            /// <summary>
            /// Gets the data/time when the <see cref="Tick"/> event was raised.
            /// </summary>
            public DateTime SignalTime { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="TickEventArgs"/> class with the specified signal time.
            /// </summary>
            /// <param name="signalTime">The time at which the signal occurred.</param>
            public TickEventArgs(DateTime signalTime) => SignalTime = signalTime;
        }

        #region Events
        /// <summary>
        /// Occurs when the timer interval elapses.
        /// </summary>
        public event EventHandler<TickEventArgs> Tick;
        /// <summary>
        /// Occurs when the enabled state of the instance changes.
        /// </summary>
        public event EventHandler EnabledChanged;
        /// <summary>
        /// Occurs when the timer interval is modified.
        /// </summary>
        public event EventHandler<IntervalChangedEventArgs> IntervalChanged;
        #endregion

        #region Private Fields
        private bool _enabled = false;
        private int _interval = 100;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the timer interval in milliseconds.
        /// </summary>
        public int Interval {
            get => _interval;
            set {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The value must be greater than zero.");

                if (_interval != value) {
                    int _oldValueCache = _interval;
                    _interval = value;

                    OnIntervalChanged(new IntervalChangedEventArgs(_oldValueCache, _interval));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the timer is enabled.
        /// </summary>
        public bool Enabled {
            get => _enabled;
            set {
                if (_enabled != value) {
                    _enabled = value;

                    if (_enabled)
                        RunTimer();

                    OnEnabledChanged(EventArgs.Empty);
                }
            }
        }
        #endregion

        #region Identity
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAsyncTimer"/> class.
        /// </summary>
        public SimpleAsyncTimer() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAsyncTimer"/> class with the specified interval in milliseconds.
        /// </summary>
        /// <param name="interval">The rate at which the <see cref="Tick"/> event is raised in milliseconds</param>
        public SimpleAsyncTimer(int interval) {
            if (interval <= 0)
                throw new ArgumentOutOfRangeException(nameof(interval), interval, "The value must be greater than zero.");

            _interval = interval;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAsyncTimer"/> class with the specified interval as a <see cref="TimeSpan"/> instance.
        /// </summary>
        /// <param name="interval">The rate at which the <see cref="Tick"/> event is raised as a <see cref="TimeSpan"/> instance.</param>
        public SimpleAsyncTimer(TimeSpan interval) {
            int _totalMilliSecs = TotalMillSecs(interval);

            if (_totalMilliSecs <= 0)
                throw new ArgumentOutOfRangeException(nameof(interval), interval, "The value must be greater than zero.");

            _interval = _totalMilliSecs;
        }
        #endregion

        #region Implementation
        private async void RunTimer() {
            while (_enabled) {
                await Task.Delay(Interval);

                if (_enabled)
                    OnTick(new TickEventArgs(DateTime.Now));
            }
        }

        private int TotalMillSecs(TimeSpan timeSpan) => Convert.ToInt32(Math.Round(timeSpan.TotalMilliseconds, MidpointRounding.AwayFromZero));
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start() => Enabled = true;

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop() => Enabled = false;

        /// <summary>
        /// Sets the timer interval to the specified time span.
        /// </summary>
        /// <param name="interval">The time span to use as the new interval.</param>
        public void SetInterval(TimeSpan interval) => Interval = TotalMillSecs(interval);
        #endregion

        #region Event Raisers
        /// <summary>
        /// Raises the Tick event.
        /// </summary>
        /// <param name="e">The event data associated with the Tick event.</param>
        protected virtual void OnTick(TickEventArgs e) => Tick?.Invoke(this, e);

        /// <summary>
        /// Raises the EnabledChanged event.
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        protected virtual void OnEnabledChanged(EventArgs e) => EnabledChanged?.Invoke(this, e);

        /// <summary>
        /// Raises the IntervalChanged event.
        /// </summary>
        /// <param name="e">An IntervalChangedEventArgs object that contains the event data.</param>
        protected virtual void OnIntervalChanged(IntervalChangedEventArgs e) => IntervalChanged?.Invoke(this, e);
        #endregion
    }
}
