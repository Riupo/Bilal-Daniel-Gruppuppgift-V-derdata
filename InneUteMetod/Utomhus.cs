using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace GruppUppgift_Väderdata.InneUteMetod
{
    public static class Utomhus
    {
        static string pattern = @"(\d{4}-\d{2}-\d{2})\s(\d{2}:\d{2}:\d{2}),(\w+),(-?\d+\.\d+),(\d+)";
        static string filename = @"C:\Users\Bilal\OneDrive\Documents\Visual Studio 2022\Demos\GruppUppgift Väderdata\Bilal-Daniel-Gruppuppgift-V-derdata\Textfiler\tempdata5-med fel.txt";
        public static void ViewBox(this string input)
        {
            Console.WriteLine(new string('-', input.Length + 4));
            Console.WriteLine("| " + input + " |");
            Console.WriteLine(new string('-', input.Length + 4));
        }

        public static void Sökmöjlighet()
        {
            Regex regex = new Regex(pattern);
            string[] lines = File.ReadAllLines(filename);
            Console.WriteLine("Ange datumet du vill kolla medeltemperaturen och medelluftfuktighet på");
            string datum = Console.ReadLine();
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
                double Medeltemp;
                double Medelluftfuktighet;
                if (double.TryParse(temperature, out Medeltemp) && double.TryParse(humidity, out Medelluftfuktighet) && location == "Ute" && date == datum)
                {
                    temperatureData.Add(Medeltemp);
                    LuftfuktighetData.Add(Medelluftfuktighet);
                    Console.WriteLine("Datum: {0}, Tid: {1}, Plats: {2}, Temperatur: {3}, Luftfuktighet: {4}",
                                          date, time, location, Medeltemp, humidity);
                }
            }
            double averageTemperature = temperatureData.Average();
            double averageHumidity = LuftfuktighetData.Average();
            string Datum = "Datum: " + datum;
            Datum.ViewBox();
            Console.WriteLine("MedelTemperatur: {0:F2}, MedelLuftfuktighet: {1:F2}",
                                  averageHumidity, averageTemperature);
        }

        public static void SorteringMedeltemperatur()
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
                if (double.TryParse(temperature, out Medeltemp) && location == "Ute")
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
                    if (double.TryParse(temperature, out Medeltemp) && match.Groups[3].Value == "Ute")
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
                Console.WriteLine("Medeltemperatur: {0:F2}", group.Average(d => d.Temperature));
            }
        }


        public static void SorteringFuktighet()
        {
            Regex regex = new Regex(pattern);
            string[] lines = File.ReadAllLines(filename);
            var LuftfuktighetData = new List<double>();
            foreach (string line in lines)
            {
                Match match = regex.Match(line);
                string date = match.Groups[1].Value;
                string location = match.Groups[3].Value;
                string humidity = match.Groups[5].Value;
                double Medelluftfuktighet;
                if (double.TryParse(humidity, out Medelluftfuktighet) && location == "Ute")
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
                    if (double.TryParse(humidity, out Medelluftfuktighet) && match.Groups[3].Value == "Ute")
                    {
                        return new { Date = date, Humidity = Medelluftfuktighet };
                    }
                    return null;
                })
               .Where(x => x != null)
                .GroupBy(x => x.Date)
                .OrderByDescending(group => group.Average(averageHumidity => averageHumidity.Humidity))
                .ToList();

            foreach (var group in dataByDate)
            {
                Console.WriteLine("Datum: {0}", group.Key);
                Console.WriteLine("medelluftfuktighet: {0:F2}", group.Average(d => d.Humidity));

            }
        }
        public static void MögelRisk(Program.MögelDelegat md)
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
                if (double.TryParse(temperature, out medeltemp) && double.TryParse(humidity, out medelluftfuktighet) && location == "Ute")
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
                    if (double.TryParse(temperature, out medeltemp) && double.TryParse(humidity, out medelluftfuktighet) && match.Groups[3].Value == "Ute")
                    {
                        return new { Date = date, Temperature = medeltemp, Humidity = medelluftfuktighet };
                    }
                    return null;
                })
               .Where(x => x != null)
                .GroupBy(x => x.Date)
                .OrderBy(group => md(group.Average(averageHumidity => averageHumidity.Humidity), group.Average(averageTemperature => averageTemperature.Temperature)))
                .ToList();
            Console.WriteLine("Högst risk för mögel i omvändordning");
            foreach (var group in dataByDate)
            {
                double avgTemperature = group.Average(d => d.Temperature);
                double avgHumidity = group.Average(d => d.Humidity);
                double mögelIndex = md(avgHumidity, avgTemperature);
                if (avgHumidity >= 70 && avgTemperature >= 10)
                {
                    Console.WriteLine("Datum: {0}", group.Key);
                    Console.WriteLine("Medel Luftfuktighet: {0:F2}", group.Average(d => d.Humidity));
                    Console.WriteLine("Medel Temperatur: {0:F2}", group.Average(d => d.Temperature));
                    Console.WriteLine("Index: {0:F}", mögelIndex);
                    Console.WriteLine("--------------------------------------------------------");
                }
            }
        }
        public static void metelogiskHöst()
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
                double medeltemp;
                double medelluftfuktighet;
                if (double.TryParse(temperature, out medeltemp) && location == "Ute")
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
                            //  .OrderBy(x => DateTime.ParseExact(x.Key, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                            .ToList();
            int count = 0;
            DateTime firstDate = DateTime.MinValue;
            foreach (var group in dataByDate)
            {
                DateTime date = DateTime.ParseExact(group.Key, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                double avgTemperature = group.Average(d => d.Temperature);

                if (avgTemperature <= 10 && date.Month >= 09 && date.Month <= 11)
                {
                    count++;
                    if (count == 1)
                    {
                        firstDate = date;
                    }
                    if (count >= 5)
                    {
                        Console.WriteLine("Första datumet: {0}", firstDate.ToString("yyyy-MM-dd"));
                        Console.WriteLine("Sista datumet: {0}", date.ToString("yyyy-MM-dd"));
                        Console.WriteLine("Medel Temperatur: {0:F2}", group.Average(d => d.Temperature));
                        Console.WriteLine();
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
        public static void metelogiskVinter()
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
                double medeltemp;
                double medelluftfuktighet;
                if (double.TryParse(temperature, out medeltemp) && location == "Ute")
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
                            .OrderBy(x => DateTime.ParseExact(x.Key, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                            .ToList();
            int count = 0;
            DateTime firstDate = DateTime.MinValue;
            foreach (var group in dataByDate)
            {
                DateTime date = DateTime.ParseExact(group.Key, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                double avgTemperature = group.Average(d => d.Temperature);

                if (avgTemperature <= 1 && date.Month >= 12 && date.Month <= 02)
                {
                    count++;
                    if (count == 1)
                    {
                        firstDate = date;
                    }
                    if (count >= 5)
                    {
                        Console.WriteLine("Första datumet: {0}", firstDate.ToString("yyyy-MM-dd"));
                        Console.WriteLine("Sista datumet: {0}", date.ToString("yyyy-MM-dd"));
                        Console.WriteLine("Medel Temperatur: {0:F2}", group.Average(d => d.Temperature));
                        Console.WriteLine();
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

