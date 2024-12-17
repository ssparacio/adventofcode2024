

using System.Runtime.Intrinsics.Arm;

Console.WriteLine($"[{DateTime.Now.Second}:{DateTime.Now.Millisecond}] Starting ... ");


//
// Load Input
// 
string input = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),"input.txt"));


(List<int> list1, List<int> list2) = ParseInput(input);

Console.WriteLine($"[{DateTime.Now.Second}:{DateTime.Now.Millisecond}] Load Finished: List1: {list1.Count} List2: {list2.Count} ");


Console.WriteLine($"[{DateTime.Now.Second}:{DateTime.Now.Millisecond}] Sorting ... "); 

list1.Sort();
list2.Sort();

Console.WriteLine($"[{DateTime.Now.Second}:{DateTime.Now.Millisecond}] Sorting Done ");

Console.WriteLine($"[{DateTime.Now.Second}:{DateTime.Now.Millisecond}] Starting Diff Calcs ");

var totalD = 0; 

for (int i = 0; i < list1.Count; i++)
{
    var d = 0;
    
    if (list1[i] > list2[i])
        d = list1[i] - list2[i];
    else 
        d = list2[i] - list1[i];   


    Console.WriteLine($"[{DateTime.Now.Second}:{DateTime.Now.Millisecond}] 1: {list1[i]} 2: {list2[i]} d: {d} ");
    totalD += d; 
}

Console.WriteLine($"[{DateTime.Now.Second}:{DateTime.Now.Millisecond}] Total Diff: {totalD} ");

Console.WriteLine("");
Console.WriteLine("");

Console.WriteLine($"[{DateTime.Now.Second}:{DateTime.Now.Millisecond}]  Starting similarity score ");

var ss = 0; 

for (int i = 0; i < list1.Count; i++)
{
    var n = list1[i];

    var uniques = list2.Where(x => x == n).Count();


    var scoreBump = 0;

    if (uniques > 0)
    {
        scoreBump = n * uniques;

        Console.WriteLine($"[{DateTime.Now.Second}:{DateTime.Now.Millisecond}] n: {n} - uniques: {uniques} - scoreBump: {scoreBump}");

        ss += scoreBump;
    }
}

Console.WriteLine($"[{DateTime.Now.Second}:{DateTime.Now.Millisecond}] Sim Score: {ss} "); 

    Console.WriteLine("");
Console.WriteLine("");



Console.WriteLine("... Press Any Key to Exit. "); 

Console.ReadLine();


// 50089607 is too high. 10:43pm 
// 1709607  is too low. 11:00pm 
// 2176849 That's the right answer! You are one gold star closer to finding the Chief Historian. 11:03pm!

//Sim Score:: 23384288 Right! 11:15pm 


static (List<int>, List<int>) ParseInput(string input)
{
    // Initialize the lists
    var list1 = new List<int>();
    var list2 = new List<int>();

    // Split the input into lines
    string[] lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

    // Process each line
    foreach (string line in lines)
    {
        // Split the line by whitespace
        string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 2)
        {
            // Parse and add numbers to respective lists
            if (int.TryParse(parts[0], out int num1) && int.TryParse(parts[1], out int num2))
            {
                list1.Add(num1);
                list2.Add(num2);
            }
        }
    }

    return (list1, list2);
}