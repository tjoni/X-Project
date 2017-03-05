using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace X_Project.Models
{
    public class FacebookProfileModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Fullname { get; set; }

        public string ImageURL { get; set; }

        public string LinkURL { get; set; }
    }
}