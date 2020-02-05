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
            // TODO please don't mutate a variable where there is no need to, it introduces complexity.
            //       Rather create a new variable.
            args = GetStringToSplit(args);

            return getSumOfAllNumbers(args, delimList);
        } // <--- TODO space blindness
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
        } // <--- TODO space blindness
        private static string GetStringToSplit(string args) // TODO this name quite general. It could be swapped out with "GetTheThingINeedToDoThingsTo()" without much information loss.
        {
            args = args.Substring(args.IndexOf('\n') + 1);
            return args;
        }

        // TODO this methods name says that it sums numbers. However it is doing more than that.
        //       If it's name were updated to be more accurate it would be
        //       SplitThenParseThenCheckForNegativesThenFilterNumbersAbove1000AndThenFinallySum().
        //       That is definitely multiple responsibilities because there is more than 1 reason to change!
        //
        //       I know that some of these things have been pulled out into the GetNumberList()
        //       method, but that one also has many reasons.
        //
        //       This discussion is a subtle one and hard to convey here. The short of it is that a method
        //       should have a single responsibility at the level of abstraction it is operating at.
        //       Parsing, splitting, summing, filtering and checking for negatives are all at the same level of abstraction,
        //       therefore no single method should be doing more than one of those. The critical thing to know here is that
        //       co-ordination (calling methods and passing data between them to do a job) is itself a responsibility.
        //       Functions who's responsibility it is to co-ordinate are called higher order functions.
        //       You can read about them at https://eloquentjavascript.net/05_higher_order.html.
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
