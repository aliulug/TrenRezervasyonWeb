using System.Collections.Generic;

namespace TrenRezervasyon.Core
{
	public class TCDDTrenListesiServisi : ITrenListesiSaglayicisi
	{
		public List<Tren> TrenListesiAl()
		{
			//burada gerçekten TCDD sistemine bağlanıp trenlerin listesini alan kodu yazmamız gerekiyor
			Tren baskent = new Tren { Ad = "Başkent Ekspr.", Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 10, DoluKoltukSayisi = 0, VagonNo = 1 } } };
			Tren fatih = new Tren { Ad = "Fatih Ekspr.", Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 5, DoluKoltukSayisi = 5, VagonNo = 1 } } };
			Tren dogu = new Tren { Ad = "Doğu Ekspr.", Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 3, DoluKoltukSayisi = 7, VagonNo = 1 } } };
			return new List<Tren> { baskent, fatih, dogu };
		}
	}
}
