using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
   public class StringCalculator1
    {
        public int Add(string args)
        {
            var delimList = getDelimiterList(args);

            var numberList = getNumberList(args, delimList);

            validateNegativeNumbers(numberList);

            return getSumOfAllNumbers(numberList);
        } 

        private List<string> getDelimiterList(string args)
        {
            var delimList = new List<string> { ",", "\n" };
            if (args.StartsWith("//"))
            {
                getCustomDelimiter(args, delimList);
            }
            return delimList;
        }

        private void getCustomDelimiter(string args, List<string> delimList)
        {
            var delimString = args.Substring(2, args.IndexOf('\n') - 2);
            delimList.AddRange(delimString.Split('[').Select(a => a.TrimEnd(']')).ToList());
            delimList.Remove(string.Empty);
        }

        private List<int> getNumberList(string args, List<string> delimList)
        {
            var numberList = args.Split(delimList.ToArray(), StringSplitOptions.None)
                .Where(a => int.TryParse(a, out _)).Select(int.Parse).ToList();

            var filterNumbersLessThanOrEqualTo1000 = getNumbersLessThanOrEqualTo1000(numberList);

            return filterNumbersLessThanOrEqualTo1000;
        }

        public List<int> getNumbersLessThanOrEqualTo1000(List<int> numberList)
        {
            return numberList.Where(a => a <= 1000).ToList();
        }

        private void validateNegativeNumbers(List<int> numbers)
        {
            if (!numbers.Any(a => a < 0)) return;
            var negativeNumbers = getNegativeNumbers(numbers);
            throw new Exception($"negatives not allowed {negativeNumbers}");
        }

        private string getNegativeNumbers(List<int> numbers)
        {
            return string.Join(",", numbers.Where(a => a < 0).Select(a => a.ToString()).ToArray());
        }

        private int getSumOfAllNumbers(List<int> numberList)
        {
            return numberList.Sum();
        }
    }
}
