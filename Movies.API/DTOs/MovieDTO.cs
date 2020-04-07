using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.DTOs
{
    public class MovieDTO
    {
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public string Genre { get; set; }
        public int DirectorId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
