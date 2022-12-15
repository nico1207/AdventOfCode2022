var input = File.ReadAllLines("input.txt").Select(l => l.Split('=', ',', ':').Where(p => int.TryParse(p, out _)).Select(int.Parse).ToArray()).Select(a => (sensor: (x: a[0], y:a[1]), beacon: (x: a[2], y: a[3])));
var distances = input.Select(i => (i.sensor, i.beacon, distance: Math.Abs(i.sensor.x - i.beacon.x) + Math.Abs(i.sensor.y - i.beacon.y))).ToArray();
int maxDistance = distances.Max(d => d.distance);
int minX = distances.Min(d => d.sensor.x);
int maxX = distances.Max(d => d.sensor.x);


int testY = 2000000;
int count = 0;
for (int x = minX - maxDistance; x <= maxX + maxDistance; x++)
{
    var testPos = (x: x, y: testY);
    var visible = distances.Any(d => d.distance >= Math.Abs(d.sensor.x - testPos.x) + Math.Abs(d.sensor.y - testPos.y));
    var isBeaconAtTestPos = distances.Any(d => d.beacon.x == testPos.x && d.beacon.y == testPos.y);
    if (visible && !isBeaconAtTestPos)
        count++;
}
Console.WriteLine(count);


var circlePoints = new List<(int x, int y)>();
foreach (var v1 in distances)
{
    for (int x = v1.sensor.x - v1.distance-1; x <= v1.sensor.x + v1.distance+1; x++)
    {
        if (x is < 0 or >= 4000000) continue;
        
        var y1 = v1.sensor.y + Math.Abs(v1.distance+1 - Math.Abs(v1.sensor.x - x));
        var y2 = v1.sensor.y - Math.Abs(v1.distance+1 - Math.Abs(v1.sensor.x - x));

        if (y1 is >= 0 and <= 4000000 && !distances.Any(d => d.distance >= Math.Abs(d.sensor.x - x) + Math.Abs(d.sensor.y - y1)))
            circlePoints.Add((x, y1));
        
        if (y2 is >= 0 and <= 4000000 && !distances.Any(d => d.distance >= Math.Abs(d.sensor.x - x) + Math.Abs(d.sensor.y - y2)))
            circlePoints.Add((x, y2));
    }
}

Console.WriteLine(circlePoints[0]);