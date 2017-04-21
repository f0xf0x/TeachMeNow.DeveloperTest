﻿(function () {
    'use strict';

    var app = angular
        .module('app', ['ui.router', 'ngMessages', 'ngStorage', 'ui.calendar'])
        .config(config)
        .run(run);
    app.value('baseUrl', 'http://localhost:50020');

    function config($stateProvider, $urlRouterProvider) {
        // default route
        $urlRouterProvider.otherwise("/");

        // app routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: 'angular/home/index.view.html',
                controller: 'Home.IndexController',
                controllerAs: 'vm'
            })
            .state('new', {
                url: '/new',
                templateUrl: 'angular/event/create.view.html',
                controller: 'Event.IndexController',
                controllerAs: 'vm'
            })
            .state('login', {
                url: '/login',
                templateUrl: 'angular/login/index.view.html',
                controller: 'Login.IndexController',
                controllerAs: 'vm'
            })
            .state('logout', {
                url: '/logout',
                controller: function ($state, AuthenticationService) {
                    AuthenticationService.Logout();
                    $state.go("login");

                }
            });
    }

    function run($rootScope, $http, $location, $localStorage) {
        // keep user logged in after page refresh
        if ($localStorage.currentUser) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + $localStorage.currentUser.token;
        }

        // redirect to login page if not logged in and trying to access a restricted page
        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            var publicPages = ['/login'];
            var restrictedPage = publicPages.indexOf($location.path()) === -1;
            if (restrictedPage && !$localStorage.currentUser) {
                $location.path('/login');
            }
        });
    }
})();