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
	public class Gra05PescaDiaColumnSpline : GraFather, INotifyPropertyChanged
	{


		private bool _isBusy ;
		public bool column_spline_chart_already_loading
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				RaisePropertyChanged("column_spline_chart_already_loading");
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

		private string title_page_ = "AVANCE DE PESCA - DESCARGA DÍA";
		private string [] menu_labels_ = {"Año: ","Zona: ","Periodo: ", "Mes: ", "Quincena: "};

		private Picker pmenu_pesquera_;
		private Picker p_list_period_;
		private Picker p_list_year_;
		private Picker p_list_zone_;
		private Picker p_list_quincena_;
		private Picker p_list_mes_;
		private List<dtoZona> zonaNameList_;

		ColumnSeries col_bars_;
		SplineSeries col_splines_;

		bool periodo_already_loading = false;

		HttpJsonLoader http_loader_ =  new HttpJsonLoader();

		public Gra05PescaDiaColumnSpline ()
		{
			GetChart();
			this.Content = main_page_;
			indicator.SetBinding (ActivityIndicator.IsRunningProperty, "column_spline_chart_already_loading");
			indicator.SetBinding (ActivityIndicator.IsVisibleProperty, "column_spline_chart_already_loading");
			indicator.BindingContext = this;
			column_spline_chart_already_loading = false;
		}

		async void ShowMyPage(){
			pmenu_pesquera_.SelectedIndex = 4;
			await Navigation.PushAsync( new MyPage() ) ;
		}

		async void ShowGra01ResumenTemporadaPie(){
			pmenu_pesquera_.SelectedIndex = 4;
			await Navigation.PushAsync( new Gra01ResumenTemporadaPie() ) ;
		}

		async void ShowGra02PescaRegionColumn(){
			pmenu_pesquera_.SelectedIndex = 4;
			await Navigation.PushAsync( new Gra02PescaRegionColumn() ) ;
		}

		async void ShowGra03PescaPuertoColumn(){
			pmenu_pesquera_.SelectedIndex = 4;
			await Navigation.PushAsync( new Gra03PescaPuertoColumn() ) ;
		}

		async void ShowGra04PescaPlantaBar(){
			pmenu_pesquera_.SelectedIndex = 4;
			await Navigation.PushAsync( new Gra04PescaPlantaBar() ) ;
		}

		async void ShowGra06QuincenaColumnSpline(){
			pmenu_pesquera_.SelectedIndex = 4;
			await Navigation.PushAsync (new Gra06QuincenaColumnSpline ());
		}

		async void ShowGra07GruposMColumn(){
			pmenu_pesquera_.SelectedIndex = 4;
			await Navigation.PushAsync (new Gra07GruposMColumn ());
		}

		async void ShowGra08GruposRangoBar(){
			pmenu_pesquera_.SelectedIndex = 4;
			await Navigation.PushAsync (new Gra08GruposRangoBar ());
		}

		private  async void GetChart()
		{

			SfChart chart = new SfChart();
			chart.PrimaryAxis = new CategoryAxis() { LabelPlacement = LabelPlacement.BetweenTicks };
			chart.SecondaryAxis = new NumericalAxis();

			ChartDataMarker col_dataMarker = new ChartDataMarker() { ShowLabel = true };
			col_dataMarker.LabelStyle.Font = Font.SystemFontOfSize(10);
			col_dataMarker.LabelStyle.BackgroundColor = Color.White;
			col_dataMarker.LabelStyle.TextColor = Color.Black;

			ChartDataMarker spline_dataMarker = new ChartDataMarker() { ShowLabel = true };
			spline_dataMarker.LabelStyle.Font = Font.SystemFontOfSize(10);
			spline_dataMarker.LabelStyle.BackgroundColor = Color.White;
			spline_dataMarker.LabelStyle.TextColor = Color.Purple;

			col_bars_ = new ColumnSeries () {
				Label = "TM",
				ItemsSource = GetEmptyData(),
				DataMarkerPosition = Syncfusion.SfChart.XForms.DataMarkerPosition.Center,
			};
			col_bars_.DataMarker = col_dataMarker;

			col_splines_ = new SplineSeries(){
				Label = "N° E/P",
				ItemsSource = GetEmptyData(),
				StrokeWidth = 3,
				YAxis = new NumericalAxis(){ OpposedPosition = true, ShowMajorGridLines = false
				}
			};
			col_splines_.DataMarker = spline_dataMarker;

			chart.Series.Add (col_bars_);
			chart.Series.Add (col_splines_);

			chart.VerticalOptions = LayoutOptions.FillAndExpand;
			chart.HorizontalOptions = LayoutOptions.FillAndExpand;
			chart.Legend = new ChartLegend ();

			////////////// Picker#
			/// 
			/// Picker Period
			p_list_period_ = new Picker
			{
				Title = menu_labels_[2],
				VerticalOptions = LayoutOptions.StartAndExpand
			};
					
			/// Picker Year
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

			/// Picker QUINCENA
			p_list_quincena_ = new Picker
			{
				Title = menu_labels_[3],
				VerticalOptions = LayoutOptions.StartAndExpand
			};
			foreach (string quincenaName in GlobalParameters.LIST_QUINCENAS)
			{
				p_list_quincena_.Items.Add(quincenaName);
			}
			p_list_quincena_.SelectedIndex = 0;
					
			/// Picker MES
			p_list_mes_ = new Picker
			{
				Title = menu_labels_[4],
				VerticalOptions = LayoutOptions.StartAndExpand
			};
			foreach (string mesName in GlobalParameters.LIST_MESES)
			{
				p_list_mes_.Items.Add(mesName);
			}
			p_list_mes_.SelectedIndex = 0;

			///////
			p_list_year_.SelectedIndexChanged += (sender, args) =>
			{
				if (p_list_year_.SelectedIndex == -1)
				{

					col_bars_.ItemsSource = GetEmptyData();
				}
				else
				{
					/*
					int num = p_list_year_.SelectedIndex;
					if (num == 0) col_bars_.ItemsSource = GetEmptyData();
					else  col_bars_.ItemsSource = GetData2();
					*/
					LoadZones();
				}
			};

			p_list_zone_.SelectedIndexChanged +=  (sender, args) => {
				if ( p_list_zone_.SelectedIndex == -1 ){
					col_bars_.ItemsSource = GetEmptyData();
				}else{
					LoadPeriodos( false );
				}
			};

			p_list_period_.SelectedIndexChanged += ( sender, args) => {
				if (p_list_period_.SelectedIndex == -1) {
					col_bars_.ItemsSource = GetEmptyData();
				} else {
					LoadAllGrafico05 (false);
				}
			};

			p_list_mes_.SelectedIndexChanged += ( sender, args) => {
				if (p_list_mes_.SelectedIndex == -1) {
					col_bars_.ItemsSource = GetEmptyData();
				} else {
					LoadAllGrafico05 (false);
				}
			};

			p_list_quincena_.SelectedIndexChanged += ( sender, args) => {
				if (p_list_quincena_.SelectedIndex == -1) {
					col_bars_.ItemsSource = GetEmptyData();
				} else {
					LoadAllGrafico05 (false);
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
							p_list_mes_,
							new Label(){
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
								Text = menu_labels_[4]
							},
							p_list_quincena_
						}
					},
					chart
				}
			};
			main_page_ = main_layout;

			List<dtoAnio> yearNameList = await http_loader_.LoadAniosFromInternet ();
			p_list_year_.Items.Clear ();
			foreach (dtoAnio yearName in yearNameList)
			{
				p_list_year_.Items.Add(yearName.anoTempo.ToString());
			}
			p_list_year_.SelectedIndex = 0;
		}


		private async void LoadZones(){
			periodo_already_loading = true;
			column_spline_chart_already_loading = true;
	
			col_bars_.ItemsSource = new ObservableCollection<ChartDataPoint> ();
			col_splines_.ItemsSource = new ObservableCollection<ChartDataPoint> ();

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
			column_spline_chart_already_loading = true;

			col_bars_.ItemsSource = new ObservableCollection<ChartDataPoint> ();
			col_splines_.ItemsSource = new ObservableCollection<ChartDataPoint> ();

			int anoTempo = int.Parse (p_list_year_.Items.ElementAt (p_list_year_.SelectedIndex));
			string codigoZona = zonaNameList_ [p_list_zone_.SelectedIndex ].codigoZona;

			List<dtoPeriodo> periodoList = await http_loader_.LoadPeriodosFromInternet ( anoTempo, codigoZona );
			p_list_period_.Items.Clear ();
			foreach (dtoPeriodo periodoId in periodoList) {
				p_list_period_.Items.Add ( periodoId.periodo );
			}
			p_list_period_.SelectedIndex = 0;
			periodo_already_loading = false;
			LoadAllGrafico05 (true);
		}

		private async void LoadAllGrafico05( bool from_load_periodos ){
			if (from_load_periodos == false) {
				if ( column_spline_chart_already_loading == true)
					return;
			}
				
			/*
			indicator.SetBinding (ActivityIndicator.IsRunningProperty, "IsLoading");
			indicator.SetBinding (ActivityIndicator.IsVisibleProperty, "IsLoading");
			LoadingModel lm = new LoadingModel ();
			indicator.BindingContext = lm;
			*/
			column_spline_chart_already_loading = true;

			ObservableCollection<ChartDataPoint> g05_data_col = new ObservableCollection<ChartDataPoint> ();
			col_bars_.ItemsSource = g05_data_col;
			ObservableCollection<ChartDataPoint> g05_data_spline = new ObservableCollection<ChartDataPoint> ();
			col_splines_.ItemsSource = g05_data_spline;

			int anoTempo = int.Parse (p_list_year_.Items.ElementAt (p_list_year_.SelectedIndex));
			string codigoZona = zonaNameList_ [p_list_zone_.SelectedIndex].codigoZona;
			string periodo = p_list_period_.Items.ElementAt (p_list_period_.SelectedIndex);
			string mes = GlobalParameters.LIST_COD_MESES [p_list_mes_.SelectedIndex];
			string quincena = GlobalParameters.LIST_COD_QUINCENAS [p_list_quincena_.SelectedIndex];

			List< dtoGrafico05> list_g05 = await http_loader_.LoadGrafico05FromInternet (anoTempo, codigoZona, periodo, mes, quincena);
			int dia_id = 1;
			foreach (dtoGrafico05 g05_item in list_g05) {
				g05_data_col.Add( new ChartDataPoint ( dia_id.ToString(), g05_item.tmDescarga ) );
				g05_data_spline.Add (new ChartDataPoint (dia_id.ToString (), g05_item.nEP));
				dia_id ++;
			}
			col_bars_.ItemsSource = g05_data_col;
			col_splines_.ItemsSource = g05_data_spline;

			column_spline_chart_already_loading = false;
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
			p_list_menu.SelectedIndex = 4;

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
					case 5:
						ShowGra06QuincenaColumnSpline();
						break;
					case 6:
						ShowGra07GruposMColumn();
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

