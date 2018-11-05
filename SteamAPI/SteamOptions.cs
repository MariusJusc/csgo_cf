using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAPI
{
    public class SteamOptions
    {
        private readonly string WebApiKey = "F99139DFB8ADF45439E634DF9D662105";

        public string getApiKey()
        {
            return WebApiKey;
        }
    }
}