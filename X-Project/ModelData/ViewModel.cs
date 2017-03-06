using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace X_Project.ModelData
{
    public class ViewModel
    {
        public X_Project.Models.FacebookProfileModel _FacebookProfileModel { get; set; }
        public IEnumerable<X_Project.ModelData.WishListItem> _WishListItem { get; set; }
    }
}