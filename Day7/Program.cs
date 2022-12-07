Dictionary<string, long> sizes = new Dictionary<string, long>();
string currentPath = "";

foreach (var line in File.ReadAllLines("input.txt"))
{
    if (line.StartsWith("$ cd "))
    {
        currentPath = Path.GetFullPath(Path.Combine(currentPath, line.Substring(5)));
        sizes.TryAdd(currentPath, 0);
    } 
    else if (line.StartsWith("$ ls") || line.StartsWith("dir "))
    {
        // ignore
    }
    else
    {
        var parts = line.Split(" ");
        var size = int.Parse(parts[0]);
        string directory = currentPath;
        while (directory != @"C:\")
        {
            sizes[directory] += size;
            directory = Path.GetFullPath(Path.Combine(directory, ".."));
        }
        sizes[directory] += size; // Add to root
    }
}

Console.WriteLine(sizes.Where(s => s.Value <= 100000).Sum(s => s.Value));

var spaceNeeded = 30000000 - (70000000 - sizes[@"C:\"]);
var firstFile = sizes.Where(s => s.Value > spaceNeeded).MinBy(s => s.Value);
Console.WriteLine(firstFile.Value);