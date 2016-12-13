using System.ComponentModel.DataAnnotations;
using System;

namespace loginAndRegistration.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string firstname { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string lastname { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [Compare(nameof(password))]
        public string passwordconfirmation { get; set;}
    }
}