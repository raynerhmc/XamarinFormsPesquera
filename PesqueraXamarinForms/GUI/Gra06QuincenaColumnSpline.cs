
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
	public class Gra06QuincenaColumnSpline : GraFather, INotifyPropertyChanged
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

		private string title_page_ = "AVANCE DE PESCA - DESCARGA QUINCENA";
		private string [] menu_labels_ = {"Año: ","Zona: ","Periodo: "};

		private Picker p_list_period_;
		private Picker p_list_year_;
		private Picker p_list_zone_;
		private List<dtoZona> zonaNameList_;

		ColumnSeries col_bars_;
		SplineSeries col_splines_;

		bool periodo_already_loading = false;

		HttpJsonLoader http_loader_ =  new HttpJsonLoader();

		public Gra06QuincenaColumnSpline ()
		{
			GetChart();
			this.Content = main_page_;
			indicator.SetBinding (ActivityIndicator.IsRunningProperty, "column_spline_chart_already_loading");
			indicator.SetBinding (ActivityIndicator.IsVisibleProperty, "column_spline_chart_already_loading");
			indicator.BindingContext = this;
			column_spline_chart_already_loading = false;
		}
			
		private  async void GetChart()
		{

			SfChart chart = new SfChart();
			chart.PrimaryAxis = new CategoryAxis() { LabelPlacement = LabelPlacement.BetweenTicks };
			chart.SecondaryAxis = new NumericalAxis();
			chart.ChartBehaviors.Add(new ChartZoomPanBehavior(){ EnablePanning = true, EnableZooming = true}) ;

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
					//LoadAllGrafico06 (false);
				}
			};
				
			Button refrescar = new Button ();
			refrescar.Image = (FileImageSource) FileImageSource.FromFile("refrescar.png");
			refrescar.Clicked += (object sender, EventArgs e) => {
				LoadAllGrafico06 (false);
			};

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
					refrescar,
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
			column_spline_chart_already_loading = false;
			//LoadAllGrafico06 (true);
		}

		private async void LoadAllGrafico06( bool from_load_periodos ){
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

			ObservableCollection<ChartDataPoint> g06_data_col = new ObservableCollection<ChartDataPoint> ();
			col_bars_.ItemsSource = g06_data_col;
			ObservableCollection<ChartDataPoint> g06_data_spline = new ObservableCollection<ChartDataPoint> ();
			col_splines_.ItemsSource = g06_data_spline;

			int anoTempo = int.Parse (p_list_year_.Items.ElementAt (p_list_year_.SelectedIndex));
			string codigoZona = zonaNameList_ [p_list_zone_.SelectedIndex].codigoZona;
			string periodo = p_list_period_.Items.ElementAt (p_list_period_.SelectedIndex);

			List< dtoGrafico06> list_g06 = await http_loader_.LoadGrafico06FromInternet (anoTempo, codigoZona, periodo);
			foreach (dtoGrafico06 g06_item in list_g06) {
				g06_data_col.Add( new ChartDataPoint ( g06_item.codigoMes + "-" + g06_item.quincena.ToString(), g06_item.tmDescarga ) );
				g06_data_spline.Add (new ChartDataPoint (g06_item.codigoMes + "-" + g06_item.quincena.ToString(), g06_item.nEP));
			}
			col_bars_.ItemsSource = g06_data_col;
			col_splines_.ItemsSource = g06_data_spline;

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
	}
}

