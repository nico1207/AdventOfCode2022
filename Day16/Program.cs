using System.Collections.Immutable;

var input = File.ReadAllLines("input.txt").Select(l => l.Replace("valve ", "valves ")); // fuck this shit
int id = 0;
var valves = input.Select(l => new Valve(++id, l[6..8], int.Parse(l[23..26].Replace(";", "")), l[49..].Trim().Split(", "), new Dictionary<int, int>())).ToDictionary(v => v.id, v => v);
var valvesByName = valves.Values.ToDictionary(v => v.name, v => v);
var significantValves = valves.Values.Where(v => v.flowrate > 0).ToArray();

int CalculateDistance(Valve start, Valve end)
{
    // Distances are always +1 to include time needed for opening the valves
    var queue = new Queue<(Valve valve, int distance)>();
    queue.Enqueue((start, 0));
    var visited = new HashSet<Valve>();
    while (queue.Count > 0)
    {
        var (valve, distance) = queue.Dequeue();
        if (valve == end)
            return distance + 1;
        visited.Add(valve);
        foreach (var next in valve.targets.Select(c => valvesByName[c]).Where(v => !visited.Contains(v)))
            queue.Enqueue((next, distance + 1));
    }

    return 0;
}

// Reduce graph to only significant nodes
foreach (var valve in significantValves.Append(valvesByName["AA"]))
{
    foreach (var otherValve in significantValves)
    {
        if (valve == otherValve) continue;
        valve.distances.Add(otherValve.id,
            valve.targets.Contains(otherValve.name) ? 2 : CalculateDistance(valve, otherValve));
    }
}

int maxFlowrate = 0;

void Traverse(int currentDistance, ImmutableHashSet<int> visited, Valve currentValve, int currentFlowrate)
{
    if (currentDistance > 30) return;
    if (currentFlowrate > maxFlowrate)
    {
        maxFlowrate = currentFlowrate;
        Console.WriteLine($"Part 1: New max flowrate: {maxFlowrate}");
    }
    
    foreach (var (id, distance) in currentValve.distances)
    {
        if (visited.Contains(id)) continue;
        var nextValve = valves[id];
        Traverse(currentDistance + distance, visited.Add(id), nextValve, currentFlowrate + nextValve.flowrate * (30 - currentDistance - distance));
    }
}

var aa = valvesByName["AA"];
foreach (var significantValve in significantValves)
{
    var dist = CalculateDistance(aa, significantValve);
    Traverse(dist, ImmutableHashSet<int>.Empty.Add(significantValve.id), significantValve, significantValve.flowrate * (30 - dist));
}

// Part 2 was shamefully stolen from https://github.com/ckainz11/AdventOfCode2022/blob/main/src/main/kotlin/days/day16/Day16.kt

int maxFlowrate2 = 0;

void Traverse2(int currentDistance, ImmutableHashSet<int> visited, Valve currentValve, int currentFlowrate, bool part2)
{
    if (currentDistance > 26) return;
    if (currentFlowrate > maxFlowrate2)
    {
        maxFlowrate2 = currentFlowrate;
        Console.WriteLine($"Part 2: New max flowrate: {maxFlowrate2}");
    }
    
    foreach (var (id, distance) in currentValve.distances)
    {
        if (visited.Contains(id)) continue;
        var nextValve = valves[id];
        Traverse2(currentDistance + distance, visited.Add(id), nextValve, currentFlowrate + nextValve.flowrate * (26 - currentDistance - distance), part2);
    }
    
    if (part2)
        Traverse2(0, visited, aa, currentFlowrate, false);
}

foreach (var significantValve in significantValves)
{
    var dist = CalculateDistance(aa, significantValve);
    Traverse2(dist, ImmutableHashSet<int>.Empty.Add(significantValve.id), significantValve, significantValve.flowrate * (26 - dist), true);
}


record Valve(int id, string name, int flowrate, string[] targets, Dictionary<int, int> distances);