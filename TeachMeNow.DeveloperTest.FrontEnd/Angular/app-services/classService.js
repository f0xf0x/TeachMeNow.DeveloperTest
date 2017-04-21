﻿(function () {
    'use strict';

    angular
        .module("app")
        .factory('classService', factory);

    factory.$inject = ['$http','baseUrl'];

    function factory($http, baseUrl) {
        var service = {
            getClasses: getClasses,
            updateClass: updateClass
        };


        var url = baseUrl + '/api/classes';
        function getClasses(startDate,endDate) {
            var classes = $http.get(url);
            return classes;
        }

        function updateClass(cm) {
            var putUrl = url + "/" + cm.Id;
            var res = $http.put(putUrl,cm);
            return res;
        }

        return service;
    }
})();