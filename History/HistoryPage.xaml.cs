using System.Windows;
using System.Windows.Controls;
using TimeTrackerWPF.Shared;

namespace TimeTrackerWPF.History
{
    public partial class HistoryPage : Page
    {
        private readonly HistoryClass _history;

        public HistoryPage(HistoryClass history)
        {
            InitializeComponent();
            _history = history;
            DataContext = _history;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Record record)
            {
                var result = MessageBox.Show(
                    $"'{record.Name}' görevini silmek istediğine emin misin?",
                    "Silme Onayı",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    _history.DeleteRecord(record);
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Record record)
            {
                var newName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Yeni görev adını gir:",
                    "Görevi Düzenle",
                    record.Name
                );

                if (!string.IsNullOrWhiteSpace(newName) && newName != record.Name)
                {
                    var updated = new Record
                    {
                        Id = record.Id,
                        Name = newName,
                        StartDate = record.StartDate,
                        EndDate = record.EndDate
                    };

                    _history.EditRecord(updated);
                }
            }
        }
    }
}
