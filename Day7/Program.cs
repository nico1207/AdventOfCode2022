using System.Collections.Immutable;

var data = File.ReadAllLines("input.txt").Aggregate((dict: ImmutableDictionary<string, long>.Empty.SetItem("/", 0), curDir: "/"), (i, s) => s.Split(" ") switch
{
    ["dir", _] or ["$", "ls"] or ["$", "cd", "/"] => i,
    ["$", "cd", ".."] => (i.dict, i.curDir[..i.curDir[..^1].LastIndexOf("/")] + "/"),
    ["$", "cd", var path] => (i.dict.SetItem(i.curDir + path + "/", 0), i.curDir + path + "/"),
    [var size, _] => (i.dict.SetItems(Enumerable.Range(0, i.curDir.Count(c => c == '/')+1).Select(j => string.Join('/', i.curDir.Split('/').Take(j)) + "/").Select(k => new KeyValuePair<string, long>(k, i.dict[k] + long.Parse(size)))), i.curDir)
});

Console.WriteLine(data.dict.Where(s => s.Value <= 100000).Sum(s => s.Value));

var spaceNeeded = 30000000 - (70000000 - data.dict["/"]);
var firstFile = data.dict.Where(s => s.Value > spaceNeeded).MinBy(s => s.Value);
Console.WriteLine(firstFile.Value);
