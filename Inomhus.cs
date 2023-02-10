using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GruppUppgift_Väderdata
{
    public class Inomhus
    {
        static string pattern = @"(\d{4}-\d{2}-\d{2})\s(\d{2}:\d{2}:\d{2}),(\w+),(\d+\.\d+),(\d+)";
        static string filename = @"C:\Users\danie\source\repos\Väderdata\Väderdata\Bilal-Daniel-Gruppuppgift-V-derdata\Textfiler\tempdata5-med fel.txt";
        public static void SökmöjlighetInne()
        {
            Regex regex = new Regex(pattern);
      
            string[] lines = System.IO.File.ReadAllLines(filename);
            Console.WriteLine("Ange datumet du vill kolla medeltemperaturen på");
            string datum = Console.ReadLine();
            var temperatureData = new List<double>();
            foreach (string line in lines)
            {
                Match match = regex.Match(line);
                string date = match.Groups[1].Value;
                string time = match.Groups[2].Value;
                string location = match.Groups[3].Value;
                string temperature = match.Groups[4].Value.Replace(".", ",");
                double Medeltemp;
                if (double.TryParse(temperature, out Medeltemp) && location == "Inne" && date == datum)
                {
                    temperatureData.Add(Medeltemp);
                    Console.WriteLine("Datum: {0}, Tid: {1}, Plats: {2}, Temperatur: {3}",
                                          date, time, location, Medeltemp);
                }
            }
            double averageTemperature = temperatureData.Average();
            string Datum = "Datum: " + datum;
            Datum.ViewBox();
            Console.WriteLine("MedelTemperatur: {0:F2}",
                                  averageTemperature);
        }
        public static void SorteringMedeltemperaturInne()
        {
            Regex regex = new Regex(pattern);
            string[] lines = System.IO.File.ReadAllLines(filename);
            var temperatureData = new List<double>();
            foreach (string line in lines)
            {
                Match match = regex.Match(line);
                string date = match.Groups[1].Value;
                string location = match.Groups[3].Value;
                string temperature = match.Groups[4].Value.Replace(".", ",");
                double Medeltemp;
                if (double.TryParse(temperature, out Medeltemp) && location == "Inne")
                {
                    temperatureData.Add(Medeltemp);
                }
            }
            var dataByDate = lines
                .Select(line =>
                {
                    Match match = regex.Match(line);
                    string date = match.Groups[1].Value;
                    string temperature = match.Groups[4].Value.Replace(".", ",");
                    double Medeltemp;
                    double Medelluftfuktighet;
                    if (double.TryParse(temperature, out Medeltemp) && match.Groups[3].Value == "Inne")
                    {
                        return new { Date = date, Temperature = Medeltemp };
                    }
                    return null;
                })
                .Where(x => x != null)
                .GroupBy(x => x.Date)
                .OrderBy(group => group.Average(d => d.Temperature))
                .ToList();

            foreach (var group in dataByDate)
            {
                Console.WriteLine("Datum: {0}", group.Key);
                Console.WriteLine("Medeltemperatur Inne: {0:F2}", group.Average(d => d.Temperature));
            }
        }
        public static void SorteringFuktighetInne()
        {  
            Regex regex = new Regex(pattern); 
            string[] lines = System.IO.File.ReadAllLines(filename);
            var LuftfuktighetData = new List<double>();
            foreach (string line in lines)
            {
                Match match = regex.Match(line);
                string date = match.Groups[1].Value;
                string location = match.Groups[3].Value;
                string humidity = match.Groups[5].Value;
                double Medelluftfuktighet;
                if (double.TryParse(humidity, out Medelluftfuktighet) && location == "Inne")
                {
                    LuftfuktighetData.Add(Medelluftfuktighet);
                }
            }

            var dataByDate = lines
                .Select(line =>
                {
                    Match match = regex.Match(line);
                    string date = match.Groups[1].Value;
                    string humidity = match.Groups[5].Value;
                    double Medelluftfuktighet;
                    if (double.TryParse(humidity, out Medelluftfuktighet) && match.Groups[3].Value == "Inne")
                    {
                        return new { Date = date, Humidity = Medelluftfuktighet };
                    }
                    return null;
                })
               .Where(x => x != null)
                .GroupBy(x => x.Date)
                .OrderByDescending(group => group.Average(averageHumidity => averageHumidity.Humidity)) //s
                .ToList();

            foreach (var group in dataByDate)
            {
                Console.WriteLine("Datum: {0}", group.Key);
                Console.WriteLine("medelluftfuktighet Inne: {0:F2}", group.Average(d => d.Humidity));

            }
        }
        public static void MögelRiskInne()
        {
            Regex regex = new Regex(pattern);
            string[] lines = System.IO.File.ReadAllLines(filename);
            var temperatureData = new List<double>();
            var LuftfuktighetData = new List<double>();
            foreach (string line in lines)
            {
                Match match = regex.Match(line);
                string date = match.Groups[1].Value;
                string time = match.Groups[2].Value;
                string location = match.Groups[3].Value;
                string temperature = match.Groups[4].Value.Replace(".", ",");
                string humidity = match.Groups[5].Value;
                double medeltemp;
                double medelluftfuktighet;
                if (double.TryParse(temperature, out medeltemp) && double.TryParse(humidity, out medelluftfuktighet) && location == "Inne")
                {
                    temperatureData.Add(medeltemp);
                    LuftfuktighetData.Add(medelluftfuktighet);
                }
            }
            var dataByDate = lines
                .Select(line =>
                {
                    Match match = regex.Match(line);
                    string date = match.Groups[1].Value;
                    string temperature = match.Groups[4].Value.Replace(".", ",");
                    string humidity = match.Groups[5].Value;
                    double medeltemp;
                    double medelluftfuktighet;
                    if (double.TryParse(temperature, out medeltemp) && double.TryParse(humidity, out medelluftfuktighet) && match.Groups[3].Value == "Inne")
                    {
                        return new { Date = date, Temperature = medeltemp, Humidity = medelluftfuktighet };
                    }
                    return null;
                })
               .Where(x => x != null)
                .GroupBy(x => x.Date)
                .OrderBy(group => group.Average(averageHumidity => averageHumidity.Humidity))
                .ToList();
            foreach (var group in dataByDate)
            {
                double avgTemperature = group.Average(d => d.Temperature);
                double avgHumidity = group.Average(d => d.Humidity);
                if (avgHumidity >= 30 && avgTemperature >= 10)
                {
                    Console.WriteLine("Högst risk för mögel i omvändordning");
                    Console.WriteLine("Datum: {0}", group.Key);
                    Console.WriteLine("Medel Luftfuktighet, Inne: {0:F2}", group.Average(d => d.Humidity));
                    Console.WriteLine("Medel Temperatur, Inne: {0:F2}", group.Average(d => d.Temperature));

                }
            }
        }
    }
}
