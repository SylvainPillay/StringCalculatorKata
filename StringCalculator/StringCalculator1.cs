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
                var customDelimiterList = GetCustomDelimiters(args, delimiterList);
                delimiterList.AddRange(customDelimiterList);
            }
            return delimiterList;
        }

        // TODO the delimterList parameter is not used and can be deleted
        private List<string> GetCustomDelimiters(string args, List<string> delimiterList)
        {
            var customDelimiterString = args.Substring(2, args.IndexOf('\n') - 2);

            return customDelimiterString.Split('[').Select(a => a.TrimEnd(']')).ToList();
        }

        private List<int> GetNumberList(string args, List<string> delimiterList)
        {
            var numberList = args.Split(delimiterList.ToArray(), StringSplitOptions.None)
                .Where(a => int.TryParse(a, out _)).Select(int.Parse).ToList();

            // TODO this is a subtle one. Filtering numbers > 1000 here works, however, ask yourself,
            //      is this the correct level of abstraction? Initially it might seem so because it has to do with
            //      numbers, but then so does checking for negative numbers and that isn't done here.
            //      Another way to think about it is, if someone were to only read the contents of the public Add method,
            //      would their understanding of what the code does be diminished because the call to GetNumbersLessThanOrEqualTo1000
            //      is hidden away down here instead of happening in the Add method?
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
            // TODO, minor, this variable doesn't really add much, I think the function call can be
            //              inlined without any loss to readability. This is however personal preference.
            var negativeNumbers = GetNegativeNumbers(numbers);
            // TODO, there is no test that ensures the actual negative numbers are part of the exception message,
            //       if you replace the below line with the commented out one all tests still pass.
            throw new Exception($"negatives not allowed {negativeNumbers}");
            //throw new Exception($"negatives not allowed");
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
