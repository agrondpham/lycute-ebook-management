using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows;

namespace LycuteEbookManagement._3DWall
{
    static class Wall3dHelper
    {
        #region Data
        //These cant be bigger than 2 and 25 as Yahoo ImageSearch API 
        //has a limit of 50 (so ROWS * COLS can't be > 50)
        public static int ROWS = 2;
        public static int COLUMNS = 25;
        //Just to be able to use it as the Maximum for the Slider control 
        //(since Slider supports binding to Doubles ONLY)
        //allow 5 images to be shown when fully scrolled
        public static double COLUMNSTOSHOW = (double)COLUMNS - 5;


        #endregion
        static Wall3dHelper() {
            int.TryParse(ConfigurationManager.AppSettings["rows"].ToString(), out Wall3dHelper.ROWS);
            int.TryParse(ConfigurationManager.AppSettings["columns"].ToString(), out Wall3dHelper.COLUMNS);
        }
    }
    /// <summary>
    /// CustomEventArgs : a custom event argument class
    /// which simply holds an int value representing 
    /// how many times the associated event has been fired
    /// </summary>
    public class ModelSelectedEventArgs : RoutedEventArgs
    {
        #region Instance fields
        public string ImageUrl { get; private set; }
        #endregion

        #region Ctor
        /// <summary>
        /// Constructs a new ModelSelectedEventArgs object
        /// using the parameters provided
        /// </summary>
        /// <param name="imageUrl">the image url for the 
        /// ModelUIElement3D selected</param>
        public ModelSelectedEventArgs(RoutedEvent routedEvent, string imageUrl)
            : base(routedEvent)
        {
            this.ImageUrl = imageUrl;
        }
        #endregion
    }
}
