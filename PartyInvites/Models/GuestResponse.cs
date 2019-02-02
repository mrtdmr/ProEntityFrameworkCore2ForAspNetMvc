using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PartyInvites.Models
{
    public class GuestResponse
    {
        public long Id { get; set; }
        [Required]
        [Display(Name = "Your Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Your Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Your Phone Number")]
        public string Phone { get; set; }
        [Display(Name = "Will You Attend?")]
        public bool? WillAttend { get; set; }
    }
}
