(function () {
    'use strict';

    angular
        .module("app")
        .factory('classService', factory);

    factory.$inject = ['$http', 'baseUrl'];

    function factory($http, baseUrl) {
        var service = {
            getClass: getClass,
            getClasses: getClasses,
            updateClass: updateClass,
            createClass: createClass,
            deleteClass:deleteClass
        };


        var url = baseUrl + '/api/classes';
        function getClass(id,startDate, endDate) {
            var classObj = $http.get(url+"/"+id);
            return classObj;
        }
        function getClasses(startDate, endDate) {
            var classes = $http.get(url);
            return classes;
        }

        function updateClass(cm) {
            var putUrl = url + "/" + cm.Id;
            var res = $http.put(putUrl, cm);
            return res;
        }
        function createClass(cm) {
            var res = $http.post(url, cm);
            return res;
        }

        function deleteClass(id) {
            var deleteUrl = url + "/" + id;
            var res = $http.delete(deleteUrl);
            return res;
        }

        return service;
    }
})();