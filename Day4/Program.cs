var input = File.ReadAllLines("input.txt");

var fullOverlaps = input.Select(line => line.Split(",").Select(ass => ass.Split("-").Select(int.Parse).ToArray()).ToArray()).Where(a => (a[0][0] >= a[1][0] && a[0][1] <= a[1][1]) || (a[1][0] >= a[0][0] && a[1][1] <= a[0][1]));
Console.WriteLine(fullOverlaps.Count());

var anyOverlaps = input.Select(line => line.Split(",").Select(ass => ass.Split("-").Select(int.Parse).ToArray()).ToArray()).Where(a => (a[0][0] >= a[1][0] && a[0][0] <= a[1][1]) || (a[1][0] >= a[0][0] && a[1][0] <= a[0][1]));
Console.WriteLine(anyOverlaps.Count());