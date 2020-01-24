using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCalculator
{
   public class StringCalculator1
    {
        private List<string> defaultDelimList = new List<string> { ",", "\n" };
        public int Add(string args)
        {
            if (string.IsNullOrEmpty(args)) return 0;

            if (args.StartsWith("//"))
            {
                defaultDelimList.AddRange(getDelimiterList(args));
                args = args.Substring(args.IndexOf('\n') + 1);

            }
            return getSumOfAllNumbers(args);
        }

        private int getSumOfAllNumbers(string args)
        {
            var numberList = args.Split(defaultDelimList.ToArray(), StringSplitOptions.None)
                .Where(a => int.TryParse(a, out _)).Select(int.Parse).ToList();
            ValidateNegativeNumbers(numberList);
            return numberList.Where(a => a < 1000).Sum();
        }

        public List<string> getDelimiterList(string args)
        {
            var delimString = args.Substring(2, args.IndexOf('\n') - 2);
            var delimList = delimString.Split('[').Select(a => a.TrimEnd(']')).ToList();
            delimList.Remove(string.Empty);
            return delimList;
        }

        private void ValidateNegativeNumbers(List<int> numbers)
        {
            if (!numbers.Any(a => a < 0)) return;
            var getNegativeNumbers = string.Join(",", numbers.Where(a => a < 0).Select(a => a.ToString()).ToArray());
            throw new Exception($"negatives not allowed {getNegativeNumbers}");

        }
    }
}
