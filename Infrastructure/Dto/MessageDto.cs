using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Dto
{
    public class MessageDto
    {
        [Display(Name = "Application number")]
        public int Id {get; set;}

        [Required(ErrorMessage = "First Name Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }



        [Required(ErrorMessage = "Email Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "MessageEmailRegularExpression")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string EmailTo { get; set; }

        [Display(Name = "EmailCc")]
        [EmailAddress]
        public string EmailCc { get; set; }

        // [Required(ErrorMessage = "Topic Required")]
        [Display(Name = "Topic")]

        public int TopicId { get; set; }

        // [Required(ErrorMessage = "Text Required")]
        [Display(Name = "Text")]
        public string MessageText { get; set; }


        [Display(Name = "Send DateTime")]
        public DateTime SendDateTime { get; set; }
    }
}
