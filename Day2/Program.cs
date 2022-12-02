var input = File.ReadAllLines("input.txt");

var dic = new Dictionary<string, (int part1, int part2)>
{
    ["A X"] = (3+1, 0+3),
    ["B X"] = (0+1, 0+1),
    ["C X"] = (6+1, 0+2),
    ["A Y"] = (6+2, 3+1),
    ["B Y"] = (3+2, 3+2),
    ["C Y"] = (0+2, 3+3),
    ["A Z"] = (0+3, 6+2),
    ["B Z"] = (6+3, 6+3),
    ["C Z"] = (3+3, 6+1)
};

Console.WriteLine(input.Sum(x => dic[x].part1));
Console.WriteLine(input.Sum(x => dic[x].part2));