// ReSharper disable InconsistentNaming
using TrenRezervasyon.Core;
using System.Collections.Generic;
using NUnit.Framework;

namespace TrenRezervasyonTest.Core
{
	[TestFixture]
	public class BiletlemeMotoruTest
	{
		private BiletlemeMotoru _biletlemeMotoru;

		[SetUp]
		public void TestIlkAyarlar()
		{
			_biletlemeMotoru = new BiletlemeMotoru();
		}

		[Test]
		public void OnKisilikTrenBos_BirKisilikRezervasyonIsteniyor_RezervasyonBasarili()
		{
			Tren tren = new Tren { Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 10, DoluKoltukSayisi = 0 } } };
			RezervasyonIstekSonucu sonuc = _biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 1 });
			Assert.That(sonuc.Basarili, Is.EqualTo(true));
		}

		[Test]
		public void OnKisilikTrendeAltiKisiVar_BirKisilikRezervasyonIsteniyor_RezervasyonBasarili()
		{
			Tren tren = new Tren { Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 4, DoluKoltukSayisi = 6 } } };
			RezervasyonIstekSonucu sonuc = _biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 1 });
			Assert.That(sonuc.Basarili, Is.EqualTo(true));
		}

		[Test]
		public void OnKisilikTrendeYediKisiVar_BirKisilikRezervasyonIsteniyor_RezervasyonBasarisiz()
		{
			Tren tren = new Tren { Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 2, DoluKoltukSayisi = 7 } } };
			RezervasyonIstekSonucu sonuc = _biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 1 });
			Assert.That(sonuc.Basarili, Is.EqualTo(false));
		}

		[Test]
		public void YuzKisilikIkiVagonBiri70Biri60Dolu_OnKisilikRezervasyonIsteniyor_RezervasyonBasarili()
		{
			Tren tren = new Tren { Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 30, DoluKoltukSayisi = 70 }, new Vagon { BosKoltukSayisi = 40, DoluKoltukSayisi = 60 } } };
			RezervasyonIstekSonucu sonuc = _biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 10 });
			Assert.That(sonuc.Basarili, Is.EqualTo(true));
		}

		[Test]
		public void YuzKisilikIkiVagonBiri70Biri61Dolu_OnKisilikRezervasyonIsteniyor_RezervasyonBasarisiz()
		{
			Tren tren = new Tren { Vagonlar = new List<Vagon> { new Vagon { BosKoltukSayisi = 30, DoluKoltukSayisi = 70 }, new Vagon { BosKoltukSayisi = 39, DoluKoltukSayisi = 61 } } };
			RezervasyonIstekSonucu sonuc = _biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 10 });
			Assert.That(sonuc.Basarili, Is.EqualTo(false));
		}

		[Test]
		public void YuzKisilikOnVagonVarHepsi50Dolu_60KisilikRezervasyonIsteniyor_RezervasyonBasarisiz()
		{
			Tren tren = new Tren();
			for (int i = 0; i < 10; i++)
				tren.Vagonlar.Add(new Vagon { BosKoltukSayisi = 50, DoluKoltukSayisi = 50 });
			RezervasyonIstekSonucu sonuc = _biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 60 });
			Assert.That(sonuc.Basarili, Is.EqualTo(false));
		}

		[Test]
		public void YuzKisilikOnVagonVar9u65KisiDoluSonuncusu10KisiDolu_6KisilikRezervasyonIsteniyor_RezervasyonSonVagonaYapilir()
		{
			Tren tren = new Tren();
			for (int i = 0; i < 9; i++)
				tren.Vagonlar.Add(new Vagon { BosKoltukSayisi = 35, DoluKoltukSayisi = 65, VagonNo = (i + 1) });
			tren.Vagonlar.Add(new Vagon { BosKoltukSayisi = 90, DoluKoltukSayisi = 10, VagonNo = 10 });
			RezervasyonIstekSonucu sonuc = _biletlemeMotoru.RezervasyonIstegiIsle(tren, new RezervasyonIstegi { KisiSayisi = 6 });
			Assert.That(sonuc.Basarili, Is.EqualTo(true));
			Assert.That(sonuc.VagonNo, Is.EqualTo(10));
		}
	}
}
// ReSharper restore InconsistentNaming