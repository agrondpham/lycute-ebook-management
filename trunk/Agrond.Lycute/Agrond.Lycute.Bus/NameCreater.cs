using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agrond.Lycute.Bus
{
    public class NameCreater
    {
        public static string CreateNameVolume(string pBookName,string pVolume) {
            string strFileName="";
            strFileName = pBookName + "(" + pVolume + ")";
            return strFileName;
        }
        public static string CreateNameManyAuthors(ICollection<String> pAuthors) {
            string strFileName="";
            foreach (string strAuthor in pAuthors) {
                strFileName += strAuthor;
            }
            return strFileName;
        }

    }
}
