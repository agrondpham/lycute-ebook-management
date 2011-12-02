using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agrond.SearchEngin
{
    public class SearchLib
    {
        public static Dictionary<string,string> Search(string pSreachString)
        {
            bool author = pSreachString.Contains("AU:");
            bool publisher = pSreachString.Contains("PL:");
            string[] listsearchdata = pSreachString.Split(';');
            //bool author = pSreachString.Contains("AU:");
            Dictionary<string, string> data=new Dictionary<string,string>();
            foreach(string searchdata in listsearchdata){
                if (searchdata.Contains("author")) {
                    string authorData = getData(searchdata);
                data.Add("author", authorData);
                }else
                if (searchdata.Contains("publisher"))
                {
                    string authorData = getData(searchdata);
                    data.Add("publisher", authorData);
                }
                else{
                    data.Add("title", searchdata.Trim());
                }
            }
            return data;
        }
        private static string getData(string data)
        {
            string[] arrayData = data.Split(':');
            string subData = arrayData[arrayData.Count() - 1];
            return subData.Trim();
        }

    }
}
