using System;
using DeliveryServiceApi.Data.Contexts;
using DeliveryServiceApi.Data.Models;
using DeliveryServiceData.Data.Models;
using DeliveryServiceData.Data.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

namespace DeliveryServiceApi.Data
{
	public static class DataSeetExtensions
    {
        private static readonly string _mainAdminRoleName = "MainAdmin";
        private static readonly string _mainDeveloperRoleName = "Developer";
        private static readonly string _mainAdminPassword = "tolik050103SANDsand123%";


        public static async Task SeedData(this IApplicationBuilder builder, IServiceProvider servise)
        {
            var roleManager = servise.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = servise.GetRequiredService<UserManager<User>>();

            try
            {

                using (AppDbContext context = servise.GetRequiredService<AppDbContext>())
                {
                    //context.Goods.RemoveRange(context.Goods);
                    //context.Photos.RemoveRange(context.Photos);
                    //context.Categories.RemoveRange(context.Categories);
                    //await context.SaveChangesAsync();

                    var rolesExist = await roleManager.Roles.ToListAsync();
                    if (rolesExist.Count() == 0)
                    {
                        await roleManager.CreateAsync(new IdentityRole(_mainAdminRoleName));
                        await roleManager.CreateAsync(new IdentityRole("Developer"));
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                        await roleManager.CreateAsync(new IdentityRole("MainAdmin"));
                        await roleManager.CreateAsync(new IdentityRole("User"));
                        await context.SaveChangesAsync();
                    }

                    //User userToDelete = userManager.Users.FirstOrDefault(x => x.Id == "be869665-dd47-41b1-a14d-5e56a7b49c18");
                    //IdentityResult result1 = await userManager.DeleteAsync(userToDelete);
                    //User userToDelete2 = userManager.Users.FirstOrDefault(x => x.Id == "a68e9fe2-1c2a-4b8f-8371-247ddab5fcd4");
                    //IdentityResult result2 = await userManager.DeleteAsync(userToDelete2);

                    if (!userManager.Users.Any())
                    {
                        List<User> users = new List<User>();

                        User admin1 = new User { UserName = "RachkovskyiDenis", Email = "denisrachkovskyi@gmail.com" };
                        User admin2 = new User { UserName = "BulavaSemen", Email = "bulavasemen@gmail.com" };
                        users.Add(admin1);
                        users.Add(admin2);
                        foreach (var user in users)
                        {
                            var result_ = await userManager.CreateAsync(user, _mainAdminPassword);
                            if (result_.Succeeded)
                            {
                                await userManager.AddToRoleAsync(user, _mainDeveloperRoleName);
                            }
                            
                        }
                        await context.SaveChangesAsync();
                    }

                    ProductType productType = new()
                    {
                        Name = "Одежда"
                    };
                    ProductType productType1 = new()
                    {
                        Name = "Строительный материал"
                    };
                    ProductType productType2 = new()
                    {
                        Name = "Мебель"
                    };
                    ProductType productType3 = new()
                    {
                        Name = "Ткань"
                    };
                    if (!context.ProductTypes.Any())
                    {
                        context.ProductTypes.AddRange(
                            productType, productType1, productType2, productType3);
                        await context.SaveChangesAsync();
                    }

                    CategoryType categoryType = new()
                    {
                        Name = "Футболки",
                        ProductType = productType,
                    };
                    CategoryType categoryType1 = new()
                    {
                        Name = "Штаны",
                        ProductType = productType,
                    };
                    CategoryType categoryType2 = new()
                    {
                        Name = "Куртки",
                        ProductType = productType,
                    };
                    CategoryType categoryType3 = new()
                    {
                        Name = "Шапки",
                        ProductType = productType,
                    };
                    CategoryType categoryType4 = new()
                    {
                        Name = "Шифер",
                        ProductType = productType1,
                    };
                    CategoryType categoryType5 = new()
                    {
                        Name = "Черепица",
                        ProductType = productType1,
                    };
                    CategoryType categoryType6 = new()
                    {
                        Name = "Кирпич",
                        ProductType = productType1,
                    };
                    CategoryType categoryType7 = new()
                    {
                        Name = "Инструменты",
                        ProductType = productType1,
                    };
                    CategoryType categoryType8 = new()
                    {
                        Name = "Шкаф",
                        ProductType = productType2,
                    };
                    CategoryType categoryType9 = new()
                    {
                        Name = "Стол",
                        ProductType = productType2,
                    };
                    CategoryType categoryType10 = new()
                    {
                        Name = "Стул",
                        ProductType = productType2,
                    };
                    CategoryType categoryType11 = new()
                    {
                        Name = "Гарнитур",
                        ProductType = productType2,
                    };
                    CategoryType categoryType12 = new()
                    {
                        Name = "Шелк",
                        ProductType = productType3,
                    };
                    CategoryType categoryType13 = new()
                    {
                        Name = "Атлас",
                        ProductType = productType3,
                    };
                    CategoryType categoryType14 = new()
                    {
                        Name = "Хлопок",
                        ProductType = productType3,
                    };
                    CategoryType categoryType15 = new()
                    {
                        Name = "Бархат",
                        ProductType = productType3,
                    };
                    if (!context.CategoryTypes.Any())
                    {
                        context.CategoryTypes.AddRange(
                            categoryType, categoryType1, categoryType2, categoryType3,
                            categoryType4, categoryType5, categoryType6, categoryType7,
                            categoryType8, categoryType9, categoryType10, categoryType11,
                            categoryType12, categoryType13, categoryType14, categoryType15);
                    }


                    Brand brand = new()
                    {
                        BrandName = "Nike",
                        Description = "Nike® предлагает широкий ассортимент товаров для активного спортивного образа жизни. Поскольку каждый спортсмен хочет быть лучше, Nike может снабдить спортсмена сверху донизу высокоэффективной обувью, одеждой, носками, сумками, часами и очками. Поскольку современный спорт направлен на создание великих спортсменов и превращение их в великих игроков, Nike делает все возможное, чтобы сделать каждую тренировку более качественной, делая каждую тренировку значимой."
                    };
                    Category category = new()
                    {
                        ProductType = productType,
                        Name = "Футболки",
                    };
                    Category category1 = new()
                    {
                        ProductType = productType,
                        Name = "Футболки",
                    };
                    productType.Categories.Add(category);
                    productType.Categories.Add(category1);
                    Department department = new()
                    {
                        Name = "Мужской"
                    };
                    Department department1 = new()
                    {
                        Name = "Женский"
                    };
                    department.Categories.Add(category);
                    department1.Categories.Add(category1);


                    Good good = new Good()
                    {
                        Manufacturer = "Китай",
                        Description = "Прозорий органайзер для ватних дисків і паличок допомагає акуратно та зручно організувати ці корисні косметичні приладдя. Організатор для ватних дисків і паличок вироблений у лаконічному дизайні, підійде під будь-який інтер'єр спальні або ванної кімнати.",
                        Color = "Білий",
                        Model = "",
                        Title = "Органайзер для ватних дисків і паличок Supretto Прозорий",
                        Country = "Україна",
                        Price = 159.89,
                        Rate = 4,
                    };
                    Good good2 = new Good()
                    {
                        Manufacturer = "Китай",
                        Color = "Червоний",
                        Model = " ZH-02D",
                        Title = "Пензлик для завивання вій Xiaomi inFace ZH-02D",
                        Country = "Україна",
                        Price = 1239,
                        Rate = 4,
                        GoodCode = "jops678",
                        CompleteSet = "Пензлик для завивання вій\nUSB-кабель"
                    };
                    Good good_2 = new Good()
                    {
                        Manufacturer = "Китай",
                        Color = "Червоний",
                        Model = " ZH-02D",
                        Title = "Пензлик для завивання вій Xiaomi inFace ZH-02D",
                        Country = "Україна",
                        Price = 1239,
                        Rate = 4,
                        GoodCode = "jops678",
                        CompleteSet = "Пензлик для завивання вій\nUSB-кабель"
                    };
                    Good good3 = new Good()
                    {
                        Manufacturer = "Китай",
                        Color = "Білий",
                        Model = "",
                        Description = "Змінні стрічки для щипців для завивання вій Beter силіконові захищають вії та збільшують інтенсивність підкручування, надаючи віям потрібної форми. Використовувати тільки за призначенням.\n\nМатеріали: силікон.\nКраїна-виробник: Іспанія.",
                        Title = "Змінні стрічки для щипців для завивання вій Beter силіконові 5 шт",
                        Country = "Україна",
                        Price = 160,
                        Rate = 5,
                        GoodCode = "0987654",
                        CompleteSet = "Пензлик для завивання вій\nUSB-кабель"
                    };
                    Good good4 = new Good()
                    {
                        Manufacturer = "Іспанія",
                        Color = "Білий",
                        Model = "",
                        Gender = "Для жінок",
                        Description = "Щіпці для завивки",
                        Title = "Щипці для завивки вій Beter нікельовані 10.5 см ",
                        Country = "Україна",
                        Price = 160,
                        Rate = 5,
                        GoodCode = "123456",
                        CompleteSet = "1"
                    };
                    Good good6 = new Good()
                    {
                        Manufacturer = "Іспанія",
                        Color = "Білий",
                        Model = "",
                        Gender = "Для жінок",
                        Description = "Щіпці для завивки",
                        Title = "Щипці для завивки вій Beter нікельовані 10.5 см ",
                        Country = "Україна",
                        Price = 160,
                        Rate = 5,
                        GoodCode = "123456",
                        CompleteSet = "1"
                    };
                    if (!context.Goods.Any())
                    {
                        
                    }
                }
            }
			catch (Exception ex)
            {

            }
		}
	}
}

