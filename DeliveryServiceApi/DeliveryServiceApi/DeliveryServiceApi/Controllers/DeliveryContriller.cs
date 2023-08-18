using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using DeliveryServiceApi.Data.Contexts;
using DeliveryServiceApi.Interfaces;
using DeliveryServiceData.Data.Models;
using DeliveryServiceData.Data.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeliveryServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryContriller : Controller
    {
        
        private readonly IOrderService _orderService;
        private readonly AppDbContext _context;

        public DeliveryContriller(IOrderService orderService, AppDbContext context)
        {
            _context = context;
            _orderService = orderService;
        }

        [HttpGet("check-status")]
        public IActionResult CheckStatus()
        {
            return Ok("Hello integtation tests");
        }

        [HttpPost("send-order")]
        public IActionResult SendOrder()
        {
            try
            {
                if (_orderService.IsFreeCourierAvaliable())
                    return Ok("Order has been sent");
                return NotFound("Avaliable curier not found");
            }catch
            {
                return BadRequest("BadRequest Curier avaliable");
            }
        }

        [HttpGet("orders-count")]
        public IActionResult GetOrderCount()
        {
            try
            {
                Good good = _context.Goods.FirstOrDefault();
                var responce = good;//.Category.ToList();
                var remaindata = JsonConvert.SerializeObject(responce,
                              new JsonSerializerSettings()
                              {
                                  ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                              });
                return Ok(remaindata);
            }
            catch(Exception ex)
            {
            }
            return Ok("");
        }
    }
}

