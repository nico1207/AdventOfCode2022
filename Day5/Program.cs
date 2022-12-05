var input = File.ReadAllLines("input.txt");

var stacks1 = Enumerable.Range(0, 9).Select(i => new Stack<char>(Enumerable.Range(0, 8).Reverse().Select(j => input[j][i * 4 + 1]).Where(c => !char.IsWhiteSpace(c)).ToList())).ToArray();
input[10..].Select(l => l.Split(' ').Where(part => int.TryParse(part, out _)).Select(int.Parse).ToArray()).ToList().ForEach(a => Enumerable.Range(0, a[0]).ToList().ForEach(_ => stacks1[a[2]-1].Push(stacks1[a[1]-1].Pop())));
stacks1.ToList().ForEach(s => Console.Write(s.Pop()));

var stacks2 = Enumerable.Range(0, 9).Select(i => new List<char>(Enumerable.Range(0, 8).Reverse().Select(j => input[j][i * 4 + 1]).Where(c => !char.IsWhiteSpace(c)).ToList())).ToArray();
input[10..].Select(l => l.Split(' ').Where(part => int.TryParse(part, out _)).Select(int.Parse).ToArray()).ToList().ForEach(a => { stacks2[a[2] - 1].AddRange(stacks2[a[1] - 1].TakeLast(a[0])); stacks2[a[1] - 1].RemoveRange(stacks2[a[1] - 1].Count - a[0], a[0]);});
stacks2.ToList().ForEach(s => Console.Write(s[^1]));