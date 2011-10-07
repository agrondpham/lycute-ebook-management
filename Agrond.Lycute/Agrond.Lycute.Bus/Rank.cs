using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agrond.Lycute.Bus
{
    public class Rank
    {
        public static String RankImage(int pIntRankNumber)
        {
            string _strImageURL="";
            switch (pIntRankNumber)
            {
                case 1:
                    _strImageURL = @"pack://application:,,,/LycuteEbookManagement;component/Images/Components/1star.png";
                    break;
                case 2:
                    _strImageURL = @"pack://application:,,,/LycuteEbookManagement;component/Images/Components/2stars.png";
                    break;
                case 3:
                    _strImageURL = @"pack://application:,,,/LycuteEbookManagement;component/Images/Components/3stars.png";
                    break;
                case 4:
                    _strImageURL = @"pack://application:,,,/LycuteEbookManagement;component/Images/Components/4stars.png";
                    break;
                case 5:
                    _strImageURL = @"pack://application:,,,/LycuteEbookManagement;component/Images/Components/5stars.png";
                    break;
            }
           //Uri uriLink= new Uri (_strImageURL,UriKind.Absolute);
           //return uriLink;
            return _strImageURL;
        }
    }
}
