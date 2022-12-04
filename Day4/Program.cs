var input = File.ReadAllLines("input.txt");
var parsedInput =
    input.Select(line => line.Split(",").Select(ass => ass.Split("-").Select(int.Parse).ToArray()).ToArray());

Console.WriteLine(parsedInput.Count(a => (a[0][0] >= a[1][0] && a[0][1] <= a[1][1]) || (a[1][0] >= a[0][0] && a[1][1] <= a[0][1])));
Console.WriteLine(parsedInput.Count(a => (a[0][0] >= a[1][0] && a[0][0] <= a[1][1]) || (a[1][0] >= a[0][0] && a[1][0] <= a[0][1])));