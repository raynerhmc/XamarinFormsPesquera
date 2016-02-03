using System;

namespace PesqueraXamarinForms
{
	public class GlobalParameters
	{
		public GlobalParameters ()
		{
		}
		public static int LABEL_TEXT_SIZE_15_ = 13;
		public static int MAIN_LAYOUT_PADDING_ = 10;
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


		public static string[] LIST_MESES = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", 
			"Junio", "Julio", "Agosto", "Setiembre", "Octubre", 
			"Noviembre", "Diciembre"
		};
		public static string[] LIST_COD_MESES = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

		public static string[] LIST_QUINCENAS = { "1°", "2°" };
		public static string[] LIST_COD_QUINCENAS = { "1", "2" };

		public static int[] LIST_ANIOS = { 2014, 2015, 2016 };

		public static string[] LIST_PERIODOS = { "I", "II" };

		public static string[] LIST_ZONAS = { "Norte y Centro", "Sur" };

		public static string[] LIST_REGIONES = { "Piura", "La Libertad", "Ancash", "Lima", "Callao", "Ica" };

		public static string[] LIST_PUERTOS = { "Carquin", "Chancay", "Supe", "Vegueta" };

	}
}

