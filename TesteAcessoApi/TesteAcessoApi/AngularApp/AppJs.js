/// <reference path="C:\Users\angel\Desktop\testeAcessoWepAPI\TesteAcessoApi\TesteAcessoApi\Scripts/angular.js" />
/// <reference path="C:\Users\angel\Desktop\testeAcessoWepAPI\TesteAcessoApi\TesteAcessoApi\Scripts/jquery-1.9.1.js" />

var app = angular.module("MyApp", []);

app.controller("MyCtrl", function ($scope, $http) {

    // -- Declaração de variáveis
    var pagina;
    var tamanho;
    var idSelecionado;

    // -- Variáveis de escopo
    $scope.exibir = false;
    $scope.excluir = false;
    $scope.estilo = null;

    $scope.nome = null;
    $scope.canal = null;
    $scope.valor = null;

    // -- Carrega dados do formulário
    carregaDados($http, $scope, pagina, tamanho);

    // -- Function Local
    $scope.filtrar = function () {
        pagina = $("#pagina").val();
        tamanho = $("#tamanho").val();

        carregaDados($http, $scope, pagina, tamanho);
    }

    $scope.limparFiltro = function () {
        pagina = null;
        tamanho = null;
        $("#pagina").val('0');
        $("#tamanho").val('0');

        carregaDados($http, $scope, pagina, tamanho);
    }

    $scope.editar = function (objEdicao) {
        $scope.exibir = true;
        $scope.excluir = true;

        idSelecionado = objEdicao.idContato;

        $scope.nome = objEdicao.nome;
        $scope.canal = objEdicao.canal;
        $scope.valor = objEdicao.valor;

        $("#nome").val(objEdicao.nome);
        $("#canal").val(objEdicao.canal);
        $("#valor").val(objEdicao.valor);
        $("#obs").val(objEdicao.obs);
    }

    $scope.cancelar = function () {
        $scope.exibir = false;
        $scope.excluir = false;

        idSelecionado = null;

        $scope.nome = null;
        $scope.canal = null;
        $scope.valor = null;

        $("#nome").val('');
        $("#canal").val('');
        $("#valor").val('');
        $("#obs").val('');
    }

    $scope.avisoAlteracao = function () {

       $scope.estilo = {
            'background-color': '#ffa566',
            'border-radius': '5px 5px 0px 0px'
       }

       $('#popupAviso').modal('show');
    }

    $scope.avisoExclusao = function (obj) {

        idSelecionado = obj.idContato;

        $scope.estilo = {
            'background-color': '#ff5f5f',
            'border-radius': '5px 5px 0px 0px'
        }
       
        $('#popupAviso').modal('show');
    }

    // -- Function Local - Alteração de dados
    $scope.executarInclusao = function () {
 
        var novo = {};

        novo.nome = $("#nome").val();
        novo.canal = $("#canal").val();
        novo.valor = $("#valor").val();
        novo.obs = $("#obs").val();

        $http.post("http://localhost:16963/components/schemas/ContatoCreate", novo).success(function (data) {
            console.log(data);
            $scope.resultado = data.contatos;
           
            controleMensagem($scope, data.status, data.mensagem)
        });

        $scope.cancelar();
    }

    $scope.executarAlteracao = function () {

        $('#popupAviso').modal('hide');

        var alterar = {};

        alterar.idContato = idSelecionado;
        alterar.nome = $("#nome").val();
        alterar.canal = $("#canal").val();
        alterar.valor = $("#valor").val();
        alterar.obs = $("#obs").val();       

        var id = idSelecionado;

        $http.put("http://localhost:16963/components/schemas/ContatoUpdate?id=" + id, alterar).success(function (data) {
            $scope.resultado = data.contatos;
            controleMensagem($scope, data.status, data.mensagem)
        });

        $scope.cancelar();
    }

    $scope.executarExclusao = function () {

        $('#popupAviso').modal('hide');

        var id = idSelecionado;
         
        $http.delete("http://localhost:16963/components/schemas/Contato/" + id).success(function (data) {
            console.log(data);
            $scope.resultado = data.contatos;

            controleMensagem($scope, data.status, data.mensagem)
        });

        $scope.cancelar();
    }
});

// Function Global
function carregaDados($http, $scope, pagina, tamanho) {
    $http.get("http://localhost:16963/components/schemas/Contato?page=" + pagina + "&size=" + tamanho).success(function (data) {
        $scope.resultado = data;
    });
}

function controleMensagem($scope, status, mensagem) {

    $scope.ocultar = true;

    if (status) {
        
        $scope.estiloMensagem = {
            'background-color': '#7ddd54',
            'border-radius': '5px 5px 0px 0px'
        }
        $('#popupMensagem').modal('show');
        $("#mensagem").text(mensagem);
    }

    if (!status) {
        $scope.status = false;
        $scope.estiloMensagem = {
            'background-color': '#f77575',
            'border-radius': '5px 5px 0px 0px'
        }
        $('#popupMensagem').modal('show');
        $("#mensagem").text(mensagem);
    }
}