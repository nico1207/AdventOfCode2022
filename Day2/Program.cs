var input = File.ReadAllLines("input.txt");
var part1Dic = new Dictionary<string, int>
{
    ["A X"] = 3+1,
    ["B X"] = 0+1, 
    ["C X"] = 6+1,
    ["A Y"] = 6+2,
    ["B Y"] = 3+2, 
    ["C Y"] = 0+2,
    ["A Z"] = 0+3,
    ["B Z"] = 6+3, 
    ["C Z"] = 3+3,
};

var part1Result = input.Sum(x => part1Dic[x]);
Console.WriteLine(part1Result);

var part2Dic = new Dictionary<string, int>
{
    ["A X"] = 0+3,
    ["B X"] = 0+1, 
    ["C X"] = 0+2,
    ["A Y"] = 3+1,
    ["B Y"] = 3+2, 
    ["C Y"] = 3+3,
    ["A Z"] = 6+2,
    ["B Z"] = 6+3, 
    ["C Z"] = 6+1,
};

var part2Result = input.Sum(x => part2Dic[x]);
Console.WriteLine(part2Result);