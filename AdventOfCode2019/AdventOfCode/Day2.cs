using System;
using NUnit.Framework;

namespace AdventOfCode
{

    [TestFixture]
    public class Day2
    {
    
        [TestCase(new[] { 1, 0, 0, 0, 99 }, new[] { 2, 0, 0, 0, 99 })]
        [TestCase(new[] { 2, 3, 0, 3, 99 }, new[] { 2, 3, 0, 6, 99 })]
        [TestCase(new[] { 2, 4, 4, 5, 99, 0 }, new[] { 2, 4, 4, 5, 99, 9801 })]
        [TestCase(new[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        [TestCase(new[]{ 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, new[]{3500,9,10,70,2,3,11,0,99,30,40,50})]
        public void testcomputer(int[] program, int[] result)
        {
            int[] computed = compute(program, 0);
            Assert.AreEqual(result, computed);
        }

        [Test]
        public void puzzel2()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    int[] input = Input;
                    //Change input per instructions
                    input[1] = i;  //Noun
                    input[2] = j;    //Verb

                    int[] result = compute(input, 0);
                    if (result[0] == 19690720)
                    {
                        Console.WriteLine("Noun: " + i + " Verb: "+ j);
                        Console.WriteLine("Solution: " + ((100* i) + j));
                    }
                }
            }
        }

        private static int[] Input =>
            new[]
            {
                1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 1, 6, 19, 1, 5, 19, 23, 1, 23, 6, 27, 1, 5, 27, 31,
                1, 31, 6, 35, 1, 9, 35, 39, 2, 10, 39, 43, 1, 43, 6, 47, 2, 6, 47, 51, 1, 5, 51, 55, 1, 55, 13, 59, 1,
                59, 10, 63, 2, 10, 63, 67, 1, 9, 67, 71, 2, 6, 71, 75, 1, 5, 75, 79, 2, 79, 13, 83, 1, 83, 5, 87, 1, 87,
                9, 91, 1, 5, 91, 95, 1, 5, 95, 99, 1, 99, 13, 103, 1, 10, 103, 107, 1, 107, 9, 111, 1, 6, 111, 115, 2,
                115, 13, 119, 1, 10, 119, 123, 2, 123, 6, 127, 1, 5, 127, 131, 1, 5, 131, 135, 1, 135, 6, 139, 2, 139,
                10, 143, 2, 143, 9, 147, 1, 147, 6, 151, 1, 151, 13, 155, 2, 155, 9, 159, 1, 6, 159, 163, 1, 5, 163,
                167, 1, 5, 167, 171, 1, 10, 171, 175, 1, 13, 175, 179, 1, 179, 2, 183, 1, 9, 183, 0, 99, 2, 14, 0, 0
            };

        [Test]
        public void day2_puzzel1()
        {

            int[] input = Input;
            //Change input per instructions
            input[1] = 12;
            input[2] = 2;

            int[] result = compute(input, 0);
            Console.WriteLine(result[0]);

            Assert.AreEqual(4023471,result[0] );
        }

        private int[] compute(int[] program, int opPos)
        {
            int opCode = program[opPos];
            int[] result = program;
           
            switch (opCode)
            {
                case 1:
                   // Console.WriteLine("Case 1");

                    //Add numbers from 2 positions after the op code, write the result in position on 3
                    int sum = program[program[opPos + 1]] + program[program[opPos + 2]];
                    result[program[opPos + 3]] = sum;
                    break;
                case 2:
                   // Console.WriteLine("Case 2");
                    int product = program[program[opPos + 1]] * program[program[opPos + 2]];
                    result[program[opPos + 3]] = product;
                    break;
                case 99:
                  //  Console.WriteLine("Case 99: stop execution!");
                    return result;

                default:
                    throw new Exception("FOUTE CODE GAST!");
                    
            }

            result =compute(result, opPos + 4);

            return result;
        }



    }
}
