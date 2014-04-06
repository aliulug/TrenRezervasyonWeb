'use strict';

var trenRezervasyonApp = angular.module('trenRezervasyonApp', []);

trenRezervasyonApp.controller('TrenRezervasyonCtrl', ['$scope','trSunucuIletisim', function ($scope, trSunucuIletisim)
{
    $scope.RezervasyonKapsam = new RezervasyonKapsam();
    $scope.RezervasyonKapsam.trenleriEkle(trSunucuIletisim.trenleriAl());

    $scope.rezervasyonYap = function()
    {
        if (typeof ($scope.trenAdi) == 'undefined' || typeof ($scope.kisiSayisi) == 'undefined')
        {
            alert("Lütfen tren ve kişi sayısı seçiniz");
            return;
        }

        var rezervasyonIstegiSonuc = trSunucuIletisim.rezervasyonIstegiGonder($scope.trenAdi, $scope.kisiSayisi);
        $scope.RezervasyonKapsam.rezervasyonSonucuIsle(rezervasyonIstegiSonuc);
    };
}]);

trenRezervasyonApp.service('trSunucuIletisim', [function ()
{
    this.trenleriAl = function ()
    {
        return [{ "Ad": "Fatih Ekspresi" }, { "Ad": "Doğu Ekspresi" }, { "Ad": "Başkent Ekspresi" }];
    };

    this.rezervasyonIstegiGonder = function (trenAdi, kisiSayisi)
    {
        return { "Basarili": true, "VagonNo": 5, "KisiSayisi": kisiSayisi };
    };
}]);

function RezervasyonKapsam()
{
    this.trenler = [];
    this.rezervasyonCevabiAlindi = false;
    this.rezervasyonBasarili = false;
    this.rezYapilanVagonNo = 0;
    this.rezYapilanKisiSayisi = 0;

    this.trenleriEkle = function(trenListesi)
    {
        this.trenler = trenListesi;
    };

    this.rezervasyonSonucuIsle = function (sunucudanGelenCevap)
    {
        this.rezervasyonCevabiAlindi = true;
        this.rezervasyonBasarili = sunucudanGelenCevap.Basarili;
        if (this.rezervasyonBasarili)
        {
            this.rezYapilanVagonNo = sunucudanGelenCevap.VagonNo;
            this.rezYapilanKisiSayisi = sunucudanGelenCevap.KisiSayisi;
        }
    };
}