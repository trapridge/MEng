function ItemsCtrl($scope, phonegapReady, $timeout, $http) {

  $scope.newRemaining = function(category) {
    var count = 0;
    var i = 0;
    for(; i < $scope.newItems.length; i++) {
      var element = $scope.newItems[i];

      if(category && category === element.category && element.hasOwnProperty('items')) {
        return _.where(element.items, { done: false }).length;
      }
      else {
        if(element.hasOwnProperty('items')) {
          count += _.where(element.items, { done: false }).length;
        }
      }
    }
    return count;
  }

  $scope.newExisting = function(category) {
    var count = 0;
    var i = 0;
    for(; i < $scope.newItems.length; i++) {
      var element = $scope.newItems[i];

      if(category && category === element.category && element.hasOwnProperty('items')) {
        return element.items.length;
      }
      else {
        if(element.hasOwnProperty('items')) {
          count += element.items.length;
        }
      }
    }
    return count;
  }

  $scope.newArchive = function() {
    $scope.newItems.forEach(function(element) {
      if(element.hasOwnProperty('items')) {
        element.items = _.filter(element.items, { done: false });
      }
    }); 
  }

  var fetch = function (newValue) {
    $timeout.cancel($scope.fetchTimeout);
    $scope.fetchTimeout = $timeout(function () {

      $http.jsonp('https://en.wikipedia.org/w/api.php?' + 
        'format=json&prop=revisions&rvprop=content& ' + 
        'action=query&titles=' + newValue + "&callback=JSON_CALLBACK").success(function(data) {

        var string = JSON.stringify(data);

// vaiheessa

        if(new RegExp('Baked goods').test(string)) { $scope.category = 'Baked goods'; return; }
        if(new RegExp('Baking products').test(string)) { $scope.category = 'Baking products'; return; }
        if(new RegExp('Beverages').test(string)) { $scope.category = 'Beverages'; return; }
        if(new RegExp('Canned food').test(string)) { $scope.category = 'Canned food'; return; }
        if(new RegExp('Coffee and tea').test(string)) { $scope.category = 'Coffee and tea'; return; }
        if(new RegExp('Dairy products').test(string)) { $scope.category = 'Dairy products'; return; }
        if(new RegExp('Fresh meat').test(string)) { $scope.category = 'Fresh meat'; return; }
        if(new RegExp('Fruits and vegetables').test(string)) { $scope.category = 'Fruits and vegetables'; return; }
        if(new RegExp('Packed meat').test(string)) { $scope.category = 'Packed meat'; return; }
        if(new RegExp('Pharmacy products').test(string)) { $scope.category = 'Pharmacy products'; return; }

      });

    }, 500);
  }

  $scope.newAddItem = function() {
    // attempt only if data is there
    if($scope.category && $scope.item) {
      // check if category exists
      var categoryElement = _.where($scope.newItems, { category: $scope.category });

      if(categoryElement.length === 0) {
        // no matching category in the list, create one and push new item into it
        var newElement = { category: $scope.category, items: [{ text: $scope.item, done: false }] };
        // push to items
        $scope.newItems.push(newElement);
      }
      else {
        // category exists
        if(!categoryElement[0].hasOwnProperty('items')) {
          // seems like this is the first item for the category - create container
          categoryElement[0].items = [];
        }
        // push into category
        categoryElement[0].items.push({ text: $scope.item, done: false });

        // replace the original category object
        $scope.newItems.forEach(function(element, index) {
          if(element.category === categoryElement[0].category) {
            $scope.newItems[index] = categoryElement[0];
          }
        });
      }

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

      $scope.item = '';
    }
  }

  $scope.$watch('item', function(newValue) {
    console.log('changed: ' + newValue);
    // save to local storage
    localStorage.setItem('yasl-items', JSON.stringify($scope.newItems));
    fetch(newValue);
  });

}
