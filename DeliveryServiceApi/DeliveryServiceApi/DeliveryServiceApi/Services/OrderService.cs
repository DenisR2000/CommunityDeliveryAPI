 using System;
using DeliveryServiceApi.Interfaces;

namespace DeliveryServiceApi.Services
{
	public class OrderService : IOrderService
    {
        public bool IsFreeCourierAvaliable()
        {
            return true;
        }
    }
}

