using System.Collections.Generic;

namespace TrenRezervasyon.Core
{
	public class BiletOfisi
	{
		private readonly ITrenListesiSaglayicisi _trenListesiSaglayicisi;

		public BiletOfisi(ITrenListesiSaglayicisi trenListesiSaglayicisi)
		{
			_trenListesiSaglayicisi = trenListesiSaglayicisi;
		}

		public List<Tren> TrenListesiAl()
		{
			return _trenListesiSaglayicisi.TrenListesiAl();
		}
	}
}