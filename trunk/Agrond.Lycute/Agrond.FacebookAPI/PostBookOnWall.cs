using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;
using System.Dynamic;


namespace Agrond.FacebookAPI
{
    public class PostBookOnWall
    {
        public void PostOnMyWall(string pContent, string pAccessToken) {
            try
            {
                var client = new FacebookClient(pAccessToken);
                dynamic parameters = new ExpandoObject();
                parameters.message = "Comment of Customer";
                parameters.link = "ebooklink http://www.example.com/article.html";
                parameters.picture = "ebook picture http://www.example.com/article-thumbnail.jpg";
                parameters.name = "Book Title";
                parameters.caption = "Caption for the link";
                parameters.description = "Longer description of the link";
                parameters.actions = new
                {
                    name = "View on Amazon",
                    link = "link http://www.zombo.com",
                };
                parameters.privacy = new
                {
                    value = "ALL_FRIENDS",
                };
                parameters.targeting = new
                {
                    countries = "US",
                    regions = "6,53",
                    locales = "6",
                };
                dynamic result = client.Post("me/feed", parameters);
            }
            catch (Exception)
            {

            }
        }

    }
}
