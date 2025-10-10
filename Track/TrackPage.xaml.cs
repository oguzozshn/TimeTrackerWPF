using System;
using System.Windows.Controls;
using System.Windows.Threading;
using TimeTrackerWPF.History;
using TimeTrackerWPF.Shared;

namespace TimeTrackerWPF.Track
{
    public partial class TrackPage : Page
    {
        private DispatcherTimer _timer;
        private DateTime _startTime;
        private HistoryClass _history;

        public TrackPage(HistoryClass history)
        {
            InitializeComponent();
            _history = history;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += Timer_Tick;
        }

        private void StartButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _startTime = TrimToSeconds(DateTime.Now);
            _timer.Start();
        }

        private void StopButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _timer.Stop();
            var endTime = TrimToSeconds(DateTime.Now);

            string taskName = string.IsNullOrWhiteSpace(NameTextBox.Text)
                ? "Görev"
                : NameTextBox.Text;

            var record = new Record
            {
                Name = taskName,
                StartDate = _startTime,
                EndDate = endTime
            };

            _history.AddRecord(record);
        }

        private void ResetButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _timer.Stop();
            TimerText.Text = "00:00:00";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var elapsed = DateTime.Now - _startTime;
            TimerText.Text = $"{elapsed:hh\\:mm\\:ss}";
        }

        private DateTime TrimToSeconds(DateTime dateTime)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second
            );
        }
    }
}
