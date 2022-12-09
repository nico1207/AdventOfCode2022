int SimulateTail(int length)
{
    var tailPositions = new HashSet<(int x, int y)>();
    var positions = new (int x, int y)[length];

    foreach (var cmd in File.ReadAllLines("input.txt").Select(l => l.Split(' ')).Select(a => (dir: a[0], steps: int.Parse(a[1]))))
    {
        for (int step = 0; step < cmd.steps; step++)
        {
            positions[0] = cmd.dir switch
            {
                "R" => (positions[0].x + 1, positions[0].y),
                "L" => (positions[0].x - 1, positions[0].y),
                "U" => (positions[0].x, positions[0].y + 1),
                "D" => (positions[0].x, positions[0].y - 1)
            };

            for (int i = 1; i < length; i++)
            {
                if (Math.Abs(positions[i - 1].x - positions[i].x) > 1 || Math.Abs(positions[i - 1].y - positions[i].y) > 1)
                {
                    positions[i] = (positions[i].x + Math.Sign(positions[i - 1].x - positions[i].x), positions[i].y + Math.Sign(positions[i - 1].y - positions[i].y));
                }
            }

            tailPositions.Add(positions[^1]);
        }
    }

    return tailPositions.Count;
}

Console.WriteLine(SimulateTail(2));
Console.WriteLine(SimulateTail(10));