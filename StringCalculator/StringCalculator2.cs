using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Microsoft.VisualBasic;

namespace StringCalculator
{
    public class StringCalculator2
    {
        public int Add(string input)
        {
            if(input == "")return 0;
            var delimiterList = GetDelimiterList(input);

            var numberList = GetNumberList(input, delimiterList);


            var numbersLessThan1001 = GetNumbersLessThanOrEqualTo1000(numberList);
            ValidateNegativeNumbers(numbersLessThan1001);
            

            return GetSumOfAllNumbers(numbersLessThan1001);
        }

        private List<int> GetNumbersLessThanOrEqualTo1000(List<int> numberList)
        {
            return numberList.Where(a => a <= 1000).ToList();
        }

        private static void ValidateNegativeNumbers(List<int> numberList)
        {
            if (!numberList.Any(a => a < 0)) return;
            throw new Exception($"negatives not allowed {GetNegativeNumbers(numberList)}");
        }

        private static string GetNegativeNumbers(List<int> numberList)
        {
           return string.Join(",", numberList.Where(a => a < 0).Select(a => a.ToString()).ToArray());
        }

        private static int GetSumOfAllNumbers(List<int> numberList)
        {
            return numberList.Sum();
        }

        private static List<int> GetNumberList(string input, List<string> delimiterList)
        {
            return input.Split(delimiterList.ToArray(), StringSplitOptions.None)
                .Where(a => int.TryParse(a, out _)).Select(int.Parse).ToList();
        }

        private static List<string> GetDelimiterList(string args)
        {
            var delimiterList = new List<string>{",","\n"};
            if (args.StartsWith("//"))
            {
                var customDelimiterList = GetCustomDelimiterList(args);
                delimiterList.AddRange(customDelimiterList);
            }

            return delimiterList;
        }

        private static List<string> GetCustomDelimiterList(string args)
        {
            var customDelimiterString = args.Substring(2, args.IndexOf('\n') - 2);
            var customDelimiterList = customDelimiterString.Split('[').Select(a => a.TrimEnd(']')).ToList();
            return customDelimiterList;
        }
    }
}