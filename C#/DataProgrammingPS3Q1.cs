using System;
using System.Collections.Generic;

namespace DataProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.angryProfessor();
        }

        private void angryProfessor()
        {
            int[] lines;
            int testCases, numOfStudents, threshold;
            List<bool> cancelLst = new List<bool>();
            //Console.WriteLine("Enter the No. of testcases: ");
            testCases = Convert.ToInt32(Console.ReadLine());
            for (int idx = 1; idx < testCases + 1; idx++)
            {
                //Console.WriteLine($"Testcase {idx}:");
                //Console.WriteLine("Enter No. of Students and threshold: ");
                lines = ParseLine(Console.ReadLine()).ToArray();
                numOfStudents = lines[0];
                threshold = lines[1];
                //Console.WriteLine("Enter Student Arrival times: ");
                List<int> arrivalLst = getArrivalTimes(Console.ReadLine(), numOfStudents);
                bool isCanceled = hasClassCancelled(arrivalLst, threshold);
                cancelLst.Add(isCanceled);
                //Console.WriteLine(isCanceled);
            }

            foreach (bool item in cancelLst)
            {
                Console.WriteLine(item);
            }
        }

        private bool hasClassCancelled(List<int> arrivalLst, int threshold)
        {
            int count = 0;
            foreach (int time in arrivalLst)
            {
                count = time > 0 ? (count + 1) : count;
            }
            if (count >= threshold)
            {
                return true;
            }
            else
            {
                return false;
            }

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
                else
                {
                    outputList.Add(outputVal);
                }
            }
            return outputList;
        }

        private List<int> getArrivalTimes(string line, int numOfStudents)
        {
            List<int> arrivalLst = ParseLine(line);
            if (arrivalLst.Count != numOfStudents)
            {
                throw new InputLengthException(numOfStudents, arrivalLst.Count);
            }
            return arrivalLst;
        }

    }

    public class InputLengthException : Exception
    {
        public InputLengthException(int excepted, int actual) :
            base($"Excepted input length {excepted}, Actual input length {actual}")
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
