using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;


namespace PesqueraXamarinForms
{
	public class HttpJsonLoader 
	{
		private string answer_json_result_ { get; set; }
		public List<dtoAnio> lanios { get; set; }
		public List<dtoZona> lzonas { get ; set; }
		public List<dtoPeriodo> lperiodos { get; set; }
		public List < dtoGrafico01 > lgrafico01 { get; set; }
		public List < dtoGrafico02 > lgrafico02 { get; set; }
		public List < dtoGrafico03 > lgrafico03 { get; set; }
		public List < dtoGrafico04 > lgrafico04 { get; set; }
		public List < dtoGrafico05 > lgrafico05 { get; set; }
		public List < dtoGrafico06 > lgrafico06 { get; set; }
		public List < dtoGrafico07 > lgrafico07 { get; set; }
		public List < dtoGrafico08 > lgrafico08 { get; set; }
		public HttpJsonLoader ()
		{
			lanios = null;
			lzonas = null;
			lperiodos = null;
			lgrafico01 = null;
			lgrafico02 = null;
			lgrafico03 = null;
			lgrafico04 = null;
			lgrafico05 = null;
			lgrafico06 = null;
			lgrafico07 = null;
			lgrafico08 = null;
		}

		public async Task< List<dtoAnio> > LoadAniosFromInternet(){
			if (lanios != null)
				return lanios;
			string url_anio = GlobalParameters.API_URL + GlobalParameters.API_ANIO;
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url_anio);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			var response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			answer_json_result_ = JsonResult.ToString ();
			lanios = JsonConvert.DeserializeObject<List<dtoAnio>>(JsonResult);
			return lanios;
		}

