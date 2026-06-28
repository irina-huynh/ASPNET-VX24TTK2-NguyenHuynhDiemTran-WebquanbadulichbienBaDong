using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VX24TTK2.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        public bool IsLocked { get; set; }

        public string Phone { get; set; }

        public string Avatar { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<CustomerReview> CustomerReviews { get; set; }
    }
}