using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

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

        public static void Part2()
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
                        Console.WriteLine($"New max value of {max} was found at index {index} \t| MLEN: {calloriesHeld.Count}");
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
    public static class Day2
    {
        public static string[] puzzleInput => File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/puzzleInputDay22022.txt").Split("\n", StringSplitOptions.RemoveEmptyEntries);

        public static int result = 0;

        /*
		 *      score mechanic
		 * 
		 * 1 - rock         OPP: A  PLR: X
		 * 2 - paper        OPP: B  PLR: Y
		 * 3 - scissors     OPP: C  PLR: Z
		 * 
		 * 0 - lost
		 * 3 - draw
		 * 6 - win
		 */

        static Dictionary<char, int> points = new()
        {
            {'A', 1},
            {'B', 2},
            {'C', 3}
        };

        public static void Part1()
        {
            Console.WriteLine("Received {0} items", puzzleInput.Length); //debug purposes

            foreach (string item in puzzleInput)
            {
                string[] currentMatch = item.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                char opponentsTurn = item[0];
                char myTurn = 'E'; //E for empty

                Console.WriteLine(item);

                switch (currentMatch[1][0])
                {
                    case 'X':
                        myTurn = 'A';
                        break;

                    case 'Y':
                        myTurn = 'B';
                        break;

                    case 'Z':
                        myTurn = 'C';
                        break;
                }

                if (myTurn == 'E')
                {
                    throw new Exception("Turn character was empty."); //do not allow for empty turns, can return false results
                }

                if (opponentsTurn == myTurn) //easily exit draws
                {
                    result += 3 + points[myTurn];
                    continue;
                }

                //rest of the logic
                if (myTurn == 'A' && opponentsTurn == 'C') //rock to scissors
                {
                    result += 6 + points[myTurn];
                    Console.WriteLine($"{myTurn} to {opponentsTurn}. Result is now {result}. | NEW POINTS: {6 + points[myTurn]}");
                    continue;
                }
                else if (myTurn == 'B' && opponentsTurn == 'A') //paper to rock
                {
                    result += 6 + points[myTurn];
                    Console.WriteLine($"{myTurn} to {opponentsTurn}. Result is now {result}. | NEW POINTS: {6 + points[myTurn]}");
                    continue;
                }
                else if (myTurn == 'C' && opponentsTurn == 'B') //scissors to paper
                {
                    result += 6 + points[myTurn];
                    Console.WriteLine($"{myTurn} to {opponentsTurn}. Result is now {result}. | NEW POINTS: {6 + points[myTurn]}");
                    continue;
                }
                else
                {
                    result += points[myTurn];
                    Console.WriteLine($"Round lost. {myTurn} to {opponentsTurn}. Result is now {result}. | NEW POINTS: {points[myTurn]}");
                    continue;
                }
            }

            PublicMethods.DrawAnswer(ConsoleColor.Cyan, "Result:", result);
        }
        public static void Part2()
        {
            int result = 0;

            /*
			*      game mechanics
			* 
			*	score
			* 
			* 1 - rock         OPP: A
			* 2 - paper        OPP: B
			* 3 - scissors     OPP: C
			* 
			* 0 - lost
			* 3 - draw
			* 6 - win
			* 
			*	the second thing I dont know how to name
			*	
			* X - lose
			* Y - draw
			* Z - win
			* 
			*/


            foreach (string item in puzzleInput)
            {
                string[] input = item.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                char opponentsMove = input[0][0];


                if (input[1][0] == 'X')
                {
                    //Round demanded for losing
                    if (opponentsMove == 'A')
                    {
                        result += points['C'];
                        continue;
                    }
                    else if (opponentsMove == 'B')
                    {
                        result += points['A'];
                        continue;
                    }
                    else if (opponentsMove == 'C')
                    {
                        result += points['B'];
                        continue;
                    }
                }
                else if (input[1][0] == 'Y') //draw demanded
                {
                    result += 3 + points[opponentsMove];
                    Console.WriteLine($"Round ended with a draw. Got {3 + points[opponentsMove]} points and now result is {result}");
                    continue;
                }
                else if (input[1][0] == 'Z')
                {
                    if (opponentsMove == 'A')
                    {
                        result += 6 + points['B'];
                    }
                    else if (opponentsMove == 'B')
                    {
                        result += 6 + points['C'];
                    }
                    else if (opponentsMove == 'C')
                    {
                        result += 6 + points['A'];
                    }
                }
            }

            PublicMethods.DrawAnswer(ConsoleColor.Red, "Final result: ", result);
        }
    }

    public static class Day3
    {
        public static string[] puzzleInput => File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/puzzleInputDay32022.txt").Split("\n", StringSplitOptions.RemoveEmptyEntries);

        public static char[] lowercaseLetters = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (char)i).ToArray();
        public static char[] uppercaseLetters = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char)i).ToArray();

        public static char[] letters = lowercaseLetters.Concat(uppercaseLetters).ToArray();

        public static void Part1()
        {
            int result = 0;

            foreach (string input in puzzleInput)
            {
                List<char> itemsInFirstCompartment = new();
                List<char> itemsInSecondCompartment = new();

                char repeatedCharacter = '\n'; // new line for empty

                for (int x = 0; x < input.Length / 2; x++)
                {
                    itemsInFirstCompartment.Add(input[x]);
                }
                for (int x = input.Length / 2; x < input.Length; x++)
                {
                    itemsInSecondCompartment.Add(input[x]);
                }

                for (int i = 0; i < itemsInFirstCompartment.Count; i++)
                {
                    if (itemsInFirstCompartment.Contains(itemsInSecondCompartment[i]))
                    {
                        repeatedCharacter = itemsInSecondCompartment[i];
                    }
                }

                if (repeatedCharacter == '\n')
                {
                    throw new Exception("Repeated character cannot be empty."); //dont allow false results
                }

                int pointsToAdd = (int)Array.IndexOf(letters, repeatedCharacter) + 1;

                Console.WriteLine($"Current item: {input}\nFirst compartment = {string.Join("", itemsInFirstCompartment)}\nSecond compartment = {string.Join("", itemsInSecondCompartment)}\nRepeated character: {repeatedCharacter}\nPoints received: {pointsToAdd}\n");
                result += pointsToAdd;
            }
            PublicMethods.DrawAnswer(ConsoleColor.Magenta, "Result: ", result);
        }
        public static void Part2() //unsolved
        {
            List<string> input = puzzleInput.ToList();
            int result = 0;

            List<List<string>> groups = new();

            char repeatedCharacter = '\n'; // new line for empty

            Console.WriteLine($"Received {groups.Count} items");

            foreach (List<string> group in groups)
            {
                char[] firstGroup = group[0].ToCharArray();

                for (int i = 0; i < firstGroup.Length; i++)
                {
                    if (group[1].Contains(firstGroup[i]))
                    {
                        if (group[2].Contains(firstGroup[i]))
                        {
                            repeatedCharacter = firstGroup[i];

                            if (repeatedCharacter == '\n')
                            {
                                throw new Exception("Repeated character cannot be empty."); //dont allow false results
                            }

                            Console.WriteLine("---------------------------------------------------" +
                                $"\nCurrent items: " +
                                $"\n\t{group[0]}" +
                                $"\n\t{group[1]}" +
                                $"\n\t{group[2]}" +
                                $"\n---------------------------------------------------");

                            int pointsToAdd = Array.IndexOf(letters, repeatedCharacter) + 1;
                            result += pointsToAdd;

                            Console.WriteLine($"The repeated character was {repeatedCharacter} ({(int)repeatedCharacter}). Adding {pointsToAdd} pts.");

                            repeatedCharacter = '\n';
                        }
                    }
                }
            }
            PublicMethods.DrawAnswer(ConsoleColor.Green, "Answer: ", result);
        }
    }

    public static class Day6
    {
        public static string example = "bvwbjplbgvbhsrlpgdmjqwftvncz";

        public static string puzzleInput => File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/puzzleInputDay62022.txt");
        public static void Part1()
        {
            string currentlyProcessedData = "";


            for (int i = 0; i < puzzleInput.Length; i++)
            {
                currentlyProcessedData += puzzleInput[i];
                if (currentlyProcessedData.Length >= 4)
                {
                    if (checkForDistinctCharacters(currentlyProcessedData, 4))
                    {
                        Console.WriteLine(currentlyProcessedData);
                        Console.WriteLine($"The start-off-packet marker was completeted at char ind. {i += 1}");
                        break;
                    }
                }
            }
        }
        public static void Main()
        {
            string currentlyProcessedData = "";


            for (int i = 0; i < puzzleInput.Length; i++)
            {
                currentlyProcessedData += puzzleInput[i];
                if (currentlyProcessedData.Length >= 14)
                {
                    if (checkForDistinctCharacters(currentlyProcessedData, 14))
                    {
                        Console.WriteLine(currentlyProcessedData);
                        Console.WriteLine($"The start-off-message marker was completeted at char ind. {i += 1}");
                        break;
                    }
                }
            }
        }

        public static bool checkForDistinctCharacters(string s, int count) //for both parts
        {
            List<char> alreadyExistingCharacters = new();

            for (int i = s.Length - 1; i >= s.Length - count; i--)
            {
                if (alreadyExistingCharacters.Contains(s[i]))
                {
                    Console.WriteLine($"String {s} failed the test.\nThe last chars were {string.Join("", alreadyExistingCharacters)}");
                    return false;
                }
                alreadyExistingCharacters.Add(s[i]);
            }
            Console.WriteLine($"String {s} succeeded.\nThe last chars were {string.Join("", alreadyExistingCharacters)}");

            return true;
        }
    }

    public static class PublicMethods
    {
        public static void DrawAnswer(ConsoleColor lineColor, string message, object data)
        {
            Console.ForegroundColor = lineColor;
            Console.WriteLine("\n-----------------------------------------------------\n");
            Console.ResetColor();
            Console.WriteLine($"\t{message}\t\t {data}");
            Console.ForegroundColor = lineColor;
            Console.WriteLine("\n-----------------------------------------------------\n");
            Console.ResetColor();
        }
    }
}
