﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.FPS;
using System.Xml.Linq;
using System.Globalization;
using System.Net;
using System.IO;
using Agrond.AmazonAPI.Amazon.FPS;
using System.Xml;

namespace Agrond.AmazonAPI
{
    public class BookInfo
    {
        public XmlDocument GetData(Boolean pImage,Boolean pReview,string pISBN,string keyword) {
            try{
                AmazonBookAPI.SetSerConfig("AKIAJKYDGZUB55CJSPIQ", "k8/H7DouynptnUheX125oXg3byDvleLs4J0EDZYZ");
                AmazonBookAPI._strVersion = "2011-11-01";
                AmazonBookAPI._strServiceEndPoint="https://ecs.amazonaws.com/onca/xml";


                Uri serviceEndPoint = new Uri(AmazonBookAPI._strServiceEndPoint);
                IDictionary<String, String> parameters = AmazonBookAPI.getFPSDefaultParams();
                if (pImage == true) AmazonBookAPI.SetResponseGroup(parameters, "Images");
                else if (pReview == true) AmazonBookAPI.SetResponseGroup(parameters, "EditorialReview");
                parameters.Add("SearchIndex", "Books");
                if (pISBN != "")
                {
                    parameters.Add("ItemId", pISBN);
                    parameters.Add("IdType", "ISBN");
                    parameters.Add("Operation", "ItemLookup");
                    parameters.Add("AssociateTag", "agrondlycute-20");
                }
                else {
                    parameters.Add("Keywords", keyword);
                    parameters.Add("Operation", "ItemSearch");
                    parameters.Add("AssociateTag", "agrondlycute-20");
                }

                // Version 2 - New approach of signing requests
                //Add parameters for GetTransactionStatus API call
                //parameters.Add("Action", "GetTransactionStatus");
                //parameters.Add("TransactionId", "<Place a valid transactionId here>");
                //parameters.Add("Keywords", "peter%20pan");
                //parameters.Add("SearchIndex", "Books");
                //parameters.Add("Operation", "ItemLookup");
                //parameters.Add("SubscriptionId","13529AWJ97PJXSM2K1R2");
                //parameters.Add("AssociateTag","httpwwwcomput-20");
                //parameters.Add("ItemId", "0446567337");
                //parameters.Add("IdType", "ISBN");
                //parameters.Add("Keywords", "0805072454");
                //parameters.Add("Operation", "ItemSearch");   
                //parameters.Add("ResponseGroup","EditorialReview");
                parameters.Add(SignatureUtils.SIGNATURE_VERSION_KEYNAME, SignatureUtils.SIGNATURE_VERSION_2);
                parameters.Add(SignatureUtils.SIGNATURE_METHOD_KEYNAME, SignatureUtils.HMAC_SHA256_ALGORITHM);
                String signature = SignatureUtils.signParameters(parameters, AmazonBookAPI._strSecretKey, AmazonBookAPI.HttpMethod, serviceEndPoint.Host, serviceEndPoint.AbsolutePath);
                parameters.Add(SignatureUtils.SIGNATURE_KEYNAME, signature);
                String fpsURL = AmazonBookAPI.getFPSURL(parameters);
                XmlDocument XmlRespose = new XmlDocument();
                XmlRespose.LoadXml(AmazonBookAPI.SendRequest(fpsURL));
                return XmlRespose;
                //string result = AmazonBookAPI.SendRequest(fpsURL);
                //return result;
            }
            catch (Exception e)
            {
                throw new AmazonFPSException(e.Message);
            }
        }
    }
}
