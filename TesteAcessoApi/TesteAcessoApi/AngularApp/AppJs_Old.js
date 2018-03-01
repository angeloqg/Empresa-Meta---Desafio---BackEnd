/// <reference path="C:\Users\angel\Desktop\testeAcessoWepAPI\TesteAcessoApi\TesteAcessoApi\Scripts/angular.js" />
/// <reference path="C:\Users\angel\Desktop\testeAcessoWepAPI\TesteAcessoApi\TesteAcessoApi\Scripts/jquery-1.9.1.js" />

var app = angular.module("MyApp", []);

app.controller("MyCtrl", function ($scope,$http) {

    var novo = {
        nome: "Marcelo Paiva",
        canal: "Jornal",
        valor: "Baixo",
        obs: "Pouco Importante"
    }
    /*
    $http.post("http://localhost:16963/components/schemas/ContatoCreate", novo).success(function (data) {
        console.log(data);        
    });*/

    var id = 3;

    var alterar = {
        idContato:3,
        nome: "Romualdo Pessanha",
        canal: "Folheto",
        valor: "Baixo",
        obs: "Sem comentátários"
    }
    /*
    $http.put("http://localhost:16963/components/schemas/ContatoUpdate?id=" + id, alterar).success(function (data) {
        console.log(data);
    });
    */
    var id;
   /* $http.delete("http://localhost:16963/components/schemas/Contato/" + id).success(function (data) {
        console.log(data);
        $scope.valor = data;
    });*/

    $http.get("http://localhost:16963/components/schemas/Contato").success(function (data) {
        console.log(data);
        $scope.resultado = data;
    });
   
    id = 3;
    $http.get("http://localhost:16963/components/schemas/Contato/" + id).success(function (data) {
        console.log(data);
        $scope.valor = data;
    });

    /*
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": "http://localhost:1586/api/Todo/",
        "method": "POST",
        "headers": {
            "content-type": "application/json",
            "cache-control": "no-cache",
            "postman-token": "8b28b726-5ae7-0110-0439-ce95189bc118"
        },
        "processData": false,
        "data": "{\n\t\"Name\":\"iara\",\n\t\"IsComplete\":true\n}"
    }

    $.ajax(settings).done(function (response) {
        console.log(response);
    });*/
});