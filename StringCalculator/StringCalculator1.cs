using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
   public class StringCalculator1
    {
        // TODO the code for getting delimiters is spread out between this field, the Add method and the getDelimiterList
        //       getting delimiters is definitely one responsibility.
        private List<string> defaultDelimList = new List<string> { ",", "\n" };
        public int Add(string args)
        {
            // TODO I'm not convinced you need this line of code.
            if (string.IsNullOrEmpty(args)) return 0;

            if (args.StartsWith("//"))
            {
                AddCustomDelimiter(ref args);
            }
            return getSumOfAllNumbers(args);
        }

        public void AddCustomDelimiter(ref string args)
        {
            defaultDelimList.AddRange(getDelimiterList(args));
            args = args.Substring(args.IndexOf('\n') + 1);
        }

        // TODO this method is doing multiple things, it is parsing number, splitting numbers, filtering number and
        //       doing a little co-ordination by calling the ValidateNegativeNumbers.
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
            var getNegativeNumbers = string.Join(",", 
                numbers.Where(a => a < 0).Select(a => a.ToString()).ToArray());
            throw new Exception($"negatives not allowed {getNegativeNumbers}");

        }
    }
}
