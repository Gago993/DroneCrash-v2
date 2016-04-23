(function () {
    'use strict';

    var core = angular.module('app.core');

    core.config(configure);

    configure.$inject = ['$stateProvider', '$locationProvider', '$urlRouterProvider', 'uiGmapGoogleMapApiProvider'];

    function configure($stateProvider, $locationProvider, $urlRouterProvider, uiGmapGoogleMapApiProvider) {

        $urlRouterProvider.otherwise("/main");

        $locationProvider.html5Mode({ enabled: true, requireBase: false });

        // here config the states
        $stateProvider
         .state('main', {
             url: "/main",
             templateUrl: 'App/Main/index.html',
             controller: 'MainController',
             controllerAs: 'vm'
         });

        uiGmapGoogleMapApiProvider.configure({
            //    key: 'your api key',
            v: '3.20', //defaults to latest 3.X anyhow
            libraries: 'weather,geometry,visualization'
        });

    }

})();