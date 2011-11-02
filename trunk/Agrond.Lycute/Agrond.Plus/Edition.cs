
namespace Agrond.Plus
{
    public class Edition
    {
        public int stringToNumber(string strEdition)
        {
            int editionValue=1;
            switch (strEdition.ToLower()) { 
                case "first":
                    editionValue = 1;
                    break;
                case "second":
                    editionValue = 2;
                    break;
                case "third":
                    editionValue = 3;
                    break;
                case "fourth":
                    editionValue = 4;
                    break;
                case "fifth":
                    editionValue = 5;
                    break;
                case "sixth":
                    editionValue = 6;
                    break;
                case "seventh":
                    editionValue = 7;
                    break;
                case "eighth":
                    editionValue = 8;
                    break;
            }
            return editionValue;
        }
        public string numberToString(int strEdition)
        {
            string editionValue = "first";
            switch (strEdition)
            {
                case 1:
                    editionValue = "First";
                    break;
                case 2:
                    editionValue = "Second";
                    break;
                case 3:
                    editionValue = "Third";
                    break;
                case 4:
                    editionValue = "Fourth";
                    break;
                case 5:
                    editionValue = "Fifth";
                    break;
                case 6:
                    editionValue = "Sixth";
                    break;
                case 7:
                    editionValue = "Seventh";
                    break;
                case 8:
                    editionValue = "Eighth";
                    break;
            }
            return editionValue;
        }
    }
}
