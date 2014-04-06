namespace TrenRezervasyon.Core
{
	public class BiletlemeMotoru : IBiletlemeMotoru
	{
		public RezervasyonIstekSonucu RezervasyonIstegiIsle(Tren tren, RezervasyonIstegi istek)
		{
			return new RezervasyonIstekSonucu {Basarili = true, VagonNo = 1, KisiSayisi = istek.KisiSayisi, TrenAdi = istek.TrenAdi};
		}
	}
}
