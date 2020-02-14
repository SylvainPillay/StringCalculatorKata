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

            ValidateNegativeNumbers(numberList);

            return GetSumOfAllNumbers(numberList);
        } 

        private List<string> GetDelimiterList(string args)
        {
            var delimiterList = new List<string> { ",", "\n" };
            if (args.StartsWith("//"))
            {
                GetCustomDelimiter(args, delimiterList);
            }
            return delimiterList;
        }

        private void GetCustomDelimiter(string args, List<string> delimiterList)
        {
            var delimiterString = args.Substring(2, args.IndexOf('\n') - 2);
            delimiterList.AddRange(delimiterString.Split('[').Select(a => a.TrimEnd(']')).ToList());
            delimiterList.Remove(string.Empty);
        }

        private List<int> GetNumberList(string args, List<string> delimiterList)
        {
            var numberList = args.Split(delimiterList.ToArray(), StringSplitOptions.None)
                .Where(a => int.TryParse(a, out _)).Select(int.Parse).ToList();

            var filterNumbersLessThanOrEqualTo1000 = GetNumbersLessThanOrEqualTo1000(numberList);

            return filterNumbersLessThanOrEqualTo1000;
        }

        public List<int> GetNumbersLessThanOrEqualTo1000(List<int> numberList)
        {
            return numberList.Where(a => a <= 1000).ToList();
        }

        private void ValidateNegativeNumbers(List<int> numbers)
        {
            if (!numbers.Any(a => a < 0)) return;
            var negativeNumbers = GetNegativeNumbers(numbers);
            throw new Exception($"negatives not allowed {negativeNumbers}");
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
