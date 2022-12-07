var sizes = new Dictionary<string, long>();
var currentPath = "";

foreach (var line in File.ReadAllLines("input.txt"))
{
    if (line.StartsWith("$ cd "))
    {
        currentPath = Path.GetFullPath(Path.Combine(currentPath, line[5..]));
        sizes.TryAdd(currentPath, 0);
    } 
    else if (char.IsDigit(line[0]))
    {
        var size = int.Parse(line[..line.IndexOf(' ')]);
        var directory = currentPath;
        while (directory != "")
        {
            sizes[directory] += size;
            directory = directory == @"C:\" ? "" : Path.GetFullPath(Path.Combine(directory, ".."));
        }
    }
}

Console.WriteLine(sizes.Values.Where(s => s <= 100000).Sum(s => s));
Console.WriteLine(sizes.Values.Where(s => s > sizes[@"C:\"] - 40000000).MinBy(s => s));