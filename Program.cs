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
		public static void Main()
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
					if(opponentsMove == 'A')
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
					if(opponentsMove == 'A')
					{
						result += 6 + points['B'];
					}
					else if (opponentsMove == 'B')
					{
						result += 6 + points['C'];
					}
					else if(opponentsMove == 'C')
					{
						result += 6 + points['A'];
					}
				}
			}

			PublicMethods.DrawAnswer(ConsoleColor.Red, "Final result: ", result);
		}
	}

	public static class PublicMethods
	{
		public static void DrawAnswer(ConsoleColor lineColor, string message, object data)
		{
			Console.ForegroundColor = lineColor;
			Console.WriteLine("\n-----------------------------------------------------\n");
			Console.ResetColor();
			Console.WriteLine("		{0:10}{1:35}", message, data.ToString());
			Console.ForegroundColor = lineColor;
			Console.WriteLine("\n-----------------------------------------------------\n");
			Console.ResetColor();
		}
	}
}