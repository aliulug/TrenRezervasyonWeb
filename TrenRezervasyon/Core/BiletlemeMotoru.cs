namespace TrenRezervasyon.Core
{
	public class BiletlemeMotoru : IBiletlemeMotoru
	{
		private const double TrenIcinMaksimumDolulukOrani = 0.7;

		public RezervasyonIstekSonucu RezervasyonIstegiIsle(Tren tren, RezervasyonIstegi istek)
		{
			if (yeniRezervasyonTrenKapasitesiniAsiyor(tren, istek))
				return new RezervasyonIstekSonucu{Basarili = false};
			return new RezervasyonIstekSonucu { Basarili = true, KisiSayisi = istek.KisiSayisi, TrenAdi = istek.TrenAdi, VagonNo = 1};
		}

		private bool yeniRezervasyonTrenKapasitesiniAsiyor(Tren tren, RezervasyonIstegi istek)
		{
			int trendekiToplamKoltukSayisi = 0;
			int trendekiToplamDoluKoltukSayisi = 0;
			foreach (Vagon vagon in tren.Vagonlar)
			{
				trendekiToplamKoltukSayisi += vagon.BosKoltukSayisi + vagon.DoluKoltukSayisi;
				trendekiToplamDoluKoltukSayisi += vagon.DoluKoltukSayisi;
			}
			return (trendekiToplamDoluKoltukSayisi + istek.KisiSayisi) / (double)trendekiToplamKoltukSayisi > TrenIcinMaksimumDolulukOrani;
		}
	}
}
