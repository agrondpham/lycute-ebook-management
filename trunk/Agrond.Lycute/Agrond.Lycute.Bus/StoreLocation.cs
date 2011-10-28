using System;
using System.IO;
using System.Data.SqlServerCe;
using System.Configuration;
using Agrond.CopyFile;

namespace Agrond.Lycute.Bus
{
    public class StoreLocation
    {
        public bool CheckDatabase(string pStrLibLocation) {
            string strDBFileName = "Library.sdf";
            bool checkResult = false;
            if (Directory.Exists(pStrLibLocation))
            {
                if (!File.Exists(pStrLibLocation + "/" + strDBFileName))
                {
                    CreateDB(pStrLibLocation, strDBFileName);
                    checkResult = true;
                }
                else checkResult = true;
            }
            else {
                CreateFolder(pStrLibLocation);
                CreateDB(pStrLibLocation,strDBFileName);
                //update database connection string
                checkResult = true;
            }
            return checkResult;
        }
        private void CreateFolder(string pStrLocation) {
            Directory.CreateDirectory(pStrLocation);
        }
        private void CreateDB(string pStrLocation,string pStrFileName) {
            Agrond.CopyFile.FileCopy CopyLib = new FileCopy();
            CopyLib.Copy(@"D:\Projects\Agrond.Lycute\Agrond.Lycute.DAO\Database\"+pStrFileName, pStrLocation+"\\"+pStrFileName,"file");
        }
    }
}
