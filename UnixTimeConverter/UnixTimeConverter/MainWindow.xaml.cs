using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace TimestampConverter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!DatePicker.SelectedDate.HasValue || !TimePicker.Value.HasValue)
                {
                    throw new ArgumentException("Please select a valid date and time.");
                }

                DateTime date = DatePicker.SelectedDate.Value;
                DateTime time = TimePicker.Value.Value;

                DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);

                // Get selected time zone offset
                ComboBoxItem selectedTimeZoneItem = (ComboBoxItem)TimeZoneComboBox.SelectedItem;
                int timeZoneOffset = int.Parse((string)selectedTimeZoneItem.Tag);

                // Convert to Unix timestamp with the selected time zone
                DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime, TimeSpan.FromHours(timeZoneOffset));
                long unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();

                // Format for Discord
                string discordTimestamp = $"<t:{unixTimestamp}>";

                // Display result
                ResultTextBox.Text = discordTimestamp;
            }
            catch (Exception ex)
            {
                ResultTextBox.Text = $"Error: {ex.Message}";
            }
        }
    }
}
