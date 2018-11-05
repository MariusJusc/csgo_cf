using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CoinFlip.Main.Hubs
{
    public class CountDownHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}