using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdventOfCode
{

    //Computer gemaakt op dag 2, daarna gebruikt op de dagen: 5
    public class Computer
    {
        public Computer(int[] program, int input)
        {
            Input = input;
            Result = compute(program, 0);

        }

        private int Input { get; set; }

        public bool Debug { get; set; }

        public int[] Result { get; private set; }

        public int LastOutput { get; private set; }

        public static (int, ParameterModes) interpretOp(int op)
        {
            string opString = op.ToString();
            int opCode = Int32.Parse(opString.Substring(Math.Max(0, opString.Length - 2)));
            ParameterModes parameterModes = new ParameterModes();

            for (int i = 3; i < opString.Length + 1; i++)
            {
                parameterModes.Add(i - 2, Int32.Parse(opString.Substring(opString.Length - i, 1)));
            }

            return (opCode, parameterModes);
        }

        private int getValue(int[] program, int parameter, int mode)
        {
            //Immediate mode
            if (mode == 1)
            {
                return program[parameter];
            }

            return program[program[parameter]];
        }

        private int[] compute(int[] program, int opPos)
        {
            var getCodes = interpretOp(program[opPos]);
            int opCode = getCodes.Item1;
            ParameterModes parameterModes = getCodes.Item2;
            int offset = 4;


            int[] result = program;

            //Instructions
            if (Debug) { Console.WriteLine("Debug opCode:" + opCode); }
            switch (opCode)
            {
                case 1:
                    // Console.WriteLine("Case 1");

                    //Add numbers from 2 positions after the op code, write the result in position on 3
                    int sum = getValue(program, opPos + 1, parameterModes.getMode(1)) + getValue(program, opPos + 2, parameterModes.getMode(2));
                    result[program[opPos + 3]] = sum;
                    break;
                case 2:
                    // Console.WriteLine("Case 2");
                    int product = getValue(program, opPos + 1, parameterModes.getMode(1)) * getValue(program, opPos + 2, parameterModes.getMode(2));
                    result[program[opPos + 3]] = product;
                    break;
                case 3:
                    // Opcode 3 takes a single integer as input and saves it to the position given by its only parameter.For example,
                    // the instruction 3,50 would take an input value and store it at address 50.
                    result[program[opPos + 1]] = Input;
                    offset = 2;
                    break;

                case 4:
                    //Opcode 4 outputs the value of its only parameter.For example, the instruction 4,50 would output the value at address 50.
                    LastOutput = getValue(program, opPos + 1, parameterModes.getMode(1));
                    Console.WriteLine("Output: " + LastOutput);
                    offset = 2;
                    break;

                case 5:
                    //Opcode 5 is jump -if-true: if the first parameter is non - zero, it sets the instruction pointer to the value from the
                    //second parameter.Otherwise, it does nothing.
                    if (getValue(program, opPos + 1, parameterModes.getMode(1)) != 0)
                    {
                        opPos = getValue(program, opPos + 2, parameterModes.getMode(2));
                        offset = 0;
                    }
                    else{offset = 3;}
                    break;

                case 6:
                    //Opcode 6 is jump -if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second
                    //parameter.Otherwise, it does nothing.
                    if (getValue(program, opPos + 1, parameterModes.getMode(1)) == 0)
                    {
                        opPos = getValue(program, opPos + 2, parameterModes.getMode(2));
                        offset = 0;
                    }

                    else{offset = 3;}
                    break;

                case 7:
                    //Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the
                    //third parameter.Otherwise, it stores 0.
                    result[program[opPos + 3]] = getValue(program, opPos + 1, parameterModes.getMode(1)) <
                                                 getValue(program, opPos + 2, parameterModes.getMode(2)) ?
                            1 : 0;
                    break;

                case 8:
                    //Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given
                    //by the third parameter.Otherwise, it stores 0.
                    result[program[opPos + 3]] = getValue(program, opPos + 1, parameterModes.getMode(1)) ==
                                                 getValue(program, opPos + 2, parameterModes.getMode(2)) ?
                        1 : 0;
                    break;

                case 99:
                    //  Console.WriteLine("Case 99: stop execution!");
                    return result;

                default:
                    Console.WriteLine(opCode);
                    throw new Exception(opCode + " IS EEN FOUTE CODE GAST!");

            }

            result = compute(result, opPos + offset);

            return result;
        }

    }

    public class ParameterModes : Dictionary<int, int>
    {
        public int getMode(int key)
        {
            return base.ContainsKey(key) ? base[key] : 0;
        }

    }
}
