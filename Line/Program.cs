using System;
using System.Collections.Generic;
using System.Globalization;

namespace Line
{
    class Program
    {
        const double Precision = 0.001;
        const int OutputMaxRounding = 3;
        
        static void Main(string[] args)
        {
            WriteLine("Witaj.");

            bool userWantContinue;
            do
            {
                WriteLine($"Uwaga!. Maksymalna precyzja zmiennych to {Precision}.");
                var a = GetParameter("a");
                var b = GetParameter("b");
                var c = GetParameter("c");

                var results = GetResults(a, b, c);
                Print(results);

                userWantContinue = PrintContinue();
                Console.Clear();
            } while (userWantContinue);

            WriteLine("Exited...");
            Console.ReadKey();
        }

        private static bool PrintContinue()
        {
            bool userWantContinune = true, isProperLetterEnter;
            do
            {
                WriteLine("Sprawdzać dalej? (t/n)");
                var key = Console.ReadKey().KeyChar.ToString().ToLower();
                WriteLine();
                isProperLetterEnter = key == "t" || key == "n";
                if (key == "n")
                {
                    userWantContinune = false;
                }
            } while (!isProperLetterEnter);

            return userWantContinune;
        }

        private static double GetParameter(string parameter)
        {
            double param;
            bool parsed;
            do
            {
                WriteLine($"Podaj parametr: {parameter}");

                parsed = double.TryParse(Console.ReadLine(), out param);

                if (!parsed)
                {
                    WriteLine("Podano niewłaściwy paramentr liczbowy.");
                }

            } while (!parsed);
            
            return param; 
        }

        private static void Print(IReadOnlyList<string> results)
        {
            foreach (var item in results)
            {
                WriteLine(item);
            }
        }
        
        private static List<string> GetResults(double a, double b, double c)
        {
            bool aIsZero = Math.Abs(a) < Precision,
                bIsZero = Math.Abs(b) < Precision,
                cIsZero = Math.Abs(c) < Precision;

            var result = new List<string>();
            if (aIsZero && bIsZero && cIsZero)
            {
                result.Add("Nieskończenie wiele rozwiązań.");
                return result;
            }
            if (aIsZero && bIsZero && !cIsZero)
            {
                result.Add("Sprzeczność.");
                return result;
            }
            if (aIsZero & !bIsZero)
            {
                var x = -c/b;
                var xRounded = Math.Round(x, OutputMaxRounding, MidpointRounding.AwayFromZero);

                result.Add($"Wynik x={xRounded}");
                return result;
            }

            var delta = b*b - 4.0*a*c;
            
            if (Math.Abs(delta) < Precision)
            {
                var x = -b/2*a;
                var xRounded = Math.Round(x, OutputMaxRounding, MidpointRounding.AwayFromZero);

                result.Add($"Wynik x={xRounded}");
                return result;
            }

            if (delta > 0)
            {
                var x1 = (-b + Math.Sqrt(delta))/2.0*a;
                var x2 = (-b - Math.Sqrt(delta))/2.0*a;

                var x1Rounded = Math.Round(x1, OutputMaxRounding, MidpointRounding.AwayFromZero);
                var x2Rounded = Math.Round(x2, OutputMaxRounding, MidpointRounding.AwayFromZero);

                result.Add($"Wynik x1={x1Rounded}, x2={x2Rounded}");
                return result;
            }

            if (delta < 0)
            {
                var x1 = (-b + Math.Sqrt(Math.Abs(delta)))/2.0*a;
                var x2 = (-b - Math.Sqrt(Math.Abs(delta)))/2.0*a;

                var x1Rounded = Math.Round(x1, OutputMaxRounding, MidpointRounding.AwayFromZero);
                var x2Rounded = Math.Round(x2, OutputMaxRounding, MidpointRounding.AwayFromZero);

                result.Add($"Wynik x1={x1Rounded}i, x2={x2Rounded}i");
                return result;
            }

            result.Add("Brak wyników.");
            return result;
        }

        private static void WriteLine(string line="")
        {
            Console.WriteLine(line, CultureInfo.InvariantCulture);
        }
    }
}
