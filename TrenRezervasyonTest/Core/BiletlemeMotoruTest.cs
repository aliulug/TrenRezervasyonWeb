// ReSharper disable InconsistentNaming

using TrenRezervasyon.Core;
using System.Collections.Generic;
using NUnit.Framework;

namespace TrenRezervasyonTest.Core
{
	[TestFixture]
	public class BiletlemeMotoruTest
	{
		[Test]
		public void OnKisilikTrenBos_BirKisilikRezervasyonIsteniyor_RezervasyonBasarili()
		{
			BiletlemeMotoru biletlemeMotoru = new BiletlemeMotoru();
			Tren tren = new Tren { Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 10, DoluKoltukSayisi = 0 } } };
			RezervasyonIstekSonucu sonuc = biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 1 });
			Assert.That(sonuc.Basarili, Is.EqualTo(true));
		}

		[Test]
		public void OnKisilikTrendeAltiKisiVar_BirKisilikRezervasyonIsteniyor_RezervasyonBasarili()
		{
			BiletlemeMotoru biletlemeMotoru = new BiletlemeMotoru();
			Tren tren = new Tren { Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 4, DoluKoltukSayisi = 6 } } };
			RezervasyonIstekSonucu sonuc = biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 1 });
			Assert.That(sonuc.Basarili, Is.EqualTo(true));
		}

		[Test]
		public void OnKisilikTrendeYediKisiVar_BirKisilikRezervasyonIsteniyor_RezervasyonBasarisiz()
		{
			BiletlemeMotoru biletlemeMotoru = new BiletlemeMotoru();
			Tren tren = new Tren { Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 2, DoluKoltukSayisi = 7 } } };
			RezervasyonIstekSonucu sonuc = biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 1 });
			Assert.That(sonuc.Basarili, Is.EqualTo(false));
		}

		[Test]
		public void YuzKisilikIkiVagonBiri70Biri60Dolu_OnKisilikRezervasyonısteniyor_RezervasyonBasarili()
		{
			BiletlemeMotoru biletlemeMotoru = new BiletlemeMotoru();
			Tren tren = new Tren { Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 30, DoluKoltukSayisi = 70 }, new Vagon { BosKoltukSayisi = 40, DoluKoltukSayisi = 60 } } };
			RezervasyonIstekSonucu sonuc = biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 10 });
			Assert.That(sonuc.Basarili, Is.EqualTo(true));
		}

		[Test]
		public void YuzKisilikIkiVagonBiri70Biri61Dolu_OnKisilikRezervasyonısteniyor_RezervasyonBasarisiz()
		{
			BiletlemeMotoru biletlemeMotoru = new BiletlemeMotoru();
			Tren tren = new Tren { Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 30, DoluKoltukSayisi = 70 }, new Vagon { BosKoltukSayisi = 39, DoluKoltukSayisi = 61 } } };
			RezervasyonIstekSonucu sonuc = biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 10 });
			Assert.That(sonuc.Basarili, Is.EqualTo(false));
		}
	}
}
// ReSharper restore InconsistentNaming