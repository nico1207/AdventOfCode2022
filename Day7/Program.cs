Dictionary<string, long> sizes = new Dictionary<string, long>();
string currentPath = "";

foreach (var line in File.ReadAllLines("input.txt").Select(l => l.Split(" ")))
{
    switch (line)
    {
        case ["dir", _]:
        case ["$", "ls"]:
            break;
        case ["$", "cd", var path]:
            currentPath = Path.GetFullPath(Path.Combine(currentPath, path));
            sizes.TryAdd(currentPath, 0);
            break;
        case [var size, _]:
            string directory = currentPath;
            while (directory != @"C:\")
            {
                sizes[directory] += long.Parse(size);
                directory = Path.GetFullPath(Path.Combine(directory, ".."));
            }
            sizes[directory] += long.Parse(size); // Add to root
            break;
    }
}

Console.WriteLine(sizes.Where(s => s.Value <= 100000).Sum(s => s.Value));

var spaceNeeded = 30000000 - (70000000 - sizes[@"C:\"]);
var firstFile = sizes.Where(s => s.Value > spaceNeeded).MinBy(s => s.Value);
Console.WriteLine(firstFile.Value);