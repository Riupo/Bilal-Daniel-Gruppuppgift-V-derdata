namespace GruppUppgift_Väderdata
{
    public class Program
    {
        public delegate double MögelDelegat(double temp, double hum);

        static void Main(string[] args)
        {
            //TextFiler.SorteringMedeltemperatur();
            //Textfilerläser.ReadAllMedeltemperatur();
            //  TextFiler.SorteringMedelLuftfuktighet();
            //MögelDelegat md = CalculateMögel;
            //TextFiler.SorteringMögelrisk(md);
            //TextFiler.TextFilMeterologisk();
            TextFiler.AlgoritmFörMögel();
        }
        public static double CalculateMögel(double temperature, double humidity)
        {
            return temperature + humidity;
        }
    }
}