using System;
using System.ComponentModel.DataAnnotations;

namespace VX24TTK2.Models
{
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}