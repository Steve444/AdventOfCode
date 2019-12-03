using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode
{
    public class Day1
    {
        private readonly List<decimal> _modules = new List<decimal>()
        {
            141496,
                50729,
                52916,
                98133,
                93839,
                107272,
                142069,
                67632,
                75009,
                74371,
                69081,
                91480,
                102664,
                105221,
                130656,
                90946,
                72792,
                148049,
                70881,
                145510,
                105035,
                149880,
                117058,
                149669,
                59725,
                122995,
                74449,
                96690,
                140220,
                59294,
                142524,
                139379,
                107322,
                57832,
                66101,
                105801,
                59189,
                58687,
                61454,
                116490,
                125198,
                116264,
                103459,
                145734,
                98738,
                62783,
                138935,
                143958,
                87769,
                100410,
                112567,
                131008,
                96648,
                62022,
                84654,
                135197,
                104771,
                116477,
                58956,
                83449,
                71150,
                86343,
                69346,
                100858,
                146224,
                142933,
                135930,
                99671,
                97840,
                145286,
                55577,
                88347,
                75169,
                73059,
                144308,
                110284,
                117688,
                146396,
                75934,
                92370,
                124781,
                133506,
                134441,
                98229,
                100872,
                75249,
                108598,
                106277,
                80388,
                138398,
                143521,
                74189,
                72945,
                79918,
                132770,
                78616,
                91499,
                124595,
                89042,
                90715
        };

        private decimal solveDay1(List<decimal> modules)
        {
            decimal fuel = 0;
            foreach (decimal module in modules)
            {
                //take its mass, divide by three, round down, and subtract 2.
                fuel += calculate(module);
            }
            return fuel;
        }

        private decimal calculate(decimal module)
        {
            decimal fuel = (Math.Floor(module / 3) - 2);
            return fuel > 0 ? fuel : 0;
        }

        private decimal solveDay1_2(List<decimal> modules)
        {
            decimal fuel = 0;
            foreach (decimal module in modules)
            {
                //take its mass, divide by three, round down, and subtract 2.
                var modulefuel = calculate(module);
                var extrafuel = recursiveFuelAddition(modulefuel, 0);
                fuel += (modulefuel + extrafuel);
            }
            return fuel;
        }

        private decimal recursiveFuelAddition(decimal mass, decimal extrafuel)
        {
            decimal newfuel = calculate(mass);
            if (newfuel > 0)
            {
                extrafuel += newfuel;
                return recursiveFuelAddition(newfuel, extrafuel);
            }

            return extrafuel;
        }

        //A module of mass 14 requires 2 fuel.This fuel requires no further fuel(2 divided by 3 and rounded down is 0,
        //which would call for a negative fuel), so the total fuel required is still just 2.
        //At first, a module of mass 1969 requires 654 fuel.Then, this fuel requires 216 more fuel(654 / 3 - 2).
        //216 then requires 70 more fuel, which requires 21 fuel, which requires 5 fuel, which requires no further fuel
        //.So, the total fuel required for a module of mass 1969 is 654 + 216 + 70 + 21 + 5 = 966.
        //The fuel required by a module of mass 100756 and its fuel is: 33583 + 11192 + 3728 + 1240 + 411 + 135 + 43 + 12 + 2 = 50346.

        [Test]
        [TestCase(14, 2)]
        [TestCase(1969, 966)]
        [TestCase(100756, 50346)]
        public void Testrecursion(decimal mass, decimal result)
        {
            Assert.AreEqual(result, recursiveFuelAddition(mass, 0));
        }

        [Test]
        public void Test2()
        {
            decimal outcome = solveDay1_2(_modules);
            Console.WriteLine(outcome);
            Assert.AreEqual(outcome, 5088176);
        }

        [Test]
        public void Test1()
        {
            decimal outcome = solveDay1(_modules);
            Console.WriteLine(outcome);
            Assert.AreEqual(3394032, outcome);
        }
    }
}