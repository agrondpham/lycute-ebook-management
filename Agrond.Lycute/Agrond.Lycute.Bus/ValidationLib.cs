using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agrond.Lycute.Bus
{
    public class ValidationLib
    {
        public static string DefaultData(String pData) {
            if (pData == "")
                return "Unknow";
            else
                return pData;
        }
    }
}
