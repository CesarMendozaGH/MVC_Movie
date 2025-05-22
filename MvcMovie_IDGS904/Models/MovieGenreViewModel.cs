using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace MvcMovie_IDGS904.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public SelectList? Genres { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchString { get; set; } 
    
    }
    //contendra un modelo de vista que contendra una lista de generos en peliculas
}
