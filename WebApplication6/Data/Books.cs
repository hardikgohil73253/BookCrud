using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Data
{
    public class Books
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
