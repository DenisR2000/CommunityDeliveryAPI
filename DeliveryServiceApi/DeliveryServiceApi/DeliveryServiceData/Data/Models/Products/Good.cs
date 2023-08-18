using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using DeliveryServiceData.Data.Models.Products;
using DeliveryServiceData.Data.Relations;


namespace DeliveryServiceData.Data.Models
{
	public class Good
	{
        public Good()
        {
            GoodCategory = new HashSet<GoodCategory>();
            Photo = new HashSet<Photo>();
        }
        [Key]
        public int GoodId { get; set; }
		[Required]
		[MaxLength(250)]
		public string? Manufacturer { get; set; }
        [Required]
        [MaxLength(250)]
        public string? Title { get; set; }
        public string? Model { get; set; }
        [MaxLength(250)]
        public string? Country { get; set; } // Происхождение
        [Required]
        public decimal Price { get; set; }
        public int SalePersent { get; set; }

        public string Material { get; set; }
        public string Gender { get; set; }
        public string FabricСontent { get; set; }//Ткань содержание (51% (включительно)-70% (включительно))
        public string Season { get; set; } // Весна / лето
        public string FabricDensity { get; set; } // Плотность ткани
        public string ClothesPatterns { get; set; } // Одежда узоры (SLIM)
        public string Size { get; set; } // M S L XXL
        public string Color { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Photo> Photo { get; set; }
        public virtual ICollection<GoodCategory> GoodCategory { get; set; }
    }
}

