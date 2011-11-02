using System.Collections.Generic;
using Agrond.DataAccess;

namespace Agrond.Plus
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
