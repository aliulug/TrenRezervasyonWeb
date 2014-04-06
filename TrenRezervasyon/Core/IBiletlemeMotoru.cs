namespace TrenRezervasyon.Core
{
	public interface IBiletlemeMotoru
	{
		RezervasyonIstekSonucu RezervasyonIstegiIsle(Tren tren, RezervasyonIstegi istek);
	}
}