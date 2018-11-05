//Multiple Item selection
var assetsArray = [];
var joinGame = false;
var cfGameId = "";

$(function ()
{
    var cfHub = $.connection.coinflipTableHub;

    cfHub.client.broadcastNewGame = function (coinflipRow) {

        $('#coinflip-list-body').append(coinflipRow);
        ViewCoinflipGame();
    };

    cfHub.client.updateGame = function (coinflipRow)
    {
        $('#' + cfGameId).replaceWith(coinflipRow);
    };
});

/*Button Clicks*/
$("#coinflipCreateBtn").click(function ()
{
    LoadInventory();
});

$("#coinflipContinue").click(function ()
{
    ContinueInventory();
});

$("#cf-tradeOffer-BtnId").click(function () {

    $.ajax({
        type: "POST",
        url: "/coinflip/begin-coinflip",
        content: "application/json;",
        dataType: "json",
        data: { assetIds: assetsArray, cfGameId: cfGameId, joinGame: joinGame },
        traditional: true,
        success: function (data) {
            if (joinGame === false)
            {
                InsertNewCfGameRow();
            }
            else
            {
                UpdateCfGameRow(data.GameId);
            }
            
            closeCoinflipModal();
        }
    });
});

$("#close").click(function ()
{
    closeCoinflipModal();
});

$("#closeCfGame").click(function () {
    $('#coinflip-game-view').modal('hide');
    $('#cf_game_playerpar').remove();
    $('#cf_game_listpar').remove();

});

/*Functions*/
function checkValueIfExist(array, value)
{
    var index = array.indexOf(value);

    if (index === -1)
    {
        assetsArray.push(value);
    }
    else
    {
        assetsArray.splice(index, 1);
    }
}

function buttonVisibility()
{
    if (assetsArray.length > 0)
    {
        document.getElementById("coinflipContinue").classList.remove('hidden-button');
        document.getElementById("coinflipContinue").classList.toggle('visible-button');
    }
    else {
        document.getElementById("coinflipContinue").classList.remove('visible-button');
        document.getElementById("coinflipContinue").classList.toggle('hidden-button');
    }
}

function TableRefresher() 
{
    $.ajax({
        type: "GET",
        url: "coinflip/coinflip-table",
        dataType: "json",
        complete: function (response)
        {
            $('#coinflip-list-body').html(response.responseText);
            ViewCoinflipGame();
        },
    });
}

function closeCoinflipModal()
{
    var userDiv = document.getElementById('coinflip-inventory-userId');
    var tradeDiv = document.getElementById('coinflip-inventory-loadingId');

    if (userDiv.style.display === 'none') {
        userDiv.style.display = 'block';
        tradeDiv.style.display = 'none';

        var coinflipContinueBtn = document.getElementById('coinflipContinue');

        coinflipContinueBtn.style.display = 'block';
    }

    joinGame = false;
    
    $('#coinflipCreate').modal('hide');
}

function responsiveInventory()
{
    var x = document.getElementsByClassName('col-item');

    $(x).on('click', function () {
        var assetId = $(this).data('assetid');

        $(this).toggleClass('selected');

        checkValueIfExist(assetsArray, assetId);
        buttonVisibility();
    });
}

function InsertNewCfGameRow() {
    var cfHub = $.connection.coinflipTableHub;
    $.connection.hub.start().done(function () {
        cfHub.server.newGame(assetsArray);
        assetsArray = [];
    });
}

function UpdateCfGameRow(gameId)
{
    var cfHub = $.connection.coinflipTableHub;

    $.connection.hub.start().done(function ()
    {
        cfHub.server.playerJoined(gameId);
    });
}

function StartCountDown() {
    var timerHub = $.connection.countDownHub;
    $.connection.hub.start();
}

function LoadInventory()
{
    $.ajax({
        type: "GET",
        url: "/coinflip/load-inventory",
        dataType: "json",
        complete: function (data) {
            $("#coinflip-inventory-userId").html(data.responseText);
            responsiveInventory();
        }
    });
}

function ViewCoinflipGame()
{
    $(".view-button-click").click(function () {
        var id = this.dataset.id;

        $.ajax({
            type: "POST",
            url: "/coinflip/load-coinflip-game",
            content: "application/json;",
            dataType: "json",
            data: { cfId: id },
            traditional: true,
            complete: function (response)
            {
                $('#cf_game_body').html(response.responseText);
                JoinCoinflipGame();
            }
        });
    });
}

function JoinCoinflipGame()
{
    $(".join-button-click").click(function () {

        var gameId = this.dataset.id;

        $("#coinflip-game-view").modal("hide");
        LoadInventory();
        $("#coinflipCreate").modal("show");
        joinGame = true;
        cfGameId = gameId;
    });
}

function ContinueInventory() {
    var userDiv = document.getElementById('coinflip-inventory-userId');
    var tradeDiv = document.getElementById('coinflip-inventory-loadingId');

    if (userDiv.style.display !== 'none') {
        userDiv.style.display = 'none';
        tradeDiv.style.display = 'block';

        var coinflipContinueBtn = document.getElementById('coinflipContinue');
        coinflipContinueBtn.style.display = 'none';
    }
}

function CountdownTimer() {
    var timespan = new Date();

    window.onload = function () {
        var performance = window.performance || window.mozPerformance || window.msPerformance || window.webkitPerformance;

        timespan.setTime(timespan.getTime() + performance.timing.loadEventStart - performance.timing.navigationStart);
        // after window.onload, you're sync with the server

        setInterval(function ()
            {
                tsp.setSeconds(tsp.getSeconds() + 1);
                document.title = tsp.toString();
            }, 1000); // 1000 ms = timer may be off by 500ms.
    };
}