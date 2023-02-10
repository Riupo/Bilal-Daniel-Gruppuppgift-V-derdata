using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GruppUppgift_Väderdata.Program;

namespace GruppUppgift_Väderdata
{
    public class EnumMetod
    {
        enum MenuList
        {
            utomhus = 1,
            inomhus,

            Quit = 9
        }
        enum MenuListUtomhus
        {
            sökverktyg = 1,
            Medeltemperatur,
            Medeluftuktighet,
            Meterolisk_höst,
            mögelrisk,
            Meterlogisk_vinter,

            Quit = 9
        }
        public static void Show()
        {
            bool loop = true;
            while (loop)
            {


                foreach (int i in Enum.GetValues(typeof(MenuList)))
                {
                    Console.WriteLine($"{i}. {Enum.GetName(typeof(MenuList), i).Replace('_', ' ')}"); // Tecknen med mellanslag för så att vi får mellalsag när vi skriver ut.
                }

                int nr;
                MenuList menu = (MenuList)99; //Default
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                {
                    menu = (MenuList)nr;
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Fel inmatining");
                }

                switch (menu)
                {
                    case MenuList.utomhus:
                        ShowUtomhus();
                        break;

                    case MenuList.inomhus:
                        Console.WriteLine(DateTime.Now);
                        break;

                    case MenuList.Quit:
                        loop = false;
                        break;
                }
            }
        }
        public static void ShowUtomhus()
        {
            bool loop = true;
            while (loop)
            {


                foreach (int i in Enum.GetValues(typeof(MenuListUtomhus)))
                {
                    Console.WriteLine($"{i}. {Enum.GetName(typeof(MenuListUtomhus), i).Replace('_', ' ')}"); // Tecknen med mellanslag för så att vi får mellalsag när vi skriver ut.
                }

                int nr;
                MenuListUtomhus menu = (MenuListUtomhus)99; //Default
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                {
                    menu = (MenuListUtomhus)nr;
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Fel inmatining");
                }

                switch (menu)
                {
                    case MenuListUtomhus.sökverktyg:
                        Utomhus.Sökmöjlighet();
                        break;

                    case MenuListUtomhus.Medeltemperatur:
                        Utomhus.SorteringMedeltemperatur();
                        break;

                    case MenuListUtomhus.Medeluftuktighet:
                        Utomhus.SorteringFuktighet();
                        break;

                    case MenuListUtomhus.Meterlogisk_vinter:
                        Utomhus.metelogiskVinter();
                        break;

                    case MenuListUtomhus.Meterolisk_höst:
                        Utomhus.metelogiskHöst();
                        break;

                    case MenuListUtomhus.mögelrisk:
                        MögelDelegat md = CalculateMögel;
                        Utomhus.MögelRisk(md);
                        break;

                    case MenuListUtomhus.Quit:
                        loop = false;
                        break;
                }
            }
        }
    }
}
        
    

