﻿
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
	public class Gra01ResumenTemporadaPie : GraFather, INotifyPropertyChanged
	{
		private bool _isBusy ;


		private Label labelTemporada;
		private Label labelExploratoria;
		private Label labelSaldo;

		public bool pie_chart_already_loading
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				RaisePropertyChanged("pie_chart_already_loading");
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
		private Picker p_list_period_;
		private Picker p_list_year_;
		private Picker p_list_zone_;
		private List<dtoZona> zonaNameList_;
		//private PieSeries pie_;
		private DoughnutSeries pie_;

		private bool periodo_already_loading = false;

		HttpJsonLoader http_loader_ =  new HttpJsonLoader();

		StackLayout main_page_;

		public Gra01ResumenTemporadaPie()
		{
			GetChart ();
			this.Content = main_page_;


			indicator.SetBinding (ActivityIndicator.IsRunningProperty, "pie_chart_already_loading");
			indicator.SetBinding (ActivityIndicator.IsVisibleProperty, "pie_chart_already_loading");
			indicator.BindingContext = this;
			pie_chart_already_loading = false;


		}
			
		void ShowGra02PescaRegionColumnWithData(){
			
			rootpage_.ShowGra02PescaRegionColumnWithData (http_loader_, p_list_year_.SelectedIndex, 
				p_list_zone_.SelectedIndex, p_list_period_.SelectedIndex);
		}

		private async void GetChart()
		{

			SfChart chart = new SfChart() { Legend = new ChartLegend(){
					DockPosition = LegendPlacement.Bottom
				} };
			chart.Legend.LabelStyle.Font = Font.SystemFontOfSize(GlobalParameters.LEGEND_TEXT_SIZE_SERIES_, FontAttributes.None);

			ObservableCollection<ChartDataPoint> datas = new ObservableCollection<ChartDataPoint>();

			//pie_ = new PieSeries();
			pie_ = new DoughnutSeries(){
				StartAngle = GlobalParameters.START_ANGLE_PIE_SERIE,
				EndAngle = GlobalParameters.END_ANGLE_PIE_SERIE,
				ExplodeIndex = 1,
				//EnableSmartLabels = true

			};

			//pie_.ExplodeOnTouch = true;
			pie_.ItemsSource = datas;
			pie_.LegendIcon = ChartLegendIcon.Diamond;


			//Appearance
			pie_.ColorModel.Palette = ChartColorPalette.Custom;
			pie_.ColorModel.CustomBrushes = GlobalParameters.COLORS_GRAPHIC01;

			pie_.DataMarkerPosition = CircularSeriesDataMarkerPosition.OutsideExtended;
			pie_.ConnectorLineType = ConnectorLineType.Line;
			pie_.DataMarker = new ChartDataMarker ();
			pie_.DataMarker.ShowMarker = true;
			pie_.DataMarker.MarkerWidth = 5;
			pie_.DataMarker.LabelContent = LabelContent.Percentage;
			pie_.DataMarker.LabelStyle.Font = Font.SystemFontOfSize(15);
			pie_.EnableDataPointSelection = true;


			chart.ChartBehaviors.Add(new ChartZoomPanBehavior(){ EnablePanning = true, EnableZooming = true}) ;
			chart.VerticalOptions = LayoutOptions.FillAndExpand;
			chart.HorizontalOptions = LayoutOptions.FillAndExpand;

			chart.Series.Add(pie_);



			////////////// Picker#
			/// 
			/// Picker Period
			p_list_period_ = new Picker
			{
				Title = "Periodo",
				VerticalOptions = LayoutOptions.StartAndExpand,
				Scale = GlobalParameters.SCALE_PICKER,
				WidthRequest = GlobalParameters.WIDTH_PICKER_PERIODO
			};

			/// Picker Anio
			p_list_year_ = new Picker
			{
				Title = "Año",
				VerticalOptions = LayoutOptions.StartAndExpand,
				Scale = GlobalParameters.SCALE_PICKER,
				WidthRequest = GlobalParameters.WIDTH_PICKER_ANIO
			};
					
			/// Picker zona
			p_list_zone_ = new Picker
			{
				Title = "Zona",
				VerticalOptions = LayoutOptions.StartAndExpand,
				Scale = GlobalParameters.SCALE_PICKER,
				WidthRequest = GlobalParameters.WIDTH_PICKER_ZONE
			};
					

			p_list_year_.SelectedIndexChanged +=   (sender, args) =>
			{
				if (p_list_year_.SelectedIndex == -1){
					pie_.ItemsSource = datas;
				} else {
					LoadZones();
				}
			};
				
			p_list_zone_.SelectedIndexChanged +=  (sender, args) => {
				if ( p_list_zone_.SelectedIndex == -1 ){
					pie_.ItemsSource = datas;
				}else{
					LoadPeriodos( false );
				}
			};

			p_list_period_.SelectedIndexChanged += ( sender, args) => {
				if (p_list_period_.SelectedIndex == -1) {
					pie_.ItemsSource = datas;
				} else {
					LoadGrafico01 (false);

				}
			};

			labelExploratoria = new Label (){
				Text = "0",
				FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.Start
			};
			labelTemporada = new Label (){
				Text = "0",
				FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.Center
			};
			labelSaldo = new Label (){
				Text = "0",
				FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.End
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
						Text = "AVANCE PESCA POR ZONA",
						HorizontalOptions = LayoutOptions.Center
					},
					new StackLayout(){
						Spacing = 0,
						VerticalOptions = LayoutOptions.Start,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Orientation = StackOrientation.Horizontal,
						Children = {
							new Label(){
								Text = "Año: ",
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
								VerticalOptions = LayoutOptions.Center
							},
							p_list_year_,

							new StackLayout{
								Spacing = 0,
								VerticalOptions = LayoutOptions.Start,
								HorizontalOptions = LayoutOptions.CenterAndExpand,
								Orientation = StackOrientation.Horizontal,
								Children = {
									new Label(){
										Text = "Zona: ",
										FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
										VerticalOptions = LayoutOptions.Center
									},
									p_list_zone_
								}
							},
							new Label(){
								Text = "Periodo: ",
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
								VerticalOptions = LayoutOptions.Center	
							},
							p_list_period_
						}
					},
					chart,
					new StackLayout () {
						Spacing = 20,
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						Orientation = StackOrientation.Horizontal,
						Children = {
							new StackLayout () {
								Spacing = 0,
								VerticalOptions = LayoutOptions.Center,
								HorizontalOptions = LayoutOptions.StartAndExpand,
								Orientation = StackOrientation.Vertical,
								Children = {
									new Label () {
										Text = "Exploratoria: ",
										FontAttributes = FontAttributes.Bold,
										FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
										VerticalOptions = LayoutOptions.Center,
										HorizontalOptions = LayoutOptions.Start,
										TextColor = GlobalParameters.COLORS_GRAPHIC01[0]
									},
									labelExploratoria
								}
							},
							new StackLayout () {
								Spacing = 0,
								VerticalOptions = LayoutOptions.Center,
								HorizontalOptions = LayoutOptions.CenterAndExpand,
								Orientation = StackOrientation.Vertical,
								Children = {
									new Label () {
										Text = "Temporada: ",
										FontAttributes = FontAttributes.Bold,
										FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
										VerticalOptions = LayoutOptions.Center,
										HorizontalOptions = LayoutOptions.Start,
										TextColor = GlobalParameters.COLORS_GRAPHIC01[1]
									},
									labelTemporada
								}
							},
							new StackLayout () {
								Spacing = 0,
								VerticalOptions = LayoutOptions.Center,
								HorizontalOptions = LayoutOptions.EndAndExpand,
								Orientation = StackOrientation.Vertical,
								Children = {
									new Label () {
										Text = "Saldo: ",
										FontAttributes = FontAttributes.Bold,
										FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_,
										VerticalOptions = LayoutOptions.Center,
										HorizontalOptions = LayoutOptions.EndAndExpand,
										TextColor = GlobalParameters.COLORS_GRAPHIC01[2]
									},
									labelSaldo
								}
							},
						}
					}
				}
			};
			main_page_ = main_layout;

			List<dtoAnio> yearNameList2 = await http_loader_.LoadAniosFromInternet ();
			p_list_year_.Items.Clear ();
			foreach (dtoAnio yearName in yearNameList2)
			{
				p_list_year_.Items.Add(yearName.anoTempo.ToString());
			}
			p_list_year_.SelectedIndex = 0;


			chart.SelectionChanged += (object sender, ChartSelectionEventArgs csea) => {
				ShowGra02PescaRegionColumnWithData();
			};

		}

		private async void LoadZones(){
			periodo_already_loading = true;
			pie_chart_already_loading = true;
			ObservableCollection<ChartDataPoint> g01_data = new ObservableCollection<ChartDataPoint> ();
			pie_.ItemsSource = g01_data;

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
			pie_chart_already_loading = true;
			ObservableCollection<ChartDataPoint> g01_data = new ObservableCollection<ChartDataPoint> ();
			pie_.ItemsSource = g01_data;


			int anoTempo = int.Parse (p_list_year_.Items.ElementAt (p_list_year_.SelectedIndex));
			string codigoZona = zonaNameList_ [p_list_zone_.SelectedIndex ].codigoZona;

			List<dtoPeriodo> periodoList = await http_loader_.LoadPeriodosFromInternet ( anoTempo, codigoZona );
			p_list_period_.Items.Clear ();
			foreach (dtoPeriodo periodoId in periodoList) {
				p_list_period_.Items.Add ( periodoId.periodo );
			}
			p_list_period_.SelectedIndex = 0;
			periodo_already_loading = false;
			LoadGrafico01 (true);
		}

		private async void LoadGrafico01( bool from_load_periodos ){
			if (from_load_periodos == false) {
				if ( pie_chart_already_loading == true)
					return;
			}
			pie_chart_already_loading = true;
			ObservableCollection<ChartDataPoint> g01_data = new ObservableCollection<ChartDataPoint> ();
			pie_.ItemsSource = g01_data;

			int anoTempo = int.Parse (p_list_year_.Items.ElementAt (p_list_year_.SelectedIndex));
			string codigoZona = zonaNameList_ [p_list_zone_.SelectedIndex ].codigoZona;
			string periodo = p_list_period_.Items.ElementAt (p_list_period_.SelectedIndex ) ;

			dtoGrafico01 g01 = await http_loader_.LoadGrafico01FromInternet (anoTempo, codigoZona, periodo);

			if (g01 != null) {
				g01_data.Add (new ChartDataPoint (GlobalParameters.GRAFICO_01_EXPLORATORIA, g01.tmExploratoria));
				g01_data.Add (new ChartDataPoint (GlobalParameters.GRAFICO_01_TEMPORADA, g01.tmTemporada));
				g01_data.Add (new ChartDataPoint (GlobalParameters.GRAFICO_01_SALDO, g01.cuotaSaldo));

				labelExploratoria.Text = g01.tmExploratoria.ToString ();
				labelTemporada.Text = g01.tmTemporada.ToString ();
				labelSaldo.Text = g01.cuotaSaldo.ToString ();
			} else {
				labelExploratoria.Text = "0";
				labelTemporada.Text = "0";
				labelSaldo.Text = "0";
			}
			pie_.ItemsSource = g01_data;
			pie_chart_already_loading = false;

			//-----------


		}
			
	}
}

