/**
	 * Computes RFC 2104-compliant HMAC signature for request parameters
	 * Implements AWS Signature, as per following spec:
	 *
	 * If Signature Version is 0, then V1 algorithm is used for ASP and CBUI apps only.
	 *
	 * If Signature Version is 1, it performs the following:
	 *
	 * Sorts all  parameters (including SignatureVersion and excluding Signature,
	 * the value of which is being created), ignoring case.
	 *
	 * Iterate over the sorted list and append the parameter name (in original case)
	 * and then its value. It will not URL-encode the parameter values before
	 * constructing this string. There are no separators.
	 *
	 * If Signature Version is 2, string to sign is based on following:
	 *
	 *    1. The HTTP Request Method followed by an ASCII newline (%0A)
	 *    2. The HTTP Host header in the form of lowercase host, followed by an ASCII newline.
	 *    3. The URL encoded HTTP absolute path component of the URI
	 *       (up to but not including the query string parameters);
	 *       if this is empty use a forward '/'. This parameter is followed by an ASCII newline.
	 *    4. The concatenation of all query string components (names and values)
	 *       as UTF-8 characters which are URL encoded as per RFC 3986
	 *       (hex characters MUST be uppercase), sorted using lexicographic byte ordering.
	 *       Parameter names are separated from their values by the '=' character
	 *       (ASCII character 61), even if the value is empty.
	 *       Pairs of parameter and values are separated by the '&' character (ASCII code 38).
	 *
	 */


namespace Amazon.FPS
{

    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;
    using System.Security.Cryptography;

    public class SignatureUtils
    {
        public static readonly String SIGNATURE_KEYNAME = "Signature";
        public static readonly String SIGNATURE_METHOD_KEYNAME = "SignatureMethod";
        public static readonly String SIGNATURE_VERSION_KEYNAME = "SignatureVersion";
        public static readonly String SIGNATURE_VERSION_1 = "1";
        public static readonly String SIGNATURE_VERSION_2 = "2";
        public static readonly String HMAC_SHA1_ALGORITHM = "HmacSHA1";
        public static readonly String HMAC_SHA256_ALGORITHM = "HmacSHA256";

        // Constants used when constructing the string to sign for v2
        public static readonly String AppName = "ASP";
        public static readonly String NewLine = "\n";
        public static readonly String EmptyUriPath = "/";
        public static String equals = "=";
        public static readonly String And = "&";
        public static readonly String UTF_8_Encoding = "UTF-8";

        /**
        * Calculate String to Sign for SignatureVersion 1
        * @param parameters request parameters
        * @return String to Sign
        */


        public static String signParameters(IDictionary<String, String> parameters, String key, String HttpMethod, String Host, String RequestURI ) //throws Exception
        {
            String signatureVersion = parameters[SIGNATURE_VERSION_KEYNAME];
            String algorithm = HMAC_SHA1_ALGORITHM;
            String stringToSign = null;
            if (signatureVersion == null && String.Compare(AppName, "FPS", true) != 0)
            {
                // String to sign algorithm is same as V1 in case of ASP and CBUI
                stringToSign = calculateStringToSignV1(parameters);
            }
            else if (String.Compare("1", signatureVersion, true) == 0)
            {
                stringToSign = calculateStringToSignV1(parameters);
            }
            else if (String.Compare("2", signatureVersion, true) == 0)
            {
                algorithm = parameters[SIGNATURE_METHOD_KEYNAME];
                //		        parameters.Add("SignatureMethod", algorithm);
                stringToSign = calculateStringToSignV2(parameters, HttpMethod, Host, RequestURI);
            }
            else
            {
                throw new AmazonFPSException("Invalid Signature Version specified");
            }
            return sign(stringToSign, key, algorithm);
        }

        /**
	    * Calculate String to Sign for SignatureVersion 1
	    * @param parameters request parameters
	    * @return String to Sign
	    */
        private static String calculateStringToSignV1(IDictionary<String, String> parameters)
        {
            StringBuilder data = new StringBuilder();
            IDictionary<String, String> sorted =
              new SortedDictionary<String, String>(parameters, StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<String, String> pair in sorted)
            {
                if (pair.Value != null)
                {
                    if (String.Compare(pair.Key, SIGNATURE_KEYNAME, true) == 0) continue;
                    data.Append(pair.Key);
                    data.Append(pair.Value);
                }
            }
            return data.ToString();
        }

        /**
    	 * Calculate String to Sign for SignatureVersion 2
	     * @param parameters
    	 * @param httpMethod - POST or GET
	     * @param hostHeader - Service end point
    	 * @param requestURI - Path
	     * @return
    	 */
        private static String calculateStringToSignV2(IDictionary<String, String> parameters, String httpMethod, String hostHeader, String requestURI)// throws SignatureException
        {
            StringBuilder stringToSign = new StringBuilder();
            if (httpMethod == null) throw new Exception("HttpMethod cannot be null");
            stringToSign.Append(httpMethod);
            stringToSign.Append(NewLine);

            // The host header - must eventually convert to lower case
            // Host header should not be null, but in Http 1.0, it can be, in that
            // case just append empty string ""
            if (hostHeader == null)
                stringToSign.Append("");
            else
                stringToSign.Append(hostHeader.ToLower());
            stringToSign.Append(NewLine);

            if (requestURI == null || requestURI.Length == 0)
                stringToSign.Append(EmptyUriPath);
            else
                stringToSign.Append(UrlEncode(requestURI, true));
            stringToSign.Append(NewLine);

            IDictionary<String, String> sortedParamMap = new SortedDictionary<String, String>(parameters, StringComparer.Ordinal);
            foreach (String key in sortedParamMap.Keys)
            {
                if (String.Compare(key, SIGNATURE_KEYNAME, true) == 0) continue;
                stringToSign.Append(UrlEncode(key, false));
                stringToSign.Append(equals);
                stringToSign.Append(UrlEncode(sortedParamMap[key], false));
                stringToSign.Append(And);
            }

            String result = stringToSign.ToString();
            return result.Remove(result.Length - 1);
        }

        public static String UrlEncode(String data, bool path)
        {
            StringBuilder encoded = new StringBuilder();
            String unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~" + (path ? "/" : "");

            foreach (char symbol in System.Text.Encoding.UTF8.GetBytes(data))
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    encoded.Append(symbol);
                }
                else
                {
                    encoded.Append("%" + String.Format("{0:X2}", (int)symbol));
                }
            }

            return encoded.ToString();

        }

        /**
         * Computes RFC 2104-compliant HMAC signature.
         */
        public static String sign(String data, String key, String signatureMethod)// throws SignatureException
	    {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                HMAC Hmac = HMAC.Create(signatureMethod);
                Hmac.Key = encoding.GetBytes(key);
                Hmac.Initialize();
                CryptoStream cs = new CryptoStream(Stream.Null, Hmac, CryptoStreamMode.Write);
                cs.Write(encoding.GetBytes(data), 0, encoding.GetBytes(data).Length);
                cs.Close();
                byte[] rawResult = Hmac.Hash;
                String sig = Convert.ToBase64String(rawResult, 0, rawResult.Length);
                return sig;
            }
            catch (Exception e)
            {
                throw new AmazonFPSException("Failed to generate signature: " + e.Message);
            }
	    }

    }
}
