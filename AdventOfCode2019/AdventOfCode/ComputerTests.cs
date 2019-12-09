using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace AdventOfCode
{
    [TestFixture]
    class ComputerTests
    {
        [TestCase(new[] { 1, 0, 0, 0, 99 }, new[] { 2, 0, 0, 0, 99 })]
        [TestCase(new[] { 2, 3, 0, 3, 99 }, new[] { 2, 3, 0, 6, 99 })]
        [TestCase(new[] { 2, 4, 4, 5, 99, 0 }, new[] { 2, 4, 4, 5, 99, 9801 })]
        [TestCase(new[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        [TestCase(new[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, new[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 })]
        [TestCase(new[]{ 1002, 4, 3, 4, 33 }, new[] { 1002, 4, 3, 4, 99 })]
        public void testcomputer(int[] program, int[] result)
        {
            Assert.AreEqual(result, new Computer(program, 0).Result);
        }


        [TestCase(1002, 2, 0,1)]
        [TestCase(5412, 12,4,5)]
        public void testInterpretOp(int op, int resOp, int mode1, int mode2)
        {
            var parametermodes = Computer.interpretOp(op);
            Assert.AreEqual(resOp, parametermodes.Item1);

            Assert.AreEqual(mode1, parametermodes.Item2[1]);
            Assert.AreEqual(mode2, parametermodes.Item2[2]);
        }
        
        [TestCase(new[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 8, 1)]
        [TestCase(new[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 8, 1)]
        [TestCase(new[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 7, 0)]
        [TestCase(new[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 7, 0)]
        [TestCase(new[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 99, 0)]
        [TestCase(new[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 99, 0)]
        [TestCase(new[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 8, 0)]
        [TestCase(new[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 8, 0)]
        [TestCase(new[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 7, 1)]
        [TestCase(new[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 7, 1)]
        [TestCase(new[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 99, 0)]
        [TestCase(new[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 99, 0)]
        [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 8, 1)]
        [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 8, 1)]
        [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 0,0)]
        [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 0, 0)]
        [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 },99, 1)]
        [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 99, 1)]

        [TestCase(new[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, 7, 999)]
        [TestCase(new[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, 8, 1000)]
        [TestCase(new[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, 9, 1001)]
        public void jumpAndEqualTests(int[] testprogram, int input, int result)
        {
            Computer comp8 = new Computer(testprogram, input);
            Assert.AreEqual(result, comp8.LastOutput);
        }
    }
}
