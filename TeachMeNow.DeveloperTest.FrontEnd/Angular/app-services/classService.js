(function () {
    'use strict';

    angular
        .module("app")
        .factory('classService', factory);

    factory.$inject = ['$http','baseUrl'];

    function factory($http, baseUrl) {
        var service = {
            getClasses: getClasses
        };

        return service;

        function getClasses(startDate,endDate) {
            var url = baseUrl + '/api/classes';
            var classes = $http.get(url);
            return classes;
        }
    }
})();