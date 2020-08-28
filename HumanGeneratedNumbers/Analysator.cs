using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HenrysDevLib.Extensions;
using static HenrysDevLib.Extensions.IComparableExtensions;

namespace HumanGeneratedNumbers
{
    static class Analysator
    {
        const string filePath = @"D:\alles\Diesdas\human_generated_numbers.txt";
        public static int[] LoadNumbers()
        {
            string[] content = File.ReadAllLines(filePath);
            List<int> numbers = new List<int>();

            foreach (string line in content)
            {
                string numS = Regex.Match(line, @"[0-9]*").Value;

                if (numS != String.Empty)
                    numbers.Add(Int32.Parse(numS));
            }
            return numbers.ToArray();
        }

        public static Dictionary<int, int> SortIntoTens(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < 100; i++)
                dict[i] = 0;

            for (int i = 0; i < nums.Length; i++)
                dict[nums[i] / 10]++;

            return dict;
        }

        public static Dictionary<int, int> SortIntoHundreds(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < 10; i++)
                dict[i] = 0;

            for (int i = 0; i < nums.Length; i++)
                dict[nums[i] / 100]++;

            return dict;
        }

        public static Dictionary<int, int> SortIntoLastTwoDigits(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < 100; i++)
                dict[i] = 0;

            for (int i = 0; i < nums.Length; i++)
                dict[nums[i] % 100]++;

            return dict;
        }

        public static Dictionary<int, int> SortIntoLastDigit(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < 10; i++)
                dict[i] = 0;

            for(int i = 0; i < nums.Length; i++)
                dict[nums[i] % 10]++;

            return dict;
        }

        public static Dictionary<int, int> SortIntoMiddleDigit(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < 10; i++)
                dict[i] = 0;

            for (int i = 0; i < nums.Length; i++)
                dict[(nums[i] % 100 - nums[i] % 10) / 10]++;

            return dict;
        }

        public static Dictionary<int, int> GetCrossSums(int[] nums)  // not suitable for graph plotter I think, but too lazy to check rn
        {
            Func<int, int> CrossSum = (num) =>
            {
                int res = 0;
                while (num > 0)
                {
                    res += num % 10;
                    num /= 10;
                }
                return res;
            };

            nums = nums.Map(CrossSum).ToArray();
            Array.Sort(nums);

            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = nums[0]; i < nums[^1]; i++)  // nums[^1] == nums[nums.Length - 1]
                dict[i] = nums.Count(e => e == i);

            return dict;
        }

        public static int GetOccurences (int[] nums, int num)
        {
            int count = 0;
            foreach (int nu in nums)
                count += nu == num ? 1 : 0;
            return count;
        }
    }
}
