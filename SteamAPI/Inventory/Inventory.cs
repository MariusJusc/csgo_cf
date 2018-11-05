using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SteamAPI.SteamModels;
using SteamAPI.Exceptions;
using SteamKit2;

namespace SteamAPI
{
    public class Inventory
    {
        public bool IsPrivate { get; private set; }
        public bool IsGood { get; private set; }

        private const int WebRequestMaxRetries = 3;
        private const int WebRequestTimeBetweenRetriesMs = 1000;
        private SteamWeb _steamWeb;
        
        public Inventory(SteamWeb steamWeb)
        {
            _steamWeb = steamWeb;
        }

        /// <summary>
        /// Used in Fetching Steam Account's Inventory by SteamID
        /// </summary>
        /// <returns>Player Inventory's Assets/Description</returns>
        public ItemRootObject FetchInventory(SteamID steamId)
        {
            var inventoryUrl = string.Format("http://steamcommunity.com/inventory/{0}/730/2", steamId.ConvertToUInt64());

            var response = RetryWebRequest(inventoryUrl);

            try
            {
                var inventory = JsonConvert.DeserializeObject<ItemRootObject>(response);

                return inventory; 
            }
            catch (Exception exc)
            {
                Console.WriteLine("Failed to deserialize: {0}", inventoryUrl);
                Console.WriteLine(exc.ToString());
                return null;
            }

        }

        /*public Item GetItem(ItemDescription itemDescription)
        {
            // Check for Private Inventory
            if (this.IsPrivate)
                throw new InventoryException("Unable to access Inventory: Inventory is Private!");

            return Item.SingleOrDefault(item =>
                itemDescription.AppId.ToString() == item.Appid &&
                itemDescription.ClassId.ToString() == item.ClassId &&
                itemDescription.InstanceId.ToString() == item.InstanceId);
        }

        public ItemDescription GetItemDescription(Item item)
        {
            // Check for Private Inventory
            if (this.IsPrivate)
                throw new InventoryException("Unable to access Inventory: Inventory is Private!");

            // Check if Item is Legit
            if (item == null)
                return null;

            return Descriptions.FirstOrDefault(itemDesc =>
                itemDesc.AppId.ToString() == item.Appid &&
                itemDesc.ClassId.ToString() == item.ClassId &&
                itemDesc.InstanceId.ToString() == item.InstanceId);

            http://steamcommunity.com/inventory/76561198080614320/730/2
        }*/



        /// <summary>
        /// Calls the given function multiple times, until we get a non-null/non-false/non-zero result, or we've made at least
        /// WEB_REQUEST_MAX_RETRIES attempts (with WEB_REQUEST_TIME_BETWEEN_RETRIES_MS between attempts)
        /// </summary>
        /// <returns>The result of the function if it succeeded, or an empty string otherwise</returns>
        private string RetryWebRequest(string url)
        {
            for (var i = 0; i < WebRequestMaxRetries; i++)
            {
                try
                {
                    return _steamWeb.Fetch(url, "GET");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                if (i != WebRequestMaxRetries)
                {
                    System.Threading.Thread.Sleep(WebRequestTimeBetweenRetriesMs);
                }
            }
            return string.Empty;
        }
    }
}
