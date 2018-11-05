using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data;
using CoinFlip.Models;
using SteamAPI;
using SteamKit2;
using Newtonsoft.Json;
using SteamAPI.SteamModels;

namespace CoinFlip.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            SteamWeb steamweb = new SteamWeb();


            //var inv = CSGOInventory.GetInventory(76561198080614320, steamweb);

            /*var inventoryUrl = string.Format("http://steamcommunity.com/inventory/76561198080614320/730/2?count=5000");

            string response = steamweb.Fetch(inventoryUrl, "GET");

            var inventory = JsonConvert.DeserializeObject<ItemRootObject>(response);

            var classId = inventory.Assets[10].ClassId.ToString();

            var itemDesc = inventory.Descriptions.FirstOrDefault(itemD => itemD.ClassId.ToString() == classId);

            System.Console.WriteLine("{0}", itemDesc.DisplayName);
            System.Console.ReadLine();*/


            Inventory inv = new Inventory(steamweb);

            var inven = inv.FetchInventory(76561198080614320);

            List<ItemDescription> list = inven.Descriptions;

            //var classId = inven.Assets[10].ClassId.ToString();

            //var itemDesc = inven.Descriptions.FirstOrDefault(itemD => itemD.ClassId.ToString() == classId);

            foreach(var assets in list)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("{0} -- {1}", inven.Descriptions.FirstOrDefault(itemD => itemD.ClassId.ToString() == assets.ClassId).DisplayName, assets.ClassId);
                System.Console.ReadLine();
            }
        }
    }
}