var input = File.ReadAllLines("input.txt").Where(line => !string.IsNullOrWhiteSpace(line)).Chunk(6).Select(c => c.ToArray());
var monkeys = input.Select(i =>
{
    var items = i[1][17..].Split(", ").Select(long.Parse).ToList();
    var operation = i[2][19..].Split(' ');
    var operationFunc = operation switch
    {
        ["old", "+", var n] => (Func<long, long>)(x => x + long.Parse(n)),
        ["old", "*", "old"] => (Func<long, long>)(x => x * x),
        ["old", "*", var n] => (Func<long, long>)(x => x * long.Parse(n)),
    };
    var divisor = int.Parse(i[3][20..]);
    var trueIndex = int.Parse(i[4][28..]);
    var falseIndex = int.Parse(i[5][29..]);
    return new Monkey(items, operationFunc, divisor, trueIndex, falseIndex);
}).ToList();

long PerformRounds(int rounds, bool part2)
{
    var inspections = new long[monkeys.Count];
    var modulo = monkeys.Select(m => m.testDivisor).Aggregate(1L, (a, b) => a * b);
    for (int round = 0; round < rounds; round++)
        foreach (var monkey in monkeys)
            for (int item = monkey.items.Count-1; item >= 0; item--)
            {
                inspections[monkeys.IndexOf(monkey)]++;
                monkey.items[item] = monkey.operation(monkey.items[item]);
                monkey.items[item] = part2 ? monkey.items[item] % modulo : monkey.items[item] / 3;
                var targetIndex = monkey.items[item] % monkey.testDivisor == 0 ? monkey.trueIndex : monkey.falseIndex;
                monkeys[targetIndex].items.Add(monkey.items[item]);
                monkey.items.RemoveAt(item);
            }

    return inspections.OrderDescending().Take(2).Aggregate((long)1, (a, b) => a * b);
}

Console.WriteLine(PerformRounds(20, false));
Console.WriteLine(PerformRounds(10000, true));

record Monkey(List<long> items, Func<long, long> operation, int testDivisor, int trueIndex, int falseIndex);