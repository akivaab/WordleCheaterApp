using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WordleCheaterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int start = 195;

        private Dictionary<int, int> daysUntilMonth = new Dictionary<int, int>();

        public MainWindow()
        {
            InitializeComponent();
            
            int numDays = 0;
            for (int month = 1; month <= 12; ++month)
            {
                daysUntilMonth.Add(month, numDays);
                numDays += DateTime.DaysInMonth(2022, month);
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            int month = MonthlyCalendar.SelectedDate.Value.Month;
            int day = MonthlyCalendar.SelectedDate.Value.Day;
            string todaysWord = getDailyWord(month, day).ToUpper();

            List<TextBlock> textBlocks = new() { Char1, Char2, Char3, Char4, Char5 };
            for (int i = 0; i < 5; ++i)
            {
                displayCharacter(textBlocks[i], todaysWord.Substring(i + 1, 1));
            }
            Letters.Background = Brushes.Green;
        }

        private string getDailyWord(int month, int day)
        {
            try
            {
                string textFile = @"wordle_list.txt";
                if ((bool)Original.IsChecked)
                {
                    textFile = @"wordle_list_original.txt";
                }

                string allWords = File.ReadAllText(textFile);
                string[] words = allWords.Replace(@"""", "").Split(",");
                return words[start + daysUntilMonth[month] + day];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private void displayCharacter(TextBlock textBlock, string text)
        {
            textBlock.Text = text;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
