using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAPI
{
    public class SteamPlayerSummaryDTO
    {
        [JsonProperty("steamid")]
        public string SteamId { get; set; }

        [JsonProperty("communityvisibilitystate")]
        public int CommunityVisibilityState { get; set; }

        [JsonProperty("profilestate")]
        public int ProfileState { get; set; }

        [JsonProperty("personaname")]
        public string PersonaName { get; set; }

        [JsonProperty("lastlogoff")]
        public int LastLogoff { get; set; }

        [JsonProperty("profileurl")]
        public string ProfileUrl { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("avatarmedium")]
        public string AvatarMedium { get; set; }

        [JsonProperty("avatarfull")]
        public string AvatarFull { get; set; }

        [JsonProperty("personastate")]
        public int PersonaState { get; set; }

        [JsonProperty("realname")]
        public string RealName { get; set; }

        [JsonProperty("primaryclanid")]
        public string PrimaryClanId { get; set; }

        [JsonProperty("timecreated")]
        public int TimeCreated { get; set; }

        [JsonProperty("personastateflags")]
        public int PersonaStateFlags { get; set; }

        [JsonProperty("loccountrycode")]
        public string LocCountryCode { get; set; }

        [JsonProperty("locstatecode")]
        public string LocStateCode { get; set; }

        [JsonProperty("loccityid")]
        public int LocCityId { get; set; }
    }

    public class SteamPlayerSummaryRespone
    {
        public List<SteamPlayerSummaryDTO> Players { get; set; }
    }

    public class SteamPlayerSummaryRootObject
    {
        public SteamPlayerSummaryRespone Response { get; set; }
    }
}
