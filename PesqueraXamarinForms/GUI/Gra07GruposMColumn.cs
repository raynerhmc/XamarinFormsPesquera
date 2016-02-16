using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PesqueraXamarinForms
{
	public class Gra07GruposMColumn : ContentPage, INotifyPropertyChanged
	{
		private bool _isBusy ;
		public bool column_chart_already_loading
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				RaisePropertyChanged("column_chart_already_loading");
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;

		public void RaisePropertyChanged(string propName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}
		ActivityIndicator indicator = new ActivityIndicator {
			IsRunning = false,
			IsVisible = false,
			Color = Color.Blue
		};

		StackLayout main_page_;

		private string title_page_ = "AVANCE POR GRUPOS";
		private string [] menu_labels_ = {"Año: ","Zona: ","Periodo: "};

		private Picker pmenu_pesquera_;
		private Picker p_list_period_;
		private Picker p_list_year_;
		private Picker p_list_zone_;
		private List<dtoZona> zonaNameList_;


		ColumnSeries col_bars1_;


		bool periodo_already_loading = false;
		bool first_time_loading_years = false;
		bool first_time_loading_zones = false;
		bool first_time_loading_periods = false;

		HttpJsonLoader http_loader_ =  new HttpJsonLoader();

		public Gra07GruposMColumn ()
		{
			GetChart();
			this.Content = main_page_;

			indicator.SetBinding (ActivityIndicator.IsRunningProperty, "column_chart_already_loading");
			indicator.SetBinding (ActivityIndicator.IsVisibleProperty, "column_chart_already_loading");
			indicator.BindingContext = this;
			column_chart_already_loading = false;
		}

		async void ShowMyPage(){
			pmenu_pesquera_.SelectedIndex = 6;
			await Navigation.PushAsync (new MyPage ());
		}

		async void ShowGra01ResumenTemporadaPie(){
			pmenu_pesquera_.SelectedIndex = 6;
			await Navigation.PushAsync (new Gra01ResumenTemporadaPie ());
		}

		async void ShowGra02PescaRegionColumn(){
			pmenu_pesquera_.SelectedIndex = 6;
			await Navigation.PushAsync( new Gra02PescaRegionColumn() ) ;
		}
			
		async void ShowGra03PescaPuertoColumn(){
			pmenu_pesquera_.SelectedIndex = 6;
			await Navigation.PushAsync (new Gra03PescaPuertoColumn ());
		}

		async void ShowGra04PescaPlantaBar(){
			pmenu_pesquera_.SelectedIndex = 6;
			await Navigation.PushAsync (new Gra04PescaPlantaBar ());
		}

		async void ShowGra05PescaDiaColumnSpline(){
			pmenu_pesquera_.SelectedIndex = 6;
			await Navigation.PushAsync (new Gra05PescaDiaColumnSpline ());
		}

		async void ShowGra06QuincenaColumnSpline(){
			pmenu_pesquera_.SelectedIndex = 6;
			await Navigation.PushAsync (new Gra06QuincenaColumnSpline ());
		}

		async void ShowGra08GruposRangoBar(){
			pmenu_pesquera_.SelectedIndex = 6;
			await Navigation.PushAsync (new Gra08GruposRangoBar ());
		}

		async void ShowGra08GruposRangoBarWithData( int selected_rango ){
			if (selected_rango == -1)
				return;
			pmenu_pesquera_.SelectedIndex = 6;
			await Navigation.PushAsync (new Gra08GruposRangoBar (http_loader_, p_list_year_.SelectedIndex, 
				p_list_zone_.SelectedIndex, p_list_period_.SelectedIndex, selected_rango));
		}

		private async void GetChart()
		{

			SfChart chart = new SfChart() { Legend = new ChartLegend(){
					DockPosition = LegendPlacement.Bottom
				} };
			chart.PrimaryAxis = new CategoryAxis() { LabelPlacement = LabelPlacement.BetweenTicks };

			chart.SecondaryAxis = new NumericalAxis();
			chart.ChartBehaviors.Add(new ChartZoomPanBehavior(){ EnablePanning = true, EnableZooming = true}) ;

			ChartDataMarker dataMarker = new ChartDataMarker() { ShowLabel = true , ShowMarker = true, LabelContent = LabelContent.YValue };
			dataMarker.LabelStyle.Font = Font.SystemFontOfSize(11);
			dataMarker.LabelStyle.BackgroundColor = Color.White;
			dataMarker.LabelStyle.TextColor = Color.Black;

			List<Color> colors = new List<Color>(){
				Color.Blue,
				Color.Green,
				Color.Yellow, 
				Color.Teal,
				Color.Red
			};

			col_bars1_ = new ColumnSeries (){
				Label = "Rango de % de avance de grupos",
				ItemsSource = GetData1(),
				DataMarkerPosition = Syncfusion.SfChart.XForms.DataMarkerPosition.Top
			};



			col_bars1_.ColorModel.Palette = ChartColorPalette.Custom;
			col_bars1_.ColorModel.CustomBrushes = colors;

			col_bars1_.DataMarker = dataMarker;
			col_bars1_.EnableDataPointSelection = true;

		

			chart.Series.Add (col_bars1_);


			chart.VerticalOptions = LayoutOptions.FillAndExpand;
			chart.HorizontalOptions = LayoutOptions.FillAndExpand;


			////////////// Picker#
			/// 
			/// Picker Period
			p_list_period_ = new Picker
			{
				Title = menu_labels_[2],
				VerticalOptions = LayoutOptions.StartAndExpand
			};


			/// Picker year
			p_list_year_ = new Picker
			{
				Title = menu_labels_[0],
				VerticalOptions = LayoutOptions.StartAndExpand
			};


			/// Picker zona
			p_list_zone_ = new Picker
			{
				Title = menu_labels_[1],
				VerticalOptions = LayoutOptions.StartAndExpand
			};



			p_list_year_.SelectedIndexChanged += (sender, args) =>
			{
				if (p_list_year_.SelectedIndex == -1){
					col_bars1_.ItemsSource = GetData1();

				}
				else
				{
					/*->  Temporal 
					int num = p_list_year_.SelectedIndex;
					if (num == 0) col_bars_.ItemsSource = GetData1();
					else  col_bars_.ItemsSource = GetData2();
					*/
					if(first_time_loading_zones == false)  {
						LoadZones();
					}
					first_time_loading_zones = false;
				}
			};

			p_list_zone_.SelectedIndexChanged +=  (sender, args) => {
				if ( p_list_zone_.SelectedIndex == -1 ){
					col_bars1_.ItemsSource = GetData1();

				}else{
					if(first_time_loading_periods == false)  {
						LoadPeriodos( false );
					}
					first_time_loading_periods = false;
				}
			};

			p_list_period_.SelectedIndexChanged += ( sender, args) => {
				if (p_list_period_.SelectedIndex == -1) {
					col_bars1_.ItemsSource = GetData1();

				} else {
					LoadAllGrafico07 (false);
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
					indicator,
					new Label(){
						Text = title_page_,
						HorizontalOptions = LayoutOptions.Center
					},
					new StackLayout(){
						Spacing = 5,
						VerticalOptions = LayoutOptions.Start,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Orientation = StackOrientation.Horizontal,
						Children = {
							new Label(){
								Text = menu_labels_[0],
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_
							},
							p_list_year_,

							new StackLayout{
								Spacing = 0,
								VerticalOptions = LayoutOptions.Start,
								HorizontalOptions = LayoutOptions.CenterAndExpand,
								Orientation = StackOrientation.Horizontal,
								Children = {
									new Label(){
										Text = menu_labels_[1],
										FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_
									},
									p_list_zone_
								}
							},
							new Label(){
								Text = menu_labels_[2],
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_
							},
							p_list_period_
						}
					},
					chart
				}
			};
			main_page_ = main_layout;

			if (first_time_loading_years == false) {
				List<dtoAnio> yearNameList = await http_loader_.LoadAniosFromInternet ();
				p_list_year_.Items.Clear ();
				foreach (dtoAnio yearName in yearNameList) {
					p_list_year_.Items.Add (yearName.anoTempo.ToString ());
				}
				p_list_year_.SelectedIndex = 0;
			}
			first_time_loading_years = false;

			chart.SelectionChanged += (object sender, ChartSelectionEventArgs csea) => {
				ShowGra08GruposRangoBarWithData (csea.SelectedDataPointIndex);
			};	
		}


		private async void LoadZones(){

			periodo_already_loading = true;
			column_chart_already_loading = true;
			col_bars1_.ItemsSource = GetData1();

			int year = int.Parse (p_list_year_.Items.ElementAt (p_list_year_.SelectedIndex));
			zonaNameList_ = await http_loader_.LoadZonasFromInternet (year);
			p_list_zone_.Items.Clear ();
			foreach (dtoZona zoneName in zonaNameList_) {
				p_list_zone_.Items.Add (zoneName.descripcionZona);
			}
			p_list_zone_.SelectedIndex = 0;
			LoadPeriodos (true);

		}

		private async void LoadPeriodos( bool from_load_zones ){

			if (from_load_zones == false) {
				if (periodo_already_loading == true)
					return;
			}
			periodo_already_loading = true;
			column_chart_already_loading = true;
			col_bars1_.ItemsSource = GetData1();


			int anoTempo = int.Parse (p_list_year_.Items.ElementAt (p_list_year_.SelectedIndex));
			string codigoZona = zonaNameList_ [p_list_zone_.SelectedIndex].codigoZona;

			List<dtoPeriodo> periodoList = await http_loader_.LoadPeriodosFromInternet (anoTempo, codigoZona);
			p_list_period_.Items.Clear ();
			foreach (dtoPeriodo periodoId in periodoList) {
				p_list_period_.Items.Add (periodoId.periodo);
			}
			p_list_period_.SelectedIndex = 0;
			periodo_already_loading = false;

			LoadAllGrafico07 (true);

		}

		private async void LoadAllGrafico07( bool from_load_periodos ){
			if (from_load_periodos == false) {
				if ( column_chart_already_loading == true)
					return;
			}
			column_chart_already_loading = true;

			ObservableCollection<ChartDataPoint> g07_data = new ObservableCollection<ChartDataPoint> ();
			col_bars1_.ItemsSource = g07_data;

			int anoTempo = int.Parse (p_list_year_.Items.ElementAt (p_list_year_.SelectedIndex));
			string codigoZona = zonaNameList_ [p_list_zone_.SelectedIndex ].codigoZona;
			string periodo = p_list_period_.Items.ElementAt (p_list_period_.SelectedIndex ) ;

			List< dtoGrafico07 > list_g07 = await http_loader_.LoadGrafico07FromInternet (anoTempo, codigoZona, periodo);

			foreach (dtoGrafico07 g07_item in list_g07) {
				g07_data .Add (new ChartDataPoint (g07_item.ranPorcen, g07_item.porcen));
			}
			col_bars1_.ItemsSource = g07_data;


			column_chart_already_loading = false;
		}

		public static ObservableCollection<ChartDataPoint> GetData1()
		{
			ObservableCollection<ChartDataPoint> datas = new ObservableCollection<ChartDataPoint>();
			return datas;
		}

		public static ObservableCollection<ChartDataPoint> GetData2()
		{
			ObservableCollection<ChartDataPoint> datas = new ObservableCollection<ChartDataPoint>();
			datas.Add(new ChartDataPoint("Bentley", 54));
			datas.Add(new ChartDataPoint("Audi", 24));
			datas.Add(new ChartDataPoint("BMW", 53));
			datas.Add(new ChartDataPoint("Jaguar", 63));
			datas.Add(new ChartDataPoint("Skoda", 35));
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
			p_list_menu.SelectedIndex = 6;

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
						ShowGra01ResumenTemporadaPie();
						break;
					case 1:
						ShowGra02PescaRegionColumn();
						break;
					case 2:
						ShowGra03PescaPuertoColumn();
						break;
					case 3:
						ShowGra04PescaPlantaBar();
						break;
					case 4:
						ShowGra05PescaDiaColumnSpline();
						break;
					case 5:
						ShowGra06QuincenaColumnSpline();
						break;
					case 7:
						ShowGra08GruposRangoBar();
						break;
					}

				}
			};
			return p_list_menu;
		}
	}
}

