using System;
using System.Collections.Generic;

namespace DataProgQ2
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            int[] arrLen = { }, a = { }, b = { };
            int aLen, bLen, total;
            try
            {
                //Console.WriteLine("Enter array length");
                arrLen = program.ParseAndValidateLine(Console.ReadLine(), 2).ToArray();
                aLen = arrLen[0];
                bLen = arrLen[1];
                program.validateArrayLength(aLen, bLen);

                //Console.WriteLine("Enter array 'a': ");
                a = program.ParseAndValidateLine(Console.ReadLine(), aLen).ToArray();
                //Console.WriteLine("Enter array 'b': ");
                b = program.ParseAndValidateLine(Console.ReadLine(), bLen).ToArray();

                total = program.getTotalX(a, b);
                Console.WriteLine(total);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }



        private int getTotalX(int[] a, int[] b)
        {
            int startIdx, endIdx, num;
            List<int> aLst = new List<int>(a);
            List<int> bLst = new List<int>(b);
            List<int> numList = new List<int>();
            aLst.Sort();
            bLst.Sort();
            startIdx = aLst[aLst.Count - 1];
            endIdx = bLst[0];
            for (num = startIdx; num <= endIdx; num++)
            {
                numList.Add(num);
            }
            numList = findNumMatchingA(numList, aLst);
            numList = findNumMatchingB(numList, bLst);

            return numList.Count;
        }

        private List<int> findNumMatchingA(List<int> numList, List<int> aLst)
        {
            List<int> newNumLst = new List<int>();
            foreach (int num in numList)
            {
                if (HasAllFactors(num, aLst))
                {
                    newNumLst.Add(num);
                }
            }
            return newNumLst;
        }

        private List<int> findNumMatchingB(List<int> numList, List<int> bLst)
        {
            List<int> newNumLst = new List<int>();
            foreach (int num in numList)
            {
                if (IsFactorOf(bLst, num))
                {
                    newNumLst.Add(num);
                }
            }
            return newNumLst;
        }

        private bool IsFactorOf(List<int> bLst, int num)
        {
            List<int> factorLst = new List<int>();
            foreach (int bItem in bLst)
            {
                factorLst = getFactors(bItem);
                if (!factorLst.Contains(num))
                {
                    return false;
                }
            }
            return true;
        }

        private List<int> getFactors(int num)
        {
            List<int> factorLst = new List<int>();
            for (int idx = 1; idx <= num; idx++)
            {
                if (num % idx == 0)
                {
                    factorLst.Add(idx);
                }
            }
            return factorLst;
        }

        private bool HasAllFactors(int num, List<int> aLst)
        {
            List<int> factorLst = getFactors(num);

            foreach (int item in aLst)
            {
                if (!factorLst.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        private void validateArrayLength(int aLen, int bLen)
        {
            if (aLen < 1 || aLen > 10)
            {
                throw new ArrayLengthException();
            }
            if (bLen < 1 || bLen > 10)
            {
                throw new ArrayLengthException();
            }
        }
        private List<int> ParseAndValidateLine(string line, int excepted)
        {
            List<int> arrLst = ParseLine(line);
            if (arrLst.Count != excepted)
            {
                throw new InputLengthException(excepted, arrLst.Count);
            }
            return arrLst;
        }
        private List<int> ParseLine(string line)
        {
            int outputVal = 0;
            bool isNumber = false;
            List<int> outputList = new List<int>();
            string[] words = line.Split(" ");
            foreach (string word in words)
            {
                isNumber = int.TryParse(word, out outputVal);
                if (!isNumber)
                {
                    throw new InvalidDataException(line);
                }
                else if (outputVal < 1 || outputVal > 100)
                {
                    throw new InvalidValueException();
                }
                else
                {
                    outputList.Add(outputVal);
                }
            }
            return outputList;
        }
    }

    public class ArrayLengthException : Exception
    {
        public ArrayLengthException() :
            base("Array length should be between 1 and 10")
        {

        }
    }

    public class InvalidValueException : Exception
    {
        public InvalidValueException() :
            base("Value should be between 1 and 100")
        {

        }
    }

    public class InputLengthException : Exception
    {
        public InputLengthException(int excepted, int actual) :
            base($"Excepted input {excepted}, Actual input {actual}")
        {

        }
    }

    public class InvalidDataException : Exception
    {
        public InvalidDataException(string data) :
            base($"Invalid data: {data}")
        {

        }
    }
}
