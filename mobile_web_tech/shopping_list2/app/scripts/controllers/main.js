

function MainCtrl($scope, phonegapReady, $timeout, $http) {
    $scope.newItems = localStorage.getItem('yasl-items') ? JSON.parse(localStorage.getItem('yasl-items')) : [];
    $scope.categories = localStorage.getItem('yasl-categories') ? JSON.parse(localStorage.getItem('yasl-categories')) : [];

    $scope.category = $scope.categories[0];

    $scope.$watch('categories', function() {
      console.log('categories changed!');
      if($scope.categories.length > 0) {
        // update dropdown selection
        $scope.category = $scope.categories[0];

      }
      // save to local storage
      localStorage.setItem('yasl-categories', JSON.stringify($scope.categories));

      // recreate items array to verify order
      var temp = [];
      $scope.categories.forEach(function(element, index, array) {
        var t = _.where($scope.newItems, { category: element });
        if(t.length > 0) {
          temp.push(t[0]);
        }
        else {
          temp.push({category: element});
        }
      });
      $scope.newItems = temp;

      // save to local storage
      localStorage.setItem('yasl-items', JSON.stringify($scope.newItems));
    }, true);



}
