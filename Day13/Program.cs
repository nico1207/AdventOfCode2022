﻿List<object> ParsePacket(string line)
{
    Stack<List<object>> lists = new Stack<List<object>>();
    for (var i = 0; i < line.Length; i++)
        switch (line[i])
        {
            case '[':
                lists.Push(new List<object>());
                break;
            case ']':
                var list = lists.Pop();
                if (lists.Count == 0)
                    return list;
                lists.Peek().Add(list);
                break;
            case char c when char.IsDigit(c):
                string number = "" + c;
                while (char.IsDigit(line[i + 1]))
                    number += line[++i];
                lists.Peek().Add(int.Parse(number));
                break;
        }

    throw new ArgumentException("Invalid packet");
}

ComparisonResult Compare(object left, object right)
{
    switch (left)
    {
        case int leftInt when right is int rightInt:
            if (leftInt < rightInt) return ComparisonResult.RightOrder;
            if (leftInt > rightInt) return ComparisonResult.WrongOrder;
            return ComparisonResult.CheckNext;
        case int leftInt when right is List<object> rightList:
            return Compare(new List<object> { leftInt }, rightList);
        case List<object> leftList when right is int rightInt:
            return Compare(leftList, new List<object> { rightInt });
        case List<object> leftList when right is List<object> rightList:
            if (leftList.Count == 0 && rightList.Count == 0) return ComparisonResult.CheckNext;
            if (leftList.Count == 0) return ComparisonResult.RightOrder;
            if (rightList.Count == 0) return ComparisonResult.WrongOrder;
            var result = Compare(leftList[0], rightList[0]);
            return result == ComparisonResult.CheckNext ? Compare(leftList.Skip(1).ToList(), rightList.Skip(1).ToList()) : result;
        default:
            throw new Exception("Unknown types");
    }
}

// Part 1
var input = File.ReadAllLines("input.txt").Select(ParsePacket).Chunk(2).Select(c => (left: c.First(), right: c.Last()));
Console.WriteLine(input.Select((pair, index) => (pair, index: index+1)).Where(t => Compare(t.pair.left, t.pair.right) == ComparisonResult.RightOrder).Sum(t => t.index));

// Part 2
var allPackets = File.ReadAllLines("input.txt").Select(ParsePacket).ToList();
var dividerA = new List<object> { new List<object> { 2 } };
var dividerB = new List<object> { new List<object> { 6 } };
allPackets.Add(dividerA);
allPackets.Add(dividerB);
allPackets.Sort((a, b) => (int)Compare(a, b));
Console.WriteLine((allPackets.IndexOf(dividerA)+1) * (allPackets.IndexOf(dividerB)+1));

enum ComparisonResult
{
    RightOrder = -1,
    WrongOrder = 1,
    CheckNext = 0,
}