(function () {
    var app = angular.module('tmnApp', ['ngRoute']);
    app.value('baseUrl', 'http://localhost:55407/');

    app.config(['$routeProvider',
      function ($routeProvider) {
          $routeProvider.
            when('/home', {
                templateUrl: 'Angular/templates/home.html',
                controller: 'homeController',
                controllerAs: 'home'
            }).
            otherwise({
                redirectTo: '/home'
            });

          
      }]);
})();
