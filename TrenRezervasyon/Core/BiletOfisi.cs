using System.Collections.Generic;

namespace TrenRezervasyon.Core
{
	public class BiletOfisi
	{
		private readonly ITrenListesiSaglayicisi _trenListesiSaglayicisi;
		private readonly IBiletlemeMotoru _biletlemeMotoru;

		public BiletOfisi(ITrenListesiSaglayicisi trenListesiSaglayicisi, IBiletlemeMotoru biletlemeMotoru)
		{
			_trenListesiSaglayicisi = trenListesiSaglayicisi;
			_biletlemeMotoru = biletlemeMotoru;
		}

		public List<Tren> TrenListesiAl()
		{
			return _trenListesiSaglayicisi.TrenListesiAl();
		}

		public RezervasyonIstekSonucu RezervasyonYap(RezervasyonIstegi istek)
		{
			List<Tren> trenler = TrenListesiAl();
			foreach (Tren tren in trenler)
				if (tren.Ad == istek.TrenAdi)
					return _biletlemeMotoru.RezervasyonIstegiIsle(tren, istek);
			return new RezervasyonIstekSonucu();
		}
	}
}