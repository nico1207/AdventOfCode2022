var input = File.ReadAllLines("input.txt").Select(l => l.Split(',').Select(int.Parse).ToArray()).Select(a => (x: a[0], y: a[1], z: a[2])).ToArray();

// Part 1
var sides = new List<(float x, float y, float z)>();

foreach (var (x, y, z) in input)
{
    sides.Add((x: x+0.5f, y: y, z: z));
    sides.Add((x: x-0.5f, y: y, z: z));
    sides.Add((x: x, y: y+0.5f, z: z));
    sides.Add((x: x, y: y-0.5f, z: z));
    sides.Add((x: x, y: y, z: z+0.5f));
    sides.Add((x: x, y: y, z: z-0.5f));
}

// Remove all sides that are not unique
var uniqueSides = sides.GroupBy(s => s).Where(g => g.Count() == 1).Select(g => g.Key).ToList();
Console.WriteLine(uniqueSides.Count);


// Part 2

var maxX = input.Max(i => i.x);
var maxY = input.Max(i => i.y);
var maxZ = input.Max(i => i.z);
var grid = new bool[maxX+2, maxY+2, maxZ+2];
foreach (var (x, y, z) in input)
{
    grid[x, y, z] = true;
}

var reachable = new bool[maxX+2, maxY+2, maxZ+2];
reachable[0, 0, 0] = true;
// Perform flood fill to find all reachable positions from (0,0,0)
var queue = new Queue<(int x, int y, int z)>();
queue.Enqueue((0, 0, 0));
while (queue.Count > 0)
{
    var (x, y, z) = queue.Dequeue();
    if (x > 0 && !grid[x-1, y, z] && !reachable[x-1, y, z])
    {
        reachable[x-1, y, z] = true;
        queue.Enqueue((x-1, y, z));
    }
    if (x < maxX+1 && !grid[x+1, y, z] && !reachable[x+1, y, z])
    {
        reachable[x+1, y, z] = true;
        queue.Enqueue((x+1, y, z));
    }
    if (y > 0 && !grid[x, y-1, z] && !reachable[x, y-1, z])
    {
        reachable[x, y-1, z] = true;
        queue.Enqueue((x, y-1, z));
    }
    if (y < maxY+1 && !grid[x, y+1, z] && !reachable[x, y+1, z])
    {
        reachable[x, y+1, z] = true;
        queue.Enqueue((x, y+1, z));
    }
    if (z > 0 && !grid[x, y, z-1] && !reachable[x, y, z-1])
    {
        reachable[x, y, z-1] = true;
        queue.Enqueue((x, y, z-1));
    }
    if (z < maxZ+1 && !grid[x, y, z+1] && !reachable[x, y, z+1])
    {
        reachable[x, y, z+1] = true;
        queue.Enqueue((x, y, z+1));
    }
}

int reachableSides = 0;
foreach (var uniqueSide in uniqueSides)
{
    if (uniqueSide.x < 0 || uniqueSide.y < 0 || uniqueSide.z < 0)
    {
        reachableSides++;
        continue;
    }
    
    if (uniqueSide.x % 1 == 0.5f && (reachable[(int)uniqueSide.x, (int)uniqueSide.y, (int)uniqueSide.z] || reachable[(int)uniqueSide.x+1, (int)uniqueSide.y, (int)uniqueSide.z]))
    {
        reachableSides++;
    }
    else if (uniqueSide.x % 1 == -0.5f && (reachable[(int)uniqueSide.x, (int)uniqueSide.y, (int)uniqueSide.z] || reachable[(int)uniqueSide.x-1, (int)uniqueSide.y, (int)uniqueSide.z]))
    {
        reachableSides++;
    }
    else if (uniqueSide.y % 1 == 0.5f && (reachable[(int)uniqueSide.x, (int)uniqueSide.y, (int)uniqueSide.z] || reachable[(int)uniqueSide.x, (int)uniqueSide.y+1, (int)uniqueSide.z]))
    {
        reachableSides++;
    }
    else if (uniqueSide.y % 1 == -0.5f && (reachable[(int)uniqueSide.x, (int)uniqueSide.y, (int)uniqueSide.z] || reachable[(int)uniqueSide.x, (int)uniqueSide.y-1, (int)uniqueSide.z]))
    {
        reachableSides++;
    }
    else if (uniqueSide.z % 1 == 0.5f && (reachable[(int)uniqueSide.x, (int)uniqueSide.y, (int)uniqueSide.z] || reachable[(int)uniqueSide.x, (int)uniqueSide.y, (int)uniqueSide.z+1]))
    {
        reachableSides++;
    }
    else if (uniqueSide.z % 1 == -0.5f && (reachable[(int)uniqueSide.x, (int)uniqueSide.y, (int)uniqueSide.z] || reachable[(int)uniqueSide.x, (int)uniqueSide.y, (int)uniqueSide.z-1]))
    {
        reachableSides++;
    }
}

Console.WriteLine(reachableSides);