namespace GruppUppgift_Väderdata.TextfilMetod
{
    public class Textfilerläser
    {
        public static void ReadAllMedeltemperatur()
        {
            using (StreamReader reader = new StreamReader("../../../Textfiler/" + "Medeltemperatur.txt"))
            {
                string fileContent = reader.ReadToEnd();
                Console.WriteLine(fileContent);
            }
        }
        public static void ReadAllLuftfuktighet()
        {
            using (StreamReader reader = new StreamReader("../../../Textfiler/" + "MedelLuftfuktighet.txt"))
            {
                string fileContent = reader.ReadToEnd();
                Console.WriteLine(fileContent);
            }
        }
        public static void ReadAllAlgoritmMögel()
        {
            using (StreamReader reader = new StreamReader("../../../Textfiler/" + "Mögelrisk.txt"))
            {
                string fileContent = reader.ReadToEnd();
                Console.WriteLine(fileContent);
            }
        }
        public static void ReadAllMeterolgisk()
        {
            using (StreamReader reader = new StreamReader("../../../Textfiler/" + "Meterolgisk.txt"))
            {
                string fileContent = reader.ReadToEnd();
                Console.WriteLine(fileContent);
            }
        }
    }
}
