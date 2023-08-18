using System;
using DeliveryServiceData.Data.Models;

namespace DeliveryServiceApi.Data
{
	public class Cart
	{
		private List<GoodLine> lineCollection = new List<GoodLine>();
	}

	class GoodLine
	{
		public Good Good { get; set; }
        public int Quantity { get; set; }
    }
}

