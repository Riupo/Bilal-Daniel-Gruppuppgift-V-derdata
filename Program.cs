namespace GruppUppgift_Väderdata
{
    public class Program
    {
        public delegate double MögelDelegat(double temp, double hum);

        static void Main(string[] args)
        {
            // Utomhus.metelogiskVinter();
            // Utomhus.SorteringMedeltemperatur();
            //Utomhus.Sökmöjlighet();
            MögelDelegat md = CalculateMögel;
            Utomhus.MögelRisk(md);
        }
        public static double CalculateMögel(double temperature, double humidity)
        {
            return temperature + humidity;
        }
    }
}