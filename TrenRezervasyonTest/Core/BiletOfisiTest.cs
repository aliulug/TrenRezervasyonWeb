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

		[SetUp]
		public void TestIlkAyarlar()
		{
			_trenListesiSaglayicisi = Substitute.For<ITrenListesiSaglayicisi>();
			_biletOfisi = new BiletOfisi(_trenListesiSaglayicisi);
		}

		[Test]
		public void SifirTrenVarken_TrenListesiIstendiginde_SifirTrenIcerenListeDonmeli()
		{
			_trenListesiSaglayicisi.TrenListesiAl().Returns(new List<Tren>());
			List<Tren> trenler = _biletOfisi.TrenListesiAl();
			Assert.That(trenler.Count, Is.EqualTo(0));
		}

		[Test]
		public void IkiTrenVarken_TrenListesiIstendiginde_IkiTrenIcerenListeDonmeli()
		{
			_trenListesiSaglayicisi.TrenListesiAl().Returns(new List<Tren> { new Tren(), new Tren() });
			List<Tren> trenler = _biletOfisi.TrenListesiAl();
			Assert.That(trenler.Count, Is.EqualTo(2));
		}

		[Test]
		public void UcTrenVarken_TrenListesiIstendiginde_UcTrenIcerenListeDonmeliVeTrenAdlariAyniOlmali()
		{
			_trenListesiSaglayicisi.TrenListesiAl().Returns(new List<Tren> { new Tren { Ad = "Kırmızı" }, new Tren { Ad = "Mavi" }, new Tren { Ad = "Yeşil" } });
			List<Tren> trenler = _biletOfisi.TrenListesiAl();
			Assert.That(trenler.Count, Is.EqualTo(3));
			Assert.That(trenler[0].Ad, Is.EqualTo("Kırmızı"));
			Assert.That(trenler[1].Ad, Is.EqualTo("Mavi"));
			Assert.That(trenler[2].Ad, Is.EqualTo("Yeşil"));
		}
	}
}
// ReSharper restore InconsistentNaming