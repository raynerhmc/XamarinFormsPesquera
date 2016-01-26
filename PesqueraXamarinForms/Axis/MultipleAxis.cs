#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PesqueraXamarinForms
{
    public class MultipleSeries : ContentPage
    {
        public MultipleSeries()
        {
            this.Content = GetChart();
        }

        private static SfChart GetChart()
        {
            SfChart chart = new SfChart() { };

            chart.PrimaryAxis = new CategoryAxis() { Title = new ChartAxisTitle() { Text = "Years", TextColor = Color.Black }, LabelPlacement = LabelPlacement.BetweenTicks };
            chart.SecondaryAxis = new NumericalAxis()
            {
                Title = new ChartAxisTitle() { Text = "Revenue", TextColor = Color.Black },
                LabelStyle = new ChartAxisLabelStyle() { LabelFormat = "$####" },
                Minimum = 6200,
                Maximum = 8800,
                Interval = 200,
                ShowMajorGridLines = false,
                //MajorTickStyle = new ChartAxisTickStyle() { TickSize = 0 },

                MajorGridLineStyle = new ChartLineStyle() { StrokeWidth = 0 }
            };

            ObservableCollection<ChartDataPoint> datas = new ObservableCollection<ChartDataPoint>();
            datas.Add(new ChartDataPoint("2010", 8000));
            datas.Add(new ChartDataPoint("2011", 8100));
            datas.Add(new ChartDataPoint("2012", 8250));
            datas.Add(new ChartDataPoint("2013", 8600));
            datas.Add(new ChartDataPoint("2014", 8700));

            ObservableCollection<ChartDataPoint> datas1 = new ObservableCollection<ChartDataPoint>();
            datas1.Add(new ChartDataPoint("2010", 6));
            datas1.Add(new ChartDataPoint("2011", 15));
            datas1.Add(new ChartDataPoint("2012", 35));
            datas1.Add(new ChartDataPoint("2013", 65));
            datas1.Add(new ChartDataPoint("2014", 75));

            chart.Series.Add(new ColumnSeries()
            {
                ItemsSource = datas
            });
            chart.Series.Add(new FastLineSeries()
            {
                Label = "Series1",
                ItemsSource = datas1,
                StrokeWidth = 7,
                YAxis = new NumericalAxis()
                {
                    OpposedPosition = true,
                    MajorGridLineStyle = new ChartLineStyle() { StrokeWidth = 0 },
                    Minimum = 0,
                    Maximum = 80,
                    Interval = 5,
                    ShowMajorGridLines = false,
                    Title = new ChartAxisTitle() { Text = "Number of Customers", TextColor = Color.Black },
                    //MajorTickStyle = new ChartAxisTickStyle() { TickSize = 0 }
                }
            });
            return chart;
        }
    }
}
