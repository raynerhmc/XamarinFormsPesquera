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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PesqueraXamarinForms
{
    public class Category : ContentPage
    {
        public Category()
        {
            Content = GetChart();
        }

        private static SfChart GetChart()
        {
            SfChart chart = new SfChart();
            chart.PrimaryAxis = new CategoryAxis() { LabelPlacement = LabelPlacement.BetweenTicks };
            chart.SecondaryAxis = new NumericalAxis();
            
            chart.Series.Add(new ColumnSeries()
            {
             //   ItemsSource = MainPage.GetCategoryData()
            });
            return chart;
        }
    }
}