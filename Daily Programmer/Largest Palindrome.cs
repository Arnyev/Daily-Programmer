using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Daily_Programmer
{
	class Largest_Palindrome
	{
		public static void Test()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			Console.WriteLine(largestPalindrome(7));
			Console.WriteLine(sw.ElapsedMilliseconds);
			sw.Restart();

			Console.WriteLine(largestPalindromeSectors(7));
			Console.WriteLine(sw.ElapsedMilliseconds);
			sw.Restart();

			Console.WriteLine(largestPalindrome(8));
			Console.WriteLine(sw.ElapsedMilliseconds);
			sw.Restart();

			Console.WriteLine(largestPalindromeSectors(8));
			Console.WriteLine(sw.ElapsedMilliseconds);
		}
		struct sector
		{
			public ulong startingPointI;
			public ulong startingPointJ;
			public sector(ulong a, ulong b)
			{
				startingPointI = a;
				startingPointJ = b;
			}
		}
		static ulong largestPalindromeSectors(int input)
		{
			ulong largest = 0;

			ulong startingFactor = (ulong)Math.Pow(10, input) - 1;
			ulong endingFactor = (ulong)Math.Pow(10, input - 1) - 1;

			ulong sectorSize = (ulong)Math.Pow(10, (input + 2) / 2) / 2;
			ulong sectorNumber = (ulong)Math.Pow(10, (input - 1) / 2) * 2;

			sector[] sectors = new sector[(sectorNumber + 1) * sectorNumber / 2];
			int index = 0;
			for (ulong i = 0; i < sectorNumber; i++)
				for (ulong j = i; j < sectorNumber; j++)
				{
					sectors[index].startingPointI = startingFactor - i * sectorSize;
					sectors[index++].startingPointJ = startingFactor - j * sectorSize;
				}

			sectors = sectors.OrderByDescending((s) => s.startingPointI * s.startingPointJ).ToArray();

			for (int k = 0; k < sectors.Length; ++k)
			{
				sector s = sectors[k];
				if (s.startingPointI * s.startingPointJ < largest) break;
				ulong endI = s.startingPointI - sectorSize;
				for (ulong i = s.startingPointI; i > endI; --i)
				{
					ulong endJ = s.startingPointJ - sectorSize;
					if (largest >= i * s.startingPointJ) break;
					for (ulong j = s.startingPointJ; j > endJ; --j)
					{
						ulong local = j * i;
						if (largest >= local)
							break;

						if (isPalindrome(local))
							largest = local;
					}
				}
			}
			return largest;
		}
		static ulong largestPalindrome(int input)
		{
			ulong startingFactor = (ulong)Math.Pow(10, input) - 1;
			ulong endingFactor = (ulong)Math.Pow(10, input - 1) - 1;

			ulong largest = 0;
			for (ulong i = startingFactor; i > endingFactor - 1; --i)
			{
				if (largest >= i * (i - 1))
					break;

				for (ulong j = i - 1; j > endingFactor; --j)
				{
					ulong local = j * i;
					if (largest >= local)
						break;

					if (isPalindrome(local))
						largest = local;
				}
			}
			return largest;
		}
		static bool isPalindrome(ulong n)
		{
			var s = n.ToString();
			for (int i = 0; i < s.Length / 2; i++)
				if (s[i] != s[s.Length - 1 - i])
					return false;
			return true;
		}
	}
}