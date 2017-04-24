(function () {
    'use strict';

    angular
        .module("app")
        .factory('accountService', factory);

    factory.$inject = ['$http', 'baseUrl'];

    function factory($http, baseUrl) {
        var service = {
            createAccount: createAccount
        };


        var url = baseUrl + '/api/account';
        function createAccount(cm) {
            var res = $http.post(url, cm);
            return res;
        }
        return service;
    }
})();