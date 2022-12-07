using System.Collections.Immutable;
using Medallion.Collections;
using MoreLinq.Extensions;
using List = System.Collections.Immutable.ImmutableList<string>;
using Dict = System.Collections.Immutable.ImmutableDictionary<System.Collections.Immutable.ImmutableList<string>, long>;

var data = File.ReadAllLines("input.txt").Aggregate((dict: Dict.Empty.SetItem(List.Empty, 0).WithComparers(EqualityComparers.GetSequenceComparer<string>()), curDir: List.Empty), (i, s) => s.Split(" ") switch
{
    ["dir", _] or ["$", "ls"] or ["$", "cd", "/"] => i,
    ["$", "cd", ".."] => (i.dict, i.curDir.RemoveAt(i.curDir.Count - 1)),
    ["$", "cd", var path] => (i.dict.SetItem(i.curDir.Add(path), 0), i.curDir.Add(path)),
    [var size, _] => (i.dict.SetItems(i.curDir.Scan(List.Empty, (l, s) => l.Add(s)).Select(k => new KeyValuePair<List, long>(k, i.dict[k] + long.Parse(size)))), i.curDir)
});

Console.WriteLine(data.dict.Where(s => s.Value <= 100000).Sum(s => s.Value));

var spaceNeeded = 30000000 - (70000000 - data.dict[List.Empty]);
var firstFile = Enumerable.MinBy(data.dict.Where(s => s.Value > spaceNeeded), s => s.Value);
Console.WriteLine(firstFile.Value);
