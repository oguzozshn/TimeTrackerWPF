using System.Windows;
using TimeTrackerWPF.History;
using TimeTrackerWPF.Track;
using TimeTrackerWPF;

namespace TimeTrackerWPF
{
    public partial class MainWindow : Window
    {
        private readonly HistoryClass _history = new HistoryClass();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new TrackPage(_history));
        }

        private void Track_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TrackPage(_history));
        }
        private void History_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new History.HistoryPage(_history));
        }
    }

}
