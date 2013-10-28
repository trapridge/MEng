'use strict';

angular.module('html5app')
  
  .controller('MainCtrl', function ($scope, $timeout) {

    var sortableEle;

    var counter = 0;

    $scope.listItems = [
      { name: 'chicken', category: 'meat' },
      { name: 'beer', category: 'drink' },
      { name: 'toast', category: 'bread' }
    ];

/*
    $scope.addTop = function() {
    	$scope.listItems.unshift({});
      sortableEle.sortable('refresh');
    }

    $scope.addBottom = function() {
    	$scope.listItems.push({});
      sortableEle.sortable('refresh');
    }

    $scope.move = function(oldIndex, newIndex) {
      if(newIndex >= 0 && newIndex < $scope.listItems.length) {
        var temp = $scope.listItems[oldIndex];
        $scope.listItems[oldIndex] = $scope.listItems[newIndex];
        $scope.listItems[newIndex] = temp;
        sortableEle.sortable('refresh');
      }
    }
*/

    $scope.addMiddle = function(index) {
      $scope.listItems.splice(++index, 0, {});

      // move to directive
      sortableEle.sortable('refresh');
    }

    $scope.remove = function(index) {
      $scope.listItems.splice(index, 1);

      // move to directive
      sortableEle.sortable('refresh');
    }

    $scope.shop = function(index) {
      if($scope.listItems[index].shopped !== 'shopped') {
        $scope.listItems[index].shopped = 'shopped';
      }
      else {
        delete $scope.listItems[index].shopped; 
      }
    }

    $scope.pretty = function() {
        return JSON.stringify($scope.listItems, undefined, 2);
    }


    /* move to directive --> */ 
    $scope.dragStart = function(e, ui) {
        ui.item.data('start', ui.item.index());
    }
    $scope.dragEnd = function(e, ui) {
        var start = ui.item.data('start');
        var end = ui.item.index();
        
        $scope.listItems.splice(end, 0, $scope.listItems.splice(start, 1)[0]);
        $scope.$apply();
    }
        
    sortableEle = jQuery('#sortable').sortable({
      start: $scope.dragStart,
      update: $scope.dragEnd,
      axis: 'y',
      opacity: 0.5,
      //cancel: "button, input, li",
      //handle: ".handle"
    });

    sortableEle.bind('click.sortable mousedown.sortable',function(ev){
      ev.target.focus();
//       if ($(touch.target).is("input") || $(touch.target).is("textarea")) {
//         ev.stopPropagation();
//       } else {
//         ev.preventDefault();
//       }
    });
    /* --> move to directive */ 

  });