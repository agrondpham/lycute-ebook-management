using System;

namespace Agrond.Plus
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
            return _strImageURL;
        }
        public static int RankNumber(String pStrRanklink)
        {
            int _intRankNumber = 1;
            switch (pStrRanklink)
            {
                case "pack://application:,,,/LycuteEbookManagement;component/Images/Components/1star.png":
                    _intRankNumber = 1;
                    break;
                case "pack://application:,,,/LycuteEbookManagement;component/Images/Components/2stars.png":
                    _intRankNumber = 2;
                    break;
                case "pack://application:,,,/LycuteEbookManagement;component/Images/Components/3stars.png":
                    _intRankNumber = 3;
                    break;
                case "pack://application:,,,/LycuteEbookManagement;component/Images/Components/4stars.png":
                    _intRankNumber = 4;
                    break;
                case "pack://application:,,,/LycuteEbookManagement;component/Images/Components/5stars.png":
                    _intRankNumber = 5;
                    break;
            }
            return _intRankNumber;
        }
    }
}
