var input = File.ReadAllText("input.txt");

// Part 1
var calories = input.Split("\n\n").Select(g => g.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Sum());
Console.WriteLine(calories.Max());

// Part 2
var top3Calories = calories.OrderDescending().Take(3);
Console.WriteLine(top3Calories.Sum());