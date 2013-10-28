'use strict';

angular.module('html5app')
  .directive('focusableItem', function($timeout) {
      return {
        restrict: 'A',
        link: function(scope, element, attrs) {
          if(scope.item.focusMe) {
            // this is the element we want - safe to delete indicator from model
            delete scope.item.focusMe;

            // $timeout(function() {
              // scope.$apply(
                jQuery(element).focus()
                // );
            // }, 0);
          }
        }
      }
  });