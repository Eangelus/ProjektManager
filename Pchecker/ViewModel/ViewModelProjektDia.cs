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

namespace ProjektManager.ViewModel
{
    public partial class ViewModelProjektDia : ObservableObject
    {

        public List<ISeries> Series { get; set; }


        private ViewModelStundenbuchung _viewModelStundenbuchung = new ViewModelStundenbuchung();

        public ViewModelStundenbuchung ViewModelStB
        {
            get
            {

                return _viewModelStundenbuchung;
            }
            set
            {


                _viewModelStundenbuchung = value;
                OnPropertyChanged(nameof(ViewModelStB));

            }
        }


        private List<LiveChartsCore.SkiaSharpView.Axis> _AxisX;

        public List<LiveChartsCore.SkiaSharpView.Axis> AxisX
        {
            get
            {
                return _AxisX;
            }
            set
            {
                _AxisX = value;
                OnPropertyChanged(nameof(AxisX));
            }
        }




        public ViewModelProjektDia()
        {
            this._AxisX = new List<Axis>();


            var StundenDerBuchung = new List<float>();
            var tage = new List<float>();
            


            var AxisL = new Axis();
            AxisL.Labels = new List<string>();
            List<Stundenbuchung> SortList = ViewModelStB.FilteredStundenbuchungen.ToList();

            SortList = SortList.OrderBy(s => s.StartTime).ToList();

            foreach (Stundenbuchung stundenb in SortList)
            {
                var minunten = stundenb.Stunden * 60;
                minunten = minunten + stundenb.Minuten;
                StundenDerBuchung.Add(minunten);

                int day = stundenb.StartTime.Day;
                int m = stundenb.StartTime.Month;


                var dateInString = day + "," + m;
                AxisL.Labels.Add(dateInString);
            }




            this.AxisX.Add(AxisL);

            var values1 = StundenDerBuchung;
            var values2 = tage;

            var fx = EasingFunctions.BounceInOut; // this is the function we are going to plot
            var x = 0f;

            //while (x <= 1)
            //{
            //    values1.Add(fx(x));
            //    values2.Add(fx(x - 0.15f));
            //    x += 0.025f;
            //}

            var columnSeries1 = new ColumnSeries<float>
            {
                Values = values1,
                Stroke = null,
                Padding = 2
            };

            var columnSeries2 = new ColumnSeries<float>
            {
                Values = values2,
                Stroke = null,
                Padding = 2
            };

            columnSeries1.PointMeasured += OnPointMeasured;
            columnSeries2.PointMeasured += OnPointMeasured;

            Series = new List<ISeries> { columnSeries1, columnSeries2 };
            

            
        }

        private void OnPointMeasured(ChartPoint<float, RoundedRectangleGeometry, LabelGeometry> point)
        {
            var perPointDelay = 100; // milliseconds
            var delay = point.Context.Entity.MetaData!.EntityIndex * perPointDelay;
            var speed = (float)point.Context.Chart.AnimationsSpeed.TotalMilliseconds + delay;

            point.Visual?.SetTransition(
                new Animation(progress =>
                {
                    var d = delay / speed;

                    return progress <= d
                        ? 0
                        : EasingFunctions.BuildCustomElasticOut(1.5f, 0.60f)((progress - d) / (1 - d));
                },
                TimeSpan.FromMilliseconds(speed)));
        }
    }
}
