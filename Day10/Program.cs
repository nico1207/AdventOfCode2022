using MoreLinq;

var input = File.ReadAllLines("input.txt");
var registerHistory = input.Select(l => l.Split(' ')).Scan((cycle: 1, register: 1), (state, cmd) => cmd switch
{
    ["noop"] => (state.cycle + 1, state.register),
    ["addx", var value] => (state.cycle + 2, state.register + int.Parse(value)),
});

var samples = new[] { 20, 60, 100, 140, 180, 220 };
Console.WriteLine(samples.Sum(s => registerHistory.Last(v => v.cycle <= s).register * s));

for (int i = 1; i <= 240; i++)
{
    var registerDuringCycle = registerHistory.Last(v => v.cycle <= i).register % 40;
    var drawPos = i % 40;
    Console.Write((registerDuringCycle >= drawPos-2 && registerDuringCycle <= drawPos ? "##" : "  ") + (drawPos == 0 ? "\n" : ""));
}