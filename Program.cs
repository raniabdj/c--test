using System;
using System.Linq;
using System.Collections.Generic;

namespace willis_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is the name of the file you want to use?");
            var nameFile = Console.ReadLine();
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            Console.WriteLine(nameFile);
            string file = FileHelper.ReadFile(System.IO.Directory.GetCurrentDirectory()+"//"+nameFile);
            //string file = FileHelper.ReadFile(@"\"+nameFile);

            string[] headers = FileHelper.getHeaders(file);
            List<string> body = FileHelper.getBody(file);
            List<string[]> lines = FileHelper.getLines(body);
            List<string> originalYears = FileHelper.getOriginalYears(lines);
            List<string> products = FileHelper.getProducts(lines);
            List<double> values = FileHelper.getValues(lines);
            // double value1 = FileHelper.getValue(lines, "Comp", "1992", "1992");
            // double value2 = FileHelper.getValue(lines, "Non-Comp", "1992", "1992");


            List<string[]> cumulativeLines = new List<string[]>();
            foreach (string product in products)
            {
                foreach (string originalYear in originalYears)
                {
                    double cumulative = 0;

                    for (int i = 0; i < originalYears.Count; i++)
                    {
                        double value = FileHelper.getValue(lines, product, originalYear, originalYears[i]);
                        if (value > 0)
                        {
                            List<string> newLine = new List<string>();
                            newLine.Add(product);
                            newLine.Add(originalYear);
                            cumulative = cumulative + value;
                            newLine.Add(originalYear);
                            newLine.Add(cumulative.ToString().Replace(',', '.'));
                            cumulativeLines.Add(newLine.ToArray());
                        }

                    }
                }
            }
            string[] cumulativeHeaders = headers;
            string result = string.Join(" , ", cumulativeHeaders).Replace("Incremental", "Cumulative");
            foreach (string[] content in cumulativeLines)
            {
                string line = string.Join(" , ", content);
                result = result + line + System.Environment.NewLine;
            }

            FileHelper.WriteFile(@"C:\Users\hp\Desktop\rania space work\willis test\cumulative.txt", result.Trim());


        }
    }
}
