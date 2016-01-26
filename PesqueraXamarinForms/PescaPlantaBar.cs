using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
namespace PesqueraXamarinForms
{
	public class PescaPlantaBar : ContentPage
	{
		private string title_page_ = "AVANCE DE PESCA POR PLANTA";
		private string [] menu_labels_ = {"Año: ","Zona: ","Periodo: ", "Región: ", "Puerto: "};

		private Picker pmenu_pesquera_;
		public PescaPlantaBar ()
		{
			this.Content = GetChart();
		}

		async void ShowMyPage(){
			pmenu_pesquera_.SelectedIndex = 3;
			await Navigation.PushAsync( new MyPage() ) ;
		}

		async void ShowResumenTemporadaPie(){
			pmenu_pesquera_.SelectedIndex = 3;
			await Navigation.PushAsync( new ResumenTemporadaPie() ) ;
		}

		async void ShowPescaRegionColumn(){
			pmenu_pesquera_.SelectedIndex = 3;
			await Navigation.PushAsync( new PescaRegionColumn() ) ;
		}

		async void ShowPescaPuertoColumn(){
			pmenu_pesquera_.SelectedIndex = 3;
			await Navigation.PushAsync( new PescaPuertoColumn() ) ;
		}

		async void ShowPescaDiaColumnSpline(){
			pmenu_pesquera_.SelectedIndex = 3;
			await Navigation.PushAsync( new PescaDiaColumnSpline() ) ;
		}

