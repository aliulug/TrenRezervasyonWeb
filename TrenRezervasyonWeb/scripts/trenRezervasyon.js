'use strict';

var trenRezervasyonApp = angular.module('trenRezervasyonApp', []);

trenRezervasyonApp.controller('TrenRezervasyonCtrl', ['$scope', 'trSunucuIletisim', function ($scope, trSunucuIletisim)
{
    $scope.RezervasyonKapsam = new RezervasyonKapsam();
    trSunucuIletisim.trenleriAl().then(function (data)
    {
        $scope.RezervasyonKapsam.trenleriEkle(data);
    });

    $scope.rezervasyonYap = function ()
    {
        if (!$scope.RezervasyonKapsam.rezervasyonIstegiGecerli())
        {
            alert("Lütfen bilgileri giriniz");
            return;
        }

        trSunucuIletisim.rezervasyonIstegiGonder($scope.RezervasyonKapsam.istek).then(function (istekSonucu)
        {
            $scope.RezervasyonKapsam.rezervasyonSonucuIsle(istekSonucu);
        });
    };
}]);

trenRezervasyonApp.service('trSunucuIletisim', ['$http', '$q', function ($http, $q)
{
    this.http = $http;
    var erteleme = $q;

    this.trenleriAl = function ()
    {
        var kilit = erteleme.defer();
        this.http.post("/TrenleriAl.tra", kilit).success(function (data)
        {
            kilit.resolve(data);
        });
        return kilit.promise;
    };

    this.rezervasyonIstegiGonder = function (istek)
    {
        var kilit = erteleme.defer();
        this.http.post("/RezervasyonYap.tra", istek).success(function (data)
        {
            kilit.resolve(data);
        });
        return kilit.promise;
    };
}]);

function RezervasyonKapsam()
{
    this.trenler = [];
    this.rezervasyonCevabiAlindi = false;
    this.rezervasyonBasarili = false;
    this.rezYapilanVagonNo = 0;
    this.rezYapilanKisiSayisi = 0;
    this.istek = {};

    this.trenleriEkle = function (trenListesi)
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

    this.rezervasyonIstegiGecerli = function ()
    {
        if (typeof (this.istek.tren) == 'undefined' || typeof (this.istek.kisiSayisi) == 'undefined')
            return false;
        return true;
    };
}