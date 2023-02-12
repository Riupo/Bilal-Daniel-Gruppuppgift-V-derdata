using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppUppgift_Väderdata
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
    }
}