		public async Task< List<dtoZona> > LoadZonasFromInternet( int anoTempo ){
			string url_zona = GlobalParameters.API_URL + GlobalParameters.API_ZONA + "?" + GlobalParameters.PARAM_HTTP_YEAR + "=" + anoTempo.ToString() ;
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url_zona);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			var response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			answer_json_result_ = JsonResult.ToString ();
			lzonas = JsonConvert.DeserializeObject<List<dtoZona>>(JsonResult);
			return lzonas;
		}

		public async Task< List<dtoPeriodo> > LoadPeriodosFromInternet( int anoTempo, string codigoZona ){
			string url_periodo = GlobalParameters.API_URL + GlobalParameters.API_PERIODO;
			url_periodo += "?" + GlobalParameters.PARAM_HTTP_YEAR + "=" + anoTempo.ToString ();
			url_periodo += "&" + GlobalParameters.PARAM_HTTP_ZONA + "=" + codigoZona;
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url_periodo);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			var response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			answer_json_result_ = JsonResult.ToString ();
			lperiodos = JsonConvert.DeserializeObject<List<dtoPeriodo>>(JsonResult);
			return lperiodos;
		}

		public async Task<  dtoGrafico01  > LoadGrafico01FromInternet( int anoTempo, string codigoZona, string periodo ){
			string url_grafico01 = GlobalParameters.API_URL + GlobalParameters.API_GRAFICO_01;
			url_grafico01 += "?" + GlobalParameters.PARAM_HTTP_YEAR + "=" + anoTempo.ToString ();
			url_grafico01 += "&" + GlobalParameters.PARAM_HTTP_ZONA + "=" + codigoZona;
			url_grafico01 += "&" + GlobalParameters.PARAM_HTTP_PERIODO + "=" + periodo;	
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url_grafico01);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			var response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			answer_json_result_ = JsonResult.ToString ();
			lgrafico01 = JsonConvert.DeserializeObject<List< dtoGrafico01 >>(JsonResult);
			if (lgrafico01.Count == 0)
				return null;
			return lgrafico01[0];
		}

		public async Task< List < dtoGrafico02 > > LoadGrafico02FromInternet( int anoTempo, string codigoZona, string periodo ){
			string url_grafico02 = GlobalParameters.API_URL + GlobalParameters.API_GRAFICO_02;
			url_grafico02 += "?" + GlobalParameters.PARAM_HTTP_YEAR + "=" + anoTempo.ToString ();
			url_grafico02 += "&" + GlobalParameters.PARAM_HTTP_ZONA + "=" + codigoZona;
			url_grafico02 += "&" + GlobalParameters.PARAM_HTTP_PERIODO + "=" + periodo;   
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url_grafico02);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			var response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			answer_json_result_ = JsonResult.ToString ();
			lgrafico02 = JsonConvert.DeserializeObject<List< dtoGrafico02 >>(JsonResult);
			return lgrafico02;
		}


		public async Task< List < dtoGrafico03 > > LoadGrafico03FromInternet( int anoTempo, string codigoZona, string periodo, string codigoRegion ){
			string url_grafico03 = GlobalParameters.API_URL + GlobalParameters.API_GRAFICO_03;
			url_grafico03 += "?" + GlobalParameters.PARAM_HTTP_YEAR + "=" + anoTempo.ToString ();
			url_grafico03 += "&" + GlobalParameters.PARAM_HTTP_ZONA + "=" + codigoZona;
			url_grafico03 += "&" + GlobalParameters.PARAM_HTTP_PERIODO + "=" + periodo;
			url_grafico03 += "&" + GlobalParameters.PARAM_HTTP_REGION + "=" + codigoRegion;
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url_grafico03);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			var response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			answer_json_result_ = JsonResult.ToString ();
			lgrafico03 = JsonConvert.DeserializeObject<List< dtoGrafico03 >>(JsonResult);
			return lgrafico03;
		}

		public async Task< List < dtoGrafico04 > > LoadGrafico04FromInternet( int anoTempo, string codigoZona, 
			string periodo, string codigoRegion, string codigoPuerto ){

			string url_grafico04 = GlobalParameters.API_URL + GlobalParameters.API_GRAFICO_04;
			url_grafico04 += "?" + GlobalParameters.PARAM_HTTP_YEAR + "=" + anoTempo.ToString ();
			url_grafico04 += "&" + GlobalParameters.PARAM_HTTP_ZONA + "=" + codigoZona;
			url_grafico04 += "&" + GlobalParameters.PARAM_HTTP_PERIODO + "=" + periodo;
			url_grafico04 += "&" + GlobalParameters.PARAM_HTTP_REGION + "=" + codigoRegion;
			url_grafico04 += "&" + GlobalParameters.PARAM_HTTP_PUERTO + "=" + codigoPuerto;
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url_grafico04);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			var response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			answer_json_result_ = JsonResult.ToString ();
			lgrafico04 = JsonConvert.DeserializeObject<List< dtoGrafico04 >>(JsonResult);
			return lgrafico04;
		}
	
		public async Task< List < dtoGrafico05 > > LoadGrafico05FromInternet( int anoTempo, string codigoZona, 
			string periodo, string mes, string quincena ){

			string url_grafico05 = GlobalParameters.API_URL + GlobalParameters.API_GRAFICO_05;
			url_grafico05 += "?" + GlobalParameters.PARAM_HTTP_YEAR + "=" + anoTempo.ToString ();
			url_grafico05 += "&" + GlobalParameters.PARAM_HTTP_ZONA + "=" + codigoZona;
			url_grafico05 += "&" + GlobalParameters.PARAM_HTTP_PERIODO + "=" + periodo;
			url_grafico05 += "&" + GlobalParameters.PARAM_HTTP_MES + "=" + mes;
			url_grafico05 += "&" + GlobalParameters.PARAM_HTTP_QUINCENA + "=" + quincena;
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url_grafico05);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			var response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			answer_json_result_ = JsonResult.ToString ();
			lgrafico05 = JsonConvert.DeserializeObject<List< dtoGrafico05 >>(JsonResult);
			return lgrafico05;
		}


		public async Task< List < dtoGrafico06 > > LoadGrafico06FromInternet( int anoTempo, string codigoZona, 
			string periodo ){

			string url_grafico06 = GlobalParameters.API_URL + GlobalParameters.API_GRAFICO_06;
			url_grafico06 += "?" + GlobalParameters.PARAM_HTTP_YEAR + "=" + anoTempo.ToString ();
			url_grafico06 += "&" + GlobalParameters.PARAM_HTTP_ZONA + "=" + codigoZona;
			url_grafico06 += "&" + GlobalParameters.PARAM_HTTP_PERIODO + "=" + periodo;
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url_grafico06);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			var response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			answer_json_result_ = JsonResult.ToString ();
			lgrafico06 = JsonConvert.DeserializeObject<List< dtoGrafico06 >>(JsonResult);
			return lgrafico06;
		}

		public async Task< List < dtoGrafico07 > > LoadGrafico07FromInternet( int anoTempo, string codigoZona, 
			string periodo ){

			string url_grafico07 = GlobalParameters.API_URL + GlobalParameters.API_GRAFICO_07;
			url_grafico07 += "?" + GlobalParameters.PARAM_HTTP_YEAR + "=" + anoTempo.ToString ();
			url_grafico07 += "&" + GlobalParameters.PARAM_HTTP_ZONA + "=" + codigoZona;
			url_grafico07 += "&" + GlobalParameters.PARAM_HTTP_PERIODO + "=" + periodo;
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url_grafico07);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			var response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			answer_json_result_ = JsonResult.ToString ();
			lgrafico07 = JsonConvert.DeserializeObject<List< dtoGrafico07 >>(JsonResult);
			return lgrafico07;
		}

		public async Task< List < dtoGrafico08 > > LoadGrafico08FromInternet( int anoTempo, string codigoZona, 
			string periodo, string ranPorcen ){

			string url_grafico08 = GlobalParameters.API_URL + GlobalParameters.API_GRAFICO_08;
			url_grafico08 += "?" + GlobalParameters.PARAM_HTTP_YEAR + "=" + anoTempo.ToString ();
			url_grafico08 += "&" + GlobalParameters.PARAM_HTTP_ZONA + "=" + codigoZona;
			url_grafico08 += "&" + GlobalParameters.PARAM_HTTP_PERIODO + "=" + periodo;
			url_grafico08 += "&" + GlobalParameters.PARAM_HTTP_RANGO_PORCENTAGE + "=" + ranPorcen;

			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url_grafico08);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			var response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			answer_json_result_ = JsonResult.ToString ();
			lgrafico08 = JsonConvert.DeserializeObject<List< dtoGrafico08 >>(JsonResult);
			return lgrafico08;
		}
	}
}

