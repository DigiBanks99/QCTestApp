var apiController = '';

function AccordionCntrl($scope) {
  $scope.expandAccordion = ExpandAccordion();
}

function HomeCtrl($scope, $http, $rootScope) {
  apiController = 'api/items';

  $scope.message = "";
  $scope.success = true;
  $scope.messageClass = "message-class-" + $scope.success;

  if (typeof $rootScope.itemList === 'undefined' || $rootScope.itemList.length == 0) {
    $http.get(apiController).then(function (response) {
      $scope.itemList = response.data.ObjectList;
      $rootScope.itemList = $scope.itemList;

      $scope.message = response.data.Message;
      $scope.success = response.data.Success;
      $scope.messageClass = "message-class-" + $scope.success;

      $scope.outdrList = new Array();
      $scope.electList = new Array();
      $scope.furnList = new Array();

      for (var i = 0; i < $scope.itemList.length; i++) {
        switch ($scope.itemList[i].CategoryID) {
          case 1:
          case 2:
          case 3:
          case 4:
            $scope.furnList[$scope.furnList.length] = $scope.itemList[i];
            break;
          case 5:
          case 6:
          case 7:
          case 8:
            $scope.electList[$scope.electList.length] = $scope.itemList[i];
            break;
          case 9:
          case 10:
          case 11:
          case 12:
            $scope.outdrList[$scope.outdrList.length] = $scope.itemList[i];
            break;
          default:
            break;
        }
      }
    });
  }
  else
  {
    $scope.itemList = $rootScope.itemList;
    $scope.outdrList = new Array();
    $scope.electList = new Array();
    $scope.furnList = new Array();

    for (var i = 0; i < $scope.itemList.length; i++) {
      switch ($scope.itemList[i].CategoryID) {
        case 1:
        case 2:
        case 3:
        case 4:
          $scope.furnList[$scope.furnList.length] = $scope.itemList[i];
          break;
        case 5:
        case 6:
        case 7:
        case 8:
          $scope.electList[$scope.electList.length] = $scope.itemList[i];
          break;
        case 9:
        case 10:
        case 11:
        case 12:
          $scope.outdrList[$scope.outdrList.length] = $scope.itemList[i];
          break;
        default:
          break;
      }
    }
  }
}

function ItemCtrl($scope, $http, $routeParams, $sce) {
  $scope.hideWishlists = true;
  apiController = '/api/items/' + $routeParams.id;

  $scope.message = "";
  $scope.success = true;
  $scope.messageClass = "message-class-" + $scope.success;

  $http.get(apiController).then(function (response) {
    $scope.ItemID = response.data.Object.ItemID;
    $scope.ItemName = response.data.Object.ItemName;
    $scope.ShortDescription = response.data.Object.ShortDescription;
    $scope.Description = ToTrusted(response.data.Object.Description, $sce);
    $scope.Price = response.data.Object.Price;
    $scope.item = response.data.Object;

    $scope.message = response.data.Message;
    $scope.success = response.data.Success;
    $scope.messageClass = "message-class-" + $scope.success;
  });

  $http.get('/api/wishlist').then(function (response) {
    $scope.wishls = response.data.ObjectList;
    $scope.message = response.data.Message;
    $scope.success = response.data.Success;
    $scope.messageClass = "message-class-" + $scope.success;
    $(".listbox li").click(function () {
      $(".listbox li").not(this).each(function () {
        $(this).css("background-color", "white");
        $(this).css("color", "black");
      });
      $(this).css("background-color", "#1661AD");
      $(this).css("color", "white");
    });
  })

  $scope.buy = function () {
    var itemInfo = new Object();
    if ($scope.item == null || $scope.item.ItemID == null)
      itemInfo.ItemID = $routeParams.id;
    else
      itemInfo.ItemID = $scope.item.ItemID;
    $scope.request = itemInfo;
    $http.post('/api/items', $scope.request).then(function (response) {
      $scope.message = response.data.Message;
      $scope.success = response.data.Success;
      $scope.messageClass = "message-class-" + $scope.success;
      if ($scope.success == true) {
        $scope.message = "The item has been added to your cart.";
        $scope.success = false;
      }
    })
  };

  $scope.addToWishlist = function AddToWishlist(wishlist) {
    var dataArray = new Array();
    if ($scope.item == null || wishlist == null) {
      console.log("DEV Error: JS - item or wishlist is null");
      return;
    }
    dataArray[0] = $scope.item.ItemID;
    dataArray[1] = wishlist.WishlistID;
    $scope.request = dataArray;
    $http.post('/api/wishlistitem', $scope.request).then(function (response) {
      $scope.message = response.data.Message;
      $scope.success = response.data.Success;
      $scope.messageClass = "message-class-" + $scope.success;
      $scope.hideWishlists = true;
      if ($scope.success == true) {
        $scope.message = "The item has been added to " + wishlist.WishlistName + ".";
        $scope.success = false;
      }
    });
  };

  $scope.showWishlists = function () {
    $scope.hideWishlists = false;
  };
}

