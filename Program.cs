using System;

namespace AoC2022
{
    public static class Day1
    {
        public static string[] puzzleInput => File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/puzzleInputDay12022.txt").Split("\n");

        public static int max = 0;

        public static void Part1()
        {
            Console.WriteLine("Received {0} items", puzzleInput.Length);

            int max = 0;

            int currentlyHeldCalories = 0;


            foreach (string item in puzzleInput)
            {

                Console.WriteLine($"Current item: {item}");

                if (string.IsNullOrWhiteSpace(item))
                {
                    Console.WriteLine("End");
                    if (max < currentlyHeldCalories)
                    {
                        Console.WriteLine($"New maximum value detected ({currentlyHeldCalories}). Old: {max}");
                        max = currentlyHeldCalories;
                    }
                    currentlyHeldCalories = 0;
                    continue;
                }

                currentlyHeldCalories += int.Parse(item);


            }
            Console.WriteLine("Max was {0}", max);
        }

        public static void Main()
        {
            int currentlyHeldCalories = 0;
            List<int> largestNumbers = new();


            List<int> calloriesHeld = new();

            foreach (string item in puzzleInput)
            {


                Console.WriteLine($"Current item: {item}");

                if (string.IsNullOrWhiteSpace(item))
                {
                    Console.WriteLine("End");
                    calloriesHeld.Add(currentlyHeldCalories);
                    currentlyHeldCalories = 0;
                    continue;
                }
                currentlyHeldCalories += int.Parse(item);
            }

            for (int x = 0; x < 3; x++)
            {
                int max = 0;
                int index = 0;
                for (int i = 0; i < calloriesHeld.Count; i++)
                {
                    if (calloriesHeld[i] > max)
                    {
                        max = calloriesHeld[i];
                        index = i;
                        Console.WriteLine($"New max value of {max} was found at index {index} | MLEN: {calloriesHeld.Count}");
                    }
                }
                largestNumbers.Add(max);
                calloriesHeld.RemoveAt(index);
            }
            int total = 0;

            foreach (int item in largestNumbers)
            {
                total += item;
            }

            Console.WriteLine("Total is {0}", total);
        }
    }
}