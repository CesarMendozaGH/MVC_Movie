using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie_IDGS904.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [Required] //para que sea obligatorio
        public string? Title { get; set; } //CON LA INTERROGRACION HACE QUE PUEDA ACEPTAR NULOS
        [Display(Name ="Release Date")] //nueva datanotation para mostrar el nombre de la propiedad en la vista

        [DataType(DataType.Date)] //PARA PODER ESPECIFICAR QUE SEA TIPO FECHA HACEMOS LA SIGUIENTE 
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }

        [Column(TypeName ="decimal(18,2)")] //para especificar el tipo de dato en la base de datos
        public decimal Price { get; set; }
        
        public string? Rating { get; set; }

        //estas son llamadas clases POCO 
    }
}
