var input = File.ReadAllLines("input.txt");
var parsedInput = input.Select(line => line.Split(',', '-').Select(int.Parse).ToArray());

Console.WriteLine(parsedInput.Count(a => (a[0] >= a[2] && a[1] <= a[3]) || (a[2] >= a[0] && a[3] <= a[1])));
Console.WriteLine(parsedInput.Count(a => (a[0] >= a[2] && a[0] <= a[3]) || (a[2] >= a[0] && a[2] <= a[1])));