using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Wprowadź ciąg bitów (maksymalnie 96 bitów): ");
            string bits = Console.ReadLine().Trim();
            if (bits.Length > 96)
            {
                Console.WriteLine("Ciąg bitów nie może mieć więcej niż 96 bitów");
                continue;
            }

            Console.Write("Wprowadź liczbę powtórzeń: ");
            string repetitionsStr = Console.ReadLine().Trim();
            if (!int.TryParse(repetitionsStr, out int repetitions))
            {
                Console.WriteLine("Nieprawidłowa liczba powtórzeń");
                continue;
            }

            var startTime = DateTime.Now;
            int crc = 0;
            for (int i = 0; i < repetitions; i++)
            {
                crc = CalculateCrc(bits);
            }
            var endTime = DateTime.Now;

            Console.WriteLine($"Suma kontrolna (CRC) dla ciągu bitów {bits} to {crc:X}");
            Console.WriteLine($"Czas wykonania dla {repetitions} powtórzeń: {(endTime - startTime).TotalSeconds:F4} sekund");

            Console.Write("Czy chcesz obliczyć CRC jeszcze raz? (t/n): ");
            string continueInput = Console.ReadLine().Trim().ToLower();
            if (continueInput != "t")
            {
                break;
            }
        }
    }

    static int CalculateCrc(string bits)
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
                crcRegister = crcRegister ^ polynomial;
            }
        }
        return crcRegister;
    }
}
