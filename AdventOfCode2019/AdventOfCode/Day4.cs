using System;
using NUnit.Framework;

namespace AdventOfCode
{
    public class Password
    {
        private string password;

        public Password(string input)
        {
            password = input;
            puzzel2 = false;
        }

        public bool puzzel2 { get; set; }

        public bool IsNummeric => Int64.TryParse(password, out _);
        public bool IsSixDigits => password.Length == 6;
        public bool IsIncreasing
        {
            get
            {
                for (int i = 1; i < password.Length; i++)
                {
                    //Going from left to right, the digits never decrease; they only ever increase or stay the same(like 111123 or 135679).
                    if (password[i] < password[i - 1])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public bool HasDouble
        {
            get
            {
                for (int i = 1; i < password.Length; i++)
                {
                    //Going from left to right, the digits never decrease; they only ever increase or stay the same(like 111123 or 135679).
                    if (password[i] == password[i - 1])
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool HasSingleDouble
        {
            get
            {
                if (!HasDouble)
                {
                    return false;
                }

                for (int i = 0; i < password.Length - 1; i++)
                {
                    if (i == 0 && password[i] == password[i + 1] && password[i] != password[i + 2])
                    {
                        return true;
                    }
                    else
                    {
                        if (i == password.Length - 2)
                        {
                            return password[i] == password[i + 1] && password[i - 1] != password[i];
                        }
                    }

                    if ((password[i] == password[i + 1] && password[i] != password[i + 2] && password[i] != password[i - 1])
                    )
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool Valid => IsNummeric && IsSixDigits && IsIncreasing && HasDouble && (!puzzel2 || HasSingleDouble);
    }

    [TestFixture]
    public class Day4
    {
        [TestCase("111111", true)]
        [TestCase("223450", false)]
        [TestCase("123789", false)]
        public void TestPuzzle1(string input, bool result)
        {
            Assert.AreEqual(result, new Password(input).Valid);
        }

        [Test]
        public void Puzzle1()
        {
            // How many different passwords within the range given in your puzzle input meet these criteria?
            //Range 134564-585159
            int count = 0;
            for (int i = 134564; i <= 585159; i++)
            {
                if (new Password(i.ToString()).Valid)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
            Assert.AreEqual(1929, count);
        }

        [TestCase("112233", true)]
        [TestCase("123444", false)]
        [TestCase("111122", true)]
        [TestCase("144477", true)]
        public void TestPuzzle2(string input, bool result)
        {
            Assert.AreEqual(result, new Password(input) { puzzel2 = true }.Valid);
        }

        [Test]
        public void Puzzle2()
        {
            // How many different passwords within the range given in your puzzle input meet these criteria?
            //Range 134564-585159
            int count = 0;
            for (int i = 134564; i <= 585159; i++)
            {
                if (new Password(i.ToString()) { puzzel2 = true }.Valid)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
            Assert.AreEqual(1306, count);
        }

        private bool passwordValid(string password, bool secondRule = false)
        {
            if ( //It is a six - digit number.
                password.Length != 6 &&
                Int64.TryParse(password, out _)
            )
            {
                return false;
            }

            bool doublefound = false;
            bool doubleok = false;
            for (int i = 1; i < password.Length; i++)
            {

                //Going from left to right, the digits never decrease; they only ever increase or stay the same(like 111123 or 135679).
                if (password[i] > password[i - 1])
                {
                    return false;
                }


                //Two adjacent digits are the same(like 22 in 122345).
                doublefound = (password[i] == password[i - 1]);
                doubleok = ((i + 1) < password.Length && password[i + 1] != password[i]);
                if (doublefound && (doubleok))
                {
                    return true;
                }
            }

            return false;
        }

    }

}
