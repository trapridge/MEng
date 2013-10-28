'use strict';

angular.module('html5app', [
    'ngRoute',
    'ngTouch',
    //'ngAnimate',
    //'ui.sortable'
  ])
  .config(function ($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'views/main.html',
        controller: 'MainCtrl'
      })
      .otherwise({
        redirectTo: '/'
      });
  });
