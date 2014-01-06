function HomeCtrl($scope, $http, $location, $sce) {
  $http.get('api/items').then(function (response) {
    $scope.itemList = response.data;

    //Category Filters
    $scope.categoryTables = 1
    $scope.categoryChairs = 2
    $scope.categoryCouches = 3
    $scope.categoryFridges = 4
    $scope.categoryComputers = 5
    $scope.categoryAppliances = 6
    $scope.categoryTelevisions = 7
    $scope.categoryRadios = 8
    $scope.categoryTents = 9
    $scope.categoryHiking = 10
    $scope.categoryCycling = 11
    $scope.categoryRunning = 12

    $scope.sce = $sce;
  });
}

function ItemCtrl($scope, $http, $routeParams, $sce, $location) {
  $scope.hideWishlists = true;

  $http.get('/api/items/' + $routeParams.id).then(function (response) {
    $scope.ItemID = response.data.ItemID;
    $scope.ItemName = response.data.ItemName;
    $scope.ShortDescription = response.data.ShortDescription;
    $scope.Description = ToTrusted(response.data.Description, $sce);
    $scope.Price = response.data.Price;
    $scope.item = response.data;
  })

  $http.get('/api/wishlist').then(function (response) {
    $scope.wishls = response.data;
  })

  $scope.buy = function () {
    var itemInfo = new Object();
    if ($scope.item == null || $scope.item.ItemID == null)
      itemInfo.ItemID = $routeParams.id;
    else 
      itemInfo.ItemID = $scope.item.ItemID;
    $scope.request = itemInfo;
    $http.post('/api/items', $scope.request)
        .then(function (response) {
          $scope.message = response.data.Message;
        }
    );
  };

  $scope.showWishlists = function () {
    $scope.hideWishlists = false;
  };

  $scope.addToWishlist = function AddToWishlist(wishlist) {
    var dataArray = new Array();
    if ($scope.item == null || wishlist == null)
    {
      console.log("DEV Error: JS - item or wishlist is null");
      return;
    }
    dataArray[0] = $scope.item.ItemID;
    dataArray[1] = wishlist.WishlistID;
    $scope.request = dataArray;
    $http.post('/api/wishlistitem', $scope.request)
        .then(function (response) {
          $scope.message = response.data.Message;
          $scope.hideWishlists = true;
        }
    );
  };
}

function WishlistCtrl($scope, $http, $routeParams, $location) {
  var apiController = '/api/wishlist';

  $scope.getItem = function (itemID) {
    $http.get('/api/items/' + $routeParams.id).then(function (response) {
      return response.data;
    });
  };

  $http.get(apiController + '/' + $routeParams).then(function (response) {
    $scope.wishlist = response.data;
  });
}

function WishlistListCtrl($scope, $http, $routeParams, $location) {
  var apiController = '/api/wishlist';

  $scope.getItem = function (itemID) {
    $http.get('/api/items/' + $routeParams.id).then(function (response) {
      return response.data;
    });
  };

  $http.get(apiController).then(function (response) {
    $scope.wishlistList = response.data;
  });
}

//Utils
function ToTrusted(html_code, $sce) {
  return $sce.trustAsHtml(html_code);
}