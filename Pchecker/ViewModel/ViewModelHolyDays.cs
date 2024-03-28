using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using ProjektManager.Models;
using ProjektManager.View;
using System.Windows;
using ProjektManager.ControlElements;
using System.Windows.Controls;
using Axis = LiveChartsCore.SkiaSharpView.Axis;
using DocumentFormat.OpenXml.Bibliography;
using System.Collections.ObjectModel;
using System.Globalization;
using ProjektManager.Services;
using DocumentFormat.OpenXml.Wordprocessing;

namespace ProjektManager.ViewModel
{
    public class ViewModelHolyDays : ViewModelBase
    {

        CultureInfo kultur = CultureInfo.GetCultureInfo("de-DE");

        public ViewModelHolyDays()
        {
            Mitarbeiterliste.Clear();
            var mitarbeiter = DatabankService.loadAllMitarbeiter();
            foreach (var m in mitarbeiter)
            {
                this.RestUrlaubsTage = RestTageUrlaub(m.AnzDerUrlaubsTage, m.UrlaubsTage);
                Mitarbeiterliste.Add(m);

            }
        }

        private DateTime _SelectedStartHolyDay;

        public DateTime SelectedStartHolyDay
        {
            get
            {
                return _SelectedStartHolyDay;
            }
            set
            {
                _SelectedStartHolyDay = value;
                OnPropertyChanged(nameof(SelectedStartHolyDay));
                berechnenederRestUrlaubBeiAuswahlAufCalender();
                BuchenUrlaub(this.SelectedMitarbeiter, SelectedStartHolyDay, SelectedEndHolyDay);
                OnPropertyChanged(nameof(SelectedEndHolyDay));
                OnPropertyChanged(nameof(RestUrlaubsTage));
            }
        }


        private int _RestUrlaubsTage;

        public int RestUrlaubsTage
        {
            get
            {
                return _RestUrlaubsTage;
            }
            set
            {

                _RestUrlaubsTage = value;

                OnPropertyChanged(nameof(RestUrlaubsTage));
            }
        }



        private DateTime _SelectedEndHolyDay;

        public DateTime SelectedEndHolyDay
        {
            get
            {
                return _SelectedEndHolyDay;
            }
            set
            {
                _SelectedEndHolyDay = value;
                OnPropertyChanged(nameof(SelectedEndHolyDay));
                BuchenUrlaub(this.SelectedMitarbeiter, SelectedStartHolyDay, SelectedEndHolyDay);
                berechnenederRestUrlaubBeiAuswahlAufCalender();
                OnPropertyChanged(nameof(SelectedStartHolyDay));
                OnPropertyChanged(nameof(RestUrlaubsTage));
            }
        }

        public DateTime Heute = DateTime.Now;


        public static ObservableCollection<Mitarbeiter> Mitarbeiterliste = new ObservableCollection<Mitarbeiter>();


        private Mitarbeiter _SelectedMitarbeiter;

        public Mitarbeiter SelectedMitarbeiter
        {
            get
            {
                return _SelectedMitarbeiter;
            }
            set
            {
                _SelectedMitarbeiter = value;
                
                RestUrlaubsTage = SelectedMitarbeiter.AnzDerUrlaubsTage;
                OnPropertyChanged(nameof(SelectedMitarbeiter));
            }
        }

        public int RestTageUrlaub(int UrlaubsTage, ObservableCollection<DateTime> DaysTaken)
        {

            foreach (DateTime day in DaysTaken)
            {
                UrlaubsTage = UrlaubsTage - 1;

            }
            return UrlaubsTage;
        }

        public Mitarbeiter BuchenUrlaub(Mitarbeiter SelectedMitarbeiter, DateTime start, DateTime end)
        {
            if (SelectedMitarbeiter != null)
            {

                if (start != null || end != null)
                {
                    int wholeDays = (end.Date - start.Date).Days;

                    while (start < end)
                    {
                        SelectedMitarbeiter.UrlaubsTage.Add(start);
                        start = start.AddDays(1);
                        SelectedMitarbeiter.UrlaubsTage.Add(start);
                    }

                    SelectedMitarbeiter.AnzDerUrlaubsTage = SelectedMitarbeiter.AnzDerUrlaubsTage - wholeDays;
                    return SelectedMitarbeiter;

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }


        }

        public void berechnenederRestUrlaubBeiAuswahlAufCalender()
        {
            if (this.SelectedMitarbeiter != null)
            {
                if (this.SelectedStartHolyDay != null || this.SelectedEndHolyDay != null)
                {
                    DateTime count = SelectedStartHolyDay;
                    ObservableCollection<DateTime> list = new ObservableCollection<DateTime>();
                    list.Add(SelectedStartHolyDay);

                    while (count <= SelectedEndHolyDay)
                    {

                        list.Add(count);
                        count = count.AddDays(1);
                    }

                    this.RestUrlaubsTage = RestTageUrlaub(SelectedMitarbeiter.AnzDerUrlaubsTage, list);
                }
            }
            else
            {

            }
        }

    }
}
