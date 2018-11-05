using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoinFlip.Main.Extensions
{
    public static class ActionImageExtension
    {
        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, string controllerName, object HtmlAttributes, string imagePath)//, string alt)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            //imgBuilder.MergeAttribute("alt", alt);
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, controllerName, HtmlAttributes));
            anchorBuilder.InnerHtml = imgHtml;
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }
    }
}