function WishlistCtrl($scope, $http, $routeParams, $rootScope) {
  var apiController = '/api/wishlist/' + $routeParams.id;
  $scope.checkedList = "";

  $scope.message = "";
  $scope.success = true;
  $scope.messageClass = "message-class-" + $scope.success;

  $http.get(apiController).then(function (response) {
    $scope.wishlist = response.data.Object;
    $scope.message = response.data.Message;
    $scope.success = response.data.Success;
    $scope.messageClass = "message-class-" + $scope.success;
  });

  if (typeof $rootScope.itemList === 'undefined' || $rootScope.itemList.length == 0) {
    $http.get('/api/items').then(function (response) {
      $scope.items = response.data.ObjectList;
      $rootScope.itemList = $scope.items;
      $scope.message = response.data.Message;
      $scope.success = response.data.Success;
      $scope.messageClass = "message-class-" + $scope.success;
    });
  }
  else
  {
    $scope.items = $rootScope.itemList;
  }

  $scope.addToCart = function () {
    var info = new Object();
    info.CheckedList = $scope.checkedList;
    if (info.CheckedList != null)
    {
      $http.post('/api/addtocart', info).then(function (response) {
        $scope.message = response.data.Message;
        $scope.success = response.data.Success;
        $scope.messageClass = "message-class-" + $scope.success;
        if ($scope.success == true) {
          $scope.message = "The items have been added to your cart.";
          $scope.success = false;
        }
      });
    }
  }
}

function WishlistListCtrl($scope, $http, $routeParams, $rootScope) {
  var apiController = '/api/wishlist';

  $scope.message = "";
  $scope.success = true;
  $scope.messageClass = "message-class-" + $scope.success;

  $scope.category = new Object();

  $http.get(apiController).then(function (response) {
    $scope.wishlistList = response.data.ObjectList;
    $scope.message = response.data.Message;
    $scope.success = response.data.Success;
    $scope.messageClass = "message-class-" + $scope.success;
  });

  if (typeof $rootScope.categories === 'undefined' || $rootScope.categories.length == 0) {
    $http.get('/api/category').then(function (response) {
      $scope.categories = response.data.ObjectList;
      $rootScope.categories = $scope.categories;
      $scope.message = response.data.Message;
      $scope.success = response.data.Success;
      $scope.messageClass = "message-class-" + $scope.success;
    });
  }
  else {
    $scope.categories = $rootScope.categories;
  }

  $scope.getCategoryNameByID = function (categoryID) {
    for (var i = 0; i < $scope.categories.length; i++) {
      if ($scope.categories[i].CategoryID == categoryID)
        return $scope.categories[i].CategoryName;
    }
    return "Uncategorised";
  };

  $scope.setCategory = function (cat) {
    $scope.category = cat;
    $(".listbox li").click(function () {
      $(".listbox li").not(this).each(function () {
        $(this).css("background-color", "white");
        $(this).css("color", "black");
      });
      $(this).css("background-color", "#1661AD");
      $(this).css("color", "white");
    });
  };

  $scope.createWishlist = function () {
    var info = new Object();
    info.WishlistName = $scope.wlname;
    if ($scope.category != null)
      info.CategoryID = $scope.category.CategoryID;
    else
      info.CategoryID = null;
    $http.post('/api/wishlist', info).then(function (response) {
      $scope.message = response.data.Message;
      $scope.success = response.data.Success;
      $scope.messageClass = "message-class-" + $scope.success;
    });
  };
}

