using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;

namespace MarsaX
{

    /// Uses XLINQ to create a List<see cref="PhotoInfo">PhotoInfo</see> using an Rss feed.
    /// 
    /// The following links are useful information regarding how the Flickr API works 
    /// 
    /// http://www.flickr.com/services/api/misc.urls.html
    /// http://www.flickr.com/services/rest/?method=flickr.photos.getRecent&api_key=
    /// http://www.flickr.com/services/rest/?method=flickr.interestingness.getList&api_key=
    /// http://www.flickr.com/services/api/flickr.photos.search.html
    /// </summary>
    public class FlickerProvider
    {
        #region Data
        private const string FLICKR_API_KEY = "2c9cae43031e99b6b5e62a2bb2a1edbf";
        private const string MOST_RECENT = "http://www.flickr.com/services/rest/?method=flickr.photos.getRecent&api_key=" + FLICKR_API_KEY;
        private const string INTERESTING = "http://www.flickr.com/services/rest/?method=flickr.interestingness.getList&api_key=" + FLICKR_API_KEY;
        private const string SEARCH = "http://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=" + FLICKR_API_KEY + "&text={0}";
        #endregion

        #region LoadPicturesFromFeed
        /// <summary>
        /// Returns a List<see cref="PhotoInfo">PhotoInfo</see> which represent
        /// the latest Flickr images
        /// </summary>
        public static List<PhotoInfo> LoadLatestPictures()
        {
            List<PhotoInfo> list = new List<PhotoInfo>();
            list.Add(new PhotoInfo { ImageUrl = "http://farm7.static.flickr.com/6168/6206807687_f8cc953e85_m.jpg" });
            list.Add(new PhotoInfo { ImageUrl = "http://farm7.static.flickr.com/6154/6206807695_33cb6d0397_m.jpg" });
            list.Add(new PhotoInfo { ImageUrl = "http://farm7.static.flickr.com/6138/6206807709_64e57fa1a4_m.jpg" });
            list.Add(new PhotoInfo { ImageUrl = "http://farm7.static.flickr.com/6156/6206807711_9ff51341a4_m.jpg" });

            return list;
            
        }
        #endregion
    }
}
