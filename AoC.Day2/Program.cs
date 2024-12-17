using System.Reflection.PortableExecutable;

namespace AoC.Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string input = File.ReadAllText("input.txt");



            var reports = ParseTextLines(input); 

            Console.WriteLine($"Loaded {reports.Count()} reports.");

            var goodReports = 0;
            var goodReportsWithDamper = 0; 

            foreach (var report in reports) 
            {
                var p = string.Join(' ', report);

                Console.WriteLine($"Report:: {p}");


                //Testing for no movement
                //Testing for single direction
                //returning error count as well 
                var direction = GetDirection(report); 

                Console.WriteLine($"Error count after Getdirection: {direction.Item2}"); 

                //Testing to make sure the range of movement are no more than 3
                var diffTests = GetDiffs(direction.Item1, report);

                var totalErrorCount = diffTests + direction.Item2; 

                Console.WriteLine($"Error count after getdiffs: {totalErrorCount}");


                Console.WriteLine("");

                if (totalErrorCount == 0)
                {
                    goodReports++; 
                }
                else if((totalErrorCount - 1) == 0)
                {
                    goodReportsWithDamper++; 
                }
            }

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine($"Good Reports: {goodReports}"); 
            Console.WriteLine($"Good Reports Saved with Damper: {goodReportsWithDamper}");
            Console.WriteLine($"Good Reports + Damper : {goodReportsWithDamper+goodReports}");


            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Done.. ");

            //324 - 12:02am - That's not the right answer; your answer is too high.
            //282 - 12:28am - That's the right answer! You are one gold star closer to finding the Chief Historian.

            //pt2: 
            //347 - 12:38am - That's not the right answer; your answer is too low.
            //329 - 1:29am - That's not the right answer; your answer is too low.

            Console.ReadLine();
        }

        private static int GetDiffs(LevelDirection item1, List<int> report)
        {
            var c = -1;
            var errCount = 0; 

            foreach(var level in report)
            {
                if(c == -1)
                {
                    c = level; 
                }
                else
                {                  
                    var d = Math.Abs(c - level);

                    //test 1B: increase or decrease can't be more than 3
                    if (d >= 1 && d <= 3)
                    {
                        System.Diagnostics.Debug.Write($"OK ");
                    }
                    else
                    {
                        if (d != 0)
                        {
                            Console.WriteLine($"c: {c} - d: {d}");
                            errCount++;
                        }
                    }                    
                }

                c = level; 
            }

            return errCount; 
        }

        private static Tuple<LevelDirection, int> GetDirection(List<int> report)
        {

            var c = -1;
            var errCount = 0;
            var cDir = LevelDirection.Unknown; 

            foreach(var level in report)
            {
                //Set that 1st level
                if (c == -1) 
                    c = level; 
                else
                {
                    //This means that we did not go up or down 
                    if (level == c)
                        errCount++; 
                    else
                    {
                        if(cDir == LevelDirection.Unknown)
                        {
                            if (level > c) cDir = LevelDirection.Up;

                            if (level < c) cDir = LevelDirection.Down; 
                        }
                        else
                        {
                            if(cDir == LevelDirection.Up)
                            {
                                if (level < c) errCount++; 
                            }

                            if(cDir == LevelDirection.Down)
                            {
                                if(level > c) errCount++;
                            }
                        }
                    }
                }

                c = level; 
            }

            
            return new Tuple<LevelDirection, int>(cDir, errCount); ;
        }

        static List<List<int>> ParseTextLines(string input)
        {
            // Initialize the list of lists
            List<List<int>> parsedLines = new List<List<int>>();

            // Split the input into lines
            string[] lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // Process each line
            foreach (string line in lines)
            {
                // Split the line into parts by whitespace and parse them as integers
                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> numbers = new List<int>();

                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int number))
                    {
                        numbers.Add(number);
                    }
                }

                // Add the parsed numbers to the list of lists
                parsedLines.Add(numbers);
            }

            return parsedLines;
        }

        private enum LevelDirection { Unknown, Up, Down }
        
    }
}
