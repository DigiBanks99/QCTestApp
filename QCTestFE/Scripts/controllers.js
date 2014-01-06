var apiController = '';

function AccordionCntrl($scope) {
  $scope.expandAccordion = ExpandAccordion();
}

function HomeCtrl($scope, $http, $sce) {
  apiController = 'api/items';
  $http.get(apiController).then(function (response) {
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

function ItemCtrl($scope, $http, $routeParams, $sce) {
  $scope.hideWishlists = true;
  apiController = '/api/items/' + $routeParams.id;

  $http.get(apiController).then(function (response) {
    $scope.ItemID = response.data.ItemID;
    $scope.ItemName = response.data.ItemName;
    $scope.ShortDescription = response.data.ShortDescription;
    $scope.Description = ToTrusted(response.data.Description, $sce);
    $scope.Price = response.data.Price;
    $scope.item = response.data;
  })

  if (typeof $scope.wishls === 'undefined') {
    $http.get('/api/wishlist').then(function (response) {
      $scope.wishls = response.data;
    })
  }

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

function WishlistCtrl($scope, $http, $routeParams) {
  var apiController = '/api/wishlist/' + $routeParams.id;

  $http.get(apiController).then(function (response) {
    $scope.wishlist = response.data;
  });

  $http.get('/api/items').then(function (response) {
    $scope.items = response.data;
  });

  $scope.getItem = function (itemID) {
    for (var i = 0; i < $scope.items.length; i++) {
      if ($scope.items[i].ItemID == itemID)
        return $scope.items[i];
    }
  };
}

function WishlistListCtrl($scope, $http, $routeParams) {
  var apiController = '/api/wishlist';

  $http.get(apiController).then(function (response) {
    $scope.wishlistList = response.data;
  });

  $http.get('/api/category').then(function (response) {
    $scope.categories = response.data;
  });

  $scope.getCategoryNameByID = function (categoryID) {
    for (var i = 0; i < $scope.categories.length; i++) {
      if ($scope.categories[i].CategoryID == categoryID)
        return $scope.categories[i].CategoryName;
    }
    return "Uncategorised";
  };
}

function CartCtrl($scope, $http) {
  apiController = '/api/order';
  $http.get(apiController).then(function (response) {
    $scope.orders = response.data;
  });

  $http.get('/api/items').then(function (response) {
    $scope.items = response.data;
  });

  $scope.getItem = function (itemID) {
    return GetItem(itemID, $scope.items);
  };

  $scope.quantityChanged = function (orderID, quantity) {
    var dataArray = new Array();
    if (orderID == null || quantity == null) {
      console.log("DEV Error: JS - orderID or quantity is null in CartCtrl");
      return;
    }
    dataArray[0] = orderID;
    dataArray[1] = quantity;
    $http.post('/api/order', dataArray).then(function (response) {
      $scope.Message = response.data.Message;
      for (var i = 0; i < $scope.orders.length; i++) {
        if ($scope.orders[i].OrderID == response.data.Order.OrderID)
          $scope.orders[i] = response.data.Order;
      }
    });
  };
}

//Utils
function ToTrusted(html_code, $sce) {
  return $sce.trustAsHtml(html_code);
}

function ExpandAccordion() {
  $("#accordian h3").click(function () {
    //slide up all the link lists
    $("#accordian ul ul").slideUp();
    //slide down the link list below the h3 clicked - only if its closed
    if (!$(this).next().is(":visible")) {
      $(this).next().slideDown();
    }
  })
}

function GetItem(itemID, itemList) {
  for (var i = 0; i < itemList.length; i++) {
    if (itemList[i].ItemID == itemID)
      return itemList[i];
  }
}