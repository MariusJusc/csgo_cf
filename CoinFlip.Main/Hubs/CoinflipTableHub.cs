using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using CoinFlip.Main.Controllers;
using Microsoft.AspNet.Identity;

namespace CoinFlip.Main.Hubs
{
    public class CoinflipTableHub : Hub
    {
        public void NewGame(List<string> assetIds)
        {
            CoinflipController _coinflipController = new CoinflipController();
            var userId = Context.User.Identity.GetUserId();

            var view = _coinflipController.ReturnViewHTML(userId, assetIds);

            Clients.All.broadcastNewGame(view);
        }

        public void PlayerJoined(Guid gameId)
        {
            CoinflipController _coinflipController = new CoinflipController();
            var view = _coinflipController.ReturnViewHTML(gameId);

            Clients.All.updateGame(view);
        }
    }
}