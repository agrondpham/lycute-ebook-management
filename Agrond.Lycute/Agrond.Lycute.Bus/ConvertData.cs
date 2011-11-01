using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Agrond.Lycute.DAO;

namespace Agrond.Lycute.Bus
{
    public class ConvertData
    {
        //public static string ToString(ICollection<Author> pAuthors)
        //{
        //    string strAuthor = "";

        //    foreach (var a in pAuthors)
        //    {
        //        strAuthor = a.ath_Name + ";" + strAuthor;
        //    }
        //    return strAuthor;
        //}
        
        public static string ToString(ICollection<Tag> pTags)
        {
            string strTarget = "";

            foreach (var t in pTags)
            {
                strTarget = t.tag_Name + ";" + strTarget;
            }
            return strTarget;
        }
        public static List<string> ToList(string pStrArray) { 
            List<string> listData = new List<string>(pStrArray.Split(';'));
            return listData;
        }
    }
}
