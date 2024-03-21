using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.ViewModel
{
    public class ViewModelAuftragsdateien : ViewModelBase
    {

        private readonly string _Netzlaufwerksverzeichniss = @"X:\";



        private List<string> _ListOfFounds;

        public List<string> ListOfFounds
        {
            get
            {
                return _ListOfFounds;
            }
            set
            {
                _ListOfFounds = value;
                OnPropertyChanged(nameof(ListOfFounds));
            }
        }


        private string selecedProjektNr {  get; set; }

        public static void DurchsuchenVerzeichnis(string verzeichnispfad, string ProjektNr, List<string> ListOfFounds)
        {
            string keinzugrif = "05 Kaufmaennisch";
            verzeichnispfad = verzeichnispfad.Replace(keinzugrif, "");
            try
            {
                foreach (var datei in Directory.EnumerateFiles(verzeichnispfad, "*" + ProjektNr + "*"))
                {
                    ListOfFounds.Add(datei);
                }

                //foreach (var unterverzeichnis in Directory.EnumerateDirectories(verzeichnispfad))
                //{
                //    DurchsuchenVerzeichnis(unterverzeichnis, ProjektNr, ListOfFounds);
                //}
            }
            catch (UnauthorizedAccessException)
            {
                // Fehler beim Zugriff auf ein Verzeichnis oder eine Datei, überspringen Sie es und fahren Sie fort
                Console.WriteLine($"Zugriff verweigert für Verzeichnis: {verzeichnispfad}");
            }
        }

        public ViewModelAuftragsdateien(string ProjektNr)
        {
            DurchsuchenVerzeichnis(_Netzlaufwerksverzeichniss, ProjektNr,this.ListOfFounds);




        }

    }
}
