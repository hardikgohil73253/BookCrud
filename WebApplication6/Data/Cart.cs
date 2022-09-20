using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication6.Models;

namespace WebApplication6.Data
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Books")]
        public int BookId { get; set; }

        public virtual Books Books { get; set; }

        [ForeignKey("ApplicationUser")]
        public virtual string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int NumberOfBook { get; set; }
    }
}
