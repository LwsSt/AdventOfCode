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
            // Console.WriteLine($"111111 Valid:{CheckNumber(111111)}");
            // Console.WriteLine($"223450 Valid:{CheckNumber(223450)}");
            // Console.WriteLine($"123789 Valid:{CheckNumber(123789)}");

            var validCodes = GetRange()
                .Where(CheckNumber);

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

        static bool CheckNumber(int target) => CheckDigits(GetDigits(target));

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

        static bool CheckDigits(int[] digits)
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