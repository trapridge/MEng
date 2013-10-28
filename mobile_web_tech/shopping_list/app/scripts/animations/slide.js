'use strict';

angular.module('html5app')
  .animation('.slide', function() {

	 var duration = 180;

    return {
      enter: function(element, parent, after, done) {
        element.hide();
        jQuery(element).slideDown(duration, done);

        return function(cancelled) {
          if(cancelled) {
            jQuery(element).stop();
          }
        }
      },
      leave: function(element, parent, after, done) {
        jQuery(element).slideUp(duration, done);

        return function(cancelled) {
          if(cancelled) {
            jQuery(element).stop();
          }
        }
      },
      /*
      move: function(element, parent, after, done) {
        element.hide();
        // after.hide();
        jQuery(element).fadeIn(duration, done);

        return function(cancelled) {
          if(cancelled) {
            jQuery(element).stop();
          }
        }
      },
      */
      // show: function(element, done) { },
      // hide: function(element, done) { },
      // addClass: function(element, className, done) { },
      // removeClass: function(element, className, done) { },
    }
  });