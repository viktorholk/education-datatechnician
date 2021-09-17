var currentList = {}
var serverLists = [];

function createShoppingList() {
    currentList.name = $("#shoppingListName").val();
    currentList.items = new Array();

    //Web Service Call
    showShoppingList();

}

function showShoppingList(id) {
    $("#shoppingListTitle").html(currentList.name);
    $("#shoppingListItems").empty();

    $("#createListDiv").hide();
    $("#shoppingListDiv").show();

    $("#newItemName").focus();
    $("#newItemName").keyup(function (event) {
        if (event.keyCode == 13) {
            addItem();
        }
    });
}

function addItem() {
    console.log(currentList);
    currentList.items.push({
        Name: $("#newItemName").val(),
        Price: 0
    });
    console.info(currentList);
    drawItems();
    $("#newItemName").val("");
}

function drawItems() {
    var $list = $("#shoppingListItems").empty();
    console.log(currentList);
    for (var i = 0; i < currentList.items.length; i++) {
        var currentItem = currentList.items[i];
        var $li = $("<li>").html(`${currentItem.Name}, <b>${currentItem.Price}</b>,- DK`).attr("id", "item_" + i);
        var $deleteBtn = $("<button onclick='deleteItem(" + i + ")'>D</button>").appendTo($li);
        var $checkBtn = $("<button onclick='checkItem(" + i + ")'>C</button>").appendTo($li);

        $li.appendTo($list);
    }
}

function deleteItem(index) {
    currentList.items.splice(index, 1);
    drawItems();
}

function checkItem(index) {
    if ($("#item_" + index).hasClass("checked")) {
        $("#item_" + index).removeClass("checked");
    }
    else {
        $("#item_" + index).addClass("checked");
    }


    $("#item_" + index).addClass("checked");
}

function getShoppingListById(id) {
    console.info(id);

    const list = serverLists[id];

    currentList.name = list.ListName;
    currentList.items = list.Items;

    showShoppingList();
    drawItems();
}




$(document).ready(function () {
    console.info("ready");
    $("#shoppingListName").focus();
    $("#shoppingListName").keyup(function (event) {
        if (event.keyCode == 13) {
            createShoppingList();
        }
    });


    $.ajax({
        method: "GET",
        url: "AjaxReceiver.aspx/GetLists",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        error: (result) => {
            console.error(result);
        },
        success: (result) => {
            // Get the data and add it to the serverLists variables
            const data = JSON.parse(result.d);
            serverLists = data;

            // Add the items as buttons to the page
            for (let i in data) {
                const item = data[i];
                console.log(item);

                $('#listButtons').append(`<Button onclick="getShoppingListById(${i})" >${item.ListName}</Button>`)
            }
        } 
    })
});