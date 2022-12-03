var input = File.ReadAllLines("input.txt");

var priorities = input.Select(line => new[] {..(line.Length/2), (line.Length/2)..}.Select(range => line[range]).ToArray()).Select(halves => halves[0].Intersect(halves[1]).Single() - 'a').Select(chr => chr < 0 ? chr + 32+26+1 : chr+1);
Console.WriteLine(priorities.Sum());

var badgePriorities = input.Chunk(3).Select(group => group.ToArray()).Select(group => group[0].Intersect(group[1]).Intersect(group[2]).Single() - 'a').Select(chr => chr < 0 ? chr + 32+26+1 : chr+1);
Console.WriteLine(badgePriorities.Sum());