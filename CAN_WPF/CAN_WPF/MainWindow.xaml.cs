using System;
using System.Diagnostics;
using System.Windows;

namespace CrcCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            string bits = textBoxBits.Text.Trim();
            if (bits.Length > 96)
            {
                MessageBox.Show("Ciąg bitów nie może mieć więcej niż 96 bitów.");
                return;
            }

            if (!int.TryParse(textBoxRepetitions.Text.Trim(), out int repetitions))
            {
                MessageBox.Show("Nieprawidłowa liczba powtórzeń.");
                return;
            }

            if(repetitions > 1000000000) 
            {
                MessageBox.Show("Nieprawidłowa liczba powtórzeń.");
                return;
            }
            // worm-up execution
            CalculateCrc(bits);

            var stopwatch = new Stopwatch();
            int crc = 0;

            stopwatch.Start();
            for (int i = 0; i < repetitions; i++)
            {
                crc = CalculateCrc(bits);
            }
            stopwatch.Stop();

            labelTotalTime.Content = $"{stopwatch.ElapsedMilliseconds} ms";
            labelIterationTime.Content = $"{(stopwatch.Elapsed.TotalMilliseconds / repetitions):F8} ms";
            labelCrc.Content = $"{crc:X}";
        }

        private int CalculateCrc(string bits)
        {
            int crcRegister = 0;
            int polynomial = 0x4599;
            foreach (char bitChar in bits)
            {
                int nextBit = int.Parse(bitChar.ToString());
                int crcNext = nextBit ^ (crcRegister >> 14);
                crcRegister = (crcRegister << 1) & 0x7FFF;
                if (crcNext != 0)
                {
                    crcRegister ^= polynomial;
                }
            }
            return crcRegister;
        }
    }
}
