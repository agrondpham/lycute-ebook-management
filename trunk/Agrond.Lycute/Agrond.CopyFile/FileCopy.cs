using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Permissions;
using System.Security;
using System.Security.AccessControl;

namespace Agrond.CopyFile
{
    public class FileCopy
    {
        protected bool CopyFile(string pStrSourceFile, string pStrTargetFile) {
            FileStream fsIn;
            FileStream fsOut;
            try
            {
                fsIn = new FileStream(pStrSourceFile, FileMode.Open, FileAccess.Read);
                fsOut = new FileStream(pStrTargetFile, FileMode.Create);
                fsIn.CopyTo(fsOut);
                fsIn.Close();
                fsOut.Close();
                return true;
            }
            catch(Exception e) { 
                Console.WriteLine(e.Message);
                return false;
            }
        }
        protected bool DeleteFile(string pStrDeleteFile) {
            System.IO.FileInfo fi = new System.IO.FileInfo(pStrDeleteFile);
            try
            {
                fi.Delete();
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        protected bool DeleteFolder(string pStrDeleteFolder) {
                try
                {
                    System.IO.Directory.Delete(pStrDeleteFolder, true);
                    return true;
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
        }
        //public function
        public string Copy(string pStrSourcePath, string pStrTargetPath)
        {
            if (System.IO.Directory.Exists(pStrTargetPath))
                System.IO.Directory.CreateDirectory(pStrTargetPath);
            if (CopyFile(pStrSourcePath,pStrTargetPath))
                return "successful";
            else return "There is/are errors when copy file";
        }
        public string Delete(string pStrDeleteFile,string pStrType) {
            if (pStrType == "file")
            {
                if (DeleteFile(pStrDeleteFile))
                    return "successful";
                else return "There is/are errors when copy file";
            }
            else
            {
                if (System.IO.Directory.Exists(pStrDeleteFile))
                {
                    if (DeleteFolder(pStrDeleteFile))
                        return "successful";
                    else return "There is/are errors when delete file";
                }
                else return "Folder is not existed";
            }
        }
    }
}
