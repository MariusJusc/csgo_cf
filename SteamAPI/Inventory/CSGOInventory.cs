using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SteamKit2;
using SteamAPI.SteamModels;

namespace SteamAPI
{
    public class CSGOInventory
    {
        /// <summary>
        /// Gets the inventory for the given Steam ID using the Steam Community website.
        /// </summary>
        /// <returns>The inventory for the given user. </returns>
        /// <param name='steamid'>The Steam identifier. </param>
        /// <param name="steamWeb">The SteamWeb instance for this Bot</param>
        public static dynamic GetInventory(SteamID steamid, SteamWeb steamWeb)
        {
            string url = String.Format("http://steamcommunity.com/inventory/{0}/730/2?trading=1", steamid.ConvertToUInt64());

            try
            {
                string response = steamWeb.Fetch(url, "GET");
                return JsonConvert.DeserializeObject(response);
            }
            catch (Exception)
            {
                return JsonConvert.DeserializeObject("{\"success\":\"false\"}");
            }
        }      
    }
    
}

