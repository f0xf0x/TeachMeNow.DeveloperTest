(function () {
    'use strict';

    angular
        .module('tmnApp')
        .factory('classService', factory);

    factory.$inject = ['$http'];

    function factory($http) {
        var service = {
            getData: getData
        };

        return service;

        function getData() { }
    }
})();