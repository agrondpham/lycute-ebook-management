using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agrond.Lycute.Bus
{
    public class NameCreater
    {
        public static string CreateName(string pBookName,string pVolume) {
            string strFileName="";
            strFileName = pBookName + "(" + pVolume + ")";
            return strFileName;
        }
        //public static string CreateNameManyAuthors(ICollection<String> pAuthors) {
        //    string strFileName="";
        //    foreach (string strAuthor in pAuthors) {
        //        strFileName += strAuthor;
        //    }
        //    return strFileName;
        //}
        public static string CreateName(string pBookName) {
            return pBookName;
        }
        public static string CreateLocation(string pAuthor, string pBookName, string pFileType) { 
            string strFileLocation="";
            Dictionary<string, string> partOfLocation=new Dictionary<string,string>();;
            string strStructureFolder= LycuteApplication.GetStructureFolderString();
            switch (strStructureFolder) { 
                case "AE":
                    strFileLocation=pAuthor+"/"+pBookName+"/"+pFileType;
                    break;
                case "EA":
                    strFileLocation = pBookName + "/" + pAuthor + "/" + pFileType;
                    break;
                case "E":
                    strFileLocation = pBookName + "/" + pFileType;
                    break;
            }
            return strFileLocation;
        }
        public static string CreateLocation(string pAuthor, string pBookName)
        {
            string strFileLocation = "";
            Dictionary<string, string> partOfLocation = new Dictionary<string, string>(); ;
            string strStructureFolder = LycuteApplication.GetStructureFolderString();
            switch (strStructureFolder)
            {
                case "AE":
                    strFileLocation = pAuthor + "/" + pBookName;
                    break;
                case "EA":
                    strFileLocation = pBookName + "/" + pAuthor;
                    break;
                case "E":
                    strFileLocation = pBookName;
                    break;
            }
            return strFileLocation;
        }
        public static string GetFirstAuthor(string pStrAuthor) {
            string[] authours = pStrAuthor.Split(';');
            return authours[0];
        }
        public static string GetFileURL(string pAuthor, string pBookName,string pFileType )
        {
            string strFileLocation = NameCreater.CreateLocation(pAuthor,pBookName, pFileType);
            string strRootLocation = LycuteApplication.GetLocationString();
            string strRealLocation = strRootLocation + "/" + strFileLocation;
            return strRealLocation;
        }
        public static List<string> AuthorStringToList(string pAuthor)
        {
            List<string> listAuthor = new List<string>(pAuthor.Split(';'));
            return listAuthor;
        }
        //public static string CreateImageUrl(string pAuthor, string pBookName, string pImageName) {
            
        //    return pAuthor + "/" + pBookName + "/" + pImageName;
        //}
        //public static string GetLocation(string pAuthor, string pBookName, string pFileType)
        //{
        //    string strFileLocation = pAuthor + "/" + pBookName + "/" + pBookName + "." + pFileType;
        //    return strFileLocation;
        //}
    }
}
