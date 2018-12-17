using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Infrastructure
{
    public static class CustomHelpers
    {
        public static MvcHtmlString ListArrayItems(this HtmlHelper html, string[] list)
        {
            TagBuilder tag = new TagBuilder("ul");
            foreach (string item in list)
            {
                TagBuilder itemTag = new TagBuilder("li");
                itemTag.SetInnerText(item);
                tag.InnerHtml += itemTag.ToString();
            }
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString DisplayMessage(this HtmlHelper html, string message)
        {
            string result = $"This is the message: <p>{message}<p>";
            return new MvcHtmlString(result);
        }

        public static MvcHtmlString DisplayMessageByEncoding(this HtmlHelper html, string message)
        {
            string result = $"This is the message: <p>{html.Encode(message)}<p>";
            return new MvcHtmlString(result);
        }
    }
}