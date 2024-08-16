using System.ComponentModel.DataAnnotations;

namespace AdminApplication.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }    
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Rating { get; set; }
    }
}
