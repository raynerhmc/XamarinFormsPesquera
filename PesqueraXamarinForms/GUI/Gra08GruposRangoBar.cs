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
	public class Gra08GruposRangoBar : GraFather, INotifyPropertyChanged
	{
		private bool _isBusy ;
		public bool row_chart_already_loading
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				RaisePropertyChanged("row_chart_already_loading");
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

		private string title_page_ = "AVANCE POR GRUPOS EN [RANGO %]";
		private string[] menu_labels_ = { "Año: ", "Zona: ", "Periodo: ", "Rango: " };

		private Picker pmenu_pesquera_;
		private Picker p_list_period_;
		private Picker p_list_year_;
		private Picker p_list_zone_;
		private Picker p_list_rango_;

		private List<dtoZona> zonaNameList_;

		BarSeries row_bars_;

		bool periodo_already_loading = false;


		HttpJsonLoader http_loader_ =  new HttpJsonLoader();

		bool first_time_loading_years = false;
		bool first_time_loading_zones = false;
		bool first_time_loading_periods = false;
		bool first_time_loading_rangos = false;

		public Gra08GruposRangoBar ( HttpJsonLoader http_loader, int anio_index, int zona_index, 
			int periodo_index, int rango_index){
			http_loader_ = http_loader;
			zonaNameList_ = http_loader_.lzonas;

			first_time_loading_years = true;
			first_time_loading_zones = true;
			first_time_loading_periods = true;
			first_time_loading_rangos = true;

			GetChart();
			this.Content = main_page_;


			p_list_year_.Items.Clear ();
			foreach (dtoAnio yearName in http_loader_.lanios)
			{
				p_list_year_.Items.Add(yearName.anoTempo.ToString());
			}
			p_list_year_.SelectedIndex = anio_index;

			p_list_zone_.Items.Clear ();
			foreach (dtoZona zoneName in http_loader_.lzonas) {
				p_list_zone_.Items.Add (zoneName.descripcionZona);
			}
			p_list_zone_.SelectedIndex = zona_index;


			p_list_period_.Items.Clear ();
			foreach (dtoPeriodo periodoId in http_loader_.lperiodos) {
				p_list_period_.Items.Add ( periodoId.periodo );
			}
			p_list_period_.SelectedIndex = periodo_index;


			p_list_rango_.SelectedIndex = rango_index;

			indicator.SetBinding (ActivityIndicator.IsRunningProperty, "row_chart_already_loading");
			indicator.SetBinding (ActivityIndicator.IsVisibleProperty, "row_chart_already_loading");
			indicator.BindingContext = this;
			row_chart_already_loading = false;
		}

		public Gra08GruposRangoBar ()
		{
			GetChart();
			this.Content = main_page_;

			indicator.SetBinding (ActivityIndicator.IsRunningProperty, "row_chart_already_loading");
			indicator.SetBinding (ActivityIndicator.IsVisibleProperty, "row_chart_already_loading");
			indicator.BindingContext = this;
			row_chart_already_loading = false;
		}

		async void ShowMyPage(){
			pmenu_pesquera_.SelectedIndex = 7;
			await Navigation.PushAsync( new MyPage() ) ;
		}

		async void ShowGra01ResumenTemporadaPie(){
			pmenu_pesquera_.SelectedIndex = 7;
			await Navigation.PushAsync( new Gra01ResumenTemporadaPie() ) ;
		}

		async void ShowGra02PescaRegionColumn(){
			pmenu_pesquera_.SelectedIndex = 7;
			await Navigation.PushAsync( new Gra02PescaRegionColumn() ) ;
		}

		async void ShowGra03PescaPuertoColumn(){
			pmenu_pesquera_.SelectedIndex = 7;
			await Navigation.PushAsync( new Gra03PescaPuertoColumn() ) ;
		}

		async void ShowGra04PescaPlantaBar(){
			pmenu_pesquera_.SelectedIndex = 7;
			await Navigation.PushAsync (new Gra04PescaPlantaBar ());
		}

		async void ShowGra05PescaDiaColumnSpline(){
			pmenu_pesquera_.SelectedIndex = 7;
			await Navigation.PushAsync( new Gra05PescaDiaColumnSpline() ) ;
		}

		async void ShowGra06QuincenaColumnSpline(){
			pmenu_pesquera_.SelectedIndex = 7;
			await Navigation.PushAsync (new Gra06QuincenaColumnSpline ());
		}

		async void ShowGra07GruposMColumn(){
			pmenu_pesquera_.SelectedIndex = 7;
			await Navigation.PushAsync (new Gra07GruposMColumn ());
		}


		private async void GetChart()
		{

			SfChart chart = new SfChart();
			chart.PrimaryAxis = new CategoryAxis() { LabelPlacement = LabelPlacement.BetweenTicks };
			chart.SecondaryAxis = new NumericalAxis(){ LabelRotationAngle = 0, MaximumLabels =5  };

			ChartDataMarker dataMarker = new ChartDataMarker() { ShowLabel = true };
			dataMarker.LabelStyle.Font = Font.SystemFontOfSize(11);
			dataMarker.LabelStyle.BackgroundColor = Color.White;
			dataMarker.LabelStyle.TextColor = Color.Black;
			dataMarker.LabelStyle.LabelPosition = DataMarkerLabelPosition.Auto;
			//dataMarker.LabelStyle.Angle = 90;
			chart.ChartBehaviors.Add(new ChartZoomPanBehavior(){ EnablePanning = true, EnableZooming = true}) ;

			chart.PrimaryAxis.LabelRotationAngle = -45;
			chart.PrimaryAxis.LabelStyle.Font = Font.SystemFontOfSize(7);
			row_bars_ = new BarSeries () {
				ItemsSource = GetEmptyData(),
				//DataMarkerPosition = Syncfusion.SfChart.XForms.DataMarkerPosition.Center,
			};

			row_bars_.DataMarker = dataMarker;
			row_bars_.EnableDataPointSelection = true;

			chart.Series.Add (row_bars_);

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

			/// Picker rango
			p_list_rango_ = new Picker
			{
				Title = menu_labels_[3],
				VerticalOptions = LayoutOptions.StartAndExpand
			};
			foreach (string rangoName in GlobalParameters.LIST_RANGO_PORCENTAGES)
			{
				p_list_rango_.Items.Add(rangoName);
			}
			p_list_rango_.SelectedIndex = 0;
			//////////


			p_list_year_.SelectedIndexChanged += (sender, args) =>
			{
				if (p_list_year_.SelectedIndex == -1){
					row_bars_.ItemsSource = GetEmptyData();
				}
				else
				{
					/*
					int num = p_list_year_.SelectedIndex;
					if (num == 0) row_bars_.ItemsSource = GetEmptyData()();
					else  row_bars_.ItemsSource = GetData2();
					*/
					if(first_time_loading_zones == false)  {
						LoadZones();
					}
					first_time_loading_zones = false;
				}
			};

			p_list_zone_.SelectedIndexChanged +=  (sender, args) => {
				if ( p_list_zone_.SelectedIndex == -1 ){
					row_bars_.ItemsSource = GetEmptyData();
				}else{
					if(first_time_loading_periods == false)  {
						LoadPeriodos( false );
					}
					first_time_loading_periods = false;
				}
			};

			p_list_period_.SelectedIndexChanged += ( sender, args) => {
				if (p_list_period_.SelectedIndex == -1) {
					row_bars_.ItemsSource = GetEmptyData();
				} else {
					if(first_time_loading_rangos == false)  {
						LoadAllGrafico08 (false);
					}
					first_time_loading_rangos = false;
				}
			};

			p_list_rango_.SelectedIndexChanged += ( sender, args) => {
				if (p_list_rango_.SelectedIndex == -1) {
					row_bars_.ItemsSource = GetEmptyData();
				} else {
					LoadAllGrafico08 (false);
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
					//pmenu_pesquera_,
					indicator,
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
							p_list_year_,

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
									p_list_zone_
								}
							},
							new Label(){
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
								Text = menu_labels_[2]
							},
							p_list_period_
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
							p_list_rango_
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
		}


		private async void LoadZones(){
			periodo_already_loading = true;

			row_chart_already_loading = true;
			ObservableCollection<ChartDataPoint> g08_data = new ObservableCollection<ChartDataPoint> ();
			row_bars_.ItemsSource = g08_data;

			int year = int.Parse (p_list_year_.Items.ElementAt (p_list_year_.SelectedIndex));
			zonaNameList_ = await http_loader_.LoadZonasFromInternet ( year );
			p_list_zone_.Items.Clear ();
			foreach (dtoZona zoneName in zonaNameList_) {
				p_list_zone_.Items.Add (zoneName.descripcionZona);
			}
			p_list_zone_.SelectedIndex = 0;
			LoadPeriodos( true );
		}

		private async void LoadPeriodos( bool from_load_zones ){

			if (from_load_zones == false) {
				if ( periodo_already_loading == true)
					return;
			}
			periodo_already_loading = true;

			row_chart_already_loading = true;
			ObservableCollection<ChartDataPoint> g08_data = new ObservableCollection<ChartDataPoint> ();
			row_bars_.ItemsSource = g08_data;

			int anoTempo = int.Parse (p_list_year_.Items.ElementAt (p_list_year_.SelectedIndex));
			string codigoZona = zonaNameList_ [p_list_zone_.SelectedIndex ].codigoZona;

			List<dtoPeriodo> periodoList = await http_loader_.LoadPeriodosFromInternet ( anoTempo, codigoZona );
			p_list_period_.Items.Clear ();
			foreach (dtoPeriodo periodoId in periodoList) {
				p_list_period_.Items.Add ( periodoId.periodo );
			}
			p_list_period_.SelectedIndex = 0;
			periodo_already_loading = false;
			LoadAllGrafico08 (true);
		}

		private async void LoadAllGrafico08( bool from_load_periodos ){
			if (from_load_periodos == false) {
				if ( row_chart_already_loading == true)
					return;
			}
			row_chart_already_loading = true;
			ObservableCollection<ChartDataPoint> g08_data = new ObservableCollection<ChartDataPoint> ();
			row_bars_.ItemsSource = g08_data;

			int anoTempo = int.Parse (p_list_year_.Items.ElementAt (p_list_year_.SelectedIndex));
			string codigoZona = zonaNameList_ [p_list_zone_.SelectedIndex ].codigoZona;
			string periodo = p_list_period_.Items.ElementAt (p_list_period_.SelectedIndex ) ;
			string ranPorcen = GlobalParameters.LIST_RANGO_PORCENTAGES [p_list_rango_.SelectedIndex];

			List< dtoGrafico08 > list_g08 = await http_loader_.LoadGrafico08FromInternet (anoTempo, codigoZona, periodo, ranPorcen);
			foreach (dtoGrafico08 g08_item in list_g08) {
				g08_data.Add ( new ChartDataPoint ( g08_item.nombreGrupo, g08_item.tmDescarga ) );
			}
			row_bars_.ItemsSource = g08_data;

			row_chart_already_loading = false;
		}



		public static ObservableCollection<ChartDataPoint> GetEmptyData()
		{
			ObservableCollection<ChartDataPoint> datas = new ObservableCollection<ChartDataPoint>();
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
					case 6:
						ShowGra07GruposMColumn();
						break;
					}

				}
			};
			return p_list_menu;
		}
	}
}

