using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agrond.Validation
{
    public class ValidationLib
    {
        public static string DefaultData(String pData) {
            if (pData == "")
                return "Unknow";
            else
                return pData;
        }
        public static bool IsNull(String pData) {
            if (pData == "")
                return true;
            else
                return false;
        }
        public static bool IsNum(String pData)
        {
            if (pData == "")
                return false;
            else
            {
                try
                {
                    int abc = Convert.ToInt32(pData);
                    return true;
                }
                catch {
                    return false;
                }
            }
        }
    }
}
