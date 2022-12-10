using MoreLinq;

var input = File.ReadAllLines("input.txt");
var history = input.Select(l => l.Split(' ')).Scan((cycle: 1, register: 1), (state, cmd) => cmd switch
{
    ["noop"] => (state.cycle + 1, state.register),
    ["addx", var value] => (state.cycle + 2, state.register + int.Parse(value)),
});

Console.WriteLine(Enumerable.Range(0, 6).Select(n => n*40+20).Sum(s => history.Last(v => v.cycle <= s).register * s));

for (int i = 1; i <= 240; i++)
{
    var spritePos = history.Last(v => v.cycle <= i).register;
    Console.Write((spritePos >= i % 40-2 && spritePos <= i % 40 ? "##" : "  ") + (i % 40 == 0 ? "\n" : ""));
}