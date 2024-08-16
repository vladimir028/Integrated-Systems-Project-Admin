using System.ComponentModel.DataAnnotations;

namespace AdminApplication.Models
{
    public class Movie
    {
        public Guid Id { get; set; }    
    
        public string MovieName { get; set; }
      
        public string MovieDescription { get; set; }
       
        public string MovieImage { get; set; }
       
        public double Rating { get; set; }
    }
}
