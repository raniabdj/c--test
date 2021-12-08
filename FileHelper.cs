using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
class FileHelper
{

    public static string ReadFile(string path)
    {
        string file = "";
        if (!File.Exists(path))
        {
            Console.WriteLine("file doesn't exist");
        }
        file = File.ReadAllText(path);
        return file.Trim();
    }
    public static void WriteFile(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    public static List<string[]> getLines(List<string> lines)
    {
        List<string[]> body = new List<string[]>();
        foreach (string line in lines)
        {
            string[] lineItems = line.Split(',');
            body.Add(lineItems);
        }
        return body;
    }

    public static List<string> getProducts(List<string[]> body)
    {
        List<string> products = new List<string>();
        foreach (string[] line in body)
        {
            products.Add(line[0].Trim());
        }
        HashSet<string> newSet = new HashSet<string>(products);
        return newSet.ToList();
    }
    public static string[] getHeaders(string file)
    {
        return file.Split('\n')[0].Split(',');
    }
    public static List<string> getBody(string file)
    {
        List<string> body = new List<string>(file.Split('\n'));
        body.RemoveAt(0);
        return body;
    }

    public static List<string> getOriginalYears(List<string[]> body)
    {
        List<string> originals = new List<string>();
        foreach (string[] line in body)
        {
            originals.Add(line[1].Trim());
        }
        HashSet<string> newSet = new HashSet<string>(originals);
        return newSet.ToList();
    }

    public static List<double> getValues(List<string[]> body)
    {
        List<double> values = new List<double>();
        foreach (string[] line in body)
        {
            double value = double.Parse(line[line.Length - 1], System.Globalization.CultureInfo.InvariantCulture);
            values.Add(value);
        }
        return values;
    }

    public static double getValue(List<string[]> lines, string product, string originalYear, string developement)
    {
        double finalValue = 0;

        List<string[]> value = lines.Where(x => (x[0].Trim().Equals(product) && x[1].Trim().Equals(originalYear) && x[2].Trim().Equals(developement))).ToList();
        if (value.Count > 0)
        {
            finalValue = double.Parse(value[0][3], System.Globalization.CultureInfo.InvariantCulture);
        }
        return finalValue;
    }


}

