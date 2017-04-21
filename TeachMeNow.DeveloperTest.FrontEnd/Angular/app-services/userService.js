(function () {
    'use strict';

    angular
        .module("app")
        .factory('userService', factory);

    factory.$inject = ['$http', 'baseUrl'];

    function factory($http, baseUrl) {
        var service = {
            getUsers: getUsers
        };

        return service;

        function getUsers() {
            return $http.get(baseUrl + 'api/users');
        }
    }
})();