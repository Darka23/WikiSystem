using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WikiSystem.Data.Identity;

namespace WikiSystem.Data.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength =3)]
        public string Title { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public DateTime LastEditDate { get; set; }

        [Required]
        public string PublisherId { get; set; }

        public ApplicationUser Publisher { get; set; }
    }
}
