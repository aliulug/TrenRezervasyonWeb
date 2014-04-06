using System.Web;
using System.Web.SessionState;

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
			response.Write("[{ \"Ad\": \"Fatih Ekspresi\" }, { \"Ad\": \"Doğu Ekspresi\" }, { \"Ad\": \"Başkent Ekspresi\" }]");
		}
		
		private void rezervasyonYap(HttpResponse response, HttpRequest request)
		{
			response.Write("{\"Basarili\":true,\"VagonNo\":3,\"KisiSayisi\":1}");
		}

		public bool IsReusable { get; private set; }
	}
}