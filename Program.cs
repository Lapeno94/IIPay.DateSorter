using System;
using System.Collections.Generic;
using System.Linq;

namespace IIPay.DateSorter
{
    class Program
    {
        static void Main(string[] args)
        {

            var sorter = new DateSorter();

            var input = new HashSet<DateTime>() 
            {
                new DateTime(2005,07,01),
                new DateTime(2005,01,02),
                new DateTime(2005,01,01),
                new DateTime(2005,05,03),
                new DateTime(2005,02,01),
                new DateTime(2005,11,03),
            };

            var sortedDates = sorter.SortDates(input);


            foreach (var date in sortedDates)
            {
                Console.WriteLine(date);
            }

        }
    }

    public interface IDateSorter
    {
        /**
        * The implementation of this method should sort dates.
        * The output should be in the following order:
        * Dates with an 'r' in the month,
        * sorted ascending(first to last),
        * then dates without an 'r' in the month,
        * sorted descending (last to first).
        * For example, October dates would come before May dates,
        * because October has an ‘r’ in it.
        * thus: (2005-07-01, 2005-01-02, 2005-01-01, 2005-05-03)
        * would sort to
        * (2005-01-01, 2005-01-02, 2005-07-01, 2005-05-03)
        *
        * @param unsortedDates - an unsorted list of dates
        * @return The list of dates now sorted as per the spec
        */
        ISet<DateTime> SortDates(ISet<DateTime> unsortedDates);
    }

    public class DateSorter : IDateSorter
    {
        // Months with words
        private const string Format = "d MMMM yyyy";

        public ISet<DateTime> SortDates(ISet<DateTime> unsortedDates)
        {
            var unsortedStringDates = new HashSet<string>();

            foreach (var date in unsortedDates)
            {
                unsortedStringDates.Add(date.ToString(Format));
            }

            // filter for dates with R in it could be indexOf or any other solution but this is unecessary bc of Oˇ2 algorithm 
            var sortedDates = unsortedStringDates.Where(x => x.Contains("r") || x.Contains("R")).Select(x => DateTime.Parse(x)).OrderBy(x => x).ToHashSet();

            // hashset add concat to the end
            foreach (var date in unsortedDates.OrderBy(x => x))
            {
                sortedDates.Add(date);
            }

            return sortedDates;
        }
    }
}
