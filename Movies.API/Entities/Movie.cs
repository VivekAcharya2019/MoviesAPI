using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Entities
{
    [Table(name:"Movie")]
    public class Movie
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public string Genre { get; set; }
        public int DirectorId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Director Director { get; set; }
    }
}
