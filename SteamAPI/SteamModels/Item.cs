using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using SteamKit2;
using System.Text;
using System.Threading.Tasks;

namespace SteamAPI.SteamModels
{
    public class Item
    {
        [JsonProperty("appid")]
        public int Appid { get; set; }

        [JsonProperty("contextid")]
        public string Contextid { get; set; }

        [JsonProperty("assetid")]
        public string Assetid { get; set; }

        [JsonProperty("classid")]
        public string ClassId { get; set; }

        [JsonProperty("instanceid")]
        public string InstanceId { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }
    }

    public class ItemDescription
    {
        [JsonProperty("appid")]
        public int AppId { get; set; }

        [JsonProperty("classid")]
        public string ClassId { get; set; }

        [JsonProperty("instanceid")]
        public string InstanceId { get; set; }

        [JsonProperty("currency")]
        public int Currency { get; set; }

        [JsonProperty("background_color")]
        public string BackgroundColor { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("descriptions")]
        public List<Description> Descriptions { get; set; }

        [JsonProperty("tradable")]
        public int IsTradable { get; set; }

        [JsonProperty("name")]
        public string DisplayName { get; set; }

        [JsonProperty("name_color")]
        public string NameColor { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("market_name")]
        public string Name { get; set; }

        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; }

        [JsonProperty("commodity")]
        public int IsCommodity { get; set; }

        [JsonProperty("market_tradable_restriction")]
        public int MarketTradableRestriction { get; set; }

        [JsonProperty("marketable")]
        public int Marketable { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("icon_url_large")]
        public string IconUrlLarge { get; set; }

        [JsonProperty("actions")]
        public List<Action> Actions { get; set; }

        [JsonProperty("market_actions")]
        public List<Action> MarketActions { get; set; }
    }

    public class Description
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

    public class Action
    {
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Tag
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("internal_name")]
        public string InternalName { get; set; }

        [JsonProperty("localized_category_name")]
        public string LocalizedCategoryName { get; set; }

        [JsonProperty("localized_tag_name")]
        public string LocalizedTagName { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

    public class ItemRootObject
    {
        [JsonProperty("assets")]
        public List<Item> Assets { get; set; }

        [JsonProperty("descriptions")]
        public List<ItemDescription> Descriptions { get; set; }

        [JsonProperty("total_inventory_count")]
        public int TotalInventoryCount { get; set; }

        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("rwgrsn")]
        public int Rwgrsn { get; set; }
    }
}