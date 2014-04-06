using System.Collections.Generic;

namespace TrenRezervasyon.Core
{
	public class TCDDTrenListesiServisi : ITrenListesiSaglayicisi
	{
		public List<Tren> TrenListesiAl()
		{
			//burada gerçekten TCDD sistemine bağlanıp trenlerin listesini alan kodu yazmamız gerekiyor
			return new List<Tren> { new Tren { Ad = "Başkent Ekspr." }, new Tren { Ad = "Fatih Ekspr." }, new Tren { Ad = "Doğu Ekspr." } };
		}
	}
}
