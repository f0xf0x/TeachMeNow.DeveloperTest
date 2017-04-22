(function () {
    'use strict';

    angular
        .module("app")
        .factory('userService', factory);

    factory.$inject = ['$http', 'baseUrl'];

    function factory($http, baseUrl) {
        var service = {
            getUsers: getUsers,
            getPartners: getPartners
        };
        var url = baseUrl + '/api/users';

        function getUsers() {
            return $http.get(url);
        }

        function getPartners() {
            return $http.get(url, {
                params:{onlyPartners:true}
            });
        }
        return service;
    }
})();