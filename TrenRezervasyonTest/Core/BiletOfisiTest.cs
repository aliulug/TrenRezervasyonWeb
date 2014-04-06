// ReSharper disable InconsistentNaming
using TrenRezervasyon.Core;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace TrenRezervasyonTest.Core
{
	[TestFixture]
	public class BiletOfisiTest
	{
		private ITrenListesiSaglayicisi _trenListesiSaglayicisi;
		private BiletOfisi _biletOfisi;
		private readonly Tren _doguEkspresi = new Tren { Ad = "Doğu" };
		private readonly Tren _baskentEkspresi = new Tren { Ad = "Başkent" };
		private readonly Tren _fatihEkspresi = new Tren { Ad = "Fatih" };

		[SetUp]
		public void TestIlkAyarlar()
		{
			_trenListesiSaglayicisi = Substitute.For<ITrenListesiSaglayicisi>();
			_biletOfisi = new BiletOfisi(_trenListesiSaglayicisi);
		}

		[Test]
		public void SifirTrenVarken_TrenListesiIstendiginde_SifirTrenIcerenListeDonmeli()
		{
			//given
			_trenListesiSaglayicisi.TrenListesiAl().Returns(new List<Tren>());
			//when
			List<Tren> trenler = _biletOfisi.TrenListesiAl();
			//then
			Assert.That(trenler.Count, Is.EqualTo(0));
		}

		[Test]
		public void IkiTrenVarken_TrenListesiIstendiginde_IkiTrenIcerenListeDonmeli()
		{
			//given
			_trenListesiSaglayicisi.TrenListesiAl().Returns(new List<Tren> { new Tren(), new Tren() });
			//when
			List<Tren> trenler = _biletOfisi.TrenListesiAl();
			//then
			Assert.That(trenler.Count, Is.EqualTo(2));
		}

		[Test]
		public void UcTrenVarken_TrenListesiIstendiginde_UcTrenIcerenListeDonmeliVeTrenAdlariAyniOlmali()
		{
			//given
			_trenListesiSaglayicisi.TrenListesiAl().Returns(new List<Tren> { _fatihEkspresi, _baskentEkspresi, _doguEkspresi });
			//when
			List<Tren> trenler = _biletOfisi.TrenListesiAl();
			//then
			Assert.That(trenler.Count, Is.EqualTo(3));
			Assert.That(trenler[0].Ad, Is.EqualTo("Fatih"));
			Assert.That(trenler[1].Ad, Is.EqualTo("Başkent"));
			Assert.That(trenler[2].Ad, Is.EqualTo("Doğu"));
		}
	}
}
// ReSharper restore InconsistentNaming