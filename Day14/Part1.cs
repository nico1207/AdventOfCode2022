using System.Diagnostics;

public class Part1 {
    public static void Run1() {
        var paths = File.ReadAllLines("input.txt").Select(l => l.Split(" -> ").Select(l => l.Split(",").Select(int.Parse).ToArray()).Select(a => (x: a[0], y:a[1])).ToArray());
        var sparseMap = new Dictionary<(int x, int y), MapTile>();
        
        foreach (var path in paths)
        {
            for (var i = 0; i < path.Length - 1; i++)
            {
                var (x1, y1) = path[i];
                var (x2, y2) = path[i + 1];
                var (x, y) = (x1, y1);
                while (x != x2 || y != y2)
                {
                    sparseMap.TryAdd((x, y), MapTile.Wall);
                    if (x != x2) x += Math.Sign(x2 - x);
                    if (y != y2) y += Math.Sign(y2 - y);
                }
                sparseMap.TryAdd((x2, y2), MapTile.Wall);
            }
        }
        
        var maxY = sparseMap.Keys.Max(k => k.y)+1;
        var sandSpawn = (x: 500, y: 0);
        
        int sandCount = 0;
        while (true)
        {
            var currentSandPosition = sandSpawn;
            while (true)
            {
                var (x, y) = currentSandPosition;
                if (y == maxY)
                {
                    goto stop;
                }
                if (sparseMap.TryGetValue((x, y + 1), out var tileBelow) && tileBelow is MapTile.Wall or MapTile.Sand)
                {
                    if (sparseMap.TryGetValue((x - 1, y + 1), out var tileDownLeft) && tileDownLeft is MapTile.Wall or MapTile.Sand)
                    {
                        if (sparseMap.TryGetValue((x + 1, y + 1), out var tileDownRight) && tileDownRight is MapTile.Wall or MapTile.Sand)
                        {
                            sparseMap.Add(currentSandPosition, MapTile.Sand);
                            break;
                        }
                        currentSandPosition = (x + 1, y + 1);
                    }
                    else
                    {
                        currentSandPosition = (x - 1, y + 1);
                    }
                }
                else
                {
                    currentSandPosition = (x, y + 1);
                }
            }
            sandCount++;
        }
        
        stop: ;
        
        Console.WriteLine(sandCount);
    }

    public static void Main()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Part1.Run1();
        sw.Stop();
        Console.WriteLine(sw.Elapsed.TotalMilliseconds);
        sw.Restart();
        Part2.Run2();
        sw.Stop();
        Console.WriteLine(sw.Elapsed.TotalMilliseconds);
    }
    
    enum MapTile
    {
        Empty,
        Wall,
        Sand
    }
}