using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
   public class StringCalculator1
    {
        public int Add(string args)
        {
            var delimiterList = GetDelimiterList(args);

            var numberList = GetNumberList(args, delimiterList);

            var numbersLessThanOrEqualTo1000 = GetNumbersLessThanOrEqualTo1000(numberList);

            var negativeNumbersExist = numbersLessThanOrEqualTo1000.Any(a => a < 0);
            if (negativeNumbersExist)
            {
                var negativeNumbers = GetNegativeNumbers(numbersLessThanOrEqualTo1000);

                throw new Exception($"negatives not allowed {negativeNumbers}");
            }

            return GetSumOfAllNumbers(numbersLessThanOrEqualTo1000);
        } 

        private List<string> GetDelimiterList(string args)
        {
            var delimiterList = new List<string> { ",", "\n" };
            if (args.StartsWith("//"))
            {
                var customDelimiterList = GetCustomDelimiters(args);
                delimiterList.AddRange(customDelimiterList);
            }
            return delimiterList;
        }

        private List<string> GetCustomDelimiters(string args)
        {
            var customDelimiterString = args.Substring(2, args.IndexOf('\n') - 2);

            return customDelimiterString.Split('[').Select(a => a.TrimEnd(']')).ToList();
        }

        private List<int> GetNumberList(string args, List<string> delimiterList)
        {
            var numberList = args.Split(delimiterList.ToArray(), StringSplitOptions.None)
                .Where(a => int.TryParse(a, out _)).Select(int.Parse).ToList();

            return numberList;
        }

        private List<int> GetNumbersLessThanOrEqualTo1000(List<int> numberList)
        {
            return numberList.Where(a => a <= 1000).ToList();
        }

        private string GetNegativeNumbers(List<int> numbers)
        {
            return string.Join(",", numbers.Where(a => a < 0).Select(a => a.ToString()).ToArray());
        }

        private int GetSumOfAllNumbers(List<int> numberList)
        {
            return numberList.Sum();
        }
    }
}
