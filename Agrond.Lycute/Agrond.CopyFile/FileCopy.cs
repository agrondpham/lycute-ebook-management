using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agrond.CopyFile
{
    public class FileCopy
    {
        protected bool CopyFile(string pStrSourceFile, string pStrTargetFile) {
            try
            {
                System.IO.File.Copy(pStrSourceFile, pStrTargetFile, true);
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
        public string Copy(string pStrSourcePath, string pStrTargetPath, string pStrFileName)
        {
            if (System.IO.Directory.Exists(pStrTargetPath))
                System.IO.Directory.CreateDirectory(pStrTargetPath);
            if (CopyFile(System.IO.Path.Combine(pStrSourcePath, pStrFileName),
                    System.IO.Path.Combine(pStrTargetPath, pStrFileName)))
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