		private  StackLayout GetChart()
		{

			SfChart chart = new SfChart();
			chart.PrimaryAxis = new CategoryAxis() { LabelPlacement = LabelPlacement.BetweenTicks };
			chart.SecondaryAxis = new NumericalAxis(){ Interval = 20 };

			ChartDataMarker dataMarker = new ChartDataMarker() { ShowLabel = true };
			dataMarker.LabelStyle.TextColor = Color.White;
			dataMarker.LabelStyle.Font = Font.SystemFontOfSize(25);
			dataMarker.LabelStyle.LabelPosition = DataMarkerLabelPosition.Inner;
			//dataMarker.LabelStyle.Angle = 90;

			BarSeries row_bars = new BarSeries () {
				ItemsSource = GetData1(),
				//DataMarkerPosition = Syncfusion.SfChart.XForms.DataMarkerPosition.Center,
			};

			row_bars.DataMarker = dataMarker;
			chart.Series.Add (row_bars);

			chart.VerticalOptions = LayoutOptions.FillAndExpand;
			chart.HorizontalOptions = LayoutOptions.FillAndExpand;


			////////////// Picker#
			/// 
			/// Picker Period
			Picker p_list_period = new Picker
			{
				Title = menu_labels_[2],
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			String [] periodNameList = {"periodo 1", "periodo 2"};

			foreach (string periodName in periodNameList)
			{
				p_list_period.Items.Add(periodName);
			} 
			p_list_period.SelectedIndex = 0;

			/// Picker year
			Picker p_list_year = new Picker
			{
				Title = menu_labels_[0],
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			String [] yearNameList = {"2010", "2011", "2012", "2013", "2014"};
			foreach (string yearName in yearNameList)
			{
				p_list_year.Items.Add(yearName);
			}
			p_list_year.SelectedIndex = 0;

			/// Picker zona
			Picker p_list_zone = new Picker
			{
				Title = menu_labels_[1],
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			String [] zoneNameList = {"Norte", "Sur", "Este", "Oeste"};
			foreach (string zoneName in zoneNameList)
			{
				p_list_zone.Items.Add(zoneName);
			}
			p_list_zone.SelectedIndex = 0;

			/// Picker region
			Picker p_list_region = new Picker
			{
				Title = menu_labels_[3],
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			String [] regionNameList = {"Callao", "Ica", "Ancash", "La Libertad", "Piura"};
			foreach (string regionName in regionNameList)
			{
				p_list_region.Items.Add(regionName);
			}
			p_list_region.SelectedIndex = 0;

			/// Picker puerto
			Picker p_list_puerto = new Picker
			{
				Title = menu_labels_[3],
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			String[] puertoNameList = { "Chimbote", "Samanco", "Coishco" };
			foreach (string puertoName in puertoNameList)
			{
				p_list_puerto.Items.Add(puertoName);
			}
			p_list_puerto.SelectedIndex = 0;

			//////////


			p_list_year.SelectedIndexChanged += (sender, args) =>
			{
				if (p_list_year.SelectedIndex == -1)
				{

					row_bars.ItemsSource = GetData1();
				}
				else
				{
					int num = p_list_year.SelectedIndex;
					if (num == 0) row_bars.ItemsSource = GetData1();
					else  row_bars.ItemsSource = GetData2();

				}
			};

			pmenu_pesquera_ = GetMenuPesquera ();
			pmenu_pesquera_.VerticalOptions = LayoutOptions.Start;


			StackLayout main_layout = new StackLayout (){
				Padding = GlobalParameters.MAIN_LAYOUT_PADDING_,
				Spacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Children = {
					pmenu_pesquera_,
					new Label(){
						Text = title_page_,
						HorizontalOptions = LayoutOptions.Center
					},
					new StackLayout(){
						Spacing = 0,
						VerticalOptions = LayoutOptions.Start,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Orientation = StackOrientation.Horizontal,
						Children = {
							new Label(){
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
								Text = menu_labels_[0]
							},
							p_list_year,

							new StackLayout{
								Spacing = 0,
								VerticalOptions = LayoutOptions.Start,
								HorizontalOptions = LayoutOptions.CenterAndExpand,
								Orientation = StackOrientation.Horizontal,
								Children = {
									new Label(){
										FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
										Text = menu_labels_[1]
									},
									p_list_zone
								}
							},
							new Label(){
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
								Text = menu_labels_[2]
							},
							p_list_period
						}
					},
					new StackLayout(){
						Spacing = 0,
						VerticalOptions = LayoutOptions.Start,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Orientation = StackOrientation.Horizontal,
						Children = {
							new Label(){
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
								Text = menu_labels_[3]
							},
							p_list_region,
							new Label(){
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
								Text = menu_labels_[4]
							},
							p_list_puerto
						}
					},
					chart
				}
			};
			return main_layout;
		}

		public static ObservableCollection<ChartDataPoint> GetData1()
		{
			ObservableCollection<ChartDataPoint> datas = new ObservableCollection<ChartDataPoint>();
			datas.Add(new ChartDataPoint("A", 45));
			datas.Add(new ChartDataPoint("B", 86));
			datas.Add(new ChartDataPoint("C", 23));
			datas.Add(new ChartDataPoint("D", 43));
			datas.Add(new ChartDataPoint("E", 54));
			return datas;
		}

		public static ObservableCollection<ChartDataPoint> GetData2()
		{
			ObservableCollection<ChartDataPoint> datas = new ObservableCollection<ChartDataPoint>();
			datas.Add(new ChartDataPoint("A", 54));
			datas.Add(new ChartDataPoint("B", 24));
			datas.Add(new ChartDataPoint("C", 53));
			datas.Add(new ChartDataPoint("D", 63));
			datas.Add(new ChartDataPoint("E", 35));
			return datas;
		}

		private  Picker GetMenuPesquera(){
			Picker p_list_menu = new Picker
			{
				Title = "Cuadros",
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			String [] menuNameList = {"Avance pesca por zona",
				"Avance pesca por región",
				"Avance pesca por puerto",
				"Avance pesca por planta",
				"Avance pesca / descargas por día",
				"Avance pesca / descargas quincena",
				"Avance por grupos",
				"Avance por grupos en [Rango %]",
			};
			foreach (string menuName in menuNameList)
			{
				p_list_menu.Items.Add(menuName);
			}
			p_list_menu.SelectedIndex = 3;

			// WHEN p_list_menu is selected
			p_list_menu.SelectedIndexChanged += (sender, args) =>
			{
				if (p_list_menu.SelectedIndex == -1)
				{
				}
				else
				{
					switch(p_list_menu.SelectedIndex) 
					{
					case 0:
						ShowResumenTemporadaPie();
						break;
					case 1:
						ShowPescaRegionColumn();
						break;
					case 2:
						ShowPescaPuertoColumn();
						break;
					case 4:
						ShowPescaDiaColumnSpline();
						break;
					case 5:
						ShowMyPage();
						break;
					}

				}
			};
			return p_list_menu;
		}
	}
}

