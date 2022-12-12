var input = File.ReadAllLines("input.txt");
var map = new int[input[0].Length, input.Length];
var startPos = (x: 0, y: 0);
var endPos = (x: 0, y: 0);
for (int y = 0; y < input.Length; y++)
for (int x = 0; x < input[y].Length; x++)
{
    map[x, y] = input[y][x];
    if (input[y][x] == 'S')
    {
        startPos = (x, y);
        map[x, y] = 'a';
    }

    if (input[y][x] == 'E')
    {
        endPos = (x, y);
        map[x, y] = 'z';
    }
}

// Part 1
{
    var queue = new PriorityQueue<(int x, int y), int>();
    var visited = new HashSet<(int x, int y)>();
    var distances = new Dictionary<(int x, int y), int>();
    queue.Enqueue(startPos, 0);
    visited.Add(startPos);
    distances.Add(startPos, 0);
    while (queue.Count > 0)
    {
        var current = queue.Dequeue();
        if (current == endPos)
            break;
        foreach (var next in new[] { (x: current.x - 1, y: current.y), (x: current.x + 1, y: current.y), (x: current.x, y: current.y - 1), (x: current.x, y: current.y + 1) })
        {
            if (next.x < 0 || next.x >= map.GetLength(0) || next.y < 0 || next.y >= map.GetLength(1))
                continue;
            if (visited.Contains(next))
                continue;
            if (map[next.x, next.y] - map[current.x, current.y] > 1)
                continue;
            visited.Add(next);
            queue.Enqueue(next, distances[current] + 1);
            distances.Add(next, distances[current] + 1);
        }
    }

    Console.WriteLine(distances[endPos]);
}

// Part 2
{
    var queue = new PriorityQueue<(int x, int y), int>();
    var visited = new HashSet<(int x, int y)>();
    var distances = new Dictionary<(int x, int y), int>();
    queue.Enqueue(endPos, 0);
    visited.Add(endPos);
    distances.Add(endPos, 0);
    var goal = (0, 0);
    while (queue.Count > 0)
    {
        var current = queue.Dequeue();
        if (map[current.x, current.y] == 'a')
        {
            goal = current;
            break;
        }
        foreach (var next in new[] { (x: current.x - 1, y: current.y), (x: current.x + 1, y: current.y), (x: current.x, y: current.y - 1), (x: current.x, y: current.y + 1) })
        {
            if (next.x < 0 || next.x >= map.GetLength(0) || next.y < 0 || next.y >= map.GetLength(1))
                continue;
            if (visited.Contains(next))
                continue;
            if (map[current.x, current.y] - map[next.x, next.y] > 1)
                continue;
            visited.Add(next);
            queue.Enqueue(next, distances[current] + 1);
            distances.Add(next, distances[current] + 1);
        }
    }

    Console.WriteLine(distances[goal]);
}