﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace X_Project.Entities
{
    public class WishListItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string WishUrl { get; set; }
        public string UserId { get; set; }
        public DateTime Time { get; set; }
    }
}