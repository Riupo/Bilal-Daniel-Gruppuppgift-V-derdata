using GruppUppgift_Väderdata.InneUteMetod;
using GruppUppgift_Väderdata.TextfilMetod;
using static GruppUppgift_Väderdata.Program;

namespace GruppUppgift_Väderdata
{
    public class EnumMetod
    {
        enum MenuList
        {
            utomhus = 1,
            inomhus,
            texfiler,

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
            Tillbaka = 8,

            Quit = 9
        }
        enum MenuListInomhus
        {
            sökverktyg = 1,
            Medeltemperatur,
            Medeluftuktighet,
            mögelrisk,
            Tillbaka = 8,

            Quit = 9
        }
        enum MenuListTextFiler
        {
            Medeltemperatur_Inne_Ute_Månad = 1,
            Medelluftfuktighet_Inne_Ute_Månad,
            Mögelrisk_Inne_Ute_Månad,
            Datum_höst_vinter_2016,

            tillbaka = 8,

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
                        Console.Clear();
                        ShowUtomhus();
                        break;

                    case MenuList.inomhus:
                        Console.Clear();
                        ShowInomhus();
                        break;
                    case MenuList.texfiler:
                        Console.Clear();
                        ShowTexfiler();
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
                        Console.Clear();
                        Utomhus.Sökmöjlighet();
                        break;

                    case MenuListUtomhus.Medeltemperatur:
                        Console.Clear();
                        Utomhus.SorteringMedeltemperatur();
                        break;

                    case MenuListUtomhus.Medeluftuktighet:
                        Console.Clear();
                        Utomhus.SorteringFuktighet();
                        break;

                    case MenuListUtomhus.Meterlogisk_vinter:
                        Console.Clear();
                        Utomhus.metelogiskVinter();
                        break;

                    case MenuListUtomhus.Meterolisk_höst:
                        Console.Clear();
                        Utomhus.metelogiskHöst();
                        break;

                    case MenuListUtomhus.mögelrisk:
                        Console.Clear();
                        MögelDelegat md = CalculateMögel;
                        Utomhus.MögelRisk(md);
                        break;

                    case MenuListUtomhus.Quit:
                        Console.Clear();
                        loop = false;
                        break;
                    case MenuListUtomhus.Tillbaka:
                        Console.Clear();
                        EnumMetod.Show();
                        break;
                }
            }
        }
        public static void ShowInomhus()
        {
            bool loop = true;
            while (loop)
            {


                foreach (int i in Enum.GetValues(typeof(MenuListInomhus)))
                {
                    Console.WriteLine($"{i}. {Enum.GetName(typeof(MenuListInomhus), i).Replace('_', ' ')}"); // Tecknen med mellanslag för så att vi får mellalsag när vi skriver ut.
                }

                int nr;
                MenuListInomhus menu = (MenuListInomhus)99; //Default
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                {
                    menu = (MenuListInomhus)nr;
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Fel inmatining");
                }

                switch (menu)
                {
                    case MenuListInomhus.sökverktyg:
                        Console.Clear();
                        Utomhus.Sökmöjlighet();
                        break;

                    case MenuListInomhus.Medeltemperatur:
                        Console.Clear();
                        Utomhus.SorteringMedeltemperatur();
                        break;

                    case MenuListInomhus.Medeluftuktighet:
                        Console.Clear();
                        Utomhus.SorteringFuktighet();
                        break;

                    case MenuListInomhus.mögelrisk:
                        Console.Clear();
                        MögelDelegat md = CalculateMögel;
                        Utomhus.MögelRisk(md);
                        break;

                    case MenuListInomhus.Quit:
                        Console.Clear();
                        loop = false;
                        break;
                    case MenuListInomhus.Tillbaka:
                        Console.Clear();
                        EnumMetod.Show();
                        break;
                }
            }
        }
        public static void ShowTexfiler()
        {
            bool loop = true;
            while (loop)
            {


                foreach (int i in Enum.GetValues(typeof(MenuListTextFiler)))
                {
                    Console.WriteLine($"{i}. {Enum.GetName(typeof(MenuListTextFiler), i).Replace('_', ' ')}"); // Tecknen med mellanslag för så att vi får mellalsag när vi skriver ut.
                }

                int nr;
                MenuListTextFiler menu = (MenuListTextFiler)99; //Default
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                {
                    menu = (MenuListTextFiler)nr;
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Fel inmatining");
                }

                switch (menu)
                {
                    case MenuListTextFiler.Medeltemperatur_Inne_Ute_Månad:

                        Console.Clear();
                        Textfilerläser.ReadAllMedeltemperatur();
                        break;

                    case MenuListTextFiler.Medelluftfuktighet_Inne_Ute_Månad:
                        Console.Clear();
                        Textfilerläser.ReadAllLuftfuktighet();
                        break;

                    case MenuListTextFiler.Mögelrisk_Inne_Ute_Månad:
                        Console.Clear();
                        Textfilerläser.ReadAllAlgoritmMögel();
                        break;

                    case MenuListTextFiler.Datum_höst_vinter_2016:
                        Console.Clear();
                        Textfilerläser.ReadAllMeterolgisk();
                        break;

                    case MenuListTextFiler.tillbaka:
                        Console.Clear();
                        EnumMetod.Show();
                        break;
                }
            }
        }
    }
}



