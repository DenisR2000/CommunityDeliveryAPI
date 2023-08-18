using System;
namespace DeliveryServiceData.Interfaces
{
	public interface ICloth
	{
		public string Material { get; set; }
		public string Gender { get; set; }
        public string FabricСontent { get; set; }//Ткань содержание (51% (включительно)-70% (включительно))
        public string Season { get; set; } // Весна / лето
        public string FabricDensity { get; set; } // Плотность ткани
        public string ClothesPatterns { get; set; } // Одежда узоры (SLIM)
        public string Size { get; set; } // M S L XXL
        public string Color { get; set; }
        public string Description { get; set; }
    }
}

