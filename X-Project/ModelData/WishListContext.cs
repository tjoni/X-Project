using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace X_Project.ModelData
{
    public class WishListContext : DbContext
    {
        public WishListContext() : base("aspnet-X-Project-20170228073820")
        {

        }
        public virtual DbSet<WishListItem> WishListItems { get; set; }
    }
}