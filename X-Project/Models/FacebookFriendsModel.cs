﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace X_Project.Models
{
    public class FacebookFriendsModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public int WishListCount { get; set; }
    }
}