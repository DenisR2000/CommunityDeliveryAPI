using System;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using DeliveryServiceApi.Interfaces;
using Moq;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DeliveryServiceApi.Data;
using DeliveryServiceApi.Data.Models;

namespace DeliveryServiceApi.integrationTests
{
	[TestFixture]
	public class DeliveryControllerTests
	{
		[Test]
		public async Task CheckStatus_SendReqestShouldReturnOk()
		{
			WebApplicationFactory<Program> webHost =
				new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });

			HttpClient httpClient = webHost.CreateClient();

			HttpResponseMessage responseMessage = await httpClient.GetAsync("api/DeliveryContriller/check-status");

			Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
		}

		[Test]
		public async Task SendFreeCuried_Sould_NotFound()
		{
			WebApplicationFactory<Program> webHost =
				new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
				{
					builder.ConfigureTestServices(services =>
					{
						var orderService = services.SingleOrDefault(d => d.ServiceType == typeof(IOrderService));

						services.Remove(orderService);

						var mockService = new Mock<IOrderService>();

                        mockService.Setup(_ => _.IsFreeCourierAvaliable()).Returns(() => false);

						services.AddTransient(_ => mockService.Object);
					});
				});

            HttpClient httpClient = webHost.CreateClient();

            HttpResponseMessage responseMessage = await httpClient.PostAsync("api/DeliveryContriller/send-order", null);

			Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }

		[Test]
		public async Task GetOrdersCount_ShouldReturnActualOrdersCount()
		{
            // Arrage
            WebApplicationFactory<Program> webHost =
				new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
				{
					builder.ConfigureTestServices(services =>
					{
						var context = services.SingleOrDefault(d =>
						d.ServiceType == typeof(DbContextOptions<AppDbContext>));

						services.Remove(context);

						services.AddDbContext<AppDbContext>(options =>
						{
							options.UseInMemoryDatabase("delivery_db");
						});
					});
				});

            AppDbContext dbContext =
                 webHost.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();

			List<Order> orders = new List<Order> { new Order(), new Order(), new Order() };

			await dbContext.Orders.AddRangeAsync(orders);

			await dbContext.SaveChangesAsync();

            HttpClient httpClient = webHost.CreateClient();

            // Act
            HttpResponseMessage responseMessage = await httpClient.GetAsync("api/DeliveryContriller/orders-count");

			// Assert
			Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);

			int ordersCount = int.Parse(await responseMessage.Content.ReadAsStringAsync());

			Assert.AreEqual(orders.Count, ordersCount);
        }
    }
}

