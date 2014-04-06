namespace TrenRezervasyon.Core
{
	public class BiletlemeMotoru : IBiletlemeMotoru
	{
		private Tren _tren;
		private RezervasyonIstegi _istek;
		private const double TrenIcinMaksimumDolulukOrani = 0.7;
		private const double VagonIcinMaksimumDolulukOrani = 0.7;

		public RezervasyonIstekSonucu RezervasyonIstegiIsle(Tren tren, RezervasyonIstegi istek)
		{
			_tren = tren;
			_istek = istek;
			RezervasyonIstekSonucu basarisizSonuc = new RezervasyonIstekSonucu();
			if (yeniRezervasyonTrenKapasitesiniAsiyor(istek))
				return basarisizSonuc;
			Vagon rezervasyonYapilacakVagon = rezervasyonYapilacakVagonBul();
			if (rezervasyonYapilacakVagon == null)
				return basarisizSonuc;
			return new RezervasyonIstekSonucu { Basarili = true, VagonNo = rezervasyonYapilacakVagon.VagonNo, KisiSayisi = istek.KisiSayisi, TrenAdi = istek.TrenAdi};
		}

		private Vagon rezervasyonYapilacakVagonBul()
		{
			Vagon rahatYerOlanVagon = rahatYerOlanVagonBul();
			if (rahatYerOlanVagon != null)
				return rahatYerOlanVagon;
			return yerOlanVagonBul();
		}

		private Vagon yerOlanVagonBul()
		{
			foreach (Vagon vagon in _tren.Vagonlar)
			{
				int vagonToplamKoltukAdedi = vagon.BosKoltukSayisi + vagon.DoluKoltukSayisi;
				if (vagon.DoluKoltukSayisi + _istek.KisiSayisi <= vagonToplamKoltukAdedi)
					return vagon;
			}
			return null;
		}

		private Vagon rahatYerOlanVagonBul()
		{
			foreach (Vagon vagon in _tren.Vagonlar)
			{
				int vagonToplamKoltukAdedi = vagon.BosKoltukSayisi + vagon.DoluKoltukSayisi;
				if ((vagon.DoluKoltukSayisi + _istek.KisiSayisi) / (double)vagonToplamKoltukAdedi <= VagonIcinMaksimumDolulukOrani)
					return vagon;
			}
			return null;
		}

		private bool yeniRezervasyonTrenKapasitesiniAsiyor(RezervasyonIstegi istek)
		{
			int trendekiToplamKoltukSayisi = 0;
			int trendekiToplamDoluKoltukSayisi = 0;
			foreach (Vagon vagon in _tren.Vagonlar)
			{
				trendekiToplamKoltukSayisi += vagon.BosKoltukSayisi + vagon.DoluKoltukSayisi;
				trendekiToplamDoluKoltukSayisi += vagon.DoluKoltukSayisi;
			}
			return (trendekiToplamDoluKoltukSayisi + istek.KisiSayisi) / (double)trendekiToplamKoltukSayisi > TrenIcinMaksimumDolulukOrani;
		}
	}
}