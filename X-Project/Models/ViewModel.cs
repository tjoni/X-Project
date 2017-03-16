using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace X_Project.Models
{
    public class ViewModel
    {
        public X_Project.Models.FacebookProfileModel _FacebookProfileModel{ get; set; }
        public IEnumerable<X_Project.Entities.WishListItem> _WishListItem { get; set; }
    }
}