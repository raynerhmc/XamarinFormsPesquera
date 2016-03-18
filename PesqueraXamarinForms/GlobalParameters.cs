using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PesqueraXamarinForms
{
	public class GlobalParameters
	{
		public GlobalParameters ()
		{
		}
		public static int LABEL_TEXT_SIZE_15_ = 12;
		public static int MAIN_LAYOUT_PADDING_ = 10;

		public static int LOGIN_NUMBER_OF_TRIES = 3;

		public static string LOGIN_USERNAME_KEY = "LOGIN_USERNAME";
		public static string LOGIN_PASSWORD_KEY = "LOGIN_PASSWORD";
		public static string LOGIN_SAVELOGIN_KEY = "LOGIN_SAVE_LOGIN";

		public static string API_URL = "http://apptemporadapesca.produce.gob.pe/api/";
		public static string API_ZONA = "Zona";
		public static string API_ANIO = "Anio";
		public static string API_PERIODO = "Periodo";
		public static string API_REGION = "Region";
		public static string API_PUERTO = "Puerto";

		public static string API_GRAFICO_01 = "Grafico01";

		public static string GRAFICO_01_EXPLORATORIA = "Exploratoria";
		public static string GRAFICO_01_TEMPORADA = "Temporada";
		public static string GRAFICO_01_SALDO = "Saldo";


		public static string API_GRAFICO_02 = "Grafico02";
		public static string API_GRAFICO_03 = "Grafico03";
		public static string API_GRAFICO_04 = "Grafico04";
		public static string API_GRAFICO_05 = "Grafico05";
		public static string API_GRAFICO_06 = "Grafico06";
		public static string API_GRAFICO_07 = "Grafico07";
		public static string API_GRAFICO_08 = "Grafico08";
		public static string API_GRAFICO_09 = "Grafico09";
		public static string API_GRAFICO_10 = "Grafico10";

		public static string PARAM_HTTP_YEAR = "Anotempo";
		public static string PARAM_HTTP_ZONA = "codigoZona";
		public static string PARAM_HTTP_PERIODO = "periodo";
		public static string PARAM_HTTP_REGION = "codigoRegion";
		public static string PARAM_HTTP_PUERTO = "codigoPuerto";
		public static string PARAM_HTTP_MES = "mes";
		public static string PARAM_HTTP_QUINCENA = "quincena";
		public static string PARAM_HTTP_RANGO_PORCENTAGE = "ranPorcen";


		public static string[] LIST_MESES = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", 
			"Junio", "Julio", "Agosto", "Setiembre", "Octubre", 
			"Noviembre", "Diciembre"
		};
		public static string[] LIST_COD_MESES = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

		public static string[] LIST_QUINCENAS = { "1°", "2°" };
		public static string[] LIST_COD_QUINCENAS = { "1", "2" };

		public static string[] LIST_RANGO_PORCENTAGES = { "[0-50]", "[51-80]", "[81-90]", "[91-100]", "[P>=101]" };
		public static string[] LIST_COD_RANGO_PORCENTAGES = { "[0-50]", "[51-80]", "[81-90]", "[91-100]", "[101-más]" };


		/* 
		 * We declare these lists for testing purposes, 
		 * they are not referenced in any other class anymore. 
		 * Thus, they can be safely removed 
		*/
		public static int[] LIST_ANIOS = { 2014, 2015, 2016 };

		public static string[] LIST_PERIODOS = { "I", "II" };

		public static string[] LIST_ZONAS = { "Norte y Centro", "Sur" };

		public static string[] LIST_REGIONES = { "Piura", "La Libertad", "Ancash", "Lima", "Callao", "Ica" };

		public static string[] LIST_PUERTOS = { "Carquin", "Chancay", "Supe", "Vegueta" };

		/////////////// UNTIL HERE ///////////////
		/// 
		/// Lista de Colores que teine cada gràfica

		public static List<Color> COLORS_GRAPHIC01 = new List<Color>(){Color.Blue, Color.FromHex("#FF9800"), Color.Silver };

		public static List<Color> COLORS_GRAPHIC02 = new List<Color> (){ Color.Blue };

		public static List<Color> COLORS_GRAPHIC03 = new List<Color> (){ Color.Blue };

		public static List<Color> COLORS_GRAPHIC04 = new List<Color> (){ Color.Blue };

		public static List<Color> COLORS_GRAPHIC05 = new List<Color> (){ Color.Blue };

		public static List<Color> COLORS_GRAPHIC06 = new List<Color> (){ Color.Blue };

		public static List<Color> COLORS_GRAPHIC07 = new List<Color> (){ Color.Blue, Color.Green, Color.Yellow, Color.FromHex("#FF9800"),Color.FromHex("#EF5350")};

		public static List<Color> COLORS_GRAPHIC08 = new List<Color> (){ Color.Blue };

		/// Tamaño de los titulos de los Picker

		public static float SCALE_PICKER = 0.8f;

		public static float WIDTH_PICKER_ANIO = 60.0f;

		public static float WIDTH_PICKER_ZONE = 70.0f;

		public static float WIDTH_PICKER_PERIODO = 45.0f;

		public static float WIDTH_PICKER_REGION = 120.0f;

		public static float WIDTH_PICKER_RANGO = 100.0f;

		/// START ANGLE PIE SERIES 

		public static int START_ANGLE_PIE_SERIE = 175;
		public static int END_ANGLE_PIE_SERIE = 360 + START_ANGLE_PIE_SERIE;

		public static int LEGEND_TEXT_SIZE_SERIES_ = 12;
	}
}