function CartCtrl($scope, $http, $rootScope) {
  apiController = '/api/order';
  $scope.checkedList = "";

  $scope.message = "";
  $scope.success = true;
  $scope.messageClass = "message-class-" + $scope.success;

  $http.get(apiController).then(function (response) {
    $scope.orders = response.data.ObjectList;
    $scope.success = response.data.Success;
    $scope.messageClass = "message-class-" + $scope.success;
    $scope.total = 0;
    for (var i = 0; i < $scope.orders.length; i++)
      $scope.total = $scope.total + $scope.orders[i].Total;
  });

  if (typeof $rootScope.itemList === 'undefined' || $rootScope.itemList.length == 0) {
    $http.get('/api/items').then(function (response) {
      $scope.items = response.data.ObjectList;
      $rootScope.itemList = $scope.items;
      $scope.message = response.data.Message;
      $scope.success = response.data.Success;
      $scope.messageClass = "message-class-" + $scope.success;
    });
  }
  else {
    $scope.items = $rootScope.itemList;
  }

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
      $scope.message = response.data.Message;
      $scope.success = response.data.Success;
      $scope.messageClass = "message-class-" + $scope.success;

      for (var i = 0; i < $scope.orders.length; i++) {
        if ($scope.orders[i].OrderID == response.data.Order.OrderID)
          $scope.orders[i] = response.data.Order;
      }
    });
  };
}

function ItemCategoryCtrl($scope, $http, $routeParams, $rootScope) {
  $scope.message = "";
  $scope.success = true;
  $scope.messageClass = "message-class-" + $scope.success;
  $scope.itemList = new Array();

  if (typeof $rootScope.categories === 'undefined' || $rootScope.categories.length == 0) {
    $http.get('/api/category').then(function (response) {
      $scope.categories = response.data.ObjectList;
      $scope.message = response.data.Message;
      $scope.success = response.data.Success;
      $scope.messageClass = "message-class-" + $scope.success;

      var categoryID = new Object();
      for (var i = 0; i < $scope.categories.length; i++) {
        if ($scope.categories[i].CategoryName.toUpperCase() != $routeParams.cat.toUpperCase())
          continue;
        categoryID = $scope.categories[i].CategoryID;
        $scope.categoryName = $scope.categories[i].CategoryName;
        break;
      }

      for (var i = 0; i < $rootScope.itemList.length; i++) {
        if ($rootScope.itemList[i].CategoryID == categoryID)
          $scope.itemList[$scope.itemList.length] = $rootScope.itemList[i];
      }
    });
  }
  else {
    $scope.categories = $rootScope.categories;
    var categoryID = new Object();
    for (var i = 0; i < $scope.categories.length; i++) {
      if ($scope.categories[i].CategoryName.toUpperCase() != $routeParams.cat.toUpperCase())
        continue;
      categoryID = $scope.categories[i].CategoryID;
      $scope.categoryName = $scope.categories[i].CategoryName;
      break;
    }

    for (var i = 0; i < $rootScope.itemList.length; i++) {
      if ($rootScope.itemList[i].CategoryID == categoryID)
        $scope.itemList[$scope.itemList.length] = $rootScope.itemList[i];
    }
  }
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

function GetMessageClass() {
  return $scope.message + $scope.success;
}