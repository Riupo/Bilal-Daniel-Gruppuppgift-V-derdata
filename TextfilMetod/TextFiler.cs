using System.Globalization;
using System.Text.RegularExpressions;

namespace GruppUppgift_Väderdata.TextfilMetod
{
    internal class TextFiler
    {
        static string path = "../../../Textfiler/";
        static string pattern = @"(\d{4}-\d{2}-\d{2})\s(\d{2}:\d{2}:\d{2}),(\w+),(-?\d+\.\d+),(\d+)";
        static string filename = @"C:\Users\danie\source\repos\Väderdata\Väderdata\Bilal-Daniel-Gruppuppgift-V-derdata\Textfiler\tempdata5-med fel.txt";
        public static void SorteringMedeltemperatur()
        {
            using (StreamWriter streamWriter = new StreamWriter(path + "Medeltemperatur.txt", true))
            {
                Regex regex = new Regex(pattern);
                string[] lines = File.ReadAllLines(filename);
                var temperatureData = new List<double>();
                foreach (string line in lines)
                {
                    Match match = regex.Match(line);
                    string date = match.Groups[1].Value;
                    string location = match.Groups[3].Value;
                    string temperature = match.Groups[4].Value.Replace(".", ",");
                    double Medeltemp;
                    if (double.TryParse(temperature, out Medeltemp))
                    {
                        temperatureData.Add(Medeltemp);
                    }
                }
                var dataByMonth = lines
                    .Select(line =>
                    {
                        Match match = regex.Match(line);
                        string date = match.Groups[1].Value;
                        string location = match.Groups[3].Value;
                        string temperature = match.Groups[4].Value.Replace(".", ",");
                        double Medeltemp;
                        if (double.TryParse(temperature, out Medeltemp))
                        {
                            return new { Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Parse(date).Month), Location = location, Temperature = Medeltemp };
                        }
                        return null;
                    })
                    .Where(x => x != null)
                    .GroupBy(x => new { x.Month, x.Location })
                    .ToList();

                foreach (var group in dataByMonth)
                {
                    streamWriter.WriteLine("Månad: {0}", group.Key.Month);
                    streamWriter.WriteLine("Plats: {0}", group.Key.Location);
                    streamWriter.WriteLine("Medeltemperatur: {0:F2}", group.Average(d => d.Temperature));
                    streamWriter.WriteLine("-----------------------------------------------------------");
                }
            }
        }
        public static void SorteringMedelLuftfuktighet()
        {
            using (StreamWriter streamWriter = new StreamWriter(path + "MedelLuftfuktighet.txt", true))
            {
                Regex regex = new Regex(pattern);
                string[] lines = File.ReadAllLines(filename);
                var Fuktighetdata = new List<double>();
                foreach (string line in lines)
                {
                    Match match = regex.Match(line);
                    string date = match.Groups[1].Value;
                    string location = match.Groups[3].Value;
                    string humidity = match.Groups[5].Value.Replace(".", ",");
                    double Medelfukt;
                    if (double.TryParse(humidity, out Medelfukt))
                    {
                        Fuktighetdata.Add(Medelfukt);
                    }
                }
                var dataByMonth = lines
                    .Select(line =>
                    {
                        Match match = regex.Match(line);
                        string date = match.Groups[1].Value;
                        string location = match.Groups[3].Value;
                        string humidity = match.Groups[5].Value.Replace(".", ",");
                        double medelfukt;
                        if (double.TryParse(humidity, out medelfukt))
                        {
                            return new { Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Parse(date).Month), Location = location, humidity = medelfukt };
                        }
                        return null;
                    })
                    .Where(x => x != null)
                    .GroupBy(x => new { x.Month, x.Location })
                    .ToList();

                foreach (var group in dataByMonth)
                {
                    streamWriter.WriteLine("Månad: {0}", group.Key.Month);
                    streamWriter.WriteLine("Plats: {0}", group.Key.Location);
                    streamWriter.WriteLine("Luftfuktighet: {0:F2}", group.Average(d => d.humidity));
                    streamWriter.WriteLine("-----------------------------------------------------------");
                }
            }
        }
        public static void SorteringMögelrisk(Program.MögelDelegat md)
        {
            using (StreamWriter streamWriter = new StreamWriter(path + "Mögelrisk.txt", true))
            {
                Regex regex = new Regex(pattern);
                string[] lines = File.ReadAllLines(filename);
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
                    if (double.TryParse(temperature, out medeltemp) && double.TryParse(humidity, out medelluftfuktighet))
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
                        string location = match.Groups[3].Value;
                        double medeltemp;
                        double medelluftfuktighet;
                        if (double.TryParse(temperature, out medeltemp) && double.TryParse(humidity, out medelluftfuktighet))
                        {
                            return new { Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Parse(date).Month), Location = location, Humidity = medelluftfuktighet, Temperature = medeltemp };
                        }
                        return null;
                    })
                   .Where(x => x != null)
                    .GroupBy(x => new { x.Month, x.Location })
                    .OrderByDescending(group => md(group.Average(averageHumidity => averageHumidity.Humidity), group.Average(averageTemperature => averageTemperature.Temperature)))
                    .ToList();
                foreach (var group in dataByDate)
                {
                    double avgTemperature = group.Average(d => d.Temperature);
                    double avgHumidity = group.Average(d => d.Humidity);
                    double mögelIndex = md(avgHumidity, avgTemperature);
                    streamWriter.WriteLine("Månad: {0}", group.Key.Month);
                    streamWriter.WriteLine("Plats: {0}", group.Key.Location);
                    streamWriter.WriteLine("MedelLuftfuktighet: {0:F2}", group.Average(d => d.Humidity));
                    streamWriter.WriteLine("MedelTemperatur: {0:F2}", group.Average(d => d.Temperature));
                    streamWriter.WriteLine("Index: {0:F}", mögelIndex);
                    streamWriter.WriteLine("-----------------------------------------------------------");
                }
            }
        }
        public static void TextFilMeterologisk()
        {
            using (StreamWriter streamWriter = new StreamWriter(path + "Meterolgisk.txt", true))
            {

                Regex regex = new Regex(pattern);
                string[] lines = File.ReadAllLines(filename);
                var temperatureData = new List<double>();
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
                    if (double.TryParse(temperature, out medeltemp) && double.TryParse(humidity, out medelluftfuktighet) && location == "Ute")
                    {
                        temperatureData.Add(medeltemp);
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
                                    if (double.TryParse(temperature, out medeltemp) && double.TryParse(humidity, out medelluftfuktighet) && match.Groups[3].Value == "Ute")
                                    {
                                        return new { Date = date, Temperature = medeltemp, Humidity = medelluftfuktighet };
                                    }
                                    return null;
                                })
                                .Where(x => x != null)
                                .GroupBy(x => x.Date)
                                //     .OrderBy(x => DateTime.ParseExact(x.Key, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                                .ToList();
                int count = 0;
                DateTime firstDate = DateTime.MinValue;
                foreach (var group in dataByDate)
                {
                    DateTime date = DateTime.ParseExact(group.Key, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    double avgTemperature = group.Average(d => d.Temperature);

                    if (avgTemperature <= 10)
                    {
                        count++;
                        if (count == 1)
                        {
                            firstDate = date;
                        }
                        if (count >= 5)
                        {
                            //Console.WriteLine("Första datumet: {0}", firstDate.ToString("yyyy-MM-dd"));
                            //Console.WriteLine("Sista datumet: {0}", date.ToString("yyyy-MM-dd"));
                            //Console.WriteLine("Medel Temperatur: {0:F2}", group.Average(d => d.Temperature));
                            streamWriter.WriteLine("Första datumet: {0}", firstDate.ToString("yyyy-MM-dd"));
                            streamWriter.WriteLine("Sista datumet: {0}", date.ToString("yyyy-MM-dd"));
                            streamWriter.WriteLine("Medel Temperatur: {0:F2}", group.Average(d => d.Temperature));
                            streamWriter.WriteLine("-------------------------------------------------");
                            count = 0;
                            firstDate = DateTime.MinValue;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
            }
        }
    }
}
