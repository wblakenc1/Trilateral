using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
namespace TMID.App_Code{
    /// <summary>
    /// Contains my site's global variables.
    /// </summary>
    public static class GlobalDataItems
    {

        // Global variable 
        static string _cItem;

        // Get or set the data
        public static string cItem
        {
            get
            {
                return _cItem;
            }
            set
            {
                _cItem = value;
            }
        }
    }
}


