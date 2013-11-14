function TodoCtrl($scope) {
  $scope.todos = [
    {text:'learn angular', done:true},
    {text:'build an angular app', done:false}];

  $scope.$on('handleBroadcast', function(event, args) {
    console.log('TodoCtrl::addTodo - received!');
    $scope.todos.push({text:args.text, done:false});
  });

  $scope.remaining = function() {
    var count = 0;
    angular.forEach($scope.todos, function(todo) {
      count += todo.done ? 0 : 1;
    });
    return count;
  };

  $scope.archive = function() {
    var oldTodos = $scope.todos;
    $scope.todos = [];
    angular.forEach(oldTodos, function(todo) {
      if (!todo.done) $scope.todos.push(todo);
    });
  };
}

function TodoAddCtrl($scope) {

  $scope.addTodo = function() {
    $scope.$emit('handleEmit', {text: $scope.todoText});
    $scope.todoText = '';
    AppRouter.instance.back();
  };
}

/*
'use strict';

angular.module('lungotestApp')
  .controller('MainCtrl', function ($scope) {
    $scope.awesomeThings = [
      'HTML5 Boilerplate',
      'AngularJS',
      'Karma'
    ];
  });
*/