using System;
using System.Collections;
using System.Text.RegularExpressions;

public class Task
{
    private static Dictionary<string, int> romanNums = new Dictionary<string, int>() 
    {
        { "M", 1000 }, { "CM", 900 }, { "D", 500 }, { "CD", 400 }, { "C", 100 }, { "XC", 90 },
        { "L", 50 }, { "XL", 40 }, { "X", 10 }, { "IX", 9 }, { "V", 5 }, { "IV", 4 },
        { "I", 1 }
    };

    public static void Main()
    {
        Console.WriteLine("Input text wth roman nums (case sensetive):");
        string input = Console.ReadLine();
        string pattern = @"\bM{0,3}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})\b";
        MatchEvaluator evaluator = new MatchEvaluator(RomanToArabic);
        Console.WriteLine();
        Console.WriteLine("Original text:");
        Console.WriteLine(input);
        Console.WriteLine();
        if (input != null)
        {
            try
            {
                Console.WriteLine("Converted text:");
                Console.WriteLine(Regex.Replace(input, pattern, evaluator,
                                                RegexOptions.IgnorePatternWhitespace,
                                                TimeSpan.FromSeconds(1)));
            }
            catch (RegexMatchTimeoutException)
            {
                Console.WriteLine("Converting roman to arabic operation timed out.");
                Console.WriteLine("Converted text:");
            }
        }  
    }

    public static string RomanToArabic(Match match)
    {
        string input = match.Value.ToString();
        // Define two arrays equal to the number of letters in the match.

        if (input.Length == 0)
        {
            return "";
        }

        else
        {
            int result = 0;

            foreach (var num in romanNums)
            {
                if (input.Length != 0)
                {
                    while (input.StartsWith(num.Key))
                    {
                        result += num.Value;
                        input = input.Remove(0, num.Key.Length);
                    }
                }
            }

            return result.ToString();
        }
    }
}

