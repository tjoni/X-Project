using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using X_Project.Entities;

namespace X_Project.Data
{
    public class WishListContext : DbContext
    {
        public WishListContext() : base ()
        {

        }
        public virtual DbSet<WishListItem> WishListItems { get; set; }
    }
}