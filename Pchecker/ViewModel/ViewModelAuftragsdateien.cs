using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjektManager.Models;
using ProjektManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.ViewModel
{
    public class ViewModelAuftragsdateien : ViewModelBase
    {

        private readonly string _Netzlaufwerksverzeichniss = @"X:";

        private ObservableCollection<Projekt> projekte = DatabankService.LoadAllProjekte();

        public ObservableCollection<Projekt> Projekte
        {
            get { return projekte; }
            set { projekte = value; OnPropertyChanged(nameof(Projekte)); }
        }
        private ObservableCollection<Problem> probleme;

        public ObservableCollection<Problem> Probleme
        {
            get { return probleme; }
            set { probleme = value; OnPropertyChanged(nameof(Probleme)); }
        }

        private void LoadPlacesFromProjekts(string ProjektNummer)
        {
            string ausschnitt;
            Projekt projekt = new Projekt();
            foreach (Projekt p in Projekte)
            {
                if (p.ProjektNr == ProjektNummer)
                {
                    projekt = p;
                    projekt.Probleme.Concat(p.Probleme);
                }
            }

            foreach (var problem in projekt.Probleme)
            {
                int startIndex = problem.Bewertung.IndexOf("X:");
                int StartIndexOfIp = problem.Bewertung.IndexOf("192.168.23.5");
                string firstCut;
                string firstCutOnIp;

                if (startIndex != -1)
                {

                        firstCut = problem.Bewertung.Substring(startIndex);
                        int index = firstCut.IndexOf("\n");
                        firstCut = problem.Bewertung.Substring(0, index );
                        
                        //if (index != -1)
                        //{
                        //    firstCut = firstCut.Substring(startIndex, index + problem.Bewertung.Length - startIndex);
                        //}
                        
                        this.ListOfFounds.Add(firstCut);
             

                }

                if (StartIndexOfIp != -1)
                {
 
                        firstCutOnIp = problem.Bewertung.Substring(StartIndexOfIp);
                        int index = firstCutOnIp.IndexOf("\n");
                    firstCutOnIp = problem.Bewertung.Substring(0, index );

                    this.ListOfFounds.Add(firstCutOnIp);
           

                }




            }
        }





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




        private string selecedProjektNr { get; set; }

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
            ListOfFounds = new List<string>();
            LoadPlacesFromProjekts(ProjektNr);


        }

    }
}
