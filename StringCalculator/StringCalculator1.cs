using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
   public class StringCalculator1
    {
        // TODO the code for getting delimiters is spread out between this field, the Add method and the getDelimiterList
        //       getting delimiters is definitely one responsibility. - done
        public int Add(string args)
        {

            var delimList = getDelimiterList(args);

            args = GetStringToSplit(args);

            return getSumOfAllNumbers(args, delimList);
        }
        private List<string> getDelimiterList(string args)
        {
            var delimList = new List<string> { ",", "\n" };
            if (args.StartsWith("//"))
            {
                var delimString = args.Substring(2, args.IndexOf('\n') - 2);
                delimList.AddRange(delimString.Split('[').Select(a => a.TrimEnd(']')).ToList());
                delimList.Remove(string.Empty);
            }
            return delimList;
        }
        private static string GetStringToSplit(string args)
        {
            args = args.Substring(args.IndexOf('\n') + 1);
            return args;
        }

        // TODO this method is doing multiple things, it is parsing number, splitting numbers, filtering number and
        //       doing a little co-ordination by calling the ValidateNegativeNumbers. - done
        private int getSumOfAllNumbers(string args, List<string> delimList)
        {
            var numberList = GetNumberList(args, delimList);
            return numberList.Where(a => a <= 1000).Sum();
        }

        private List<int> GetNumberList(string args, List<string> delimList)
        {
            var numberList = args.Split(delimList.ToArray(), StringSplitOptions.None)
                .Where(a => int.TryParse(a, out _)).Select(int.Parse).ToList();

            ValidateNegativeNumbers(numberList);

            return numberList;
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
