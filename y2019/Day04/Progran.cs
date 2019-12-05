using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2019.Day04
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"112233 Valid:{CheckNumberPart2(112233)} Expected: true");
            Console.WriteLine($"123444 Valid:{CheckNumberPart2(123444)} Expected: false");
            Console.WriteLine($"111122 Valid:{CheckNumberPart2(111122)} Expected: true");

            var validCodes = GetRange()
                .Where(CheckNumberPart2);

            int validCodeCount = validCodes.Count();
            Console.WriteLine(validCodeCount);
        }

        static IEnumerable<int> GetRange()
        {
            for (int i = 156218; i <= 652527; i++)
            {
                yield return i;   
            }
        }

        static bool CheckNumberPart1(int target) => CheckDigitsPart1(GetDigits(target));
        static bool CheckNumberPart2(int target) => CheckDigitsPart2(GetDigits(target));

        static int[] GetDigits(int target)
        {
            int[] output = new int[6];
            for (int i = output.Length - 1; i >= 0 ; i--)
            {
                output[i] = target % 10;
                target /= 10;
            }

            return output;
        }

        static bool CheckDigitsPart2(int[] digits)
        {
            var groupings = new List<int>();

            int previousDigit = digits[0];
            groupings.Add(1);
            for (int i = 1; i < digits.Length; i++)
            {
                if (previousDigit > digits[i])
                {
                    return false;
                }

                if(previousDigit == digits[i])
                {
                    groupings[groupings.Count - 1]++;
                }
                else
                {
                    groupings.Add(1);
                }

                previousDigit = digits[i];
            }

            return groupings.Contains(2);
        }

        static bool CheckDigitsPart1(int[] digits)
        {
            bool twoConsecutiveDigits = false;

            int previousDigit = digits[0];
            for (int i = 1; i < digits.Length; i++)
            {
                if (previousDigit > digits[i])
                {
                    return false;
                }

                if(!twoConsecutiveDigits && previousDigit == digits[i])
                {
                    twoConsecutiveDigits = true;
                }

                previousDigit = digits[i];
            }

            return twoConsecutiveDigits;
        }
    }
}