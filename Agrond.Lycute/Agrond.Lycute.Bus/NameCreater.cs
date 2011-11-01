using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agrond.Lycute.Bus
{
    public class NameCreater
    {
        public static string CreateName(string pBookName,string pVolume) {
            string strFileName = "";
            char[] exceptChar = { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
            foreach (char c in exceptChar)
            {
                strFileName = pBookName.Replace(c, ' ');
            } 
            strFileName = pBookName + "(" + pVolume + ")";
            return strFileName;
        }
        public static string CreateName(string pBookName){
            string strFileName=pBookName;
            char[] exceptChar = { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
            foreach (char c in exceptChar){
                strFileName = strFileName.Replace(c, ' ');
            }
            return strFileName;
        }
        //public static string CreateNameManyAuthors(ICollection<String> pAuthors) {
        //    string strFileName="";
        //    foreach (string strAuthor in pAuthors) {
        //        strFileName += strAuthor;
        //    }
        //    return strFileName;
        //}
        public static string FileLocation(string pLocation, string pFileName) { 
            string strFileLocation="";
                strFileLocation = pLocation + "\\" + pFileName;
            return strFileLocation;
        }
        public static string CreateLocation(string pAuthor, string pBookName)
        {
            string strFileLocation = "";
            string strStructureFolder = LycuteApplication.GetStructureFolderString();
            switch (strStructureFolder)
            {
                case "AE":
                    strFileLocation = pAuthor + "\\" + pBookName;
                    break;
                case "EA":
                    strFileLocation = pBookName + "\\" + pAuthor;
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
        public static string GetFileURL(string pAuthor, string pBookName,string pFileName )
        {
            string strBookName = CreateName(pBookName);
            //string strFileLocation = FileLocation(CreateLocation(pAuthor,strBookName),pFileName);
            string strRootLocation = LycuteApplication.GetLocationString();
            string strRealLocation = strRootLocation + "\\"+NameCreater.CreateLocation(pAuthor,strBookName)+"\\" + pFileName;//strFileLocation;
            return strRealLocation;
        }
        public static string GetFileType(string pStrFileName) {
            string[] FileNameArray = pStrFileName.Split('.');
            string strFileType = FileNameArray[FileNameArray.Count() - 1];
            return strFileType;
        }
        public static string RepalceURL(string pURL) {
            string strFileName = pURL;
            strFileName = strFileName.Replace("\\", "/");
            return strFileName;
        }
        //public static string CreateImageUrl(string pAuthor, string pBookName, string pImageName) {
            
        //    return pAuthor + "/" + pBookName + "/" + pImageName;
        //}
        //public static string GetLocation(string pAuthor, string pBookName, string pFileType)
        //{
        //    string strFileLocation = pAuthor + "/" + pBookName + "/" + pBookName + "." + pFileType;
        //    return strFileLocation;
        //}
        //public static Boolean BookInFileType()
    }
}
