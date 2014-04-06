using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using TrenRezervasyon.Core;

namespace TrenRezervasyonWeb.HttpHandlers
{
	public class RezervasyonHandler : IHttpHandler, IRequiresSessionState
	{
		public void ProcessRequest(HttpContext context)
		{
			istekIsle(context.Request.RawUrl, context.Response, context.Request);
		}

		private void istekIsle(string rawUrl, HttpResponse response, HttpRequest request)
		{
			if (rawUrl.Contains("TrenleriAl"))
				trenleriAl(response);
			else if (rawUrl.Contains("RezervasyonYap"))
				rezervasyonYap(response, request);
			else
				response.Write("Tanımsız istek");
		}

		private void trenleriAl(HttpResponse response)
		{
			ITrenListesiSaglayicisi trenListesiSaglayicisi = trenListesiSaglayicisiYarat();
			List<Tren> trenler = trenListesiSaglayicisi.TrenListesiAl();
			response.Write(JsonConvert.SerializeObject(trenler));
		}

		private void rezervasyonYap(HttpResponse response, HttpRequest request)
		{
			RezervasyonIstegi gelenRezervasyonIstegi = JsonConvert.DeserializeObject<RezervasyonIstegi>(new StreamReader(request.InputStream).ReadToEnd());
			BiletOfisi biletOfisi = biletOfisiYarat();
			RezervasyonIstekSonucu istekSonucu = biletOfisi.RezervasyonYap(gelenRezervasyonIstegi);
			response.Write(JsonConvert.SerializeObject(istekSonucu));
		}

		private BiletOfisi biletOfisiYarat()
		{
			return new BiletOfisi(trenListesiSaglayicisiYarat(), biletlemeMotoruYarat());
		}

		private IBiletlemeMotoru biletlemeMotoruYarat()
		{
			return new BiletlemeMotoru();
		}

		private TCDDTrenListesiServisi trenListesiSaglayicisiYarat()
		{
			return new TCDDTrenListesiServisi();
		}

		public bool IsReusable { get; private set; }
	}
}