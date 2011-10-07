using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.FPS;
using System.Xml;
using System.Globalization;
using System.Net;
using System.IO;


namespace Agrond.AmazonAPI.Amazon.FPS
{
    public class AmazonBookAPI
    {
        // Set these values depending on the service endpoint you are going to hit
        public static readonly String HttpMethod = "GET";
        public static String _strAppName { set; get; }
        public static String _strServiceEndPoint { set; get; }
        public static String _strVersion { set; get; }
        public static String _strAccessKey { set; get; }
        public static String _strSecretKey { set; get; }

        //Set Variable
        public static void SetSerConfig(String pStrAccessKey, String pStrSecretKey)
        {
            _strAccessKey = pStrAccessKey;
            _strSecretKey = pStrSecretKey;
        }

        public static IDictionary<String, String> getFPSDefaultParams()
        {
            IDictionary<String, String> parameters = new Dictionary<String, String>();
            parameters.Add("Service", "AWSECommerceService");
            parameters.Add("Version", _strVersion);
            parameters.Add("Timestamp", GetFormattedTimestamp());
            parameters.Add("AWSAccessKeyId", _strAccessKey);

            return parameters;
        }

        public static void SetResponseGroup(IDictionary<String, String> parameters, String pStrGroup)
        {
            parameters.Add("ResponseGroup", pStrGroup);
        }

        private static String GetFormattedTimestamp()
        {
            DateTime dateTime = DateTime.Now;
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                                 dateTime.Hour, dateTime.Minute, dateTime.Second,
                                 dateTime.Millisecond
                                 , DateTimeKind.Local
                               ).ToUniversalTime().ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z",
                                CultureInfo.InvariantCulture);
        }
        public static String getFPSURL(IDictionary<String, String> parameters)
        {
            StringBuilder fpsURL = new StringBuilder(_strServiceEndPoint + "?");
            foreach (KeyValuePair<String, String> pair in parameters)
            {
                fpsURL.Append(pair.Key);
                fpsURL.Append(SignatureUtils.equals);
                fpsURL.Append(SignatureUtils.UrlEncode(pair.Value, false));
                fpsURL.Append(SignatureUtils.And);
            }
            String result = fpsURL.ToString();
            return result.Remove(result.Length - 1);
        }
        public static string SendRequest(string pURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pURL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader StreamResponse = new StreamReader(response.GetResponseStream());
            string strResponse = StreamResponse.ReadToEnd();

            StreamResponse.Close();
            StreamResponse.Dispose();

            return strResponse;
        }
    }
}
