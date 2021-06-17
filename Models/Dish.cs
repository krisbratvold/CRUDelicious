using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        [Required(ErrorMessage="is required")]
        public string Name { get; set; }
        [Required(ErrorMessage="is required")]
        public string Chef { get; set; }
        [Required(ErrorMessage="is required")]
        public int Tastiness { get; set; }
        [Required(ErrorMessage="is required")]
        [Range(1,100000,ErrorMessage="calories must be greater than 0")]
        public int Calories { get; set; }
        [Required(ErrorMessage="is required")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}