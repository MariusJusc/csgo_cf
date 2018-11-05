using System;
using Newtonsoft.Json;

namespace SteamAPI
{
    public class UserRetriever
    {
        private const int WebRequestMaxRetries = 3;
        private const int WebRequestTimeBetweenRetriesMs = 1000;
        private SteamWeb _steamWeb;
        private readonly SteamOptions steamOptions;

        public UserRetriever(SteamWeb steamWeb)
        {
            _steamWeb = steamWeb;
            steamOptions = new SteamOptions();
        }

        public SteamPlayerSummaryDTO SteamUser(string steamId)
        {
            if (!String.IsNullOrEmpty(steamId))
            {
                string uri = String.Format("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={0}&steamids={1}", RetrieveWebAPI(), steamId);

                var response = RetryWebRequest(uri);

                try
                {
                    var user = JsonConvert.DeserializeObject<SteamPlayerSummaryRootObject>(response);
                    return user.Response.Players[0];
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Failed to deserialize: {0}", uri);
                    Console.WriteLine(exc.ToString());
                    return null;
                }
            }
            return null;
        }

        private string RetrieveWebAPI()
        {
            return steamOptions.getApiKey();
        }
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
