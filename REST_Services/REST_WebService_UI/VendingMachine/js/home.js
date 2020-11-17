var currentlySelectedItemId = 0;
var amountEntered = 0;
var change = 0;
var selectedItemId = '';

$(document).ready(function () {
    $('#addDollarButton').click(function(){
        amountEntered += 1;
        UpdateTotal(amountEntered);
    });
    $('#addQuarterButton').click(function(){
        amountEntered += 0.25;
        UpdateTotal(amountEntered);
    });
    $('#addDimeButton').click(function(){
        amountEntered += 0.10;
        UpdateTotal(amountEntered);
    });
    $('#addNickelButton').click(function(){
        amountEntered += 0.05;
        UpdateTotal(amountEntered);
    });
    $('#custom-message-total').show(function(){
        return amountEntered;
    });
    loadItems();
    $('#makePurchaseButton').click(function(){
        // MakePurchase(amountEntered, currentlySelectedItemId);
        // MakeChange(amountEntered, currentlySelectedItemId, change);
        MakePurchaseAndChange(amountEntered, currentlySelectedItemId, change);
    });
    $('#changeReturnButton').click(function(){
        location.reload(true);
    });
});

function UpdateTotal(amountEntered)
{
    $('#custom-message-total').val("$" + amountEntered.toFixed(2));
}

function ItemSelection(selectedItemId){
    $('#itemId_'+selectedItemId+'').find('#itemID-textField').toggleClass("border border-primary");
    if(currentlySelectedItemId > 0){
        $('#itemId_'+currentlySelectedItemId+'').find('#itemID-textField').toggleClass("border border-primary");
    }
    currentlySelectedItemId = selectedItemId;
    UpdateItem(currentlySelectedItemId);
    return currentlySelectedItemId;
}
function UpdateItem(currentlySelectedItemId){
    $('#itemID-textField').val(currentlySelectedItemId);
}

// LOAD ALL ITEMS
function loadItems() {
    $.ajax ({
        type: 'GET',
        url: 'http://localhost:8080/items',
        success: function(itemArray) {
            var itemContents = $('#itemContents');
            var rowId = '';
            var newRowIndex=0;
            $.each(itemArray, function(index,item){
                if (index == newRowIndex){
                    newRowIndex + 3;
                    rowId = "row" + index + "";
                    itemContents.append(Row(rowId));
                }
                var column = Column(item);
                var row = $('#'+rowId+'');
                row.append(column);
            });
        },
       error: function() {
        $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Error calling web service.  Please try again later.'));
        }
    });
}
function Column(item) {
    var selectedItemId = '<div id="itemId_'+item.id+'" class="itemGrid">';
        selectedItemId += '<button type="button" class="itemButtons" id="'+ item.id +'" onclick="ItemSelection(this.id)">'+'<span class="pull-left">' + item.id + '</span>' + '<br>' + '$' + item.price.toFixed(2) + '<br>' + 'Quantity Left: ' + item.quantity + '</button>';
        selectedItemId += '</div>';
    return selectedItemId;
}
function Row(id) {
    var row = '<div class="item-row" id="'+id+'">';
    row += '</div>';
    return row;
}

// MAKE PURCHASE AND CHANGE
function MakePurchaseAndChange(amountEntered, currentlySelectedItemId, change){
    $.ajax({
        type: 'GET',
        url: 'http://localhost:8080/money/' + amountEntered + '/item/' + currentlySelectedItemId + '',
        success: function(response){
            $('#custom-message-large').val("THANK YOU!!!");
            var change = response.quarters * 0.25;
            change += response.dimes * 0.10;
            change += response.nickels * 0.05;
            change += response.pennies * 0.01;
            $('#custom-message-change').val('$' + change.toFixed(2));
        },
        error: function (jqXHR, ajaxOptions, throwError) {
            var json = JSON.parse(jqXHR.responseText);
            $('#custom-message-large').val(json.message);
            $('#custom-message-change').val('');
        }
    });
}

/* 
OLD FUNCTIONS - NO LONGER CALLING
// MAKE PURCHASE
function MakePurchase(amountEntered, currentlySelectedItemId) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:8080/money/' + amountEntered + '/item/' + currentlySelectedItemId + '',
        success: function() {
            $('#custom-message-large').val("THANK YOU!!!");
        },
        error: function (jqXHR, ajaxOptions, throwError) {
            var json = JSON.parse(jqXHR.responseText);
            $('#custom-message-large').val(json.message);
        }
    });
}
// MAKE CHANGE
function MakeChange(amountEntered, currentlySelectedItemId, change) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:8080/money/' + amountEntered + '/item/' + currentlySelectedItemId + '',
        success: function(response){
            var change = response.quarters * 0.25;
            change += response.dimes * 0.10;
            change += response.nickels * 0.05;
            change += response.pennies * 0.01;
            $('#custom-message-change').val('$' + change.toFixed(2));
        },
        error: function(){
            $('#custom-message-change').val('No Change');

        }
    });    
} 
*/