var input = File.ReadAllLines("input.txt").Where(line => !string.IsNullOrWhiteSpace(line)).Chunk(6).Select(c => c.ToArray());
var monkeys = input.Select(i =>
{
    var items = i[1][17..].Split(", ").Select(long.Parse).ToList();
    var operation = i[2][19..].Split(' ');
    Func<long, long> operationFunc = operation switch
    {
        ["old", "+", var n] => x => x + long.Parse(n),
        ["old", "*", "old"] => x => x * x,
        ["old", "*", var n] => x => x * long.Parse(n),
    };
    var divisor = int.Parse(i[3][20..]);
    var trueIndex = int.Parse(i[4][28..]);
    var falseIndex = int.Parse(i[5][29..]);
    return new Monkey(items, operationFunc, divisor, trueIndex, falseIndex);
}).ToArray();

long PerformRounds(int rounds, bool part2)
{
    var inspections = new long[monkeys.Length];
    var modulo = monkeys.Select(m => m.Divisor).Aggregate(1L, (a, b) => a * b);
    for (var round = 0; round < rounds; round++)
    for (var monkeyIndex = 0; monkeyIndex < monkeys.Length; monkeyIndex++)
    {
        var monkey = monkeys[monkeyIndex];
        foreach (var item in monkey.Items)
        {
            inspections[monkeyIndex]++;
            var newItem = monkey.Operation(item) % modulo;
            if (!part2) newItem /= 3;
            var targetIndex = newItem % monkey.Divisor == 0 ? monkey.TrueIdx : monkey.FalseIdx;
            monkeys[targetIndex].Items.Add(newItem);
        }
        monkey.Items.Clear();
    }
    return inspections.OrderDescending().Take(2).Aggregate(1L, (a, b) => a * b);
}

Console.WriteLine(PerformRounds(20, false));
Console.WriteLine(PerformRounds(10000, true));

record Monkey(List<long> Items, Func<long, long> Operation, int Divisor, int TrueIdx, int FalseIdx);