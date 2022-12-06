using MoreLinq;

var input = File.ReadAllText("input.txt");

var marker1 = input.Window(4).ToList().FindIndex(w => w.Distinct().Count() == 4);
Console.WriteLine(marker1+4);

var marker2 = input.Window(14).ToList().FindIndex(w => w.Distinct().Count() == 14);
Console.WriteLine(marker2+14);